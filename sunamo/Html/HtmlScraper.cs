using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Text;

namespace sunamo.Html
{


    public class HtmlScraper
    {
        static StringBuilder sb = new StringBuilder();

        public static string AttributeValuesOfTag(HtmlNode hd, bool recursive, string tag, string attr)
        {
            var nodes = HtmlAgilityHelper.Nodes(hd, true, tag);
            foreach (var item in nodes)
            {
                sb.AppendLine(HtmlAssistant.GetValueOfAttribute(attr, item));
            }
            return sb.ToString();
        }

        public override string ToString()
        {
            return sb.ToString();
        }
    }
}
