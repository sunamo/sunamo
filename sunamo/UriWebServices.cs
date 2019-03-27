﻿using sunamo.Values;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

public static class UriWebServices
{
    public static class RepairMobile
    {
        public static void SearchInAll(string what)
        {
            //UriWebServices.SearchInAll(RepairMobileValues.allRepairKitShops, what);
        }
    }

    public static class SpiceMarks
    {
        static List<string> list = null;
        
        public static void SearchInAll(string spicyName)
        {
            if (list == null)
            {
                list = new List<string>(CA.ToEnumerable("kotanyi", "avokádo", "nadir", "Orient", "Drago", "vítana", "svět bylinek"));
            }

            foreach (var item in list)
            {
                Process.Start(UriWebServices.GoogleSearch($"{item} koření {spicyName}"));
            }
        }
    }

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

    /// <summary>
    /// For phones, etc. is better repas sites as mp.cz
    /// </summary>
    public static class AdsMsRegion
    {
        /*
Template for which I will find, have to be in derivates the same:

1) bazos.cz
2) hyperinzerce.cz
3) bazar.cz
4) sbazar.cz
5) avizo.cz
6) letgo.cz
7) aukro.cz
         */

        public const string bazosCz = "https://www.bazos.cz/search.php?hledat=%s&rubriky=www&hlokalita=70800&humkreis=25&cenaod=&cenado=&Submit=Hledat&kitx=ano";
        public const string hyperinzerceCz = "http://moravskoslezsky-kraj.hyperinzerce.cz/%s/";
        public const string bazarCz = "https://www.bazar.cz/?search=1&ft=%s&p=70800&a=25&pid=6934";
        public const string sBazarCz = "https://www.sbazar.cz/hledej/%s";
        public const string avizoCz = "https://www.avizo.cz/fulltext/?beng=1&searchfor=ads&keywords=%s";
        public const string letGoCz = "https://www.letgo.cz/moravskoslezsky-kraj_g200003339573/q-%s";
        public const string aukroCz = "https://aukro.cz/vysledky-vyhledavani?text=%s&postCode=708%2000&distance=40";

        public const string letGoCzPoruba = "https://www.letgo.cz/poruba_g50000007359/q-%s";

        public static readonly string[] All = new string[] { bazosCz, hyperinzerceCz, bazarCz, sBazarCz, avizoCz, letGoCz, aukroCz };

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

    /// <summary>
    /// For phones, etc. is better repas sites as mp.cz
    /// </summary>
    public static class AdsWholeCR
    {
        /*
Template for which I will find, have to be in derivates the same:

1) bazos.cz
2) hyperinzerce.cz
3) bazar.cz
4) sbazar.cz
5) avizo.cz
6) letgo.cz
7) aukro.cz
 */

        public const string bazosCz = "https://www.bazos.cz/search.php?hledat=%s&rubriky=www&cenaod=&cenado=&Submit=Hledat&kitx=ano";
        public const string hyperinzerceCz = "https://inzeraty.hyperinzerce.cz/%s/";
        public const string bazarCz = "https://www.bazar.cz/?search=1&ft=%s&pid=6934";
        public const string sBazarCz = "https://www.sbazar.cz/hledej/%s";
        public const string avizoCz = "https://www.avizo.cz/fulltext/?beng=1&searchfor=ads&keywords=%s";
        public const string letGoCz = "https://www.letgo.cz/items/q-%s";
        public const string aukroCz = "https://aukro.cz/vysledky-vyhledavani?text=%s";

        public static readonly string[] All = new string[] { bazosCz, hyperinzerceCz, bazarCz, sBazarCz, avizoCz, letGoCz, aukroCz };

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

    public static class SolarShops
    {
        static List<string> shops = new List<string>(CA.ToEnumerable("mulac.cz", "solar-eshop.cz", "karavan3nec.cz", "campi-shop.cz", "ges.cz", "dstechnik.cz", "emerx.cz", "vpcentrum.eu", "dexhal.cz"));

        public const string mulacCz = @"https://www.mulac.cz/hledani/?q=%s";
        public const string solarEshop = @"https://www.solar-eshop.cz/vyhledavani/?w=%s&submit=";
        
