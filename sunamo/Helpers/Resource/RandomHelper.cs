using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
/// <summary>
/// M pro gen. nahodnych ruznych typu dat bez pretypovani
/// </summary>
public static partial class RandomHelper
{
    public static void RemoveChars(List<string> p)
    {
        foreach (string item in p)
        {
            if (p.Count == 1)
            {
                vsZnaky.Remove(item[0]);
            }
        }
    }

    public static IntPtr RandomIntPtr()
    {
        IntPtr p = new IntPtr(RandomInt());
        return p;
    }

    private static char RandomNumberChar()
    {
        return RandomElementOfCollection(AllChars.numericChars)[0];
    }

    /// <summary>
    /// Vrac� ��slo od A1 do A2 v�.
    /// </summary>
    /// <param name="od"></param>
    /// <param name="to"></param>
    
    public static float RandomFloat(int p, float maxValue, int maxP)
    {
        if (p > 7)
        {
            p = 7;
        }
        string predCarkou = "";
        if (maxP > 8)
        {
            predCarkou = RandomHelper.RandomNumberString(p);
        }
        else
        {
            predCarkou = RandomInt(maxP + 1).ToString();
        }
        int z = 7 - p;
        float vr = 0;
        if (z != 0)
        {
            string zaCarkou = RandomHelper.RandomNumberString(z);
            vr = float.Parse(predCarkou + AllStrings.dot + zaCarkou);
        }
        else
        {
            vr = float.Parse(predCarkou);
        }
        if (vr > maxValue)
        {
            return maxValue;
        }
        return vr;
    }

    private static string RandomNumberString(int delka)
    {
        delka--;
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i != delka; i++)
        {
            sb.Append(RandomNumberChar());
        }
        return sb.ToString();
    }

    public static DateTime RandomSmallDateTime(int minDaysAdd, int maxDaysAdd)
    {
        DateTime dt = DateTime.Today;
        int pridat = RandomInt(minDaysAdd, maxDaysAdd);
        dt = dt.AddDays(pridat);
        return dt;
    }

    private static float s_lightColorBase = (float)(256 - 229);

    public static byte RandomColorPart(bool light)
    {
        return RandomColorPart(light, 127f);
    }

    public static byte RandomColorPart(bool light, float add)
    {
        if (light)
        {
            float r = RandomFloatBetween0And1();
            r *= s_lightColorBase;
            return (byte)(r + add);
        }
        return RandomByte(0, 255);
    }

    private static float RandomFloatBetween0And1()
    {
        return RandomFloat(1, 1, 0);
    }
}