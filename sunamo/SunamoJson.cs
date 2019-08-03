using Newtonsoft.Json;
using System.Xml;
using System.Collections.Generic;
using System;
using sunamo;
using sunamo.Essential;

public class SunamoJson
{
    /// <summary>
    /// Musí se to zkonvertovat do xml, protože to je jediná možnost jak to parsovat.
    /// </summary>
    /// <param name="fd"></param>
    /// <returns></returns>
    public static string ConvertToXml(string fd)
    {
        return JsonConvert.DeserializeXmlNode(fd, ThisApp.Name, false).OuterXml;
    }

    public static string SerializeXmlNode(XmlNode xn)
    {
        return JsonConvert.SerializeXmlNode(xn);
    }

    /// <summary>
    /// Nač JSON převádět na JSON? 
    /// </summary>
    /// <param name="fd"></param>
    /// <returns></returns>
    public static string ConvertToJson(string fd)
    {
        return JsonConvert.DeserializeObject(fd).ToString();
    }



    public static string SerializeXmlString(object p)
    {
        return JsonConvert.SerializeObject(p);
    }

    public static string SerializeObject(object ab, bool intended = true)
    {
        Newtonsoft.Json.Formatting formatting = Newtonsoft.Json.Formatting.None;
        if (intended)
        {
            formatting = Newtonsoft.Json.Formatting.Indented;
        }
        string dd = JsonConvert.SerializeObject(ab, formatting);//.Replace("\\\"", AllStrings.qm);
        List<char> ch = new List<char>(dd.ToCharArray());
        ch[0] = AllChars.bs;
        ch.Insert(1, AllChars.cbl);
        ch.Insert(ch.Count - 1, AllChars.cbr);
        ch[ch.Count - 1] = AllChars.bs;
        string vr = new string(ch.ToArray());
        return vr;  //.Substring(1, vr.Length - 2);
    }
}
