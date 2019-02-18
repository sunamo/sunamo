using System;
using System.Collections.Generic;
using System.Text;

public partial class DTHelperEn{ 
//public static string ToShortTime(DateTime value)
        //{
        //    return 
        //}

        public static DateTime ParseDateUSA(string input)
        {
            DateTime vr = DateTime.MinValue;
            var parts = SH.Split(input, '/');
            var day = -1;
            var month = -1;
            var year = -1;

            TryParse.Integer tpi = new TryParse.Integer();
            if (tpi.TryParseInt(parts[0]))
            {
                month = tpi.lastInt;
                if (tpi.TryParseInt(parts[1]))
                {
                    day = tpi.lastInt;
                    if (tpi.TryParseInt(parts[2]))
                    {
                        year = tpi.lastInt;
                        try
                        {
                            vr = new DateTime(year, month, day, 0, 0, 0);
                        }
                        catch (Exception)
                        {
                        }
                    }
                }
            }
            return vr;
        }

public static DateTime ParseTimeUSA(string t)
        {
            var vr = DateTime.MinValue;
            var parts2 = SH.Split(t, ' ');
            if (parts2.Count == 2)
            {
                var pm = false;
                var amorpm = parts2[1].ToLower();
                if (amorpm == "pm" || amorpm == "am")
                {
                    if (amorpm == "pm")
                    {
                        pm = true;
                    }
                    var t2 = parts2[0];
                    var parts = SH.Split(t2, ':');
                    if (parts.Count == 2)
                    {
                        t += ":00";
                        parts = SH.Split(t, ':');
                    }
                    int hours = -1;
                    int minutes = -1;
                    int seconds = -1;
                    if (parts.Count == 3)
                    {
                        TryParse.Integer itp = new TryParse.Integer();
                        if (itp.TryParseInt(parts[0]))
                        {
                            hours = itp.lastInt;
                            if (itp.TryParseInt(parts[1]))
                            {
                                minutes = itp.lastInt;
                                if (itp.TryParseInt(parts[2]))
                                {
                                    seconds = itp.lastInt;
                                    vr = DateTime.Today;
                                    if (!pm && hours == 12)
                                    {
                                        hours = 0;
                                    }
                                    else if (pm)
                                    {
                                        hours += 12;
                                    }
                                    vr = vr.AddHours(hours);
                                    vr = vr.AddMinutes(minutes);
                                    vr = vr.AddSeconds(seconds);
                                }
                            }
                        }
                    }
                }

            }
            return vr;
        }
}