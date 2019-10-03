using sunamo.Values;
using System;
/// <summary>
/// Number Helper Class
/// </summary>
using System.Collections.Generic;
using System.Linq;

public static partial class NH
{
   




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

    public static float RoundAndReturnInInputType(float ugtKm, int v)
    {
        string vr = Math.Round(ugtKm, v).ToString();
        return float.Parse(vr);
    }

    public static void RemoveEndingZeroPadding(List<byte> bajty)
    {
        for (int i = bajty.Count - 1; i >= 0; i--)
        {
            if (bajty[i] == 0)
            {
                bajty.RemoveAt(i);
            }
            else
            {
                break;
            }
        }
    }

    /// <summary>
    /// Cast A1,2 to double and divide
    /// </summary>
    /// <param name="textC"></param>
    /// <param name="diac"></param>
    /// <returns></returns>
    public static double Divide(object textC, object diac)
    {
        return double.Parse( textC.ToString()) / double.Parse( diac.ToString());
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
public static string MakeUpTo2NumbersToZero(int p)
    {
        string s = p.ToString();
        if (s.Length == 1)
        {
            return "0" + p;
        }
        return s;
    }
}