        public const string karavan3nec = @"http://www.karavan3nec.cz/?page=search&sortmode=7&search=%s";
        public const string campiShopCz = @"https://www.campi-shop.cz/obchod/vyhledavani/_q=%s";
        public const string gesCz = @"https://www.ges.cz/cz/hledat/?search=%s";
        public const string dstechnikCz = @"https://www.dstechnik.cz/vyhledavani/?qkk=333af8f0cfef3cbbe82db1e238b1ba2d&hledej=%s&x=0&y=0";
        public const string emerxCz = @"https://www.emerx.cz/hledani?s=%s&submit_=HLEDAT&do=searchForm-submit";
        // not search term in uri
        //public const string vpCentrumCz = @"https://www.vpcentrum.eu/index.php?route=product/search&filter_name=Hledat";
        // not search term in uri
        //public const string dexhalCz = @"https://dexhal.cz/search?controller=search&orderby=position&orderway=desc&search_query=s&submit_search=Hledat";

        public static readonly List<string> All = CA.ToListString(mulacCz, solarEshop, karavan3nec, campiShopCz, gesCz, dstechnikCz, emerxCz);

    }

    public static void GoogleSearch(List<string> list)
    {
        foreach (var item in list)
        {
            Process.Start( GoogleSearch(item));
        }
    }

    public static void GoogleMaps(List<string> list)
    {
        foreach (var item in list)
        {
            Process.Start(GoogleMaps(item));
        }
    }

