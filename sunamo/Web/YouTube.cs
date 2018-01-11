using sunamo;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text.RegularExpressions;


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
