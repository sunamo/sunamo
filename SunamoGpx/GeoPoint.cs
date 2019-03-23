using csGeoTools.Parsers;
using csGeoTools.Utilites;
using csGeoTools.Vincenty;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace csGeoTools
{
    internal static class FNVHasher
    {
        private static readonly int offsetBasis = unchecked((int)2166136261);
        private static readonly int prime = 16777619;
        public static int CreateHash(params object[] objs)
        {
            return objs.Aggregate(offsetBasis, (r, o) => (r ^ o.GetHashCode()) * prime);
        }
    }

    [DataContract]
    public class GeoPoint
    {
        [DataMember]
        public virtual Double Latitude { get; protected set; }
        [DataMember]
        public virtual Double LatitudeRadians { get; protected set; }
        [DataMember]
        public virtual Double Longitude { get; protected set; }
        [DataMember]
        public virtual Double LongitudeRadians { get; protected set; }
        [DataMember]
        public virtual Ellipsoid ReferenceEllipsoid { get; protected set; }

        internal GeoPoint() { }
        
        private GeoPoint(Double latitude, Double longitude, Ellipsoid referenceEllipsoid)
        {
            this.Latitude = latitude;
            this.LatitudeRadians = MathUtilities.ToRadians(latitude);
            this.Longitude = longitude;
            this.LongitudeRadians = MathUtilities.ToRadians(longitude);
            this.ReferenceEllipsoid = referenceEllipsoid;
        }
        
        public static GeoPoint Parse(Double latitude, Double longitude)
        {
            return Parse(latitude, longitude, GeoConstants.DEFAULT_ELLIPSOID);
        }
        
        public static GeoPoint Parse(Double latitude, Double longitude, Ellipsoid referenceEllipsoid)
        {
            return new GeoPoint(latitude, longitude, referenceEllipsoid);
        }

        public static GeoPoint Parse(String coordinates)
        {
            return Parse(coordinates, GeoConstants.DEFAULT_ELLIPSOID);
        }
        
        public static GeoPoint Parse(String coordinates, Ellipsoid referenceEllipsoid)
        {
            GeoPointParser parser = GeoPointParser.getParserFor(coordinates);
            return parser.Parse(coordinates, referenceEllipsoid);
        }

        public virtual String As(GeoPointFormat format)
        {
            return As(format, CultureInfo.InvariantCulture);
        }

        public virtual String As(GeoPointFormat format, CultureInfo culture)
        {
            switch (format)
            {
                case GeoPointFormat.DD: return AsDd(culture);
                case GeoPointFormat.DecimalDegrees: return AsDd(culture);
                case GeoPointFormat.DM: return AsDm(culture);
                case GeoPointFormat.DecimalMinutes: return AsDm(culture);
                case GeoPointFormat.DMS: return AsDms(culture);
                case GeoPointFormat.DegreesMinutesSeconds: return AsDms(culture);
                default: return String.Empty;
            }
        }

        public virtual String AsDd()
        {
            return AsDd(CultureInfo.InvariantCulture);
        }

        public virtual String AsDd(CultureInfo culture)
        {
            string decimalFormat = "#.######"; // 6 decimal places = ~11cm precision
            return String.Format("{0}, {1}", 
                Latitude.ToString(decimalFormat, culture.NumberFormat), 
                Longitude.ToString(decimalFormat,culture.NumberFormat));
        }
        
        private String decimalToDm(Double coordinate, String degreeFormatPattern, CultureInfo culture)
        {
            coordinate = Math.Abs(coordinate);
            Double fractionalPart = coordinate % 1;
            Int32 degrees = (Int32)coordinate;
            coordinate = fractionalPart * 60;

            string minuteFormat = "00.000";
            return String.Format("{0}° {1}", 
                degrees.ToString(degreeFormatPattern, culture.NumberFormat), 
                coordinate.ToString(minuteFormat, culture.NumberFormat));
        }

        public virtual String AsDm()
        {
            return AsDm(CultureInfo.InvariantCulture);
        }

        public virtual String AsDm(CultureInfo culture)
        {
            String latitudeDirection = Latitude > 0 ? "N" : "S";
            String longitudeDirection = Longitude > 0 ? "E" : "W";
            return String.Format("{0}{1} {2}{3}",
                Latitude != 0 ? latitudeDirection : "", decimalToDm(Latitude, "00", culture),
                Longitude != 0 ? longitudeDirection : "", decimalToDm(Longitude, "000", culture));
        }
        
        private String decimalToDms(Double coordinate, CultureInfo culture)
        {
            coordinate = Math.Abs(coordinate);
            Double fractionalPart = coordinate % 1;
            String degrees = ((int)coordinate).ToString();
            coordinate = fractionalPart * 60;
            fractionalPart = coordinate % 1;
            String minutes = ((int)coordinate).ToString();
            coordinate = fractionalPart * 60;
            string decimalFormat = "0.###";
            return String.Format("{0}° {1}' {2}\"", degrees, minutes, coordinate.ToString(decimalFormat, culture.NumberFormat));
        }

        public virtual String AsDms()
        {
            return AsDms(CultureInfo.InvariantCulture);
        }

        public virtual String AsDms(CultureInfo culture)
        {
            String latitudeDirection = Latitude > 0 ? "N" : "S";
            String longitudeDirection = Longitude > 0 ? "E" : "W";
            return String.Format("{0}{1} {2}{3}",
                Latitude != 0 ? latitudeDirection : "", decimalToDms(Latitude, culture),
                Longitude != 0 ? longitudeDirection : "", decimalToDms(Longitude, culture));
        }

        public virtual GeoPoint ConvertTo(Ellipsoid referenceEllipsoid)
        {
            if (this.ReferenceEllipsoid == referenceEllipsoid)
            {
                return this;
            }
            throw new NotImplementedException("Conversion between different reference ellipsoids is not yet implemented");
        }

        public virtual Bearing BearingTo(GeoPoint that)
        {
		    return Bearing.Between(this, that);
	    }

        public virtual Bearing InitialBearingTo(GeoPoint that)
        {
		    return BearingTo(that);
	    }

        public virtual Bearing FinalBearingTo(GeoPoint that)
        {
		    return Bearing.DecimalDegrees((Bearing.Between(that, this).DecimalDegrees() + 180) % 360);
	    }

        public virtual Distance DistanceTo(GeoPoint that)
        {
            return Distance.Between(this, that);
        }

        public virtual GeoPoint MidpointTo(GeoPoint that)
        {
            return ((PointToPointDistance) Distance.Between(this, that)).Midpoint();
        }
        
        public virtual GeoPoint Project(Distance distance, Bearing bearing)
        {
            return new Projection(this, distance, bearing).Project();
        }
        
        override public bool Equals(Object other)
        {
            if (other == this) { return true; }
            if (!(other.GetType().Equals(this.GetType()))) { return false; }
            GeoPoint d = (GeoPoint) other;
            return this.ReferenceEllipsoid.Equals(d.ReferenceEllipsoid)
                && this.Latitude.Equals(d.Latitude) && this.Longitude.Equals(d.Longitude);
        }

        public override int GetHashCode()
        {
            return FNVHasher.CreateHash(Latitude, Longitude, ReferenceEllipsoid);
        }

        public virtual int GetHashCode(Double latitude, Double longitude, Ellipsoid ellipsoid = GeoConstants.DEFAULT_ELLIPSOID)
        {
            return FNVHasher.CreateHash(latitude, longitude, ellipsoid);
        }
    }
}