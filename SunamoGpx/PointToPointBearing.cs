using csGeoTools.Utilites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace csGeoTools
{
    public class PointToPointBearing : Bearing
    {
        protected GeoPoint from;
        protected GeoPoint to;

        public PointToPointBearing(GeoPoint from, GeoPoint to)
        {
            this.from = from;
            this.to = to.ConvertTo(from.ReferenceEllipsoid);
        }
        
        private Double getBearing()
        {
            Double longitudeDistance = MathUtilities.ToRadians(to.Longitude - from.Longitude);
            Double x = Math.Cos(from.LatitudeRadians) * Math.Sin(to.LatitudeRadians) -
                Math.Sin(from.LatitudeRadians) * Math.Cos(to.LatitudeRadians) * Math.Cos(longitudeDistance);
            Double y = Math.Sin(longitudeDistance) * Math.Cos(to.LatitudeRadians);
            Double bearing = Math.Atan2(y, x);
            return (MathUtilities.ToDegrees(bearing) + 360) % 360;
        }
        
        override protected Double GetBearingInDecimalDegrees() {
            return getBearing();
        }
    }
}
