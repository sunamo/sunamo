using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Text;
using System.Net;
using System.Collections.Generic;
using System.Linq;

public partial class UH
{
    #region Other methods

    public static string HostUriToPascalConvention(string s)
    {
        // Uri must be checked always before passed into method. Then I would make same checks again and again
        var uri = CreateUri(s);
        var result = SH.ReplaceAll(uri.Host, AllStrings.space, AllStrings.dot);
        result = ConvertPascalConvention.ToConvention(result);
        return SH.FirstCharUpper(result);
    }

    private static string GetUriSafeString2(string title)
    {
        if (String.IsNullOrEmpty(title)) return "";

        // remove entities
        title = Regex.Replace(title, @"&\w+;", "");
        // remove anything that is not letters, numbers, dash, or space
        title = Regex.Replace(title, @"[^A-Za-z0-9\-\s]", "");
        // remove any leading or trailing spaces left over
        title = title.Trim();
        // replace spaces with single dash
        title = Regex.Replace(title, @"\s+", AllStrings.dash);
        // if we end up with multiple dashes, collapse to single dash            
        title = Regex.Replace(title, @"\-{2,}", AllStrings.dash);
        // make it all lower case
        title = title.ToLower();
        // if it's too long, clip it
        if (title.Length > 80)
            title = title.Substring(0, 79);
        // remove trailing dash, if there is one
        if (title.EndsWith(AllStrings.dash))
            title = title.Substring(0, title.Length - 1);
        return title;
    }

    public static string InsertBetweenPathAndFile(string uri, string vlozit)
    {
        var s = SH.Split(uri, AllStrings.slash);
        s[s.Count - 2] += AllStrings.slash + vlozit;
        //Uri uri2 = new Uri(uri);
        string vr = null;
        vr = Join(s);
        return vr.Replace(":/", "://");
    }

    public static bool Contains(Uri source, string hostnameEndsWith, string pathContaint, params string[] qsContainsAll)
    {
        hostnameEndsWith = hostnameEndsWith.ToLower();
        pathContaint = pathContaint.ToLower();
        Uri uri = CreateUri(source.ToString().ToLower());
        if (uri.Host.EndsWith(hostnameEndsWith))
        {
            if (UH.GetFilePathAsHttpRequest(uri).Contains(pathContaint))
            {
                foreach (var item in qsContainsAll)
                {
                    if (!uri.Query.Contains(item))
                    {
                        return false;
                    }
                    return true;
                }
            }
        }
        return false;
    } 
    #endregion

    #region Is*
    public static bool IsHttpDecoded(ref string input)
    {
        string decoded = WebUtility.UrlDecode(input);
        if (true)
        {
        }
        return false;
    }

    /// <summary>
    /// A2 can be * - then return true for any domain
    /// </summary>
    /// <param name="p"></param>
    /// <param name="domain"></param>
    
    public static string RemovePrefixHttpOrHttps(string t, out string protocol)
    {
        if (t.Contains("http" + ":" + "//"))
        {
            protocol = "http" + ":" + "//";
            t = t.Replace("http" + ":" + "//", "");
            return t;
        }
        if (t.Contains("https" + ":" + "//"))
        {
            protocol = "https" + ":" + "//";
            t = t.Replace("https" + ":" + "//", "");
            return t;
        }
        protocol = "";
        return t;
    }

    public static string RemoveHostAndProtocol(Uri uri)
    {
        string p = RemovePrefixHttpOrHttps(uri.ToString());
        int dex = p.IndexOf(AllChars.slash);
        return p.Substring(dex);
    }

    public static bool IsUri(string href)
    {
        var uri = CreateUri(href);
        return uri != null;
    }
    #endregion


}