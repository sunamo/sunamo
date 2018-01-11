using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace sunamo.ObjectsCommon
{
    public class OuterObjectMapping  //: Dictionary<Type, string>
    {
        /// <summary>
        /// DB nemusí mít primární klíč, je to pouze značka toho že žádný jiný prvek v DB stejný primární klíč mít nemůže.
        /// </summary>
        public PropertyInfo primaryKey = null;
        public List<PropertyInfo> propertyInfos = new List<PropertyInfo>();
    }
}
