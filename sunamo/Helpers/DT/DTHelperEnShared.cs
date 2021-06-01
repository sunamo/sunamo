using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

public partial class DTHelperEn
{
    #region Parse
    #region Only Date
    /// <summary>
    /// return MinValue when fail
    /// </summary>
    /// <param name="input"></param>
    public static DateTime ParseDateUSA(string input)
    {
        DateTime vr = DateTime.MinValue;
        var parts = SH.Split(input, AllChars.slash);
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
                    catch (Exception ex)
                    {
                    }
                }
            }
        }
        return vr;
    } 
    #endregion

    #region Only time
    /// <summary>
    /// Seconds can be omit
    /// hh:mm tt
    /// </summary>
    /// <param name="t"></param>
    public static DateTime ParseTimeUSA(string t)
    {
        var vr = DateTime.MinValue;
        var parts2 = SH.Split(t, AllChars.space);
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
                var parts = SH.Split(t2, AllChars.colon);
                if (parts.Count == 2)
                {
                    t += ":00";
                    parts = SH.Split(t, AllChars.colon);
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
    #endregion

    #region Date and time
    /// <summary>
    /// Seconds can be omit
    /// hh:mm tt
    /// </summary>
    /// <param name="s"></param>
    public static DateTime ParseDateTimeUSA(string s)
    {
        var p = SH.Split(s, AllStrings.space);
        DateTime result = ParseDateUSA(p[0]);
        var time = ParseTimeUSA(p[1] + AllStrings.space + p[2]);
        return DTHelperGeneral.Combine(result, time);
        //return DateTime.Parse(s, CultureInfo.GetCultureInfo("en-us"));
    }
    #endregion
    #endregion

    

    #region Helper
    /// <summary>
    /// Insert {number}_{days,weeks,years nebo months}
    /// Get date shortened about A1
    /// </summary>
    /// <param name="AddedAgo"></param>
    public static DateTime CalculateStartOfPeriod(string AddedAgo)
    {
        int days = -1;
        int number = -1;

        var arg = SH.SplitNone(AddedAgo, AllStrings.lowbar);
        if (arg.Length() == 2)
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
        }
        days *= -1;
        return DateTime.Today.AddDays(days);
    }

    public static string DateToStringWithDayOfWeekEN(DateTime dt)
    {
        return dt.DayOfWeek.ToString() + ", " + ToShortDateString(dt);
    }
    #endregion
}