using System;
using System.Collections.Generic;
using System.Text;
using sunamo;

/// <summary>
/// Summary description for QSHelper
/// </summary>
public  class QSHelper
{
    /// <summary>
    /// Do A1 se zadává Request.Url.Query.Substring(1) neboli třeba pid=1&amp;aid=10 
    /// </summary>
    /// <param name="args"></param>
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
            #region Nyní nahradím argumenty QS které jsou výchozí a proto tam nemusejí být v DB
            #endregion
        }
        return args;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public static string GetParameter(string uri, string nameParam)
    {
        string[] main = SH.Split(uri, "?", "&");
        foreach (string var in main)
        {
            string[] v = SH.Split(var, "=");
            if (v[0] == nameParam)
            {
                return v[1];
            }
        }
        return null;
    }

    public static string GetParameterSE(string r1, string p)
    {
        p = p + "=";
        int dexPocatek = r1.IndexOf(p);
        if (dexPocatek != -1)
        {
            int dexKonec = r1.IndexOf("&", dexPocatek);
            dexPocatek = dexPocatek + p.Length;
            if (dexKonec != -1)
            {
                return SH.Substring(r1, dexPocatek, dexKonec);
            }
            return r1.Substring(dexPocatek);
        }
        return "";

    }

    

    /// <summary>
    /// A1 je adresa bez konzového otazníku
    /// Všechny parametry automaticky zakóduje metodou UH.UrlEncode
    /// </summary>
    /// <param name="adresa"></param>
    /// <param name="p"></param>
    /// <returns></returns>
    public static string GetQS(string adresa, params object[] p)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append(adresa + "?");
        int to = (p.Length / 2) *2;
        for (int i = 0; i < p.Length; i++)
        {
            if (i == to)
            {
                break;
            }
            string k = p[i].ToString();
            string v = UH.UrlEncode( p[++i].ToString());
            sb.Append(k + "=" + v + "&");
        }
        return sb.ToString().TrimEnd('&');
    }

    

    

    public static void GetArray(string[] p, StringBuilder sb, bool uvo)
    {
        sb.Append("new Array(");
        //int to = (p.Length / 2) * 2;
        int to = p.Length;
        if (p.Length == 1)
        {
            to = 1;
        }
        int to2 = to -1;
        if (to2==-1)
	{
            to2 = 0;
	}
        if (uvo)
        {
            for (int i = 0; i < to; i++)
            {
                string k = p[i].ToString();
                sb.Append("\"" + k + "\"");
                if (to2 != i)
                {
                    sb.Append(",");
                }
                
            }
        }
        else
        {
            for (int i = 0; i < to; i++)
            {
                string k = p[i].ToString();
                sb.Append("ToString(" + k + ").toString()");
                if (to2 != i)
                {
                    sb.Append(",");
                }
            }
        }
        sb.Append(")");
    }

    

    

    
}
