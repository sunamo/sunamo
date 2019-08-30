using cmd.Essential;
using sunamo.Essential;

public partial class CmdApp{
    /// <summary>
    /// Dont ask in console, load from Clipboard
    /// </summary>
    public static bool loadFromClipboard = false;

    /// <summary>
    /// Alternatives are:
    /// InitApp.SetDebugLogger
    /// CmdApp.SetLogger
    /// WpfApp.SetLogger
    /// </summary>
    public static void SetLogger()
    {
        InitApp.Logger = ConsoleLogger.Instance;
        InitApp.TemplateLogger = ConsoleTemplateLogger.Instance;
        InitApp.TypedLogger = TypedConsoleLogger.Instance;
    }
}