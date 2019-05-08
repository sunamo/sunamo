
using HtmlAgilityPack;
using sunamo.Helpers;
using sunamo.Html;
using System;
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


    public static bool IsNotFound(object uri)
    {
        HttpWebResponse r;
        var test = GetResponseText(uri.ToString(), HttpMethod.Get, null, out r);

        switch (r.StatusCode)
        {
            case HttpStatusCode.NotFound:
                return true;
        }
        return false;
    }

    public static bool SomeError(object uri)
    {
        HttpWebResponse r;
        var test = GetResponseText(uri.ToString(), HttpMethod.Get, null, out r);

        switch (r.StatusCode)
        {
            case HttpStatusCode.OK:
                return false;
        }
        return true;
    }

        /// <summary>
        /// A2 can be null (if dont have duplicated extension, set null)
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
            ext = SH.RemoveAfterFirst(ext, AllChars.q);
        }

        fn = SH.RemoveAfterFirst(fn, AllChars.q);
        string path = FS.Combine(folder2, fn + ext);
        FS.CreateFoldersPsysicallyUnlessThere(folder2);
        if (!FS.ExistsFile(path))
        {
            var c = HttpRequestHelper.GetResponseBytes(href, HttpMethod.Get);
            File.WriteAllBytes(path, c);
        }

        return ext;
    }

    public static void Download(string uri, BoolString DontHaveAllowedExtension, string path)
    {
        string p, fn, ext;
        FS.GetPathAndFileNameWithoutExtension(path, out p, out fn, out ext);
        Download(uri, null, p, fn, FS.GetExtension(path));
    }
}