
using cmd.Essential;
using sunamo.Essential;

public partial class CmdApp
{


    public static TypedLoggerBase ConsoleOrDebugTyped()
    {
#if DEBUG
        return TypedDebugLogger.Instance;
#elif !DEBUG
        return TypedConsoleLogger.Instance;
#endif
    }
}