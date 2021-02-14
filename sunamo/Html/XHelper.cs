using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using sunamo.Essential;

/// <summary>
/// XH = XmlElement
/// XHelper = XElement
/// </summary>
public partial class XHelper
{
    
    public static string GetInnerXml(XElement parent)
    {
        var reader = parent.CreateReader();
        reader.MoveToContent();
        return reader.ReadInnerXml();
    }



    public static List<XElement> GetElementsOfNameWithAttr(System.Xml.Linq.XElement xElement, string tag, string attr, string value, bool caseSensitive)
    {
        return GetElementsOfNameWithAttrWorker(xElement, tag, attr, value, false, caseSensitive);
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name = "item"></param>
    /// <param name = "p"></param>
    public static XElement GetElementOfNameRecursive(XElement node, string nazev)
    {
        string p, z;
        //bool ns = true;
        if (nazev.Contains(AllStrings.colon))
        {
            SH.GetPartsByLocation(out p, out z, nazev, AllChars.colon);
            p = XHelper.ns[p];
            foreach (XElement item in node.DescendantsAndSelf())
            {
                if (item.Name.LocalName == z && item.Name.NamespaceName == p)
                {
                    return item;
                }
            }
        }
        else
        {
            foreach (XElement item in node.DescendantsAndSelf())
            {
                if (item.Name.LocalName == nazev)
                {
                    return item;
                }
            }
        }

        return null;
    }

    /// <summary>
    /// Is usage only in _Uap/SocialNetworksManager -> open for find out how looks input data and then move to RegexHelper
    /// </summary>
    /// <param name = "p"></param>
    /// <param name = "deli"></param>
    public static string ReturnValueAllSubElementsSeparatedBy(XElement p, string deli)
    {
        StringBuilder sb = new StringBuilder();
        string xml = XHelper.GetXml(p);
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

    public static string GetXml(XElement node)
    {
        StringWriter sw = new StringWriter();
        XmlWriter xtw = XmlWriter.Create(sw);
        node.WriteTo(xtw);
        return sw.ToString();
    }



    public static XElement GetElementOfSecondLevel(XElement var, string first, string second)
    {
        XElement f = var.Element(XName.Get(first));
        if (f != null)
        {
            XElement s = f.Element(XName.Get(second));
            return s;
        }

        return null;
    }

    public static string GetValueOfElementOfSecondLevelOrSE(XElement var, string first, string second)
    {
        XElement xe = GetElementOfSecondLevel(var, first, second);
        if (xe != null)
        {
            return xe.Value.Trim();
        }

        return "";
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name = "var"></param>
    /// <param name = "p"></param>
    public static string GetValueOfElementOfNameOrSE(XElement var, string nazev)
    {
        XElement xe = GetElementOfName(var, nazev);
        if (xe == null)
        {
            return "";
        }

        return xe.Value.Trim();
    }
}