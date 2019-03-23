using csGeoTools.Parsers.gpx.gc101;
using csGeoTools.Parsers.Gpx;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace csGeoTools.Parsers.gpx.gpx10
{
    [XmlTypeAttribute(Namespace = "http://www.topografix.com/GPX/1/0")]
    public class Waypoint
    {
        [XmlElement("ele")]
        public decimal Elevation { get; set; }
        [XmlElement("time")]
        public String _time { get; set; }
        public DateTime Time
        {
            get
            {
                return DateTime.ParseExact(_time, DateFormats.KNOWN_FORMATS, DateTimeFormatInfo.InvariantInfo, DateTimeStyles.AdjustToUniversal);
            }
            set
            {
                _time = value.ToString(DateFormats.KNOWN_FORMATS.First());
            }
        }
        [XmlElement("magvar")]
        public decimal Magvar { get; set; }
        [XmlElement("geoidheight")]
        public decimal GeoIdHeight { get; set; }
        /// <summary>
        /// GC Code
        /// </summary>
        [XmlElement("name")]
        public String Name { get; set; }
        [XmlElement("cmt")]
        public String Comment { get; set; }
        [XmlElement("desc")]
        public String Description { get; set; }
        [XmlElement("src")]
        public String Source { get; set; }
        [XmlElement("url")]
        public String Url { get; set; }
        [XmlElement("urlname")]
        public String Urlname { get; set; }
        [XmlElement("sym")]
        public String Sym { get; set; }
        [XmlElement("type")]
        public String Type { get; set; }
        [XmlElement("fix")]
        public FixType Fix { get; set; }
        [XmlElement("sat")]
        public int Satellite { get; set; }
        [XmlElement("hdop")]
        public decimal HorizontalDop { get; set; }
        [XmlElement("vdop")]
        public decimal VerticalDop { get; set; }
        [XmlElement("pdop")]
        public decimal PDop { get; set; }
        [XmlElement("ageofdgpsdata")]
        public decimal AgeOfDGpsData { get; set; }
        [XmlElement("dgpsid")]
        public int DifferentialGpsId { get; set; }
        [XmlElement("cache", Namespace = "http://www.groundspeak.com/cache/1/0/1")]
        public Cache[] Caches { get; set; }
        public Cache Cache
        {
            get
            {
                return Caches.First();
            }
        }
        [XmlAttribute("lat")]
        public decimal Latitude { get; set; }
        [XmlAttribute("lon")]
        public decimal Longitude { get; set; }
    }
}
