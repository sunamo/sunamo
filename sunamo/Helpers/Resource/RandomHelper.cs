using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
/// <summary>
/// M pro gen. nahodnych ruznych typu dat bez pretypovani
/// </summary>
public static class RandomHelper
{
    
    public static List<char> vsZnaky = null;
    public static List<char> vsZnakyWithoutSpecial = null;
    public static void RemoveChars(string[] p)
    {
        foreach (string item in p)
        {
            if (p.Length == 1)
            {
                vsZnaky.Remove(item[0]);
            }
        }
    }

    

    static RandomHelper()
    {
        vsZnaky = new List<char>(AllChars.lowerChars.Count + AllChars.numericChars.Count + AllChars.specialChars.Count + AllChars.upperChars.Count);
        vsZnaky.AddRange(AllChars.lowerChars);
        vsZnaky.AddRange(AllChars.numericChars);
        vsZnaky.AddRange(AllChars.specialChars);
        vsZnaky.AddRange(AllChars.upperChars);

        vsZnakyWithoutSpecial = new List<char>(AllChars.lowerChars.Count + AllChars.numericChars.Count + AllChars.upperChars.Count);
        vsZnakyWithoutSpecial.AddRange(AllChars.lowerChars);
        vsZnakyWithoutSpecial.AddRange(AllChars.numericChars);
        vsZnakyWithoutSpecial.AddRange(AllChars.upperChars);
        
        #region Generov�ni postaru
        #endregion
    }

    

    static Random rnd = new Random();
    #region Gen. nahodnych posloupnosti - jen pro vitrni potreby. Pro praci s poli je v T KolekcePole
    public static byte[] RandomBytes(int kolik)
    {
        byte[] b = new byte[kolik];
        for (int i = 0; i < kolik; i++)
        {
            b[i] = (byte)rnd.Next(0, byte.MaxValue);
        }
        return b;
    }


