using csGeoTools.Parsers.Gpx;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace csGeoTools.Parsers.gpx.gc10
{
    [XmlTypeAttribute(Namespace = "http://www.groundspeak.com/cache/1/0")]
    public class Log
    {
        [XmlElement("date")]
        public String _date { get; set; }
        public DateTime Date
        {
            get
            {
                return DateTime.ParseExact(_date, DateFormats.KNOWN_FORMATS, DateTimeFormatInfo.InvariantInfo, DateTimeStyles.AdjustToUniversal);
            }
            set
            {
                _date = value.ToString(DateFormats.KNOWN_FORMATS.First());
            }
        }
        [XmlElement("type")]
        public String Type { get; set; }
        [XmlElement("finder")]
        public User[] Finder { get; set; }
        [XmlElement("text")]
        public LogText[] Text { get; set; }
        [XmlElement("log_wpt")]
        public LogWaypoint[] LogWayPoints { get; set; }
        [XmlAttribute("id")]
        public String Id { get; set; }
    }

    [XmlTypeAttribute(Namespace = "http://www.groundspeak.com/cache/1/0/1")]
    public class LogWaypoint
    {
        [XmlAttribute("lat")]
        public String Latitude { get; set; }
        [XmlAttribute("lon")]
        public String Longitude { get; set; }
	}

    [XmlTypeAttribute(Namespace = "http://www.groundspeak.com/cache/1/0/1")]
    public class LogText
    {
        [XmlTextAttribute()]
        public String value { get; set; }
		[XmlAttribute("encoded")]
        public String _encoded { get; set; }
		public bool IsEncoded
        {
            get
            {
                return _encoded.Equals(true.ToString());
            }
            set
            {
                _encoded = value.ToString();
            }
        }
	}
}
