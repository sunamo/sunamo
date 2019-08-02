
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
    static Type type = typeof(NH);

    public static int MinForLength(int length)
    {
        return int.Parse( "1".PadRight(4, '0'));
    }

    public static int MaxForLength(int length)
    {
        return int.Parse("9".PadRight(4, '9'));
    }

    /// <summary>
    /// Note: specified list would be mutated in the process.
    /// Working excellent
    /// </summary>
    public static T Median<T>(this IList<T> list) where T : IComparable<T>
    {
        return list.NthOrderStatistic((list.Count - 1) / 2);
    }

    public static double Median<T>(this IEnumerable<T> sequence, Func<T, double> getValue)
    {
        var list = sequence.Select(getValue).ToList();
        var mid = (list.Count - 1) / 2;
        return list.NthOrderStatistic(mid);
    }

    /// <summary>
    /// Working excellent
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="numbers"></param>
    /// <returns></returns>
    public static double Median2<T>(IEnumerable<T> numbers) 
    {
        int numberCount = numbers.Count();
        int halfIndex = numbers.Count() / 2;
        var sortedNumbers = numbers.OrderBy(n => n);
        double median;
        if ((numberCount % 2) == 0)
        {
            var d = sortedNumbers.ElementAt(halfIndex);
            var d2 = sortedNumbers.ElementAt((halfIndex - 1));
            median = Sum(CA.ToListString( d,d2)) / 2;
        }
        else
        {
            median = double.Parse( sortedNumbers.ElementAt(halfIndex).ToString());
        }
        return median;
    }

    public static T Average<T>(List<T> list)
    {
        return Average<T>(Sum<T>(list), list.Count);
    }

    public static double Sum(List<string> list)
    {
        double result = 0;
        foreach (var item in list)
        {
            var d = double.Parse(item);
            result += d;
        }
        return result;
    }

    public static T Sum<T>(List<T> list)
    {
        dynamic sum = 0;
        foreach (var item in list)
        {
            sum += item;
        }
        return sum;
    }

    public static double Average(double gridWidth, double columnsCount)
    {
        return Average<double>(gridWidth, columnsCount);
    }



    public static T Average<T>(dynamic gridWidth, dynamic columnsCount)
    {
        if (EqualityComparer<T>.Default.Equals(columnsCount, (T)NH.ReturnZero<T>()))
        {
            return (T)NH.ReturnZero<T>() ;
        }

        if (EqualityComparer<T>.Default.Equals( gridWidth, (T)NH.ReturnZero<T>()))
        {
            return (T)NH.ReturnZero<T>();
        }

        dynamic result = gridWidth / columnsCount;
        return result;
    }

    /// <summary>
    /// Must be object to use in EqualityComparer
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    private static object ReturnZero<T>()
    {
        if (typeof(T) == Consts.tDouble)
        {
            return Consts.zeroDouble;
        }
        else if (typeof(T) == Consts.tInt)
        {
            return Consts.zeroInt;
        }
        else if (typeof(T) == Consts.tFloat)
        {
            return Consts.zeroFloat;
        }
        ThrowExceptions.NotImplementedCase(type, "ReturnZero");
        return null;
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