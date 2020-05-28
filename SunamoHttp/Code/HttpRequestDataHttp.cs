using System.Collections.Generic;
using System.Net.Http;
using System.Text;

public class HttpRequestDataHttp
{
    public Dictionary<string, string> headers = new Dictionary<string, string>();
    public string contentType = null;
    public string accept = null;
    public Encoding encodingPostData;
    //public int? timeout = null; // Není v třídě HttpKnownHeaderNames
    public bool? keepAlive = null;
    /// <summary>
    /// Assign: StreamContent,ByteArrayContent,FormUrlEncodedContent,StringContent,MultipartContent,MultipartFormDataContent
    /// </summary>
    public HttpContent content = null;
}