using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;

public class QSHelperWebForms
    {
    public static string GetParameterUrlReferrerSE(HttpRequest Request, string p)
    {
        string d = Request.QueryString[p];
        if (d == null)
        {
            return string.Empty;
        }
        return HttpUtility.UrlDecode(d);
    }

    /// <summary>
    /// Vrátím -1 pokud se nepodaří najít nebo vyparsovat
    /// </summary>
    /// <param name="page"></param>
    /// <param name="p"></param>
    /// <returns></returns>
    public static int GetParameterInt(Page page, string p)
    {
        string d = page.Request.QueryString[p];
        int vr = -1;
        if (!int.TryParse(d, out vr))
        {
            return -1;
        }
        return vr;
    }

    public static string VratQSSimple2(MySitesShort ms, int countUp, string adresa, string[] p)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append(countUp.ToString() + ",");
        sb.Append(((byte)ms).ToString() + ",");
        sb.Append("'");
        sb.Append(adresa);
        sb.Append("',");
        QSHelper.GetArray(p, sb, true);
        sb.Append(",");
        QSHelper.GetArray(p, sb, false);

        string vr = sb.ToString();//.TrimEnd('&', '\'', '+');
        return vr;
    }

    /// <summary>
    /// A1 se ignoruje u Nope, tam je vždy pouze 1 up
    /// </summary>
    /// <param name="ms"></param>
    /// <param name="countUp"></param>
    /// <param name="adresa"></param>
    /// <param name="p"></param>
    /// <returns></returns>
    public static string VratQSSimple(MySitesShort ms, int countUp, string adresa, params object[] p)
    {
        StringBuilder sb = new StringBuilder();
        if (ms != MySitesShort.Nope)
        {
            //sb.Append("'../" + ms.ToString() + "_");
            sb.Append("'");
            for (int i = 0; i < countUp; i++)
            {
                sb.Append("../");
            }
            sb.Append(ms.ToString() + "_");
        }
        else
        {
            sb.Append("'../");
        }

        sb.Append(adresa + "?");
        int to = (p.Length / 2) * 2;
        for (int i = 0; i < to; i++)
        {
            string k = p[i].ToString();
            string v = k; //p[++i].ToString();
            sb.Append(k + "=' + encodeURIComponent(" + v + ") +'&");
        }
        string vr = sb.ToString();//.TrimEnd('&', '\'', '+');
        if (p.Length == 0)
        {
            vr = vr.Substring(0, vr.Length - 1) + "'";
            return vr;
        }
        vr = vr.Substring(0, vr.Length - 1) + "'";
        return vr;
    }

    /// <summary>
    /// Vrátí bez počátečního otazníku
    /// </summary>
    /// <param name="nameValueCollection"></param>
    /// <returns></returns>
    public static string GetQS(NameValueCollection nameValueCollection)
    {
        List<string> sb = new List<string>();
        for (int i = 0; i < nameValueCollection.Count; i++)
        {
            sb.Add(nameValueCollection.AllKeys[i] + "=" + nameValueCollection[i]);
        }
        return SH.Join('&', sb.ToArray());
    }

    /// <summary>
    /// Vrátí null pokud parametr nebude přítomný
    /// </summary>
    /// <param name="page"></param>
    /// <param name="p"></param>
    /// <returns></returns>
    public static string GetParameter(Page page, string p)
    {
        return page.Request.QueryString[p];
    }

    /// <summary>
    /// Vrátí SE pokud parametr nebude přítomný
    /// </summary>
    /// <param name="page"></param>
    /// <param name="p"></param>
    /// <returns></returns>
    public static string GetParameterSE(Page page, string p)
    {
        string d = page.Request.QueryString[p];
        if (d == null)
        {
            return string.Empty;
        }
        return HttpUtility.UrlDecode(d);
    }


    /// <summary>
    /// Vrátí SE pokud v QS A1 nebude parametr A2 nebo nebude platný vyparsovatelný řetězec bool
    /// </summary>
    /// <param name="page"></param>
    /// <param name="p"></param>
    /// <returns></returns>
    public static string GetParameterBoolSE(Page page, string p)
    {
        string s = page.Request.QueryString[p];
        if (s == null)
        {
            return string.Empty;
        }
        bool nvr = false;
        if (bool.TryParse(s, out nvr))
        {
            return nvr.ToString();
        }
        return string.Empty;
    }

    public static string GetParameterIntSE(Page page, string p)
    {
        string s = page.Request.QueryString[p];
        if (s == null)
        {
            return string.Empty;
        }
        int nvr = -1;
        if (int.TryParse(s, out nvr))
        {
            return nvr.ToString();
        }
        return string.Empty;
    }
}
