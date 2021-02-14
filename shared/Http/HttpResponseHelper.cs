using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

/// <summary>
/// Can be only in shared coz is not available in standard
/// </summary>
public class HttpResponseHelper
{
    public static bool SomeError(HttpResponseMessage r)
    {
        if (r == null)
        {
            return true;
        }

        switch (StatusCodeToHttpStatusCode(r.StatusCode))
        {
            case HttpStatusCode.OK:
                return false;
        }
        return true;
    }

    public static bool SomeError(HttpWebResponse r)
    {
        if (r == null)
        {
            return true;
        }

        switch (StatusCodeToHttpStatusCode( r.StatusCode))
        {
            case HttpStatusCode.OK:
                return false;
        }
        return true;
    }

    public static bool IsNotFound(HttpWebResponse r)
    {
        if (r == null)
        {
            return true;
        }

        switch (StatusCodeToHttpStatusCode( r.StatusCode))
        {
            case HttpStatusCode.NotFound:
                return true;
        }
        return false;
    }

    static HttpStatusCode StatusCodeToHttpStatusCode(HttpStatusCode s)
    {
        return (HttpStatusCode)s;
    }
}