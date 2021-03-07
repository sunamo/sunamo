using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace SunamoCode
{
    public static partial class XmlLocalisationInterchangeFileFormat
    {
        public static string Id(XElement item)
        {
            return XHelper.Attr(item, "id");
        }
    }
}
