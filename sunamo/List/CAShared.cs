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
                if (item == null)
                {
                    result.Add("(null)");
                }
                else
                {
                    result.Add(item.ToString());
                }
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

public static void Replace(List<string> files_in, string what, string forWhat)
    {
        CA.ChangeContent(files_in, SH.Replace, what, forWhat);
    }

private static List<TResult> ChangeContent<T1, TResult>(List<T1> files_in, Func<T1, TResult> func)
    {
        List<TResult> result = new List<TResult>(files_in.Count);
        for (int i = 0; i < files_in.Count; i++)
        {
            result.Add( func.Invoke(files_in[i]));
        }
        return result;
    }
/// <summary>
    /// TResult is the same type as T1 (output collection is the same generic as input)
    /// </summary>
    /// <typeparam name="T1"></typeparam>
    /// <typeparam name="T2"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    /// <param name="files_in"></param>
    /// <param name="func"></param>
    /// <returns></returns>
    private static List<TResult> ChangeContent<T1, T2, TResult>(Func<T1, T2, TResult> func, List<T1> files_in, T2 t2)
    {
        List<TResult> result = new List<TResult>(files_in.Count);
        for (int i = 0; i < files_in.Count; i++)
        {
            // Fully generic - no strict string can't return the same collection
            result.Add( func.Invoke(files_in[i], t2));
        }
        return result;
    }
/// <summary>
    /// Direct edit
    /// </summary>
    /// <param name="files_in"></param>
    /// <param name="func"></param>
    /// <returns></returns>
    public static bool ChangeContent(List<string> files_in, Predicate<string> predicate, Func<string, string> func)
    {
        bool changed = false;
        for (int i = 0; i < files_in.Count; i++)
        {
            if (predicate.Invoke(files_in[i]))
            {
                files_in[i] = func.Invoke(files_in[i]);
                changed = true;
            }
        }
        return changed;
    }

    public static List<T> JoinIEnumerable<T>(params IEnumerable< T>[] enumerable)
    {
        List<T> t = new List<T>();
        foreach (var item in enumerable)
        {
            foreach (var item2 in item)
            {
                t.Add((T)item2);
            }
        }
        return t;
    }

    /// <summary>
    /// Direct edit
    /// </summary>
    /// <param name="files_in"></param>
    /// <param name="func"></param>
    /// <returns></returns>
    public static List<string> ChangeContent(List<string> files_in, Func<string, string> func)
    {
        for (int i = 0; i < files_in.Count; i++)
        {
            files_in[i] = func.Invoke(files_in[i]);
        }
        return files_in;
    }
public static List<string> ChangeContent<Arg1, Arg2>(List<string> files_in, Func<string, Arg1, Arg2, string> func, Arg1 arg1, Arg2 arg2)
    {
        for (int i = 0; i < files_in.Count; i++)
        {
            files_in[i] = func.Invoke(files_in[i], arg1, arg2);
        }
        return files_in;
    }
/// <summary>
    /// Direct edit input collection
    /// </summary>
    /// <typeparam name="Arg1"></typeparam>
    /// <param name="files_in"></param>
    /// <param name="func"></param>
    /// <param name="arg"></param>
    /// <returns></returns>
    public static List<string> ChangeContent<Arg1>(List<string> files_in, Func<string, Arg1, string> func, Arg1 arg)
    {
        for (int i = 0; i < files_in.Count; i++)
        {
            files_in[i] = func.Invoke(files_in[i], arg);
        }
        return files_in;
    }

public static List<string> WrapWith(IList<string> whereIsUsed2, string v)
    {
        List<string> result = new List<string>();
        for (int i = 0; i < whereIsUsed2.Count; i++)
        {
            result.Add(v + whereIsUsed2[i] + v);
        }
        return result;
    }

public static List<string> WrapWithQm(List<string> value)
    {
        for (int i = 0; i < value.Count; i++)
        {
            value[i] = SH.WrapWithQm(value[i]);
        }
        return value;
    }

/// <summary>
    /// Multi deep array is not suppported
    /// For convert into string use ListToString
    /// </summary>
    /// <param name="para"></param>
    /// <returns></returns>
    public static object[] TwoDimensionParamsIntoOne(object[] para)
    {
        List<object> result = new List<object>();
        foreach (var item in para)
        {
            if (item is IEnumerable && item.GetType() != typeof(string))
            {
                foreach (var r in (IEnumerable)item)
                {
                    result.Add(r);
                }
            }
            else
            {
                result.Add(item);
            }
        }
        return result.ToArray();
    }

public static List<long> ToLong(IEnumerable enumerable)
    {
        List<long> result = new List<long>();
        foreach (var item in enumerable)
        {
            result.Add(long.Parse(item.ToString()));
        }
        return result;
    }

public static List<string> ToLower(List<string> slova)
    {
        for (int i = 0; i < slova.Count; i++)
        {
            slova[i] = slova[i].ToLower();
        }
        return slova;
    }
/// <summary>
    /// Pro vyssi vykon uklada primo do zdrojoveho pole, pokud neni A2
    /// </summary>
    /// <param name="ss"></param>
    /// <returns></returns>
    public static string[] ToLower(string[] ss, bool createNewArray = false)
    {
        string[] outArr = ss;

        if (createNewArray)
        {
            outArr = new string[ss.Length];
        }

        for (int i = 0; i < ss.Length; i++)
        {
            outArr[i] = ss[i].ToLower();

        }
        return outArr;

    }

