using sunamo.Enums;
using System;

public partial class DTHelperFormalized
{
    #region ToString
    #region Date with time (without seconds)
    /// <summary>
    /// 2011-10-18 10:30
    /// </summary>
    /// <param name="dt"></param>
    /// <param name="fullCalendar"></param>
    
    public static string DateTimeToStringFormalizeDate(DateTime dt)
    {
        return dt.Year + AllStrings.dash + NH.MakeUpTo2NumbersToZero(dt.Month) + AllStrings.dash + NH.MakeUpTo2NumbersToZero(dt.Day);
    }  
    #endregion
    #endregion
}