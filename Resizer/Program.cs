using System.Drawing.Drawing2D;

namespace Resizer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            int width = 1280;
            int height = 720;
            int mode = (int) InterpolationMode.Bicubic;

            ArgumentService argument = new ArgumentService(args);
            string target = argument.Get("-target");
            string output = argument.Get("-output");

            if (string.IsNullOrEmpty(target) || string.IsNullOrEmpty(output))
            {
                Console.WriteLine("Argument required.");
                Console.WriteLine("Resizer [-target <target>] [-output <output>]");
                return;
            }

            string tWidth = argument.Get("-width");
            if (!string.IsNullOrEmpty(tWidth) && int.TryParse(tWidth, out int tWidthNumber))
            {
                width = tWidthNumber;
            }

            string tHeight = argument.Get("-height");
            if (!string.IsNullOrEmpty(tHeight) && int.TryParse(tHeight, out int tHeightNumber))
            {
                height = tHeightNumber;
            }

            string tMode = argument.Get("-mode");
            if (!string.IsNullOrEmpty(tMode) && int.TryParse(tMode, out int tModeNumber))
            {
                try
                {
                    InterpolationMode iMode = (InterpolationMode) Enum.ToObject(typeof(InterpolationMode), tModeNumber);
                    mode = (int) iMode;
                }
                catch (Exception)
                {
                    // catch exceptions.
                }
            }

            Console.WriteLine("[Arguments]");
            Console.WriteLine(" Source Folder: {0}", target);
            Console.WriteLine(" Output Folder: {0}", output);
            Console.WriteLine(" Image  Width : {0}", width);
            Console.WriteLine(" Image  Height: {0}", height);
            Console.WriteLine(" Drawer   Mode: {0}", (InterpolationMode) mode);
            Console.WriteLine();

            ResizeService service = new ResizeService(target, output, width, height, mode);
            service.Resize();
        }
    }
}