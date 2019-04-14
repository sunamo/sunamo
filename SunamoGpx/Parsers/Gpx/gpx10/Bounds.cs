using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace csGeoTools.Parsers.gpx.gpx10
{
    [XmlTypeAttribute(Namespace = "http://www.topografix.com/GPX/1/0")]
    public class Bounds
    {
        [XmlAttribute("minlat")]
        public decimal MinLatitude { get; set; }
        [XmlAttribute("minlon")]
        public decimal MinLongitude { get; set; }
        [XmlAttribute("maxlat")]
        public decimal MaxLatitude { get; set; }
        [XmlAttribute("maxlon")]
        public decimal MaxLongitude { get; set; }
    }
}
