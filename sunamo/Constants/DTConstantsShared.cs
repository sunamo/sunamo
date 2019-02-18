using System;
using System.Collections.Generic;
using System.Text;

public partial class DTConstants
{
    public const long secondsInMinute = 60;
    public const long secondsInHour = secondsInMinute * 60;
    public const long secondsInDay = secondsInHour * 24;
    public static readonly string[] daysInWeekEN = new string[] { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday" };
    public static readonly string[] monthsInYearEN = new string[] { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };

    public static readonly DateTime UnixFsStart = new DateTime(1970, 1, 1);
    public static readonly string[] daysInWeekCS = new string[] { Pondeli, Utery, Streda, Ctvrtek, Patek, Sobota, Nedele };

    #region Dny v týdny CS
    public const string Pondeli = "Pondělí";
    public const string Utery = "Úterý";
    public const string Streda = "Středa";
    public const string Ctvrtek = "Čtvrtek";
    public const string Patek = "Pátek";
    public const string Sobota = "Sobota";
    public const string Nedele = "Neděle";
    #endregion

    #region Měsíce v roce CS
    public const string Leden = "Leden";
    public const string Unor = "Únor";
    public const string Brezen = "Březen";
    public const string Duben = "Duben";
    public const string Kveten = "Květen";
    public const string Cerven = "Červen";
    public const string Cervenec = "Červenec";
    public const string Srpen = "Srpen";
    public const string Zari = "Září";
    public const string Rijen = "Říjen";
    public const string Listopad = "Listopad";
    public const string Prosinec = "Prosinec";
    #endregion
    public static readonly string[] monthsInYearCZ = new string[] { Leden, Unor, Brezen, Duben, Kveten, Cerven, Cervenec, Srpen, Zari, Rijen, Listopad, Prosinec };
}
