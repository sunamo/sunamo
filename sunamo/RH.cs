﻿using sunamo.Constants;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;


/// <summary>
/// Cant name Reflection because exists System.Reflection
/// </summary>
public class RH
{
    private static Type type = typeof(RH);

    public static object GetValueOfPropertyOrField(object o, string name)
    {
        var type = o.GetType();

        var value = GetValueOfProperty(name, type, o, false);

        if (value == null)
        {
            value = GetValueOfField(name, type, o, false);
        }

        return value;
    }

    #region Copy object
    public static object CopyObject(object input)
    {
        if (input != null)
        {
            object result = Activator.CreateInstance(input.GetType());//, BindingFlags.Instance);
            foreach (FieldInfo field in input.GetType().GetFields(
                BindingFlags.GetField |
                BindingFlags.GetProperty |
                BindingFlags.NonPublic |
                BindingFlags.Public |
                BindingFlags.Static |
                BindingFlags.Instance |
                BindingFlags.Default |
                BindingFlags.CreateInstance |
                BindingFlags.DeclaredOnly
                ))
            {
                if (field.FieldType.GetInterface("IList", false) == null)
                {
                    field.SetValue(result, field.GetValue(input));
                }
                else
                {
                    IList listObject = (IList)field.GetValue(result);
                    if (listObject != null)
                    {
                        foreach (object item in ((IList)field.GetValue(input)))
                        {
                            listObject.Add(CopyObject(item));
                        }
                    }
                }
            }
            return result;
        }
        else
        {
            return null;
        }
    }

    

    public static Assembly AssemblyWithName(string name)
    {
        var ass = AppDomain.CurrentDomain.GetAssemblies();
        var result = ass.Where(d => d.GetName().Name == name);
        if (result.Count() == 0)
        {
            result = ass.Where(d => d.FullName == name);
        }
        return result.FirstOrDefault();
    }

    /// <summary>
    /// Perform a deep Copy of the object.
    /// </summary>
    /// <typeparam name="T">The type of object being copied.</typeparam>
    /// <param name="source">The object instance to copy.</param>
    /// <returns>The copied object.</returns>
    public static T Clone<T>(T source)
    {
        if (!typeof(T).IsSerializable)
        {
            ThrowExceptions.Custom(Exc.GetStackTrace(), type, Exc.CallingMethod(),sess.i18n(XlfKeys.TheTypeMustBeSerializable) + ". source");
        }

        // Don't serialize a null object, simply return the default for that object
        if (Object.ReferenceEquals(source, null))
        {
            return default(T);
        }

        IFormatter formatter = new BinaryFormatter();
        Stream stream = new MemoryStream();
        using (stream)
        {
            formatter.Serialize(stream, source);
            stream.Seek(0, SeekOrigin.Begin);
            return (T)formatter.Deserialize(stream);
        }
    }

    public static List<string> GetValuesOfPropertyOrField(object o, params string[] onlyNames)
    {
        List<string> values = new List<string>();
        values.AddRange(GetValuesOfProperty(o, onlyNames));
        values.AddRange(GetValuesOfField(o, onlyNames));

        return values;
    }

    public static List<string> GetValuesOfField(object o, params string[] onlyNames)
    {
        var t = o.GetType();
        var props = t.GetFields();
        List<string> values = new List<string>(props.Length);

        foreach (var item in props)
        {
            if (onlyNames.Length > 0)
            {
                if (!onlyNames.Contains(item.Name))
                {
                    continue;
                }
            }

            values.Add(item.Name + AllStrings.cs2 + SH.ListToString( GetValueOfField(item.Name, t, o, false)));
        }

        return values;
    }

    public static List<string> GetValuesOfProperty(object o, params string[] onlyNames)
    {
        var props = o.GetType().GetProperties();
        List<string> values = new List<string>(props.Length);

        foreach (var item in props)
        {
            if (onlyNames.Length > 0)
            {
                if (!onlyNames.Contains(item.Name))
                {
                    continue;
                }
            }

            var getMethod = item.GetGetMethod();
            if (getMethod != null)
            {
                string name = getMethod.Name;
                object value = null;

                if (getMethod.GetParameters().Length > 0)
                {
                    name += "[]";
                    value = item.GetValue(o, new[] { (object)1/* indexer value(s)*/});
                }
                else
                {
                    try
                    {
                        value = item.GetValue(o);
                    }
                    catch (Exception ex)
                    {
                        value = Exceptions.TextOfExceptions(ex);
                    }
                }

                name = name.Replace("get_", string.Empty);

                values.Add($"{name}: {SH.ListToString(value)}");
            }
        }

        return values;
    }

    /// <summary>
    /// A1 can be Type of instance
    /// All fields must be public
    /// </summary>
    /// <param name="carSAutoType"></param>
    public static List<FieldInfo> GetFields(object carSAuto)
    {
        Type carSAutoType = null;
        var t1 = carSAuto.GetType();
        
        if (RH.IsType(t1))
        {
            carSAutoType = carSAuto as Type;
        }
        else
        {
            carSAutoType = carSAuto.GetType();
        }
        var result = carSAutoType.GetFields().ToList();
        return result;
    }

