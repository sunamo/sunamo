using System;
public static class ConvertTypeShortcutFullName //: IConvertShortcutFullName
{
    public static string FromShortcut(string shortcut)
    {
        decimal d = -1m;
        double dd = -1d;
        float f = -1f;

        switch (shortcut)
        {
            case "string":
                return "System.String";
            case "int":
                return "System.Int32";
            case "bool":
                return "System.Boolean";
            case "float":
                return "System.Single";
            case "DateTime":
                return "System.DateTime";
            case "double":
                return "System.Double";
            case "decimal":
                return "System.Decimal";
            case "char":
                return "System.Char";
            case "byte":
                return "System.Byte";
            case "sbyte":
                return "System.SByte";
            case "short":
                return "System.Int16";
            case "long":
                return "System.Int64";
            case "ushort":
                return "System.UInt16";
            case "uint":
                return "System.UInt32";
            case "ulong":
                return "System.UInt64";
        }
        throw new Exception("Nepodporované klíčové slovo");
    }

    public static string ToShortcut(object instance)
    {
        return ToShortcut(instance.GetType().FullName, false);
    }

    public static string ToShortcut(string fullName)
    {
        return ToShortcut(fullName, true);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="fullName"></param>
    /// <returns></returns>
    public static string ToShortcut(string fullName, bool throwExceptionWhenNotBasicType)
    {
        switch (fullName)
        {
            #region MyRegion
            case "System.String":
                return "string";
            case "System.Int32":
                return "int";
            case "System.Boolean":
                return "bool";
            case "System.Single":
                return "float";
            case "System.DateTime":
                return "DateTime";
            case "System.Double":
                return "double";
            case "System.Decimal":
                return "decimal";
            case "System.Char":
                return "char";
            case "System.Byte":
                return "byte";
            case "System.SByte":
                return "sbyte";
            case "System.Int16":
                return "short";
            case "System.Int64":
                return "long";
            case "System.UInt16":
                return "ushort"; 
            case "System.UInt32":
                return "uint";
            case "System.UInt64":
                return "ulong";
                #endregion
        }
        if (throwExceptionWhenNotBasicType)
        {
            throw new Exception("Nepodporovaný typ");
        }
        return fullName;
    }
}
