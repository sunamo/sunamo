using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

public partial class UH
{
    public static string AppendHttpsIfNotExists(string p)
    {
        string p2 = p;
        if (!p.StartsWith("https"))
        {
            p2 = "https://" + p;
        }

        return p2;
    }
    public static string AppendHttpIfNotExists(string p)
    {
        string p2 = p;
        if (!p.StartsWith("http"))
        {
            p2 = "http://" + p;
        }

        return p2;
    }

    public static string CombineTrimEndSlash(params string[] p)
    {
        StringBuilder vr = new StringBuilder();
        foreach (string item in p)
        {
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
                vr.Append(item + AllChars.slash);
            }
            //vr.Append(item.TrimEnd(AllChars.slash) + AllStrings.slash);
        }
        return vr.ToString().TrimEnd(AllChars.slash);
    }

    public static string GetExtension(string image)
    {
        return FS.GetExtension(image);
    }

    public static string UrlEncode(string co)
    {
        return WebUtility.UrlEncode(co.Trim());
    }

    public static string GetFileName(string rp)
    {
        rp = rp.TrimEnd(AllChars.slash);
        int dex = rp.LastIndexOf(AllChars.slash);
        return rp.Substring(dex + 1);
    }

    public static string GetUriSafeString(string title)
    {
        if (String.IsNullOrEmpty(title)) return "";

        title = SH.AddBeforeUpperChars(title, AllChars.dash, false);

        title = SH.TextWithoutDiacritic(title);
        // replace spaces with single dash
        title = Regex.Replace(title, @"\s+", AllStrings.dash);
        // if we end up with multiple dashes, collapse to single dash            
        title = Regex.Replace(title, @"\-{2,}", AllStrings.dash);

        // make it all lower case
        title = title.ToLower();
        // remove entities
        title = Regex.Replace(title, @"&\w+;", "");
        // remove anything that is not letters, numbers, dash, or space
        title = Regex.Replace(title, @"[^a-z0-9\-\s]", "");
        // replace spaces
        title = title.Replace(AllChars.space, AllChars.dash);
        // collapse dashes
        title = Regex.Replace(title, @"-{2,}", AllStrings.dash);
        // trim excessive dashes at the beginning
        title = title.TrimStart(new char[] { AllChars.dash });
        // if it's too long, clip it
        if (title.Length > 80)
            title = title.Substring(0, 79);
        // remove trailing dashes
        title = title.TrimEnd(new char[] { AllChars.dash });
        return title;
    }

    public static string GetUriSafeString(string title, int maxLenght)
    {
        if (String.IsNullOrEmpty(title)) return "";

        title = SH.TextWithoutDiacritic(title);
        // replace spaces with single dash
        title = Regex.Replace(title, @"\s+", AllStrings.dash);
        // if we end up with multiple dashes, collapse to single dash            
        title = Regex.Replace(title, @"\-{2,}", AllStrings.dash);

        // make it all lower case
        title = title.ToLower();
        // remove entities
        title = Regex.Replace(title, @"&\w+;", "");
        // remove anything that is not letters, numbers, dash, or space
        title = Regex.Replace(title, @"[^a-z0-9\-\s]", "");
        // replace spaces
        title = title.Replace(AllChars.space, AllChars.dash);
        // collapse dashes
        title = Regex.Replace(title, @"-{2,}", AllStrings.dash);
        // trim excessive dashes at the beginning
        title = title.TrimStart(new char[] { AllChars.dash });
        // remove trailing dashes
        title = title.TrimEnd(new char[] { AllChars.dash });
        title = SH.ReplaceAll(title, AllStrings.dash, "--");
        // if it's too long, clip it
        if (title.Length > maxLenght)
            title = title.Substring(0, maxLenght);

        return title;
    }

    public static string GetUriSafeString(string tagName, int maxLength, BoolString methodInWebExists)
    {
        string uri = UH.GetUriSafeString(tagName, maxLength);
        int pripocist = 1;
        while (methodInWebExists.Invoke(uri))
        {
            if (uri.Length + pripocist.ToString().Length >= maxLength)
            {
                tagName = SH.RemoveLastChar(tagName);
            }
            else
            {
                string prip = pripocist.ToString();
                if (pripocist == 1)
                {
                    prip = "";
                }
                uri = UH.GetUriSafeString(tagName + prip, maxLength);
                pripocist++;
            }
        }
        return uri;
    }

    public static string UrlDecodeWithRemovePathSeparatorCharacter(string pridat)
    {
        pridat = WebUtility.UrlDecode(pridat);
        //%22 = \
        pridat = SH.ReplaceAll(pridat, "", "%22", "%5c");
        return pridat;
    }

    public static string GetPageNameFromUri(Uri uri)
    {
        int nt = uri.PathAndQuery.IndexOf(AllStrings.q);
        if (nt != -1)
        {
            return uri.PathAndQuery.Substring(0, nt);
        }
        return uri.PathAndQuery;
    }
    public static string GetPageNameFromUri(string atr, string p)
    {
        if (!atr.StartsWith("https://") && !atr.StartsWith("https" + ":" + "//"))
        {
            return GetPageNameFromUri(new Uri("https://" + p + AllStrings.slash + atr.TrimStart(AllChars.slash)));
        }
        return GetPageNameFromUri(new Uri(atr));
    }

    /// <summary>
    /// Pod�v� naprosto stejn� v�sledek jako UH.GetPageNameFromUri
    /// Tedy nap��klad pro str�nku https://localhost/Widgets/VerifyDomain.aspx?code=xer4o51s0aavpdmndwrmdbd1 d�v� /Widgets/VerifyDomain.aspx
    /// </summary>
    /// <param name="uri"></param>
    /// <returns></returns>
    public static string GetFilePathAsHttpRequest(Uri uri)
    {
        return uri.LocalPath;
    }



public static string ChangeExtension(string attrA, string oldExt, string extL)
    {
        attrA = SH.TrimEnd(attrA, oldExt);
        return attrA + extL;
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
}