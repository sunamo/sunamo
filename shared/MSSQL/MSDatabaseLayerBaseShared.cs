using sunamo.Values;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

public partial class MSDatabaseLayerBase{ 
/// <summary>
    /// Vrací @p0,@p1,...,@pX
    /// Vrátí tolik hodnot kolik je v A1.
    /// Do A1 se mohou předávat libovolné hodnoty - AB, string, object,...
    /// </summary>
    /// <param name="sloupce"></param>
    /// <returns></returns>
    public static string GetValues(params object[] sloupce)
    {
        int to = sloupce.Length;

        return GetValuesDirect(to);
    }

/// <summary>
    /// Direct znamená že mohu přímo zadat počet parametrů které si přeji vytvořit
    /// Parametr s číslem A1 už ve výsledku nebude.
    /// </summary>
    /// <param name="to"></param>
    /// <returns></returns>
    public static string GetValuesDirect(int to)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("(");
        for (int i = 0; i < to; i++)
        {
            sb.Append("@p" + (i).ToString() + ",");
        }
        return sb.ToString().TrimEnd(',') + ")";
    }

/// <summary>
    /// NSN
    /// </summary>
    /// <param name="p"></param>
    /// <returns></returns>
    public static string ConvertObjectToDotNetType(object p)
    {
        if (p.GetType() == Consts.tString)
        {
            return "string";
        }
        else if (p.GetType() == Consts.tInt)
        {
            return "int";
        }
        else if (p.GetType() == Consts.tBool)
        {
            return "bool";
        }
        else if (p.GetType() == Consts.tDateTime)
        {
            return "DateTime";
        }
        else if (p.GetType() == Consts.tBinary)
        {
            return "byte[]";
        }
        else if (p.GetType() == Consts.tFloat)
        {
            return "float";
        }
        else if (p.GetType().IsEnum)
        {
            return p.GetType().Name;
        }
        else if (p.GetType() == Consts.tDouble)
        {
            return "double";
        }
        else if (p.GetType() == Consts.tDecimal)
        {
            return "decimal";
        }
        else if (p.GetType() == Consts.tSbyte)
        {
            return "sbyte";
        }
        else if (p.GetType() == Consts.tSbyte)
        {
            return "byte";
        }
        else if (p.GetType() == Consts.tChar)
        {
            return "char";
        }
        else if (p.GetType() == Consts.tShort)
        {
            return "short";
        }
        else if (p.GetType() == Consts.tUshort)
        {
            return "ushort";
        }
        else if (p.GetType() == Consts.tUint)
        {
            return "uint";
        }
        else if (p.GetType() == Consts.tLong)
        {
            return "long";
        }
        else if (p.GetType() == Consts.uUlong)
        {
            return "ulong";
        }
        else
        {
            throw new Exception("Snažíte se vytvořit třídu s nepodporovaným typem");
        }
    }
/// <summary>
    /// NSN
    /// </summary>
    /// <param name="p"></param>
    /// <returns></returns>
    public static string ConvertObjectToDotNetType(Type p)
    {
        if (p == Consts.tString)
        {
            return "string";
        }
        else if (p == Consts.tInt)
        {
            return "int";
        }
        else if (p == Consts.tBool)
        {
            return "bool";
        }
        else if (p == Consts.tDateTime)
        {
            return "DateTime";
        }
        else if (p == Consts.tBinary)
        {
            return "byte[]";
        }
        else if (p == Consts.tFloat)
        {
            return "float";
        }
        else if (p.IsEnum)
        {
            return p.Name;
        }
        else if (p == Consts.tDouble)
        {
            return "double";
        }
        else if (p == Consts.tDecimal)
        {
            return "decimal";
        }
        else if (p == Consts.tSbyte)
        {
            return "sbyte";
        }
        else if (p == Consts.tByte)
        {
            return "byte";
        }
        else if (p == Consts.tChar)
        {
            return "char";
        }
        else if (p == Consts.tShort)
        {
            return "short";
        }
        else if (p == Consts.tUshort)
        {
            return "ushort";
        }
        else if (p == Consts.tUint)
        {
            return "uint";
        }
        else if (p == Consts.tLong)
        {
            return "long";
        }
        else if (p == Consts.uUlong)
        {
            return "ulong";
        }
        else
        {
            throw new Exception("Snažíte se vytvořit název třídy s nepodporovaným typem");
        }
    }
}