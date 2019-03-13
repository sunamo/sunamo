
using HtmlAgilityPack;
using sunamo.Helpers;
using sunamo.Html;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
/// <summary>
/// Náhrada za třídu NetHelper
/// </summary>
public static partial class HttpRequestHelper
{
    /// <summary>
    /// A3 cant be null
    /// </summary>
    /// <param name = "address"></param>
    /// <param name = "method"></param>
    /// <param name = "hrd"></param>
    /// <returns></returns>
    public static string GetResponseText(string address, HttpMethod method, HttpRequestData hrd)
    {
        int dex = address.IndexOf('?');
        string adressCopy = address;
        if (method.Method.ToUpper() == "POST")
        {
            if (dex != -1)
            {
                address = address.Substring(0, dex);
            }
        }

        var request = (HttpWebRequest)WebRequest.Create(address);
        request.Method = method.Method;
        if (method == HttpMethod.Post)
        {
            string query = adressCopy.Substring(dex + 1);
            Encoding encoder = null;
            if (hrd.encodingPostData == null)
            {
                encoder = new ASCIIEncoding();
            }
            else
            {
                encoder = hrd.encodingPostData;
            }

            byte[] data = encoder.GetBytes((query));
            request.ContentType = "application/x-www-urlencoded";
            request.ContentLength = data.Length;
            request.GetRequestStream().Write(data, 0, data.Length);
        }

        //request.UserAgent = "Mozilla/5.0 (Windows NT 6.1) AppleWebKit/537.11 (KHTML, like Gecko) Chrome/23.0.1271.64 Safari/537.11";
        request.UserAgent = "Mozilla/5.0 (Windows NT 6.3; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/44.0.2403.157 Safari/537.36";
        if (hrd.contentType != null)
        {
            request.ContentType = hrd.contentType;
        }

        if (hrd.accept != null)
        {
            request.Accept = hrd.accept;
        }

        if (hrd != null)
        {
            foreach (var item in hrd.headers)
            {
                request.Headers.Add(item.Key, item.Value);
            }
        }

        try
        {
            using (var response = (HttpWebResponse)request.GetResponse())
            {
                Encoding encoding = null;
                if (response.CharacterSet == "")
                {
                //encoding = Encoding.UTF8;
                }
                else
                {
                    encoding = Encoding.GetEncoding(response.CharacterSet);
                }

                using (var responseStream = response.GetResponseStream())
                {
                    StreamReader reader = null;
                    if (encoding == null)
                    {
                        reader = new StreamReader(responseStream, true);
                    }
                    else
                    {
                        reader = new StreamReader(responseStream, encoding);
                    }

                    return reader.ReadToEnd();
                }
            }
        }
        catch (System.Exception ex)
        {
            return Exceptions.TextOfExceptions(ex);
        }
    }

    /// <summary>
    /// A2 can be null
    /// </summary>
    /// <param name = "href"></param>
    /// <param name = "DontHaveAllowedExtension"></param>
    /// <param name = "folder2"></param>
    /// <param name = "fn"></param>
    /// <param name = "ext"></param>
    /// <returns></returns>
    public static string Download(string href, BoolString DontHaveAllowedExtension, string folder2, string fn, string ext = null)
    {
        if (DontHaveAllowedExtension != null)
        {
            if (DontHaveAllowedExtension(ext))
            {
                ext += ".jpeg";
            }
        }

        if (string.IsNullOrWhiteSpace(ext))
        {
            ext = FS.GetExtension(href);
        }

        fn = SH.RemoveAfterFirst(fn, '?');
        string path = FS.Combine(folder2, fn + ext);
        FS.CreateFoldersPsysicallyUnlessThere(folder2);
        if (!FS.ExistsFile(path))
        {
            var c = HttpRequestHelper.GetResponseBytes(href, HttpMethod.Get);
            File.WriteAllBytes(path, c);
        }

        return ext;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name = "address"></param>
    /// <returns></returns>
    public static byte[] GetResponseBytes(string address, HttpMethod method)
    {
        var request = (HttpWebRequest)WebRequest.Create(address);
        request.Method = method.Method;
        request.UserAgent = "Mozilla/5.0 (Windows NT 6.1) AppleWebKit/537.11 (KHTML, like Gecko) Chrome/23.0.1271.64 Safari/537.11";
        using (var response = (HttpWebResponse)request.GetResponse())
        {
            Encoding encoding = null;
            if (response.CharacterSet == "")
            {
                encoding = Encoding.UTF8;
            }
            else
            {
                encoding = Encoding.GetEncoding(response.CharacterSet);
            }

            using (var responseStream = response.GetResponseStream())
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    responseStream.CopyTo(ms);
                    ms.Seek(0, SeekOrigin.Begin);
                    using (var reader = new StreamReader(ms, encoding))
                    {
                        using (BinaryReader br = new BinaryReader(reader.BaseStream))
                        {
                            return br.ReadBytes((int)ms.Length);
                        }
                    }
                }
            }
        }
    }

    
}