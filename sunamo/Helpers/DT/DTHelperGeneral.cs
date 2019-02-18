using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

    public partial class DTHelperGeneral
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

        public static string CalculateAgeString(DateTime bday, DateTime dtMinVal)
        {
            byte b = CalculateAge(bday, dtMinVal);
            if (b == 255)
            {
                return "";
            }
            return b.ToString();
        }

        public static DateTime SetMinute(DateTime d, int v)
        {
            return new DateTime(d.Year, d.Month, d.Day, d.Hour, v, d.Second);
        }

        public static DateTime SetHour(DateTime d, int v)
        {
            return new DateTime(d.Year, d.Month, d.Day, v, d.Minute, d.Second);
        }

        /// <summary>
        /// Subtract A2 from A1
        /// </summary>
        /// <param name="dt1"></param>
        /// <param name="dt2"></param>
        /// <returns></returns>
        public static TimeSpan Substract(DateTime dt1, DateTime dt2)
        {
            TimeSpan ts = dt1 - dt2;
            return ts;
        }

        public static DateTime SetDateToMinValue(DateTime dt)
        {
            DateTime minVal = DateTime.MinValue;
            return new DateTime(minVal.Year, minVal.Month, minVal.Day, dt.Hour, dt.Minute, dt.Second, dt.Millisecond);
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
            long vr = t.Hour * DTConstants.secondsInHour;
            vr += t.Minute * DTConstants.secondsInMinute;
            vr += t.Second;
            vr *= TimeSpan.TicksPerSecond;
            //vr += SqlServerHelper.DateTimeMinVal
            return vr;
        }


        public static DateTime Create(string day, string month, string hour, string minute)
        {
            
            return new DateTime(1, int.Parse(month), int.Parse(day), int.Parse(hour), int.Parse(minute), 0);
        }

        public static DateTime CreateTime(string v1, string v2)
        {
            DateTime today = DateTime.MinValue;
            today = today.AddHours(double.Parse(v1));
            today = today.AddHours(double.Parse(v2));
            return today;
        }
    }