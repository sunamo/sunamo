using csGeoTools.Vincenty;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace csGeoTools
{
    public class GeoConstants
    {
        // base unit for all computations is meters
        public const Double EARTH_RADIUS = 6371009.0;

        public static IDictionary<Ellipsoid, EllipsoidParameters> REFERENCE_ELLIPSOIDS = new Dictionary<Ellipsoid, EllipsoidParameters>()
        {
            { Ellipsoid.GRS80, new EllipsoidParameters(6378137.0, 6356752.314140, 298.257222101) },
            { Ellipsoid.WGS84, new EllipsoidParameters(6378137.0, 6356752.3142, 298.257223563) }
        };

        public const Ellipsoid DEFAULT_ELLIPSOID = Ellipsoid.WGS84;
    }
}