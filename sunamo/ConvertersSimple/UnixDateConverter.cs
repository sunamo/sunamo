using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class UnixDateConverter
{
    //public static DateTime From(int unixTimeStamp)
    //{
    //    // Unix timestamp is seconds past epoch
    //    System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
    //    dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();
    //    return dtDateTime;
    //}

    //public static int To(DateTime dt)
    //{
    //    Int32 unixTimestamp = (Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;

    //    return unixTimestamp;
    //}

    public static long To(DateTime target)
    {
        var date = new DateTime(1970, 1, 1, 0, 0, 0, target.Kind);
        var unixTimestamp = System.Convert.ToInt64((target - date).TotalSeconds);

        return unixTimestamp;
    }

    public static DateTime From(long timestamp)
    {
        var dateTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Unspecified);

        return dateTime.AddSeconds(timestamp);
    }
}