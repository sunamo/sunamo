using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public  static partial class CA
{
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
}