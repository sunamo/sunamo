using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace sunamo.Html
{
    public class HtmlAssistant
    {
        public static string GetValueOfAttribute(string p, HtmlNode divMain, bool trim = false)
        {
            object o = divMain.Attributes[p];
            if (o != null)
            {
                string st = ((HtmlAttribute)o).Value;
                if (trim)
                {
                    st = st.Trim();
                }
                return st;
            }
            return "";
        }

        public static void SetAttribute(HtmlNode node, string atr, string hod)
        {

            object o = null;
            while (true)
            {
                o = node.Attributes.FirstOrDefault(a => a.Name == atr);
                if (o != null)
                {
                    node.Attributes.Remove((HtmlAttribute)o);
                }
                else
                {
                    break;
                }
            }
            node.Attributes.Add(atr, hod);
        }

        public static string InnerText(HtmlNode node, bool recursive, string tag, string attr, string attrValue)
        {
            var node2 = HtmlAgilityHelper.NodeWithAttr(node, true, tag, attr, attrValue);
            if (node2 != null)
            {
                return HtmlAssistant.HtmlDecode( node2.InnerText.Trim());
            }
            return string.Empty;
        }

        private static string HtmlDecode(string v)
        {
            return HttpUtility.HtmlDecode(v);
        }
    }
}
