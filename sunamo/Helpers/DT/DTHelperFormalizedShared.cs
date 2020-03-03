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
    public static string FormatDateTime(DateTime dt, DateTimeFormatStyles fullCalendar)
    {
        if (fullCalendar == DateTimeFormatStyles.FullCalendar)
        {
            //2011-10-18 10:30
            return dt.Year + AllStrings.dash + NH.MakeUpTo2NumbersToZero(dt.Month) + AllStrings.dash + NH.MakeUpTo2NumbersToZero(dt.Day) + AllStrings.space + NH.MakeUpTo2NumbersToZero(dt.Hour) + AllStrings.colon + NH.MakeUpTo2NumbersToZero(dt.Minute);
        }

        return "";
    }
    #endregion

    #region Date
    /// <summary>
    /// 1989-06-21
    /// </summary>
    /// <param name = "dt"></param>
    public static string DateTimeToStringFormalizeDate(DateTime dt)
    {
        return dt.Year + AllStrings.dash + NH.MakeUpTo2NumbersToZero(dt.Month) + AllStrings.dash + NH.MakeUpTo2NumbersToZero(dt.Day);
    }  
    #endregion
    #endregion
}