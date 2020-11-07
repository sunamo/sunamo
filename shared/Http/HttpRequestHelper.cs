
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
/// Can be only in shared coz is not available in standard
/// </summary>
public static partial class HttpRequestHelper
{

    public static bool IsNotFound(object uri)
    {
        HttpWebResponse r;
        var test = GetResponseText(uri.ToString(), HttpMethod.Get, null, out r);

        return HttpResponseHelper.IsNotFound(r);
    }

    public static bool SomeError(object uri)
    {
        HttpWebResponse r;
        var test = GetResponseText(uri.ToString(), HttpMethod.Get, null, out r);

        return HttpResponseHelper.SomeError(r);
    }

    static Type type = typeof(HttpRequestHelper);

    /// <summary>
    /// A2 can be null (if dont have duplicated extension, set null)
    /// </summary>
    /// <param name="hrefs"></param>
    /// <param name="DontHaveAllowedExtension"></param>
    /// <param name="folder2"></param>
    /// <param name="co"></param>
    /// <param name="ext"></param>
    public static int DownloadAll(List< string> hrefs, BoolString DontHaveAllowedExtension, string folder2, FileMoveCollisionOption co, string ext = "")
    {
        int reallyDownloaded = 0;

        foreach (var item in hrefs)
        {
            var tempPath = FS.GetTempFilePath();

            var to = FS.Combine(folder2, UH.GetFileName(item) + ext);

            switch (co)
            {
                case FileMoveCollisionOption.AddSerie:
                case FileMoveCollisionOption.AddFileSize:
                case FileMoveCollisionOption.Overwrite:
                case FileMoveCollisionOption.DiscardFrom:
                case FileMoveCollisionOption.LeaveLarger:
                    break;
                case FileMoveCollisionOption.DontManipulate:
                    if (FS.ExistsFile(to))
                    {
                        continue;
                    }
                    break;
                default:
                    ThrowExceptions.NotImplementedCase(Exc.GetStackTrace(), type, Exc.CallingMethod(), co);
                    break;
            }

            Download(item, DontHaveAllowedExtension, tempPath);
            reallyDownloaded++;

            FS.MoveFile(tempPath, to, co);
        }

        return reallyDownloaded;
    }

        /// <summary>
        /// A2 can be null (if dont have duplicated extension, set null)
        /// In earlier time return ext
        /// Now return whether was downloaded
        /// </summary>
        /// <param name = "href"></param>
        /// <param name = "DontHaveAllowedExtension"></param>
        /// <param name = "folder2"></param>
        /// <param name = "fn"></param>
        /// <param name = "ext"></param>
    public static bool Download(string href, BoolString DontHaveAllowedExtension, string folder2, string fn, string ext = null)
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
            return true;
        }

        return false;
    }

    /// <summary>
    /// In earlier time return ext
    /// Now return whether was downloaded
    /// </summary>
    /// <param name="uri"></param>
    /// <param name="DontHaveAllowedExtension"></param>
    /// <param name="path"></param>
    /// <returns></returns>
    public static bool Download(string uri, BoolString DontHaveAllowedExtension, string path)
    {
        string p, fn, ext;
        FS.GetPathAndFileNameWithoutExtension(path, out p, out fn, out ext);
        return Download(uri, null, p, fn, FS.GetExtension(path));
    }
}