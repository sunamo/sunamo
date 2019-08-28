using System;

public static partial class AspNet
{
    private const string error = "error" + ":";
    private const string warning = "warning" + ":";
    private const string success = "success" + ":";
    private const string info = "info" + ":";
    private const string information = "information" + ":";
    private const string appeal = "appeal" + ":";

    /// <summary>
    /// If dont start with none, return Ordinal
    /// </summary>
    /// <param name = "resp"></param>
    /// <returns></returns>
    public static TypeOfMessage IsStatusMessage(ref string resp)
    {
        if (SH.TrimIfStartsWith(ref resp, error))
        {
            return TypeOfMessage.Error;
        }
        else if (SH.TrimIfStartsWith(ref resp, warning))
        {
            return TypeOfMessage.Warning;
        }
        else if (SH.TrimIfStartsWith(ref resp, success))
        {
            return TypeOfMessage.Success;
        }
        else if (SH.TrimIfStartsWith(ref resp, info))
        {
            return TypeOfMessage.Information;
        }
        else if (SH.TrimIfStartsWith(ref resp, information))
        {
            return TypeOfMessage.Information;
        }
        else if (SH.TrimIfStartsWith(ref resp, appeal))
        {
            return TypeOfMessage.Appeal;
        }

        return TypeOfMessage.Ordinal;
    }


}