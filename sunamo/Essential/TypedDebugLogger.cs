using System;
using System.Collections.Generic;
using System.Text;

// Cant be DEBUG, in dependent assembly often dont see this classes even if all projects is Debug
//#if DEBUG
namespace sunamo.Essential
{
    public class TypedDebugLogger : TypedLoggerBase
    {
#if DEBUG //2
        public static TypedDebugLogger Instance = new TypedDebugLogger();

        private TypedDebugLogger() : base(DebugLogger.DebugWriteLine)
        {
        }
#endif
    }
}
//#endif