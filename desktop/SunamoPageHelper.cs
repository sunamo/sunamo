using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// Must be SunamoPageHelper, not SunamoPageHelperDesktop
/// </summary>
public static partial class SunamoPageHelper
{
    static Type type = typeof(SunamoPageHelper);

    public static string LocalizedString_String(Langs l, string key, string ms)
    {
        var ms2 = EnumHelper.Parse<MySites>(ms, MySites.None);

        if (ms2 == MySites.None)
        {
            ThrowExceptions.Custom(Exc.GetStackTrace(), type, Exc.CallingMethod(), "Not supported for " + ms);
        }

        return LocalizedString(l, key, ms2);
    }

    public static string LocalizedString(Langs l, string key, MySites ms)
    {
        if (SubdomainHelper.translatedWebsites.Contains(ms))
        {
            if (l == Langs.en)
            {
                return RLData.en[key];
            }
            else if (l == Langs.cs)
            {
                if (RLData.cs.ContainsKey(key))
                {
                    return RLData.cs[key];
                }
                return RLData.en[key];
            }
            else
            {
                ThrowExceptions.NotImplementedCase(Exc.GetStackTrace(), type, Exc.CallingMethod(), l);
                return null;
            }
        }
        else
        {
            return RLData.en[key];
        }
    }
}