using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace csGeoTools.Parsers.gpx.gc10
{
    [XmlTypeAttribute(Namespace = "http://www.groundspeak.com/cache/1/0")]
    public class Description
    {
        [XmlAttribute("html")]
        public String _html { get; set; }
        public bool Html
        {
            get
            {
                return _html.Equals(true.ToString());
            }
            set
            {
                _html = value.ToString();
            }
        }
        [XmlTextAttribute()]
        public String value { get; set; }
        public String Value
        {
            get
            {
                return this.value;
            }
            set
            {
                this.value = value;
            }
        }
    }
}
