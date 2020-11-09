using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using sunamo;
/// <summary>
/// Summary description for QSHelper
/// </summary>
public partial class QSHelper
{
    /// <summary>
    /// GetParameter = return null when not found
    /// GetParameterSE = return string.Empty when not found
    /// </summary>
    public static string GetParameter(string uri, string nameParam)
    {
        // 
        var main = SH.Split(uri, AllStrings.q, "&");
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
    /// get value of A2 parametr in A1
    /// GetParameter = return null when not found
    /// GetParameterSE = return string.Empty when not found
    /// </summary>
    /// <param name="uri"></param>
    /// <param name="nameParam"></param>
    public static string GetParameterSE(string uri, string nameParam)
    {
        nameParam = nameParam + "=";
        int dexPocatek = uri.IndexOf(nameParam);
        if (dexPocatek != -1)
        {
            int dexKonec = uri.IndexOf("&", dexPocatek);
            dexPocatek = dexPocatek + nameParam.Length;
            if (dexKonec != -1)
            {
                return SH.Substring(uri, dexPocatek, dexKonec, SubstringArgs.Instance);
            }

            return uri.Substring(dexPocatek);
        }

        return "";
    }

    /// <summary>
    /// A1 je adresa bez konzového otazníku
    /// Všechny parametry automaticky zakóduje metodou UH.UrlEncode
    /// </summary>
    /// <param name = "adresa"></param>
    /// <param name = "p"></param>
    public static string GetQS(string adresa, params object[] p2)
    {
        var p = CA.TwoDimensionParamsIntoOne(p2);

        StringBuilder sb = new StringBuilder();
        sb.Append(adresa + AllStrings.q);
        int to = (p.Count / 2) * 2;
        for (int i = 0; i < p.Count; i++)
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

    public static string GetQS(string adresa, Dictionary<string, string> p2)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append(adresa + AllStrings.q);

        foreach (var item in p2)
        {
            sb.Append(item.Key + AllStrings.equals + item.Value + AllStrings.amp);
        }

        return sb.ToString().TrimEnd('&');
    }

    /// <summary>
    /// Do A1 se zadává Request.Url.Query.Substring(1) neboli třeba pid=1&amp;aid=10 
    /// </summary>
    /// <param name = "args"></param>
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
    /// Must get just qs without uri => use UH.GetQueryAsHttpRequest before
    /// </summary>
    /// <param name="qs"></param>
    /// <returns></returns>
    public static Dictionary<string, string> ParseQs(string qs)
    {
        Dictionary<string, string> d = new Dictionary<string, string>();

        qs = qs.TrimStart(AllChars.q);

        var parts = SH.Split(qs, "&", "=");

        return DictionaryHelper.GetDictionaryByKeyValueInString<string>(parts);
    }

    public static void GetArray(List<string> p, StringBuilder sb, bool uvo)
    {
        sb.Append("new Array(");
        //int to = (p.Length / 2) * 2;
        int to = p.Count;
        if (p.Count == 1)
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
                sb.Append(AllStrings.qm + k + AllStrings.qm);
                if (to2 != i)
                {
                    sb.Append(AllStrings.comma);
                }
            }
        }
        else
        {
            for (int i = 0; i < to; i++)
            {
                string k = p[i].ToString();
                sb.Append("su.ToString(" + k + ")");
                if (to2 != i)
                {
                    sb.Append(AllStrings.comma);
                }
            }
        }

        sb.Append(AllStrings.rb);
    }

    public static Dictionary<string, string> ParseQs(NameValueCollection qs)
    {
        Dictionary<string, string> dict = new Dictionary<string, string>();

        foreach (var item in qs)
        {
            var key = item.ToString();
            var v = qs.Get(key);

            dict.Add(key, v);
        }

        return dict;
    }
}