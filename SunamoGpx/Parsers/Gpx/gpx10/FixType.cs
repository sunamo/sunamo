using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace csGeoTools.Parsers.gpx.gpx10
{
    [XmlTypeAttribute(Namespace = "http://www.topografix.com/GPX/1/0")]
    public enum FixType
    {
        [XmlEnumAttribute("none")]
        None,
        [XmlEnumAttribute("2d")]
        Fix2d,
        [XmlEnumAttribute("3d")]
        Fix3d,
        [XmlEnumAttribute("dgps")]
        DifferentialGps,
        [XmlEnumAttribute("pps")]
        Military
    }
}
