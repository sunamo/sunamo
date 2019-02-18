using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

public  static partial class CA
{
    public static bool IsEmptyOrNull(IEnumerable mustBe)
    {
        if (mustBe == null)
        {
            return true;
        }
        else if (mustBe.Count() == 0)
        {
            return true;
        }
        return false;
    }

    public static List<string> CreateListStringWithReverse(int reverse, params string[] v)
    {
        List<string> vs = new List<string>(reverse + v.Length);
        vs.AddRange(v);
        return vs;
    }

    /// <summary>
    /// direct edit
    /// Remove duplicities from A1
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="idKesek"></param>
    /// <returns></returns>
    public static List<T> RemoveDuplicitiesList<T>(List<T> idKesek)
    {
        List<T> foundedDuplicities;
        return RemoveDuplicitiesList<T>(idKesek, out foundedDuplicities);
    }

    /// <summary>
    /// direct edit
    /// Remove duplicities from A1
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="idKesek"></param>
    /// <param name="foundedDuplicities"></param>
    /// <returns></returns>
    public static List<T> RemoveDuplicitiesList<T>(List<T> idKesek, out List<T> foundedDuplicities)
    {
        foundedDuplicities = new List<T>();
        List<T> h = new List<T>();
        for (int i = idKesek.Count - 1; i >= 0; i--)
        {
            var item = idKesek[i];
            if (!h.Contains(item))
            {
                h.Add(item);
            }
            else
            {
                idKesek.RemoveAt(i);
                foundedDuplicities.Add(item);
            }
        }

        return h;
    }

    public static List<string> ToListString(params object[] enumerable)
    {
        List<string> result = new List<string>();
        foreach (var item in enumerable)
        {
            result.Add(item.ToString());
        }
        return result;
    }

    

    public static IEnumerable ToEnumerable(params object[] p)
    {
        if (p.Count() == 0)
        {
            return new List<string>();
        }

        if (p[0] is IEnumerable && p.Length == 1)
        {
            return (IEnumerable)p.First();
        }
        else if (p[0] is IEnumerable)
        {
            return (IEnumerable)p;
        }

        return p;
    }

    public static List<string> ToListString(IEnumerable enumerable)
    {

        List<string> result = new List<string>();
        if (enumerable is IEnumerable<char>)
        {
            result.Add(SH.JoinIEnumerable(string.Empty, enumerable));
        }
        else
        {
            foreach (var item in enumerable)
            {
                result.Add(item.ToString());
            }
        }
        return result;
    }


    public static T[] ToArrayT<T>(params T[] aB)
    {
        return aB;
    }

[MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Swap<T>(this IList<T> list, int i, int j)
    {
        if (i == j)   //This check is not required but Partition function may make many calls so its for perf reason
            return;
        var temp = list[i];
        list[i] = list[j];
        list[j] = temp;
    }

public static List<T> ToList<T>(params T[] f)
    {
        return new List<T>(f);
    }

    public static List<string> TrimStart(char backslash, List<string> s)
    {
        for (int i = 0; i < s.Count; i++)
        {
            s[i] = s[i].TrimStart(backslash);
        }
        return s;
    }

    public static string[] TrimStart(char backslash, params string[] s)
    {
        return TrimStart(backslash, s.ToList()).ToArray();
    }

/// <summary>
    /// For all types
    /// </summary>
    /// <param name="times"></param>
    /// <returns></returns>
    public static List<int> IndexesWithNull(IEnumerable times) 
    {
        List<int> nulled = new List<int>();
        int i = 0;
        foreach (var item in times)
        {
            if (item == null)
            {
                nulled.Add(i);
            }
            i++;
        }

        return nulled;
    }
/// <summary>
    /// Only for structs
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="times"></param>
    /// <returns></returns>
    public static List<int> IndexesWithNull<T>(List<Nullable<T>> times) where T : struct
    {

        List<int> nulled = new List<int>();
        for (int i = 0; i < times.Count; i++)
        {
            T? t = new Nullable<T>(times[i].Value);
            if (!t.HasValue)
            {
                nulled.Add(i);
            }
        }
        return nulled;
    }

public static void AppendToLastElement(List<string> list, string s)
    {
        if (list.Count >0 )
        {
            list[list.Count - 1] += s;
        }
        else
        {
            list.Add(s);
        }
    }

/// <summary>
    /// Dont trim
    /// </summary>
    /// <param name="times"></param>
    /// <returns></returns>
    public static List<int> IndexesWithNullOrEmpty(IEnumerable times)
    {
        List<int> nulled = new List<int>();
        int i = 0;
        foreach (var item in times)
        {
            if (item == null)
            {
                nulled.Add(i);
            }
            else if(item.ToString() == string.Empty)
            {
                nulled.Add(i);
            }
            i++;
        }

        return nulled;
    }

/// <summary>
    /// Direct edit input collection
    /// </summary>
    /// <param name="l"></param>
    /// <returns></returns>
    public static List<string> Trim(List<string> l)
    {
        for (int i = 0; i < l.Count; i++)
        {
            l[i] = l[i].Trim();
        }
        return l;
    }
public static string[] Trim(string[] l)
    {
        var list = CA.ToListString(l);
        CA.Trim(list);
        return list.ToArray();
    }
}