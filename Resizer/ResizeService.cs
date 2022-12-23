using System.Collections.Generic;

namespace Resizer
{
    public class ResizeService
    {
        private int animateFlag;
        private int cursorTop;

        private string targetDirectory;
        private string outputDirectory;

        private int width;
        private int height;
        private int mode;

        public ResizeService(string targetDirectory, string outputDirectory, int width, int height, int mode)
        {
            this.animateFlag = 0;

            this.targetDirectory = targetDirectory;
            this.outputDirectory = outputDirectory;

            this.width = width;
            this.height = height;
            this.mode = mode;
        }

        public void Resize()
        {
            List<Task> tasks = new List<Task>();

            try
            {
                DirectoryInfo root = new DirectoryInfo(this.targetDirectory);
                int x = 1;

                Console.WriteLine("[Resizer]");
                this.cursorTop = Console.CursorTop;
                ResizeImage resizer = new ResizeImage(this.mode);

                foreach (FileInfo file in root.EnumerateFiles())
                {
                    string destPath = Path.Combine(this.outputDirectory, (x.ToString().PadLeft(4, '0')) + ".png");
                    tasks.Add(resizer.ResizeAspect(file.FullName, destPath, width, height));
                    x++;
                }

                bool pending = true;
                while (pending)
                {
                    pending = false;
                    foreach (Task task in tasks)
                    {
                        if (!task.IsCompleted)
                        {
                            pending = true;
                        }
                    }

                    AnimateConsole();
                    Thread.Sleep(300);
                }

                Console.CursorTop = this.cursorTop;
                Console.CursorLeft = 0;
                Console.WriteLine(" ===========>  Done :D  <===========");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private void AnimateConsole()
        {
            string[] frames = new string[]
            {
                @"\ Resizing...",
                @"| Resizing...",
                @"/ Resizing...",
                @"- Resizing...",
            };

            string frame = frames[animateFlag++ % frames.Length];

            Console.CursorTop = this.cursorTop;
            Console.CursorLeft = 0;
            Console.Write(frame);
        }
    }
}