using System.Runtime.Serialization;
using System.IO;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.Reflection;
using System.Collections;
using System.Drawing.Imaging;
public  class RH
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
                BindingFlags.Static|
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

}
