using System;
using System.Collections.Generic;
using System.Net;
using System.Text;



    public partial class UH
    {
        public static string AppendHttpIfNotExists(string p)
        {
            string p2 = p;
            if (!p.StartsWith("http"))
            {
                p2 = "http://" + p;
            }
            return p2;
        }

        public static string CombineTrimEndSlash(params string[] p)
        {
            StringBuilder vr = new StringBuilder();
            foreach (string item in p)
            {
                if (string.IsNullOrWhiteSpace(item))
                {
                    continue;
                }
                if (item[item.Length - 1] == '/')
                {
                    vr.Append(item);
                }
                else
                {
                    vr.Append(item + '/');
                }
                //vr.Append(item.TrimEnd('/') + "/");
            }
            return vr.ToString().TrimEnd('/');
        }
    
public static string UrlEncode(string co)
        {
            return WebUtility.UrlEncode(co.Trim());
        }
}