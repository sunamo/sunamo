using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

public enum SerializationLibrary
{
    Microsoft,
    Newtonsoft
}

public class JavascriptSerialization
{
    SerializationLibrary sl = SerializationLibrary.Newtonsoft;
    /// <summary>
    /// Výchozí pro A1 je Microsoft
    /// </summary>
    /// <param name="sl"></param>
    public JavascriptSerialization(SerializationLibrary sl)
    {
    }

    public string Serialize(object o)
    {
        if (sl == SerializationLibrary.Microsoft)
        {
            return ThrowExceptionsMicrosoftSerializerNotSupported<string>();
            //return js.Serialize(o);
        }
        else if (sl == SerializationLibrary.Newtonsoft)
        {
            return JsonConvert.SerializeObject(o);
        }
        else
        {
            return NotSupportedElseIfClasule<string>("Serialize");
        }
    }

    private T ThrowExceptionsMicrosoftSerializerNotSupported<T>()
    {
        throw new Exception("System.Web.Scripting.Serialization.JavaScriptSerializer is not supported in Windows Store Apps.");
        return default(T);
    }

    private T NotSupportedElseIfClasule<T>(string v)
    {
        throw new NotImplementedException("Else if with enum value " + sl + " in JavascriptSerialization." + v);
        return default(T);
    }

    public object Deserialize(String o, Type targetType)
    {
        if (sl == SerializationLibrary.Microsoft)
        {
            return ThrowExceptionsMicrosoftSerializerNotSupported<object>();
            //return js.Deserialize(o, targetType);
        }
        else if (sl == SerializationLibrary.Newtonsoft)
        {
            return JsonConvert.DeserializeObject(o, targetType);
        }
        else
        {
            return NotSupportedElseIfClasule<object>("Serialize(String,Type)");
        }
    }

    public T Deserialize<T>(String o)
    {
        if (sl == SerializationLibrary.Microsoft)
        {
            //return js.Deserialize<T>(o);
            return (T)ThrowExceptionsMicrosoftSerializerNotSupported<T>();
        }
        else if (sl == SerializationLibrary.Newtonsoft)
        {
            return JsonConvert.DeserializeObject<T>(o);
        }
        else
        {
            return NotSupportedElseIfClasule<T>("Serialize(String)");
        }
    }


}
