
using sunamo.Enums;
using System;
/// <summary>
/// When date parts are delimited by -
/// Next relative methods are in DTHelperFormalizedWithT / DTHelperCode
/// </summary>
public partial class DTHelperFormalized
{
    /// <summary>
    /// Vrátí formalizované datum - tedyu např. 1989-06-21
    /// </summary>
    /// <param name = "dt"></param>
    /// <returns></returns>
    public static string DateTimeToStringFormalizeDate(DateTime dt)
    {
        return dt.Year + "-" + NH.MakeUpTo2NumbersToZero(dt.Month) + "-" + NH.MakeUpTo2NumbersToZero(dt.Day);
    }

    public static string FormatDateTime(DateTime dt, DateTimeFormatStyles fullCalendar)
    {
        if (fullCalendar == DateTimeFormatStyles.FullCalendar)
        {
            //2011-10-18 10:30
            return dt.Year + "-" + NH.MakeUpTo2NumbersToZero(dt.Month) + "-" + NH.MakeUpTo2NumbersToZero(dt.Day) + " " + NH.MakeUpTo2NumbersToZero(dt.Hour) + ":" + NH.MakeUpTo2NumbersToZero(dt.Minute);
        }

        return "";
    }
}