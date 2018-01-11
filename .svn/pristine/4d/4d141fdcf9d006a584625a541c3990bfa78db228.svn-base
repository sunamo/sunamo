using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Text;
using System.Net;

namespace sunamo
{
    public class UH
    {
        public static string GetUriSafeString(string title, int maxLenght)
        {
            if (String.IsNullOrEmpty(title)) return "";

            title = SH.TextWithoutDiacritic(title);
            // replace spaces with single dash
            title = Regex.Replace(title, @"\s+", "-");
            // if we end up with multiple dashes, collapse to single dash            
            title = Regex.Replace(title, @"\-{2,}", "-");

            // make it all lower case
            title = title.ToLower();
            // remove entities
            title = Regex.Replace(title, @"&\w+;", "");
            // remove anything that is not letters, numbers, dash, or space
            title = Regex.Replace(title, @"[^a-z0-9\-\s]", "");
            // replace spaces
            title = title.Replace(' ', '-');
            // collapse dashes
            title = Regex.Replace(title, @"-{2,}", "-");
            // trim excessive dashes at the beginning
            title = title.TrimStart(new char[] { '-' });
            // remove trailing dashes
            title = title.TrimEnd(new char[] { '-' });
            title = SH.ReplaceAll(title, "-", "--");
            // if it's too long, clip it
            if (title.Length > maxLenght)
                title = title.Substring(0, maxLenght);

            return title;
        }

        public static string UrlEncode(string co)
        {
            return WebUtility.UrlEncode(co.Trim());
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
            title = Regex.Replace(title, @"\s+", "-");
            // if we end up with multiple dashes, collapse to single dash            
            title = Regex.Replace(title, @"\-{2,}", "-");
            // make it all lower case
            title = title.ToLower();
            // if it's too long, clip it
            if (title.Length > 80)
                title = title.Substring(0, 79);
            // remove trailing dash, if there is one
            if (title.EndsWith("-"))
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

        public static bool IsValidUriAndDomainIs(string p, string domain)
        {
            string p2 = AppendHttpIfNotExists(p);
            Uri uri = null;

            if (Uri.TryCreate(p2, UriKind.Absolute, out uri))
            {
                if (uri.Host == domain)
                {
                    return true;
                }
            }
            return false;
        }

        public static string GetPageNameFromUri(Uri uri)
        {
            int nt = uri.PathAndQuery.IndexOf("?");
            if (nt != -1)
            {
                return uri.PathAndQuery.Substring(0, nt);
            }
            return uri.PathAndQuery;
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

        public static string GetPageNameFromUri(string atr, string p)
        {
            if (!atr.StartsWith("http://") && !atr.StartsWith("https://"))
            {
                return GetPageNameFromUri(new Uri("http://" + p + "/" + atr.TrimStart('/')));
            }
            return GetPageNameFromUri(new Uri(atr));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public static string Combine(bool dir, params string[] p)
        {
            string vr = SH.Join('/', p).Replace("///", "/").Replace("//", "/").TrimEnd('/').Replace(":/", "://");
            if (dir)
            {

                vr += "/";
            }
            return vr;
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
                if (item[item.Length - 1] == '/')
                {
                    vr.Append(item);
                }
                else
                {
                    vr.Append(item + '/');
                }
                //vr.Append(item.TrimEnd('/') + "/");
            }
            return vr.ToString().TrimEnd('/');
        }

        /// <summary>
        /// Vrac� podle konvence se / na konci
        /// </summary>
        /// <param name="rp"></param>
        /// <returns></returns>
        public static string GetDirectoryName(string rp)
        {
            if (rp != "/")
            {
                rp = rp.TrimEnd('/');
            }


            int dex = rp.LastIndexOf('/');
            if (dex != -1)
            {
                return rp.Substring(0, dex + 1);
            }
            return rp;
        }

        public static string GetFileName(string rp)
        {
            rp = rp.TrimEnd('/');
            int dex = rp.LastIndexOf('/');
            return rp.Substring(dex + 1);
        }

        public static string GetFileNameWithoutExtension(string p)
        {
            return Path.GetFileNameWithoutExtension(GetFileName(p));
        }

        public static string UrlDecodeWithRemovePathSeparatorCharacter(string pridat)
        {
            pridat = WebUtility.UrlDecode(pridat);
            //%22 = \
            pridat = SH.ReplaceAll(pridat, "", "%22", "%5c");
            return pridat;
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

        

        public static string InsertBetweenPathAndFile(string uri, string vlozit)
        {
            string[] s = SH.Split(uri, "/");
            s[s.Length - 2] += "/" + vlozit;
            //Uri uri2 = new Uri(uri);
            string vr = null;
            vr = Join(s);
            return vr.Replace(":/", "://");
        }

        private static string Join(params string[] s)
        {
            return SH.Join('/', s);
        }





        /// <summary>
        /// 
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public static string Combine(params string[] p)
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
                if (item[item.Length - 1] == '/')
                {
                    vr.Append(item);
                }
                else
                {
                    if (i == p.Length && FS.GetExtension(item) != "")
                    {
                        vr.Append(item);
                    }
                    else
                    {
                        vr.Append(item + '/');
                    }
                }
                //vr.Append(item.TrimEnd('/') + "/");
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
            if (p.StartsWith("http://"))
            {
                return true;
            }
            if (p.StartsWith("https://"))
            {
                return true;
            }
            return false;
        }

        public static string RemovePrefixHttpOrHttps(string t)
        {
            t = t.Replace("http://", "");
            t = t.Replace("https://", "");
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

            if (t.Contains("http://"))
            {
                protocol = "http://";
                t = t.Replace("http://", "");
                return t;
            }
            if (t.Contains("https://"))
            {
                protocol = "https://";
                t = t.Replace("https://", "");
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
        /// Pod�v� naprosto stejn� v�sledek jako UH.GetPageNameFromUri
        /// Tedy nap��klad pro str�nku http://localhost/Widgets/VerifyDomain.aspx?code=xer4o51s0aavpdmndwrmdbd1 d�v� /Widgets/VerifyDomain.aspx
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        public static string GetFilePathAsHttpRequest(Uri uri)
        {
            return uri.LocalPath;
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
            int dex = p.IndexOf('/');
            return p.Substring(dex);
        }

        public static bool Contains(Uri source, string hostnameEndsWith, string pathContaint, params string[] qsContainsAll)
        {
            hostnameEndsWith = hostnameEndsWith.ToLower();
            pathContaint = pathContaint.ToLower();
            Uri uri = new Uri(source.ToString().ToLower());
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
}
