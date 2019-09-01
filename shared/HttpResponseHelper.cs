﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;

public class HttpResponseHelper
{
    public void IsOk(HttpResponse r)
    {
        
    }

    public static bool SomeError(HttpResponse r)
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

    public static bool IsNotFound(HttpResponse r)
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

    static HttpStatusCode StatusCodeToHttpStatusCode(int s)
    {
        return (HttpStatusCode)s;
    }
}
