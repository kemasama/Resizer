namespace Resizer
{
    public class LoggerService
    {
        private static object lockObj = new object();

        public static void WriteLine(int left, int top, string message, params object[] parameters)
        {
            lock (lockObj)
            {
                Console.CursorLeft = left;
                Console.CursorTop = top;
                Console.WriteLine(message, parameters);
            }
        }

        public static void WriteLine(string message, params object[] parameters)
        {
            lock (lockObj)
            {
                Console.WriteLine(message, parameters);
            }
        }
    }
}