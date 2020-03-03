using Newtonsoft.Json;
using System.Xml;
using System.Collections.Generic;
using System;
using sunamo;
using sunamo.Essential;
public partial class SunamoJson
{
    /// <summary>
    /// Musí se to zkonvertovat do xml, protože to je jediná možnost jak to parsovat.
    /// </summary>
    /// <param name = "fd"></param>
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
    /// <param name = "fd"></param>
    public static string ConvertToJson(string fd)
    {
        return JsonConvert.DeserializeObject(fd).ToString();
    }

    public static string FormatJson(string json)
    {
        dynamic parsedJson = JsonConvert.DeserializeObject(json);
        return JsonConvert.SerializeObject(parsedJson, Newtonsoft.Json.Formatting.Indented);
    }

    public static string SerializeXmlString(object p)
    {
        return JsonConvert.SerializeObject(p);
    }

}