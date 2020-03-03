using System;
public partial class DTHelperCs
{
    static Type type = typeof(DTHelperCs);

    #region ToString
    #region Only date
    /// <summary>
    /// Středa, 21.6.1989
    /// </summary>
    /// <param name="dt"></param>
    
    public static string DayOfWeek2DenVTydnu(DayOfWeek dayOfWeek)
    {
        switch (dayOfWeek)
        {
            case DayOfWeek.Monday:
                return DTConstants.Pondeli;
            case DayOfWeek.Tuesday:
                return DTConstants.Utery;
            case DayOfWeek.Wednesday:
                return DTConstants.Streda;
            case DayOfWeek.Thursday:
                return DTConstants.Ctvrtek;
            case DayOfWeek.Friday:
                return DTConstants.Patek;
            case DayOfWeek.Saturday:
                return DTConstants.Sobota;
            case DayOfWeek.Sunday:
                return DTConstants.Nedele;
        }
        throw new Exception("Nezn\u00E1m\u00FD den v t\u00FDdnu");
    }
    #endregion
}