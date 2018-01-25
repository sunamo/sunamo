using sunamo.Essential;
using System;
using System.Collections.Generic;

namespace sunamo.Essential
{
    public class ConsoleLogger : LoggerBase
    {
        public static ConsoleLogger Instance = new ConsoleLogger(Console.WriteLine);

        public ConsoleLogger(VoidString writeLineHandler) : base(writeLineHandler)
        {

        }
    }
}
