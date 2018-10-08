using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

public static class UriWebServices
{
    public static class EnglishMobileParts
    {
        /// <summary>
        /// eb
        /// </summary>
        public const string ebay = "https://www.ebay.com/sch/i.html?_nkw=%s";
        /// <summary>
        /// wt
        /// </summary>
        public const string witrigs = "https://www.witrigs.com/searchautocomplete/autoresult?q=%s";
        /// <summary>
        /// ae
        /// </summary>
        public const string aliexpress = "https://www.aliexpress.com/wholesale?SearchText=%s";

        public static readonly string[] All = new string[] { ebay, witrigs, aliexpress };

        public static void SearchInAll(string what)
        {
            UriWebServices.SearchInAll(All, what);
        }
    }

    public static class AdsMsRegion
    {
        public const string bazosCz = "https://www.bazos.cz/search.php?hledat=%s&rubriky=www&hlokalita=70800&humkreis=25&cenaod=&cenado=&Submit=Hledat&kitx=ano";
        public const string hyperinzerceCz = "http://moravskoslezsky-kraj.hyperinzerce.cz/%s/";
        public const string bazarCz = "https://www.bazar.cz/?search=1&ft=%s&p=70800&a=25&pid=6934";
        public const string sBazarCz = "https://www.sbazar.cz/hledej/%s";
        public const string avizoCz = "https://www.avizo.cz/fulltext/?beng=1&searchfor=ads&keywords=%s";
        public const string letGoCz = "https://www.letgo.cz/moravskoslezsky-kraj_g200003339573/q-%s";
        public const string letGoCzPoruba = "https://www.letgo.cz/poruba_g50000007359/q-%s";

        public static readonly string[] All = new string[] { bazosCz, hyperinzerceCz, bazarCz, sBazarCz, avizoCz, letGoCz };

        public static void SearchInAll(string what)
        {
            UriWebServices.SearchInAll(All, what);


        }

        /// <summary>
        /// 70800 v okoli 25km
        /// </summary>
        /// <param name="what"></param>
        /// <returns></returns>
        public static string BazosCz(string what)
        {
            return FromChromeReplacement(bazosCz, what);
        }

        /// <summary>
        /// MS kraj
        /// </summary>
        /// <param name="what"></param>
        /// <returns></returns>
        public static string HyperInzerceCz(string what)
        {
            return FromChromeReplacement(hyperinzerceCz, what);
        }

        /// <summary>
        /// 70800 +25km
        /// </summary>
        /// <param name="what"></param>
        /// <returns></returns>
        public static string BazarCz(string what)
        {
            return FromChromeReplacement(bazarCz, what);
        }

        public static string SBazarCz(string what)
        {
            return FromChromeReplacement(sBazarCz, what);
        }

        public static string AvizoCz(string what)
        {
            return FromChromeReplacement(avizoCz, what);
        }

        public static string LetGoCz(string what)
        {
            return FromChromeReplacement(letGoCz, what);
        }


    }

    public static class AdsWholeCR
    {
        public const string bazosCz = "https://www.bazos.cz/search.php?hledat=%s&rubriky=www&cenaod=&cenado=&Submit=Hledat&kitx=ano";
        public const string hyperinzerceCz = "https://inzeraty.hyperinzerce.cz/%s/";
        public const string bazarCz = "https://www.bazar.cz/?search=1&ft=%s&pid=6934";
        public const string sBazarCz = "https://www.sbazar.cz/hledej/%s";
        public const string avizoCz = "https://www.avizo.cz/fulltext/?beng=1&searchfor=ads&keywords=%s";
        public const string letGoCz = "https://www.letgo.cz/items/q-%s";
        
        public static readonly string[] All = new string[] { bazosCz, hyperinzerceCz, bazarCz, sBazarCz, avizoCz, letGoCz };

        public static void SearchInAll(string what)
        {
            UriWebServices.SearchInAll(All, what);
        }

        /// <summary>
        /// 70800 v okoli 25km
        /// </summary>
        /// <param name="what"></param>
        /// <returns></returns>
        public static string BazosCz(string what)
        {
            return FromChromeReplacement(bazosCz, what);
        }

        /// <summary>
        /// MS kraj
        /// </summary>
        /// <param name="what"></param>
        /// <returns></returns>
        public static string HyperInzerceCz(string what)
        {
            return FromChromeReplacement(hyperinzerceCz, what);
        }

        /// <summary>
        /// 70800 +25km
        /// </summary>
        /// <param name="what"></param>
        /// <returns></returns>
        public static string BazarCz(string what)
        {
            return FromChromeReplacement(bazarCz, what);
        }

        public static string SBazarCz(string what)
        {
            return FromChromeReplacement(sBazarCz, what);
        }

        public static string AvizoCz(string what)
        {
            return FromChromeReplacement(avizoCz, what);
        }

        public static string LetGoCz(string what)
        {
            return FromChromeReplacement(letGoCz, what);
        }


    }

    public const string chromeSearchstringReplacement = "%s";

    public static void SearchInAll(string[] array, string what)
    {
        foreach (var item in array)
        {
            string uri = UriWebServices.FromChromeReplacement(item, what);
            Process.Start(uri);
        }
    }

    public static string FacebookProfile(string nick)
    {
        return "http://www.facebook.com/" + nick;
    }

    public static string TwitterProfile(string nick)
    {
        return "http://www.twitter.com/" + nick;
    }

    public static string YouTubeProfile(string nick)
    {
        return "http://www.youtube.com/c/" + nick;
    }

    public static string GooglePlusProfile(string nick)
    {
        return "http://www.google.com/+" + nick;
    }

    public static string GoogleMaps(string coordsOrAddress, string center, string zoom)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("https://maps.google.com/maps?q=" + coordsOrAddress.Replace(" ", "+") + "&hl=cs&ie=UTF8&t=h");
        if (!string.IsNullOrEmpty(center))
        {
            sb.Append("&ll=" + center);
        }
        if (!string.IsNullOrEmpty(zoom))
        {
            sb.Append( "&z=" + zoom);
        }
        return sb.ToString();
    }

    /// <summary>
    /// A1 už musí být escapováno
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    public static string GoogleSearch(string s)
    {
        return "https://www.google.cz/search?q=" + s;
    }

    public static string GoogleSearchSite(string site, string v)
    {
        //https://www.google.cz/search?q=site%3Asunamo.cz+s
        return "https://www.google.cz/search?q=site%3A" + site + "+" + v;
    }

    public static string KmoAll(string item)
    {
        return FromChromeReplacement("https://tritius.kmo.cz/Katalog/search?q=%s&area=247&field=0", item);
    }

    public static string FromChromeReplacement(string uri, string term)
    {
        return uri.Replace(chromeSearchstringReplacement, term);
    }

    public static string KmoAV(string item)
    {
        return FromChromeReplacement("https://tritius.kmo.cz/Katalog/search?q=%s&area=238&field=0", item);
    }

    public static string KmoMP(string item)
    {
        return FromChromeReplacement("https://tritius.kmo.cz/Katalog/search?q=%s&area=242&field=0", item);
    }

    public static string CoordsInfo(string f)
    {
        return "http://coords.info/" + f;
    }

    public static string GitRepoInVsts(string slnName)
    {
        return "https://sunamocz.visualstudio.com/_git/" + slnName;
    }

    public static string GoogleImFeelingLucky(string v)
    {
        return FromChromeReplacement("http://www.google.com/search?btnI&q=%s", v);
    }
}
