using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
public static class StaticParse
{

    public static string GetString(List<string> o, int p)
    {
        string vr = o[p];
        return vr.TrimEnd(AllChars.space);
    }

    public static int GetInt(List<string> o, int p)
    {
        //ID,Name,IDArtist
        /*
0-2147483547 Just Like You Live At The Palace -32727
2-2147483546 Let It Die Live At The Palace -32727
0-2147483546 Let It Die Live At The Palace -32727
         */
        //CL.WriteLine(SH.JoinSpace( CA.ToListString( o)));
        //CL.WriteLine(p);

        return int.Parse(o[p]);
    }

    public static float GetFloat(List<string> o, int p)
    {
        return float.Parse(o[p]);
    }

    public static long GetLong(List<string> o, int p)
    {
        return long.Parse(o[p]);
    }

    /// <summary>
    /// POužívá metodu bool.Parse
    /// </summary>
    /// <param name="o"></param>
    /// <param name="p"></param>
    public static bool GetBoolMS(List<string> o, int p)
    {
        return bool.Parse(o[p]);
    }

    /// <summary>
    /// Používá metodu Convert.ToBoolean
    /// </summary>
    /// <param name="o"></param>
    /// <param name="p"></param>
    public static bool GetBool(List<string> o, int p)
    {
        return Convert.ToBoolean(o[p]);
    }

    public static string GetBoolS(List<string> o, int p)
    {
        return BTS.BoolToStringEn(GetBool(o, p));
    }

    public static DateTime GetDateTime(List<string> o, int p)
    {
        string dd = o[p];
        return DTHelperCs.ParseDateCzech(dd);
    }



    public static string GetDateTimeS(List<string> o, int p)
    {
        return DateTime.Parse(o[p].Trim()).ToString();
    }

    public static byte[] GetImage(List<string> o, int dex)
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

    public static decimal GetDecimal(List<string> o, int p)
    {
        return decimal.Parse(o[p]);
    }

    public static double GetDouble(List<string> o, int p)
    {
        return double.Parse(o[p]);
    }

    public static short GetShort(List<string> o, int p)
    {
        return short.Parse(o[p]);
    }

    public static byte GetByte(List<string> o, int p)
    {
        return byte.Parse(o[p]);
    }

    public static Guid GetGuid(List<string> o, int p)
    {
        return Guid.Parse(o[p]);
    }
}