using sunamo.Essential;
using System;
using System.Collections.Generic;

namespace shared.Essential
{
    public class ConsoleLogger
    {
        static LogHelper logHelper = new LogHelper(Console.WriteLine);

        public static void WriteLine(string what, object text)
        {
            logHelper.WriteLine(what, text);
        }

        public static void WriteNumberedList(string what, List<string> list, bool numbered)
        {
            logHelper.WriteNumberedList(what, list, numbered);
        }

        public static void WriteList(List<string> list)
        {
            list.ForEach(d => logHelper.WriteLine(d));
        }

        public static void WriteLine(string text)
        {
            logHelper.WriteLine( text);
        }
    }
}
