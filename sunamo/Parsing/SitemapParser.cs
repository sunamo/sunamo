using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

public class SitemapParser
{
    public static void ParseSitemap(string xml)
    {
        List<>

        XmlDocument urldoc = new XmlDocument();
        /*Load the downloaded string as XML*/
        urldoc.LoadXml(xml);
        /*Create an list of XML nodes from the url nodes in the sitemap*/
        XmlNodeList xnList = urldoc.GetElementsByTagName("url");
        /*Loops through the node list and prints the values of each node*/
        foreach (XmlNode node in xnList)
        {
            
        }
    }
}
}

