
using System;
/// <summary>
/// Methods which in string result have T as delimiter date and time
/// Next relative methods are in DTHelperFormalized / DTHelperCode
/// </summary>
public partial class DTHelperFormalizedWithT
{
    #region ToString
    /// <summary>
    /// 1989-06-21T00:00:00.000Z (Z/TZD/+hh:mm/-hh:mm - timezone designation)
    /// </summary>
    /// <param name="dt"></param>
    public static string DateTimeToStringStringifyDateEmptyTime(DateTime dt)
    {
        return dt.Year + AllStrings.dash + NH.MakeUpTo2NumbersToZero(dt.Month) + AllStrings.dash + NH.MakeUpTo2NumbersToZero(dt.Day) + "T00:00:00.000Z";
    }

    /// <summary>
    /// 1989-06-21Thh:mm:ss.000Z
    /// </summary>
    /// <param name="dt"></param>
    public static string DateTimeToStringStringifyDateTime(DateTime dt)
    {
        return dt.Year + AllStrings.dash + NH.MakeUpTo2NumbersToZero(dt.Month) + AllStrings.dash + NH.MakeUpTo2NumbersToZero(dt.Day) + "T" + NH.MakeUpTo2NumbersToZero(dt.Hour) + AllStrings.colon + NH.MakeUpTo2NumbersToZero(dt.Minute) + AllStrings.colon + NH.MakeUpTo2NumbersToZero(dt.Second) + AllStrings.dot + NH.MakeUpTo3NumbersToZero(dt.Millisecond) + "Z";
    }

    /// <summary>
    /// 1989-06-21T11:22:00
    /// </summary>
    /// <param name = "dt"></param>
    public static string DateAndTimeToStringFormalizeDate(DateTime dt)
    {
        return dt.Year + AllStrings.dash + NH.MakeUpTo2NumbersToZero(dt.Month) + AllStrings.dash + NH.MakeUpTo2NumbersToZero(dt.Day) + "T" + NH.MakeUpTo2NumbersToZero(dt.Hour) + AllStrings.colon + NH.MakeUpTo2NumbersToZero(dt.Minute) + AllStrings.colon + NH.MakeUpTo2NumbersToZero(dt.Second);
    } 
    #endregion
}