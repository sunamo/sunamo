using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

public partial class SunamoWebClient : WebClient
{
    public HttpRequestData hrd = null;

    public SunamoWebClient()
    {

    }

    protected override WebRequest GetWebRequest(Uri uri)
    {
        WebRequest w = base.GetWebRequest(uri);
        w.Timeout = hrd.timeoutInS * 1000;
        return w;
    }
}