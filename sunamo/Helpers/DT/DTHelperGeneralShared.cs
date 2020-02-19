using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

public partial class DTHelperGeneral
{
    

    /// <summary>
    /// A2 = SqlServerHelper.DateTimeMinVal
    /// if A1 = A2, return 255
    /// </summary>
    /// <param name="bday"></param>
    /// <returns></returns>
    public static byte CalculateAge(DateTime bday, DateTime dtMinVal)
    {
        if (bday == dtMinVal)
        {
            return 255;
        }
        DateTime today = DateTime.Today;
        int age = today.Year - bday.Year;
        if (bday > today.AddYears(-age)) age--;
        byte vr = (byte)age;
        if (vr == 255)
        {
            return 0;
        }
        return vr;
    }

    public static long SecondsInMonth(DateTime dt)
    {
        return DTConstants.secondsInDay * DateTime.DaysInMonth(dt.Year, dt.Month);
    }

    public static long SecondsInYear(int year)
    {
        long mal = 365;
        if (DateTime.IsLeapYear(year))
        {
            mal = 366;
        }

        return mal * DTConstants.secondsInDay;
    }

public static string TimeInMsToSeconds(Stopwatch p)
    {
        p.Stop();
        string d = ((double)p.ElapsedMilliseconds / 1000).ToString();
        if (d.Length > 4)
        {
            d = d.Substring(0, 4);
        }
        return d + "s";
        //return Math.Round(((double)p.ElapsedMilliseconds / 999), 2).ToString() + "s";
    }

public static string CalculateAgeString(DateTime bday, DateTime dtMinVal)
    {
        byte b = CalculateAge(bday, dtMinVal);
        if (b == 255)
        {
            return "";
        }
        return b.ToString();
    }

public static DateTime TodayPlusActualHour()
    {
        DateTime dt = DateTime.Today;
        return dt.AddHours(DateTime.Now.Hour);
    }

public static DateTime Combine(DateTime result, DateTime time)
    {
        result.AddHours(time.Hour);
        result.AddMinutes(time.Minute);
        result.AddSeconds(time.Second);
        result.AddMilliseconds(time.Millisecond);
        return result;
    }
}