﻿using System;
using System.Collections.Generic;
using System.Text;
using sunamo;
/// <summary>
/// Summary description for QSHelper
/// </summary>
public partial class QSHelper
{
    /// <summary>
    /// Do A1 se zadává Request.Url.Query.Substring(1) neboli třeba pid=1&amp;aid=10 
    /// </summary>
    /// <param name = "args"></param>
    /// <returns></returns>
    public static string GetNormalizeQS(string args)
    {
        if (args.Length != 0)
        {
            if (args.Contains("contextkey=") || args.Contains("guid=") || args.Contains("SelectingPhotos="))
            {
                // Pouze uploaduji fotky pomocí AjaxControlToolkit, ¨přece nebudu každé odeslání fotky ukládat do DB
                return null;
            }

            //args = args.Substring(1);
            List<string> splited = new List<string>(SH.Split(args, '&'));
            splited.Sort();
            args = SH.Join('&', splited.ToArray());
        }

        return args;
    }

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