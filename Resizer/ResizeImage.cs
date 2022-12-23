using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace Resizer
{
    public class ResizeImage
    {
        private int cursorTop;
        private InterpolationMode mode;

        public ResizeImage(int mode)
        {
            this.cursorTop = Console.CursorTop + 1;
            this.mode = (InterpolationMode) mode;
        }
        
        public async Task ResizeAspect(string sourceFile, string destFile, int width, int height)
        {
            await Task.Run(() => {
                try
                {
                    using (Image image = Image.FromFile(sourceFile))
                    {
                        float scale = Math.Min((float)width / (float)image.Width, (float)height / (float)image.Height);

                        int wScale = (int)(image.Width * scale);
                        int hScale = (int)(image.Height * scale);

                        using (FileStream fs = new FileStream(destFile, FileMode.OpenOrCreate, FileAccess.ReadWrite))
                        using (Bitmap bitmap = new Bitmap(wScale, hScale))
                        using (Graphics g = Graphics.FromImage(bitmap))
                        {
                            using (ImageAttributes wrapMode = new ImageAttributes())
                            {
                                wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                                g.InterpolationMode = this.mode;
                                g.DrawImage(image, new Rectangle(0, 0, wScale, hScale), 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);

                                bitmap.Save(fs, ImageFormat.Png);
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            });
        }

    }
}