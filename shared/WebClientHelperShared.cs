using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

public partial class WebClientHelper{
    static WebClient wc = new WebClient();

public static string GetResponseText(string address, HttpMethod method, HttpRequestData hrd)
    {
        return wc.DownloadString(address);
    }

public static byte[] GetResponseBytes(string address, HttpMethod method)
    {
        return wc.DownloadData(address);
    }
}