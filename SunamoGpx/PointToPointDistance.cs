using csGeoTools.Utilites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace csGeoTools
{
    public class PointToPointDistance : Distance
    {
        protected GeoPoint p1;
        protected GeoPoint p2;
        
        public PointToPointDistance(GeoPoint p1, GeoPoint p2)
        {
            this.p1 = p1;
            this.p2 = p2.ConvertTo(p1.ReferenceEllipsoid);
        }
        
        protected Double GetLatitudeDistance()
        {
            return MathUtilities.ToRadians(p2.Latitude - p1.Latitude);
        }
        
        protected Double GetLongitudeDistance()
        {
            return MathUtilities.ToRadians(p2.Longitude - p1.Longitude);
        }
        
        protected Double GetHaversineDistance()
        {
            Double a = Math.Pow(Math.Sin(GetLatitudeDistance() / 2.0), 2) +
				Math.Pow(Math.Sin(GetLongitudeDistance() / 2.0), 2) *
				Math.Cos(p1.LatitudeRadians) * Math.Cos(p2.LatitudeRadians);
            Double angularDistance = 2.0 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1.0 - a));
            return GeoConstants.EARTH_RADIUS * angularDistance;
        }

        override protected Double GetDistanceInMeters()
        {
            return GetHaversineDistance();
        }
        
        public GeoPoint Midpoint()
        {
            Double bx = Math.Cos(p2.LatitudeRadians) * Math.Cos(GetLongitudeDistance());
            Double by = Math.Cos(p2.LatitudeRadians) * Math.Sin(GetLongitudeDistance());

            Double latitude = Math.Atan2(Math.Sin(p1.LatitudeRadians) + Math.Sin(p2.LatitudeRadians),
                Math.Sqrt(Math.Pow(Math.Cos(p1.LatitudeRadians) + bx, 2) + Math.Pow(by, 2)));

            Double longitude = p1.LongitudeRadians + Math.Atan2(by, Math.Cos(p1.LatitudeRadians) + bx);

            longitude = (longitude + 3 * Math.PI) % (2 * Math.PI) - Math.PI;

            return GeoPoint.Parse(MathUtilities.ToDegrees(latitude), MathUtilities.ToDegrees(longitude), p1.ReferenceEllipsoid);
        }
    }
}
