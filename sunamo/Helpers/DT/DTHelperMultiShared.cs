using System;
using System.Collections.Generic;
using System.Text;

public partial class DTHelperMulti
{
    static Type type = typeof(DTHelperMulti);

    #region ToString
    /// <summary>
    /// 21.6.1989 11:22 (fill zero)
    /// 6/21/1989 11:22 (fill zero)
    /// Vrátí datum a čas v českém formátu bez ms a s
    /// </summary>
    /// <param name="d"></param>
    public static string DateTimeToString(DateTime d, Langs l, DateTime dtMinVal)
    {
        if (d == dtMinVal)
        {
            if (l == Langs.cs)
            {
                return SunamoPageHelperSunamo.i18n(XlfKeys.ItWasNotMentioned);
            }
            else
            {
                return SunamoPageHelperSunamo.i18n(XlfKeys.NotIndicated);
            }
        }

        if (l == Langs.cs)
        {
            // 21.6.1989 11:22 (fill zero)
            return d.Day + AllStrings.dot + d.Month + AllStrings.dot + d.Year + AllStrings.space + NH.MakeUpTo2NumbersToZero(d.Hour) + AllStrings.colon + NH.MakeUpTo2NumbersToZero(d.Minute);
        }
        else
        {
            // 6/21/1989 11:22 (fill zero)
            return d.Month + AllStrings.slash + d.Day + AllStrings.slash + d.Year + AllStrings.space + NH.MakeUpTo2NumbersToZero(d.Hour) + AllStrings.colon + NH.MakeUpTo2NumbersToZero(d.Minute);
        }
    }


    public static string TimeToString(DateTime d, Langs l, DateTime dtMinVal)
    {
        if (d == dtMinVal)
        {
            if (l == Langs.cs)
            {
                return SunamoPageHelperSunamo.i18n(XlfKeys.ItWasNotMentioned);
            }
            else
            {
                return SunamoPageHelperSunamo.i18n(XlfKeys.NotIndicated);
            }
        }

        if (l == Langs.cs)
        {
            // 21.6.1989 11:22 (fill zero)
            return  NH.MakeUpTo2NumbersToZero(d.Hour) + AllStrings.colon + NH.MakeUpTo2NumbersToZero(d.Minute);
        }
        else
        {
            // 6/21/1989 11:22 (fill zero)
            return NH.MakeUpTo2NumbersToZero(d.Hour) + AllStrings.colon + NH.MakeUpTo2NumbersToZero(d.Minute);
        }
    }

    

    /// <summary>
    /// 21.6.1989 (středa) / 6/21/1989 (wednesday)
    /// </summary>
    /// <param name="dateTime"></param>
    /// <param name="l"></param>
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

        return DateToString(dateTime, l) + " (" + dayOfWeek + AllStrings.rb;
    }
    #endregion

    public static string BoolToString(bool b, Langs l)
    {
        if (l == Langs.en)
        {
            return BTS.BoolToStringEn(b);
        }
        else if (l == Langs.cs)
        {
            return BTS.BoolToString(b);
        }
        else
        {
            ThrowExceptions.NotImplementedCase(Exc.GetStackTrace(), type, Exc.CallingMethod(), l);
            return string.Empty;
        }
    }

    public static string DateToStringWithDayOfWeek(DateTime dt, Langs l)
    {
        if (l == Langs.en)
        {
            return DTHelperEn.DateToStringWithDayOfWeekEN(dt);
        }
        else if (l == Langs.cs)
        {
            return DTHelperCs.DateToStringWithDayOfWeekCS(dt);
        }
        else
        {
            ThrowExceptions.NotImplementedCase(Exc.GetStackTrace(), type, Exc.CallingMethod(), l);
            return null;
        }
    }

    #region IsValid*
    /// <summary>
    /// Return whether can be parse with DTHelperCs.ParseDateCzech or DTHelperEn.ParseDateUSA
    /// </summary>
    /// <param name="r"></param>
    public static DateTime IsValidDateText(string r)
    {
        DateTime dt = DateTime.MinValue;
        r = r.Trim();
        if (r != "")
        {
            var indexTecky = r.IndexOf(AllChars.dot);
            if (indexTecky != -1)
            {
                dt = DTHelperCs.ParseDateCzech(r);
            }
            else
            {
                dt = DTHelperEn.ParseDateUSA(r);
            }
        }
        return dt;
    }

    /// <summary>
    /// A1 can be in en or cs
    /// parse time after first space
    /// </summary>
    /// <param name="datum"></param>
    public static DateTime IsValidDateTimeText(string datum)
    {
        DateTime vr = DateTime.MinValue;
        int indexMezery = datum.IndexOf(AllChars.space);
        if (indexMezery != -1)
        {
            var datum2 = DateTime.Today;
            var cas2 = DateTime.Today;
            var datum3 = datum.Substring(0, indexMezery);
            var cas3 = datum.Substring(indexMezery + 1);

            if (datum3.IndexOf(AllChars.dot) != -1)
            {
                datum2 = DTHelperCs.ParseDateCzech(datum3);
            }
            else
            {
                datum2 = DTHelperEn.ParseDateUSA(datum3);
            }

            if (cas3.IndexOf(AllChars.space) == -1)
            {
                cas2 = DTHelperCs.ParseTimeCzech(cas3);
            }
            else
            {
                cas2 = DTHelperEn.ParseTimeUSA(cas3);
            }

            if (datum2 != DateTime.MinValue && cas2 != DateTime.MinValue)
            {
                vr = new DateTime(datum2.Year, datum2.Month, datum2.Day, cas2.Hour, cas2.Minute, cas2.Second);
            }
        }

        return vr;
    }

    public static DateTime IsValidTimeText(string r)
    {
        DateTime dt = DateTime.MinValue;
        r = r.Trim();
        if (r != "")
        {
            var indexMezery = r.IndexOf(AllChars.space);
            if (indexMezery == -1)
            {
                dt = DTHelperCs.ParseTimeCzech(r);
            }
            else
            {
                dt = DTHelperEn.ParseTimeUSA(r);
            }
        }
        return dt;
    }
    #endregion

   
}