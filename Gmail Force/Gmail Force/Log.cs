using System;

namespace Gmail_Force
{
    public static class Log
    {
        public enum LogType
        {
            Default,
            Error,
            Success
        }
        public static void Write(string Text, LogType LogType)
        {
            switch(LogType)
            {
                case LogType.Default:
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    break;
                case LogType.Error:
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    break;                  
                case LogType.Success:
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    break;
            }

            string t = "[" + DateTime.Now.ToLongTimeString() + "] ";
            Console.WriteLine(t + Text);
            Console.ResetColor();
        }
    }
}
