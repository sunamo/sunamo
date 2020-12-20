﻿
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

/// <summary>
/// vše co je s adresou musím encodovat
/// </summary>
public partial class UriWebServices
{
    static int opened = 0;

    public static class ChromeSearchShortcut
    {
        public const string gp = "https://play.google.com/store/search?q=%s";
    }

    public static class BatteryEshops
    {
        #region Specialize on batteries
        public static readonly string wwwBatteryshopCz = "www.batteryshop.cz";
        public static readonly string wwwAvacomCz = "www.avacom.cz";
        public static readonly string wwwAkuShopCz = "www.aku-shop.cz";
        public static readonly string wwwPowerguyCz = "www.powerguy.cz";
        #endregion


        #region Have category for it
        public static readonly string wwwCeskyMobilCz = "www.cesky-mobil.cz";
        public static readonly string wwwDatartCz = "www.datart.cz";
        public static readonly string wwwSmartyCz = "www.smarty.cz";
        public static readonly string wwwMobilprislusenstviCz = "www.mobilprislusenstvi.cz";
        public static readonly string wwwHuramobilCz = "www.huramobil.cz"; 
        #endregion

    }

    public static class FurnitureInOvaWithBestRating
    {
        //public const string wwwNabytekmodenCz = "https://www.nabytekmoden.cz/";
        //public const string wwwJitonaCz = "http://www.jitona.cz/";
        public const string wwwIntenaCz = "https://www.intena.cz/vyhledavani?search_query=%s&submit_search=&orderby=price&orderway=asc";

        //public const string wwwJechCz = "https://www.jech.cz/hledat?query=%s";
        public const string wwwIkeaCom = "https://www.ikea.com/cz/cs/search/products/?q=%s";
    }
    public static class FurnitureInOva
        {
            public const string wwwOrfaNabytekCz = "https://www.orfa-nabytek.cz/produkty/hledani?sor=pra&pfr=&pto=&send=Zobrazit&m=&q=%s&do=formProductsFilter-submit";
        public const string wwwOkayCz = "https://www.okay.cz/hledani/?query=%s";
        
        public const string wwwScontoCz = "https://www.sconto.cz/hledani?q=%s";
        public const string jyskCz = "https://jysk.cz/search?query=%s&search_category=typed_query&op=Hledat#meta=solr&start=0&sort=fts_field_minsingleprice%2Basc";
        
        public const string wwwMoebelixCz = "https://www.moebelix.cz/s/?s=%s";
        //public const string wwwIdeaNabytekCz = "https://www.idea-nabytek.cz/ulozne-prostory/%se/?ordertype=asc&Ordering=ProductPriceWithVat";
    }

    public static class RepairMobile
    {
        public static void SearchInAll(string what)
        {
            opened++;
            //UriWebServices.SearchInAll(RepairMobileValues.allRepairKitShops, what);
            if (opened % 10 == 0)
            {
                Debugger.Break();
            }
        }
    }

    public static class TechSitesRss
    {
        public static Type type = typeof(TechSitesRss);

        public const string feedsFeedburnerCom = "http://feeds.feedburner.com/TechCrunch/";
        public const string wwwEngadgetCom = "http://www.engadget.com/rss.xml";
        
        /// <summary>
        /// Unknown rss feed
        /// </summary>
        public const string wwwThevergeCom = "http://www.theverge.com/rss/index.xml";

        public const string wwwSciencedailyCom = "https://www.sciencedaily.com/rss/all.xml";
        public const string wwwTechradarCom = "https://www.techradar.com/rss";
        public const string wwwWiredCom = "https://www.wired.com/feed/rss";
        public const string feedsArstechnicaCom = "http://feeds.arstechnica.com/arstechnica/index";
        public const string thenextwebCom = "https://thenextweb.com/feed/";
        public const string wwwTomshardwareCom = "https://www.tomshardware.com/feeds/all";

        public static List<string> haveImages = CA.ToList<string>("thenextwebCom", "wwwEngadgetCom");
    }

