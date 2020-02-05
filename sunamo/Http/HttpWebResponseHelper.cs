using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;

public class HttpWebResponseHelper
{


    public static bool SomeError(HttpWebResponse r)
    {
        if (r == null)
        {
            return true;
        }

        // 400 errors for Bojánci and other which doesn't exists on lfm
        // 429 Too many errors (mainly for 
        switch (r.StatusCode)
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

        switch (r.StatusCode)
        {
            case HttpStatusCode.NotFound:
                return true;
        }
        return false;
    }
}