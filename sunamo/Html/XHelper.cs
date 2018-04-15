using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;


    public class XHelper
    {
        public static Dictionary<string, string> ns = new Dictionary<string, string>();

        public static string GetInnerXml(XElement parent)
        {
            var reader = parent.CreateReader();
            reader.MoveToContent();
            return reader.ReadInnerXml();
        }

        public static List<XElement> GetElementsOfNameWithAttr(System.Xml.Linq.XElement xElement, string tag, string attr, string value)
        {
            List<XElement> vr = new List<XElement>();
            List<XElement> e = XHelper.GetElementsOfNameRecursive(xElement, tag);
            foreach (XElement item in e)
            {
                if (XHelper.Attr(item, attr) == value)
                {
                    vr.Add(item);
                }
            }
            return vr;
        }

        /// <summary>
        /// Při nenalezení vrací null
        /// </summary>
        /// <param name="item"></param>
        /// <param name="attr"></param>
        /// <returns></returns>
        public static string Attr(XElement item, string attr)
        {
            XAttribute xa = item.Attribute(XName.Get(attr));
            if (xa != null)
            {
                return xa.Value;
            }
            return null;
        }

        public static List<XElement> GetElementsOfNameRecursive(XElement node, string nazev)
        {
            List<XElement> vr = new List<XElement>();
            string p, z;
            if (nazev.Contains(":"))
            {
                SH.GetPartsByLocation(out p, out z, nazev, ':');
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

        public static XElement GetElementOfNameWithAttr(XElement node, string nazev, string attr, string value)
        {
            string p, z;
            if (nazev.Contains(":"))
            {
                SH.GetPartsByLocation(out p, out z, nazev, ':');
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <param name="p"></param>
        /// <returns></returns>
        public static XElement GetElementOfNameRecursive(XElement node, string nazev)
        {
            string p, z;
            //bool ns = true;
            if (nazev.Contains(":"))
            {
                SH.GetPartsByLocation(out p, out z, nazev, ':');
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



        /// <summary>
        /// Získá element jména A2 v A1.
        /// Umí pracovat v NS, stačí zadat zkratku namepsace jako ns:tab
        /// </summary>
        /// <param name="node"></param>
        /// <param name="nazev"></param>
        /// <returns></returns>
        public static XElement GetElementOfName(XElement node, string nazev)
        {
            string p, z;
            if (nazev.Contains(":"))
            {
                SH.GetPartsByLocation(out p, out z, nazev, ':');
                p = XHelper.ns[p];
                foreach (XElement item in node.Elements())
                {

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

        public static string GetXml(XElement node)
        {
            StringWriter sw = new StringWriter();
            XmlWriter xtw = XmlWriter.Create(sw);
            node.WriteTo(xtw);
            return sw.ToString();
        }

        public static bool IsRightTag(XName xName, string nazev)
        {
            string p, z;

            SH.GetPartsByLocation(out p, out z, nazev, ':');
            p = XHelper.ns[p];
            if (xName.LocalName == z && xName.NamespaceName == p)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="p"></param>
        public static void AddXmlNamespaces(params string[] p)
        {
            for (int i = 0; i < p.Length; i++)
            {
                //.TrimEnd('/') + "/"
                ns.Add(p[i].Replace("xmlns:", ""), p[++i]);
            }
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
        /// <param name="var"></param>
        /// <param name="p"></param>
        /// <returns></returns>
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