    public static class Business
    {
        public const string wwwFirmoCz = "www.firmo.cz";
        public const string rejstrikPenizeCz = "rejstrik.penize.cz";
        public const string wwwFirmyCz = "www.firmy.cz";
        public const string rejstrikFiremKurzyCz = "rejstrik-firem.kurzy.cz";
        public const string wwwPodnikatelCz = "www.podnikatel.cz";
        public const string rejstrikyFinanceCz = "rejstriky.finance.cz";


        public static List<string> All = CA.ToListString(wwwFirmoCz, rejstrikPenizeCz, wwwFirmyCz, rejstrikFiremKurzyCz, wwwPodnikatelCz, rejstrikyFinanceCz);
    }

    public static class MyBlogs
    {
        public const string jpnAdminAllPosts = @"https://jepsano.net/wp-admin/edit.php?s=%s&post_status=all&post_type=post&action=-1&m=0&cat=0&seo_filter&readability_filter&paged=1&action2=-1";
    }

    public const string karaokeTexty = "http://www.karaoketexty.cz/search?q=%s&sid=bbrpp&x=36&y=9";
    public static class AutomotiveSpareParts
    {
        public const string wwwAutokseftCz = "https://www.autokseft.cz/index.php?main_page=shop_search&keyword=%s";
        public const string wwwAutodocCz = "https://www.autodoc.cz/search?keyword=%" ;
        public const string wwwNahradniDilyZhCz = "https://www.nahradni-dily-zh.cz/search.asp?searchinput=%" ;
        public const string wwwAutomobilovedilyCz = "https://www.automobilovedily24.cz/search?keyword=%" ;
        public static List<string> All = CA.ToListString(wwwAutokseftCz, wwwAutodocCz, wwwNahradniDilyZhCz, wwwAutomobilovedilyCz);
    }

    public static class UriShareService
    {
        public static List<string> domains = null;

        static UriShareService()
        {
            domains = CA.ToListString("mega.co", "uploading.com", "zippyshare.com", "box.com", "rapidshare.com", "dfiles.eu", "4shared.com", "mediafire.com", "dropbox.com", "bayfiles.com", "divxstage.eu", "hulkshare.com", "megashares", "files.fm", "wetransfer.com", "filehosting.org", "yourfilelink.com");
        }
    }

    public static class CdnProviders
    {
        //
        public const string cdnjs = "https://api.cdnjs.com/libraries?search=%" ;
        /// <summary>
        /// Search for everything on npm
        /// </summary>
        public const string unpkg = "https://www.npmjs.com/search?q=%" ;
        //public const string cdnjs = "";
        //public const string cdnjs = "";
        //public const string cdnjs = "";
        //public const string cdnjs = "";
        //public const string cdnjs = "";

        public static readonly List<string> All = CA.ToListString(cdnjs, unpkg);
    }

    public static class RemoteJobs
    {
        public const string WwwFlexjobsCom = "https://www.flexjobs.com/search?search=%s&location=";
        public const string AngelCo = "https://angel.co/jobs#find/f!%7B%22remote%22%3Atrue%2C%22keywords%22%3A%5B%22%s%22%5D%7D";
        public const string TalentHubstaffCom = "https://talent.hubstaff.com/search/jobs?search%5Bkeywords%5D=%s&page=1&search%5Btype%5D=&search%5Blast_slider%5D=&search%5Bnewer_than%5D=&search%5Bnewer_than%5D=&search%5Bpayrate_start%5D=1&search%5Bpayrate_end%5D=100%2B&search%5Bpayrate_null%5D=0&search%5Bpayrate_null%5D=1&search%5Bbudget_start%5D=1&search%5Bbudget_end%5D=100000%2B&search%5Bbudget_null%5D=0&search%5Bbudget_null%5D=1&search%5Bexperience_level%5D=-1&search%5Bcountries%5D%5B%5D=&search%5Blanguages%5D%5B%5D=&search%5Bsort_by%5D=relevance";
        // not fulltext, always search only for exact position https://pangian.com/job-travel-remote/
        //public const string PangianCom = "";
        public const string RemoteCom = "https://remote.com/jobs/browse?keyword=%" ;
        // https://remote.co/search-results/?cx=009859377982936732048%3Awihm_nznrgm
        public const string RemoteCo = "https://remote.co/remote-jobs/search/?search_keywords=%" ;
        public const string WeworkremotelyCom = "https://weworkremotely.com/remote-jobs/search?utf8=%E2%9C%93&term=%s";
        public const string JobspressoCo = "https://jobspresso.co/remote-work/#%s=1";
        //https://remoteok.io/remote-virtual-assistant-jobs
        public const string RemoteokIo = "https://remoteok.io/";
        //https://www.workingnomads.co/jobs
        public const string WwwWorkingnomadsCo = "https://www.workingnomads.co";

