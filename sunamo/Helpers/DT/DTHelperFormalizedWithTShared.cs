using System;

/// <summary>
/// Use W3C datetime format 
/// https://www.w3.org/TR/NOTE-datetime
/// 
/// </summary>
public partial class DTHelperFormalizedWithT{

    #region ToString
    /// <summary>
    /// yyyy-mm-ddT00:00:00 (without timezone)
    /// 
    /// </summary>
    /// <param name = "dt"></param>
    public static string DateTimeToStringFormalizeDateEmptyTime(DateTime dt)
    {
        return dt.Year + AllStrings.dash + NH.MakeUpTo2NumbersToZero(dt.Month) + AllStrings.dash + NH.MakeUpTo2NumbersToZero(dt.Day) + "T00:00:00";
    } 
    #endregion
}