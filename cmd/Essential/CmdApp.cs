
using sunamo.Essential;

public partial class CmdApp
{

    public static void EnableDesktopLogging(bool v)
    {
        if (v)
        {
            // because method was called two times 
            ThisApp.StatusSetted -= ThisApp_StatusSetted;
            ThisApp.StatusSetted += ThisApp_StatusSetted;
        }
        else
        {
            ThisApp.StatusSetted -= ThisApp_StatusSetted;
        }
    }

    private static void ThisApp_StatusSetted(TypeOfMessage t, string message)
    {
        InitApp.TypedLogger.WriteLine(t, message);
    }
}