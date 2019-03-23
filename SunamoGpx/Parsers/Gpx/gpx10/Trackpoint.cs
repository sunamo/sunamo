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
    public class Trackpoint
    {
        [XmlElement("ele")]
        public decimal Elevation { get; set; }
        //[XmlIgnoreAttribute()]
        //public bool eleSpecified { get; set; }
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
        //[System.Xml.Serialization.XmlIgnoreAttribute()]
        //public bool timeSpecified { get; set; }
        [XmlElement("course")]
        public decimal Course { get; set; }
        [XmlElement("speed")]
        public decimal Speed { get; set; }
        [XmlElement("magvar")]
        public decimal Magvar { get; set; }
        //[XmlIgnoreAttribute()]
        //public bool magvarSpecified { get; set; }
        [XmlElement("geoidheight")]
        public decimal GeoIdHeight { get; set; }
        //[System.Xml.Serialization.XmlIgnoreAttribute()]
        //public bool geoidheightSpecified { get; set; }

        [XmlElement("name")]
        public String Name { get; set; }
        [XmlElement("cmt")]
        public String Comment { get; set; }
        [XmlElement("desc")]
        public String Description { get; set; }
        [XmlElement("src")]
        public String Source { get; set; }
        [XmlElement("url")]                        // , DataType = "anyURI"
        public String Url { get; set; }
        [XmlElement("urlname")]
        public String Urlname { get; set; }
        [XmlElement("sym")]
        public String Sym { get; set; }
        [XmlElement("type")]
        public String Type { get; set; }
        [XmlElement("fix")]
        public FixType Fix { get; set; }
        //[System.Xml.Serialization.XmlIgnoreAttribute()]
        //public bool fixSpecified { get; set; }
        [XmlElement("sat")]                        // , DataType = "nonNegativeInteger"
        public int Satellite { get; set; }
        [XmlElement("hdop")]
        public decimal HorizontalDop { get; set; }
        //[System.Xml.Serialization.XmlIgnoreAttribute()]
        //public bool hdopSpecified { get; set; }
        [XmlElement("vdop")]
        public decimal VerticalDop { get; set; }
        //[System.Xml.Serialization.XmlIgnoreAttribute()]
        //public bool vdopSpecified { get; set; }
        [XmlElement("pdop")]
        public decimal PDop { get; set; }
        //[System.Xml.Serialization.XmlIgnoreAttribute()]
        //public bool pdopSpecified { get; set; }
        [XmlElement("ageofdgpsdata")]
        public decimal AgeOfDGpsData { get; set; }
        //[System.Xml.Serialization.XmlIgnoreAttribute()]
        //public bool ageofdgpsdataSpecified { get; set; }
        [XmlElement("dgpsid")]                     // , DataType = "integer"
        public int DifferentialGpsId { get; set; }
        //[System.Xml.Serialization.XmlAnyElementAttribute()]
        //public System.Xml.XmlElement[] Any { get; set; }
        [XmlAttribute("lat")]
        public decimal Latitude { get; set; }
        [XmlAttribute("lon")]
        public decimal Longitude { get; set; }
    }
}