public static bool IsTheSame<T>(IEnumerable<T> sloupce, IEnumerable<T> sloupce2)
    {
        return sloupce.SequenceEqual(sloupce2);
    }

public static List<short> ToShort(IEnumerable enumerable)
    {
        List<short> result = new List<short>();
        foreach (var item in enumerable)
        {
            result.Add(short.Parse(item.ToString()));
        }
        return result;
    }

/// <summary>
    /// Pokud potřebuješ vrátit null když něco nebude sedět, použij ToInt s parametry nebo ToIntMinRequiredLength
    /// </summary>
    /// <param name="altitudes"></param>
    /// <returns></returns>
    public static List<int> ToInt(IEnumerable enumerable)
    {
        List<int> result = new List<int>();
        foreach (var item in enumerable)
        {
            result.Add(int.Parse(item.ToString()));
        }
        return result;
    }
/// <summary>
    /// Pokud A1 nebude mít délku A2 nebo prvek v A1 nebude vyparsovatelný na int, vrátí null
    /// </summary>
    /// <param name="altitudes"></param>
    /// <param name="requiredLength"></param>
    /// <returns></returns>
    public static List<int> ToInt(IEnumerable enumerable, int requiredLength)
    {
        int enumerableCount = enumerable.Count();
        if (enumerableCount != requiredLength)
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
    /// Pokud prvek v A1 nebude vyparsovatelný na int, vrátí null
    /// </summary>
    /// <param name="altitudes"></param>
    /// <param name="requiredLength"></param>
    /// <returns></returns>
    public static List<int> ToInt(IEnumerable altitudes, int requiredLength, int startFrom)
    {
        int finalLength = altitudes.Count() - startFrom;
        if (finalLength < requiredLength)
        {
            return null;
        }
        List<int> vr = new List<int>(finalLength);

        int i = 0;
        foreach (var item in altitudes)
        {
            if (i < startFrom)
            {
                continue;
            }

            int y = 0;
            if (int.TryParse(item.ToString(), out y))
            {
                vr.Add(y);
            }
            else
            {
                return null;
            }

            i++;
        }

        return vr;
    }

public static List<byte> JoinBytesArray(byte[] pass, byte[] salt)
    {
        List<byte> lb = new List<byte>(pass.Length + salt.Length);
        lb.AddRange(pass);
        lb.AddRange(salt);
        return lb;
    }

public static bool Contains(int idUser, int[] onlyUsers)
    {
        foreach (int item in onlyUsers)
        {
            if (item == idUser)
            {
                return true;
            }
        }
        return false;
    }
/// <summary>
    /// G zda se alespoň 1 prvek A2 == A1
    /// </summary>
    /// <param name="value"></param>
    /// <param name="availableValues"></param>
    /// <returns></returns>
    public static bool Contains(string value, List<string> availableValues)
    {
        foreach (var item in availableValues)
        {
            if (item == value)
            {
                return true;
            }
        }
        return false;
    }

public static IEnumerable<string> ToEnumerable(params string[] p)
    {
        return p;
    }

public static T[] JumbleUp<T>(T[] b)
    {
        int bl = b.Length;
        for (int i = 0; i < bl; ++i)
        {
            int index1 = (RandomHelper.RandomInt() % bl);
            int index2 = (RandomHelper.RandomInt() % bl);

            T temp = b[index1];
            b[index1] = b[index2];
            b[index2] = temp;
        }
        return b;
    }
public static List<T> JumbleUp<T>(List<T> b)
    {
        int bl = b.Count;
        for (int i = 0; i < bl; ++i)
        {
            int index1 = (RandomHelper.RandomInt() % bl);
            int index2 = (RandomHelper.RandomInt() % bl);

            T temp = b[index1];
            b[index1] = b[index2];
            b[index2] = temp;
        }
        return b;
    }





public static bool HasIndex(int dex, Array col)
    {
        return col.Length > dex;
    }
public static bool HasIndex(int p, IEnumerable nahledy)
    {
        if (p < 0)
        {
            throw new Exception("Chybný parametr p");
        }
        if (nahledy.Count() > p)
        {
            return true;
        }
        return false;
    }

public static int GetLength(IList where)
    {
        if (where == null)
        {
            return 0;
        }
        return where.Count;
    }

/// <summary>
    /// Is same as ContainsElement, only have switched arguments
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="p"></param>
    /// <param name="list"></param>
    /// <returns></returns>
    public static bool IsEqualToAnyElement<T>(T p, IEnumerable<T> list)
    {
        foreach (T item in list)
        {
            if (EqualityComparer<T>.Default.Equals(p, item))
            {
                return true;
            }
        }
        return false;
    }
public static bool IsEqualToAnyElement<T>(T p, params T[] prvky)
    {
        return IsEqualToAnyElement(p, prvky.ToList());
    }

public static object[] JoinVariableAndArray(object p, object[] sloupce)
    {
        List<object> o = new List<object>();
        o.Add(p);
        o.AddRange(sloupce);
        return o.ToArray();
    }

public static List<string> TrimEnd(List<string> sf, params char[] toTrim)
    {
        for (int i = 0; i < sf.Count; i++)
        {
            sf[i] = sf[i].TrimEnd(toTrim);
        }
        return sf;
    }
public static string[] TrimEnd(string[] sf, params char[] toTrim)
    {
        return TrimEnd(new List<string>(sf), toTrim).ToArray();
    }
}