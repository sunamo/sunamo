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
    
    public static string DateAndTimeToStringAngularDateTime(DateTime dt)
    {
        return dt.Year + NH.MakeUpTo2NumbersToZero(dt.Month) + NH.MakeUpTo2NumbersToZero(dt.Day) + "T" + NH.MakeUpTo2NumbersToZero(dt.Hour) + AllStrings.colon + NH.MakeUpTo2NumbersToZero(dt.Minute) + AllStrings.colon + NH.MakeUpTo2NumbersToZero(dt.Second);
    }  
    #endregion
    #endregion
}