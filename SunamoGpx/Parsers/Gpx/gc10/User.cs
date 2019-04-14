using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace csGeoTools.Parsers.gpx.gc10
{
    [XmlTypeAttribute(Namespace = "http://www.groundspeak.com/cache/1/0")]
    public class User
    {
        [XmlTextAttribute()]
        public String Name { get; set; }
        [XmlAttribute("id")]
        public String Id { get; set; }
    }
}
