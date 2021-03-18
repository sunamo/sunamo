using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

public partial class DTHelperGeneral
{
    public static List<DateTime> GetDatesBetween(DateTime startDate, DateTime endDate)
    {

        List<DateTime> allDates = new List<DateTime>();
        for (DateTime date = startDate; date <= endDate; date = date.AddDays(1))
            allDates.Add(date);
        return allDates;

    }

    

    #region Parse special
    /// <summary>
    /// Find four digit letter in any string
    /// </summary>
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

    
    #endregion

    #region Set*


    public static DateTime SetMinute(DateTime d, int v)
    {
        return new DateTime(d.Year, d.Month, d.Day, d.Hour, v, d.Second);
    }

    public static DateTime SetHour(DateTime d, int v)
    {
        return new DateTime(d.Year, d.Month, d.Day, v, d.Minute, d.Second);
    }
    #endregion

    #region Other
    /// <summary>
    /// Check also for MinValue and MaxValue
    /// </summary>
    /// <param name="dt"></param>
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

    /// <summary>
    /// Is counting only time, return as non-normalized int
    /// </summary>
    /// <param name="t"></param>
    public static long DateTimeToSecondsOnlyTime(DateTime t)
    {
        long vr = t.Hour * DTConstants.secondsInHour;
        vr += t.Minute * DTConstants.secondsInMinute;
        vr += t.Second;
        vr *= TimeSpan.TicksPerSecond;
        //vr += SqlServerHelper.DateTimeMinVal
        return vr;
    }

    public static DateTime AddDays(ref DateTime dt, double day)
    {
        dt = dt.AddDays(day);
        return dt;
    }

    /// <summary>
    /// Subtract A2 from A1
    /// </summary>
    /// <param name="dt1"></param>
    /// <param name="dt2"></param>
    public static TimeSpan Substract(DateTime dt1, DateTime dt2)
    {
        TimeSpan ts = dt1 - dt2;
        return ts;
    } 
    #endregion

    #region Set*
    public static DateTime SetDateToMinValue(DateTime dt)
    {
        DateTime minVal = DateTime.MinValue;
        return new DateTime(minVal.Year, minVal.Month, minVal.Day, dt.Hour, dt.Minute, dt.Second, dt.Millisecond);
    }

    public static DateTime SetToday(DateTime ugtFirstStep)
    {
        DateTime t = DateTime.Today;
        return new DateTime(t.Year, t.Month, t.Day, ugtFirstStep.Hour, ugtFirstStep.Minute, ugtFirstStep.Second);
    } 
    #endregion

    #region Create*
    public static DateTime? Create(string y, string m, string d)
    {
        return Create(BTS.ParseInt(y), BTS.ParseInt(m), BTS.ParseInt(d));
    }

    public static DateTime? Create(int y, int m, int d)
    {
        try
        {
            return new DateTime(y, m, d);
        }
        catch (ArgumentOutOfRangeException)
        {
            return null;

        }
        return null;
    }

    public static DateTime Create(string day, string month, string hour, string minute)
    {
        return new DateTime(1, int.Parse(month), int.Parse(day), int.Parse(hour), int.Parse(minute), 0);
    }

    public static DateTime CreateTime(string hour, string minutes)
    {
        DateTime today = DateTime.MinValue;
        today = today.AddHours(double.Parse(hour));
        today = today.AddMinutes(double.Parse(minutes));
        return today;
    } 
    #endregion
}