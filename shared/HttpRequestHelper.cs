
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

        return HttpWebResponseHelper.IsNotFound(r);
    }

    public static bool SomeError(object uri)
    {
        HttpWebResponse r;
        var test = GetResponseText(uri.ToString(), HttpMethod.Get, null, out r);

        return HttpWebResponseHelper.SomeError(r);
    }

    /// <summary>
    /// A2 can be null (if dont have duplicated extension, set null)
    /// </summary>
    /// <param name="hrefs"></param>
    /// <param name="DontHaveAllowedExtension"></param>
    /// <param name="folder2"></param>
    /// <param name="co"></param>
    /// <param name="ext"></param>
    public static void DownloadAll(List< string> hrefs, BoolString DontHaveAllowedExtension, string folder2, FileMoveCollisionOption co, string ext = null)
    {
        foreach (var item in hrefs)
        {
            var tempPath = FS.GetTempFilePath();

            Download(item, DontHaveAllowedExtension, tempPath);
            var to = FS.Combine(folder2, Path.GetFileName(item) + ext);

            FS.MoveFile(tempPath, to, co);
        }
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

        if (!FS.ExistsFile(path) || FS.GetFileSize(path) == 0)
        {
            var c = HttpRequestHelper.GetResponseBytes(href, HttpMethod.Get);
            
            File.WriteAllBytes(path, c);
        }

        return ext;
    }

    public static string Download(string uri, BoolString DontHaveAllowedExtension, string path)
    {
        string p, fn, ext;
        FS.GetPathAndFileNameWithoutExtension(path, out p, out fn, out ext);
        return Download(uri, null, p, fn, FS.GetExtension(path));
    }
}