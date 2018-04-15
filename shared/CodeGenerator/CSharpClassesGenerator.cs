using System.Collections.Generic;
/// <summary>
/// Generate of single element is in CSharpHelper
/// </summary>
public class CSharpClassesGenerator
{
    public static string Dictionary(string nameClass, List<string> keys, StringVoid randomValue)
    {
        GeneratorCSharp genCS = new GeneratorCSharp();
        genCS.StartClass(0, AccessModifiers.Private, false, nameClass);
        genCS.Field(1, AccessModifiers.Private, false, VariableModifiers.None, "Dictionary&lt;string, string&gt;", "dict", false, "new Dictionary&lt;string, string&gt;()");
        GeneratorCSharp inner = new GeneratorCSharp();
        foreach (var item in keys)
        {
            inner.AppendLine(2, "dict.Add(\"{0}\", \"{1}\");", item, randomValue.Invoke());
        }
        genCS.Ctor(1, ModifiersConstructor.Private, nameClass, inner.ToString());
        genCS.EndBrace(0);
        return genCS.ToString();
    }


}
