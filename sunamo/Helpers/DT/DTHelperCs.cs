using System;
using System.Collections.Generic;
using System.Text;

public partial class DTHelperCs
{
    /// <summary>
    /// Vrátím aktuální čas(například 12:00:00:000 a zatím A1
    /// </summary>
    /// <param name="defin"></param>
    /// <returns></returns>
    public static string AppendToFrontOnlyTime(string defin)
    {
        DateTime dt = DateTime.Now;
        return NH.MakeUpTo2NumbersToZero(dt.Hour) + AllStrings.colon + NH.MakeUpTo2NumbersToZero(dt.Minute) + AllStrings.colon + NH.MakeUpTo2NumbersToZero(dt.Second) + AllStrings.colon + NH.MakeUpTo3NumbersToZero(dt.Millisecond) + AllStrings.space + defin;
    }

    public static string IntervalToString(DateTime oDTStart, DateTime oDTEnd, Langs l, DateTime dtMinVal)
    {
        return DTHelperMulti.DateTimeToString(oDTStart, l, dtMinVal) + AllStrings.swda + DTHelperMulti.DateTimeToString(oDTEnd, l, dtMinVal);
    }

    /// <summary>
    /// POkud bude !A2 a bude čas menší než 1 den, vrátí mi pro tuto časovou jednotku " " + " den"
    /// A3 bylo původně SqlServerHelper.DateTimeMinVal
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
                        return " " + " den";
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
                                return tt.Hours + " " + "hodina";
                            }
                            else if (tt.Hours > 1 && tt.Hours < 5)
                            {
                                return tt.Hours + " " + "hodiny";
                            }
                            else if (tt.Hours > 4)
                            {
                                return tt.Hours + " " + "hodin";
                            }
                            else
                            {
                                // Hodin je méně než 1
                                if (tt.Minutes == 1)
                                {
                                    return tt.Minutes + " " + "minuta";
                                }
                                else if (tt.Minutes > 1 && tt.Minutes < 5)
                                {
                                    return tt.Minutes + " " + "minuty";
                                }
                                else if (tt.Minutes > 4)
                                {
                                    return tt.Minutes + " " + "minut";
                                }
                                else //if (tt.Minutes == 0)
                                {
                                    if (tt.Seconds == 1)
                                    {
                                        return tt.Seconds + " " + "sekunda";
                                    }
                                    else if (tt.Seconds > 1 && tt.Seconds < 5)
                                    {
                                        return tt.Seconds + " " + "sekundy";
                                    }
                                    else //if (tt.Seconds > 4)
                                    {
                                        return tt.Seconds + " " + "sekund";
                                    }
                                }
                            }
                        }
                        else
                        {
                            return tt.Days + " dn\u016F";
                        }
                    }
                }
                else if (totalWeeks == 1)
                {
                    return totalWeeks + " " + "t\u00FDden";
                }
                else if (totalWeeks < 5 && totalWeeks > 1)
                {
                    return totalWeeks + " " + "t\u00FDdny";
                }
                else
                {
                    return totalWeeks + " " + "t\u00FDdn\u016F";
                }
            }
            else
            {
                if (months == 1)
                {
                    return months + " " + "m\u011Bs\u00EDc";
                }
                else if (months > 1 && months < 5)
                {
                    return months + " " + "m\u011Bs\u00EDce";
                }
                else
                {
                    return months + " " + "m\u011Bs\u00EDc\u016F";
                }
            }
        }
        else if (age == 1)
        {
            return " " + " rok";
        }
        else if (age > 1 && age < 5)
        {
            return age + " " + "roky";
        }
        else if (age > 4 || age == 0)
        {
            return age + " " + "rok\u016F";
        }
        else
        {
            return "Nezn\u00E1m\u00FD v\u011Bk";
        }
    }

    /// <summary>
    /// Tato metoda vrací i čas, proto má v názvu i Time
    /// </summary>
    /// <param name="dt"></param>
    /// <returns></returns>
    public static string DateTimeToStringWithDayOfWeekCS(DateTime dt)
    {
        return DayOfWeek2DenVTydnu(dt.DayOfWeek) + ", " + dt.Day + AllStrings.dot + dt.Month + AllStrings.dot + dt.Year + AllStrings.space + NH.MakeUpTo2NumbersToZero(dt.Hour) + AllStrings.colon + NH.MakeUpTo2NumbersToZero(dt.Minute);
    }
}