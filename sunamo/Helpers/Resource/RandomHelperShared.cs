using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

public static partial class RandomHelper
{
    
    private static float s_lightColorBase = (float)(256 - 229);

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

    private static char RandomNumberChar()
    {
        return RandomElementOfCollection(AllChars.numericChars)[0];
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

    public static byte RandomByte(int od, int toInclude)
    {
        return (byte)s_rnd.Next(od, toInclude + 1);
    }

    public static byte RandomColorPart(bool light)
    {
        return RandomColorPart(light, 127f);
    }

    private static float RandomFloatBetween0And1()
    {
        return RandomFloat(1, 1, 0);
    }

    

    /// <summary>
    /// better is take keys from dict and RandomElementOfCollection
    /// </summary>
    /// <typeparam name="Key"></typeparam>
    /// <typeparam name="Value"></typeparam>
    /// <param name="dict"></param>
    public static Key RandomKeyOfDictionary<Key, Value>(Dictionary<Key,Value> dict)
    {
        return default(Key);
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

    public static T RandomElementOfCollectionT<T>(IList<T> ppk)
    {
        if (ppk.Count == 0)
        {
            return default(T);
        }
        int nt = RandomInt(ppk.Count);
        return ppk[nt];
    }

    

    public static T RandomEnum<T>() where T  : struct
    {
        var v = EnumHelper.GetValues<T>();
        var s = RandomElementOfCollectionT<T>(v);
        return s;
    }

    /// <summary>
    /// Vrac� ��slo od A1 do A2-1
    /// </summary>
    /// <param name="od"></param>
    /// <param name="to"></param>
    public static int RandomInt2(int od, int to)
    {
        return s_rnd.Next(od, to);
    }

    public static string RandomElementOfCollection(Array ppk)
    {
        int nt = RandomInt(ppk.Length);
        return ppk.GetValue(nt).ToString();
    }

    public static int RandomInt()
    {
        return s_rnd.Next(0, int.MaxValue);
    }
    /// <summary>
    /// Vr�t� ��slo mezi A1 a A2 v�etn�
    /// </summary>
    /// <param name="od"></param>
    /// <param name="to"></param>\
    public static int RandomInt(int od, int to)
    {
        if (to == int.MaxValue)
        {
            to--;
        }
        if (od > to)
        {
            ThrowExceptions.Custom(Exc.GetStackTrace(), type, Exc.CallingMethod(), $"From {od} is higher than to {to}");
        }

        return s_rnd.Next(od, to + 1);
    }

    public static Type type = typeof(RandomHelper);

    /// <summary>
    /// Zad�vej ��slo o 1 v�t�� ne� skute�n� po�et znak� kter� chce�
    /// Vr�t� mi n�hodn� �et�zec pouze z velk�ch, mal�ch p�smen a ��slic
    /// Call ToLower when save to DB
    /// Newly call ToLower automatically
    /// </summary>
    /// <param name="delka"></param>
    public static string RandomStringWithoutSpecial(int delka, bool alsoUpper = false)
    {
        delka--;
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i != delka; i++)
        {
            
            sb.Append(RandomCharWithoutSpecial());
        }
        var result = sb.ToString();
        if (!alsoUpper)
        {
            return result.ToLower();
        }
        return result;
    }

    /// <summary>
    /// Hod� se pro po��tan� index� proto�e vrac� ��slo mezi A1 do A2-1
    /// </summary>
    /// <param name="od"></param>
    /// <param name="to"></param>
    public static byte RandomByte2(int od, int to)
    {
        return (byte)s_rnd.Next(od, to);
    }

    /// <summary>
    /// Vr�t� mi n�hodn� znak pouze z velk�ch, mal�ch p�smen a ��slic
    /// Call ToLower when save to DB
    /// </summary>
    public static char RandomCharWithoutSpecial()
    {
        return RandomElementOfCollection(vsZnakyWithoutSpecial)[0];
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
            b[i] = (byte)s_rnd.Next(0, byte.MaxValue);
        }
        return b;
    }

    

    /// <summary>
    /// Vr�t� ��slo mezi 0 a A1-1
    /// </summary>
    /// <param name="to"></param>
    public static short RandomShort(short to)
    {
        return (short)s_rnd.Next(0, to);
    }
    /// <summary>
    /// Vr�t� ��slo mezi A1 v�etn� a A2+1 v�etn�
    /// </summary>
    /// <param name="to"></param>
    public static short RandomShort(short from, short to)
    {
        return (short)s_rnd.Next(from, to + 1);
    }
    /// <summary>
    /// Vr�t� ��slo mezi 0 a short.MaxValue-1
    /// </summary>
    public static short RandomShort()
    {
        return (short)s_rnd.Next(0, short.MaxValue);
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

    public static DateTime RandomDateTime(int yearTo)
    {
        DateTime result = Consts.DateTimeMinVal;
         result = result.AddDays(RandomHelper.RandomDouble(1, 28));
        result = result.AddMonths(RandomHelper.RandomInt(1, 12));
        var yearTo2 = yearTo - DTConstants.yearStartUnixDate  ;
        result = result.AddYears(RandomHelper.RandomInt(1, yearTo2) + 70);

        result = result.AddHours(RandomDouble(1, 24));
        result = result.AddMinutes(RandomDouble(1, 60));
        result = result.AddSeconds(RandomDouble(1, 60));

        return result;
    }

    private static double RandomDouble(int v1, int v2)
    {
        return (double)RandomInt(v1, v2);
    }
}