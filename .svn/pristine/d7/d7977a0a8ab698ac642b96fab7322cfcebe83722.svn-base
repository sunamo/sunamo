using sunamo.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace webforms.Helpers
{
    public class HttpRequestHelpers
    {
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
        public static byte[] GetIPAddressInArray(HttpRequest httpRequest)
        {
            return IPAddressHelper.GetIPAddressInArray(GetUserIPString(httpRequest));
        }
        /// <summary>
        /// Vrátí null pokud se nepodaří zjistit IP adresa
        /// </summary>
        /// <param name="Request"></param>
        /// <returns></returns>
        public static string GetUserIPString(HttpRequest Request)
        {
            string vr = Request.ServerVariables["REMOTE_ADDR"];
            if (vr == "::1")
            {
                vr = "127.0.0.1";
            }
            if (string.IsNullOrWhiteSpace(vr) || SH.OccurencesOfStringIn(vr, ".") != 3)
            {
                string ipList = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

                if (!string.IsNullOrEmpty(ipList))
                {

                    vr = ipList.Split(',')[0];
                    if (SH.OccurencesOfStringIn(vr, ".") != 3)
                    {
                        return null;
                    }
                    return vr;
                }
            }
            return vr;
        }
    }
}
