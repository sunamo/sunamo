using HtmlAgilityPack;

using sunamo.Helpers;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

public static partial class HttpRequestHelper{ 

/// <summary>
    /// 
    /// </summary>
    /// <param name = "address"></param>
    /// <returns></returns>
    public static Stream GetResponseStream(string address, HttpMethod method)
    {
        
        var request = (HttpWebRequest)WebRequest.Create(address);
        request.Method = method.Method;
        HttpWebResponse response = null;
        try
        {
            response = (HttpWebResponse)request.GetResponse();
        }
        catch (Exception ex)
        {
            return null;
        }

        return response.GetResponseStream();
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
}