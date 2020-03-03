using System;
using System.Collections.Generic;
using System.Text;

public partial class DTHelperCode
{
    #region ToString
    #region Time (with seconds)
    /// <summary>
    /// 12:00:00
    /// </summary>
    /// <param name="dt"></param>
    public static string TimeToStringAngularTime(DateTime dt)
    {
        return NH.MakeUpTo2NumbersToZero(dt.Hour) + AllStrings.colon + NH.MakeUpTo2NumbersToZero(dt.Minute) + AllStrings.colon + NH.MakeUpTo2NumbersToZero(dt.Second);
    }
    #endregion

    #region Date and time (with seconds)
    /// <summary>
    /// 19890621T00:00:00
    /// </summary>
    /// <param name="dt"></param>
    public static string DateToStringAngularDate(DateTime dt)
    {
        return dt.Year + NH.MakeUpTo2NumbersToZero(dt.Month) + NH.MakeUpTo2NumbersToZero(dt.Day) + "T00:00:00";
    } 
    #endregion
    #endregion
}