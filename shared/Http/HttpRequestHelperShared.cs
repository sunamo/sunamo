

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

    /// <summary>
    /// In earlier time return ext
    /// Now return whether was downloaded
    /// </summary>
    /// <param name="path"></param>
    /// <param name="uri"></param>
    /// <returns></returns>
    public static string DownloadOrRead(string path, string uri)
    {
        string html = null;

        if (!FS.ExistsFile(path))
        {
            Download(uri, null, path);
        }

        html = TF.ReadFile(path); 

        return html;
    }

    /// <summary>
    /// As folder is use Cache
    /// </summary>
    /// <param name="cache"></param>
    /// <param name="uri"></param>
    public static string DownloadOrRead(string uri)
    {
        var v = UH.GetFileName(uri);
        var fn = FS.ReplaceInvalidFileNameChars(v);
        fn = FS.Combine(AppData.ci.GetFolder(AppFolders.Cache), SH.AppendIfDontEndingWith(fn, AllExtensions.html));
        return DownloadOrRead(fn, uri);
    }

    public static bool ExistsPage(string url)
    {
        try
        {
            //Creating the HttpWebRequest
            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
            //Setting the Request method HEAD, you can also use GET too.
            request.Method = "HEAD";
            //Getting the Web Response.
            HttpWebResponse response = request.GetResponse() as HttpWebResponse;
            //Returns TRUE if the Status code == 200
            response.Close();
            return (response.StatusCode == HttpStatusCode.OK);
        }
        catch
        {
            //Any exception will returns false.
            return false;
        }
    }


    /// <summary>
    /// Is not async coz t.Result
    /// </summary>
    /// <param name="address"></param>
    public async static Task<string> GetResponseTextAsync(string address)
    {
        var request = (HttpWebRequest)WebRequest.CreateHttp(address);
        request.Timeout = int.MaxValue;
        request.UserAgent = "Mozilla/5.0 (Windows NT 6.1) AppleWebKit/537.11 (KHTML, like Gecko) Chrome/23.0.1271.64 Safari/537.11";
        var t = request.GetResponseAsync();
        HttpWebResponse response = null;
       // var response = (HttpWebResponse)t.Result;
        var result2 = AsyncHelper.ci.GetResult<WebResponse>(t);

        if (false)
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
                string vr = reader.ReadToEnd();

                //response.Dispose();
                result2.Dispose();

                return vr;
            }
        }

        var ts = result2.ToString();
        return ts;
        
    }

    /// <summary>
    /// A3 can be null
    /// </summary>
    /// <param name="address"></param>
    /// <param name="method"></param>
    /// <param name="hrd"></param>
    public static string GetResponseText(string address, HttpMethod method, HttpRequestData hrd)
    {
        HttpWebResponse response;
        return GetResponseText(address, method, hrd, out response);
    }

    public static IPAddress GetUserIP(HttpRequest Request)
    {
        IPAddress vr = null;
        //return (IPAddress)System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName()).AddressList.GetValue(0);
        if (!IPAddress.TryParse(GetUserIPString(Request), out vr))
        {
            return null;
        }
        return vr;
    }

/// <summary>
/// 
/// </summary>
/// <param name = "address"></param>
public static Stream GetResponseStream(string address, HttpMethod method)
{
    var request = (HttpWebRequest)WebRequest.Create(address);
    request.Method = method.Method;
    HttpWebResponse response = null;
    try
    {
        response = (HttpWebResponse)request.GetResponse();
    }
    catch (System.Exception)
    {
        return null;
    }

    return response.GetResponseStream();
}

    public static string GetResponseText(string address, HttpMethod method, HttpRequestData hrd, out HttpWebResponse response)
    {
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(address);
        return GetResponseText(request, method, hrd, out response);
    }

/// <summary>
/// A3 can be null
/// Dont forger Dispose on A4
/// </summary>
/// <param name = "address"></param>
/// <param name = "method"></param>
/// <param name = "hrd"></param>
    public static string GetResponseText(HttpWebRequest request, HttpMethod method, HttpRequestData hrd, out HttpWebResponse response)
    {


        NetHelperSunamo.NEVER_EAT_POISON_Disable_CertificateValidation();

        response = null;
        
        if (hrd == null)
        {
            hrd = new HttpRequestData();
        }

        var address = request.Address.ToString();

        int dex = address.IndexOf(AllChars.q);
        string adressCopy = address;
        if (method.Method.ToUpper() == "POST")
        {
            if (dex != -1)
            {
                address = address.Substring(0, dex);
            }
        }

        // Cant create new instance, in A1 can be setted up property which is not allowed in Headers
        //request.Address = address;

        string result = null;

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

            byte[] data = encoder.GetBytes(query);
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
            response = (HttpWebResponse)request.GetResponse();
            
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

                    result = reader.ReadToEnd();
                }
            
        }
        catch (System.Exception ex)
        {
            result = Exceptions.TextOfExceptions(ex);
        }

        return result;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name = "address"></param>
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

    /// <summary>
    /// Musí být v shared.web? ten HttpRequest je samozřejmě nekompatibilní
    /// Přesunul jsem metodu zpátky do shared
    /// Vrátí null pokud se nepodaří zjistit IP adresa
    /// </summary>
    /// <param name="Request"></param>
    public static string GetUserIPString(HttpRequest Request)
    {
        string vr = Request.ServerVariables["REMOTE_ADDR"];
        vr = BeforeTestingIpAddress(vr);
        if (string.IsNullOrWhiteSpace(vr) || SH.OccurencesOfStringIn(vr, AllStrings.dot) != 3)
        {
            string ipList = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

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

    public static string BeforeTestingIpAddress(string vr)
    {
        if (vr == "::1")
        {
            vr = "127.0.0.1";
        }

        return vr;
    }

}