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
    /// Pokud chceš náhradu za třídu HttpRequestHelper, použij
    /// </summary>
    public class HttpClientHelper
    {
        public static HttpClient hc = new HttpClient();
        private HttpClientHelper()
        {

        }

        public async static Task<string> GetResponseText(string address, HttpMethod method, HttpRequestData hrd)
        {
            HttpResponseMessage response = await GetResponse(address, method, hrd);
            return await GetResponseText(response);
        }

        private static async Task<string> GetResponseText(HttpResponseMessage response)
        {
            string vr = "";
            using (response)
            {
                vr = await response.Content.ReadAsStringAsync();
            }
            return vr;
        }

        public async static Task< Stream> GetResponseStream(string address, HttpMethod method, HttpRequestData hrd)
        {
            HttpResponseMessage response = await GetResponse(address, method, hrd);

            using (response)
            {
                return await response.Content.ReadAsStreamAsync();
            }
        }

        public async static Task< HttpResponseMessage> GetResponse(string address, HttpMethod method, HttpRequestData hrd)
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
                response = await hc.GetAsync(address);
            }
            else if (method == HttpMethod.Post)
            {
                var resp = hc.PostAsync(address, httpContent);
                response = resp.Result;
            }
            else
            {
                throw new Exception("Non supported http method in HttpMethod.GetResponseText");
            }
            //HttpResponseMessage response = responseTask.Result;
            return response;
        }

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
