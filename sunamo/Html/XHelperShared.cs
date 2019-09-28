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
public static Dictionary<string, string> XmlNamespaces(XmlNamespaceManager nsmgr)
    {
        Dictionary<string, string> ns = new Dictionary<string, string>();
        foreach (string item in nsmgr)
        {
            // Jaký je typ item, at nemusím používat slovník
            var v = nsmgr.LookupNamespace(item);
            if (!ns.ContainsKey(item))
            {
                ns.Add(item, v);
            }
        }

        return ns;
    }
}