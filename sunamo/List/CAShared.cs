using sunamo;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

public  static partial class CA
{
    static Type type = typeof(CA);

    /// <summary>
    /// Direct edit collection
    /// Na rozdíl od metody RemoveStringsEmpty2 NEtrimuje před porovnáním
    /// </summary>
    /// <param name="mySites"></param>
    /// <returns></returns>
    public static List<string> RemoveStringsEmpty(List<string> mySites)
    {
        for (int i = mySites.Count - 1; i >= 0; i--)
        {
            if (mySites[i] == string.Empty)
            {
                mySites.RemoveAt(i);
            }
        }
        return mySites;
    }

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

    public static void AddSuffix(List<string> headers, string v)
    {
        for (int i = 0; i < headers.Count; i++)
        {
            headers[i] = headers[i] + v;
        }    
        
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
    /// Return null if A1 will be null
    /// </summary>
    /// <param name="captions"></param>
    /// <param name="i"></param>
    /// <returns></returns>
    public static object GetIndex(List<string> captions, int i)
    {
        if (captions == null)
        {
            return null;
        }
        if (!HasIndex(i, captions))
        {
            return null;
        }
        return captions[i];
    }



    /// <summary>
    /// direct edit
    /// Remove duplicities from A1
    /// In output is from every one instance
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

    public static List<T> ReplaceNullFor<T>(List<T> l, T empty) where T : class
    {
        for (int i = 0; i < l.Count; i++)
        {
            if (l[i] == null)
            {
                l[i] = empty;
            }
        }
        return l;
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

   

    /// <summary>
    /// Convert IEnumerable to List<string> Nothing more, nothing less
    /// Must be private - to use only public in CA
    /// bcoz Cast() not working
    /// Dont make any type checking - could be done before
    /// </summary>
    /// <returns></returns>
    private static List<string> ToListString2(IEnumerable enumerable)
    {
        List<string> result = new List<string>(enumerable.Count());
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
        return result;
    }

    

    public static List<string> Join(params object[] o)
    {
        List<string> result = new List<string>();
        foreach (var item in o)
        {
            result.AddRange(CA.ToListString(item));
        }

        return result;
    }

    /// <summary>
    /// Simply create new list in ctor from A1
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="f"></param>
    /// <returns></returns>
    public static List<T> ToList<T>(params T[] f)
    {
        IEnumerable enu = f;
        return ToList<T>(enu);
    }

    

    public static List<T> ToList<T>(IEnumerable enumerable)
    {
        List<T> result = null;
        //if (enumerable is IEnumerable<char>)
        //{
        //    result = new List<T>(1);
        //    result.Add(SH.JoinIEnumerable(string.Empty, enumerable));
        //}
        if (enumerable.Count() == 1 && enumerable.FirstOrNull() is IEnumerable<object>)
        {
            result = ToListT2<T>((IEnumerable<object>)enumerable.FirstOrNull());
        }
        else if (enumerable.Count() == 1 && enumerable.FirstOrNull() is IEnumerable)
        {
            result = ToListT2<T>(((IEnumerable)enumerable.FirstOrNull()));
        }
        else
        {
            return ToListT2<T>(enumerable);
        }
        return result;
    }

    /// <summary>
    /// Remove from A1 which exists in A2
    /// </summary>
    /// <param name="s"></param>
    /// <param name="manuallyNo"></param>
    public static void RemoveWhichExists(List<string> s, List<string> manuallyNo)
    {
        var dex = -1;
        foreach (var item in manuallyNo)
        {
            dex = s.IndexOf(item);
            if (dex != -1)
            {
                s.RemoveAt(dex);
            }
        }
    }

    /// <summary>
    /// Just call ToListString
    /// </summary>
    /// <param name="enumerable"></param>
    /// <returns></returns>
    public static List<string> ToListString(params object[] enumerable)
    {
        IEnumerable ienum = enumerable;
        return ToListString(ienum);
    }

    /// <summary>
    /// Just 3 cases of working:
    /// IEnumerable<char> => string
    /// IEnumerable<string> => List<string>
    /// IEnumerable => List<string>
    /// </summary>
    /// <param name="enumerable"></param>
    /// <returns></returns>
    public static List<string> ToListString(IEnumerable enumerable2)
    {
        List<string> result = null;
        result = new List<string>();
        if (enumerable2.GetType() != typeof(string))
        {
            foreach (object item in enumerable2)
            {
                // !(item is string)  - not working
                if (RH.IsOrIsDeriveFromBaseClass(item.GetType(), typeof(IEnumerable)))
                {
                    var enumerable = (IEnumerable)item;
                    var type = enumerable.GetType();
                    if (type == typeof(string))
                    {
                        result.Add(SH.JoinIEnumerable(string.Empty, enumerable));
                    }
                    else if (RH.IsOrIsDeriveFromBaseClass(type, typeof(IEnumerable<char>)))
                    {
                        // IEnumerable<char> => string
                        //enumerable2 is not string, then I can add all to list
                        result.AddRange(CA.ToListString2( enumerable));
                        //
                    }
                    else if (enumerable.Count() == 1 && enumerable.FirstOrNull() is IEnumerable<string>)
                    {
                        // IEnumerable<string> => List<string>
                        result.AddRange(((IEnumerable<string>)enumerable.FirstOrNull()).ToList());
                    }
                    else if (enumerable.Count() == 1 && enumerable.FirstOrNull() is IEnumerable)
                    {
                        result.AddRange(ToListString2(((IEnumerable)enumerable.FirstOrNull())));
                    }
                    else
                    {
                        // IEnumerable => List<string>
                        result.AddRange(ToListString2(enumerable));
                    }
                }
                else
                {
                    result.Add(item.ToString());
                }
            }
        }
        else
        {
            result.Add(enumerable2.ToString());
        }
        return result;
    }

    /// <summary>
    /// Must be private - to use only public in CA
    /// bcoz Cast() not working
    /// Dont make any type checking - could be done before
    /// </summary>
    /// <returns></returns>
    private static List<T> ToListT2<T>(IEnumerable enumerable)
    {
        List<T> result = new List<T>(enumerable.Count());
        foreach (var item in enumerable)
        {
            if (item == null)
            {
                result.Add(default(T));
            }
            else
            {
                result.Add((T)item);
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

    /// <summary>
    /// Return first of A2 which starts with  A1. Otherwise null
    /// So, isnt finding occurences but find out something in A2 have right format. 
    /// Method with shifted parameters working for searching occurences
    /// </summary>
    /// <param name="item2"></param>
    /// <param name="v1"></param>
    /// <returns></returns>
    public static string StartWith(string item2, params string[] v1)
    {
        return StartWith(item2, v1.ToList());
    }

    /// <summary>
    /// Return first of A2 which starts with  A1. Otherwise null
    /// So, isnt finding occurences but find out something in A2 have right format. 
    /// Method with shifted parameters working for searching occurences
    /// Cant be use if A1 is shorter than A2 (text vs textarea)
    /// </summary>
    /// <param name="item2"></param>
    /// <param name="v1"></param>
    /// <param name="v2"></param>
    /// <returns></returns>
    public static string StartWith(string item2, IEnumerable<string> v1)
    {
        foreach (var item in v1)
        {
            if (item.StartsWith(item2))
            {
                return item;
            }
        }
        return null;
    }

    /// <summary>
    /// Direct edit
    /// </summary>
    /// <param name="backslash"></param>
    /// <param name="s"></param>
    /// <returns></returns>
    public static List<string> TrimStart(string backslash, List<string> s)
    {
        string methodName = "TrimStart";

        ThrowExceptions.IsNull(type, methodName, "backslash", backslash);
        ThrowExceptions.IsNull(type, methodName, "s", s);

        for (int i = 0; i < s.Count; i++)
        {
            if (s[i].StartsWith(backslash))
            {
                s[i] = s[i].Substring(backslash.Length);
            }
            
        }
        return s;
    }

    /// <summary>
    /// Direct edit
    /// </summary>
    /// <param name="backslash"></param>
    /// <param name="s"></param>
    /// <returns></returns>
    public static List<string> TrimStart(char backslash, List<string> s)
    {
        for (int i = 0; i < s.Count; i++)
        {
            s[i] = s[i].TrimStart(backslash);
        }
        return s;
    }

    /// <summary>
    /// Non direct edit
    /// </summary>
    /// <param name="backslash"></param>
    /// <param name="s"></param>
    /// <returns></returns>
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

    /// <summary>
    /// Direct edit
    /// </summary>
    /// <param name="files_in"></param>
    /// <param name="what"></param>
    /// <param name="forWhat"></param>
    public static void Replace(List<string> files_in, string what, string forWhat)
    {
        CA.ChangeContent(files_in, SH.Replace, what, forWhat);
    }

    /// <summary>
    /// Direct edit
    /// </summary>
    /// <typeparam name="T1"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    /// <param name="files_in"></param>
    /// <param name="func"></param>
    /// <returns></returns>
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

    public static void RemoveAfterFirst(List<FieldInfo> withType)
    {
        
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

    /// <summary>
    /// Direct edit
    /// </summary>
    /// <typeparam name="Arg1"></typeparam>
    /// <typeparam name="Arg2"></typeparam>
    /// <param name="files_in"></param>
    /// <param name="func"></param>
    /// <param name="arg1"></param>
    /// <param name="arg2"></param>
    /// <returns></returns>
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

    public static List<string> WrapWith(List<string> whereIsUsed2, string v)
    {
        return WrapWith(whereIsUsed2, v, v);
    }

    /// <summary>
    /// direct edit
    /// </summary>
    /// <param name="whereIsUsed2"></param>
    /// <param name="v"></param>
    /// <returns></returns>
    public static List<string> WrapWith(List<string> whereIsUsed2, string before, string after)
    {
        for (int i = 0; i < whereIsUsed2.Count; i++)
        {
            whereIsUsed2[i] = before + whereIsUsed2[i] + after;
        }
        return whereIsUsed2;
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

    /// <summary>
    /// Direct edit
    /// </summary>
    /// <param name="slova"></param>
    /// <returns></returns>
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

    public static List<T> ToNumber<T>(Func<string, T> parse, IEnumerable enumerable, bool mustBeAllNumbers = true)
    {
        List<T> result = new List<T>();
        foreach (var item in enumerable)
        {
            if (item.ToString() == "NA")
            {
                continue;
            }

            if (SH.IsNumber(item.ToString(), ',', '.'))
            {

            var number = parse.Invoke(item.ToString());
            
                result.Add(number);
            }

        }
        return result;
    }

    public static List<T> ToNumber<T>(Func<string, bool, T> parse, IEnumerable enumerable, bool mustBeAllNumbers = true) 
    {
        List<T> result = new List<T>();
        foreach (var item in enumerable)
        {
            

            var number = parse.Invoke(item.ToString(), mustBeAllNumbers);
            if (number.ToString() == int.MinValue.ToString())
            {
                result.Add(number);
            }
            
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
        return ToNumber<int>(int.Parse, enumerable);
    }

    public static List<int> ToInt(IEnumerable enumerable, int requiredLength)
    {
        return ToNumber<int>(BTS.TryParseInt, enumerable, requiredLength);
    }

/// <summary>
/// Pokud A1 nebude mít délku A2 nebo prvek v A1 nebude vyparsovatelný na int, vrátí null
/// </summary>
/// <param name="altitudes"></param>
/// <param name="requiredLength"></param>
/// <returns></returns>
    public static List<T> ToNumber<T>(Func<string, T, T> tryParse, IEnumerable enumerable, int requiredLength)
    {
        int enumerableCount = enumerable.Count();
        if (enumerableCount != requiredLength)
        {
            return null;
        }

        List<T> result = new List<T>();
        T y = default(T);
        foreach (var item in enumerable)
        {
            var yy = tryParse.Invoke(item.ToString(), y);
            if (!EqualityComparer<T>.Default.Equals( yy , y))
            {
                result.Add(yy);
            }
            else
            {
                return null;
            }
        }
        return result;
    }

    public static List<int> ToInt(IEnumerable altitudes, int requiredLength, int startFrom)
    {
        return ToNumber<int>(BTS.TryParseInt, altitudes, requiredLength, startFrom);
    }

/// <summary>
/// Pokud prvek v A1 nebude vyparsovatelný na int, vrátí null
/// </summary>
/// <param name="altitudes"></param>
/// <param name="requiredLength"></param>
/// <returns></returns>
    public static List<T> ToNumber<T>(Func<string, T, T> tryParse, IEnumerable altitudes, int requiredLength, T startFrom) where T : IComparable
    {
        int finalLength = altitudes.Count() - int.Parse( startFrom.ToString());
        if (finalLength < requiredLength)
        {
            return null;
        }
        List<T> vr = new List<T>(finalLength);

        T i = default(T);
        foreach (var item in altitudes)
        {
            if (i.CompareTo(startFrom) !=0)
            {
                continue;
            }

            T y = default(T);
            var yy = tryParse.Invoke(item.ToString(), y);
            if (!EqualityComparer<T>.Default.Equals(yy, y))
            {
                vr.Add(yy);
            }
            else
            {
                return null;
            }

            
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
            throw new Exception("Chybný parametr" + " " + "");
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

public static object[] JoinVariableAndArray(object p, IEnumerable sloupce)
    {
        List<object> o = new List<object>();
        o.Add(p);
        foreach (var item in sloupce)
        {
            o.Add(item);
        }
        
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

/// <summary>
    /// better is use first or default, because here I also have to use default(T)
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="list"></param>
    /// <returns></returns>
    public static T FirstOrNull<T>(List<T> list)
    {
        if (list.Count > 0)
        {
            return list[0];
        }
        return default(T);
    }

    /// <summary>
    /// IsEqualToAnyElement - same as ContainsElement, only have switched elements
    /// ContainsElement - at least one element must contains.
    /// IsSomethingTheSame - only for string.   
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="list"></param>
    /// <param name="t"></param>
    /// <returns></returns>
    public static bool ContainsElement<T>(IEnumerable<T> list, T t)
    {
        if (list.Count() == 0)
        {
            return false;
        }
        foreach (T item in list)
        {
            if (Comparer<T>.Equals(item, t))
            {
                return true;
            }
        }
        
        return false;
    }

public static List<string> WithoutDiacritic(List<string> nazev)
    {
        for (int i = 0; i < nazev.Count; i++)
        {
            nazev[i] = SH.TextWithoutDiacritic(nazev[i]);
        }
        return nazev;
    }

public static bool HasIndexWithValueWithoutException(int p, List<string> nahledy, string item)
    {
        if (p < 0)
        {
            return false;
        }
        if (nahledy.Count > p && nahledy[p] == item)
        {
            return true;
        }
        return false;
    }

public static bool HasIndexWithoutException(int p, IList nahledy)
    {
        if (p < 0)
        {
            return false;
        }
        if (nahledy.Count > p)
        {
            return true;
        }
        return false;
    }
}