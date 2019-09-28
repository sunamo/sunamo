using System;
using System.Collections.Generic;
using System.Text;

public partial class DTConstants
{
    public const long secondsInMinute = 60;
    public const long secondsInHour = secondsInMinute * 60;
    public const long secondsInDay = secondsInHour * 24;
    public static readonly List<string> daysInWeekENShortcut = CA.ToListString("Mon", "Tue", "Wed", "Thu", "Fri", "Sat", "Sun");
    public static readonly string[] daysInWeekEN = new string[] { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday" };
    public static readonly string[] monthsInYearEN = new string[] { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };

    public static readonly DateTime UnixFsStart = new DateTime(1970, 1, 1);
    public static readonly string[] daysInWeekCS = new string[] { Pondeli, Utery, Streda, Ctvrtek, Patek, Sobota, Nedele };

    #region Dny v týdny CS
    public const string Pondeli = "Pond\u011Bl\u00ED";
    public const string Utery = "\u00DAter\u00FD";
    public const string Streda = "St\u0159eda";
    public const string Ctvrtek = "\u010Ctvrtek";
    public const string Patek = "P\u00E1tek";
    public const string Sobota = "Sobota";
    public const string Nedele = "Ned\u011Ble";
    #endregion

    #region Měsíce v roce CS
    public const string Leden = "Leden";
    public const string Unor = "\u00DAnor";
    public const string Brezen = "B\u0159ezen";
    public const string Duben = "Duben";
    public const string Kveten = "Kv\u011Bten";
    public const string Cerven = "\u010Cerven";
    public const string Cervenec = "\u010Cervenec";
    public const string Srpen = "Srpen";
    public const string Zari = "Z\u00E1\u0159\u00ED";
    public const string Rijen = "\u0158\u00EDjen";
    public const string Listopad = "Listopad";
    public const string Prosinec = "Prosinec";
    #endregion
    public static readonly string[] monthsInYearCZ = new string[] { Leden, Unor, Brezen, Duben, Kveten, Cerven, Cervenec, Srpen, Zari, Rijen, Listopad, Prosinec };
}
