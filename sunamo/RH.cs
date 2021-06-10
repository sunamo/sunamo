using sunamo.Constants;
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
public partial class RH
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
        return GetValue(name, type, instance, pis, ignoreCase, v);
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



    public static object SetValue(string name, Type type, object instance, IEnumerable pis, bool ignoreCase, object v)
    {
        return GetOrSetValue(name, type, instance, pis, ignoreCase, SetValue,v);
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

    public static List<string> GetValuesOfConsts(Type type)
    {
        var c = GetConsts(type);
        List<string> vr = new List<string>();
        foreach (var item in c)
        {
            vr.Add(SH.NullToStringOrDefault( item.GetValue(null)));
        }
        CA.Trim(vr);
        return vr;
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
            ThrowExceptions.Custom(Exc.GetStackTrace(), type, Exc.CallingMethod(),SunamoPageHelperSunamo.i18n(XlfKeys.TheTypeMustBeSerializable) + ". source");
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

    public static Dictionary<string, string> GetValuesOfConsts(Type t, params string[] onlyNames)
    {
        var props = RH.GetConsts(t);
        Dictionary<string, string> values = new Dictionary<string, string>(props.Count);

        foreach (var item in props)
        {
            if (onlyNames.Length > 0)
            {
                if (!onlyNames.Contains(item.Name))
                {
                    continue;
                }
            }

            var o = GetValueOfField(item.Name, t, null, false);
            values.Add(item.Name, o.ToString());
        }

        return values;
    }

    

    

    /// <summary>
    /// U složitějších ne mých .net objektů tu byla chyba, proto je zde GetValuesOfProperty2
    /// </summary>
    /// <param name="o"></param>
    /// <param name="onlyNames"></param>
    /// <returns></returns>
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
                AddValue(values, name, value, false);
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

    /// <summary>
    /// Pokud mám chybu Could not load file or assembly System.Reflection.Metadata, Version=1.4.5.0
    /// program volám z AllProjectsSearchConsole tuto sunamo assembly,
    /// musím přidat System.Reflection.Metadata do obou. Ověřeno.
    /// 
    /// Better than load assembly directly from running is use Assembly.LoadFrom
    /// </summary>
    /// <param name="assembly"></param>
    /// <param name="contains"></param>
    /// <returns></returns>
    public static IEnumerable<Type> GetTypesInAssembly(Assembly assembly, string contains)
    {
        var types = assembly.GetTypes();
        return types.Where(t => t.Name.Contains(contains));
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

    

    
}