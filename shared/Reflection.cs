using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace sunamo
{
    public class Reflection
    {
        public void CopyProperties(object source, object target)
        {
            Type typeB = target.GetType();
            foreach (PropertyInfo property in source.GetType().GetProperties())
            {
                if (!property.CanRead || (property.GetIndexParameters().Length > 0))
                    continue;

                PropertyInfo other = typeB.GetProperty(property.Name);
                if ((other != null) && (other.CanWrite))
                    other.SetValue(target, property.GetValue(source, null), null);
            }
        }

        public static string FullNameOfExecutedCode(MethodBase method)
        {
            string methodName = method.Name;
            string type = method.ReflectedType.Name;
            return SH.ConcatIfBeforeHasValue(type, ".", methodName, ":");
        }
    }
}
