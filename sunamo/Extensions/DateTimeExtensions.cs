using sunamo;

using System;

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

    public static string ToStringShortTimeNullable(this DateTime? dt)
    {
        if (dt.HasValue)
        {
            return DTHelperCs.ToShortTime(dt.Value);
        }
        return string.Empty;
    }
}