    public static string SpritMonitor(string car)
    {
        // https://www.spritmonitor.de/en/overview/45-Skoda/1289-Citigo.html?fueltype=4
        string d = "cng overview -\"/detail/\" " + car;
        return GoogleSearchSite("spritmonitor.de", d);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    private static string GoogleMaps(string item)
    {
        return FromChromeReplacement( "https://www.google.com/maps/place/%s", item);
    }

    public static class Libraries
    {
        /// <summary>
        /// Narodni knihovna
        /// </summary>
        public const string nkp = @"https://aleph.nkp.cz/F/K1AF26NFNIRG8S216J2Q7YBV19F2F8LF11VEA4AY4I2L2Y42M3-55374?func=find-b&find_code=WRD&x=0&y=0&request=%s&filter_code_1=WTP&filter_request_1=&filter_code_2=WLN&adjacent=N";
        public const string vsb = "https://katalog.vsb.cz/search?type=global&q=s";
        /// <summary>
        /// Knihovna akademie ved
        /// </summary>
        public const string cas = @"https://vufind.lib.cas.cz/ustav/KNAV/Search/Results?type=AllFields&institution=KNAV&filter%5B%5D=institution%3AKNAV&lookfor=nginx&rQhtuXCSid=04u.IQRKfg&swLoQZTxFJEVbrgB=_oD3lR7wWZ6Sx0yt&umXNFi=c5lOmp&rQhtuXCSid=04u.IQRKfg&swLoQZTxFJEVbrgB=_oD3lR7wWZ6Sx0yt&umXNFi=c5lOmp";
        public const string mlp = "http://search.mlp.cz/en/?query=%s&kde=all#/c_s_ol=query-eq:%s";
        public const string kmoAll = "https://tritius.kmo.cz/Katalog/search?q=%s&area=247&field=0";
        public const string kmoAV = "https://tritius.kmo.cz/Katalog/search?q=%s&area=238&field=0";
        public const string kmoMP = "https://tritius.kmo.cz/Katalog/search?q=%s&area=242&field=0";
        public const string svkos = "https://katalog.svkos.cz/F/JSAUCF45R2HDYLIMN5CFCRTY5LIRAYKG33QJR7IT42N8G4X53M-60701?func=find-b&request=%s&x=0&y=0&find_code=WRD&adjacent=N&local_base=KATALOG&filter_code_4=WFM&filter_request_4=&filter_code_1=WLN&filter_request_1=&filter_code_2=WYR&filter_request_2=&filter_code_3=WYR&filter_request_3=";
        public const string dk = "https://www.databazeknih.cz/search?q=%s&hledat=";
    }

    /// <summary>
    /// Is possible user also OpenInBrowser.OpenCachesFromCacheList
    /// </summary>
    public class GeoCachingComSite
    {
        public static string CacheDetails(string cacheGuid)
        {
            return "http://www.geocaching.com/seek/cache_details.aspx?guid=" + cacheGuid;
        }

        public static string Gallery(string cacheGuid)
        {
            return "http://www.geocaching.com/seek/gallery.aspx?guid=" + cacheGuid;
        }

        public static string Log(string cacheGuid)
        {
            return "http://www.geocaching.com/seek/log.aspx?guid=" + cacheGuid;
        }

        public static string CoordsInfo(string f)
        {
            return "http://coords.info/" + f;
        }

        public static string GC(string f)
        {
            return "http://coords.info/GC" + f;
        }
    }

    public static string KmoAll(string item)
    {
        return FromChromeReplacement("", item);
    }

    public static string KmoAV(string item)
    {
        return FromChromeReplacement("", item);
    }

    public static string KmoMP(string item)
    {
        return FromChromeReplacement("", item);
    }

    public const string chromeSearchstringReplacement = "%s";

    public static void SearchInAll(IEnumerable array, string what)
    {
        foreach (var item in array)
        {
            string uri = UriWebServices.FromChromeReplacement(item.ToString(), what);
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

    public static string SearchGitHub(string item)
    {
        return "https://github.com/search?q=" + item;
    }

    public static string WebShare(string item)
    {
        return "https://webshare.cz/#/search?what=" + item;
    }

    public static string YouTubeProfile(string nick)
    {
        return "http://www.youtube.com/c/" + nick;
    }

    public static string GooglePlusProfile(string nick)
    {
        return "http://www.google.com/+" + nick;
    }

    public static void GoogleSearchInAllSite(List<string> allRepairKitShops, string v)
    {
        foreach (var item in allRepairKitShops)
        {
            Process.Start( GoogleSearchSite(item, v));
        }
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
        // q for reviews in czech and not translated 
        return "https://www.google.cz/search?hl=cs&q=" + s;
    }

    public static string GoogleSearchSite(string site, string v)
    {
        //https://www.google.cz/search?q=site%3Asunamo.cz+s
        return  "https://www.google.cz/search?q=site%3A" + site + "+" + v;
    }

    public static string FromChromeReplacement(string uri, string term)
    {
        return uri.Replace(chromeSearchstringReplacement, HttpUtility.UrlEncode( term));
    }

    

    /// <summary>
    /// Already new radekjancik
    /// Working with spaces right (SQL Server Scripts1)
    /// </summary>
    /// <param name="slnName"></param>
    /// <returns></returns>
    public static string GitRepoInVsts(string slnName)
    {
        return "https://radekjancik.visualstudio.com/_git/" + HttpUtility.UrlEncode( slnName);
    }

    public static string GoogleImFeelingLucky(string v)
    {
        return FromChromeReplacement("http://www.google.com/search?btnI&q=%s", v);
    }

    public static string MapyCz(string v)
    {
        return FromChromeReplacement( "https://mapy.cz/?q=%s&sourceid=Searchmodule_1", v);
    }

    /// <summary>
    /// Summary description for YouTube
    /// </summary>
    public static class YouTube
    {
        public static string GetLinkToVideo(string kod)
        {
            return "http://www.youtube.com/watch?v=" + kod;
        }

        public static string GetHtmlAnchor(string kod)
        {
            return "<a href='" + GetLinkToVideo(kod) + "'>" + kod + "</a>";
        }

        public static string ReplaceOperators(string vstup)
        {
            return SH.ReplaceAll(vstup, "", "OR", "+", "-", "\"", "*");
        }

        public static string GetLinkToSearch(string co)
        {
            return "http://www.youtube.com/results?search_query=" + UH.UrlEncode(co);
        }

        /// <summary>
        /// G null pokud se YT kód nepodaří získat
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        public static string ParseYtCode(string uri)
        {
            Regex regex = new Regex("youtu(?:\\.be|be\\.com)/(?:.*v(?:/|=)|(?:.*/)?)([a-zA-Z0-9-_]+)");
            var match = regex.Match(uri);
            if (match.Success)
            {
                return match.Groups[1].Value;
            }
            return null;
        }
    }
}
