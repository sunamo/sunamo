using sunamo.Essential;
using System;
using System.Collections.Generic;

namespace cmd.Essential
{
    public class ConsoleLogger : LoggerBase
    {
        public static ConsoleLogger Instance = new ConsoleLogger(Console.WriteLine);

        public ConsoleLogger(VoidStringParamsObjects writeLineHandler) : base(writeLineHandler)
        {

        }

        #region Change color of Console

        public static void ChangeColorOfConsoleAndWrite(TypeOfMessage tz, string text, params object[] args)
        {
            SetColorOfConsole(tz);
            
            Console.WriteLine(SH.Format( text, args));
            SetColorOfConsole(TypeOfMessage.Ordinal);
        }

        private static void SetColorOfConsole(TypeOfMessage tz)
        {
            ConsoleColor bk = ConsoleColor.White;

            switch (tz)
            {
                case TypeOfMessage.Error:
                    bk = ConsoleColor.Red;
                    break;
                case TypeOfMessage.Warning:
                    bk = ConsoleColor.Yellow;
                    break;
                case TypeOfMessage.Information:

                case TypeOfMessage.Ordinal:
                    bk = ConsoleColor.White;
                    break;
                case TypeOfMessage.Appeal:
                    bk = ConsoleColor.Magenta;
                    break;
                case TypeOfMessage.Success:
                    bk = ConsoleColor.Green;
                    break;
                default:
                    throw new Exception("Neinplementovana vetev");
                    break;
            }
            if (bk != ConsoleColor.Black)
            {
                Console.ForegroundColor = bk;
            }
            else
            {
                Console.ResetColor();
            }
        }

        #endregion

        public static void WriteMessage(TypeOfMessage typeOfMessage, string text, params object[] args)
        {
            ChangeColorOfConsoleAndWrite(typeOfMessage, text, args);
        }
    }
}
