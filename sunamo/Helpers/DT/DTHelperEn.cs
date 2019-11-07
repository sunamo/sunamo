using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

public partial class DTHelperEn
{
    /// <summary>
    /// Its named ToString due to exactly same format return dt.ToString while is en-us localization
    /// </summary>
    /// <returns></returns>
    public static string ToString(DateTime dt)
    {
        return ToShortDateString(dt) + " " + ToShortTimeString(dt);
    }

    public static string ToShortTimeString(DateTime dt)
    {
        return string.Format("{0:mm:ss tt}", dt);
        //return dt.Hour + ":" + dt.Minute + " " + dt.ToString("tt", CultureInfo.InvariantCulture);
    }
}