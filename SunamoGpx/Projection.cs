using csGeoTools.Utilites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace csGeoTools
{
    public class Projection
    {
        protected GeoPoint from;
        protected Distance distance;
        protected Bearing bearing;

        public Projection(GeoPoint from, Distance distance, Bearing heading)
        {
            this.from = from;
            this.distance = distance;
            this.bearing = heading;
        }

        public GeoPoint Project()
        {
            Double d = distance.In(MetricUnit.meters);
            Double dR = d / GeoConstants.EARTH_RADIUS;
            
            Double latitude = Math.Asin(Math.Sin(from.LatitudeRadians) *
                Math.Cos(dR) + Math.Cos(from.LatitudeRadians) *
                Math.Sin(dR) * Math.Cos(bearing.Radians()));
            
            Double longitude = from.LongitudeRadians + Math.Atan2(Math.Sin(bearing.Radians()) *
                Math.Sin(dR) * Math.Cos(from.LatitudeRadians),
				Math.Cos(dR) - Math.Sin(from.LatitudeRadians) *
                Math.Sin(latitude));
            
            longitude = (longitude + 3 * Math.PI) % (2 * Math.PI) - Math.PI;
            
            return GeoPoint.Parse(MathUtilities.ToDegrees(latitude), MathUtilities.ToDegrees(longitude), from.ReferenceEllipsoid);
        }
    }
}
