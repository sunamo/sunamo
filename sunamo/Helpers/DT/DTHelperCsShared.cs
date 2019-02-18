using System;
using System.Collections.Generic;
using System.Text;


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


    

public static string ToShortTime(DateTime value)
        {
            return value.Hour + ":" + DTHelperGeneral.MakeUpTo2NumbersToZero( value.Minute);
        }
        
/// <summary>
        /// If fail, return DT.MinValue
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public static DateTime ParseTimeCzech(string t)
        {
            var vr = DateTime.MinValue;
            var parts = SH.Split(t, ':');
            if (parts.Count == 2)
            {
                t += ":00";
                parts = SH.Split(t, ':');
            }
            int hours = -1;
            int minutes = -1;
            int seconds = -1;
            if (parts.Count == 3)
            {
                TryParse.Integer itp = new TryParse.Integer();
                if (itp.TryParseInt(parts[0]))
                {
                    hours = itp.lastInt;
                    if (itp.TryParseInt(parts[1]))
                    {
                        minutes = itp.lastInt;
                        if (itp.TryParseInt(parts[2]))
                        {
                            seconds = itp.lastInt;
                            vr = DateTime.Today;
                            vr = vr.AddHours(hours);
                            vr = vr.AddMinutes(minutes);
                            vr = vr.AddSeconds(seconds);
                        }
                    }
                }
            }
            return vr;
        }

public static DateTime ParseDateCzech(string input)
        {
            DateTime vr = DateTime.MinValue;
            var parts = SH.Split(input, '.');
            var day = -1;
            var month = -1;
            var year = -1;

            TryParse.Integer tpi = new TryParse.Integer();
            if (tpi.TryParseInt(parts[0]))
            {
                day = tpi.lastInt;
                if (tpi.TryParseInt(parts[1]))
                {
                    month = tpi.lastInt;
                    if (tpi.TryParseInt(parts[2]))
                    {
                        year = tpi.lastInt;
                        try
                        {
                            vr = new DateTime(year, month, day, 0, 0, 0);
                        }
                        catch (Exception)
                        {
                        }
                    }
                }
            }
            return vr;
        }
}