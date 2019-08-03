using System;
using System.Collections.Generic;
using System.Text;

namespace sunamo.Essential
{
    public class TypedDebugLogger : TypedLoggerBase
    {
        public static TypedDebugLogger Instance = new TypedDebugLogger();

        private TypedDebugLogger() : base(DebugLogger.DebugWriteLine)
        {
        }
    }
}
