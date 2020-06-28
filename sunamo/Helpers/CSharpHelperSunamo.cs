using System;
using System.Collections.Generic;
using System.Text;
public partial class CSharpHelperSunamo
{
  


    public static void IndentAsPreviousLine(List<string> lines)
    {
        string indentPrevious = string.Empty;
        string line = null;
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < lines.Count; i++)
        {
            line = lines[i];
            if (line.Length > 0)
            {
                if (!char.IsWhiteSpace(line[0]))
                {
                    lines[i] = indentPrevious + lines[i];
                }
                else
                {
                    indentPrevious = SH.GetWhitespaceFromBeginning(sb, line);
                }
            }
        }
    }
    public static bool IsInterface(string item)
    {
        if (item[0] == 'I')
        {
            if (char.IsUpper(item[1]))
            {
                return true;
            }
        }
        return false;
    }
    public static string ReplaceNulled(string s)
    {
        return s.Replace(Consts.nulled, string.Empty).Trim();
    }
    public static string ShortcutForControl(string name)
    {
        StringBuilder sb = new StringBuilder();
        foreach (var item in name)
        {
            if (char.IsUpper(item))
            {
                sb.Append(item.ToString().ToLower());
            }
        }
        return sb.ToString();
    }
    /// <summary>
    /// Its not compatible with default operator
    /// https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/default-values
    /// Nonsense, cant type too many different output types to T. 
    /// Must cast manually
    /// </summary>
    /// <typeparam name = "T"></typeparam>
    /// <param name = "t"></param>
    public static object DefaultValueForTypeT<T>(T t)
    {
        var type = t.GetType().FullName;
        if (type.Contains(AllStrings.dot))
        {
            type = ConvertTypeShortcutFullName.ToShortcut(type);
        }
        #region Same seria as in Types
        switch (type)
        {
            case "string":
                return string.Empty;
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
            case "byte[]":
                // Podporovaný typ pouze v desktopových aplikacích, kde není lsožka sbf
                return null;
            case "Guid":
                return Guid.Empty;
            case "char":
                ThrowExceptions.Custom(Exc.GetStackTrace(), type, Exc.CallingMethod(),"Nepodporovan\u00FD typ");
                break;
        } 
        #endregion
        ThrowExceptions.Custom(Exc.GetStackTrace(), type, Exc.CallingMethod(),"Nepodporovan\u00FD typ");
        return null;
    }
}