using System;
using System.Text;
using System.Web;

public static class UriWebServices
{
    public const string SearchTermSubsitite = "%s";
    
    public static string KmoAV(string name)
    {
        return ChromeOmniboxSearch("https://tritius.kmo.cz/Katalog/search?q=%s&area=238&field=0", name);
    }

    public static string KmoMP(string name)
    {
        return ChromeOmniboxSearch("https://tritius.kmo.cz/Katalog/search?q=%s&area=242&field=0", name);
    }

    public static string KmoAll(string name)
    {
        return ChromeOmniboxSearch("https://tritius.kmo.cz/Katalog/search?q=%s&area=247&field=0", name);
    }

    public static string MapyCzSearch(string what)
    {
        string mapyCzSearch = $"https://en.mapy.cz/zakladni?q={SearchTermSubsitite}";
        return ChromeOmniboxSearch( mapyCzSearch, what);
    }

    private static string ChromeOmniboxSearch(string mapyCzSearch, string what)
    {
        return mapyCzSearch.Replace(SearchTermSubsitite, HttpUtility.UrlEncode(what));
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
    public static Uri GoogleSearch(string s)
    {
        Uri uri = new Uri("https://www.google.cz/search?q=" + s);
        
        return uri;
    }
}
