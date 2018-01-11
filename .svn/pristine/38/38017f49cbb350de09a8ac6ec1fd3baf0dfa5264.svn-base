using System;
public static class CSharpHelper
{
    public static string DefaultValueForTypeSqLite(string type)
    {
        if (type.Contains("."))
        {
            type = ConvertTypeShortcutFullName.ToShortcut(type);
        }
        switch (type)
        {
            case "TEXT":
                return "\"\"";
            case "INTEGER":
                return int.MaxValue.ToString();
            case "REAL":
                return "0.0";
            case "DATETIME":
                // Původně tu bylo MinValue kvůli SQLite ale dohodl jsem se že SQLite už nebudu používat a proto si ušetřím v kódu práci s MSSQL 
                return "DateTime.MinValue";
            case "BLOB":
                // Podporovaný typ pouze v desktopových aplikacích, kde není lsožka sbf
                return "null";
            default:
                throw new Exception("Nepodporovaný typ v metodě DefaultValueForTypeSqLite");
        }
        throw new Exception("Nepodporovaný typ");
    }
    public static string DefaultValueForType(string type)
    {
        if (type.Contains("."))
        {
            type = ConvertTypeShortcutFullName.ToShortcut(type);
        }
        switch (type)
        {
            case "string":
                return "\"\"";
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
                return "MSStoredProceduresI.DateTimeMinVal";
            case "byte[]":
                // Podporovaný typ pouze v desktopových aplikacích, kde není lsožka sbf
                return "null";
            case "Guid":
                return "Guid.Empty";
            case "char":
                throw new Exception("Nepodporovaný typ");
        }
        throw new Exception("Nepodporovaný typ");
    }

    public static object DefaultValueForTypeObject(string type)
    {
        if (type.Contains("."))
        {
            type = ConvertTypeShortcutFullName.ToShortcut(type);
        }
        switch (type)
        {
            case "string":
                return "\"\"";
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
                return MSStoredProceduresI.DateTimeMinVal;
            case "char":
                throw new Exception("Nepodporovaný typ");
            case "byte[]":
                // Podporovaný typ pouze v desktopových aplikacích, kde není lsožka sbf
                return null;
        }
        throw new Exception("Nepodporovaný typ");
    }
}
