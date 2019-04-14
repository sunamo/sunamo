using System;
using System.Collections.Generic;
using System.Text;
using sunamo;

public partial class QSHelper{ 
public static void GetArray(string[] p, StringBuilder sb, bool uvo)
    {
        sb.Append("new Array(");
        //int to = (p.Length / 2) * 2;
        int to = p.Length;
        if (p.Length == 1)
        {
            to = 1;
        }

        int to2 = to - 1;
        if (to2 == -1)
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
}