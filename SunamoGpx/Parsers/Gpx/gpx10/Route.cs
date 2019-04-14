using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace csGeoTools.Parsers.gpx.gpx10
{
    [XmlTypeAttribute(Namespace = "http://www.topografix.com/GPX/1/0")]
    public class Route
    {
        [XmlElement("name")]
        public string Name { get; set; }
        [XmlElement("cmt")]
        public string Comment { get; set; }
        [XmlElement("desc")]
        public string Description { get; set; }
        [XmlElement("src")]
        public string Source { get; set; }
        [XmlElement("url")]                        // , DataType = "anyURI"
        public string Url { get; set; }
        [XmlElement("urlname")]
        public string Urlname { get; set; }
        [XmlElement("number")]                     // , DataType="nonNegativeInteger"
        public int Number { get; set; }

        //[System.Xml.Serialization.XmlAnyElementAttribute()]
        //public System.Xml.XmlElement[] Any { get; set; }
        [XmlArrayItemAttribute("rtept", typeof(Waypoint))]
        public Waypoint[] RouteParts { get; set; }
    }
}
