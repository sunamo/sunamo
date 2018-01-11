using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
public static class MSTableRowParse
{

    public static string GetString(object[] o, int p)
    {
        string vr = o[p].ToString();
        return vr.TrimEnd(' ');
    }

    public static int GetInt(object[] o, int p)
    {
        return int.Parse(o[p].ToString());
    }

    public static float GetFloat(object[] o, int p)
    {
        return float.Parse(o[p].ToString());
    }

    public static long GetLong(object[] o, int p)
    {
        return long.Parse(o[p].ToString());
    }

    /// <summary>
    /// POužívá metodu bool.Parse
    /// </summary>
    /// <param name="o"></param>
    /// <param name="p"></param>
    /// <returns></returns>
    public static bool GetBoolMS(object[] o, int p)
    {
        return bool.Parse(o[p].ToString());
    }

    /// <summary>
    /// Používá metodu Convert.ToBoolean
    /// </summary>
    /// <param name="o"></param>
    /// <param name="p"></param>
    /// <returns></returns>
    public static bool GetBool(object[] o, int p)
    {
        return Convert.ToBoolean(o[p]);
    }

    public static string GetBoolS(object[] o, int p)
    {
        return BTS.BoolToStringCs(GetBool(o,p));
    }

    public static DateTime GetDateTime(object[] o, int p)
    {
        string dd = o[p].ToString();
        return DateTime.Parse(dd);
    }



    public static string GetDateTimeS(object[] o, int p)
    {
        return DateTime.Parse(o[p].ToString().Trim()).ToString();
    }

    public static byte[] GetImage(object[] o, int dex)
    {
        object obj = o[dex];
        if (obj == System.DBNull.Value)
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
        return decimal.Parse(o[p].ToString());
    }

    public static double GetDouble(object[] o, int p)
    {
        return double.Parse(o[p].ToString());
    }

    public static short GetShort(object[] o, int p)
    {
        return short.Parse(o[p].ToString());
    }



    public static byte GetByte(object[] o, int p)
    {
        return byte.Parse(o[p].ToString());
    }

    public static Guid GetGuid(object[] o, int p)
    {
        return Guid.Parse(o[p].ToString());
    }
}
