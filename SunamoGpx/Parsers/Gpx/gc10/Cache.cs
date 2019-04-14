using csGeoTools.ModelsBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace csGeoTools.Parsers.gpx.gc10
{
    [XmlTypeAttribute(Namespace = "http://www.groundspeak.com/cache/1/0")]
    public class Cache : GeocacheBase
    {
        [XmlElement("name")]
        public String Name { get; set; }
        [XmlElement("placed_by")]
        public String PlacedBy { get; set; }
        [XmlElement("owner")]
        public User[] Owners;
        public User Owner
        {
            get
            {
                return Owners.First();
            }
        }

        [XmlElement("type")]
        public String Type { get; set; }
        [XmlElement("container")]
        public String Container { get; set; }
        [XmlArray("attributes")]
        [XmlArrayItem("attribute")]
        public Attribute[] AttributesField { get; set; }
        [XmlElement("difficulty")]
        public Double Difficulty { get; set; }
        [XmlElement("terrain")]
        public Double Terrain { get; set; }
        [XmlElement("country")]
        public String Country { get; set; }
        [XmlElement("state")]
        public String State { get; set; }
        [XmlElement("short_description")]
        public Description[] ShortDescription { get; set; }
        [XmlElement("long_description")]
        public Description[] LongDescription { get; set; }
        [XmlElement("encoded_hints")]
        public String EncodedHints { get; set; }
        [XmlArray("logs")]
        [XmlArrayItem("log")]
        public Log[] Logs { get; set; }
        [XmlArray("travelbugs")]
        [XmlArrayItem("travelbug")]
        public Travelbug[] Travelbugs { get; set; }
        [XmlAttribute("id")]
        public String Id { get; set; }
        [XmlAttribute("available")]
        public String _available { get; set; }
        public bool Available
        {
            get
            {
                return _available.Equals(true.ToString());
            }
            set
            {
                _available = value.ToString();
            }
        }
        [XmlAttribute("archived")]
        public string _archived { get; set; }
        public bool Archived
        {
            get
            {
                return _archived.Equals(true.ToString());
            }
            set
            {
                _archived = value.ToString();
            }
        }
    }
}
