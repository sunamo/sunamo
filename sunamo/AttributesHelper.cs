using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace sunamo
{
    public class AttributesHelper
    {
        /// <summary>
        /// In A3 can be element SE - in input will add only SE
        /// A4 can be null - its for translate element in A3 to other
        /// </summary>
        /// <param name="car"></param>
        /// <param name="fields"></param>
        /// <param name="basic"></param>
        public static List<object> DataMember(object car, List<FieldInfo> fields, List<string> basic, Dictionary<string, string> dict)
        {
            List<object> result = new List<object>();
            foreach (var item in basic)
            {
                if (item == string.Empty)
                {
                    result.Add(string.Empty);
                }
                else
                {
                    var i = item;
                    if (dict != null)
                    {
                        i = dict[item];
                    }
                    result.Add(fields.Where(d => d.Name == i).First().GetValue(car));
                }
            }
            return result;
        }
    }
}