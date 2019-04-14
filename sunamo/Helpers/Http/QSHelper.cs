using System;
using System.Collections.Generic;
using System.Text;
using sunamo;
/// <summary>
/// Summary description for QSHelper
/// </summary>
public partial class QSHelper
{

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public static string GetParameter(string uri, string nameParam)
    {
        var main = SH.Split(uri, "?", "&");
        foreach (string var in main)
        {
            var v = SH.Split(var, "=");
            if (v[0] == nameParam)
            {
                return v[1];
            }
        }

        return null;
    }

    /// <summary>
    /// A1 je adresa bez konzového otazníku
    /// Všechny parametry automaticky zakóduje metodou UH.UrlEncode
    /// </summary>
    /// <param name = "adresa"></param>
    /// <param name = "p"></param>
    /// <returns></returns>
    public static string GetQS(string adresa, params object[] p)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append(adresa + "?");
        int to = (p.Length / 2) * 2;
        for (int i = 0; i < p.Length; i++)
        {
            if (i == to)
            {
                break;
            }

            string k = p[i].ToString();
            string v = UH.UrlEncode(p[++i].ToString());
            sb.Append(k + "=" + v + "&");
        }

        return sb.ToString().TrimEnd('&');
    }

}