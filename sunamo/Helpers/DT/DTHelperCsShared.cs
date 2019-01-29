using System;
using System.Collections.Generic;
using System.Text;

namespace sunamo.Helpers.DT
{
    public partial class DTHelperCs
    {
        /// <summary>
        /// Tato metoda bude vždy bezčasová! Proto má v názvu jen Date.
        /// 
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string DateToStringWithDayOfWeekCS(DateTime dt)
        {
            return DayOfWeek2DenVTydnu(dt.DayOfWeek) + ", " + dt.Day + "." + dt.Month + "." + dt.Year;
        }

        #region CZ Other
        /// <summary>
        /// Vrátí český název dne v týdnu podle A1
        /// </summary>
        /// <param name="dayOfWeek"></param>
        /// <returns></returns>
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
            throw new Exception("Neznámý den v týdnu");
        }


        #endregion
    }
}
