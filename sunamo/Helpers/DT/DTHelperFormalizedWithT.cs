using System;
using System.Collections.Generic;
using System.Text;

namespace sunamo.Helpers.DT
{
    /// <summary>
    /// Methods which in string result have T as delimiter date and time
    /// Next relative methods are in DTHelperFormalized / DTHelperCode
    /// </summary>
    public class DTHelperFormalizedWithT
    {
        

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
            return dt.Year + "-" + NH.MakeUpTo2NumbersToZero(dt.Month) + "-" + NH.MakeUpTo2NumbersToZero(dt.Day) + "T" + NH.MakeUpTo2NumbersToZero(dt.Hour) + ":" + NH.MakeUpTo2NumbersToZero(dt.Minute) + ":" + NH.MakeUpTo2NumbersToZero(dt.Second) + "." + NH.MakeUpTo3NumbersToZero(dt.Millisecond) + "Z";
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


    }
}
