using csGeoTools.Vincenty;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace csGeoTools.Parsers
{
    public class DecimalDegreeParser : GeoPointParser
    {
        protected Regex coordinatePattern = new Regex("^(-?\\d+?\\.\\d+)°? ?,? ?(-?\\d+?\\.\\d+)°?$");
        
        override public bool CanParse(String input)
        {
            return coordinatePattern.IsMatch(input);
        }

        override public GeoPoint Parse(String input, Ellipsoid ellipsoid)
        {
            Match match = coordinatePattern.Match(input);
            Double latitude = Double.Parse(match.Groups[1].Value, CultureInfo.InvariantCulture) % 90;
            Double longitude = Double.Parse(match.Groups[2].Value, CultureInfo.InvariantCulture) % 180;
            return GeoPoint.Parse(latitude, longitude, ellipsoid);
        }
    }
}
