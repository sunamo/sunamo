using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace sunamo.Essential
{
    public class SunamoLogger : LoggerBase
    {
        public static SunamoLogger Instance = new SunamoLogger(WriteLine);

        public SunamoLogger(VoidStringParamsObjects writeLineHandler) : base(writeLineHandler)
        {
        }

        private static void WriteLine(string text, params object[] args)
        {
            ThisApp.SetStatus(TypeOfMessage.Ordinal, text, args);
        }
    }
}