using System;
public static class ConvertTypeShortcutFullName //: IConvertShortcutFullName
{
    const string systemDot = "System.";

    public static string FromShortcut(string shortcut)
    {
        
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
        ThrowExceptions.Custom(Exc.GetStackTrace(), type, Exc.CallingMethod(),"Nepodporovan\u00E9 kl\u00ED\u010Dov\u00E9 slovo");
        return null;
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
    public static string ToShortcut(string fullName, bool throwExceptionWhenNotBasicType)
    {
        if (!fullName.StartsWith(systemDot))
        {
            fullName = systemDot + fullName;
        }

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
            ThrowExceptions.Custom(Exc.GetStackTrace(), type, Exc.CallingMethod(),"Nepodporovan\u00FD typ");
            return null;
        }
        return fullName;
    }

    static Type type = typeof(ConvertTypeShortcutFullName);
}