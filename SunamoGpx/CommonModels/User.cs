using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace csGeoTools.CommonModels
{
    [DataContract]
    public class User
    {
        [DataMember]
        public virtual int Id { get; set; }
        [DataMember]
        public virtual String Name { get; set; }
        [DataMember]
        public virtual String Password { get; set; }

        public User() { }
    }
}
