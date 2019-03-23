using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace csGeoTools.Parsers.gpx.gc101
{
    [XmlTypeAttribute(Namespace="http://www.groundspeak.com/cache/1/0/1")]
    public class Attribute
    {
        [XmlAttribute("id")]
        public string Id { get; set; }
        [XmlAttribute("inc")]
        public sbyte Inc { get; set; }
        [XmlTextAttribute()]
        public string Value { get; set; }
    }
}
