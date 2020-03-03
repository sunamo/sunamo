using sunamo.Helpers.DT;
using System;
using System.Collections.Generic;
using System.Text;

public partial class DTHelperMulti
{
    #region Other
    /// <summary>
    /// If A1 could be lower than 1d, return 1d
    /// </summary>
    /// <param name="ts"></param>
    /// <param name="calculateTime"></param>
    
    public static DateTime? ParseDateMonthDayYear(string p)
    {
        var s = SH.SplitNone(p, AllStrings.slash);
        if (s.Count == 1)
        {
            s = SH.SplitNone(p, AllStrings.dot);
            DateTime vr;
            if (DateTime.TryParse(s[0] + AllStrings.dot + s[1] + AllStrings.dot + s[2], out vr))
            {
                return vr;
            }
        }
        else
        {
            DateTime vr;
            if (DateTime.TryParse(s[1] + AllStrings.dot + s[0] + AllStrings.dot + s[2], out vr))
            {
                return vr;
            }
        }
        return null;
    } 
    #endregion
}