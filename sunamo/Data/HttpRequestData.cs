using System.Collections.Generic;
using System.Net.Http;
using System.Text;

public class HttpRequestData
{
    public Dictionary<string, string> headers = new Dictionary<string, string>();
    public string contentType = null;
    public string accept = null;
    public Encoding encodingPostData;
    //public int? timeout = null; // Není v třídě HttpKnownHeaderNames
    public bool? keepAlive = null;
    public HttpContent content = null;
}
