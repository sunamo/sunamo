using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


    public class SmallDateTime
    {
        public static DateTime RandomSmallDateTime()
        {
            TimeSpan ts = DateTime.Today - MSStoredProceduresI.DateTimeMinVal;
            int pridatDnu = RandomHelper.RandomInt2(1, ts.Days);
            DateTime vr = MSStoredProceduresI.DateTimeMinVal.AddDays(pridatDnu);
            return vr;
        }

    /// <summary>
    /// Ověřit zda funguje do A1 vložit čas zobrazený na stránkách zobrazující akt. unix čas a porovnat výsledek
    /// </summary>
    /// <param name="t"></param>
    /// <param name="addDateTimeMinVal"></param>
    /// <returns></returns>
    public static long DateTimeToSecondsUnixTime(DateTime t, bool addDateTimeMinVal)
    {
        long vr = DTHelper.SecondsInYear(t.Year);
        vr += DTHelper.SecondsInMonth(t);
        vr += DTHelper.secondsInDay * t.Day;
        vr += t.Hour * DTHelper.secondsInHour;
        vr += t.Minute * DTHelper.secondsInMinute;
        vr += t.Second;
        //vr *= TimeSpan.TicksPerSecond;
        if (addDateTimeMinVal)
        {
            vr += DateTimeToSecondsUnixTime(MSStoredProceduresI.DateTimeMinVal, false);
        }


        return vr;
    }


}
