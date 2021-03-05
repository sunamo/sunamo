using System;
using System.Collections.Generic;
using System.Text;
using sunamo.Essential;

public class SunamoTemplateLogger : TemplateLoggerBase
    {
        public static SunamoTemplateLogger Instance = new SunamoTemplateLogger();

        private SunamoTemplateLogger() : base(ThisApp.SetStatus)
        {
        }

   
}