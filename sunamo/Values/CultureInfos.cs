using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class CultureInfos
{
    public static CultureInfo cz = null;

    public static IFormatProvider neutral { get; set; }

    public static void Init()
    {
        if (cz == null)
        {
            
            cz = CultureInfo.GetCultureInfo("cs");
            if (cz == null)
            {
                DebugLogger.Break();
                // use cs-CZ
            }
        }
    }
}