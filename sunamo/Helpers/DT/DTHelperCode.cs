using System;
using System.Collections.Generic;
using System.Text;

/// <summary>
/// For web frameworks - angular, jquery etc.  
/// Must contains in header Input, Angular, jQuery, etc. 
/// Next relative methods are in DTHelperFormalized / DTHelperFormalizedWithT
/// </summary>
public partial class DTHelperCode
{
    public static string DateTimeToStringToInputDateTimeLocal(DateTime dt, DateTime dtMinVal)
    {
        if (dt == dtMinVal)
        {
            return "";
        }
        return dt.Year + AllStrings.dash + NH.MakeUpTo2NumbersToZero(dt.Month) + AllStrings.dash + NH.MakeUpTo2NumbersToZero(dt.Day) + "T" + NH.MakeUpTo2NumbersToZero(dt.Hour) + AllStrings.colon + NH.MakeUpTo2NumbersToZero(dt.Minute);
    }

    /// <summary>
    /// Tato metoda bude vždy bezčasová! Proto má v názvu jen Date.
    /// Input v názvu znamená že výstup z této metody budu vkládat do inputů, nikoliv nic se vstupem A1
    /// </summary>
    /// <param name="dt"></param>
    /// <returns></returns>
    public static string DateToStringjQueryDatePicker(DateTime dt)
    {
        //return NH.MakeUpTo2NumbersToZero(dt.Day) + AllStrings.dot + NH.MakeUpTo2NumbersToZero(dt.Month) + AllStrings.dot + dt.Year;
        return NH.MakeUpTo2NumbersToZero(dt.Month) + AllStrings.slash + NH.MakeUpTo2NumbersToZero(dt.Day) + AllStrings.slash + dt.Year;
    }

    public static string DateAndTimeToStringAngularDateTime(DateTime dt)
    {
        return dt.Year + NH.MakeUpTo2NumbersToZero(dt.Month) + NH.MakeUpTo2NumbersToZero(dt.Day) + "T" + NH.MakeUpTo2NumbersToZero(dt.Hour) + AllStrings.colon + NH.MakeUpTo2NumbersToZero(dt.Minute) + AllStrings.colon + NH.MakeUpTo2NumbersToZero(dt.Second);
    }
}