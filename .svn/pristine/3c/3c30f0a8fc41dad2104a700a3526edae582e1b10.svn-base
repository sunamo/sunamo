using System;
using System.Text.RegularExpressions;
using System.Xml;

namespace sunamo.Xml
{
    public static class XH
    {
        public static string RemoveXmlDeclaration(string vstup)
        {
            vstup = Regex.Replace(vstup, @"<\?xml.*?\?>", "");
            vstup = Regex.Replace(vstup, @"<\?xml.*?\>", "");
            vstup = Regex.Replace(vstup, @"<\?xml.*?\/>", "");
            return vstup;
        }
    }
}
