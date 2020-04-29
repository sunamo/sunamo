using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
public static class MSTableRowParse
{

    public static string GetString(object[] o, int p)
    {
        string vr = o[p].ToString();
        return vr.TrimEnd(AllChars.space);
    }

    public static int GetInt(object[] o, int p)
    {
        return GetNullableInt(o, p).Value;
    }

    public static int? GetNullableInt(object[] o, int p)
    {
        //ID,Name,IDArtist
        /*
0-2147483547 Just Like You Live At The Palace -32727
2-2147483546 Let It Die Live At The Palace -32727
0-2147483546 Let It Die Live At The Palace -32727
         */
        //Console.WriteLine(SH.JoinSpace( CA.ToListString( o)));
        //Console.WriteLine(p);

        var value = o[p];
        if (SqlServerHelper.IsNull(value))
        {
            return null;
        }
        return int.Parse(value.ToString());
    }

    static bool IsNull(object v)
    {
        return SqlServerHelper.IsNull(v);
    }

    public static float GetFloat(object[] o, int p)
    {
        return GetNullableFloat(o, p).Value;
    }

    public static float? GetNullableFloat(object[] o, int p)
    {
        var value = o[p];
        if (SqlServerHelper.IsNull(value))
        {
            return null;
        }
        return float.Parse(value.ToString());
    }

    public static long GetLong(object[] o, int p)
    {
        return GetNullableLong(o, p).Value;
    }

    public static long? GetNullableLong(object[] o, int p)
    {
        var value = o[p];
        if (SqlServerHelper.IsNull(value))
        {
            return null;
        }
        return long.Parse(value.ToString());
    }



    /// <summary>
    /// POužívá metodu bool.Parse
    /// </summary>
    /// <param name="o"></param>
    /// <param name="p"></param>
    public static bool GetBoolMS(object[] o, int p)
    {
        return bool.Parse(o[p].ToString());
    }

    public static bool GetBool(object[] o, int p)
    {
        return GetNullableBool(o, p).Value;
    }

    /// <summary>
    /// Používá metodu Convert.ToBoolean
    /// </summary>
    /// <param name="o"></param>
    /// <param name="p"></param>
    public static bool? GetNullableBool(object[] o, int p)
    {
        var value = o[p];
        if (SqlServerHelper.IsNull(value))
        {
            return null;
        }
        return Convert.ToBoolean(value);
    }

    public static string GetBoolS(object[] o, int p)
    {
        return BTS.BoolToStringEn(GetBool(o, p));
    }

    public static DateTime GetDateTime(object[] o, int p)
    {
        return GetNullableDateTime(o, p).Value;
    }

    public static DateTime? GetNullableDateTime(object[] o, int p)
    {
        var value = o[p];
        if (SqlServerHelper.IsNull(value))
        {
            return null;
        }
        string dd = value.ToString();
        return DateTime.Parse(dd);
    }



    public static string GetDateTimeS(object[] o, int p)
    {
        return DateTime.Parse(o[p].ToString().Trim()).ToString();
    }

    public static byte[] GetImage(object[] o, int dex)
    {
        object obj = o[dex];
        if (SqlServerHelper.IsNull(obj))
            return null;
        else
        {
            BinaryFormatter bf = new BinaryFormatter();
            using (MemoryStream ms = new MemoryStream())
            {
                bf.Serialize(ms, obj); //now in Memory Stream
                ms.ToArray(); // Array object
                ms.Seek(0, SeekOrigin.Begin);

                object o2 = bf.Deserialize(ms);
                if (o2.GetType() != typeof(System.DBNull))
                {
                    return (byte[])o2;
                }
                return new byte[0];
            }
        }
    }

    public static decimal GetDecimal(object[] o, int p)
    {
        return GetNullableDecimal(o, p).Value;
    }

    public static decimal? GetNullableDecimal(object[] o, int p)
    {
        var value = o[p];
        if (SqlServerHelper.IsNull(value))
        {
            return null;
        }
        return decimal.Parse(value.ToString());
    }

    public static double GetDouble(object[] o, int p)
    {
        return double.Parse(o[p].ToString());
    }

    public static short GetShort(object[] o, int p)
    {
        return GetNullableShort(o, p).Value;
    }

    public static short? GetNullableShort(object[] o, int p)
    {
        var value = o[p];
        if (SqlServerHelper.IsNull(value))
        {
            return null;
        }
        return short.Parse(value.ToString());
    }

    public static byte GetByte(object[] o, int p)
    {
        return GetNullableByte(o, p).Value;
    }

    public static byte? GetNullableByte(object[] o, int p)
    {
        var value = o[p];
        if (SqlServerHelper.IsNull(value))
        {
            return null;
        }
        return byte.Parse(value.ToString());
    }

    public static Guid GetGuid(object[] o, int p)
    {
        return GetNullableGuid(o, p).Value;
    }

    public static Guid? GetNullableGuid(object[] o, int p)
    {
        var value = o[p];
        if (SqlServerHelper.IsNull(value))
        {
            return null;
        }
        return Guid.Parse(value.ToString());
    }
}