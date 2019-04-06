using System;
using System.Collections.Generic;
using System.Text;

namespace sunamo.Values
{
    public class AllLists
    {
        public static List<string> OstravaCityParts = null;
        public static List<string> HtmlNonPairTags = CA.ToListString("area", "base", "br", "col", "embed", "hr", "img", "input", "link", "meta", "param", "source", "track", "wbr");
        public static List<string> PairingTagsDontWrapToParagraph = CA.ToListString("h1", "h2", "h3", "h4", "h5", "h6", "ul", "ol", "li");
    }
}
