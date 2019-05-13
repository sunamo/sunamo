using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

public class XmlNamespacesHolder
{
    //public NameTable nt = new NameTable();
    public XmlNamespaceManager nsmgr = null;

    public XmlDocument ParseAndRemoveNamespaces(string content)
    {
        XmlDocument xd = new XmlDocument();
        

        ParseAndRemoveNamespaces(content, xd.NameTable);

        return xd;
    }

    public XmlDocument ParseAndRemoveNamespaces(string content, XmlNameTable nt)
    {
        XmlDocument xd = new XmlDocument();
        xd.LoadXml(content);

        nsmgr = new XmlNamespaceManager(nt);

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
                string key = string.Empty;
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
                    root.Attributes.RemoveAt(i);
                }
            }
            int i2 = 0;
        }

        return xd;
    }
}

