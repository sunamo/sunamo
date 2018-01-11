using System;
using System.Data;

public class ConvertSqlDbType 
{
    public static SqlDbType2 FromSqlDbType(SqlDbType db)
    {
        switch (db)
        {
            case SqlDbType.SmallDateTime:
            case SqlDbType.Real:
            case SqlDbType.Int:
            case SqlDbType.NVarChar:
            case SqlDbType.NChar:
            case SqlDbType.NText:
            case SqlDbType.Bit:
            case SqlDbType.TinyInt:
            case SqlDbType.SmallInt:
            case SqlDbType.Binary:
                break;

            case SqlDbType.BigInt:
            case SqlDbType.Char:
            case SqlDbType.Date:
            case SqlDbType.DateTime:
            case SqlDbType.DateTime2:
            case SqlDbType.DateTimeOffset:
            case SqlDbType.Decimal:
            case SqlDbType.Float:
            case SqlDbType.Image:
            case SqlDbType.Money:
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
                throw new Exception("Program nezná pro výčtový typ SqlDbType2 hodnotu " + db.ToString());
        }
        return (SqlDbType2)Enum.Parse(typeof(SqlDbType2), db.ToString());
    }

    public static SqlDbType ToSqlDbType(SqlDbType2 db, out bool isNewId )
    {
        isNewId = false;
        if (db == SqlDbType2.UniqueIdentifierAutoNewId)
        {
            isNewId = true;
            db = SqlDbType2.UniqueIdentifier;
        }
        return (SqlDbType)Enum.Parse(typeof(SqlDbType), db.ToString());
    }
}
