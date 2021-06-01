using sunamo.Data;

using System;
/// <summary>
/// Number Helper Class
/// </summary>
using System.Collections.Generic;
using System.Linq;
using System.Text;

public static partial class NH
{
    /// <summary>
    /// Vytvoří interval od A1 do A2 včetně
    /// </summary>
    /// <param name="od"></param>
    /// <param name="to"></param>
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

    public static string CalculateMedianAverage(List<long> l2)
    {
        var l = CA.ToNumber<double>(double.Parse, l2);
        return CalculateMedianAverage(l);
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
    /// Reversion is DTHelperGeneral.FullYear
    /// </summary>
    /// <param name="year"></param>
    /// <returns></returns>
    public static byte Last2NumberByte(int year)
    {
        var ts = year.ToString();
        ts = ts.Substring(ts.Length - 3);
        return byte.Parse(ts);
    }

    /// <summary>
    /// Cast A1,2 to double and divide
    /// </summary>
    /// <param name="textC"></param>
    /// <param name="diac"></param>
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


    public static int GetLowest(List<int> excludedValues, List<int> list)
    {
        list.Sort();
        var vr = list[0];
        while (excludedValues.Contains(vr))
        {
            list.RemoveAt(0);
            if (list.Count > 0)
            {
                vr = list[0];
            }
            else
            {
                // 
            }
        }

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

    public static List<T> Sort<T>(params T[] t)
    {
        List<T> c = new List<T>(t);
        c.Sort();
        return c;
    }

public static string CalculateMedianAverage(List<double> list)
    {
        list.RemoveAll(d => d == 0);

        ThrowExceptions.OnlyOneElement(Exc.GetStackTrace(),type, "CalculateMedianAverage", "list", list);

        MedianAverage<double> medianAverage = new MedianAverage<double>();
        medianAverage.count = list.Count;
        medianAverage.median = NH.Median<double>(list);
        medianAverage.average = NH.Average<double>(list);
        medianAverage.min = list.Min();
        medianAverage.max = list.Max();

        return medianAverage.ToString();
    }


public static double Average(double gridWidth, double columnsCount)
    {
        return Average<double>(gridWidth, columnsCount);
    }


    /// <summary>
    /// Median = most frequented value
    /// Note: specified list would be mutated in the process.
    /// Working excellent
    /// 4, 0 = 0
    /// 4, 4, 250, 500, 500 = 250
    /// 4, 4, 250, 500, 500 = 250
    /// 4, 4, 4, 4, 250, 500, 500 = 4
    /// </summary>
    public static T Median<T>(this IList<T> list) where T : IComparable<T>
    {
        return list.NthOrderStatistic((list.Count - 1) / 2);
    }

    public static int NumberIntUntilWontReachOtherChar(ref string s)
    {
        StringBuilder sb = new StringBuilder();

        for (int i = 0; i < s.Length; i++)
        {
            if (char.IsNumber(s[i]))
            {
                sb.Append(s[i]);
            }
            else
            {
                break;
            }
        }

        var result = sb.ToString();

        s = SH.ReplaceOnce(s, result, string.Empty);


        return BTS.ParseInt( result, int.MaxValue);
    }

    /// <summary>
    /// Working excellent
    /// 4, 0 = 2 (as online)
    /// 4, 4, 250, 500, 500 = 250
    /// 4, 4, 250, 500, 500 = 250
    /// 4, 4, 4, 4, 250, 500, 500 = 4
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="numbers"></param>
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
            median = Sum(CA.ToListString(d, d2)) / 2;
        }
        else
        {
            median = double.Parse(sortedNumbers.ElementAt(halfIndex).ToString());
        }
        return median;
    }

    public static double Median<T>(this IEnumerable<T> sequence, Func<T, double> getValue)
    {
        var list = sequence.Select(getValue).ToList();
        var mid = (list.Count - 1) / 2;
        return list.NthOrderStatistic(mid);
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

}