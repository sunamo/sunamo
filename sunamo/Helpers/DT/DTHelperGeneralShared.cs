using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

public partial class DTHelperGeneral
{
    

   

    public static int FullYear(byte b)
    {
        var bs = b.ToString().PadLeft(3, AllChars.zero);
        return int.Parse("2" + bs);
    }

    #region Helper
    /// <summary>
    /// A2 = SqlServerHelper.DateTimeMinVal
    /// if A1 = A2, return 255
    /// </summary>
    /// <param name="bday"></param>
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

    public static DateTimeOrShort ShortToday()
    {
        return DateTimeOrShort.Sh( NormalizeDate.To(DateTime.Today));
    }

    public static DateTime WithoutTime(DateTime time)
    {
        return new DateTime(time.Year, time.Month, time.Day);
    }

    public static DateTime WithoutDate(DateTime dt)
    {
        return new DateTime(1,1,1, dt.Hour, dt.Minute, dt.Second);
    }

    public static string TimeInMsToSeconds(Stopwatch p)
    {
        var d2 = (double)p.ElapsedMilliseconds;
        p.Reset();
        string d = (d2 / 1000).ToString();
        if (d.Length > 4)
        {
            d = d.Substring(0, 4);
        }
        return d + "s";
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
    #endregion
}