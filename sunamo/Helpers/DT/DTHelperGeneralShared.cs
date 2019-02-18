using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

public partial class DTHelperGeneral{ 
public static string MakeUpTo2NumbersToZero(int p)
        {
            if (p.ToString().Length == 1)
            {
                return "0" + p;
            }
            return p.ToString();
        }

/// <summary>
        /// A2 bylo původně SqlServerHelper.DateTimeMinVal
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
}