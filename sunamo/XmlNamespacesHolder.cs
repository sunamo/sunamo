using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

public class XmlNamespacesHolder
{
    //public NameTable nt = new NameTable();
    public XmlNamespaceManager nsmgr = null;

    /// <summary>
    /// Return XmlDocument but dont use return value
    /// Just use XHelper class, because with XmlDocument is still not working
    /// </summary>
    /// <param name="content"></param>
    public XmlDocument ParseAndRemoveNamespacesXmlDocument(string content)
    {
        XmlDocument xd = new XmlDocument();

        xd = ParseAndRemoveNamespacesXmlDocument(content, xd.NameTable);

        return xd;
    }

    /// <summary>
    /// A3 is default prefix because cant be empty anytime (/:Tag or /Tag dont working but /prefix:Tag yes)
    /// Return XmlDocument but dont use return value
    /// Just use XHelper class, because with XmlDocument is still not working
    /// </summary>
    /// <param name="content"></param>
    /// <param name="nt"></param>
    /// <param name="defaultPrefix"></param>
    public XmlDocument ParseAndRemoveNamespacesXmlDocument(string content, XmlNameTable nt, string defaultPrefix = "x")
    {
        XmlDocument xd = new XmlDocument();

        /*
         * In default state have already three keys:
         * "" = ""
xmlns=http://www.w3.org/2000/xmlns/
xml=http://www.w3.org/XML/1998/namespace
         */
        nsmgr = new XmlNamespaceManager(nt);

        xd.LoadXml(content);

        foreach (XmlNode item in xd.ChildNodes)
        {
            if (item.NodeType == XmlNodeType.XmlDeclaration)
            {
                continue;
            }
            var root = item;
            for (int i = root.Attributes.Count - 1; i >= 0; i--)
            {
                var att = root.Attributes[i];
                //
                string key = defaultPrefix;
                if (att.Name.StartsWith("xmlns"))
                {
                    if (att.Name.Contains(":"))
                    {
                        key = att.Name.Substring(6);
                    }
                    else
                    {
                    }

                    nsmgr.AddNamespace(key, att.Value);
                    // TODO: Delete wrong attribute but in outerXml is still figuring
                    root.Attributes.RemoveAt(i);
                }
            }
            int i2 = 0;
        }

        var outer = xd.OuterXml;

        return xd;
    }

    /// <summary>
    /// Return XmlDocument but dont use return value
    /// Just use XHelper class, because with XmlDocument is still not working
    /// </summary>
    /// <param name="content"></param>
    public XDocument ParseAndRemoveNamespacesXDocument(string content)
    {
        var xd = ParseAndRemoveNamespacesXmlDocument(content);
        return  XDocument.Parse (xd.OuterXml);
    }

    /// <summary>
    /// A3 is default prefix because cant be empty anytime (/:Tag or /Tag dont working but /prefix:Tag yes)
    /// Return XmlDocument but dont use return value
    /// Just use XHelper class, because with XmlDocument is still not working
    /// </summary>
    /// <param name="content"></param>
    /// <param name="nt"></param>
    /// <param name="defaultPrefix"></param>
    public XDocument ParseAndRemoveNamespacesXDocument(string content, XmlNameTable nt, string defaultPrefix = "x")
    {
        var xd = ParseAndRemoveNamespacesXmlDocument(content, nt, defaultPrefix);
        return new XDocument(xd.OuterXml);
    }
}