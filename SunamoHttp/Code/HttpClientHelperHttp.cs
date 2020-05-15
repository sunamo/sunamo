using sunamo.Data;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace sunamo.Helpers
{
    /// <summary>
    /// Pokud chceš náhradu za třídu HttpRequestHelperHttp, použij
    /// </summary>
    public class HttpClientHelperHttp
    {
        public static HttpClient hc = new HttpClient();
        private HttpClientHelperHttp()
        {
        }

        public static string GetResponseText(string address, HttpMethod method, HttpRequestData hrd)
        {
            HttpResponseMessage response =  GetResponse(address, method, hrd);
            return  GetResponseText(response);
        }

        private  static  string GetResponseText(HttpResponseMessage response)
        {
            string vr = "";
            using (response)
            {
                // Must be await, not AsyncHelper, not .Result, otherwise will be frozen
                //vr =  AsyncHelper.ci.GetResult<string>( response.Content.ReadAsStringAsync());
                vr = AsyncHelper.ci.GetResult<string>( response.Content.ReadAsStringAsync());
            }
            return vr;
        }

        public static Stream GetResponseStream(string address, HttpMethod method, HttpRequestData hrd)
        {
            HttpResponseMessage response = GetResponse(address, method, hrd);

            using (response)
            {
                return response.Content.ReadAsStreamAsync().Result;
            }
        }

        public static HttpResponseMessage GetResponse(string address, HttpMethod method, HttpRequestData hrd)
        {
            if (hrd == null)
            {
                hrd = new HttpRequestData();
            }

            SetHttpHeaders(hrd, hc);


            string adressCopy = address;
            #region Do samostatné metody pokud bych to někdy potřeboval, post neznamená že požadavek nemůže mít query string
            #endregion

            HttpContent httpContent = hrd.content;
            HttpResponseMessage response = null;
            if (method == HttpMethod.Get)
            {
                response = hc.GetAsync(address).Result;
            }
            else if (method == HttpMethod.Post)
            {
                var resp = hc.PostAsync(address, httpContent);
                response = resp.Result;
            }
            else
            {
                ThrowExceptions.Custom(Exc.GetStackTrace(), type, Exc.CallingMethod(),"Non supported http method in HttpMethod.GetResponseText");
            }
            //HttpResponseMessage response = responseTask.Result;
            return response;
        }

        static Type type = typeof(HttpClientHelperHttp);

        private static void SetHttpHeaders(HttpRequestData hrd, HttpClient hc)
        {
            hc = new HttpClient();
            //request.UserAgent = "Mozilla/5.0 (Windows NT 6.1) AppleWebKit/537.11 (KHTML, like Gecko) Chrome/23.0.1271.64 Safari/537.11";
            hc.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 6.3; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/44.0.2403.157 Safari/537.36");
            if (hrd.accept != null)
            {
                hc.DefaultRequestHeaders.Add(HttpKnownHeaderNames.Accept, hrd.accept);
            }
            if (hrd.keepAlive.HasValue)
            {
                hc.DefaultRequestHeaders.Add(HttpKnownHeaderNames.KeepAlive, hrd.keepAlive.ToString());
            }
            if (hrd != null)
            {
                foreach (var item in hrd.headers)
                {
                    hc.DefaultRequestHeaders.Add(item.Key, item.Value);
                }
            }
        }
    }
}