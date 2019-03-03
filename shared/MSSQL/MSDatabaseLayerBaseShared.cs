using sunamo.Values;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

public partial class MSDatabaseLayerBase{
    /// <summary>
    /// Jsou rozděleny do 2 dict ze 2 důvodů: 
    /// 1) aby se rychleji získavali popisy daných datových typů
    /// 2) aby jsem odlišil a zaznamenal typy které chci používat a které nikoliv
    /// </summary>
    public static Dictionary<SqlDbType, string> usedTa = new Dictionary<SqlDbType, string>();
    public static Dictionary<SqlDbType, string> hiddenTa = new Dictionary<SqlDbType, string>();

    static MSDatabaseLayerBase()
    {
        usedTa.Add(SqlDbType.SmallDateTime, "Datum a čas");
        usedTa.Add(SqlDbType.Real, "Číslo s desetinnou čárkou");
        usedTa.Add(SqlDbType.Int, "Číslo bez desetinné čárky v rozsahu od -2,147,483,648 do 2,147,483,647");
        usedTa.Add(SqlDbType.NVarChar, "Omezený řetězec na max. 4000 znaků");
        usedTa.Add(SqlDbType.Bit, "Duální hodnota pravda/nepravda");
        usedTa.Add(SqlDbType.TinyInt, "Číslo bez desetinné čárky v rozsahu od 0 do 255");
        usedTa.Add(SqlDbType.SmallInt, "Číslo bez desetinné čárky v rozsahu od -32,768 do 32,767");
        usedTa.Add(SqlDbType.Binary, "Zápis bajtů v rozmezí velikosti 1-8000");




        // Je to sice nejlepší možná varianta(Text) pro ukládání textů, ale i tak to NEpatří do DB
        hiddenTa.Add(SqlDbType.Text, "Neomezený řetězec");
        hiddenTa.Add(SqlDbType.VarChar, "Omezený řetězec na 8000 znaků");
        hiddenTa.Add(SqlDbType.NText, "Omezený řetězec na 1073741823");
        hiddenTa.Add(SqlDbType.VarBinary, "Zápis bajtů v rozmezí velikosti 1-8000");
        hiddenTa.Add(SqlDbType.Image, "Zápis bajtů v rozmezí velikosti 0-2147483647");

        hiddenTa.Add(SqlDbType.Char, "Řetězec non-unicode s pevnou šířkou do velikosti 8000 znaků");
        hiddenTa.Add(SqlDbType.NChar, "Řetězec Unicode s pevnou šířkou do velikosti 4000 znaků");

    }

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

public static string ConvertSqlDbTypeToDotNetType(SqlDbType p)
    {
        switch (p)
        {
            case SqlDbType.Text:
            case SqlDbType.Char:
            case SqlDbType.NText:
            case SqlDbType.NChar:
            case SqlDbType.NVarChar:
                return "string";
                
            case SqlDbType.Int:
                return "int";
                
            case SqlDbType.Real:
                return "float";
                
            case SqlDbType.BigInt:
                return "long";
                
            case SqlDbType.Bit:
                return "bool";
                
            case SqlDbType.Date:
            case SqlDbType.DateTime:
            case SqlDbType.DateTime2:
            case SqlDbType.Time:
            case SqlDbType.DateTimeOffset:
            case SqlDbType.SmallDateTime:
                return "DateTime";
                
            // Bude to až po všech běžně používaných datových typech, protože bych se měl vyvarovat ukládat do malé DB takové množství dat
            case SqlDbType.Timestamp:
            case SqlDbType.Binary:
            case SqlDbType.VarBinary:
            case SqlDbType.Image:
                return "byte[]";
                
            case SqlDbType.SmallMoney:
            case SqlDbType.Money:
            case SqlDbType.Decimal:
                return "decimal";
                
            case SqlDbType.Float:
                return "double";
                
            case SqlDbType.SmallInt:
                return "short";
                
            case SqlDbType.TinyInt:
                return "byte";
                
            case SqlDbType.Structured:
            case SqlDbType.Udt:
            case SqlDbType.Xml:
                throw new Exception("Snažíte se převést na int strukturovaný(složitý) datový typ");
                
            case SqlDbType.UniqueIdentifier:
                return "Guid";
                

            case SqlDbType.VarChar:
                return "string";
                
            case SqlDbType.Variant:
                return "object";
                
            default:
                throw new Exception("Snažíte se převést datový typ, pro který není implementována větev");
        }
    }
}