using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace sunamo
{
    public class RH
    {
        public static object GetPropertyOfName(string name, Type type, object instance, bool ignoreCase)
        {
            PropertyInfo[] pis = type.GetProperties();
            if (ignoreCase)
            {
                name = name.ToLower();
                foreach (PropertyInfo item in pis)
                {
                    if (item.Name.ToLower() == name)
                    {
                        return type.GetProperty(name).GetValue(instance);
                    }
                }
            }
            else
            {
                foreach (PropertyInfo item in pis)
                {
                    if (item.Name == name)
                    {
                        return type.GetProperty(name).GetValue(instance);
                    }
                }
            }
            return null;
        }

        public static List<FieldInfo> GetConstants(Type type)
        {
            // | BindingFlags.FlattenHierarchy
            FieldInfo[] fieldInfos = type.GetFields(BindingFlags.Public |
                 BindingFlags.Static);

            return fieldInfos.Where(fi => fi.IsLiteral && !fi.IsInitOnly).ToList();
        }

        public static string FullNameOfMethod(MethodInfo mi)
        {
            return mi.DeclaringType.FullName + mi.Name;
        }

        public static string FullNameOfClassEndsDot(Type v)
        {
            return v.FullName + ".";
        }
    }
}
