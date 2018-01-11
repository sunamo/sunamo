using System;

public static class TimeSpanExtensions
{
    public static int TotalYears(this TimeSpan timespan)
    {
        return (int)((double)timespan.Days / 365.2425);
    }
    public static int TotalMonths(this TimeSpan timespan)
    {
        return (int)((double)timespan.Days / 30.436875);
    }
}
