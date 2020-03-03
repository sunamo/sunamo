using System;
using System.Collections.Generic;
using System.Text;

namespace sunamo.Essential
{
    /// <summary>
    /// Special public class for variables which must be init on startup of every app
    /// </summary>
    public class InitApp
    {
        /// <summary>
        /// Alternatives are:
        /// InitApp.SetDebugLogger
        /// CmdApp.SetLogger
        /// WpfApp.SetLogger
        /// </summary>
        public static void SetDebugLogger()
        {

#if DEBUG
            InitApp.Logger = DebugLogger.Instance;
            InitApp.TemplateLogger = DebugTemplateLogger.Instance;
            InitApp.TypedLogger = TypedDebugLogger.Instance;
#endif
        }

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