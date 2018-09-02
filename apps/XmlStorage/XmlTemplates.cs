using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace apps
{
    public class XmlTemplates
    {
        public static string GetXml2(string prvni, string druhy)
        {
            return "<sunamo><prvni><![CDATA[" + prvni + "]]></prvni><druhy><![CDATA[" + druhy + "]]></druhy></sunamo>";
        }
    }
}
