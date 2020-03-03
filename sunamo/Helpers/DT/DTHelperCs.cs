using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

public partial class DTHelperCs
{
    #region ToString
    #region Time (with s and ms)
    /// <summary>
    /// Return actual time(for example 12:00:00:000) and after that A1 postfix
    /// </summary>
    /// <param name="defin"></param>
    
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
    #endregion
}