    /// <summary>
    /// Zad�vej ��slo o 1 v�t�� ne� skute�n� po�et znak� kter� chce�
    /// Vr�t� mi n�hodn� �et�zec pouze z velk�ch, mal�ch p�smen a ��slic
    /// </summary>
    /// <param name="delka"></param>
    /// <returns></returns>
    public static string RandomStringWithoutSpecial(int delka)
    {
        delka--;
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i != delka; i++)
        {
            sb.Append(RandomCharWithoutSpecial());
        }
        return sb.ToString();
    }

    public static string RandomString(int delka)
    {
        delka--;
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i != delka; i++)
        {
            sb.Append(RandomChar());
        }
        return sb.ToString();
    }

    public static string RandomString(int delka, bool upper, bool lower, bool numeric, bool special)
    {
        List<char> ch = new List<char>();
        if (lower)
        {
            ch.AddRange(AllChars.lowerChars);
        }
        if (numeric)
        {
            ch.AddRange(AllChars.numericChars);
        }
        if (special)
        {
            ch.AddRange(AllChars.specialChars);
        }
        if (upper)
        {
            ch.AddRange(AllChars.upperChars);
        }

        delka--;
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i != delka; i++)
        {
            sb.Append(RandomElementOfCollection(ch));
        }
        return sb.ToString();
    }

    public static string RandomString()
    {
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < 7; i++)
        {
            sb.Append(RandomChar());
        }
        return sb.ToString();
    }

    public static char RandomChar()
    {
        return RandomElementOfCollection(vsZnaky)[0];
    }
    /// <summary>
    /// Vr�t� mi n�hodn� znak pouze z velk�ch, mal�ch p�smen a ��slic
    /// </summary>
    /// <returns></returns>
    public static char RandomCharWithoutSpecial()
    {
        return RandomElementOfCollection(vsZnakyWithoutSpecial)[0];
    }

    private static char RandomNumberChar()
    {
        return RandomElementOfCollection(AllChars.numericChars)[0];
    }

    public static int RandomInt()
    {
        return rnd.Next(0, int.MaxValue);
    }

    /// <summary>
    /// Vr�t� ��slo mezi 0 a A1-1
    /// </summary>
    /// <param name="to"></param>
    /// <returns></returns>
    public static int RandomInt(int to)
    {

        return rnd.Next(0, to);
    }

    /// <summary>
    /// Vr�t� ��slo mezi A1 a A2 v�etn�
    /// </summary>
    /// <param name="od"></param>
    /// <param name="to"></param>
    /// <returns></returns>
    public static int RandomInt(int od, int to)
    {

        return rnd.Next(od, to+1);
    }

    /// <summary>
    /// Vrac� ��slo od A1 do A2 v�.
    /// </summary>
    /// <param name="od"></param>
    /// <param name="to"></param>
    /// <returns></returns>
    public static byte RandomByte(int od, int to)
    {
        return (byte)rnd.Next(od, to + 1);
    }

    /// <summary>
    /// Hod� se pro po��tan� index� proto�e vrac� ��slo mezi A1 do A2-1
    /// </summary>
    /// <param name="od"></param>
    /// <param name="to"></param>
    /// <returns></returns>
    public static byte RandomByte2(int od, int to)
    {
        return (byte)rnd.Next(od, to);
    }
    /// <summary>
    /// Vr�t� ��slo mezi 0 a A1-1
    /// </summary>
    /// <param name="to"></param>
    /// <returns></returns>
    public static short RandomShort(short to)
    {
        return (short)rnd.Next(0, to);
    }

    /// <summary>
    /// Vr�t� ��slo mezi A1 v�etn� a A2+1 v�etn�
    /// </summary>
    /// <param name="to"></param>
    /// <returns></returns>
    public static short RandomShort(short from, short to)
    {
        return (short)rnd.Next(from, to + 1);
    }

    /// <summary>
    /// Vr�t� ��slo mezi 0 a short.MaxValue-1
    /// </summary>
    /// <returns></returns>
    public static short RandomShort()
    {
        return (short)rnd.Next(0, short.MaxValue);
    }

    public static bool RandomBool()
    {
        int nt = RandomInt(2);
        string pars = "";
        if (nt == 0)
        {
            pars = bool.FalseString;
        }
        else
        {
            pars = bool.TrueString;
        }
        return bool.Parse(pars);
    }

    public static T RandomElementOfCollectionT<T>(IList<T> ppk)
    {
        int nt = RandomInt(ppk.Count);
        return ppk[nt];
    }

    public static T RandomElementOfCollectionT<T>(IEnumerable<T> ppk)
    {
        List < T > col = new List<T>();
        foreach (var item in ppk)
        {
            col.Add(item);
        }

        return RandomElementOfCollectionT<T>(col);
    }

    public static string RandomElementOfCollection(IList ppk)
    {
        int nt = RandomInt(ppk.Count);
        return ppk[nt].ToString();
    }

    public static string RandomElementOfArray(Array ppk)
    {
        int nt = RandomInt(ppk.Length);
        return ppk.GetValue(nt).ToString();
    }
    #endregion

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
    /// Vrac� ��slo od A1 do A2-1
    /// </summary>
    /// <param name="od"></param>
    /// <param name="to"></param>
    /// <returns></returns>
    public static int RandomInt2(int od, int to)
    {
        return rnd.Next(od, to);
    }

    /// <summary>
    /// A1 je po�et ��sel p�ed des. ��rkou. Pokud bude men�� ne� 7, automaticky se dopln� i ��sla za desetinnou ��rku.
    /// A2 je maxim�ln� hodnota v�sledn�ho ��sla. Pokud bude vypo�ten� vy��� ne� tato, vr�t�m tuto. Vhodn�� nastavit na float.MaxValue
    /// A3 je ��slo, kter� m��e b�t nejvy��� jako ��slo p�ed des. ��rkou - finta, pokud chce� vygenerovat ��slo mezi 0 a 1 (exclude), zadej do t�to hodntoy 0 a po��te�n� ��slo bude v�dy tak 0. Pro nejvy��� mo�n� ��slo nastav na float.MaxValue nebo int.MaxValue
    /// 
    /// </summary>
    /// <param name="p"></param>
    /// <returns></returns>
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
            vr = float.Parse(predCarkou + "." + zaCarkou);
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
        dt =dt.AddDays(pridat);
        return dt;
    }

    static float lightColorBase = (float)(256 - 229);

    public static byte RandomColorPart(bool light)
    {
        return RandomColorPart(light, 127f);
    }

    public static byte RandomColorPart(bool light, float add)
    {
        if (light)
        {
            float r = RandomFloatBetween0And1();
            r *= lightColorBase;
            return (byte)(r + add);
        }
        return RandomByte(0, 255);
    }

    private static float RandomFloatBetween0And1()
    {
        return RandomFloat(1, 1, 0);
    }
}


