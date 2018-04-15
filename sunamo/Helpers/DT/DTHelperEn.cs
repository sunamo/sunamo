using System;
using System.Collections.Generic;
using System.Text;

namespace sunamo.Helpers.DT
{
    public class DTHelperEn
    {
        public static DateTime ParseTimeUSA(string t)
        {
            var vr = DateTime.MinValue;
            var parts2 = SH.Split(t, ' ');
            if (parts2.Length == 2)
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
                    if (parts.Length == 2)
                    {
                        t += ":00";
                        parts = SH.Split(t, ':');
                    }
                    int hours = -1;
                    int minutes = -1;
                    int seconds = -1;
                    if (parts.Length == 3)
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

        /// <summary>
        /// Do této metody se předává {cislo}_{days,weeks,years nebo months}
        /// Vrátí mi aktuální datum zkrácené o A1
        /// </summary>
        /// <param name="AddedAgo"></param>
        /// <returns></returns>
        public static DateTime CalculateStartOfPeriod(string AddedAgo)
        {
            int days = -1;
            int number = -1;

            string[] arg = SH.SplitNone(AddedAgo, "_");
            if (arg.Length == 2)
            {
                TryParse.Integer dt = new TryParse.Integer();
                if (dt.TryParseInt(arg[0].ToString()))
                {
                    number = dt.lastInt;
                }
                else
                {
                    number = 1;
                }

                #region MyRegion
                switch (arg[1])
                {
                    case "days":
                        days = number;
                        break;
                    case "weeks":
                        days = 7 * number;
                        break;
                    case "years":
                        days = 365 * number;
                        break;
                    case "months":
                        days = 31 * number;
                        break;
                    default:
                        days = 1;
                        break;
                }
                #endregion
            }
            days *= -1;
            return DateTime.Today.AddDays(days);
        }
    }
}
