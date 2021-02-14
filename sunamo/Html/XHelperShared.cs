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
public partial class XHelper{

    public static Dictionary<string, string> ns = new Dictionary<string, string>();
    public static Dictionary<string, string> XmlNamespaces(XmlNamespaceManager nsmgr, bool withPrexixedXmlnsColon)
    {
        Dictionary<string, string> ns = new Dictionary<string, string>();
        foreach (string item2 in nsmgr)
        {
            var item = item2;

            if (withPrexixedXmlnsColon)
            {
                if (item == string.Empty || item == "xmlns")
                {
                    item = "xmlns";
                }
                else
                {
                    item = "xmlns:" + item;
                }
                
            }

            // Jaký je typ item, at nemusím používat slovník
            var v = nsmgr.LookupNamespace(item2);

            if (!ns.ContainsKey(item))
            {
                ns.Add(item, v);
            }
        }

        return ns;
    }

    public static string InnerTextOfNode(XElement xe, string v)
    {
        var desc = xe.Descendants(XName.Get(v));
        if (desc.Count() == 0)
        {
            return string.Empty;
        }
        var first = desc.First();
        return first.Value;
    }

    /// <summary>
    /// Při nenalezení vrací null
    /// </summary>
    /// <param name = "item"></param>
    /// <param name = "attr"></param>
    public static string Attr(XElement item, string attr)
    {
        XAttribute xa = item.Attribute(XName.Get(attr));
        if (xa != null)
        {
            return xa.Value;
        }

        return null;
    }

public static XElement GetElementOfNameWithAttr(XElement node, string nazev, string attr, string value)
    {
        string p, z;
        if (nazev.Contains(AllStrings.colon))
        {
            SH.GetPartsByLocation(out p, out z, nazev, AllChars.colon);
            p = XHelper.ns[p];
            foreach (XElement item in node.Elements())
            {
                if (item.Name.LocalName == z && item.Name.NamespaceName == p)
                {
                    if (Attr(item, attr) == value)
                    {
                        return item;
                    }
                }
            }
        }
        else
        {
            foreach (XElement item in node.DescendantsAndSelf())
            {
                if (item.Name.LocalName == nazev)
                {
                    if (Attr(item, attr) == value)
                    {
                        return item;
                    }
                }
            }
        }

        return null;
    }

public static XElement MakeAllElementsWithDefaultNs(XElement settings)
    {
        var ns2 = XHelper.ns[string.Empty];
        List<object> toInsert = new List<object>();
        // shift ALL elements in the settings document into the target namespace
        foreach (XElement e in settings.DescendantsAndSelf())
        {
            //e.Name =  e.Name.LocalName;
            e.Name = XName.Get(e.Name.LocalName, ns2);
        }

        //foreach (var e in settings.Attributes())
        //{
        //    //e.Name = XName.Get(e.Name.LocalName, ns2);
        //    toInsert.Add(e);
        //}
        //t
        var vr = new XElement(XName.Get(settings.Name.LocalName, ns2), settings.Attributes(), settings.Descendants());
        return vr;
    }

/// <summary>
    /// If A1 is file, output will be save to file and return null
    /// Otherwise return string
    /// </summary>
    /// <param name = "xml"></param>
    public static string FormatXml(string xml)
    {
        var xmlFormat = xml;
        if (FS.ExistsFile(xml))
        {
            xmlFormat = TF.ReadFile(xml);
        }

        XmlNamespacesHolder h = new XmlNamespacesHolder();
        XDocument doc =  h.ParseAndRemoveNamespacesXDocument(xmlFormat);

        
        var formatted = doc.ToString();
        formatted = SH.ReplaceAll2(formatted, string.Empty, " xmlns=\"\"");
        if (FS.ExistsFile(xml))
        {
            TF.SaveFile(formatted, xml);
            ThisApp.SetStatus(TypeOfMessage.Success, SunamoPageHelperSunamo.i18n(XlfKeys.ChangesSavedToFile));
            return null;
        }
        else
        {
            ThisApp.SetStatus(TypeOfMessage.Success, SunamoPageHelperSunamo.i18n(XlfKeys.ChangesSavedToClipboard));
            return formatted;
        }
    }

public static List<XElement> GetElementsOfName(XElement node, string nazev)
    {
        List<XElement> result = new List<XElement>();
        string p, z;
        if (nazev.Contains(AllStrings.colon))
        {
            foreach (XElement item in node.Elements())
            {
                if (IsRightTag(item, nazev))
                {
                    result.Add(item);
                }
            }
        }
        else
        {
            foreach (XElement item in node.Elements())
            {
                if (item.Name.LocalName == nazev)
                {
                    result.Add(item);
                }
            }
        }

        return result;
    }

public static IEnumerable<XElement> GetElementsOfNameWithAttrContains(XElement group, string tag, string attr, string value, bool caseSensitive = false)
    {
        return GetElementsOfNameWithAttrWorker(group, tag, attr, value, true, caseSensitive);
    }

public static void AddXmlNamespaces(XmlNamespaceManager nsmgr)
    {
        foreach (string item in nsmgr)
        {
            // Jaký je typ item, at nemusím používat slovník
            var v = nsmgr.LookupNamespace(item);
            if (!ns.ContainsKey(item))
            {
                ns.Add(item, v);
            }

            int o = 0;
        }
    }
/// <summary>
    /// 
    /// </summary>
    /// <param name = "p"></param>
    public static void AddXmlNamespaces(params string[] p)
    {
        for (int i = 0; i < p.Length; i++)
        {
            //.TrimEnd(AllChars.slash) + AllStrings.slash
            ns.Add(p[i].Replace("xmlns:", ""), p[++i]);
        }
    }
public static void AddXmlNamespaces(Dictionary<string, string> d)
    {
        foreach (var item in d)
        {
            ns.Add(item.Key, item.Value);
        }
    }

public static XDocument CreateXDocument(string contentOrFn)
    {
        if (FS.ExistsFile(contentOrFn))
        {
            contentOrFn = TF.ReadFile(contentOrFn);
        }

        var enB = BTS.ConvertFromUtf8ToBytes(contentOrFn);
        XDocument xd = null;
        using (MemoryStream oStream = new MemoryStream(enB.ToArray()))
            using (XmlReader oReader = XmlReader.Create(oStream))
            {
                xd = XDocument.Load(oReader);
            }

        return xd;
    }

/// <summary>
    /// Získá element jména A2 v A1.
    /// Umí pracovat v NS, stačí zadat zkratku namepsace jako ns:tab
    /// </summary>
    /// <param name = "node"></param>
    /// <param name = "nazev"></param>
    public static XElement GetElementOfName(XContainer node, string nazev)
    {
        string p, z;
        if (nazev.Contains(AllStrings.colon))
        {
            SH.GetPartsByLocation(out p, out z, nazev, AllChars.colon);
            p = XHelper.ns[p];
            foreach (XElement item in node.Elements())
            {
                if (IsRightTag(item, z, p))
                {
                }

                if (item.Name.LocalName == z && item.Name.NamespaceName == p)
                {
                    return item;
                }
            }
        }
        else
        {
            foreach (XElement item in node.Elements())
            {
                if (item.Name.LocalName == nazev)
                {
                    return item;
                }
            }
        }

        return null;
    }

public static bool IsRightTag(XElement xName, string nazev)
    {
        return IsRightTag(xName.Name, nazev);
    }
/// <summary>
    /// Will split A2 to LocalName and NamespaceName
    /// </summary>
    /// <param name = "xName"></param>
    /// <param name = "nazev"></param>
    public static bool IsRightTag(XName xName, string nazev)
    {
        string p, z;
        SH.GetPartsByLocation(out p, out z, nazev, AllChars.colon);
        p = XHelper.ns[p];
        if (xName.LocalName == z && xName.NamespaceName == p)
        {
            return true;
        }

        return false;
    }
public static bool IsRightTag(XElement xName, string localName, string namespaceName)
    {
        return IsRightTag(xName.Name, localName, namespaceName);
    }
/// <summary>
    /// Into A3 is passing shortcut
    /// </summary>
    /// <param name = "xName"></param>
    /// <param name = "localName"></param>
    /// <param name = "namespaceName"></param>
    public static bool IsRightTag(XName xName, string localName, string namespaceName)
    {
        string p, z;
        namespaceName = XHelper.ns[namespaceName];
        if (xName.LocalName == localName && xName.NamespaceName == namespaceName)
        {
            return true;
        }

        return false;
    }

public static List<XElement> GetElementsOfNameWithAttrWorker(System.Xml.Linq.XElement xElement, string tag, string attr, string value, bool enoughIsContainsAttribute, bool caseSensitive)
    {
        List<XElement> vr = new List<XElement>();
        List<XElement> e = XHelper.GetElementsOfNameRecursive(xElement, tag);
        foreach (XElement item in e)
        {
            var attrValue = XHelper.Attr(item, attr);
            if (SH.Contains(attrValue, value, enoughIsContainsAttribute, caseSensitive))
            {
                vr.Add(item);
            }
        }

        return vr;
    }

    public static List<XElement> GetElementsOfNameWithAttr(XElement hlavniCL, string v1, string v2, string v3)
    {
        return null;
    }

    public static List<XElement> GetElementsOfNameRecursive(XElement node, string nazev)
    {
        List<XElement> vr = new List<XElement>();
        string p, z;
        if (nazev.Contains(AllStrings.colon))
        {
            SH.GetPartsByLocation(out p, out z, nazev, AllChars.colon);
            p = XHelper.ns[p];
            foreach (XElement item in node.DescendantsAndSelf())
            {
                if (item.Name.LocalName == z && item.Name.NamespaceName == p)
                {
                    vr.Add(item);
                }
            }
        }
        else
        {
            foreach (XElement item in node.DescendantsAndSelf())
            {
                if (item.Name.LocalName == nazev)
                {
                    vr.Add(item);
                }
            }
        }

        return vr;
    }

    public static string Minify(string c)
    {
        c = c.Replace(Environment.NewLine, string.Empty);
        c = SH.ReplaceAllWhitecharsForSpace(c);
        c = SH.ReplaceAllDoubleSpaceToSingle(c);
        c = c.Replace("> <", "><");
        return c;
    }
}