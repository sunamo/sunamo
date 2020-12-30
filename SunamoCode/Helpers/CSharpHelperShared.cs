using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
public static partial class CSharpHelper
{
static Type type = typeof(CSharpHelper);
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
                ThrowExceptions.Custom(Exc.GetStackTrace(), type, Exc.CallingMethod(),type);
                return 0;
            case "byte" + "[]":
                // Podporovaný typ pouze v desktopových aplikacích, kde není lsožka sbf
                return null;
        }
        ThrowExceptions.Custom(Exc.GetStackTrace(), type, Exc.CallingMethod(),"Nepodporovaný typ");
        return null;
    }

    public static string WrapWithRegion(string s, string v)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("#region ");
        sb.AppendLine(v);
        sb.AppendLine(s);
        sb.AppendLine("#endregion");
        return sb.ToString();
    }

    /// <summary>
    /// call CsKeywords.Init before use
    /// </summary>
    /// <param name="con"></param>
    /// <returns></returns>
    public static bool IsKeyword(string con)
    {
        //CsKeywords.Init();

        if (CsKeywordsList.modifier.Contains(con))
        {
            return true;
        }
        if (CsKeywordsList.accessModifier.Contains(con))
        {
            return true;
        }
        if (CsKeywordsList.statement.Contains(con))
        {
            return true;
        }
        if (CsKeywordsList.methodParameter.Contains(con))
        {
            return true;
        }
        if (CsKeywordsList._namespace.Contains(con))
        {
            return true;
        }
        if (CsKeywordsList._operator.Contains(con))
        {
            return true;
        }
        if (CsKeywordsList.access.Contains(con))
        {
            return true;
        }
        if (CsKeywordsList.literal.Contains(con))
        {
            return true;
        }
        if (CsKeywordsList.type.Contains(con))
        {
            return true;
        }
        if (CsKeywordsList.contextual.Contains(con))
        {
            return true;
        }
        if (CsKeywordsList.query.Contains(con))
        {
            return true;
        }

        return false;
    }
}