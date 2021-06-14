using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

public partial class RH
{
    static Type type = typeof(ThrowExceptions);

    public static string DumpListAsString(DumpAsStringArgs a, bool removeNull = false)
    {
        StringBuilder sb = new StringBuilder();
        var f = CA.ToList<object>((IEnumerable)a.o);

        if (removeNull)
        {
            f.RemoveAll(d => d == null);
        }

        if (f.Count > 0)
        {
            sb.AppendLine(RH.NameOfFieldsFromDump(f.First(), a));

            foreach (var item in f)
            {
                a.o = item;
                sb.AppendLine(DumpAsString(a));
            }
        }
        return sb.ToString();
    }

    public static string DumpListAsString(string name, IEnumerable o)
    {
        StringBuilder sb = new StringBuilder();

        int i = 0;
        foreach (var item in o)
        {
            sb.AppendLine(DumpAsString2(name + "#" + i, item));
            i++;
        }

        return sb.ToString();
    }

    public static string DumpListAsStringOneLine(string operation, IEnumerable o, DumpAsStringHeaderArgs a)
    {
        if (o.Count() > 0)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(Consts._3Asterisks);
            sb.AppendLine(operation + AllStrings.space + AllStrings.lb + o.Count() + AllStrings.rb + AllStrings.colon);

            sb.AppendLine(NameOfFieldsFromDump(o.FirstOrNull(), a)); 

            int i = 0;
            foreach (var item in o)
            {
                sb.AppendLine(DumpAsString(new DumpAsStringArgs { d = DumpProvider.Reflection, deli = AllStrings.swd, o = item, onlyValues = true,onlyNames = a.onlyNames }));
                i++;
            }

            sb.AppendLine(Consts._3Asterisks);
            return sb.ToString();
        }
        return string.Empty;
    }

    /// <summary>
    /// Delimited by NL
    /// </summary>
    /// <param name="v"></param>
    /// <param name="device"></param>
    /// <returns></returns>
    public static string DumpAsString2(string v, object device)
    {
        return DumpAsString(new DumpAsStringArgs { name = v, o = device, d = DumpProvider.Yaml });
    }

    /// <summary>
    /// swda Delimiter
    /// Mainly for fast comparing objects
    /// </summary>
    /// <param name="v"></param>
    /// <param name="tableRowPageNew"></param>
    /// <returns></returns>
    public static string DumpAsString3(object tableRowPageNew, DumpAsStringHeaderArgs a = null)
    {
        if (a == null)
        {
            a = DumpAsStringHeaderArgs.Default;
        }
        var dasa = new DumpAsStringArgs { o = tableRowPageNew, deli = AllStrings.swd, onlyValues = true, onlyNames = a.onlyNames };
        return DumpAsString(dasa);
    }

    public static List<string> GetValuesOfField(object o, params string[] onlyNames)
    {
        return GetValuesOfField(o, onlyNames);
    }

    public static List<string> GetValuesOfField(object o, IList<string> onlyNames, bool onlyValues)
    {
        var t = o.GetType();
        var props = t.GetFields();
        List<string> values = new List<string>(props.Length);

        foreach (var item in props)
        {
            if (onlyNames.Count > 0)
            {
                if (!onlyNames.Contains(item.Name))
                {
                    continue;
                }
            }

            //values.Add(item.Name + AllStrings.cs2 + SH.ListToString(GetValueOfField(item.Name, t, o, false)));

            AddValue(values, item.Name, SH.ListToString(GetValueOfField(item.Name, t, o, false)), onlyValues);
        }

        return values;
    }

    public static List<string> GetValuesOfProperty2(object obj, List<string> onlyNames, bool onlyValues)
    {
        var onlyNames2 = onlyNames.ToList();
        List<string> values = new List<string>();
        bool add = false;

        string name = null;
        var props = TypeDescriptor.GetProperties(obj);

        bool isAllNeg = true;
        foreach (var item in onlyNames)
        {
            if (!item.StartsWith(AllStrings.excl))
            {
                isAllNeg = false;
            }
        }

        foreach (PropertyDescriptor descriptor in props)
        {
            add = true;
            name = descriptor.Name;

            if (onlyNames.Contains(AllStrings.excl + name))
            {
                continue;
            }

            if (onlyNames2.Count > 0)
            {
                
                if (isAllNeg)
                {
                    if (onlyNames2.Contains(AllStrings.excl + name))
                    {
                        add = false;
                    }
                }
                else
                {
                    if (!onlyNames2.Contains(name))
                    {
                        add = false;
                    }
                }
            }

            if (add)
            {
                object value = descriptor.GetValue(obj);
                AddValue(values, name, value, onlyValues);
            }
        }

        return values;
    }

    #region Get value

    #endregion

    public static object GetValueOfField(string name, Type type, object instance, bool ignoreCase)
    {
        FieldInfo[] pis = type.GetFields();

        return GetValue(name, type, instance, pis, ignoreCase, null);
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


    

    private static void AddValue(List<string> values, string name, object value, bool onlyValue)
    {
        var v = SH.ListToString(value);
        if (onlyValue)
        {
            values.Add(v);
        }
        else
        {
            values.Add($"{name}: {v}");
        }

    }

    /// <summary>
    /// Check whether A1 is or is derived from A2
    /// </summary>
    /// <param name="type1"></param>
    /// <param name="type2"></param>
    public static bool IsOrIsDeriveFromBaseClass(Type children, Type parent, bool a1CanBeString = true)
    {
        if (children == Types.tString && !a1CanBeString)
        {
            return false;
        }

        if (children == null)
        {
            ThrowExceptions.IsNull(Exc.GetStackTrace(), type, "IsOrIsDeriveFromBaseClass", "children", children);
        }
        while (true)
        {
            if (children == null)
            {
                return false;
            }
            if (children == parent)
            {
                return true;
            }
            foreach (var inter in children.GetInterfaces())
            {
                if (inter == parent)
                {
                    return true;
                }
            }

            children = children.BaseType;
        }
        return false;
    }

   

    /// <summary>
    /// A1 have to be selected
    /// </summary>
    /// <param name="name"></param>
    /// <param name="o"></param>
    public static string DumpAsString(DumpAsStringArgs a)
    {
        // When I was serializing ISymbol, execution takes unlimited time here
        //return o.DumpToString(name);
        string dump = null;
        switch (a.d)
        {
            case DumpProvider.Yaml:
            case DumpProvider.Json:
            case DumpProvider.ObjectDumper:
            case DumpProvider.Reflection:
                dump = SH.Join(a.onlyValues ? a.deli : Environment.NewLine, RH.GetValuesOfProperty2(a.o, a.onlyNames, a.onlyValues));
                break;
            default:
                ThrowExceptions.NotImplementedCase(Exc.GetStackTrace(), type, "DumpAsString", a.d);
                break;
        }
        if (string.IsNullOrWhiteSpace(a.name))
        {
            return dump;
        }
        return a.name + Environment.NewLine + dump;
        
    }

    

    private static string NameOfFieldsFromDump(object obj, DumpAsStringHeaderArgs a)
    {
        var properties = TypeDescriptor.GetProperties(obj);
        List<string> ls = new List<string>();

        string name = null;

        foreach (PropertyDescriptor descriptor in properties)
        {
            name = descriptor.Name;
            if (a.onlyNames.Contains(AllStrings.excl + name))
            {
                continue;
            }
            ls.Add(name);
        }

        return SH.Join(AllStrings.swd, ls);
    }

    public static string DumpAsString3Dictionary3<T, T2, U>(string operation, Dictionary<T, Dictionary<T2, List<U>>> grouped)
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine(operation);

        foreach (var item in grouped)
        {
            sb.AppendLine("1) " + item.Key.ToString());

            foreach (var item3 in item.Value)
            {
                sb.AppendLine("2) " + item3.Key.ToString());

                foreach (var v in item3.Value)
                {
                    sb.AppendLine(v.ToString());
                }
                sb.AppendLine();
            }
        }

        var vr = sb.ToString();
        return vr;
    }

    public static string DumpAsString3Dictionary2<T, T1>(string operation, Dictionary<T, List<T1>> grouped)
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine(operation);

        foreach (var item in grouped)
        {
            sb.AppendLine("1) " + item.Key.ToString());

                foreach (var v in item.Value)
                {
                    sb.AppendLine(v.ToString());
                }
                sb.AppendLine();
        }

        var vr = sb.ToString();
        return vr;
    }
}
