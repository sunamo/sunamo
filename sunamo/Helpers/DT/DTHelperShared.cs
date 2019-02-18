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
}