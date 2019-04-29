using System;

public partial class DTHelperFormalized{ 
public static DateTime StringToDateTimeFormalizeDate(string p)
    {
        return DateTime.Parse(p, null, System.Globalization.DateTimeStyles.None);
    }
}