        public const string StackoverflowCom = "https://stackoverflow.com/jobs?q=%" ;

        public static List<string> All = CA.ToListString(WwwFlexjobsCom, AngelCo, TalentHubstaffCom, RemoteCo, WeworkremotelyCom, JobspressoCo, StackoverflowCom);
    }

    public static void SearchAll(string lyricsScz, List<string> clipboardL)
    {
        foreach (var item in clipboardL)
        {
            opened++;
            PH.Start(FromChromeReplacement(lyricsScz, item));

            if (opened % 10 == 0)
            {
                Debugger.Break();
            }
        }
    }

    public static void SearchAll(Func<string, string> topRecepty, List<string> clipboardL)
    {

        foreach (var item in clipboardL)
        {
            opened++;
            PH.Start(topRecepty.Invoke(item));
            if (opened % 10 == 0)
            {
                Debugger.Break();
            }
        }
    }

    public static class Lyrics
    {
        #region Space for %20, uri encoded
        public const string wwwMusixmatchCom = "https://www.musixmatch.com/search/%s";
        public const string geniusCom = "https://genius.com/search?q=%s";
        public const string wwwLyricsCom = "https://www.lyrics.com/lyrics/%s";
        #endregion

        #region Space for plus
        public const string wwwMetrolyricsCom = "https://www.metrolyrics.com/search.html?search=%s";
        public const string azlyricsCom = "https://search.azlyrics.com/search.php?q=%s"; 
        #endregion

        public static List<string> All  = CA.ToListString(wwwMusixmatchCom, geniusCom, wwwMetrolyricsCom, wwwLyricsCom, azlyricsCom);
    }

    /// <summary>
    /// Localhost due to easy convert with 
    /// </summary>
    public static class SunamoCz
    {
        public const string lyricsScz = "https://lyr.localhost/search/%s";
        public const string appsHelp = "https://app.localhost/help/%s";
        public const string appsFeedBack = "https://app.localhost/feedback/%s";
        public const string appsApp = "https://app.localhost/app/%s";
    }

    public static class SexShops
    {
        public const string wwwRuzovyslonCz = "https://www.ruzovyslon.cz/hledani?_submit=Hledat&s=%s&do=searchForm-submit";
        public const string wwwEroticcityCz = "https://www.eroticcity.cz/vyhledavani.html?q=%" ;
        public const string wwwSexshopikCz = "https://www.sexshopik.cz/vyhledavani/?search%5Bquery%5D=%s";
        public const string wwwSexShopCz = "https://www.sex-shop69.cz/search/search?st_search%5Bsearch%5D=%s&st_search%5Band_search%5D=1&st_search%5Bdetail%5D=";
        public const string intimmShopCz = "https://intimm-shop.cz/vyhledavani?controller=search&orderby=position&orderway=desc&search_query=%s&submit_search=";
        public const string wwwEroticstoreCz = "https://www.eroticstore.cz/vysledky-hledani/?search=%" ;
        public const string wwwNejlevnejsierotickepomuckyCz = "https://www.nejlevnejsierotickepomucky.cz/vyhledavani/?string=%" ;
        public const string wwwWillistoreCz = "https://www.willistore.cz/?controller=search&orderby=position&orderway=desc&q=%s&submit_search=";
        public const string wwwVibratoryOnlineCz = "https://www.vibratory-online.cz/hledat/?search=%s&searchButton.x=0&searchButton.y=0";
        public const string wwwLuxusnipradloCz = "https://www.luxusnipradlo.cz/hledani/?q=%" ;
        public const string eKondomyCz = "https://e-kondomy.cz/catalogsearch/result/?q=%" ;

