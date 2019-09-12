using System;
using System.Collections.Generic;
using System.Text;

// Cant be DEBUG, in dependent assembly often dont see this classes even if all projects is Debug
//#if DEBUG
namespace sunamo.Essential
{
    public class DebugTemplateLogger : TemplateLoggerBase
    {
        public static DebugTemplateLogger Instance = new DebugTemplateLogger();

        private DebugTemplateLogger() : base(DebugLogger.DebugWriteLine)
        {
        }
    }
}
//#endif