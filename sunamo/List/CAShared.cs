using sunamo;
using sunamo.Values;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;

public static partial class CA
{
    private static Type s_type = typeof(CA);

    /// <summary>
    /// Direct edit collection
    /// Na rozdíl od metody RemoveStringsEmpty2 NEtrimuje před porovnáním
    /// </summary>
    /// <param name="mySites"></param>
    
    public static List<int> ToIntMinRequiredLength(IEnumerable enumerable, int requiredLength)
    {
        if (enumerable.Count() < requiredLength)
        {
            return null;
        }

        List<int> result = new List<int>();
        int y = 0;
        foreach (var item in enumerable)
        {
            if (int.TryParse(item.ToString(), out y))
            {
                result.Add(y);
            }
            else
            {
                return null;
            }
        }
        return result;
    }

/// <summary>
    /// Index A2 a další bude již v poli A4
    /// </summary>
    /// <param name="p1"></param>
    /// <param name="p2"></param>
    /// <param name="before"></param>
    /// <param name="after"></param>
    public static void Split<T>(T[] p1, int p2, out T[] before, out T[] after)
    {
        before = new T[p2];
        int p1l = p1.Length;
        after = new T[p1l - p2];
        bool b = true;
        for (int i = 0; i < p1l; i++)
        {
            if (i == p2)
            {
                b = false;
            }
            if (b)
            {
                before[i] = p1[i];
            }
            else
            {
                after[i] = p1[i - p2];
            }
        }
    }


public static List<string> EnsureBackslash(List<string> eb)
    {
        for (int i = 0; i < eb.Count; i++)
        {
            string r = eb[i];
            if (r[r.Length - 1] != AllChars.bs)
            {
                eb[i] = r + Consts.bs;
            }
        }

        return eb;
    }

    /// <summary>
    /// Delete which fullfil A2 wildcard
    /// </summary>
    /// <param name="d"></param>
    /// <param name="mask"></param>
    public static void RemoveWildcard(List<string> d, string mask)
    {
        for (int i = d.Count - 1; i >= 0; i--)
        {
            if (SH.MatchWildcard(d[i], mask))
            {
                d.RemoveAt(i);
            }
        }
    }

    public static IEnumerable<List<T>> SplitList<T>(IList<T> locations, int nSize = 30)
    {
        for (int i = 0; i < locations.Count; i += nSize)
        {
            yield return locations.ToList().GetRange(i, Math.Min(nSize, locations.Count - i));
        }
    }

public static List<object> ToObject(IEnumerable enumerable)
    {
        List<object> result = new List<object>();
        foreach (var item in enumerable)
        {
            result.Add(item);
        }
        return result;
    }

public static List<bool> ToBool(List<int> numbers)
    {
        var b = new List<bool>(numbers.Count);
        foreach (var item in numbers)
        {
            b.Add(BTS.IntToBool(item));
        }
        return b;
    }

public static void RemoveWhichContains(List<string> files, List<string> list, bool wildcard)
    {
        foreach (var item in list)
        {
            RemoveWhichContains(files, item, wildcard);
        }
    }
public static void RemoveWhichContains(List<string> files1, string item, bool wildcard)
    {
        if (wildcard)
        {
            //item = SH.WrapWith(item, AllChars.asterisk);
            for (int i = files1.Count - 1; i >= 0; i--)
            {
                if (Wildcard.IsMatch(files1[i], item))
                {
                    files1.RemoveAt(i);
                }
            }
        }
        else
        {
            for (int i = files1.Count - 1; i >= 0; i--)
            {
                if (files1[i].Contains(item))
                {
                    files1.RemoveAt(i);
                }
            }
        }
    }

    public static string RemovePadding(List<byte> decrypted, byte v, bool returnStringInUtf8)
    {
        RemovePadding<byte>(decrypted, v);

        if (returnStringInUtf8)
        {
            return Encoding.UTF8.GetString(decrypted.ToArray());
        }
        return string.Empty;
    }

public static void RemovePadding<T>(List<T> decrypted, T v)
    {
        for (int i = decrypted.Count - 1; i >= 0; i--)
        {
            if(!EqualityComparer<T>.Default.Equals( decrypted[i], v))
            {
                break;
            }
            decrypted.RemoveAt(i);
        }

        
    }

public static bool HasAtLeastOneElementInArray(List<string> d)
    {
        if (d != null)
        {
            if (d.Count != 0)
            {
                return true;
            }
        }
        return false;
    }

/// <summary>
    /// Return what exists in both
    /// Modify both A1 and A2 - keep only which is only in one
    /// </summary>
    /// <param name="c1"></param>
    /// <param name="c2"></param>
    public static List<string> CompareList(List<string> c1, List<string> c2)
    {
        List<string> existsInBoth = new List<string>();

        int dex = -1;

        for (int i = c2.Count - 1; i >= 0; i--)
        {
            string item = c2[i];
            dex = c1.IndexOf(item);
            if (dex != -1)
            {
                existsInBoth.Add(item);
                c2.RemoveAt(i);
                c1.RemoveAt(dex);
            }
        }

        for (int i = c1.Count - 1; i >= 0; i--)
        {
            string item = c1[i];
            dex = c2.IndexOf(item);
            if (dex != -1)
            {
                existsInBoth.Add(item);
                c1.RemoveAt(i);
                c2.RemoveAt(dex);
            }
        }

        return existsInBoth;
    }

public static void InitFillWith(List<string> datas, int pocet, string initWith = Consts.stringEmpty)
    {
        for (int i = 0; i < pocet; i++)
        {
            datas.Add(initWith);
        }
    }

public static void TrimWhereIsOnlyWhitespace(List<string> list)
    {
        for (int i = list.Count - 1; i >= 0; i--)
        {
            var l = list[i];
            if (string.IsNullOrWhiteSpace(l))
            {
                list[i] = list[i].Trim();
            }
        }
    }

public static string DoubleOrMoreMultiLinesToSingle(ref string list)
    {
        var n = Environment.NewLine;
        list = Regex.Replace(list, "[\\r\\n]+", System.Environment.NewLine, System.Text.RegularExpressions.RegexOptions.Multiline);
        list = list.Replace(n, n + n);
        return list;
    }
}