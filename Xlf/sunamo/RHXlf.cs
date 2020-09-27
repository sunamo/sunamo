using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

public class RHXlf
{
    #region For easy copy
    public static object GetValueOfProperty(string name, Type type, object instance, bool ignoreCase)
    {
        PropertyInfo[] pis = type.GetProperties();
        return GetValue(name, type, instance, pis, ignoreCase, null);
    }

    public static object SetValueOfProperty(string name, Type type, object instance, bool ignoreCase, object v)
    {
        PropertyInfo[] pis = type.GetProperties();
        return SetValue(name, type, instance, pis, ignoreCase, v);
    }

    private static object SetValue(object instance, MemberInfo[] property, object v)
    {
        var val = property[0];
        if (val is PropertyInfo)
        {
            var pi = (PropertyInfo)val;
            pi.SetValue(instance, v);
        }
        else if (val is FieldInfo)
        {
            var pi = (FieldInfo)val;
            pi.SetValue(instance, v);
        }
        return null;
    }

    private static object GetValue(object instance, MemberInfo[] property, object v)
    {
        var val = property[0];
        if (val is PropertyInfo)
        {
            var pi = (PropertyInfo)val;
            return pi.GetValue(instance);
        }
        else if (val is FieldInfo)
        {
            var pi = (FieldInfo)val;
            return pi.GetValue(instance);
        }
        return null;
    }

    public static object GetValue(string name, Type type, object instance, IEnumerable pis, bool ignoreCase, object v)
    {
        return GetOrSetValue(name, type, instance, pis, ignoreCase, GetValue, v);
    }

    public static object SetValue(string name, Type type, object instance, IEnumerable pis, bool ignoreCase, object v)
    {
        return GetOrSetValue(name, type, instance, pis, ignoreCase, SetValue, v);
    }

    public static object GetOrSetValue(string name, Type type, object instance, IEnumerable pis, bool ignoreCase, Func<object, MemberInfo[], object, object> getOrSet, object v)
    {
        if (ignoreCase)
        {
            name = name.ToLower();
            foreach (MemberInfo item in pis)
            {
                if (item.Name.ToLower() == name)
                {
                    var property = type.GetMember(name);
                    if (property != null)
                    {
                        return getOrSet(instance, property, v);
                        //return GetValue(instance, property);
                    }
                }
            }
        }
        else
        {
            foreach (MemberInfo item in pis)
            {
                if (item.Name == name)
                {
                    var property = type.GetMember(name);
                    if (property != null)
                    {
                        return getOrSet(instance, property, v);
                        //return GetValue(instance, property);
                    }
                }
            }
        }
        return null;
    }

    public static bool ExistsClass(string className)
    {
        var type2 = (from assembly in AppDomain.CurrentDomain.GetAssemblies()
                     from type in assembly.GetTypes()
                     where type.Name == className
                     select type).FirstOrDefault();

        return type2 != null;
    }
    #endregion
}