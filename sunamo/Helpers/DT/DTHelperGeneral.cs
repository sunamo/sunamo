using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

public partial class DTHelperGeneral
{
    public static DateTime AddDays(ref DateTime dt, double day)
    {
        dt = dt.AddDays(day);
        return dt;
    }

    /// <summary>
    /// Find four digit letter in any string
    /// </summary>
    /// <returns></returns>
    public static string ParseYear(string s)
    {
        var p = SH.Split(s, AllChars.dash, AllChars.slash);
        foreach (var item in p)
        {
            if (item.Length == 4)
            {
                if (SH.IsNumber(item))
                {
                    return item;
                }
            }
        }
        return string.Empty;
    }

    public static DateTime SetMinute(DateTime d, int v)
    {
        return new DateTime(d.Year, d.Month, d.Day, d.Hour, v, d.Second);
    }

    public static DateTime SetHour(DateTime d, int v)
    {
        return new DateTime(d.Year, d.Month, d.Day, v, d.Minute, d.Second);
    }

    /// <summary>
    /// Subtract A2 from A1
    /// </summary>
    /// <param name="dt1"></param>
    /// <param name="dt2"></param>
    /// <returns></returns>
    public static TimeSpan Substract(DateTime dt1, DateTime dt2)
    {
        TimeSpan ts = dt1 - dt2;
        return ts;
    }

    public static DateTime SetDateToMinValue(DateTime dt)
    {
        DateTime minVal = DateTime.MinValue;
        return new DateTime(minVal.Year, minVal.Month, minVal.Day, dt.Hour, dt.Minute, dt.Second, dt.Millisecond);
    }

    /// <summary>
    /// Kontroluje i na MinValue a MaxValue
    /// </summary>
    /// <param name="dt"></param>
    /// <returns></returns>
    public static bool HasNullableDateTimeValue(DateTime? dt)
    {
        if (dt.HasValue)
        {
            if (dt.Value != DateTime.MinValue && dt.Value != DateTime.MaxValue)
            {
                return true;
            }
        }

        return false;
    }

    public static DateTime SetToday(DateTime ugtFirstStep)
    {
        DateTime t = DateTime.Today;
        return new DateTime(t.Year, t.Month, t.Day, ugtFirstStep.Hour, ugtFirstStep.Minute, ugtFirstStep.Second);
    }

    /// <summary>
    /// Počítá pouze čas, vrátí jako nenormalizovaný int
    /// </summary>
    /// <param name="t"></param>
    /// <returns></returns>
    public static long DateTimeToSecondsOnlyTime(DateTime t)
    {
        long vr = t.Hour * DTConstants.secondsInHour;
        vr += t.Minute * DTConstants.secondsInMinute;
        vr += t.Second;
        vr *= TimeSpan.TicksPerSecond;
        //vr += SqlServerHelper.DateTimeMinVal
        return vr;
    }


    public static DateTime Create(string day, string month, string hour, string minute)
    {
        return new DateTime(1, int.Parse(month), int.Parse(day), int.Parse(hour), int.Parse(minute), 0);
    }

    public static DateTime CreateTime(string v1, string v2)
    {
        DateTime today = DateTime.MinValue;
        today = today.AddHours(double.Parse(v1));
        today = today.AddHours(double.Parse(v2));
        return today;
    }

    internal static DateTime Combine(DateTime result, DateTime time)
    {
        result.AddHours(time.Hour);
        result.AddMinutes(time.Minute);
        result.AddSeconds(time.Second);
        result.AddMilliseconds(time.Millisecond);
        return result;
    }
}