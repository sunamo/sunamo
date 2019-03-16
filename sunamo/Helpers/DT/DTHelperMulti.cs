using sunamo.Helpers.DT;
using System;
using System.Collections.Generic;
using System.Text;

    public partial class DTHelperMulti
    {

        /// <summary>
        /// POkud bude !A2 a bude čas menší než 1 den, vrátí mi pro tuto časovou jednotku "1 den"
        /// </summary>
        /// <param name="ts"></param>
        /// <param name="calculateTime"></param>
        /// <returns></returns>
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
                                return tt.Days + " dní";
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
                            return totalWeeks + " týden";
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
                            return totalWeeks + " týdny";
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
                            return totalWeeks + " týdnů";
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
                            return months + " měsíc";
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
                            return months + " měsíce";
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
                            return months + " měsíců";
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
                    return "1 rok";
                }
                else
                {
                    return "1 year";
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
                    return age + " roků";
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
                    return "Neznámý čas";
                }
                return "No known period";
            }
        }

        public static string DateToStringOrSE(DateTime p, Langs l, DateTime dtMinVal)
        {
            if (p == dtMinVal)
            {
                return "";
            }
            return DTHelperMulti.DateToString(p, l);
        }

        /// <summary>
        /// POkud bude !A2 a bude čas menší než 1 den, vrátí mi pro tuto časovou jednotku "1 den"
        /// </summary>
        /// <param name="dateTime"></param>
        /// <param name="calculateTime"></param>
        /// <returns></returns>
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

            string s = SH.Join(' ', vr);

            return s;
        }

        public static string DateWithDayOfWeek(DateTime dateTime, Langs l)
        {
            int day = (int)dateTime.DayOfWeek;
            if (day == 0)
            {
                day = 6;
            }
            else
            {
                day--;
            }

            string dayOfWeek = DTConstants.daysInWeekEN[day];
            if (l == Langs.cs)
            {
                dayOfWeek = DTConstants.daysInWeekCS[day];
            }

            return DateToString(dateTime, l) + " (" + dayOfWeek + ")";
        }

        /// <summary>
        /// Vyparsuje datum ve formátu měsíc/den/rok
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public static DateTime? ParseDateMonthDayYear(string p)
        {
            var s = SH.SplitNone(p, "/");
            DateTime vr;
            if (DateTime.TryParse(s[1] + "." + s[0] + "." + s[2], out vr))
            {
                return vr;
            }
            return null;
        }


    }