using sunamo.Constants;
using System;
using System.Collections.Generic;
using System.Text;

namespace sunamo.Helpers.DT
{
    /// <summary>
    /// Underscore
    /// </summary>
    public class DTHelperUs
    {
        #region ToString
        /// <summary>
        /// yyyy_mm_dd 
        /// </summary>
        /// <param name="dt"></param>
        public static string DateTimeToFileName(DateTime dt)
        {
            string dDate = AllStrings.lowbar;
            string dSpace = AllStrings.lowbar;
            string dTime = AllStrings.lowbar;
            return DateTimeToFileName(dt, true);
        }

        /// <summary>
        /// yyyy_mm_dd 
        /// With A2 append hh_mm
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="time"></param>
        public static string DateTimeToFileName(DateTime dt, bool time)
        {
            string dDate = AllStrings.lowbar;
            string dSpace = AllStrings.lowbar;
            string dTime = AllStrings.lowbar;
            string vr = dt.Year + dDate + NH.MakeUpTo2NumbersToZero(dt.Month) + dDate + NH.MakeUpTo2NumbersToZero(dt.Day);
            if (time)
            {
                vr += dSpace + NH.MakeUpTo2NumbersToZero(dt.Hour) + dTime + NH.MakeUpTo2NumbersToZero(dt.Minute);
            }
            return vr;
        }
        #endregion

        #region Parse - FileNameToDateTime
        /// <summary>
        /// 1989_06_21_11_22 or 1989_06_21 if !A2
        /// Return null if A1 wont have right format
        /// </summary>
        /// <param name="fnwoe"></param>
        public static DateTime? FileNameToDateTimePrefix(string fnwoe, bool time, out string prefix)
        {
            List<string> sp = SH.SplitToPartsFromEnd(fnwoe, time ? 6 : 4, AllStrings.lowbar[0]);
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
        /// Return null if wont have right format
        /// If A2, A1 must have format ????_??_??_??_?? 
        /// if !A2, A1 must have format ????_??_??
        /// In any case what is after A2 is not important
        /// </summary>
        public static DateTime? FileNameToDateTimePostfix(string fnwoe, bool time, out string postfix)
        {
            var sp = SH.SplitToParts(fnwoe, time ? 6 : 4, AllStrings.lowbar);
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
        /// Return null if wont have right format
        /// If A2, A1 must have format ????_??_??_S_?* 
        /// 
        /// </summary>
        public static DateTime? FileNameToDateWithSeriePostfix(string fnwoe, out int? serie, out string postfix)
        {
            postfix = "";
            serie = null;

            var sp = SH.SplitToParts(fnwoe, 6, AllStrings.lowbar);

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
            serie = BTS.ParseInt(sp[4], null);


            return new DateTime(date[0], date[1], date[2]);
        }

        /// <summary>
        /// 1989_06_21_11_22
        /// Return null if wont have right format
        /// </summary>
        /// <param name="fnwoe"></param>
        public static DateTime? FileNameToDateTime(string fnwoe)
        {
            var sp = SH.Split(fnwoe, AllStrings.lowbar);
            // Tady jsem to rozděloval na 6 ale pak mi to vracelo null. Úprava na 5
            var dd = CA.ToInt(sp, 5);
            if (dd == null)
            {
                return null;
            }
            return new DateTime(dd[0], dd[1], dd[2], dd[3], dd[4], 0);
        }


        #endregion
    }
}