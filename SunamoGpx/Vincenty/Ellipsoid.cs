using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace csGeoTools.Vincenty
{
    public enum Ellipsoid
    {
        /// <summary>
        /// Clarke 1866 / North American Datum of 1927
        /// </summary>
        NAD27,
        /// <summary>
        /// Krasovsky 1940
        /// </summary>
        SK42,
        GRS80,
        WGS84
    }
}
