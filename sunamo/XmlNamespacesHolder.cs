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
    
    public XDocument ParseAndRemoveNamespacesXDocument(string content, XmlNameTable nt, string defaultPrefix = "x")
    {
        var xd = ParseAndRemoveNamespacesXmlDocument(content, nt, defaultPrefix);
        return new XDocument(xd.OuterXml);
    }
}

