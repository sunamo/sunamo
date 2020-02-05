using System;
using System.Net;
using System.Net.Http;
using System.Text;

namespace SunamoMathpix
{
    public class MathpixHelper
    {
        const string contentType = "content-type";
        const string appId = "app_id";
        const string appKey = "app_key";
        const string appJson = "application/json";
        const string uri = "https://api.mathpix.com/v3/text";

        string app_id; 
        string app_key;
        public MathpixHelper(string app_id, string app_key)
        {
            this.app_id = app_id;
            this.app_key = app_key;
        }

        public string TextClient(string base64)
        {
            WebClient data = new WebClient();
            data.Headers.Add(appId, app_id);
            data.Headers.Add(appKey, app_key);
            data.Headers.Add("src", base64);
            data.Headers.Add(contentType, "application/x-www-form-urlencoded");

            using (WebClient client = new WebClient())
            {
                var reqparm = new System.Collections.Specialized.NameValueCollection();
                reqparm.Add(appId, app_id);
                reqparm.Add(appKey, app_key);
                reqparm.Add("src", base64);

                byte[] responsebytes = client.UploadValues(uri, "POST", reqparm);
                string responsebody = Encoding.UTF8.GetString(responsebytes);
                int i = 0;
            }

            //var stream = data.OpenRead();

            var content = GetContent(base64);
            var onlySrc = GetContentOnlySrc(base64);

            try
            {
                var b1 = data.UploadString(uri, base64);
            }
            catch (Exception ex)
            {

            
            }

            try
            {
                var b3 = data.UploadString(uri, content);
            }
            catch (Exception ex)
            {

            }
            try
            {
                var b4 = data.UploadString(uri, onlySrc);
            }
            catch (Exception ex)
            {

             
            }

            

            return null;
        }

        public string Text(string base64)
        {
            //System.Net.HttpWebRequest httpWebRequest = System.Net.HttpWebRequest.CreateHttp();
            HttpRequestData data = new HttpRequestData();
            //data.headers.Add(contentType, appJson);
            data.headers.Add(appId, app_id);
            data.headers.Add(appKey, app_key);
            data.headers.Add("src", base64);
            data.contentType = appJson;
            string content = GetContent(base64);

            var result = HttpRequestHelper.GetResponseText(@"https://api.mathpix.com/v3/text?" + content, HttpMethod.Post, data);
            return result;
        }

        private string GetContentOnlySrc(string base64)
        {
            return "{" +
  "\"src\": \"" + base64 + "\"" +
"}";
        }


        private string GetContent(string base64)
        {
            return "{" +
  "\"content-type\": \"application/json\"," +
  "\"app_id\": \"" + app_id + "\"," +
  "\"app_key\": \"" + app_key + "\"" +
  "\"src\": \"" + base64 + "\"" +
"}";
        }
    }
}
