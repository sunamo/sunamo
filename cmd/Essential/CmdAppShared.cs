using cmd.Essential;
using sunamo.Essential;
using System;

public partial class CmdApp{
    public static void Init()
    {
        AppDomain.CurrentDomain.UnhandledException += UnhandledExceptionTrapper;
    }

    static void UnhandledExceptionTrapper(object sender, UnhandledExceptionEventArgs e)
    {
        string dump = null;
        //dump = YamlHelper.DumpAsYaml(e);
        dump = SunamoJson.SerializeObject(e, true);

        ThisApp.SetStatus(TypeOfMessage.Error, e.ExceptionObject.ToString());
        WriterEventLog.WriteToMainAppLog(dump, System.Diagnostics.EventLogEntryType.Error);
    }

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