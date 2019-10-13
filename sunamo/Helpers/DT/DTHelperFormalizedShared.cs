using sunamo.Enums;
using System;

public partial class DTHelperFormalized
{

public static string FormatDateTime(DateTime dt, DateTimeFormatStyles fullCalendar)
    {
        if (fullCalendar == DateTimeFormatStyles.FullCalendar)
        {
            //2011-10-18 10:30
            return dt.Year + AllStrings.dash + NH.MakeUpTo2NumbersToZero(dt.Month) + AllStrings.dash + NH.MakeUpTo2NumbersToZero(dt.Day) + AllStrings.space + NH.MakeUpTo2NumbersToZero(dt.Hour) + AllStrings.colon + NH.MakeUpTo2NumbersToZero(dt.Minute);
        }

        return "";
    }

/// <summary>
    /// Vrátí formalizované datum - tedyu např. 1989-06-21
    /// </summary>
    /// <param name = "dt"></param>
    /// <returns></returns>
    public static string DateTimeToStringFormalizeDate(DateTime dt)
    {
        return dt.Year + AllStrings.dash + NH.MakeUpTo2NumbersToZero(dt.Month) + AllStrings.dash + NH.MakeUpTo2NumbersToZero(dt.Day);
    }
}