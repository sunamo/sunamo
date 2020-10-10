using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public partial class StatusHelperSunamo{ 
    public static TypeOfMessage IsStatusMessage(string resp)
    {
        var r = resp;
        return IsStatusMessage(ref r);
    }

    /// <summary>
    /// If dont start with none, return Ordinal
    /// </summary>
    /// <param name = "resp"></param>
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