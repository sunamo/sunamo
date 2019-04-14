using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace csGeoTools.CommonModels
{
    [DataContract]
    public class Tag : IEquatable<Tag>
    {
        [DataMember]
        public virtual int Id { get; set; }
        [DataMember]
        public virtual String Text { get; set; }
        [DataMember]
        public virtual TagType Type { get; set; }

        public Tag() { }

        public virtual new bool Equals(Tag other)
        {
            return (this.Type == other.Type && this.Text.Equals(other.Text));
        }
    }
}
