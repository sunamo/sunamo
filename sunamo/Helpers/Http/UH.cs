using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Text;
using System.Net;
using System.Collections.Generic;
using System.Linq;

public partial class UH
{
    public static Uri CreateUri(string s)
    {
        try
        {
            return new Uri(s);
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    public static string HostUriToPascalConvention(string s)
    {
        // Uri must be checked always before passed into method. Then I would make same checks again and again
        var uri = CreateUri(s);
        var result = SH.ReplaceAll(uri.Host, AllStrings.space, AllStrings.dot);
        result = ConvertPascalConvention.ToConvention(result);
        return SH.FirstCharUpper(result);
    }

    public static string GetHost(string s)
    {
        var u = CreateUri(AppendHttpIfNotExists(s));
        return u.Host;
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
    /// <returns></returns>
    public static bool IsValidUriAndDomainIs(string p, string domain, out bool surelyDomain)
    {
        string p2 = AppendHttpIfNotExists(p);
        Uri uri = null;
        surelyDomain = false;

        // Nema smysl hledat na přípony souborů, vrátil bych false pro to co by možná byla doména. Dnes už doména může být opravdu jakákoliv

        if (Uri.TryCreate(p2, UriKind.Absolute, out uri))
        {
            if (uri.Host == domain || domain == "*")
            {
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="p"></param>
    /// <returns></returns>
    public static string Combine(bool dir, params string[] p)
    {
        string vr = SH.Join(AllChars.slash, p).Replace("///", AllStrings.slash).Replace("//", AllStrings.slash).TrimEnd(AllChars.slash).Replace(":/", "://");
        if (dir)
        {
            vr += AllStrings.slash;
        }
        return vr;
    }



    /// <summary>
    /// Vrac� podle konvence se / na konci
    /// </summary>
    /// <param name="rp"></param>
    /// <returns></returns>
    public static string GetDirectoryName(string rp)
    {
        if (rp != AllStrings.slash)
        {
            rp = rp.TrimEnd(AllChars.slash);
        }


        int dex = rp.LastIndexOf(AllChars.slash);
        if (dex != -1)
        {
            return rp.Substring(0, dex + 1);
        }
        return rp;
    }

    public static string GetFileNameWithoutExtension(string p)
    {
        return Path.GetFileNameWithoutExtension(GetFileName(p));
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

    private static string Join(params object[] s)
    {
        return SH.Join(AllChars.slash, s);
    }


    public static string Combine(params string[] p)
    {
        return Combine(p.ToList());
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="p"></param>
    /// <returns></returns>
    public static string Combine(IEnumerable<string> p)
    {
        StringBuilder vr = new StringBuilder();
        int i = 0;
        foreach (string item in p)
        {
            i++;
            if (string.IsNullOrWhiteSpace(item))
            {
                continue;
            }
            if (item[item.Length - 1] == AllChars.slash)
            {
                vr.Append(item);
            }
            else
            {
                if (i == p.Length() && FS.GetExtension(item) != "")
                {
                    vr.Append(item);
                }
                else
                {
                    vr.Append(item + AllChars.slash);
                }
            }
            //vr.Append(item.TrimEnd(AllChars.slash) + AllStrings.slash);
        }
        return vr.ToString();
    }





    /// <summary>
    /// Vr�t� true pokud m� A1 protokol http nebo https
    /// </summary>
    /// <param name="p"></param>
    /// <returns></returns>
    public static bool HasHttpProtocol(string p)
    {
        p = p.ToLower();
        if (p.StartsWith("http" + ":" + "//"))
        {
            return true;
        }
        if (p.StartsWith("https" + ":" + "//"))
        {
            return true;
        }
        return false;
    }

    public static string RemovePrefixHttpOrHttps(string t)
    {
        t = t.Replace("http" + ":" + "//", "");
        t = t.Replace("https" + ":" + "//", "");
        return t;
    }

    /// <summary>
    /// V p��pad� �e v A1 nebude protokol, ulo�� se do A2 ""
    /// V p��pad� �e tam protokol bude, ulo�� se do A2 v�etn� ://
    /// </summary>
    /// <param name="t"></param>
    /// <param name="protocol"></param>
    /// <returns></returns>
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

    public static string GetProtocolString(Uri uri)
    {
        return uri.Scheme + "://";
    }

    /// <summary>
    /// Vr�t� cel� QS v�etn� po��te�n�ho otazn�ku
    /// Tedy nap��klad pro str�nku http://localhost/Widgets/VerifyDomain.aspx?code=xer4o51s0aavpdmndwrmdbd1 d�v� ?code=xer4o51s0aavpdmndwrmdbd1
    /// </summary>
    public static string GetQueryAsHttpRequest(Uri uri)
    {
        return uri.Query;
    }

    public static string RemoveHostAndProtocol(Uri uri)
    {
        string p = RemovePrefixHttpOrHttps(uri.ToString());
        int dex = p.IndexOf(AllChars.slash);
        return p.Substring(dex);
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
}