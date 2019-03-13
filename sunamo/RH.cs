﻿
using sunamo.Constants;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace sunamo
{
    /// <summary>
    /// Cant name Reflection because exists System.Reflection
    /// </summary>
    public class RH
    {
        static Type type = typeof(RH);

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
                throw new ArgumentException("The type must be serializable.", "source");
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

        public static List<string> GetValuesOfProperty(object o, params string[] onlyNames)
        {
            List<string> values = new List<string>();

            var props = o.GetType().GetProperties();
            foreach (var item in props)
            {
                if (!onlyNames.Contains(item.Name))
                {
                    continue;
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

                    values.Add($"{name}: {SH.ListToString( value)}");
                }
            }

            return values;
        }

        /// <summary>
        /// A1 can be Type of instance
        /// </summary>
        /// <param name="carSAutoType"></param>
        /// <returns></returns>
        public static List<FieldInfo> GetFields(object carSAuto)
        {
            Type carSAutoType = null;
            if (carSAuto.GetType() == typeof(Type))
            {
                carSAutoType = carSAuto as Type;
            }
            else
            {
                carSAutoType = carSAuto.GetType();
            }
            return carSAutoType.GetFields().ToList();
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
        #endregion

        #region FullName
        public static string FullNameOfMethod(MethodInfo mi)
        {
            return mi.DeclaringType.FullName + mi.Name;
        }

        public static string FullNameOfClassEndsDot(Type v)
        {
            return v.FullName + ".";
        }

        public static string FullPathCodeEntity(Type t)
        {
            return t.Namespace + AllStrings.dot + t.Name;
        }

        public static string FullNameOfExecutedCode(MethodBase method)
        {
            string methodName = method.Name;
            string type = method.ReflectedType.Name;
            return SH.ConcatIfBeforeHasValue(type, ".", methodName, ":");
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
        public static List<FieldInfo> GetConsts(Type type)
        {
            FieldInfo[] fieldInfos = type.GetFields(BindingFlags.Public | BindingFlags.Static | 
                // return protected/public but not private
                BindingFlags.FlattenHierarchy);

            return fieldInfos.Where(fi => fi.IsLiteral && !fi.IsInitOnly).ToList();
        }
        #endregion

        /// <summary>
        /// A1 have to be selected
        /// </summary>
        /// <param name="name"></param>
        /// <param name="o"></param>
        /// <returns></returns>
        public static string DumpAsString(string name, object o)
        {

            // When I was serializing ISymbol, execution takes unlimited time here
            //return o.DumpToString(name);
            ThrowExceptions.NotImplementedCase(type, "DumpAsString");
            return string.Empty;
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
}
