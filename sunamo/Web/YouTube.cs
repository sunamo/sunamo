using sunamo;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text.RegularExpressions;
/// <summary>
/// Summary description for YouTube
/// </summary>
public static partial class YouTube
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

}