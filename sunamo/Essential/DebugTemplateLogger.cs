using System;
using System.Collections.Generic;
using System.Text;

// Cant be DEBUG, in dependent assembly often dont see this classes even if all projects is Debug
//#if DEBUG
namespace sunamo.Essential
{
#if DEBUG2
    public class DebugTemplateLogger : TemplateLoggerBase
    {
        static DebugTemplateLogger instance = null;

        public static DebugTemplateLogger Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DebugTemplateLogger();
                }

                return instance;
            }
        }

        private DebugTemplateLogger() : base(DebugLogger.DebugWriteLine)
        {
        }
    }
#endif
}
//#endif