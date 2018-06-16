using System;
/// <summary>
/// Number Helper Class
/// </summary>
using System.Collections.Generic;

public static class NH
{
    public static string MakeUpTo2NumbersToZero(int p)
    {
        string s = p.ToString();
        if (s.Length == 1)
        {
            return "0" + p;
        }
        return s;
    }

    public static double Average(double gridWidth, int columnsCount)
    {
        //int columnsCount = g.ColumnDefinitions.Count;
        if (columnsCount == 0)
        {
            return 0;
        }

        //double gridWidth = g.ActualWidth;
        if (gridWidth == 0)
        {
            return 0;
        }

        return gridWidth / columnsCount;
    }

    public static string MakeUpTo3NumbersToZero(int p)
    {
        string ps = p.ToString();
        int delka = ps.Length;
        if (delka == 1)
        {
            return "00" + ps;
        }
        else if (delka == 2)
        {
            return "0" + ps;
        }
        return ps;
    }

    public static string MakeUpTo2NumbersToZero(byte p)
    {
        string s = p.ToString();
        if (s.Length == 1)
        {
            return "0" + p;
        }
        return s;
    }

    /// <summary>
    /// Vytvoří interval od A1 do A2 včetně
    /// </summary>
    /// <param name="od"></param>
    /// <param name="to"></param>
    /// <returns></returns>
    public static List<short> GenerateIntervalShort(short od, short to)
    {
        List<short> vr = new List<short>();
        for (short i = od; i < to; i++)
        {
            vr.Add(i);
        }
        vr.Add(to);
        return vr;
    }

    /// <summary>
    /// Vytvoří interval od A1 do A2 včetně
    /// </summary>
    /// <param name="od"></param>
    /// <param name="to"></param>
    /// <returns></returns>
    public static List<int> GenerateIntervalInt(int od, int to)
    {
        List<int> vr = new List<int>();
        for (int i = od; i < to; i++)
        {
            vr.Add(i);
        }
        vr.Add(to);
        return vr;
    }

    public static List<byte> GenerateIntervalByte(byte od, byte to)
    {
        List<byte> vr = new List<byte>();
        for (byte i = od; i < to; i++)
        {
            vr.Add(i);
        }
        vr.Add(to);
        return vr;
    }

    public static double ReturnTheNearestSmallIntegerNumber(double d)
    {
        return (double)Convert.ToInt32(d);
    }

    public static float RoundAndReturnInInputType(float ugtKm, int v)
    {
        string vr = Math.Round(ugtKm, v).ToString();
        return float.Parse(vr);
    }

    public static List<int> Invert(List<int> arr, int changeTo, int finalCount)
    {
        List<int> vr = new List<int>(finalCount);
        for (int i = 0; i < finalCount; i++)
        {
            if (arr.Contains(i))
            {
                vr.Add(arr[arr.IndexOf(i)]);
            }
            else
            {
                vr.Add(changeTo);
            }
        }
        return vr;
    }
}
