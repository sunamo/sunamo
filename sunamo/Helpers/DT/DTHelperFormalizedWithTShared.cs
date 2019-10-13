using System;

public partial class DTHelperFormalizedWithT{ 
/// <summary>
    /// Vrátí normalizovaný datum a čas, to znamená že bude oddělen T
    /// Čas bude nastaven na 00:00:00
    /// </summary>
    /// <param name = "dt"></param>
    /// <returns></returns>
    public static string DateTimeToStringFormalizeDateEmptyTime(DateTime dt)
    {
        return dt.Year + AllStrings.dash + NH.MakeUpTo2NumbersToZero(dt.Month) + AllStrings.dash + NH.MakeUpTo2NumbersToZero(dt.Day) + "T00:00:00";
    }
}