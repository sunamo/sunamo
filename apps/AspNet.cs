public static class AspNet
{

    public static TypeOfMessage IsStatusMessage(string resp)
    {
        if (resp.StartsWith("error:"))
        {
            return TypeOfMessage.Error;
        }
        else if (resp.StartsWith("warning:"))
        {
            return TypeOfMessage.Warning;
        }
        else if (resp.StartsWith("success:"))
        {
            return TypeOfMessage.Success;
        }
        else if (resp.StartsWith("info:"))
        {
            return TypeOfMessage.Information;
        }
        else if (resp.StartsWith("information:"))
        {
            return TypeOfMessage.Information;
        }
        else if (resp.StartsWith("appeal:"))
        {
            return TypeOfMessage.Appeal;
        }
        return TypeOfMessage.Ordinal;
    }
}
