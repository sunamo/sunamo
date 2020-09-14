using sunamo.Helpers.DT;
using System;
using System.Collections.Generic;
using System.Text;

public partial class DTHelperMulti
{
    #region Other
    /// <summary>
    /// If A1 could be lower than 1d, return 1d
    /// </summary>
    /// <param name="ts"></param>
    /// <param name="calculateTime"></param>
    public static string AddRightStringToTimeSpan(TimeSpan tt, bool calculateTime, Langs l)
    {
        int age = tt.TotalYears();

        if (tt.TotalMilliseconds == 0)
        {
            int months = tt.TotalMonths();
            if (months < 3)
            {
                int totalWeeks = tt.Days / 7;
                if (totalWeeks == 0)
                {
                    if (tt.Days == 1)
                    {
                        if (l == Langs.cs)
                        {
                            return tt.Days + " den";
                        }
                        else
                        {
                            return tt.Days + " day";
                        }
                    }
                    else if (tt.Days < 5 && tt.Days > 1)
                    {
                        if (l == Langs.cs)
                        {
                            return tt.Days + " dn\u00ED";
                        }
                        else
                        {
                            return tt.Days + " days";
                        }
                    }
                    else
                    {
                        if (calculateTime)
                        {
                            if (tt.Hours == 1)
                            {
                                if (l == Langs.cs)
                                {
                                    return tt.Hours + " hodinu";
                                }
                                else
                                {
                                    return tt.Hours + " hour";
                                }
                            }
                            else if (tt.Hours > 1 && tt.Hours < 5)
                            {
                                if (l == Langs.cs)
                                {
                                    return tt.Hours + " hodiny";
                                }
                                else
                                {
                                    return tt.Hours + " hours";
                                }
                            }
                            else if (tt.Hours > 4)
                            {
                                if (l == Langs.cs)
                                {
                                    return tt.Hours + " hodin";
                                }
                                else
                                {
                                    return tt.Hours + " hours";
                                }
                            }
                            else
                            {
                                // Hodin je méně než 1
                                if (tt.Minutes == 1)
                                {
                                    if (l == Langs.cs)
                                    {
                                        return tt.Minutes + " minutu";
                                    }
                                    else
                                    {
                                        return tt.Minutes + " minute";
                                    }
                                }
                                else if (tt.Minutes > 1 && tt.Minutes < 5)
                                {
                                    if (l == Langs.cs)
                                    {
                                        return tt.Minutes + " minuty";
                                    }
                                    else
                                    {
                                        return tt.Minutes + " minutes";
                                    }
                                }
                                else if (tt.Minutes > 4)
                                {
                                    if (l == Langs.cs)
                                    {
                                        return tt.Minutes + " minut";
                                    }
                                    else
                                    {
                                        return tt.Minutes + " minutes";
                                    }
                                }
                                else //if (tt.Minutes == 0)
                                {
                                    if (tt.Seconds == 1)
                                    {
                                        if (l == Langs.cs)
                                        {
                                            return tt.Seconds + " sekundu";
                                        }
                                        else
                                        {
                                            return tt.Seconds + " second";
                                        }
                                    }
                                    else if (tt.Seconds > 1 && tt.Seconds < 5)
                                    {
                                        if (l == Langs.cs)
                                        {
                                            return tt.Seconds + " sekundy";
                                        }
                                        else
                                        {
                                            return tt.Seconds + " seconds";
                                        }
                                    }
                                    else //if (tt.Seconds > 4)
                                    {
                                        if (l == Langs.cs)
                                        {
                                            return tt.Seconds + " sekund";
                                        }
                                        else
                                        {
                                            return tt.Seconds + " seconds";
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (l == Langs.cs)
                            {
                                return "~1 den";
                            }
                            else
                            {
                                return "~1 day";
                            }
                        }
                    }
                }
                else if (totalWeeks == 1)
                {
                    if (l == Langs.cs)
                    {
                        return totalWeeks + " t\u00FDden";
                    }
                    else
                    {
                        return totalWeeks + " week";
                    }
                }
                else if (totalWeeks < 5 && totalWeeks > 1)
                {
                    if (l == Langs.cs)
                    {
                        return totalWeeks + " t\u00FDdny";
                    }
                    else
                    {
                        return totalWeeks + " weeks";
                    }
                }
                else
                {
                    if (l == Langs.cs)
                    {
                        return totalWeeks + " t\u00FDdn\u016F";
                    }
                    else
                    {
                        return totalWeeks + " weeks";
                    }
                }
            }
            else
            {
                if (months == 1)
                {
                    if (l == Langs.cs)
                    {
                        return months + " m\u011Bs\u00EDc";
                    }
                    else
                    {
                        return months + " months";
                    }
                }
                else if (months > 1 && months < 5)
                {
                    if (l == Langs.cs)
                    {
                        return months + " m\u011Bs\u00EDce";
                    }
                    else
                    {
                        return months + " months";
                    }
                }
                else
                {
                    if (l == Langs.cs)
                    {
                        return months + " m\u011Bs\u00EDc\u016F";
                    }
                    else
                    {
                        return months + " months";
                    }
                }
            }
        }
        else if (age == 1)
        {
            if (l == Langs.cs)
            {
                return "  rok";
            }
            else
            {
                return "  year";
            }
        }
        else if (age > 1 && age < 5)
        {
            if (l == Langs.cs)
            {
                return age + " roky";
            }
            else
            {
                return age + " years";
            }
        }
        else if (age > 4 || age == 0)
        {
            if (l == Langs.cs)
            {
                return age + " rok\u016F";
            }
            else
            {
                return age + " years";
            }
        }
        else
        {
            if (l == Langs.cs)
            {
                return "Nezn\u00E1m\u00FD \u010Das";
            }
            return sess.i18n(XlfKeys.NoKnownPeriod);
        }
    }

    /// <summary>
    /// If A1 could be lower than 1d, return 1d
    /// </summary>
    /// <param name="dateTime"></param>
    /// <param name="calculateTime"></param>
    public static string OperationLastedInLocalizateString(TimeSpan tt, Langs l)
    {
        List<string> vr = new List<string>();

        if (tt.Hours == 1)
        {
            if (l == Langs.cs)
            {
                vr.Add(tt.Hours + " hodinu");
            }
            else
            {
                vr.Add(tt.Hours + " hour");
            }
        }
        else if (tt.Hours > 1 && tt.Hours < 5)
        {
            if (l == Langs.cs)
            {
                vr.Add(tt.Hours + " hodiny");
            }
            else
            {
                vr.Add(tt.Hours + " hours");
            }
        }
        else if (tt.Hours > 4)
        {
            if (l == Langs.cs)
            {
                vr.Add(tt.Hours + " hodin");
            }
            else
            {
                vr.Add(tt.Hours + " hours");
            }
        }
        else
        {
            // Hodin je méně než 1
            if (tt.Minutes == 1)
            {
                if (l == Langs.cs)
                {
                    vr.Add(tt.Minutes + " minutu");
                }
                else
                {
                    vr.Add(tt.Minutes + " minute");
                }
            }
            else if (tt.Minutes > 1 && tt.Minutes < 5)
            {
                if (l == Langs.cs)
                {
                    vr.Add(tt.Minutes + " minuty");
                }
                else
                {
                    vr.Add(tt.Minutes + " minutes");
                }
            }
            else if (tt.Minutes > 4)
            {
                if (l == Langs.cs)
                {
                    vr.Add(tt.Minutes + " minut");
                }
                else
                {
                    vr.Add(tt.Minutes + " minutes");
                }
            }
            else //if (tt.Minutes == 0)
            {
                if (tt.Seconds == 1)
                {
                    if (l == Langs.cs)
                    {
                        vr.Add(tt.Seconds + " sekundu");
                    }
                    else
                    {
                        vr.Add(tt.Seconds + " second");
                    }
                }
                else if (tt.Seconds > 1 && tt.Seconds < 5)
                {
                    if (l == Langs.cs)
                    {
                        vr.Add(tt.Seconds + " sekundy");
                    }
                    else
                    {
                        vr.Add(tt.Seconds + " seconds");
                    }
                }
                else if (tt.Seconds > 4)
                {
                    if (l == Langs.cs)
                    {
                        vr.Add(tt.Seconds + " sekund");
                    }
                    else
                    {
                        vr.Add(tt.Seconds + " seconds");
                    }
                }
                else
                {
                    if (tt.Seconds == 1)
                    {
                        if (l == Langs.cs)
                        {
                            vr.Add(tt.Milliseconds + " milisekundu");
                        }
                        else
                        {
                            vr.Add(tt.Milliseconds + " millisecond");
                        }
                    }
                    else if (tt.Seconds > 1 && tt.Seconds < 5)
                    {
                        if (l == Langs.cs)
                        {
                            vr.Add(tt.Milliseconds + " milisekundy");
                        }
                        else
                        {
                            vr.Add(tt.Milliseconds + " milliseconds");
                        }
                    }
                    else if (tt.Seconds > 4)
                    {
                        if (l == Langs.cs)
                        {
                            vr.Add(tt.Milliseconds + " milisekund");
                        }
                        else
                        {
                            vr.Add(tt.Milliseconds + " milliseconds");
                        }
                    }
                    else
                    {
                        if (l == Langs.cs)
                        {
                            vr.Add(tt.Milliseconds + " milisekund");
                        }
                        else
                        {
                            vr.Add(tt.Milliseconds + " milliseconds");
                        }
                    }
                }
            }
        }

        string s = SH.Join(AllChars.space, vr);

        return s;
    }
    #endregion


    #region ToString
    /// <summary>
    /// 21.6.1989 / 6/21/1989
    /// </summary>
    /// <param name="p"></param>
    /// <param name="l"></param>
    /// <param name="dtMinVal"></param>
    public static string DateToStringOrSE(DateTime p, Langs l, DateTime dtMinVal)
    {
        if (p == dtMinVal)
        {
            return "";
        }
        return DTHelperMulti.DateToString(p, l);
    } 
    #endregion

    

    #region Parse
    /// <summary>
    /// m/d/yyyy / d/m/yyyy
    /// </summary>
    /// <param name="p"></param>
    public static DateTime? ParseDateMonthDayYear(string p, out int dayTo)
    {
        dayTo = -1;

        var s = SH.SplitNone(p, AllStrings.slash);
        if (s.Count == 1)
        {
            s = SH.SplitNone(p, AllStrings.dot);

            s[0] = DayTo(s[0], out dayTo);

            DateTime vr = DTHelperCs.ParseDateCzech(s[0] + AllStrings.dot + s[1] + AllStrings.dot + s[2]);
            if (vr != DateTime.MinValue)
            {
                return vr;
            }
        }
        else
        {
            s[1] = DayTo(s[1], out dayTo);

            DateTime vr = DTHelperCs.ParseDateCzech(s[1] + AllStrings.dot + s[0] + AllStrings.dot + s[2]);
            if (vr != DateTime.MinValue)
            {
                return vr;
            }
        }
        return null;
    }

    private static string DayTo(string v, out int dayTo)
    {
        if (v.Contains(AllStrings.dash))
        {
            string b, a;
            SH.GetPartsByLocation(out b, out a, v, AllChars.dash);
            dayTo = BTS.ParseInt(v, -1);
            return b;
        }
        dayTo =- 1;
        return v;
    }
    #endregion
}