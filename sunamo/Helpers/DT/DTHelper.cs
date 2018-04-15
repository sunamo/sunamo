
using sunamo;
using sunamo.Enums;
using sunamo.Helpers.DT;
using System;
using System.Collections.Generic;
using System.Diagnostics;



public class DTHelper
{
    #region Code
    public static string DateToStringjQueryDatePicker(DateTime dt)
    {
        return DTHelperCode.DateToStringjQueryDatePicker(dt);
    }

    public static string DateToStringAngularDate(DateTime dt)
    {
        return DTHelperCode.DateToStringAngularDate(dt);
    }

    public static string TimeToStringAngularTime(DateTime dt)
    {
        return DTHelperCode.TimeToStringAngularTime(dt);
    }

    public static string DateAndTimeToStringAngularDateTime(DateTime dt)
    {
        return DTHelperCode.DateAndTimeToStringAngularDateTime(dt);
    }

    public static string DateTimeToStringToInputDateTimeLocal(DateTime dt, DateTime dtMinVal)
    {
        return DTHelperCode.DateTimeToStringToInputDateTimeLocal(dt, dtMinVal);
    }

    public static DateTime StringToDateTimeFromInputDateTimeLocal(string v, DateTime dtMinVal)
    {
        return DTHelperCode.StringToDateTimeFromInputDateTimeLocal(v, dtMinVal);
    }
    #endregion

    #region Cs
    public static string AppendToFrontOnlyTime(string defin)
    {
        return DTHelperCs.AppendToFrontOnlyTime(defin);
    }

    public static string IntervalToString(DateTime oDTStart, DateTime oDTEnd, Langs l, DateTime dtMinVal)
    {
        return DTHelperCs.IntervalToString(oDTStart, oDTEnd, l, dtMinVal);
    }

    public static DateTime ParseTimeCzech(string t)
    {
        return DTHelperCs.ParseTimeCzech(t);
    }

    public static DateTime ParseDateCzech(string input)
    {
        return DTHelperCs.ParseDateCzech(input);
    }

    public static string CalculateAgeAndAddRightString(DateTime dateTime, bool calculateTime, DateTime dtMinVal)
    {
        return DTHelperCs.CalculateAgeAndAddRightString(dateTime, calculateTime, dtMinVal);
    }

    public static string CalculateAgeAndAddRightStringKymCim(DateTime dateTime, bool calculateTime, Langs l, DateTime dtMinVal)
    {
        return DTHelperCs.CalculateAgeAndAddRightStringKymCim(dateTime, calculateTime, l, dtMinVal);
    }

    public static string DayOfWeek2DenVTydnu(DayOfWeek dayOfWeek)
    {
        return DTHelperCs.DayOfWeek2DenVTydnu(dayOfWeek);
    }

    public static string DateTimeToStringWithDayOfWeekCS(DateTime dt)
    {
        return DTHelperCs.DateTimeToStringWithDayOfWeekCS(dt);
    }

    public static string DateToStringWithDayOfWeekCS(DateTime dt)
    {
        return DTHelperCs.DateToStringWithDayOfWeekCS(dt);
    }
    #endregion

    #region En
    public static DateTime ParseTimeUSA(string t)
    {
        return DTHelperEn.ParseTimeUSA(t);
    }

    public static DateTime ParseDateUSA(string input)
    {
        return DTHelperEn.ParseDateUSA(input);
    }

    public static DateTime CalculateStartOfPeriod(string AddedAgo)
    {
        return DTHelperEn.CalculateStartOfPeriod(AddedAgo);
    }
    #endregion

    #region Formalized
    public static DateTime StringToDateTimeFormalizeDate(string p)
    {
        return DTHelperFormalized.StringToDateTimeFormalizeDate(p);    }

    public static string DateTimeToStringFormalizeDate(DateTime dt)
    {
        return DTHelperFormalized.DateTimeToStringFormalizeDate(dt);
    }