        public static List<string> All = CA.ToListString(wwwRuzovyslonCz, wwwEroticcityCz, wwwSexshopikCz, wwwSexShopCz, intimmShopCz, wwwEroticstoreCz, wwwNejlevnejsierotickepomuckyCz, wwwWillistoreCz, wwwVibratoryOnlineCz, wwwLuxusnipradloCz, eKondomyCz);
    }
    public static class SpiceMarks
    {
        private static List<string> s_list = null;

        public static void SearchInAll(string spicyName)
        {
            if (s_list == null)
            {
                s_list = new List<string>(CA.ToEnumerable("kotanyi", "avok\u00E1do", "nadir", XlfKeys.Orient, XlfKeys.Drago, "v\u00EDtana", "sv\u011Bt bylinek"));
            }

            foreach (var item in s_list)
            {
                Process.Start(UriWebServices.GoogleSearch($"{item} koření {spicyName}"));
            }
        }
    }
    public static class CashBack
    {
        public const string vratnepenize = "https://www.vratnepenize.cz/zbozi/hledej?g=%s";
        public const string tipli = "https://www.tipli.cz/hledat/%s";
        public const string plnapenezenka = "https://www.plnapenezenka.cz/hledej/%s";

        public static readonly List<string> All = CA.ToListString(vratnepenize, tipli, plnapenezenka);
    }

    public static class HorticultureWholeCzech
    {
        public const string wwwZahradnictviFlosCz = "https://www.zahradnictvi-flos.cz/vyhledavani/%s?productFilter-s%5B13%5D=%s";
        public const string eshopStarklCom = "https://eshop.starkl.com/search/?q=%s";
        public const string wwwHornbachCz = "https://www.hornbach.cz/shop/vyhledavani/sortiment/%s";
        public const string wwwObiCz = "https://www.obi.cz/search/%s/";

        public static readonly List<string> All = CA.ToListString(wwwZahradnictviFlosCz, eshopStarklCom, wwwHornbachCz, wwwObiCz);
    }

    public static class HorticultureHavirovAndSurroundings
    {
        public const string wwwZahradnictviporubaCz = "https://www.zahradnictviporuba.cz/";
        public const string wwwKornerCz = "https://www.korner.cz";
        public const string wwwHavlinaCz = "https://www.havlina.cz/";
        public const string wwwZahradnictviSimkovaCz = "https://www.zahradnictvi-simkova.cz";
        public const string zahradnictviDetmaroviceWebnodeCz = "https://zahradnictvi-detmarovice.webnode.cz/";
        public const string wwwFrutoCz = "https://www.fruto.cz/";
        public const string wwwZahradnictvikrhutCz = "https://www.zahradnictvikrhut.cz";
        public const string wwwZupazCz = "https://www.zupaz.cz/";
        public const string wwwVahamoCz = "https://www.vahamo.cz";
        public const string eshopPasicCz = "https://eshop.pasic.cz";

        public static List<string> All()
        {
            var fi = RH.GetConsts(typeof(HorticultureHavirovAndSurroundings));
            var l = fi.Select(d => d.GetValue(null).ToString()).ToList();
            return l;
        }
    }

    public static class EnglishMobileParts
    {
        /// <summary>
        /// eb
        /// </summary>
        public const string ebay = "https://www.ebay.com/sch/i.html?_nkw=%" ;
        /// <summary>
        /// wt
        /// </summary>
        public const string witrigs = "https://www.witrigs.com/searchautocomplete/autoresult?q=%" ;
        /// <summary>
        /// ae
        /// </summary>
        public const string aliexpress = "https://www.aliexpress.com/wholesale?SearchText=%s";


        public static readonly List<string> All = new List<string> { ebay, witrigs, aliexpress };

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
        public const string hyperinzerceCz = "https://moravskoslezsky-kraj.hyperinzerce.cz/%s/";
        public const string bazarCz = "https://www.bazar.cz/ostrava/hledat/%s/?a=25&p=70800&pid=6934";
        public const string sBazarCz = "https://www.sbazar.cz/hledej/%s/0-vsechny-kategorie/moravskoslezsky";
        public const string avizoCz = "https://www.avizo.cz/fulltext/?beng=1&searchfor=ads&keywords=%s";
        //public const string letGoCz = "https://www.letgo.cz/moravskoslezsky-kraj_g200003339573/q-%s" ;
        public const string aukroCz = "https://aukro.cz/vysledky-vyhledavani?text=%s&postCode=708%2000&distance=40";
        public const string letGoCzPoruba = "https://www.letgo.cz/poruba_g50000007359/q-%" ;

