using sunamo.Essential;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cmd.Essential
{
    public class TypedConsoleLogger : TypedLoggerBase
    {
        public static TypedConsoleLogger Instance = new TypedConsoleLogger();

        private TypedConsoleLogger() : base(ConsoleLogger.ChangeColorOfConsoleAndWrite)
        {

        }
        

    }
}
