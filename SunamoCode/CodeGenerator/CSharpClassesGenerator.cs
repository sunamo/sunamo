using System;
using System.Collections.Generic;
/// <summary>
/// Generate of single element is in CSharpHelper
/// </summary>
public class CSharpClassesGenerator
{
    public static Type type = typeof(CSharpClassesGenerator);

    public static string Dictionary(string nameClass, List<string> keys, StringVoid randomValue)
    {
        List<string> values = new List<string>();
        for (int i = 0; i < keys.Count; i++)
        {
            values.Add(randomValue());
        }

        return Dictionary(nameClass, keys, values);
    }

    public static string Dictionary(string nameClass, List<string> keys, List<string> values)
    {
        if (keys.Count != values.Count)
        {
            ThrowExceptions.DifferentCountInLists(Exc.GetStackTrace(), type, Exc.CallingMethod(), "", keys.Count, "", values.Count);
        }

        CSharpGenerator genCS = new CSharpGenerator();
        genCS.StartClass(0, AccessModifiers.Private, false, nameClass);
        genCS.Field(1, AccessModifiers.Private, false, VariableModifiers.None, "Dictionary<string, string>", "dict", false, "new Dictionary<string, string>()");
        CSharpGenerator inner = new CSharpGenerator();
        for (int i = 0; i < keys.Count; i++)
        {
            inner.AppendLine(2, "dict.Add(\"{0}\", \"{1}\");", keys[i], values[i]);
        }
        genCS.Ctor(1, ModifiersConstructor.Private, nameClass, inner.ToString());
        genCS.EndBrace(0);
        return genCS.ToString();
    }

    public static string DictionaryPascalConvention(string nameClass, List<string> list, bool switchKeysAndValues)
    {
        List<string> values = new List<string>();
        for (int i = 0; i < list.Count; i++)
        {
            values.Add(ConvertPascalConventionWithNumbers.ToConvention(list[i]));
        }

        if (switchKeysAndValues)
        {
            return Dictionary(nameClass, values, list);
        }
        else
        {
            return Dictionary(nameClass, list, values);
        }
    }
}