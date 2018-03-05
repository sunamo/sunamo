using sunamo.Essential;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shared.Essential
{
    public class ConsoleTemplateLogger : TemplateLoggerBase
    {
        public static ConsoleTemplateLogger Instance = new ConsoleTemplateLogger();

        private ConsoleTemplateLogger() : base(ConsoleLogger.WriteMessage)
        {

        }
    }
}
