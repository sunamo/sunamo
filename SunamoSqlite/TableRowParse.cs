using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class TableRowParse
{
    public  static int GetInt(object[] o, int v)
    {
        return int.Parse(o[v].ToString());
    }

    public  static DateTime GetDateTime(object[] o, int v)
    {
        return DateTime.MinValue;
    }

    public  static string GetString(object[] o, int v)
    {
        return string.Empty;
    }

    public  static float GetFloat(object[] o, int p)
    {
        return 0;
    }

    public  static long GetLong(object[] o, int p)
    {
        return 0;
    }

    public  static bool GetBool(object[] o, int p)
    {
        return false;
    }

    public  static string GetBoolS(object[] o, int p)
    {
        return false.ToString();
    }

    public  static string GetDateTimeS(object[] o, int p)
    {
        return string.Empty;
    }

    public  static byte[] GetImage(object[] o, int dex)
    {
        return null;
    }

    public  static decimal GetDecimal(object[] o, int p)
    {
        return 0;
    }

    public  static double GetDouble(object[] o, int p)
    {
        return 0;
    }

    public  static short GetShort(object[] o, int p)
    {
        return 0;
    }

    public  static byte GetByte(object[] o, int p)
    {
        return 0;
    }

    public  static Guid GetGuid(object[] o, int p)
    {
        return Guid.Empty;
    }
}