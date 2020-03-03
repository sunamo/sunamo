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

    public static DateTime CreateTime(string v1, string v2)
    {
        DateTime today = DateTime.MinValue;
        today = today.AddHours(double.Parse(v1));
        today = today.AddHours(double.Parse(v2));
        return today;
    } 
    #endregion
}