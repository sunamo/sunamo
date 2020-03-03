

using sunamo.Helpers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

/// <summary>
/// Can be only in shared coz is not available in standard
/// </summary>
public static partial class HttpRequestHelper{
    public static string DownloadOrRead(string path, string uri)
    {
        string html = null;

        if (FS.ExistsFile(path))
        {
            html = TF.ReadFile(path);
        }
        else
        {
            html = Download(uri, null, path);
        }

        return html;
    }

    /// <summary>
    /// As folder is use Cache
    /// </summary>
    /// <param name="cache"></param>
    /// <param name="uri"></param>
    
    public static string GetUserIPString(HttpRequest Request)
    {
        string vr =  Request.ServerVariables["REMOTE_ADDR"];
        if (vr == "::1")
        {
            vr = "127.0.0.1";
        }
        if (string.IsNullOrWhiteSpace(vr) || SH.OccurencesOfStringIn(vr, AllStrings.dot) != 3)
        {
            string ipList =  Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

            if (!string.IsNullOrEmpty(ipList))
            {

                vr = ipList.Split(AllChars.comma)[0];
                if (SH.OccurencesOfStringIn(vr, AllStrings.dot) != 3)
                {
                    return null;
                }
                return vr;
            }
        }
        return vr;
    }

    
}