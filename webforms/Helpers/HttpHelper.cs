using sunamo.Data;
using System;
using System.Collections.Generic;
using System.Web;

public static class HttpHelper
{
    public static int CountInHttpFileCollection(HttpFileCollection hfc)
    {
        int vr = ListOfHttpPostedFile(hfc).Count;
        return vr;
    }

    public static bool SetAccessControlHeaders(HttpRequest Request, HttpResponse Response)
    {
        Uri uri = Request.UrlReferrer;
        if (uri != null)
        {
            string port = "";
            if (uri.Port != 80)
            {
                port = ":" + uri.Port;
            }

            Response.AddHeader("Access-Control-Allow-Origin", uri.Scheme + Uri.SchemeDelimiter + uri.Host + port);
        }
        if (Request.HttpMethod == "OPTIONS")
        {
            Response.AddHeader("Access-Control-Allow-Methods", "GET, POST, PUT, DELETE, OPTIONS");
            //Response.AddHeader("Access-Control-Allow-Headers", "*");
            Response.AddHeader("Access-Control-Max-Age", "1728000");
            Response.Flush();
            Response.End();
            return true;
        }
        return false;
    }

    public static bool SetAccessControlHeaders(HttpContext c)
    {
        return SetAccessControlHeaders(c.Request, c.Response);
    }

    public static List<SunamoHttpPostedFile> ListOfHttpPostedFile(HttpFileCollection httpFileCollection)
    {
        List<SunamoHttpPostedFile> d = new List<SunamoHttpPostedFile>();
        for (int i = 0; i < httpFileCollection.Count; i++)
        {
            var hpf = httpFileCollection[i];
            if (IsFile(hpf))
            {
                d.Add(new SunamoHttpPostedFile( hpf.ContentLength, hpf.ContentType, hpf.FileName, hpf.InputStream));
            }
            
        }
        return d;
    }

    public static bool HasFile(HttpFileCollection httpFileCollection)
    {
        //List<HttpPostedFile> d = new List<HttpPostedFile>();
        foreach (string item in httpFileCollection)
        {
            var hpf = httpFileCollection[item];
            if (IsFile(hpf))
            {
                return true;
            }

        }
        return false;
    }

    public static bool IsFile(HttpPostedFile hpf)
    {
        if (/* hpf.ContentType != "application/octet-stream" && */ hpf.ContentLength != 0)
        {
            return true;
        }
        return false;
    }

    public static bool IsFile(SunamoHttpPostedFile hpf)
    {
        if (/* hpf.ContentType != "application/octet-stream" && */ hpf.ContentLength != 0)
        {
            return true;
        }
        return false;
    }

    public static List<HttpPostedFile> ListOfRealFiles(IList<HttpPostedFile> list)
    {
        List<HttpPostedFile> vr = new List<HttpPostedFile>();
        foreach (var item in list)
        {
            if (IsFile(item))
            {
                vr.Add(item);
            }
        }
        return vr;
    }

    public static List<SunamoHttpPostedFile> ListOfRealFiles(IList<SunamoHttpPostedFile> list)
    {
        List<SunamoHttpPostedFile> vr = new List<SunamoHttpPostedFile>();
        foreach (var item in list)
        {
            if (IsFile(item))
            {
                vr.Add(item);
            }
        }
        return vr;
    }


    public static int Count(IList<HttpPostedFile> list)
    {
        int vr = ListOfRealFiles(list).Count;
        return vr;
    }
}
