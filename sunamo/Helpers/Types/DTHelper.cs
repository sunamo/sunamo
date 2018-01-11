
using sunamo;
using System;
using System.Collections.Generic;
using System.Diagnostics;

public enum DateTimeFormatStyles
{
    /// <summary>
    /// 2011-10-18 10:30
    /// </summary>
    FullCalendar
}

public class DTHelper
{
    static string dash = "-";
    static string space = " ";
    static string colon = ":";
    static string underscore = "_";

    /// <summary>
    /// Kontroluje i na MinValue a MaxValue
    /// </summary>
    /// <param name="dt"></param>
    /// <returns></returns>
    public static bool HasNullableDateTimeValue(DateTime? dt)
    {
        if (dt.HasValue)
        {
            if (dt.Value != DateTime.MinValue && dt.Value != DateTime.MaxValue)
            {

                return true;
            }
        }

        return false;
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

    public static string DateTimeToFileName(DateTime dt, bool time)
    {
        string dDate = underscore;
        string dSpace = underscore;
        string dTime = underscore;
        string vr = dt.Year + dDate + NH.MakeUpTo2NumbersToZero(dt.Month) + dDate + NH.MakeUpTo2NumbersToZero(dt.Day);
        if (time)
        {
            vr += dSpace + NH.MakeUpTo2NumbersToZero(dt.Hour) + dTime + NH.MakeUpTo2NumbersToZero(dt.Minute);
        }
        return vr;
    }

    /// <summary>
    /// Vrátí null pokud A1 nebude mít správný formát
    /// </summary>
    /// <param name="fnwoe"></param>
    /// <returns></returns>
    public static DateTime? FileNameToDateTimePrefix(string fnwoe, bool time, out string prefix)
    {
        string[] sp = SH.SplitToPartsFromEnd(fnwoe, time ? 6 : 4, underscore[0]);
        if (time)
        {
            prefix = sp[0];
            var dd = CA.ToInt(sp, 5, 1);
            if (dd == null)
            {
                return null;
            }
            return new DateTime(dd[0], dd[1], dd[2], dd[3], dd[4], 0);
        }
        else
        {
            prefix = sp[0];
            var dd = CA.ToInt(sp, 3, 1);
            if (dd == null)
            {
                return null;
            }
            return new DateTime(dd[0], dd[1], dd[2]);
        }
    }

    /// <summary>
    /// Vrátí null pokud A1 nebude mít správný formát
    /// Pokud A2, A1 musí mít formát ????_??_??_??_?? 
    /// Pokud !A2, A1 musí mít formát ????_??_??
    /// V obojím případě co je za A2 je nepodstatné
    /// </summary>
    /// <param name="fnwoe"></param>
    /// <returns></returns>
    public static DateTime? FileNameToDateTimePostfix(string fnwoe, bool time, out string postfix)
    {
        string[] sp = SH.SplitToParts(fnwoe, time ? 6 : 4, underscore);
        if (time)
        {
            if (CA.HasIndex(5, sp))
            {
                postfix = sp[5];
            }
            else
            {
                postfix = "";
                return null;
            }

            var date = CA.ToInt(sp, 3, 0);
            if (date == null)
            {
                return null;
            }

            var time2 = CA.ToInt(sp, 2, 3);
            if (time2 == null)
            {
                return null;
            }

            return new DateTime(date[0], date[1], date[2], time2[0], time2[1], 0);
        }
        else
        {
            if (CA.HasIndex(3, sp))
            {
                postfix = sp[3];
            }
            else
            {
                postfix = "";
                return null;
            }

            var dd = CA.ToInt(sp, 3, 0);
            if (dd == null)
            {
                return null;
            }
            return new DateTime(dd[0], dd[1], dd[2]);
        }
    }

    /// <summary>
    /// Vrátí null pokud A1 nebude mít správný formát
    /// Pokud A2, A1 musí mít formát ????_??_??_S_??
    /// Pokud !A2, A1 musí mít formát ????_??_??
    /// V obojím případě co je za A2 je nepodstatné
    /// </summary>
    /// <param name="fnwoe"></param>
    /// <returns></returns>
    public static DateTime? FileNameToDateWithSeriePostfix(string fnwoe, out int? serie, out string postfix)
    {
        postfix = "";
        serie = null;

        string[] sp = SH.SplitToParts(fnwoe, 6, underscore);

        if (CA.HasIndex(5, sp))
        {
            postfix = sp[5];
        }
        else
        {
            postfix = "";
            return null;
        }

        var date = CA.ToInt(sp, 3, 0);
        if (date == null)
        {
            return null;
        }
        if (sp[3] != "S")
        {
            return null;
        }
        serie = BT.ParseInt(sp[4], null);


        return new DateTime(date[0], date[1], date[2]);

    }

    #region Dny v týdny CS
    const string Pondeli = "Pondělí";
    const string Utery = "Úterý";
    const string Streda = "Středa";
    const string Ctvrtek = "Čtvrtek";
    const string Patek = "Pátek";
    const string Sobota = "Sobota";
    const string Nedele = "Neděle";
    #endregion

    #region Měsíce v roce CS
    const string Leden = "Leden";
    const string Unor = "Únor";
    const string Brezen = "Březen";
    const string Duben = "Duben";
    const string Kveten = "Květen";
    const string Cerven = "Červen";
    const string Cervenec = "Červenec";
    const string Srpen = "Srpen";
    const string Zari = "Září";
    const string Rijen = "Říjen";
    const string Listopad = "Listopad";
    const string Prosinec = "Prosinec"; 
    #endregion

    public static readonly string[] daysInWeekCS = new string[] { Pondeli, Utery, Streda, Ctvrtek, Patek, Sobota, Nedele };
    public static readonly string[] daysInWeekEN = new string[] { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday" };
    public static readonly string[] monthsInYearEN = new string[] { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };
    public static readonly string[] monthsInYearCZ = new string[] { Leden, Unor, Brezen, Duben, Kveten, Cerven, Cervenec, Srpen, Zari, Rijen, Listopad, Prosinec };


    public const long secondsInMinute = 60;
    public const long secondsInHour = secondsInMinute * 60;
    public const long secondsInDay = secondsInHour * 24;
    public static long SecondsInMonth(DateTime dt)
    {
        return secondsInDay * DateTime.DaysInMonth(dt.Year, dt.Month);
    }

    public static long SecondsInYear(int year)
    {
        long mal = 365;
        if (DateTime.IsLeapYear(year))
        {
            mal = 366;
        }

        return mal * secondsInDay;
    }

    public static DateTime SetToday(DateTime ugtFirstStep)
    {
        DateTime t = DateTime.Today;
        return new DateTime(t.Year, t.Month, t.Day, ugtFirstStep.Hour, ugtFirstStep.Minute, ugtFirstStep.Second);
    }

    /// <summary>
    /// Počítá pouze čas, vrátí jako nenormalizovaný int
    /// </summary>
    /// <param name="t"></param>
    /// <returns></returns>
    public static long DateTimeToSecondsOnlyTime(DateTime t)
    {
        long vr = t.Hour * secondsInHour;
        vr += t.Minute * secondsInMinute;
        vr += t.Second;
        vr *= TimeSpan.TicksPerSecond;
        //vr += MSStoredProceduresI.DateTimeMinVal
        return vr;
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
                return Pondeli;
            case DayOfWeek.Tuesday:
                return Utery;
            case DayOfWeek.Wednesday:
                return Streda;
            case DayOfWeek.Thursday:
                return Ctvrtek;
            case DayOfWeek.Friday:
                return Patek;
            case DayOfWeek.Saturday:
                return Sobota;
            case DayOfWeek.Sunday:
                return Nedele;
        }
        throw new Exception("Neznámý den v týdnu");
    }

    public static string FormatDateTime(DateTime dt, DateTimeFormatStyles fullCalendar)
    {
        if (fullCalendar == DateTimeFormatStyles.FullCalendar)
        {
            //2011-10-18 10:30
            return dt.Year + "-" + NH.MakeUpTo2NumbersToZero(dt.Month) + "-" + NH.MakeUpTo2NumbersToZero(dt.Day) + " " + NH.MakeUpTo2NumbersToZero(dt.Hour) + ":" + NH.MakeUpTo2NumbersToZero(dt.Minute);
        }
        return "";
    }

    public static string DateTimeToFileName(DateTime dt)
    {
        string dDate = underscore;
        string dSpace = underscore;
        string dTime = underscore;
        return dt.Year + dDate + NH.MakeUpTo2NumbersToZero(dt.Month) + dDate + NH.MakeUpTo2NumbersToZero(dt.Day) + dSpace + NH.MakeUpTo2NumbersToZero(dt.Hour) + dTime + NH.MakeUpTo2NumbersToZero(dt.Minute);
    }

    /// <summary>
    /// Vrátí null pokud A1 nebude mít správný formát
    /// </summary>
    /// <param name="fnwoe"></param>
    /// <returns></returns>
    public static DateTime? FileNameToDateTime(string fnwoe)
    {
        string[] sp = SH.Split(fnwoe, underscore);
        var dd = CA.ToInt(sp, 6);
        if (dd == null)
        {
            return null;
        }
        return new DateTime(dd[0], dd[1], dd[2], dd[3], dd[4], 0);
    }
    #endregion

    #region Formalizované datum a/nebo čas
    /// <summary>
    /// Vrátí formalizované datum - tedyu např. 1989-06-21
    /// </summary>
    /// <param name="dt"></param>
    /// <returns></returns>
    public static string DateTimeToStringFormalizeDate(DateTime dt)
    {
        return dt.Year + "-" +  NH.MakeUpTo2NumbersToZero(dt.Month) + "-" + NH.MakeUpTo2NumbersToZero(dt.Day);
    }

    /// <summary>
    /// Vrátí normalizovaný datum a čas, to znamená že bude oddělen T
    /// Čas bude nastaven na 00:00:00
    /// </summary>
    /// <param name="dt"></param>
    /// <returns></returns>
    public static string DateTimeToStringFormalizeDateEmptyTime(DateTime dt)
    {
        return dt.Year + "-" + NH.MakeUpTo2NumbersToZero(dt.Month) + "-" + NH.MakeUpTo2NumbersToZero(dt.Day) + "T00:00:00";
    }

    public static string DateTimeToStringStringifyDateEmptyTime(DateTime dt)
    {
        return dt.Year + "-" + NH.MakeUpTo2NumbersToZero(dt.Month) + "-" + NH.MakeUpTo2NumbersToZero(dt.Day) + "T00:00:00.000Z";
    }

    public static string DateTimeToStringStringifyDateTime(DateTime dt)
    {
        return dt.Year + "-" + NH.MakeUpTo2NumbersToZero(dt.Month) + "-" + NH.MakeUpTo2NumbersToZero(dt.Day) + "T"+ NH.MakeUpTo2NumbersToZero(dt.Hour)+":"+ NH.MakeUpTo2NumbersToZero(dt.Minute)+":"+ NH.MakeUpTo2NumbersToZero(dt.Second)+"."+ NH.MakeUpTo3NumbersToZero(dt.Millisecond)+"Z";
    }

    public static string DateToStringAngularDate(DateTime dt)
    {
        return dt.Year + NH.MakeUpTo2NumbersToZero(dt.Month) + NH.MakeUpTo2NumbersToZero(dt.Day) + "T00:00:00"; 
    }

    /// <summary>
    /// Vrací například 12:00:00
    /// </summary>
    /// <param name="dt"></param>
    /// <returns></returns>
    public static string TimeToStringAngularTime(DateTime dt)
    {
        return NH.MakeUpTo2NumbersToZero(dt.Hour) + ":" + NH.MakeUpTo2NumbersToZero(dt.Minute) + ":" + NH.MakeUpTo2NumbersToZero(dt.Second);
    }

    public static string DateAndTimeToStringAngularDateTime(DateTime dt)
    {
        return dt.Year  + NH.MakeUpTo2NumbersToZero(dt.Month)  + NH.MakeUpTo2NumbersToZero(dt.Day) + "T" + NH.MakeUpTo2NumbersToZero(dt.Hour) + ":" + NH.MakeUpTo2NumbersToZero(dt.Minute) + ":" + NH.MakeUpTo2NumbersToZero(dt.Second);
    }

    /// <summary>
    /// Vrátí normalizovaný datum a čas, to znamená že bude oddělen T, jednotlivé části datumu budou odděleny - a času :
    /// </summary>
    /// <param name="dt"></param>
    /// <returns></returns>
    public static string DateAndTimeToStringFormalizeDate(DateTime dt)
    {
        return dt.Year + "-" + NH.MakeUpTo2NumbersToZero(dt.Month) + "-" + NH.MakeUpTo2NumbersToZero(dt.Day) + "T" + NH.MakeUpTo2NumbersToZero(dt.Hour) + ":" + NH.MakeUpTo2NumbersToZero(dt.Minute) + ":" + NH.MakeUpTo2NumbersToZero(dt.Second);
    }

    public static string IntervalToString(DateTime oDTStart, DateTime oDTEnd, Langs l, DateTime dtMinVal)
    {
        return DTHelper.DateTimeToString(oDTStart, l, dtMinVal) + " - " + DTHelper.DateTimeToString(oDTEnd, l, dtMinVal);
    }

    public static DateTime StringToDateTimeFromInputDateTimeLocal(string v, DateTime dtMinVal)
    {
        if (!v.Contains("-"))
        {
            return dtMinVal;
        }
        //2015-09-03T21:01
        string[] sp = SH.Split(v, '-', 'T', ':');
        var dd = CA.ToInt(sp);
        return new DateTime(dd[0], dd[1], dd[2], dd[3], dd[4], 0);
    }

    public static string DateTimeToStringToInputDateTimeLocal(DateTime dt, DateTime dtMinVal)
    {
        if (dt == dtMinVal)
        {
            return "";
        }
        return dt.Year + "-" + NH.MakeUpTo2NumbersToZero(dt.Month) + "-" + NH.MakeUpTo2NumbersToZero(dt.Day) + "T" + NH.MakeUpTo2NumbersToZero(dt.Hour) + ":" + NH.MakeUpTo2NumbersToZero(dt.Minute);
    }
    #endregion

    public static string MakeUpTo2NumbersToZero(int p)
    {
        if (p.ToString().Length == 1)
        {
            return "0" + p;
        }
        return p.ToString();
    }

    public static string DateToStringOrSE(DateTime p, Langs l, DateTime dtMinVal)
    {
        if (p == dtMinVal)
        {
            return "";
        }
        return DTHelper.DateToString(p, l);
    }

    #region General
    /// <summary>
    /// A2 bylo původně MSStoredProceduresI.DateTimeMinVal
    /// </summary>
    /// <param name="bday"></param>
    /// <returns></returns>
    public static byte CalculateAge(DateTime bday, DateTime dtMinVal)
    {
        if (bday == dtMinVal)
        {
            return 255;
        }
        DateTime today = DateTime.Today;
        int age = today.Year - bday.Year;
        if (bday > today.AddYears(-age)) age--;
        byte vr = (byte)age;
        if (vr == 255)
        {
            return 0;
        }
        return vr;
    }

    public static string CalculateAgeString(DateTime bday, DateTime dtMinVal)
    {
        byte b = CalculateAge(bday, dtMinVal);
        if (b == 255)
        {
            return "";
        }
        return b.ToString();
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

    /// <summary>
    /// Tato metoda bude vždy bezčasová! Proto má v názvu jen Date.
    /// Input v názvu znamená že výstup z této metody budu vkládat do inputů, nikoliv nic se vstupem A1
    /// </summary>
    /// <param name="dt"></param>
    /// <returns></returns>
    public static string DateToStringjQueryDatePicker(DateTime dt)
    {
        //return NH.MakeUpTo2NumbersToZero(dt.Day) + "." + NH.MakeUpTo2NumbersToZero(dt.Month) + "." + dt.Year;
        return NH.MakeUpTo2NumbersToZero(dt.Month) + "/" + NH.MakeUpTo2NumbersToZero(dt.Day) + "/" + dt.Year;
    }

    /// <summary>
    /// Vrátí datum v českém formátu(například 21.6.1989)
    /// </summary>
    /// <param name="p"></param>
    /// <returns></returns>
    public static string DateToString(DateTime p, Langs l)
    {
        if (l == Langs.cs)
        {
            return p.Day + "." + p.Month + "." + p.Year;
        }
        return p.Month + "/" + p.Day + "/" + p.Year;
    } 
    #endregion

    #region Parse EN->CZ
    /// <summary>
    /// Vyparsuje datum ve formátu měsíc/den/rok
    /// </summary>
    /// <param name="p"></param>
    /// <returns></returns>
    public static DateTime? ParseDateMonthDayYear(string p)
    {
        string[] s = SH.SplitNone(p, "/");
        DateTime vr;
        if (DateTime.TryParse(s[1] + "." + s[0] + "." + s[2], out vr))
        {
            return vr;
        }
        return null;
    } 
    #endregion

    #region Pouze čas
    /// <summary>
    /// Vrátím aktuální čas(například 12:00:00 a zatím A1
    /// </summary>
    /// <param name="defin"></param>
    /// <returns></returns>
    public static string AppendToFrontOnlyTime(string defin)
    {
        DateTime dt = DateTime.Now;
        return NH.MakeUpTo2NumbersToZero(dt.Hour) + ":" + NH.MakeUpTo2NumbersToZero(dt.Minute) + ":" + NH.MakeUpTo2NumbersToZero(dt.Second) + ":" + NH.MakeUpTo3NumbersToZero(dt.Millisecond) + " " + defin;
    }


    #endregion

    public static string TimeInMsToSeconds(Stopwatch p)
    {
        p.Stop();
        string d = ((double)p.ElapsedMilliseconds / 1000).ToString() ;
        if (d.Length > 4)
        {
            d = d.Substring(0, 4);
        }
        return d + "s";
        //return Math.Round(((double)p.ElapsedMilliseconds / 999), 2).ToString() + "s";
    }

    /// <summary>
    /// Vrátí datum a čas v českém formátu bez ms a s
    /// </summary>
    /// <param name="d"></param>
    /// <returns></returns>
    public static string DateTimeToString(DateTime d, Langs l, DateTime dtMinVal)
    {
        if (d == dtMinVal)
        {
            if (l == Langs.cs)
            {
                return "Nebylo uvedeno";
            }
            else
            {
                return "Not indicated";
            }
        }

        if (l == Langs.cs)
        {
            return d.Day + "." + d.Month + "." + d.Year + " " + NH.MakeUpTo2NumbersToZero(d.Hour) + ":" + NH.MakeUpTo2NumbersToZero(d.Minute);
        }
        else
        {
            return d.Month + "/" + d.Day + "/" + d.Year + " " + NH.MakeUpTo2NumbersToZero(d.Hour) + ":" + NH.MakeUpTo2NumbersToZero(d.Minute);
        }

    }

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
        int age =  CalculateAge(dateTime, dtMinVal);

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
                else if(totalWeeks == 1)
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
        else if(age > 1 && age <5)
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
        int age = CalculateAge(dateTime, dtMinVal);

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

    public static DateTime TodayPlusActualHour()
    {
        DateTime dt = DateTime.Today;
        return dt.AddHours(DateTime.Now.Hour);
    }

    public static DateTime IsValidDateTimeText(string datum)
    {
        DateTime vr = DateTime.MinValue;
        int indexMezery = datum.IndexOf(' ');
        if (indexMezery != -1)
        {
            var datum2 = DateTime.Today;
            var cas2 = DateTime.Today;
            var datum3 = datum.Substring(0, indexMezery);
            var cas3 = datum.Substring(indexMezery + 1);
            if (datum3.IndexOf('.') != -1)
            {
                datum2 = ParseDateCzech(datum3);
            }
            else
            {
                datum2 = ParseDateUSA(datum3);
            }

            if (cas3.IndexOf(' ') == -1)
            {
                cas2 = ParseTimeCzech(cas3);
            }
            else
            {
                cas2 = ParseTimeUSA(cas3);
            }

            if (datum2 != DateTime.MinValue && cas2 != DateTime.MinValue)
            {
                vr = new DateTime(datum2.Year, datum2.Month, datum2.Day, cas2.Hour, cas2.Minute, cas2.Second);
            }


        }

        return vr;
    }

    private static DateTime ParseTimeUSA(string t)
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

    private static DateTime ParseTimeCzech(string t)
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
                        vr = vr.AddHours (hours);
                        vr = vr.AddMinutes(minutes);
                        vr = vr.AddSeconds(seconds);
                    }
                }
            }
        }
        return vr;
    }

    private static DateTime ParseDateUSA(string input)
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

    private static DateTime ParseDateCzech(string input)
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

    public static DateTime IsValidDateText(string r)
    {
        DateTime dt = DateTime.MinValue;
        r = r.Trim();
        if (r != "")
        {
            var indexTecky = r.IndexOf('.');
            if (indexTecky != -1)
            {
                dt = ParseDateCzech(r);
            }
            else
            {
                dt = ParseDateUSA(r);
            }
        }
        return dt;
    }

    public static DateTime IsValidTimeText(string r)
    {
        DateTime dt = DateTime.MinValue;
        r = r.Trim();
        if (r != "")
        {
            var indexMezery = r.IndexOf(' ');
            if (indexMezery == -1)
            {
                dt = ParseTimeCzech(r);
            }
            else
            {
                dt = ParseTimeUSA(r);
            }
        }
        return dt;
    }

    public static DateTime StringToDateTimeFormalizeDate(string p)
    {
        return DateTime.Parse(p, null, System.Globalization.DateTimeStyles.None);
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

        string dayOfWeek = daysInWeekEN[day];
        if (l  == Langs.cs)
        {
            dayOfWeek = daysInWeekCS[day];
        }

        return DateToString(dateTime, l) + " (" + dayOfWeek + ")";
    }

    
}
