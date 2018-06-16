using System;

// Extension class can't be in namespace
public static class DateTimeExtensions
{
    public static string ToLongTimeString(this DateTime dt)
    {
        return dt.Hour + ":" + dt.Minute + ":" + dt.Second;
    }

    public static string ToShortTimeString(this DateTime dt)
    {
        return dt.Hour + ":" + dt.Minute;
    }


}
