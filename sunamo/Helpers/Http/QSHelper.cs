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

    public static Dictionary<string, string> ParseQs(string qs)
    {
        Dictionary<string, string> d = new Dictionary<string, string>();

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
                sb.Append("ToString(" + k + "." + "." + "toString()");
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