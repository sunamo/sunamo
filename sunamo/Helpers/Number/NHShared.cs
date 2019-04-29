﻿using sunamo.Values;
using System;
/// <summary>
/// Number Helper Class
/// </summary>
using System.Collections.Generic;
using System.Linq;

public static partial class NH{ 
public static string MakeUpTo2NumbersToZero(int p)
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
}