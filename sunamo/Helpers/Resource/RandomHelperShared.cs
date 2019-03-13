using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

public static partial class RandomHelper
{
    static Random rnd = new Random();

    public static T RandomElementOfCollectionT<T>(IList<T> ppk)
    {
        int nt = RandomInt(ppk.Count);
        return ppk[nt];
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

    public static T RandomElementOfCollectionT<T>(IEnumerable<T> ppk)
    {
        List<T> col = new List<T>();
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

public static string RandomElementOfArray(Array ppk)
    {
        int nt = RandomInt(ppk.Length);
        return ppk.GetValue(nt).ToString();
    }

public static int RandomInt()
    {
        return rnd.Next(0, int.MaxValue);
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
    /// Vr�t� mi n�hodn� znak pouze z velk�ch, mal�ch p�smen a ��slic
    /// </summary>
    /// <returns></returns>
    public static char RandomCharWithoutSpecial()
    {
        return RandomElementOfCollection(vsZnakyWithoutSpecial)[0];
    }

    public static List<char> vsZnaky = null;
    public static List<char> vsZnakyWithoutSpecial = null;
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

public static byte[] RandomBytes(int kolik)
    {
        byte[] b = new byte[kolik];
        for (int i = 0; i < kolik; i++)
        {
            b[i] = (byte)rnd.Next(0, byte.MaxValue);
        }
        return b;
    }

public static char RandomChar()
    {
        return RandomElementOfCollection(vsZnaky)[0];
    }
}