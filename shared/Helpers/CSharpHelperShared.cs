using sunamo;
using System;
using System.Collections;
using System.Collections.Generic;

public static partial class CSharpHelper{ 



public static object DefaultValueForTypeObject(string type)
    {
        if (type.Contains(AllStrings.dot))
        {
            type = ConvertTypeShortcutFullName.ToShortcut(type);
        }

        switch (type)
        {
            case "string":
                return AllStrings.qm;
            case "bool":
                return false;
            case "float":
            case "double":
            case "int":
            case "long":
            case "short":
            case "decimal":
            case "sbyte":
                return -1;
            case "byte":
            case "ushort":
            case "uint":
            case "ulong":
                return 0;
            case "DateTime":
                // Původně tu bylo MinValue kvůli SQLite ale dohodl jsem se že SQLite už nebudu používat a proto si ušetřím v kódu práci s MSSQL 
                return SqlServerHelper.DateTimeMinVal;
            case "char":
                throw new Exception("Nepodporovaný typ");
            case "byte[]":
                // Podporovaný typ pouze v desktopových aplikacích, kde není lsožka sbf
                return null;
        }

        throw new Exception("Nepodporovaný typ");
    }

public static string Dictionary(string nameClass, List<string> keys, StringVoid randomValue)
    {
        CSharpGenerator genCS = new CSharpGenerator();
        genCS.StartClass(0, AccessModifiers.Private, false, nameClass);
        genCS.Field(1, AccessModifiers.Private, false, VariableModifiers.None, "Dictionary&lt;string, string&gt;", "dict", false, "new Dictionary&lt;string, string&gt;()");
        CSharpGenerator inner = new CSharpGenerator();
        foreach (var item in keys)
        {
            inner.AppendLine(2, "dict.Add(\"{0}\", \"{1}\");", item, randomValue.Invoke());
        }

        genCS.Ctor(1, ModifiersConstructor.Private, nameClass, inner.ToString());
        genCS.EndBrace(0);
        return genCS.ToString();
    }
}