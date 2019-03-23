using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace csGeoTools
{
    public abstract class Bearing
    {
        public static Bearing Degrees(Double degrees)
        {
            return DecimalDegrees(degrees);
        }
        
        public static Bearing DecimalDegrees(Double degrees) 
        {
            return new FixedBearing(degrees);
        }
        
        public static Bearing Radians(Double radians)
        {
            return new FixedBearing(radians * (180.0 / Math.PI));
        }
        
        public static Bearing Between(GeoPoint p1, GeoPoint p2)
        {
            return new PointToPointBearing(p1, p2);
        }
        
        protected abstract Double GetBearingInDecimalDegrees();
        
        public Double Degrees()
        {
            return DecimalDegrees();
        }
        
        public Double DecimalDegrees()
        {
            return GetBearingInDecimalDegrees();
        }
        
        public Double Radians()
        {
            return (Math.PI * (GetBearingInDecimalDegrees() / 180.0));
        }
        
        override public bool Equals(Object other)
        {
            if (other == this) { return true; }
            if (!(other.GetType().BaseType.Equals(typeof(Bearing)))) { return false; }
            return this.GetBearingInDecimalDegrees().Equals(((Bearing) other).GetBearingInDecimalDegrees());
        }


    }
}