    private static bool IsType(Type t1)
    {
        var t2 = typeof(Type);
        return t1.FullName == "System.RuntimeType" || t1 == t2;
    }

    /// <summary>
    /// Copy values of all readable properties
    /// </summary>
    /// <param name="source"></param>
    /// <param name="target"></param>
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
    #endregion

    #region Get value
    

    public static object GetValueOfField(string name, Type type, object instance, bool ignoreCase)
    {
        FieldInfo[] pis = type.GetFields();

        return GetValue(name, type, instance, pis, ignoreCase);
    }

    public static object GetValue(string name, Type type, object instance, IEnumerable pis, bool ignoreCase)
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
                        return GetValue(instance, property);
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
                        return GetValue(instance, property);
                    }
                    
                }
            }
        }
        return null;
    }

    private static object GetValue(object instance, MemberInfo[] property)
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

    public static object GetValueOfProperty(string name, Type type, object instance, bool ignoreCase)
    {
        PropertyInfo[] pis = type.GetProperties();
        return GetValue(name, type, instance, pis, ignoreCase);
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
            ThrowExceptions.IsNull(Exc.GetStackTrace(),type, "IsOrIsDeriveFromBaseClass", "children", children);
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
    #endregion

    #region FullName
    public static string FullNameOfMethod(MethodInfo mi)
    {
        return mi.DeclaringType.FullName + mi.Name;
    }

    public static string FullNameOfClassEndsDot(Type v)
    {
        return v.FullName + AllStrings.dot;
    }

    public static string FullPathCodeEntity(Type t)
    {
        return t.Namespace + AllStrings.dot + t.Name;
    }

    public static string FullNameOfExecutedCode(MethodBase method)
    {
        string methodName = method.Name;
        string type = method.ReflectedType.Name;
        return SH.ConcatIfBeforeHasValue(type, AllStrings.dot, methodName, AllStrings.colon);
    }
    #endregion

    #region Whole assembly
    public static IEnumerable<Type> GetTypesInNamespace(Assembly assembly, string nameSpace)
    {
        var types = assembly.GetTypes();
        return types.Where(t => String.Equals(t.Namespace, nameSpace, StringComparison.Ordinal));
    }
    #endregion

    #region Get types of class
    /// <summary>
    /// Return FieldInfo, so will be useful extract Name etc. 
    /// </summary>
    /// <param name="type"></param>
    public static List<FieldInfo> GetConsts( Type type, GetMemberArgs a = null)
    {
        if (a == null)
        {
            a = new GetMemberArgs();
        }
        IEnumerable<FieldInfo> fieldInfos = null;
        if (a.onlyPublic)
        {
            fieldInfos = type.GetFields(BindingFlags.Public | BindingFlags.Static |
            // return protected/public but not private
            BindingFlags.FlattenHierarchy).ToList();
        }
        else
        {
            ///fieldInfos = type.GetFields(BindingFlags.Static);//.Where(f => f.IsLiteral);
            fieldInfos = type.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.NonPublic |
              BindingFlags.FlattenHierarchy).ToList();

        }


        var withType = fieldInfos.Where(fi => fi.IsLiteral && !fi.IsInitOnly).ToList();
        return withType;
    }

    public static List<MethodInfo> GetMethods(Type t)
    {
        var methods = t.GetMethods(BindingFlags.Public | BindingFlags.Static |
           // return protected/public but not private
           BindingFlags.FlattenHierarchy).ToList();
        return methods;
    }
    #endregion

    /// <summary>
    /// A1 have to be selected
    /// </summary>
    /// <param name="name"></param>
    /// <param name="o"></param>
    public static string DumpAsString(string name, object o, DumpProvider d, params string[] onlyNames)
    {
        // When I was serializing ISymbol, execution takes unlimited time here
        //return o.DumpToString(name);
        string dump = null;
        switch (d)
        {
            case DumpProvider.Yaml:
            case DumpProvider.Json:
            case DumpProvider.ObjectDumper:
            case DumpProvider.Reflection:
                dump = SH.Join(Environment.NewLine,RH.GetValuesOfProperty(o, onlyNames));
                break;
            default:
                ThrowExceptions.NotImplementedCase(Exc.GetStackTrace(),type, "DumpAsString", d);
                break;
        }

        return name + Environment.NewLine + dump;
    }

    private static string DumpAsString(string name, object o)
    {
        ThrowExceptions.NotImplementedMethod(Exc.GetStackTrace(),type, "DumpAsString" );
        return null;
    }

    public static string DumpListAsString(string name, IEnumerable o)
    {
        StringBuilder sb = new StringBuilder();

        int i = 0;
        foreach (var item in o)
        {
            sb.AppendLine(DumpAsString(name + "#" + i, item));
            i++;
        }

        return sb.ToString();
    }
}