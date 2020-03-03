
using sunamo.Constants;
using sunamo.Values;
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
    private static Type s_type = typeof(RH);

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

    /// <summary>
    /// Print name of calling method, not GetCurrentMethod
    /// If is on end Test, will trim
    /// </summary>
    
    public static string DumpAsString(string name, object o, DumpProvider d, params string[] onlyNames)
    {
        // When I was serializing ISymbol, execution takes unlimited time here
        //return o.DumpToString(name);
        string dump = null;
        switch (d)
        {
            case DumpProvider.Reflection:
                dump = SH.Join(RH.GetValuesOfProperty(o, onlyNames), Environment.NewLine);
                break;
            case DumpProvider.Yaml:
                dump = YamlHelper.DumpAsYaml(o);
                break;
            case DumpProvider.Microsoft:
                dump = JavascriptSerialization.InstanceMs.Serialize(o);
                break;
            case DumpProvider.Newtonsoft:
                dump = JavascriptSerialization.InstanceNewtonSoft.Serialize(o);
                break;
            case DumpProvider.ObjectDumper:
                dump = RH.DumpAsString(name, o);
                break;
            default:
                ThrowExceptions.NotImplementedCase(s_type, "DumpAsString", d);
                break;
        }

        return name + Environment.NewLine + dump;
    }

    private static string DumpAsString(string name, object o)
    {
        ThrowExceptions.NotImplementedMethod(s_type, "DumpAsString" );
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
