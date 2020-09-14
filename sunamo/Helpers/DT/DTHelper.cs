
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
    public static string DateToStringjQueryDatePicker(DateTime dt)
    {
        return DTHelperCode.DateToStringjQueryDatePicker(dt);
    }

    /// <summary>
    /// 19890621T11:22:00
    /// </summary>
    /// <param name="dt"></param>
    public static string DateAndTimeToStringAngularDateTime(DateTime dt)
    {
        return DTHelperCode.DateAndTimeToStringAngularDateTime(dt);
    }

    /// <summary>
    /// 1989-06-21T11:22
    /// </summary>
    /// <param name="dt"></param>
    /// <param name="dtMinVal"></param>
    public static string DateTimeToStringToInputDateTimeLocal(DateTime dt, DateTime dtMinVal)
    {
        return DTHelperCode.DateTimeToStringToInputDateTimeLocal(dt, dtMinVal);
    }

    /// <summary>
    /// Return actual time(for example 12:00:00:000) and after that A1 postfix
    /// </summary>
    /// <param name="defin"></param>
    public static string AppendToFrontOnlyTime(string defin)
    {
        return DTHelperCs.AppendToFrontOnlyTime(defin);
    }

    /// <summary>
    /// Wednesday, 21.6.1989 11:22 (dont fill with zero)
    /// </summary>
    /// <param name="dt"></param>
    public static string DateTimeToStringWithDayOfWeekCS(DateTime dt)
    {
        return DTHelperCs.DateTimeToStringWithDayOfWeekCS(dt);
    }

    /// <summary>
    /// 1989-06-21
    /// </summary>
    /// <param name="dt"></param>
    public static string DateTimeToStringFormalizeDate(DateTime dt)
    {
        return DTHelperFormalized.DateTimeToStringFormalizeDate(dt);
    }

    /// <summary>
    /// 2011-10-18 10:30
    /// </summary>
    /// <param name="dt"></param>
    /// <param name="fullCalendar"></param>
    public static string FormatDateTime(DateTime dt, DateTimeFormatStyles fullCalendar)
    {
        return DTHelperFormalized.FormatDateTime(dt, fullCalendar);
    }

    /// <summary>
    /// yyyy-mm-ddT00:00:00
    /// </summary>
    /// <param name="dt"></param>
    public static string DateTimeToStringFormalizeDateEmptyTime(DateTime dt)
    {
        return DTHelperFormalizedWithT.DateTimeToStringFormalizeDateEmptyTime(dt);
    }

    /// <summary>
    /// 1989-06-21T00:00:00.000Z
    /// </summary>
    /// <param name="dt"></param>
    public static string DateTimeToStringStringifyDateEmptyTime(DateTime dt)
    {
        return DTHelperFormalizedWithT.DateTimeToStringStringifyDateEmptyTime(dt);
    }

    /// <summary>
    /// 1989-06-21Thh:mm:ss.000Z
    /// </summary>
    /// <param name="dt"></param>
    public static string DateTimeToStringStringifyDateTime(DateTime dt)
    {
        return DTHelperFormalizedWithT.DateTimeToStringStringifyDateTime(dt);
    }

    /// <summary>
    /// 1989-06-21T11:22:00
    /// </summary>
    /// <param name="dt"></param>
    public static string DateAndTimeToStringFormalizeDate(DateTime dt)
    {
        return DTHelperFormalizedWithT.DateAndTimeToStringFormalizeDate(dt);
    }

    /// <summary>
    /// yyyy_mm_dd 
    /// With A2 append hh_mm
    /// </summary>
    /// <param name="dt"></param>
    /// <param name="time"></param>
    public static string DateTimeToFileName(DateTime dt, bool time)
    {
        return DTHelperUs.DateTimeToFileName(dt, time);
    }

    /// <summary>
    /// 21.6.1989
    /// </summary>
    /// <param name="p"></param>
    /// <param name="l"></param>
    /// <param name="dtMinVal"></param>
    public static string DateToStringOrSE(DateTime p, Langs l, DateTime dtMinVal)
    {
        return DTHelperMulti.DateToStringOrSE(p, l, dtMinVal);
    }

    /// <summary>
    /// 21.6.1989 (středa) / 6/21/1989 (wednesday)
    /// </summary>
    /// <param name="dateTime"></param>
    /// <param name="l"></param>
    public static string DateWithDayOfWeek(DateTime dateTime, Langs l)
    {
        return DTHelperMulti.DateWithDayOfWeek(dateTime, l);
    }

    /// <summary>
    /// yyyy_mm_dd
    /// </summary>
    /// <param name="dt"></param>
    public static string DateTimeToFileName(DateTime dt)
    {
        return DTHelperUs.DateTimeToFileName(dt);
    }
    #endregion

    #region Parse
    #region Only time
    /// <summary>
    /// hh:mm:ss
    /// If fail, return DT.MinValue
    /// Seconds can be omit
    /// </summary>
    /// <param name="t"></param>
    public static DateTime ParseTimeCzech(string t)
    {
        return DTHelperCs.ParseTimeCzech(t);
    }

    /// <summary>
    /// Seconds can be omit
    /// hh:mm tt
    /// </summary>
    /// <param name="t"></param>
    public static DateTime ParseTimeUSA(string t)
    {
        return DTHelperEn.ParseTimeUSA(t);
    }
    #endregion

    #region Only date
    /// <summary>
    /// 21.6.1989
    /// </summary>
    /// <param name="input"></param>
    public static DateTime ParseDateCzech(string input)
    {
        return DTHelperCs.ParseDateCzech(input);
    }

    /// <summary>
    /// mm/d/yyyy
    /// </summary>
    /// <param name="p"></param>
    public static DateTime? ParseDateMonthDayYear(string p)
    {
        int dayTo = -1;
        return DTHelperMulti.ParseDateMonthDayYear(p, out dayTo);
    }
    #endregion

    #region Date with time (without seconds)
    /// <summary>
    /// Input in format like 2015-09-03T21:01
    /// </summary>
    /// <param name="v"></param>
    /// <param name="dtMinVal"></param>
    public static DateTime StringToDateTimeFromInputDateTimeLocal(string v, DateTime dtMinVal)
    {
        return DTHelperCode.StringToDateTimeFromInputDateTimeLocal(v, dtMinVal);
    }

    /// <summary>
    /// 1989_06_21_11_22 or 1989_06_21 if !A2
    /// </summary>
    /// <param name="fnwoe"></param>
    /// <param name="time"></param>
    /// <param name="prefix"></param>
    public static DateTime? FileNameToDateTimePrefix(string fnwoe, bool time, out string prefix)
    {
        return DTHelperUs.FileNameToDateTimePrefix(fnwoe, time, out prefix);
    }

    /// <summary>
    /// Return null if wont have right format
    /// If A2, A1 must have format ????_??_??_??_?? 
    /// if !A2, A1 must have format ????_??_??
    /// In any case what is after A2 is not important
    /// </summary>
    /// <param name="fnwoe"></param>
    /// <param name="time"></param>
    /// <param name="postfix"></param>
    public static DateTime? FileNameToDateTimePostfix(string fnwoe, bool time, out string postfix)
    {
        return DTHelperUs.FileNameToDateTimePostfix(fnwoe, time, out postfix);
    }

    /// <summary>
    /// Return null if wont have right format
    /// If A2, A1 must have format ????_??_??_??_?? 
    /// if !A2, A1 must have format ????_??_??
    /// In any case what is after A2 is not important
    /// </summary>
    /// <param name="fnwoe"></param>
    /// <param name="serie"></param>
    /// <param name="postfix"></param>
    public static DateTime? FileNameToDateWithSeriePostfix(string fnwoe, out int? serie, out string postfix)
    {
        return DTHelperUs.FileNameToDateWithSeriePostfix(fnwoe, out serie, out postfix);
    }

    /// <summary>
    /// 1989_06_21_11_22
    /// </summary>
    /// <param name="fnwoe"></param>
    public static DateTime? FileNameToDateTime(string fnwoe)
    {
        return DTHelperUs.FileNameToDateTime(fnwoe);
    } 
    #endregion
    #endregion

    #region Helper
    /// <summary>
    /// If A1 could be lower than 1d, return 1d
    /// </summary>
    /// <param name="tt"></param>
    /// <param name="calculateTime"></param>
    /// <param name="l"></param>
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