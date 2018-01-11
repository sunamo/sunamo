using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Xml;
using System.Xml.Linq;

public class XHelperWebForms
    {
        public static void WriteNamespaceToFile(string file, string prefix, string uri)
        {
            NameTable nt = new NameTable();
            XmlNamespaceManager xn2 = new XmlNamespaceManager(nt);
            xn2.AddNamespace(prefix, uri);
            XmlDocument xd2 = new XmlDocument(nt);
            xd2.Load(file);
            xd2.Save(file);
        }

    public static string GetXml(XElement node)
    {
        StringWriter sw = new StringWriter();
        XmlTextWriter xtw = new XmlTextWriter(sw);
        node.WriteTo(xtw);
        return sw.ToString();
    }

    public static string ReturnValueAllSubElementsSeparatedBy(XElement p, string deli)
    {
        StringBuilder sb = new StringBuilder();
        string xml = GetXml(p);
        MatchCollection mc = Regex.Matches(xml, "<(?:\"[^\"]*\"['\"]*|'[^']*'['\"]*|[^'\">])+>");
        List<string> nahrazeno = new List<string>();
        foreach (Match item in mc)
        {
            if (!nahrazeno.Contains(item.Value))
            {
                nahrazeno.Add(item.Value);
                xml = xml.Replace(item.Value, deli);
            }
        }
        sb.Append(xml);
        return sb.ToString().Replace(deli + deli, deli);
    }
}
