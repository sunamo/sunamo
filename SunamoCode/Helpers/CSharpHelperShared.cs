using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public static partial class CSharpHelper
{

    public static object DefaultValueForTypeObject(string type)
    {
        if (type.Contains(AllStrings.dot))
        {
            type = ConvertTypeShortcutFullName.ToShortcut(type);
        }

        switch (type)
        {
            case "string":
                return AllStrings.qm + AllStrings.qm;
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
                return Consts.DateTimeMinVal;
            case "char":
                throw new Exception("Nepodporovaný typ");
            case "byte" + "[]":
                // Podporovaný typ pouze v desktopových aplikacích, kde není lsožka sbf
                return null;
        }

        throw new Exception("Nepodporovaný typ");
    }
}