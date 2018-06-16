using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace sunamo.Helpers.DT
{
    public class DTHelperGeneral
    {
        public static string TimeInMsToSeconds(Stopwatch p)
        {
            p.Stop();
            string d = ((double)p.ElapsedMilliseconds / 1000).ToString();
            if (d.Length > 4)
            {
                d = d.Substring(0, 4);
            }
            return d + "s";
            //return Math.Round(((double)p.ElapsedMilliseconds / 999), 2).ToString() + "s";
        }

        public static DateTime TodayPlusActualHour()
        {
            DateTime dt = DateTime.Today;
            return dt.AddHours(DateTime.Now.Hour);
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

        #region Seconds in
        public static long SecondsInMonth(DateTime dt)
        {
            return DTConstants.secondsInDay * DateTime.DaysInMonth(dt.Year, dt.Month);
        }

        public static long SecondsInYear(int year)
        {
            long mal = 365;
            if (DateTime.IsLeapYear(year))
            {
                mal = 366;
            }

            return mal * DTConstants.secondsInDay;
        }
        #endregion

        public static DateTime SetToday(DateTime ugtFirstStep)
        {
            DateTime t = DateTime.Today;
            return new DateTime(t.Year, t.Month, t.Day, ugtFirstStep.Hour, ugtFirstStep.Minute, ugtFirstStep.Second);
        }

        #endregion
        /// <summary>
        /// Počítá pouze čas, vrátí jako nenormalizovaný int
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public static long DateTimeToSecondsOnlyTime(DateTime t)
        {
            long vr = t.Hour * DTConstants.secondsInHour;
            vr += t.Minute * DTConstants.secondsInMinute;
            vr += t.Second;
            vr *= TimeSpan.TicksPerSecond;
            //vr += MSStoredProceduresI.DateTimeMinVal
            return vr;
        }

        public static string MakeUpTo2NumbersToZero(int p)
        {
            if (p.ToString().Length == 1)
            {
                return "0" + p;
            }
            return p.ToString();
        }
    }
}