    public static string FormatDateTime(DateTime dt, DateTimeFormatStyles fullCalendar)
    {
        return DTHelperFormalized.FormatDateTime(dt, fullCalendar);
    }
    #endregion

    #region FormalizedWithT
    

    public static string DateTimeToStringFormalizeDateEmptyTime(DateTime dt)
    {
        return DTHelperFormalizedWithT.DateTimeToStringFormalizeDateEmptyTime(dt);
    }

    public static string DateTimeToStringStringifyDateEmptyTime(DateTime dt)
    {
        return DTHelperFormalizedWithT.DateTimeToStringStringifyDateEmptyTime(dt);
    }

    public static string DateTimeToStringStringifyDateTime(DateTime dt)
    {
        return DTHelperFormalizedWithT.DateTimeToStringStringifyDateTime(dt);
    }

    public static string DateAndTimeToStringFormalizeDate(DateTime dt)
    {
        return DTHelperFormalizedWithT.DateAndTimeToStringFormalizeDate(dt);
    }
    #endregion

    #region Cs
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

    public static string MakeUpTo2NumbersToZero(int p)
    {
        return DTHelperGeneral.MakeUpTo2NumbersToZero(p);
    }
    #endregion

    #region Multi
    public static string DateToString(DateTime p, Langs l)
    {
        return DTHelperMulti.DateToString(p, l);
    }

    public static DateTime IsValidDateText(string r)
    {
        return DTHelperMulti.IsValidDateText(r);
    }

    public static DateTime IsValidTimeText(string r)
    {
        return DTHelperMulti.IsValidTimeText(r);
    }

    public static string AddRightStringToTimeSpan(TimeSpan tt, bool calculateTime, Langs l)
    {
        return DTHelperMulti.AddRightStringToTimeSpan(tt, calculateTime, l);
    }

    public static string DateToStringOrSE(DateTime p, Langs l, DateTime dtMinVal)
    {
        return DTHelperMulti.DateToStringOrSE(p, l, dtMinVal);
    }

    

    public static string OperationLastedInLocalizateString(TimeSpan tt, Langs l)
    {
        return DTHelperMulti.OperationLastedInLocalizateString(tt, l);
    }

    public static string DateWithDayOfWeek(DateTime dateTime, Langs l)
    {
        return DTHelperMulti.DateWithDayOfWeek(dateTime, l);
    }

    public static DateTime? ParseDateMonthDayYear(string p)
    {
        return DTHelperMulti.ParseDateMonthDayYear(p);
    }

    public static string DateTimeToString(DateTime d, Langs l, DateTime dtMinVal)
    {
        return DTHelperMulti.DateTimeToString(d, l, dtMinVal);
    }

    public static DateTime IsValidDateTimeText(string datum)
    {
        return DTHelperMulti.IsValidDateTimeText(datum);
    }
    #endregion

    #region Us
    public static string DateTimeToFileName(DateTime dt)
    {
        return DTHelperUs.DateTimeToFileName(dt);
    }

    public static DateTime? FileNameToDateTimePrefix(string fnwoe, bool time, out string prefix)
    {
        return DTHelperUs.FileNameToDateTimePrefix(fnwoe, time, out prefix);
    }

    public static DateTime? FileNameToDateTimePostfix(string fnwoe, bool time, out string postfix)
    {
        return DTHelperUs.FileNameToDateTimePostfix(fnwoe, time, out postfix);
    }

    public static DateTime? FileNameToDateWithSeriePostfix(string fnwoe, out int? serie, out string postfix)
    {
        return DTHelperUs.FileNameToDateWithSeriePostfix(fnwoe, out serie, out postfix);
    }

    public static DateTime? FileNameToDateTime(string fnwoe)
    {
        return DTHelperUs.FileNameToDateTime(fnwoe);
    }

    public static string DateTimeToFileName(DateTime dt, bool time)
    {
        return DTHelperUs.DateTimeToFileName(dt, time);
    }
    #endregion
}
