using sunamo.Data;
using sunamo.Helpers;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace sunamo.Helpers
{
    public class NetHelper
    {
        public async static Task<string> PostFiles(string address, HttpMethod method, IEnumerable<UploadFile> files, Dictionary<string, string> values, HttpRequestData hrd)
        {
            var boundary = "---------------------------" + DateTime.Now.Ticks.ToString("x", NumberFormatInfo.InvariantInfo);

            hrd.contentType = "multipart/form-data; boundary=" + boundary;
            //hrd.timeout = Timeout.Infinite;
            hrd.keepAlive = false;
            boundary = "--" + boundary;

            
            MemoryStream requestStream = new MemoryStream();
            var content = new StreamContent(requestStream);


            // Write the values
            foreach (string name in values.Keys)
            {
                var buffer = Encoding.ASCII.GetBytes(boundary + Environment.NewLine);
                requestStream.Write(buffer, 0, buffer.Length);
                buffer = Encoding.ASCII.GetBytes(string.Format("Content-Disposition: form-data; name=\"{0}\"{1}{1}", name, Environment.NewLine));
                requestStream.Write(buffer, 0, buffer.Length);
                buffer = Encoding.UTF8.GetBytes(values[name] + Environment.NewLine);
                requestStream.Write(buffer, 0, buffer.Length);
            }

            // Write the files
            foreach (var file in files)
            {
                var buffer = Encoding.ASCII.GetBytes(boundary + Environment.NewLine);
                requestStream.Write(buffer, 0, buffer.Length);

                buffer = Encoding.UTF8.GetBytes(string.Format("Content-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"{2}", file.Name, file.Filename, Environment.NewLine));
                requestStream.Write(buffer, 0, buffer.Length);

                buffer = Encoding.ASCII.GetBytes(string.Format("Content-Type: {0}{1}{1}", file.ContentType, Environment.NewLine));
                requestStream.Write(buffer, 0, buffer.Length);

                file.Stream.CopyTo(requestStream);

                buffer = Encoding.ASCII.GetBytes(Environment.NewLine);
                requestStream.Write(buffer, 0, buffer.Length);
            }

            var boundaryBuffer = Encoding.ASCII.GetBytes(boundary + "--");
            requestStream.Write(boundaryBuffer, 0, boundaryBuffer.Length);
            if (hrd.contentType != null)
            {
                content.Headers.Add("Content-Type", hrd.contentType);
            }
            hrd.content = content;

            var vr =  HttpClientHelper.GetResponseText(address, method, hrd);
            string vr2 = vr.Result;
            return vr2;
        }

        private static void SetHttpHeaders(HttpRequestData hrd, HttpRequestMessage hrm)
        {
            //request.UserAgent = "Mozilla/5.0 (Windows NT 6.1) AppleWebKit/537.11 (KHTML, like Gecko) Chrome/23.0.1271.64 Safari/537.11";
            hrm.Headers.Add("User-Agent", "Mozilla/5.0 (Windows NT 6.3; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/44.0.2403.157 Safari/537.36");
            
            if (hrd.accept != null)
            {
                hrm.Headers.Add(HttpKnownHeaderNames.Accept, hrd.accept);
            }
            if (hrd.keepAlive.HasValue)
            {
                hrm.Headers.Add(HttpKnownHeaderNames.KeepAlive, hrd.keepAlive.ToString());
            }
            if (hrd != null)
            {
                foreach (var item in hrd.headers)
                {
                    hrm.Headers.Add(item.Key, item.Value);
                }
            }
        }

       
    }
}
