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
        /// <summary>
        /// M_d_y
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string DateTimeToFileName(DateTime dt)
        {
            string dDate = AllStrings.us;
            string dSpace = AllStrings.us;
            string dTime = AllStrings.us;
            return dt.Year + dDate + NH.MakeUpTo2NumbersToZero(dt.Month) + dDate + NH.MakeUpTo2NumbersToZero(dt.Day) + dSpace + NH.MakeUpTo2NumbersToZero(dt.Hour) + dTime + NH.MakeUpTo2NumbersToZero(dt.Minute);
        }

        #region FileNameToDateTime
        /// <summary>
        /// Vrátí null pokud A1 nebude mít správný formát
        /// </summary>
        /// <param name="fnwoe"></param>
        /// <returns></returns>
        public static DateTime? FileNameToDateTimePrefix(string fnwoe, bool time, out string prefix)
        {
            string[] sp = SH.SplitToPartsFromEnd(fnwoe, time ? 6 : 4, AllStrings.us[0]);
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
            string[] sp = SH.SplitToParts(fnwoe, time ? 6 : 4, AllStrings.us);
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

            string[] sp = SH.SplitToParts(fnwoe, 6, AllStrings.us);

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

        /// <summary>
        /// Vrátí null pokud A1 nebude mít správný formát
        /// </summary>
        /// <param name="fnwoe"></param>
        /// <returns></returns>
        public static DateTime? FileNameToDateTime(string fnwoe)
        {
            string[] sp = SH.Split(fnwoe, AllStrings.us);
            var dd = CA.ToInt(sp, 6);
            if (dd == null)
            {
                return null;
            }
            return new DateTime(dd[0], dd[1], dd[2], dd[3], dd[4], 0);
        }


        #endregion

        public static string DateTimeToFileName(DateTime dt, bool time)
        {
            string dDate = AllStrings.us;
            string dSpace = AllStrings.us;
            string dTime = AllStrings.us;
            string vr = dt.Year + dDate + NH.MakeUpTo2NumbersToZero(dt.Month) + dDate + NH.MakeUpTo2NumbersToZero(dt.Day);
            if (time)
            {
                vr += dSpace + NH.MakeUpTo2NumbersToZero(dt.Hour) + dTime + NH.MakeUpTo2NumbersToZero(dt.Minute);
            }
            return vr;
        }
    }
}
