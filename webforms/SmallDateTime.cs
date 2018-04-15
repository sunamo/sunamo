using sunamo.Helpers.DT;
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
        long vr = DTHelperGeneral.SecondsInYear(t.Year);
        vr += DTHelperGeneral.SecondsInMonth(t);
        vr += DTConstants.secondsInDay * t.Day;
        vr += t.Hour * DTConstants.secondsInHour;
        vr += t.Minute * DTConstants.secondsInMinute;
        vr += t.Second;
        //vr *= TimeSpan.TicksPerSecond;
        if (addDateTimeMinVal)
        {
            vr += DateTimeToSecondsUnixTime(MSStoredProceduresI.DateTimeMinVal, false);
        }


        return vr;
    }


}
