using System;
using System.Collections.Generic;
using System.Text;

namespace sunamo.Helpers.DT
{
    /// <summary>
    /// For web frameworks - angular, jquery etc.  
    /// Must contains in header Input, Angular, jQuery, etc. 
    /// Next relative methods are in DTHelperFormalized / DTHelperFormalizedWithT
    /// </summary>
    public class DTHelperCode
    {
        public static string DateTimeToStringToInputDateTimeLocal(DateTime dt, DateTime dtMinVal)
        {
            if (dt == dtMinVal)
            {
                return "";
            }
            return dt.Year + "-" + NH.MakeUpTo2NumbersToZero(dt.Month) + "-" + NH.MakeUpTo2NumbersToZero(dt.Day) + "T" + NH.MakeUpTo2NumbersToZero(dt.Hour) + ":" + NH.MakeUpTo2NumbersToZero(dt.Minute);
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

        #region Perharps Cs
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
        #endregion

        /// <summary>
        /// Vrací například 12:00:00
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string TimeToStringAngularTime(DateTime dt)
        {
            return NH.MakeUpTo2NumbersToZero(dt.Hour) + ":" + NH.MakeUpTo2NumbersToZero(dt.Minute) + ":" + NH.MakeUpTo2NumbersToZero(dt.Second);
        }

        public static string DateToStringAngularDate(DateTime dt)
        {
            return dt.Year + NH.MakeUpTo2NumbersToZero(dt.Month) + NH.MakeUpTo2NumbersToZero(dt.Day) + "T00:00:00";
        }

        public static string DateAndTimeToStringAngularDateTime(DateTime dt)
        {
            return dt.Year + NH.MakeUpTo2NumbersToZero(dt.Month) + NH.MakeUpTo2NumbersToZero(dt.Day) + "T" + NH.MakeUpTo2NumbersToZero(dt.Hour) + ":" + NH.MakeUpTo2NumbersToZero(dt.Minute) + ":" + NH.MakeUpTo2NumbersToZero(dt.Second);
        }
    }
}
