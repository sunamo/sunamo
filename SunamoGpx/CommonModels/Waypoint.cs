using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace csGeoTools.CommonModels
{
    [DataContract]
    public class Waypoint
    {
        [DataMember]
        public virtual int Id { get; set; }
        [DataMember]
        public virtual GeoPoint Location { get; set; }
        [DataMember]
        public virtual List<Tag> Tags { get; set; }
        [DataMember]
        public virtual WaypointType Type { get; set; }

        public Waypoint() { }

        internal static List<Waypoint> Parse(Parsers.gpx.gc101.LogWaypoint[] logWaypoints)
        {
            List<Waypoint> waypoints = new List<Waypoint>();
            if (logWaypoints == null || logWaypoints.Count() == 0) return waypoints;
            foreach (var item in logWaypoints)
            {
                waypoints.Add(new Waypoint()
                {
                    Location = GeoPoint.Parse(Double.Parse(item.Latitude), Double.Parse(item.Longitude)),
                    Tags = new List<Tag>()
                    {
                        new Tag() { Text = "LogWaypoint", Type = TagType.System }
                    }
                });
            }
            return waypoints;
        }
    }
}
