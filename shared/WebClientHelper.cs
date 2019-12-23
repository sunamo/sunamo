using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
public partial class WebClientHelper
{
    static WebClient wc = new WebClient();

    public static Stream GetResponseStream(string address, HttpMethod method)
    {
        return wc.OpenRead(address);
    }
}