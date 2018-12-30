using System;
using System.Collections.Generic;
using System.Text;

namespace sunamo.Essential
{
    /// <summary>
    /// Special class for variables which must be init on startup of every app
    /// </summary>
    public class InitApp
    {
        

        #region Must be set during app initializing
        public static IClipboardHelper Clipboard
        {
            set
            {
                ClipboardHelper.Instance = value;
            }
        }
        public static LoggerBase Logger = null;
        public static TypedLoggerBase TypedLogger = null;
        public static TemplateLoggerBase TemplateLogger = null;
        #endregion
    }
}
