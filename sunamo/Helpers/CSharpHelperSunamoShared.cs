using System;
using System.Collections.Generic;
using System.Text;
public partial class CSharpHelperSunamo
{
static Type type = typeof(CSharpHelperSunamo);
    public static string DefaultValueForType(string type)
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
                return "false";
            case "float":
            case "double":
            case "int":
            case "long":
            case "short":
            case "decimal":
            case "sbyte":
                return "-1";
            case "byte":
            case "ushort":
            case "uint":
            case "ulong":
                return "0";
            case "DateTime":
                // Původně tu bylo MinValue kvůli SQLite ale dohodl jsem se že SQLite už nebudu používat a proto si ušetřím v kódu práci s MSSQL 
                return "SqlServerHelper.DateTimeMinVal";
            case "byte[]":
                // Podporovaný typ pouze v desktopových aplikacích, kde není lsožka sbf
                return "null";
            case "Guid":
                return "Guid.Empty";
            case "char":
                ThrowExceptions.Custom(Exc.GetStackTrace(), type, Exc.CallingMethod(),"Nepodporovan\u00FD typ");
                break;
            default:
                // For types like Dictionary<int,int>
                return "new " + type + "()";
        }
        //ThrowExceptions.Custom(Exc.GetStackTrace(), type, Exc.CallingMethod(),"Nepodporovan\u00FD typ");
        return null;
    }

    public static string ReplaceReadonlyToConst(string arg)
    {
        arg = arg.Replace("static readonly", "const");
        arg = arg.Replace("readonly", "const");
        return arg;
    }
}