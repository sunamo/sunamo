using System;
public partial class DTHelperCs
{
    static Type type = typeof(DTHelperCs);

    #region ToString
    #region Only date
    /// <summary>
    /// Středa, 21.6.1989
    /// </summary>
    /// <param name="dt"></param>
    public static string DateToStringWithDayOfWeekCS(DateTime dt)
    {
        return DayOfWeek2DenVTydnu(dt.DayOfWeek) + ", " + dt.Day + AllStrings.dot + dt.Month + AllStrings.dot + dt.Year;
    }
    #endregion

    #region Time with seconds
    /// <summary>
    /// 11:22 dont fill up with zero
    /// </summary>
    /// <param name="from"></param>
    public static string ToShortTimeFromSeconds(long from)
    {
        var dt = DateTime.MinValue;
        dt = dt.AddSeconds(from);

        return ToShortTime(dt, true);
    }

    /// <summary>
    /// With seconds
    /// 11:22:00 (depends on A2)
    /// </summary>
    /// <param name="value"></param>
    /// <param name="fillUpByZeros"></param>
    public static string ToShortTimeWithSecond(DateTime value, bool fillUpByZeros = false)
    {
        // Must be array due to params []
        var parts = CA.ToArrayT<int>(value.Hour, value.Minute, value.Second);

        return ToShortTimeWorker(parts, fillUpByZeros);
    }
    #endregion

    /// <summary>
    /// Must be int[] due to params[]
    /// </summary>
    /// <param name="parts"></param>
    /// <param name="fillUpByZeros"></param>
    static string ToShortTimeWorker(int[] parts, bool fillUpByZeros)
    {
        if (fillUpByZeros)
        {
            return SH.JoinMakeUpTo2NumbersToZero(AllStrings.colon, parts);
        }
        return SH.Join(AllStrings.colon, parts);
    }

    #region Time without seconds
    /// <summary>
    /// 11:22
    /// Without seconds
    /// </summary>
    /// <param name="value"></param>
    /// <param name="fillUpByZeros"></param>
    public static string ToShortTime(DateTime value, bool fillUpByZeros = false)
    {
        // Must be array due to params []
        var parts = CA.ToArrayT<int>(value.Hour, value.Minute);

        return ToShortTimeWorker(parts, fillUpByZeros);
    }
    #endregion
    #endregion

    #region Parse
    #region Time with seconds
    /// <summary>
    /// hh:mm:ss
    /// If fail, return DT.MinValue
    /// Seconds can be omit
    /// </summary>
    /// <param name="t"></param>
    public static DateTime ParseTimeCzech(string t)
    {
        var vr = DateTime.MinValue;
        var parts = SH.Split(t, AllChars.colon);
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
                        vr = vr.AddHours(hours);
                        vr = vr.AddMinutes(minutes);
                        vr = vr.AddSeconds(seconds);
                    }
                }
            }
        }
        return vr;
    }
    #endregion

    #region Date
    /// <summary>
    /// 21.6.1989. DateTime.MinValue when cannot be parsed
    /// </summary>
    /// <param name="input"></param>
    public static DateTime ParseDateCzech(string input)
    {
        DateTime vr = DateTime.MinValue;
        var parts = SH.Split(input, AllChars.dot);
        var day = -1;
        var month = -1;
        var year = -1;
        var dt = DateTime.Today;

        TryParse.Integer tpi = new TryParse.Integer();
        if (tpi.TryParseInt(parts[0]))
        {
            day = tpi.lastInt;
            if (parts.Count > 1)
            {

                if (tpi.TryParseInt(parts[1]))
                {

                    month = tpi.lastInt;
                    if (parts.Count > 2)
                    {
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
                    else
                    {
                        year = dt.Year;
                    }
                }

            }
            else
            {
                month = dt.Month;
            }
        }
        return vr;
    }  
    #endregion
    #endregion

    #region Helper
    /// <summary>
    /// </summary>
    /// <param name="dateTime"></param>
    /// <param name="calculateTime"></param>
    public static string CalculateAgeAndAddRightStringKymCim(DateTime dateTime, bool calculateTime, Langs l, DateTime dtMinVal)
    {
        if (l != Langs.cs)
        {
            ThrowExceptions.NotImplementedCase(Exc.GetStackTrace(),type, Exc.CallingMethod(), l);
        }

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
                            return "  dnem";
                        }
                    }

                    //return tt.Days + " dnů";
                }
                else if (totalWeeks == 1)
                {
                    return totalWeeks + " t\u00FDdnem";
                }
                else if (totalWeeks < 5 && totalWeeks > 1)
                {
                    return totalWeeks + " t\u00FDdny";
                }
                else
                {
                    return totalWeeks + " t\u00FDdny";
                }
            }
            else
            {
                if (months == 1)
                {
                    return months + " m\u011Bs\u00EDcem";
                }
                else if (months > 1 && months < 5)
                {
                    return months + " m\u011Bs\u00EDci";
                }
                else
                {
                    return months + " m\u011Bs\u00EDc\u016F";
                }
            }
        }
        else if (age == 1)
        {
            return "  rokem";
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
            return "Nezn\u00E1m\u00FD v\u011Bk";
        }
    }

    public static string IntervalToString(DateTime oDTStart, DateTime oDTEnd, Langs l, DateTime dtMinVal)
    {
        return DTHelperMulti.DateTimeToString(oDTStart, l, dtMinVal) + AllStrings.swda + DTHelperMulti.DateTimeToString(oDTEnd, l, dtMinVal);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="dayOfWeek"></param>
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
        ThrowExceptions.Custom(Exc.GetStackTrace(), type, Exc.CallingMethod(),"Nezn\u00E1m\u00FD den v t\u00FDdnu");
        return null;
    }
    #endregion
}