        public static readonly List<string> All = new List<string> {bazosCz, hyperinzerceCz,
 bazarCz, sBazarCz, avizoCz, aukroCz };

        //Letadlová postel

        public static void SearchInAll(string what)
        {
            UriWebServices.SearchInAll(All, what);
        }

        /// <summary>
        /// 70800 v okoli 25km
        /// </summary>
        /// <param name="what"></param>
        public static string BazosCz(string what)
        {
            return FromChromeReplacement(bazosCz, what);
        }

        /// <summary>
        /// MS kraj
        /// </summary>
        /// <param name="what"></param>
        public static string HyperInzerceCz(string what)
        {
            return FromChromeReplacement(hyperinzerceCz, what);
        }

        /// <summary>
        /// 70800 +25km
        /// </summary>
        /// <param name="what"></param>
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

        //public static string LetGoCz(string what)
        //{
        //    return FromChromeReplacement(letGoCz, what);
        //}
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
        public const string bazarCz = "https://www.bazar.cz/hledat/%s/";
        public const string sBazarCz = "https://www.sbazar.cz/hledej/%s" ;
        public const string avizoCz = "https://www.avizo.cz/fulltext/?beng=1&searchfor=ads&keywords=%s";
        //public const string letGoCz = "https://www.letgo.cz/items/q-%s";
        public const string aukroCz = "https://aukro.cz/vysledky-vyhledavani?text=%s" ;

        public static readonly List<string> All = new List<string> { bazosCz, hyperinzerceCz, bazarCz, sBazarCz, avizoCz,  aukroCz };

        public static void SearchInAll(string what)
        {
            UriWebServices.SearchInAll(All, what);
        }

        /// <summary>
        /// 70800 v okoli 25km
        /// </summary>
        /// <param name="what"></param>
        public static string BazosCz(string what)
        {
            return FromChromeReplacement(bazosCz, what);
        }

        /// <summary>
        /// MS kraj
        /// </summary>
        /// <param name="what"></param>
        public static string HyperInzerceCz(string what)
        {
            return FromChromeReplacement(hyperinzerceCz, what);
        }

        /// <summary>
        /// 70800 +25km
        /// </summary>
        /// <param name="what"></param>
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

        //public static string LetGoCz(string what)
        //{
        //    return FromChromeReplacement(letGoCz, what);
        //}
    }

    public static class SolarShops
    {
        private static List<string> s_shops = new List<string>(CA.ToEnumerable("mulac.cz", "solar-eshop.cz", "karavan3nec.cz", "campi-shop.cz", "ges.cz", "dstechnik.cz", "emerx.cz", "vpcentrum.eu", "dexhal.cz"));

        public const string mulacCz = @"https://www.mulac.cz/hledani/?q=%" ;
        public const string solarEshop = @"https://www.solar-eshop.cz/vyhledavani/?w=%s&submit=";

        public const string karavan3nec = @"https://www.karavan3nec.cz/?page=search&sortmode=7&search=%s";
        public const string campiShopCz = @"https://www.campi-shop.cz/obchod/vyhledavani/_q=%" ;
        public const string gesCz = @"https://www.ges.cz/cz/hledat/?search=%" ;
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
            Process.Start(GoogleSearch(item));
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
        string d = "cng overview -\"/detail/\"" + car;
        return GoogleSearchSite("spritmonitor.de", d);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="item"></param>
    private static string GoogleMaps(string item)
    {
        return FromChromeReplacement("https://www.google.com/maps/place/%" , item);
    }

