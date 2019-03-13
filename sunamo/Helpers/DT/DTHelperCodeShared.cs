using System;
using System.Collections.Generic;
using System.Text;

public partial class DTHelperCode{ 
public static string DateToStringAngularDate(DateTime dt)
    {
        return dt.Year + NH.MakeUpTo2NumbersToZero(dt.Month) + NH.MakeUpTo2NumbersToZero(dt.Day) + "T00:00:00";
    }


        /// <summary>
        /// Vrací například 12:00:00
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string TimeToStringAngularTime(DateTime dt)
        {
            return NH.MakeUpTo2NumbersToZero(dt.Hour) + ":" + NH.MakeUpTo2NumbersToZero(dt.Minute) + ":" + NH.MakeUpTo2NumbersToZero(dt.Second);
        }
}