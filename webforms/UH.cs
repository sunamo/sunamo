using sunamo;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;

namespace web
{
    public class UH
    {
        



        public static string GetWebUri2(Page sp, MySites item2)
        {
            string host = sp.Request.Url.Host.Replace("www.", "");
            bool localHost = host == "localhost";
            if (localHost)
            {
                return GetWebUri3(sp, item2.ToString().ToLower());
            }
            return "http://" + item2.ToString().ToLower() + "." + host;
        }

        public static string GetWebUriWithoutHttp(Page sp)
        {
            return sp.Request.Url.Host.Replace("www.", "");
        }

        public static string AppendSiteNameIfNotExists(HttpRequest p1, string p2)
        {
            return p1.Url.Host.Replace("www.", "") + "/" + p2.TrimStart('/');
        }

        /// <summary>
        /// Toto se používá pokud chci sdresu na nějakou stránku v adresáři, který je v rootu
        /// A2 je název webu, A3 název stránky
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <param name="p3"></param>
        /// <returns></returns>
        public static string GetWebUri(HttpRequest p1, string p2, string p3)
        {
            return "http://" + p1.Url.Host.Replace("www.", "") + "/" + p2 + "/" + p3;
        }

        /// <summary>
        /// Toto se používá pokud chci do adresáře v rootu, vytvoří tedy adresu například http://casdmladez.sunamo.net
        /// </summary>
        /// <param name="sp"></param>
        /// <param name="item2"></param>
        /// <returns></returns>
        public static string GetWebUri2(Page sp, string item2, bool localHost, string host)
        {
            return GetWebUri3(sp, item2);
        }

        /// <summary>
        /// Toto se používá pokud chci sdresu na nějakou stránku v adresáři, který je v rootu
        /// A2 je název webu, A3 název stránky
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <param name="p3"></param>
        /// <returns></returns>
        public static string GetWebUri(Page p1, string p2, string p3)
        {
            return "http://" + p1.Request.Url.Host.Replace("www.", "") + "/" + p2 + "/" + p3;
        }

        /// <summary>
        /// Odstraňuje www. z hostname
        /// Toto se používá když chci získat adresu v rootu nebo když mám kompletní relativní adresu k rootu.
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <returns></returns>
        public static string GetWebUri(Page p1, string p2)
        {
            return "http://" + p1.Request.Url.Host.Replace("www.", "") + "/" + p2.TrimStart('/');
        }

        public static string GetWebUri(HttpRequest Request, string p2)
        {
            return "http://" + Request.Url.Host.Replace("www.", "") + "/" + p2.TrimStart('/');
        }

        /// <summary>
        /// Neodstraňuje www. z hostname
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <returns></returns>
        public static string GetWebUri3(Page p1, string p2)
        {
            return "http://" + p1.Request.Url.Host + "/" + p2.TrimStart('/');
        }

        /// <summary>
        /// Neodstraňuje www. z hostname
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <returns></returns>
        public static string GetWebUri3(HttpRequest Request, string p2)
        {
            return "http://" + Request.Url.Host + "/" + p2.TrimStart('/');
        }

        public static string GetWithProtocol(byte protocol, string p2)
        {
            return GeneralColls.internetProtocols[protocol] + "://" + p2;
        }

        /// <summary>
        /// Unknown 0 Http 1 Https 2
        /// </summary>
        /// <param name="uriS"></param>
        /// <returns></returns>
        public static byte GetProtocol(string uriS)
        {
            for (byte i = 1; i < GeneralColls.internetProtocols.Count; i++)
            {
                if (uriS.StartsWith(GeneralColls.internetProtocols[i] + "://"))
                {
                    return i;
                }
            }
            return 0;
        }
    }
}