    public static class Libraries
    {
        /// <summary>
        /// Narodni knihovna
        /// </summary>
        public const string nkp = @"https://aleph.nkp.cz/F/K1AF26NFNIRG8S216J2Q7YBV19F2F8LF11VEA4AY4I2L2Y42M3-55374?func=find-b&find_code=WRD&x=0&y=0&request=%s&filter_code_1=WTP&filter_request_1=&filter_code_2=WLN&adjacent=N";
        public const string vsb = "https://katalog.vsb.cz/search?type=global&q=%s";
        /// <summary>
        /// Knihovna akademie ved
        /// </summary>
        public const string cas = @"https://vufind.lib.cas.cz/ustav/KNAV/Search/Results?type=AllFields&institution=KNAV&filter%5B%5D=institution%3AKNAV&lookfor=%s&rQhtuXCSid=04u.IQRKfg&swLoQZTxFJEVbrgB=_oD3lR7wWZ6Sx0yt&umXNFi=c5lOmp&rQhtuXCSid=04u.IQRKfg&swLoQZTxFJEVbrgB=_oD3lR7wWZ6Sx0yt&umXNFi=c5lOmp";
        public const string mlp = "https://search.mlp.cz/en/?query=%s&kde=all#/c_s_ol=query-eq:%s";
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
            return "https://www.geocaching.com/seek/cache_details.aspx?guid=" + cacheGuid;
        }

        public static string Gallery(string cacheGuid)
        {
            return "https://www.geocaching.com/seek/gallery.aspx?guid=" + cacheGuid;
        }

        public static string Log(string cacheGuid)
        {
            return "https://www.geocaching.com/seek/log.aspx?guid=" + cacheGuid;
        }

        public static string CoordsInfo(string f)
        {
            return "https://coords.info/" + f;
        }

        public static string GC(string f)
        {
            return "https://coords.info/GC" + f;
        }
    }

    public class Facebook
    {
        public static string FacebookProfile(string nick)
        {
            return "https://www.facebook.com/" + nick;
        }

        public static string FbTopSearch(string q)
        {
            return UriWebServices.FromChromeReplacement("https://www.facebook.com/search/top/?q=%s&epa=SEARCH_BOX", q);
        }
    }

    public static string KmoAll(string item)
    {
        return FromChromeReplacement("https://tritius.kmo.cz/Katalog/search?q=%s&area=247&field=0", item);
    }

    public static string KmoAV(string item)
    {
        return FromChromeReplacement("https://tritius.kmo.cz/Katalog/search?q=%s&area=238&field=0", item);
    }

    public static string KmoMP(string item)
    {
        return FromChromeReplacement("https://tritius.kmo.cz/Katalog/search?q=%s&area=242&field=0", item);
    }

    public const string chromeSearchstringReplacement = "%s";

    /// <summary>
    /// A1 is chrome replacement
    /// </summary>
    /// <param name="array"></param>
    /// <param name="what"></param>
    public static void SearchInAll(IEnumerable array, string what)
    {
        foreach (var item in array)
        {
            opened++;
            string uri = UriWebServices.FromChromeReplacement(item.ToString(), what);
            Process.Start(uri);
            if (opened % 10 == 0)
            {
                Debugger.Break();
            }
        }
    }

    public static string TwitterProfile(string nick)
    {
        return "https://www.twitter.com/" + nick;
    }

    public static string SearchGitHub(string item)
    {
        return "https://github.com/search?q=" + item;
    }

    public static string WebShare(string item)
    {
        return "https://webshare.cz/#/search?what=" + UrlEncode(item);
    }

    public static string YouTubeProfile(string nick)
    {
        return "https://www.youtube.com/c/" + nick;
    }

    public static string GooglePlusProfile(string nick)
    {
        return "https://www.google.com/"  + nick;
    }

    public static void GoogleSearchInAllSite(List<string> allRepairKitShops, string v)
    {
        foreach (var item in allRepairKitShops)
        {
            if (opened % 10 == 0 && opened != 0)
            {
                Debugger.Break();
            }
            var uri = GoogleSearchSite(item, v);
            Process.Start(uri);
            opened++;
        }
    }

    //http://www.bdsluzby.cz/stavebni-cinnost/materialy.htm

