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
    public static byte RandomByte(int od, int to)
    {
        return (byte)s_rnd.Next(od, to + 1);
    }

    public static List<string> RandomElementsOfCollection(IList sou, int pol)
    {
        List<string> vr = new List<string>();
        for (int i = 0; i < pol; i++)
        {
            vr.Add(RandomElementOfCollection(sou));
        }
        return vr;
    }


    /// <summary>
    /// A1 je po�et ��sel p�ed des. ��rkou. Pokud bude men�� ne� 7, automaticky se dopln� i ��sla za desetinnou ��rku.
    /// A2 je maxim�ln� hodnota v�sledn�ho ��sla. Pokud bude vypo�ten� vy��� ne� tato, vr�t�m tuto. Vhodn�� nastavit na float.MaxValue
    /// A3 je ��slo, kter� m��e b�t nejvy��� jako ��slo p�ed des. ��rkou - finta, pokud chce� vygenerovat ��slo mezi 0 a 1 (exclude), zadej do t�to hodntoy 0 a po��te�n� ��slo bude v�dy tak 0. Pro nejvy��� mo�n� ��slo nastav na float.MaxValue nebo int.MaxValue
    /// 
    /// </summary>
    /// <param name="p"></param>
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