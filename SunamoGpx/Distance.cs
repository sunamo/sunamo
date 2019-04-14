using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace csGeoTools
{
    public abstract class Distance
    {
        protected static IDictionary<MetricUnit, Double> metricFactors = new Dictionary<MetricUnit, Double>()
        {
            { MetricUnit.meters, 1.0 },
            { MetricUnit.kilometers, 0.001 }
        };
        protected static IDictionary<ImperialUnit, Double> imperialFactors = new Dictionary<ImperialUnit, Double>()
        {
            { ImperialUnit.feet, 3.2808399 },
            { ImperialUnit.yards, 1.0936133 },
            { ImperialUnit.miles, 0.000621371192 }
        };

        public static Distance Between(GeoPoint p1, GeoPoint p2)
        {
            return new PointToPointDistance(p1, p2);
        }
        
        public static Distance Meters(Double meters)
        {
            return new FixedDistance(meters);
        }
        
        public static Distance Kilometers(Double kilometers)
        {
            return Distance.Of(kilometers, MetricUnit.kilometers);
        }
        
        public static Distance Feet(Double feet)
        {
            return Distance.Of(feet, ImperialUnit.feet);
        }
        
        public static Distance Yards(Double yards)
        {
            return Distance.Of(yards, ImperialUnit.yards);
        }
        
        public static Distance Miles(Double miles)
        {
            return Distance.Of(miles, ImperialUnit.miles);
        }
        
        public static Distance Of(Double amount, MetricUnit unit)
        {
            return new FixedDistance(amount / metricFactors[unit]);
        }
        
        public static Distance Of(Double amount, ImperialUnit unit)
        {
            return new FixedDistance(amount / imperialFactors[unit]);
        }
        
        protected abstract Double GetDistanceInMeters();
        
        public Distance AddDistance(Distance that)
        {
            return Distance.Meters(this.GetDistanceInMeters() + that.GetDistanceInMeters());
        }
        
        public Distance SubtractDistance(Distance that)
        {
            return Distance.Meters(this.GetDistanceInMeters() - that.GetDistanceInMeters());
        }
        
        public Double In(MetricUnit unit)
        {
            return GetDistanceInMeters() * metricFactors[unit];
        }
        
        public Double In(ImperialUnit unit)
        {
            return GetDistanceInMeters() * imperialFactors[unit];
        }
        
        override public bool Equals(Object other)
        {
            if (other == this) { return true; }
            if (!(other.GetType().BaseType.Equals(typeof(Distance)))) { return false; }
            return this.GetDistanceInMeters().Equals(((Distance) other).GetDistanceInMeters());
        }
    }
}