    public static string GoogleMaps(string coordsOrAddress, string center, string zoom)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("https://maps.google.com/maps?q=" + coordsOrAddress.Replace(AllStrings.space, "+") + "&hl=cs&ie=UTF8&t=h");
        if (!string.IsNullOrEmpty(center))
        {
            sb.Append("&ll=" + center);
        }
        if (!string.IsNullOrEmpty(zoom))
        {
            sb.Append("&z=" + zoom);
        }
        return sb.ToString();
    }

    /// <summary>
    /// A1 už musí být escapováno
    /// </summary>
    /// <param name="s"></param>
    public static string GoogleSearch(string s)
    {
        // q for reviews in czech and not translated 
        return "https://www.google.cz/search?hl=cs&q=" + UrlEncode(s);
    }

    public static string GoogleSearchImages(string s)
    {
        // q for reviews in czech and not translated 
        return "https://www.google.cz/search?hl=cs&tbm=isch&q=" + UrlEncode(s) ;
    }

    public static string GoogleSearchSite(string site, string v)
    {
        site = site.Trim();

        var uri = UH.CreateUri(site);

        var host = string.Empty;
        if (uri != null)
        {
            host = uri.Host;
        }
        else
        {
            host = site.ToString();
        }

        //https://www.google.cz/search?q=site%3Asunamo.cz+s
        return "https://www.google.cz/search?q=site%3A" + host + "+" + UrlEncode(v);
    }

    public static string FromChromeReplacement(string uri, string term)
    {
        // UrlEncode is not needed because not encode space to %20
        term = Uri.EscapeUriString(term);
        //term = UH.UrlEncode(term);
        return uri.Replace(chromeSearchstringReplacement, term);
    }

    public static string TopRecepty(string what)
    {
        return FromChromeReplacement("https://www.toprecepty.cz/vyhledavani.php?hledam=%s&kategorie=&autor=&razeni=", HttpUtility.UrlEncode( what));
    }

    /// <summary>
    /// Already new radekjancik
    /// Working with spaces right (SQL Server Scripts1)
    /// </summary>
    /// <param name="slnName"></param>
    public static string GitRepoInVsts(string slnName)
    {
        return "https://radekjancik.visualstudio.com/_git/" + HttpUtility.UrlEncode(slnName);
    }

    public static string AzureRepoWebUI(string slnName)
    {
        return "https://dev.azure.com/radekjancik/" + HttpUtility.UrlEncode(slnName);
    }

    public static string GoogleImFeelingLucky(string v)
    {
        return FromChromeReplacement("https://www.google.com/search?btnI&q=%s", v);
    }

    public static string MapyCz(string v)
    {
        return FromChromeReplacement("https://mapy.cz/?q=%s&sourceid=Searchmodule_1", v);
    }

    public static string UrlEncode(string s)
    {
        return UH.UrlEncode(s);
    }

    /// <summary>
    /// Summary description for YouTube
    /// </summary>
    public static class YouTube
    {
        public static void SearchYouTubeSerialSerie(int parts, int serie, string name)
        {
            parts++;
            for (int i = 1; i < parts; i++)
            {
                PH.Start(YouTube.GetLinkToSearch(name + " " + serie + " x " + i));
            }
        }

        public const string ytVideoStart = "https://www.youtube.com/watch?v=";

        public static string GetLinkToVideo(string kod)
        {
            return ytVideoStart + kod;
        }

        public static string GetHtmlAnchor(string kod)
        {
            return "<a href='" + GetLinkToVideo(kod) + "'>" + kod + "</a>";
        }

        public static string ReplaceOperators(string vstup)
        {
            return SH.ReplaceAll(vstup, "", "OR", "+", AllStrings.dash, AllStrings.qm, AllStrings.asterisk);
        }

        public static string GetLinkToSearch(string co)
        {
            return "https://www.youtube.com/results?search_query=" + UH.UrlEncode(co);
        }

        /// <summary>
        /// G null pokud se YT kód nepodaří získat
        /// </summary>
        /// <param name="uri"></param>
        public static string ParseYtCode(string uri)
        {
            return YouTube.ParseYtCode(uri);
        }
    }

    public static class CinemaMsk
    {
        public const string k3bohumin = "https://www.k3bohumin.cz/cz/search/?search_string=s";
        public const string kosmos = "https://www.google.com/search?q=site%3Akinokosmos.cz+s";
        public const string dkorlova = "https://www.google.com/search?q=site%3Adkorlova.cz+s";
        public const string kinokarvina = "https://www.google.com/search?q=site%3Akinokarvina.cz+s";
    }
}