
using sunamo;
using sunamo.Enums;
using sunamo.Helpers.DT;
using System;
using System.Collections.Generic;
using System.Diagnostics;


/// <summary>
/// 
/// </summary>
public partial class DTHelper
{
    #region ToString
    /// <summary>
    /// mm/dd/yyyy
    /// </summary>
    /// <param name="dt"></param>
    
    public static string AddRightStringToTimeSpan(TimeSpan tt, bool calculateTime, Langs l)
    {
        return DTHelperMulti.AddRightStringToTimeSpan(tt, calculateTime, l);
    }

    public static string OperationLastedInLocalizateString(TimeSpan tt, Langs l)
    {
        return DTHelperMulti.OperationLastedInLocalizateString(tt, l);
    }


    public static string TimeInMsToSeconds(Stopwatch p)
    {
        return DTHelperGeneral.TimeInMsToSeconds(p);
    }

    public static DateTime TodayPlusActualHour()
    {
        return DTHelperGeneral.TodayPlusActualHour();
    }

    public static long DateTimeToSecondsOnlyTime(DateTime t)
    {
        return DTHelperGeneral.DateTimeToSecondsOnlyTime(t);
    }

    public static string CalculateAgeAndAddRightString(DateTime dateTime, bool calculateTime, DateTime dtMinVal)
    {
        return DTHelperCs.CalculateAgeAndAddRightString(dateTime, calculateTime, dtMinVal);
    }

    public static string DayOfWeek2DenVTydnu(DayOfWeek dayOfWeek)
    {
        return DTHelperCs.DayOfWeek2DenVTydnu(dayOfWeek);
    }
    #endregion
}