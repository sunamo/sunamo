public static partial class AspNet{
    const string error = "error:";
    const string warning = "warning:";
    const string success = "success:";
    const string info = "info:";
    const string information = "information:";
    const string appeal = "appeal:";
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