using System;
using System.Collections.Generic;
using System.Text;


public partial class DTHelper
{
    public static string DateToStringWithDayOfWeekCS(DateTime dt)
    {
        return DTHelperCs.DateToStringWithDayOfWeekCS(dt);
    }

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

public static string CalculateAgeAndAddRightStringKymCim(DateTime dateTime, bool calculateTime, Langs l, DateTime dtMinVal)
    {
        return DTHelperCs.CalculateAgeAndAddRightStringKymCim(dateTime, calculateTime, l, dtMinVal);
    }

public static string MakeUpTo2NumbersToZero(int p)
    {
        return DTHelperGeneral.MakeUpTo2NumbersToZero(p);
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
}