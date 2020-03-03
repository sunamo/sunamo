using System;
using System.Collections.Generic;
using System.Text;

namespace sunamo.Attributes
{
    public class DataMemberAttribute : Attribute
    {
        public string Name;

        public DataMemberAttribute(string Name)
        {
            this.Name = Name;
        }
    }
}