using System;
using System.Collections.Generic;
using System.Text;

#if DEBUG
namespace sunamo.Essential
{
    public class DebugTemplateLogger : TemplateLoggerBase
    {
        public static DebugTemplateLogger Instance = new DebugTemplateLogger();

        private DebugTemplateLogger() : base(DebugLogger.DebugWriteMessage)
        {

        }
    }
}
#endif