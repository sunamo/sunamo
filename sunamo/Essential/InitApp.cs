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
            InitApp.Logger = DebugLogger.Instance;
#if DEBUG
            InitApp.TemplateLogger = DebugTemplateLogger.Instance;
#endif
            InitApp.TypedLogger = TypedDebugLogger.Instance;
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
