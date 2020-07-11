using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class LocaleHelper
{
    public static void Init()
    {
        foreach (var item in CultureInfo.GetCultures(CultureTypes.AllCultures))
        {

        }
    }



    #region For easy copy
    public static string GetCountryForLang2(string lang)
    {
        // Easy copy = BCL enum parse
        Langs l = (Langs)Enum.Parse(typeof(Langs), lang); 
        switch (l)
        {
            case Langs.cs:
                return "CZ";
            case Langs.en:
            default:
                return "GB";
        }
    }
    public static Langs? GetLangForCountry2(string country)
    {
        foreach (var item in CountryLang.d)
        {
            if (item.Value == country)
            {
                return item.Key;
            }
        }
        return null;
    }

    public static string GetLangForCountry(string country)
    {
        country = country.ToLower();
        foreach (var item in CultureInfo.GetCultures(CultureTypes.AllCultures))
        {
            var p = SH.Split(item.Name, AllStrings.dash);
            if (p.Count > 1)
            {
                if (p[1] == country)
                {
                    if (p[0].Length == 2)
                    {
                        //ComplexInfoString cis = new ComplexInfoString(p[0]);
                        //if (cis.QuantityLowerChars == 2)
                        //{
                            // Its not good idea because for en return AG
                            return p[0];
                        //}
                    }

                }
            }
        }
        return null;
    } 
    #endregion

    /// <summary>
    /// Its not good idea because for en return AG
    /// Must use GetCountryForLang2
    /// </summary>
    /// <param name="lang"></param>
    /// <returns></returns>
    public static string GetCountryForLang(string lang)
    {
        lang = lang.ToLower();
        foreach (var item in CultureInfo.GetCultures(CultureTypes.AllCultures))
        {
            var p = SH.Split(item.Name, AllStrings.dash);
            if (p.Count > 1)
            {
                if (p[0] == lang)
                {
                    if (p[1].Length == 2)
                    {
                        ComplexInfoString cis = new ComplexInfoString(p[1]);
                        if (cis.QuantityUpperChars == 2)
                        {
                            // Its not good idea because for en return AG
                            return p[1];
                        }
                    }
                    
                }
            }
        }
        return null;
    }
}