using sunamo;
using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace apps
{
    public static class XElementExtensions
    {
        public static XElement XPathSelectElement(this XElement xe, string xpath)
        {
            if (!xpath.StartsWith("/"))
            {
                throw new Exception("Argument xpath in XElementExtensions.XPathSelectElement dont start with slash (/)");
            }

            string[] parts = SH.Split(xpath, "/");
            List<XPathPart> xpps = new List<XPathPart>();
            foreach (var part in parts)
            {
                xpps.Add(new XPathPart(part));
            }
            XElement actual = xe;
            foreach (var item in xpps)
            {
                if (actual == null)
                {
                    return null;
                }
                if (item.attName != null)
                {
                    actual = XHelper.GetElementOfNameWithAttr(actual, item.tag, item.attName, item.attValue);
                }
                else
                {
                    actual = XHelper.GetElementOfName(actual, item.tag);
                }
            }
            return actual;
        }
    }
}
