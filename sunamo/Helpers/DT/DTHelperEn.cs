using System;
using System.Collections.Generic;
using System.Text;

    public partial class DTHelperEn
    {

        //public static string ToShortTime(DateTime value)
        //{
        //    return 
        //}


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
            }
            days *= -1;
            return DateTime.Today.AddDays(days);
        }
    }