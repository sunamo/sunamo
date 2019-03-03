using sunamo.Values;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
public partial class MSDatabaseLayerBase
{
    public static SqlConnection conn = null;


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
    }


}