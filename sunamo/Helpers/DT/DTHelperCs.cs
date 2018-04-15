using System;
using System.Collections.Generic;
using System.Text;

namespace sunamo.Helpers.DT
{
    public class DTHelperCs
    {
        #region Pouze čas
        /// <summary>
        /// Vrátím aktuální čas(například 12:00:00:000 a zatím A1
        /// </summary>
        /// <param name="defin"></param>
        /// <returns></returns>
        public static string AppendToFrontOnlyTime(string defin)
        {
            DateTime dt = DateTime.Now;
            return NH.MakeUpTo2NumbersToZero(dt.Hour) + ":" + NH.MakeUpTo2NumbersToZero(dt.Minute) + ":" + NH.MakeUpTo2NumbersToZero(dt.Second) + ":" + NH.MakeUpTo3NumbersToZero(dt.Millisecond) + " " + defin;
        }


        #endregion

        public static string IntervalToString(DateTime oDTStart, DateTime oDTEnd, Langs l, DateTime dtMinVal)
        {
            return DTHelperMulti.DateTimeToString(oDTStart, l, dtMinVal) + " - " + DTHelperMulti.DateTimeToString(oDTEnd, l, dtMinVal);
        }

        /// <summary>
        /// If fail, return DT.MinValue
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public static DateTime ParseTimeCzech(string t)
        {
            var vr = DateTime.MinValue;
            var parts = SH.Split(t, ':');
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
                            vr = vr.AddHours(hours);
                            vr = vr.AddMinutes(minutes);
                            vr = vr.AddSeconds(seconds);
                        }
                    }
                }
            }
            return vr;
        }

        public static DateTime ParseDateCzech(string input)
        {
            DateTime vr = DateTime.MinValue;
            var parts = SH.Split(input, '.');
            var day = -1;
            var month = -1;
            var year = -1;

            TryParse.Integer tpi = new TryParse.Integer();
            if (tpi.TryParseInt(parts[0]))
            {
                day = tpi.lastInt;
                if (tpi.TryParseInt(parts[1]))
                {
                    month = tpi.lastInt;
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
        /// POkud bude !A2 a bude čas menší než 1 den, vrátí mi pro tuto časovou jednotku "1 den"
        /// A3 bylo původně MSStoredProceduresI.DateTimeMinVal
        /// </summary>
        /// <param name="dateTime"></param>
        /// <param name="calculateTime"></param>
        /// <returns></returns>
        public static string CalculateAgeAndAddRightString(DateTime dateTime, bool calculateTime, DateTime dtMinVal)
        {
            if (dateTime == dtMinVal)
            {
                return "";
            }
            int age = DTHelperGeneral.CalculateAge(dateTime, dtMinVal);

            if (age == 0)
            {
                DateTime Date1 = dateTime;
                DateTime Date2 = DateTime.Now;
                int months = (Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;
                if (months < 3)
                {
                    TimeSpan tt = Date2 - Date1;

                    int totalWeeks = tt.Days / 7;
                    if (totalWeeks == 0)
                    {
                        if (tt.Days == 1)
                        {
                            return tt.Days + " den";
                        }
                        else if (tt.Days < 5 && tt.Days > 1)
                        {
                            return tt.Days + " dny";
                        }
                        else
                        {
                            if (calculateTime)
                            {
                                if (tt.Hours == 1)
                                {
                                    return tt.Hours + " hodina";
                                }
                                else if (tt.Hours > 1 && tt.Hours < 5)
                                {
                                    return tt.Hours + " hodiny";
                                }
                                else if (tt.Hours > 4)
                                {
                                    return tt.Hours + " hodin";
                                }
                                else
                                {
                                    // Hodin je méně než 1
                                    if (tt.Minutes == 1)
                                    {
                                        return tt.Minutes + " minuta";
                                    }
                                    else if (tt.Minutes > 1 && tt.Minutes < 5)
                                    {
                                        return tt.Minutes + " minuty";
                                    }
                                    else if (tt.Minutes > 4)
                                    {
                                        return tt.Minutes + " minut";
                                    }
                                    else //if (tt.Minutes == 0)
                                    {
                                        if (tt.Seconds == 1)
                                        {
                                            return tt.Seconds + " sekunda";
                                        }
                                        else if (tt.Seconds > 1 && tt.Seconds < 5)
                                        {
                                            return tt.Seconds + " sekundy";
                                        }
                                        else //if (tt.Seconds > 4)
                                        {
                                            return tt.Seconds + " sekund";
                                        }

                                    }
                                }
                            }
                            else
                            {
                                return "1 den";
                            }
                        }

                        return tt.Days + " dnů";
                    }
                    else if (totalWeeks == 1)
                    {
                        return totalWeeks + " týden";
                    }
                    else if (totalWeeks < 5 && totalWeeks > 1)
                    {
                        return totalWeeks + " týdny";
                    }
                    else
                    {
                        return totalWeeks + " týdnů";
                    }
                }
                else
                {
                    if (months == 1)
                    {
                        return months + " měsíc";
                    }
                    else if (months > 1 && months < 5)
                    {
                        return months + " měsíce";
                    }
                    else
                    {
                        return months + " měsíců";
                    }
                }
            }
            else if (age == 1)
            {
                return "1 rok";
            }
            else if (age > 1 && age < 5)
            {
                return age + " roky";
            }
            else if (age > 4 || age == 0)
            {
                return age + " roků";
            }
            else
            {
                return "Neznámý věk";
            }
        }

        /// <summary>
        /// Tato metoda zatím funguje pouze česky, ať ji předáš parametr Langs jaký chceš..
        /// POkud bude !A2 a bude čas menší než 1 den, vrátí mi pro tuto časovou jednotku "1 den"
        /// A4 bylo původně MSStoredProceduresI.DateTimeMinVal
        /// </summary>
        /// <param name="dateTime"></param>
        /// <param name="calculateTime"></param>
        /// <returns></returns>
        public static string CalculateAgeAndAddRightStringKymCim(DateTime dateTime, bool calculateTime, Langs l, DateTime dtMinVal)
        {
            if (dateTime == dtMinVal)
            {
                return "";
            }
            int age = DTHelperGeneral.CalculateAge(dateTime, dtMinVal);

            if (age == 0)
            {
                DateTime Date1 = dateTime;
                DateTime Date2 = DateTime.Now;
                int months = (Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;
                if (months < 3)
                {
                    TimeSpan tt = Date2 - Date1;

                    int totalWeeks = tt.Days / 7;
                    if (totalWeeks == 0)
                    {
                        if (tt.Days == 1)
                        {
                            return tt.Days + " dnem";
                        }
                        else if (tt.Days < 5 && tt.Days > 1)
                        {
                            return tt.Days + " dny";
                        }
                        else
                        {
                            if (calculateTime)
                            {
                                if (tt.Hours == 1)
                                {
                                    return tt.Hours + " hodinou";
                                }
                                else if (tt.Hours > 1 && tt.Hours < 5)
                                {
                                    return tt.Hours + " hodinami";
                                }
                                else if (tt.Hours > 4)
                                {
                                    return tt.Hours + " hodinami";
                                }
                                else
                                {
                                    // Hodin je méně než 1
                                    if (tt.Minutes == 1)
                                    {
                                        return tt.Minutes + " minutou";
                                    }
                                    else if (tt.Minutes > 1 && tt.Minutes < 5)
                                    {
                                        return tt.Minutes + " minutami";
                                    }
                                    else if (tt.Minutes > 4)
                                    {
                                        return tt.Minutes + " minutami";
                                    }
                                    else //if (tt.Minutes == 0)
                                    {
                                        if (tt.Seconds == 1)
                                        {
                                            return tt.Seconds + " sekundou";
                                        }
                                        else if (tt.Seconds > 1 && tt.Seconds < 5)
                                        {
                                            return tt.Seconds + " sekundami";
                                        }
                                        else //if (tt.Seconds > 4)
                                        {
                                            return tt.Seconds + " sekundami";
                                        }

                                    }
                                }
                            }
                            else
                            {
                                return "1 dnem";
                            }
                        }

                        //return tt.Days + " dnů";
                    }
                    else if (totalWeeks == 1)
                    {
                        return totalWeeks + " týdnem";
                    }
                    else if (totalWeeks < 5 && totalWeeks > 1)
                    {
                        return totalWeeks + " týdny";
                    }
                    else
                    {
                        return totalWeeks + " týdny";
                    }
                }
                else
                {
                    if (months == 1)
                    {
                        return months + " měsícem";
                    }
                    else if (months > 1 && months < 5)
                    {
                        return months + " měsíci";
                    }
                    else
                    {
                        return months + " měsíců";
                    }
                }
            }
            else if (age == 1)
            {
                return "1 rokem";
            }
            else if (age > 1 && age < 5)
            {
                return age + " roky";
            }
            else if (age > 4 || age == 0)
            {
                return age + " roky";
            }
            else
            {
                return "Neznámý věk";
            }
        }

        #region CZ Other
        /// <summary>
        /// Vrátí český název dne v týdnu podle A1
        /// </summary>
        /// <param name="dayOfWeek"></param>
        /// <returns></returns>
        public static string DayOfWeek2DenVTydnu(DayOfWeek dayOfWeek)
        {
            switch (dayOfWeek)
            {
                case DayOfWeek.Monday:
                    return DTConstants.Pondeli;
                case DayOfWeek.Tuesday:
                    return DTConstants.Utery;
                case DayOfWeek.Wednesday:
                    return DTConstants.Streda;
                case DayOfWeek.Thursday:
                    return DTConstants.Ctvrtek;
                case DayOfWeek.Friday:
                    return DTConstants.Patek;
                case DayOfWeek.Saturday:
                    return DTConstants.Sobota;
                case DayOfWeek.Sunday:
                    return DTConstants.Nedele;
            }
            throw new Exception("Neznámý den v týdnu");
        }


        #endregion

        

        

        #region Český čas a/nebo datum
        /// <summary>
        /// Tato metoda vrací i čas, proto má v názvu i Time
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string DateTimeToStringWithDayOfWeekCS(DateTime dt)
        {
            return DayOfWeek2DenVTydnu(dt.DayOfWeek) + ", " + dt.Day + "." + dt.Month + "." + dt.Year + " " + NH.MakeUpTo2NumbersToZero(dt.Hour) + ":" + NH.MakeUpTo2NumbersToZero(dt.Minute);
        }

        /// <summary>
        /// Tato metoda bude vždy bezčasová! Proto má v názvu jen Date.
        /// 
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string DateToStringWithDayOfWeekCS(DateTime dt)
        {
            return DayOfWeek2DenVTydnu(dt.DayOfWeek) + ", " + dt.Day + "." + dt.Month + "." + dt.Year;
        }
        #endregion
    }
}
