using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace csGeoTools.Parsers.gpx.gpx10
{
    [XmlTypeAttribute(Namespace = "http://www.topografix.com/GPX/1/0")]
    public class Track
    {
        [XmlElement("name")]
        public string Name { get; set; }
        [XmlElement("cmt")]
        public string Comment { get; set; }
        [XmlElement("desc")]
        public string Description { get; set; }
        [XmlElement("src")]
        public string Source { get; set; }
        [XmlElement("url")]
        public string Url { get; set; }
        [XmlElement("urlname")]
        public string Urlname { get; set; }
        [XmlElement("number")]
        public int Number { get; set; }
        [XmlArrayItemAttribute("trkpt", typeof(Trackpoint))]
        public Trackpoint[] TrackSegments { get; set; }
    }
}
