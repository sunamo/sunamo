using sunamo.Values;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
public class MSDatabaseLayerBase
{
    public static SqlConnection conn = null;

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

    public static string GetValues2(object[] sloupce)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("(");
        int to = sloupce.Length;
        for (int i = 0; i < to; i++)
        {
            sb.Append("@p" + (i).ToString() + ",");
        }
        return sb.ToString().TrimEnd(',') + ")";
    }





    /// <summary>
    /// Je stejná jako metoda GetValues, až na to že počítá od 1 a ne od 0
    /// </summary>
    /// <param name="sloupce"></param>
    /// <returns></returns>
    public static string GetValues1(params object[] sloupce)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("(");
        int to = sloupce.Length + 1;
        for (int i = 1; i < to; i++)
        {
            sb.Append("@p" + (i).ToString() + ",");
        }
        return sb.ToString().TrimEnd(',') + ")";
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
                break;
            case SqlDbType.Int:
                return "int";
                break;
            case SqlDbType.Real:
                return "float";
                break;
            case SqlDbType.BigInt:
                return "long";
                break;
            case SqlDbType.Bit:
                return "bool";
                break;
            case SqlDbType.Date:
            case SqlDbType.DateTime:
            case SqlDbType.DateTime2:
            case SqlDbType.Time:
            case SqlDbType.DateTimeOffset:
            case SqlDbType.SmallDateTime:
                return "DateTime";
                break;
            // Bude to až po všech běžně používaných datových typech, protože bych se měl vyvarovat ukládat do malé DB takové množství dat
            case SqlDbType.Timestamp:
            case SqlDbType.Binary:
            case SqlDbType.VarBinary:
            case SqlDbType.Image:
                return "byte[]";
                break;
            case SqlDbType.SmallMoney:
            case SqlDbType.Money:
            case SqlDbType.Decimal:
                return "decimal";
                break;
            case SqlDbType.Float:
                return "double";
                break;
            case SqlDbType.SmallInt:
                return "short";
                break;
            case SqlDbType.TinyInt:
                return "byte";
                break;
            case SqlDbType.Structured:
            case SqlDbType.Udt:
            case SqlDbType.Xml:
                throw new Exception("Snažíte se převést na int strukturovaný(složitý) datový typ");
                break;
            case SqlDbType.UniqueIdentifier:
                return "Guid";
                break;

            case SqlDbType.VarChar:
                return "string";
                break;
            case SqlDbType.Variant:
                return "object";
                break;
            default:
                throw new Exception("Snažíte se převést datový typ, pro který není implementována větev");
                break;
        }
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

    /// <summary>
    /// Jsou rozděleny do 2 dict ze 2 důvodů: 
    /// 1) aby se rychleji získavali popisy daných datových typů
    /// 2) aby jsem odlišil a zaznamenal typy které chci používat a které nikoliv
    /// </summary>
    public static Dictionary<SqlDbType, string> usedTa = new Dictionary<SqlDbType, string>();
    public static Dictionary<SqlDbType, string> hiddenTa = new Dictionary<SqlDbType, string>();

    static MSDatabaseLayerBase()
    {
        #region Používané datové typy
        usedTa.Add(SqlDbType.SmallDateTime, "Datum a čas");
        usedTa.Add(SqlDbType.Real, "Číslo s desetinnou čárkou");
        usedTa.Add(SqlDbType.Int, "Číslo bez desetinné čárky v rozsahu od -2,147,483,648 do 2,147,483,647");
        usedTa.Add(SqlDbType.NVarChar, "Omezený řetězec na max. 4000 znaků");
        usedTa.Add(SqlDbType.Bit, "Duální hodnota pravda/nepravda");
        usedTa.Add(SqlDbType.TinyInt, "Číslo bez desetinné čárky v rozsahu od 0 do 255");
        usedTa.Add(SqlDbType.SmallInt, "Číslo bez desetinné čárky v rozsahu od -32,768 do 32,767");
        usedTa.Add(SqlDbType.Binary, "Zápis bajtů v rozmezí velikosti 1-8000");


        #endregion

        #region Skryté datové typy

        // Je to sice nejlepší možná varianta(Text) pro ukládání textů, ale i tak to NEpatří do DB
        hiddenTa.Add(SqlDbType.Text, "Neomezený řetězec");
        hiddenTa.Add(SqlDbType.VarChar, "Omezený řetězec na 8000 znaků");
        hiddenTa.Add(SqlDbType.NText, "Omezený řetězec na 1073741823");
        hiddenTa.Add(SqlDbType.VarBinary, "Zápis bajtů v rozmezí velikosti 1-8000");
        hiddenTa.Add(SqlDbType.Image, "Zápis bajtů v rozmezí velikosti 0-2147483647");

        hiddenTa.Add(SqlDbType.Char, "Řetězec non-unicode s pevnou šířkou do velikosti 8000 znaků");
        hiddenTa.Add(SqlDbType.NChar, "Řetězec Unicode s pevnou šířkou do velikosti 4000 znaků");

        #endregion
    }

    public static List<object> GetUsedDataTypes()
    {
        List<object> vr = new List<object>();
        vr.Add("");
        foreach (var item in usedTa)
        {
            vr.Add(item.Key);
        }
        return vr;
    }

    public static SqlDbType GetProbablyDataType(string p)
    {
        string trim = p.Trim();

        if (trim == "NULL" || trim == "")
        {
            return SqlDbType.NVarChar;
        }
        if (trim.StartsWith("BLOB("))
        {
            return SqlDbType.VarBinary;
        }
        string[] d = SH.SplitNone(trim, ",");
        if (d.Length == 2)
        {
            if (BTS.IsInt(d[0]) && BTS.IsInt(d[1]))
            {
                return SqlDbType.Real;
            }
        }
        else
        {
            if (BTS.ParseByte(trim) != byte.MinValue)
            {
                return SqlDbType.TinyInt;
            }
            if (BTS.ParseShort(trim) != short.MinValue)
            {
                return SqlDbType.SmallInt;
            }
            if (BTS.ParseInt(trim) != int.MinValue)
            {
                return SqlDbType.Int;
            }
            else if (BTS.IsDateTime(trim))
            {
                return SqlDbType.SmallDateTime;
            }
            else if (BTS.IsBool(trim))
            {
                return SqlDbType.Bit;
            }
        }
        return SqlDbType.NVarChar; ;
    }

    public static SqlDbType GetSqlDbTypeFromType(Type type)
    {

        string t = type.ToString();
        switch (t)
        {
            case "System.Int32":
                return SqlDbType.Int;
            case "System.DateTime":
                return SqlDbType.SmallDateTime;
            case "System.Single":
                return SqlDbType.Real;
            case "System.String":
                return SqlDbType.NVarChar;
            case "System.Boolean":
                return SqlDbType.Bit;
            case "System.Int16":
                return SqlDbType.SmallInt;
            case "System.Byte":
                return SqlDbType.TinyInt;
            default:
                throw new Exception("Neimplementovaná větev");
                break;
        }
    }

    public static object ConvertStringToObject(string vstup, SqlDbType type)
    {
        vstup = vstup.Trim();
        switch (type)
        {
            case SqlDbType.NVarChar:
                return vstup;
            case SqlDbType.Int:
                return int.Parse(vstup);
            case SqlDbType.Bit:
                return bool.Parse(vstup);
            case SqlDbType.SmallDateTime:
                return DateTime.Parse(vstup);
            case SqlDbType.Real:
                return float.Parse(vstup);
            case SqlDbType.TinyInt:
                return byte.Parse(vstup);
            case SqlDbType.SmallInt:
                return short.Parse(vstup);

            case SqlDbType.BigInt:

            case SqlDbType.Binary:


            case SqlDbType.Char:

            case SqlDbType.Date:

            case SqlDbType.DateTime:

            case SqlDbType.DateTime2:

            case SqlDbType.DateTimeOffset:

            case SqlDbType.Decimal:

            case SqlDbType.Float:

            case SqlDbType.Image:


            case SqlDbType.Money:

            case SqlDbType.NChar:

            case SqlDbType.NText:





            case SqlDbType.SmallMoney:

            case SqlDbType.Structured:

            case SqlDbType.Text:

            case SqlDbType.Time:

            case SqlDbType.Timestamp:



            case SqlDbType.Udt:

            case SqlDbType.UniqueIdentifier:

            case SqlDbType.VarBinary:

            case SqlDbType.VarChar:

            case SqlDbType.Variant:

            case SqlDbType.Xml:

            default:
                throw new Exception("Nepodporovaný datový typ");

        }
        return vstup;
    }


}
