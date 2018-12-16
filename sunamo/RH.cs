using sunamo.Constants;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
//using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace sunamo
{
    public class RH
    {
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

        public static string FullPathCodeEntity(Type t)
        {
            return t.Namespace + AllStrings.dot + t.Name;
        }
    }
}
