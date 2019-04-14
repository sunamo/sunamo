using csGeoTools.ModelsBase;
using csGeoTools.Parsers.gpx.gpx10;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace csGeoTools.CommonModels
{
    [DataContract]
    public class Geocache : GeocacheBase
    {
        [DataMember]
        public virtual String Id { get; set; }
        [DataMember]
        public virtual String Name { get; set; }
        [DataMember]
        public virtual GeoPoint Location { get; set; }
        [DataMember]
        public virtual User Owner { get; set; }
        [DataMember]
        public virtual String Url { get; set; }
        [DataMember]
        public virtual IList<Waypoint> Waypoints { get; set; }
        [DataMember]
        public virtual IList<Tag> Tags { get; set; }
        [DataMember]
        public virtual CacheSize Size { get; set; }
        [DataMember]
        public virtual Double Difficulty { get; set; }
        [DataMember]
        public virtual Double Terrain { get; set; }
        [DataMember]
        public virtual String Description { get; set; }
        [DataMember]
        public virtual String EncodedHint { get; set; }
        [DataMember]
        public virtual IList<Log> Logs { get; set; }
        [DataMember]
        public virtual IList<Travelbug> Travelbugs { get; set; }
        [DataMember]
        public virtual bool AlreadyFound { get; set; }
        [DataMember]
        public virtual bool Archived { get; set; }
        [DataMember]
        public virtual bool Disabled { get; set; }

        public Geocache()
        {
            Tags = new List<Tag>();
        }

        public static List<Geocache> Parse(Gpx gpx)
        {
            List<Geocache> caches = new List<Geocache>();

            foreach (var item in gpx.Waypoints)
            {
                foreach (var singleCache in item.Caches)
                {
                    Geocache cache = new Geocache();
                    cache.Id = item.Name;
                    cache.Description = item.Description;
                    cache.Difficulty = singleCache.Difficulty;
                    cache.Terrain = singleCache.Terrain;
                    cache.AlreadyFound = false;
                    cache.Archived = singleCache.Archived;
                    cache.Disabled = singleCache.Available;
                    cache.EncodedHint = singleCache.EncodedHints;
                    cache.Location = GeoPoint.Parse((double)item.Latitude, (double)item.Longitude);
                    cache.Logs = Log.Parse(singleCache.Logs);
                    cache.Name = singleCache.Name;
                    cache.Owner = new User()
                    {
                        Name = singleCache.Owners.First().Name,
                        //Id = int.Parse(singleCache.Owners.First().Id)
                    };
                    cache.Size = (CacheSize)Enum.Parse(typeof(CacheSize), singleCache.Container, true);
                    cache.Travelbugs = Travelbug.Parse(singleCache.Travelbugs);
                    cache.Url = item.Url;
                    foreach (var attribute in singleCache.AttributesField)
                    {
                        cache.Tags.Add(new Tag()
                        {
                            Type = TagType.Attribute,
                            Text = (attribute.Inc == 1) ? attribute.Value : "not " + attribute.Value
                        });
                    }
                    cache.Tags.Add(new Tag() { Type = TagType.System, Text = singleCache.Type });
                    caches.Add(cache);
                }
            }
            return caches;
        }
    }
}
