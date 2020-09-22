using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class SunamoPageHelperSunamo
{
    
    public static Func<Langs, string, string, string> localizedString;

    /// <summary>
    /// Return always en
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public static string i18n(string key)
    {
        return i18n(Langs.en, key, Consts.Nope);
    }

    public static string i18n(Langs l, string key, string ms)
    {
        if (localizedString == null)
        {
            // For small apps where is loading xlf overkill
            return key;
        }

        return localizedString(l, key, ms);
    }
}