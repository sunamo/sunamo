using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
public static class StaticParse
{

    public static string GetString(string[] o, int p)
    {
        string vr = o[p];
        return vr.TrimEnd(AllChars.space);
    }

    public static int GetInt(string[] o, int p)
    {
        //ID,Name,IDArtist
        /*
0-2147483547 Just Like You Live At The Palace -32727
2-2147483546 Let It Die Live At The Palace -32727
0-2147483546 Let It Die Live At The Palace -32727
         */
        //Console.WriteLine(SH.JoinSpace( CA.ToListString( o)));
        //Console.WriteLine(p);

        return int.Parse(o[p]);
    }

    public static float GetFloat(string[] o, int p)
    {
        return float.Parse(o[p]);
    }

    public static long GetLong(string[] o, int p)
    {
        return long.Parse(o[p]);
    }

    /// <summary>
    /// POužívá metodu bool.Parse
    /// </summary>
    /// <param name="o"></param>
    /// <param name="p"></param>
    /// <returns></returns>
    public static bool GetBoolMS(string[] o, int p)
    {
        return bool.Parse(o[p]);
    }

    /// <summary>
    /// Používá metodu Convert.ToBoolean
    /// </summary>
    /// <param name="o"></param>
    /// <param name="p"></param>
    /// <returns></returns>
    public static bool GetBool(string[] o, int p)
    {
        return Convert.ToBoolean(o[p]);
    }

    public static string GetBoolS(string[] o, int p)
    {
        return BTS.BoolToStringEn(GetBool(o, p));
    }

    public static DateTime GetDateTime(string[] o, int p)
    {
        string dd = o[p];
        return DTHelperCs.ParseDateCzech(dd);
    }



    public static string GetDateTimeS(string[] o, int p)
    {
        return DateTime.Parse(o[p].Trim()).ToString();
    }

    public static byte[] GetImage(string[] o, int dex)
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

    public static decimal GetDecimal(string[] o, int p)
    {
        return decimal.Parse(o[p]);
    }

    public static double GetDouble(string[] o, int p)
    {
        return double.Parse(o[p]);
    }

    public static short GetShort(string[] o, int p)
    {
        return short.Parse(o[p]);
    }



    public static byte GetByte(string[] o, int p)
    {
        return byte.Parse(o[p]);
    }

    public static Guid GetGuid(string[] o, int p)
    {
        return Guid.Parse(o[p]);
    }
}
