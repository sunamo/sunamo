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
    #region ToString
    #region Date with time (without seconds)
    /// <summary>
    /// 1989-06-21T11:22
    /// </summary>
    /// <param name="dt"></param>
    /// <param name="dtMinVal"></param>
    public static string DateTimeToStringToInputDateTimeLocal(DateTime dt, DateTime dtMinVal)
    {
        if (dt == dtMinVal)
        {
            return "";
        }
        return dt.Year + AllStrings.dash + NH.MakeUpTo2NumbersToZero(dt.Month) + AllStrings.dash + NH.MakeUpTo2NumbersToZero(dt.Day) + "T" + NH.MakeUpTo2NumbersToZero(dt.Hour) + AllStrings.colon + NH.MakeUpTo2NumbersToZero(dt.Minute);
    } 
    #endregion

    #region Only date
    /// <summary>
    /// mm/dd/yyyy
    /// 
    /// Method will be always timeless! Also because of has in name only Date.
    /// Input in name mean that output of this method I will insert only to input, it dont mean anything method argument
    /// </summary>
    /// <param name="dt"></param>
    public static string DateToStringjQueryDatePicker(DateTime dt)
    {
        //return NH.MakeUpTo2NumbersToZero(dt.Day) + AllStrings.dot + NH.MakeUpTo2NumbersToZero(dt.Month) + AllStrings.dot + dt.Year;
        return NH.MakeUpTo2NumbersToZero(dt.Month) + AllStrings.slash + NH.MakeUpTo2NumbersToZero(dt.Day) + AllStrings.slash + dt.Year;
    }
    #endregion


    #region Date with time
    /// <summary>
    /// 19890621T11:22:00
    /// </summary>
    /// <param name="dt"></param>
    public static string DateAndTimeToStringAngularDateTime(DateTime dt)
    {
        return dt.Year + NH.MakeUpTo2NumbersToZero(dt.Month) + NH.MakeUpTo2NumbersToZero(dt.Day) + "T" + NH.MakeUpTo2NumbersToZero(dt.Hour) + AllStrings.colon + NH.MakeUpTo2NumbersToZero(dt.Minute) + AllStrings.colon + NH.MakeUpTo2NumbersToZero(dt.Second);
    }  
    #endregion
    #endregion
}