
using sunamo.Data;
using sunamo.Values;
using System;
using System.Collections;
/// <summary>
/// Number Helper Class
/// </summary>
using System.Collections.Generic;
using System.Linq;

public static partial class NH
{ 
    public static int MinForLength(int length)
    {
        return int.Parse("1".PadRight(4, '0'));
    }

    public static int MaxForLength(int length)
    {
        return int.Parse("9".PadRight(4, '9'));
    }

    


    



    public static float AverageFloat(double gridWidth, double columnsCount)
    {
        return (float)Average<double>(gridWidth, columnsCount);
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

    /// <summary>
    /// Vytvoří interval od A1 do A2 včetně
    /// </summary>
    /// <param name="od"></param>
    /// <param name="to"></param>
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

    public static double ReturnTheNearestSmallIntegerNumber(double d)
    {
        return (double)Convert.ToInt32(d);
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


    public static string Round0(float v)
    {
        return Math.Round(v, 0).ToString();
    }
}