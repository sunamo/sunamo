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
}