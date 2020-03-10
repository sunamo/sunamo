using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;



public class JavascriptSerialization
{
    public static JavascriptSerialization InstanceMs = new JavascriptSerialization(SerializationLibrary.Microsoft);
    public static JavascriptSerialization InstanceNewtonSoft = new JavascriptSerialization(SerializationLibrary.Newtonsoft);

    private SerializationLibrary _sl = SerializationLibrary.Newtonsoft;
    /// <summary>
    /// Výchozí pro A1 je Microsoft
    /// </summary>
    /// <param name="sl"></param>
    public JavascriptSerialization(SerializationLibrary sl)
    {
    }

    public string Serialize(object o)
    {
        if (_sl == SerializationLibrary.Microsoft)
        {
            return ThrowExceptionsMicrosoftSerializerNotSupported<string>();
            //return js.Serialize(o);
        }
        else if (_sl == SerializationLibrary.Newtonsoft)
        {
            return JsonConvert.SerializeObject(o);
        }
        else
        {
            return NotSupportedElseIfClasule<string>("Serialize");
        }
    }
    static Type type = typeof(JavascriptSerialization);
    private T ThrowExceptionsMicrosoftSerializerNotSupported<T>()
    {
        ThrowExceptions.Custom(Exc.GetStackTrace(), type, Exc.CallingMethod(),"System.Web.Scripting.Serialization.JavaScriptSerializer is not supported in Windows Store Apps" + ".");
        return default(T);
    }

    private T NotSupportedElseIfClasule<T>(string v)
    {
        ThrowExceptions.Custom(Exc.GetStackTrace(), type, Exc.CallingMethod(),"Else if with enum value" + " " + _sl + " " + "in JavascriptSerialization" + "." + v);
        return default(T);
    }

    public object Deserialize(String o, Type targetType)
    {
        if (_sl == SerializationLibrary.Microsoft)
        {
            return ThrowExceptionsMicrosoftSerializerNotSupported<object>();
        }
        else if (_sl == SerializationLibrary.Newtonsoft)
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
        if (_sl == SerializationLibrary.Microsoft)
        {
            //return js.Deserialize<T>(o);
            return (T)ThrowExceptionsMicrosoftSerializerNotSupported<T>();
        }
        else if (_sl == SerializationLibrary.Newtonsoft)
        {
            return JsonConvert.DeserializeObject<T>(o);
        }
        else
        {
            return NotSupportedElseIfClasule<T>("Serialize(String)");
        }
    }
}