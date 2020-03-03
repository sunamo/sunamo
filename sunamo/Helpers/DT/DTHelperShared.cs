using System;
using System.Collections.Generic;
using System.Text;


public partial class DTHelper
{
    #region Parse
    public static DateTime IsValidTimeText(string r)
    {
        return DTHelperMulti.IsValidTimeText(r);
    }

    public static DateTime IsValidDateTimeText(string datum)
    {
        return DTHelperMulti.IsValidDateTimeText(datum);
    }

    public static DateTime IsValidDateText(string r)
    {
        return DTHelperMulti.IsValidDateText(r);
    }

    public static DateTime ParseDateUSA(string input)
    {
        return DTHelperEn.ParseDateUSA(input);
    }

    /// <summary>
    /// 2018-08-10T11:33:19Z
    /// </summary>
    /// <param name="p"></param>
    public static DateTime StringToDateTimeFormalizeDate(string p)
    {
        return DTHelperFormalized.StringToDateTimeFormalizeDate(p);
    }
    #endregion

    #region ToString
    public static string DateToStringWithDayOfWeekCS(DateTime dt)
    {
        return DTHelperCs.DateToStringWithDayOfWeekCS(dt);
    }

    public static string CalculateAgeAndAddRightStringKymCim(DateTime dateTime, bool calculateTime, Langs l, DateTime dtMinVal)
    {
        return DTHelperCs.CalculateAgeAndAddRightStringKymCim(dateTime, calculateTime, l, dtMinVal);
    }

    public static string MakeUpTo2NumbersToZero(int p)
    {
        return NH.MakeUpTo2NumbersToZero(p);
    }

    public static string TimeToStringAngularTime(DateTime dt)
    {
        return DTHelperCode.TimeToStringAngularTime(dt);
    }

    public static string DateToStringAngularDate(DateTime dt)
    {
        return DTHelperCode.DateToStringAngularDate(dt);
    }

    public static string DateToString(DateTime p, Langs l)
    {
        return DTHelperMulti.DateToString(p, l);
    }

    public static string DateTimeToString(DateTime d, Langs l, DateTime dtMinVal)
    {
        return DTHelperMulti.DateTimeToString(d, l, dtMinVal);
    }

    public static string DateTimeToStringWithoutDayOfWeek(DateTime actualMessageDt)
    {
        return null;
    }
    #endregion

    #region Other
    public static DateTime OnlyDateProperties(DateTime p)
    {
        return new DateTime(p.Year, p.Month, p.Day);
    }

    public static DateTime CalculateStartOfPeriod(string AddedAgo)
    {
        return DTHelperEn.CalculateStartOfPeriod(AddedAgo);
    }

    public static string AddRightStringToTimeSpan(TimeSpan ts, bool v)
    {
        return null;
    } 
    #endregion


}