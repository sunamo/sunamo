﻿using sunamo;
using sunamo.Essential;

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
    public static int Count(IEnumerable e)
    {
        if (e == null)
        {
            return 0;
        }

        if (e is IList)
        {
            return (e as IList).Count;
        }

        if (e is Array)
        {
            return (e as Array).Length;
        }

        int count = 0;

        foreach (var item in e)
        {
            count++;
        }

        return count;
    }

    private static Type type = typeof(CA);

    #region ChangeContent for easy copy
 

    /// <summary>
    /// Direct edit
    /// </summary>
    /// <typeparam name="T1"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    /// <param name="files_in"></param>
    /// <param name="func"></param>
    private static List<TResult> ChangeContent<T1, TResult>(List<T1> files_in, Func<T1, TResult> func)
    {
        List<TResult> result = new List<TResult>(files_in.Count);
        for (int i = 0; i < files_in.Count; i++)
        {
            result.Add(func.Invoke(files_in[i]));
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
    private static List<TResult> ChangeContent<T1, T2, TResult>(ChangeContentArgs a, Func<T1, T2, TResult> func, List<T1> files_in, T2 t2)
    {
        List<TResult> result = new List<TResult>(files_in.Count);
        for (int i = 0; i < files_in.Count; i++)
        {
            // Fully generic - no strict string can't return the same collection
            result.Add(func.Invoke(files_in[i], t2));
        }

        CA.RemoveDefaultT<TResult>(result);
        return result;
    }



    /// <summary>
    /// Direct edit
    /// If not every element fullfil pattern, is good to remove null (or values returned if cant be changed) from result
    /// </summary>
    /// <param name="files_in"></param>
    /// <param name="func"></param>
    public static List<string> ChangeContent(ChangeContentArgs a, List<string> files_in, Func<string, string> func)
    {
        for (int i = 0; i < files_in.Count; i++)
        {
            files_in[i] = func.Invoke(files_in[i]);
        }

        RemoveNullOrEmpty(a, files_in);

        return files_in;
    }

    /// <summary>
    /// Direct edit input collection
    /// </summary>
    /// <typeparam name="Arg1"></typeparam>
    /// <param name="files_in"></param>
    /// <param name="func"></param>
    /// <param name="arg"></param>
    public static List<string> ChangeContent<Arg1>(ChangeContentArgs a, List<string> files_in, Func<string, Arg1, string> func, Arg1 arg)
    {
        for (int i = 0; i < files_in.Count; i++)
        {
            files_in[i] = func.Invoke(files_in[i], arg);
        }

        RemoveNullOrEmpty(a, files_in);

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
    public static List<string> ChangeContent<Arg1, Arg2>(ChangeContentArgs a, List<string> files_in, Func<string, Arg1, Arg2, string> func, Arg1 arg1, Arg2 arg2)
    {
        for (int i = 0; i < files_in.Count; i++)
        {
            files_in[i] = func.Invoke(files_in[i], arg1, arg2);
        }

        RemoveNullOrEmpty(a, files_in);

        return files_in;
    }

    /// <summary>
    /// Direct edit
    /// </summary>
    /// <param name="files_in"></param>
    /// <param name="func"></param>
    public static bool ChangeContent(ChangeContentArgs a, List<string> files_in, Predicate<string> predicate, Func<string, string> func)
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

        RemoveNullOrEmpty(a, files_in);

        return changed;
    }
    #endregion

    private static void RemoveNullOrEmpty(ChangeContentArgs a, List<string> files_in)
    {
        if (a != null)
        {
            if (a.removeNull)
            {
                CA.RemoveDefaultT<string>(files_in);
            }

            if (a.removeEmpty)
            {
                CA.RemoveStringsEmpty2(files_in);
            }
        }
    }

    /// <summary>
    /// Direct edit collection
    /// Na rozdíl od metody RemoveStringsEmpty2 NEtrimuje před porovnáním
    /// </summary>
    /// <param name="mySites"></param>
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



    /// <summary>
    /// Return true if A1 is null or have zero elements
    /// </summary>
    /// <param name="mustBe"></param>
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

    public static List<string> CreateListStringWithReverse(int reverse, IEnumerable<string> v)
    {
        List<string> vs = new List<string>(reverse + v.Count());
        vs.AddRange(v);
        return vs;
    }

    /// <summary>
    /// direct edit
    /// Remove duplicities from A1
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="idKesek"></param>
    public static List<T> RemoveDuplicitiesList<T>(IList<T> idKesek)
    {
        List<T> foundedDuplicities;
        return RemoveDuplicitiesList<T>(idKesek, out foundedDuplicities);
    }



    public static void FirstCharUpper(List<string> s)
    {
        CA.ChangeContent(null,s, r => SH.FirstCharUpper(r));
    }

    public static void FirstCharOfEveryWordUpperDash(List<string> appNames)
    {
        CA.ChangeContent(null,appNames, r => SH.FirstCharOfEveryWordUpperDash(r));
    }

    public static List<char> ToListChar(ICollection<string> values)
    {
        var v = new List<char>(values.Count);
        foreach (var item in values)
        {
            v.Add(item[0]);
        }
        return v;
    }

    /// <summary>
    /// Return null if A1 will be null
    /// </summary>
    /// <param name="captions"></param>
    /// <param name="i"></param>
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

    public static bool HasDuplicates(List<string> list)
    {
        var list2 = list.ToList();
        CA.RemoveDuplicitiesList(list);
        if (list2.Count != list.Count)
        {
            //Console.WriteLine( Exceptions.DifferentCountInLists(string.Empty, "list2", list2.Count, "list", list.Count));
            return true;
        }
        return false;
    }



    /// <summary>
    /// direct edit
    /// Remove duplicities from A1
    /// In return value is from every one instance
    /// In A2 is every duplicities (maybe the same more times)
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="idKesek"></param>
    /// <param name="foundedDuplicities"></param>
    public static List<T> RemoveDuplicitiesList<T>(IList<T> idKesek, out List<T> foundedDuplicities)
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

    public static void Unindent(List<string> l, int v)
    {
        for (int i = 0; i < l.Count; i++)
        {
            var line = l[i];
            if (line.StartsWith(AllStrings.tab))
            {
                l[i] = l[i].Substring(AllStrings.tab.Length);

            }
            else if(line.StartsWith(Consts.spaces4))
            {
                l[i] = l[i].Substring(Consts.spaces4.Length);
            }
        }


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
        if (p == null)
        {
            return new List<string>();
        }

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

    public static List<T> ToArrayTCheckNull<T>(params T[] where)
    {

        var vr = CA.ToList(where);
        RemoveDefaultT(vr);
        return vr;
    }

    public static bool EndsWith(string fn, List<string> allowedExtension)
    {
        foreach (var item in allowedExtension)
        {
            if (fn.EndsWith(item))
            {
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// Direct edit
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="vr"></param>
    public static void RemoveDefaultT<T>(List<T> vr)
    {
        for (int i = vr.Count - 1; i >= 0; i--)
        {
            if (EqualityComparer<T>.Default.Equals(vr[i], default(T)))
            {
                vr.RemoveAt(i);
            }
        }
    }



    /// <summary>
    /// Convert IEnumerable to List<string> Nothing more, nothing less
    /// Must be private - to use only public in CA
    /// bcoz Cast() not working
    /// Dont make any type checking - could be done before
    /// </summary>
    private static List<string> ToListString2(IEnumerable enumerable)
    {
        List<string> result = new List<string>(enumerable.Count());
        foreach (var item in enumerable)
        {
            if (item == null)
            {
                result.Add(Consts.nulled);
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

    public static string ReplaceAll(string r, List<string> what, string forWhat)
    {
        foreach (var item in what)
        {
            r = r.Replace(item, forWhat);
        }

        return r;
    }

    /// <summary>
    /// element can be null, then will be added as default(T)
    /// Do hard cast
    /// If need cast to number, simply use CA.ToNumber
    /// If item is null, add instead it default(T)
    /// Simply create new list in ctor from A1
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="f"></param>
    public static List<T> ToList<T>(params T[] f)
    {
        IEnumerable enu = f;
        return ToList<T>(enu);
    }


    /// <summary>
    /// element can be null, then will be added as default(T)
    /// If item is null, add instead it default(T)
    /// cant join from IEnumerable elements because there must be T2 for element's type of collection
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="enumerable"></param>
    public static List<T> ToList<T>(IEnumerable enumerable)
    {
        // system array etc cant be casted
        var ien = enumerable as IEnumerable<object>;
        var ienf = ien.FirstOrNull() as IEnumerable;
        List<T> result = null;
        //if (enumerable is IEnumerable<char>)
        //{
        //    result = new List<T>(1);
        //    result.Add(SH.JoinIEnumerable(string.Empty, enumerable));
        //}

        bool b1 = ien != null;
        bool b2 = typeof(T) == Types.tString;
        bool b3 = ienf.Count() > 1;
        bool b4 = false;
        bool b5 = false;

        if (ienf != null)
        {
            var f = ienf.FirstOrNull();
            if (f != null)
            {
                b4 = f.GetType() == Types.tChar;
            }
        }
        if (ien != null)
        {
            var l = ien.Last();
            if (l != null)
            {
                b5 = l.GetType() == Types.tChar;
            }
        }

        if (enumerable.Count() == 1 && enumerable.FirstOrNull() is IEnumerable<object>)
        {
            result = ToListT2<T>((IEnumerable<object>)enumerable.FirstOrNull());
        }
        else if (b1 && b2&& b3 && b4 && b5)
        {
            result.Add(RuntimeHelper.CastToGeneric<T>( SH.Join(string.Empty, enumerable)));
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
    public static void RemoveWhichExists(IList<string> s, List<string> manuallyNo)
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

                    var isEnumerableChar = RH.IsOrIsDeriveFromBaseClass(type, typeof(IEnumerable<char>));
                    var isEnumerableString = RH.IsOrIsDeriveFromBaseClass(type, typeof(IEnumerable<string>));

                    if (type == typeof(string))
                    {
                        result.Add(SH.JoinIEnumerable(string.Empty, enumerable));
                    }
                    else if (isEnumerableChar)
                    {
                        // IEnumerable<char> => string
                        //enumerable2 is not string, then I can add all to list
                        result.AddRange(CA.ToListString2(enumerable));
                        //
                    }
                    else if (enumerable.Count() == 1 && enumerable.FirstOrNull() is IEnumerable<string>)
                    {
                        // IEnumerable<string> => List<string>
                        result.AddRange(((IEnumerable<string>)enumerable.FirstOrNull()).ToList());
                    }
                    else if (enumerable.Count() == 1 && enumerable.FirstOrNull() is IEnumerable && !isEnumerableChar && !isEnumerableString)
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

    public static T IndexOrNull<T>(T[] where, int v)
    {
        if (where.Length > v)
        {
            return where[v];
        }
        return default(T);
    }

    /// <summary>
    /// element can be null, then will be added as default(T)
    /// Must be private - to use only public in CA
    /// bcoz Cast() not working
    /// Dont make any type checking - could be done before
    /// </summary>
    private static List<T> ToListT2<T>(IEnumerable enumerable)
    {


        if (typeof(T) == Types.tString)
        {
            List<T> t = new List<T>();
            

            foreach (var item in enumerable)
            {
                if (item is IEnumerable)
                {
                    var ie = (IEnumerable)item;
                    StringBuilder sb = new StringBuilder();
                    foreach (var item2 in ie)
                    {
                        sb.Append(item2.ToString());
                    }
                    object t2 = sb.ToString();
                    t.Add((T)t2);
                }
                else if (item is char)
                {
                    StringBuilder sb = new StringBuilder();
                    foreach (var item2 in enumerable)
                    {
                        sb.Append(item2.ToString());
                    }
                    object t2 = sb.ToString();
                    t.Add((T)t2);
                    break;
                }
            }
            return t;

        }

        List<T> result = new List<T>(enumerable.Count());
        foreach (var item in enumerable)
        {
            if (item == null)
            {
                result.Add(default(T));
            }
            else
            {
                // cant join from IEnumerable elements because there must be T2 for element's type of collection. Use CA.TwoDimensionParamsIntoOne instead

                //var t1 = item.GetType();
                //var t2 = typeof(IEnumerable);
                //if (RH.IsOrIsDeriveFromBaseClass(t1 , t2, false) && t1 != Types.tString)
                //{
                //    //result.AddRange(item as IEnumerable);
                //    var item3 = (IEnumerable)item;

                //    foreach (var item2 in item3)
                //    {
                //        result.Add(item2);
                //    }
                //}
                //else
                //{
                try
                {
                    result.Add((T)item);
                }
                catch (Exception ex)
                {

                    
                }
                //}
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
    public static string StartWith(string item2, params string[] v1)
    {
        return StartWith(item2, v1.ToList());
    }

    public static string StartWith(string item2, IEnumerable<string> v1)
    {
        int i;
        return StartWith(item2, v1, out i);
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
    public static string StartWith(string item2, IEnumerable<string> v1, out int i )
    {
        i = -1;
        foreach (var item in v1)
        {
            i++;
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
    public static List<string> TrimStart(string backslash, List<string> s)
    {
        string methodName = "TrimStart";

        ThrowExceptions.IsNull(Exc.GetStackTrace(),type, methodName, "backslash", backslash);
        ThrowExceptions.IsNull(Exc.GetStackTrace(),type, methodName, "s", s);

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
    public static List<string> TrimStart(char backslash, params string[] s)
    {
        return TrimStart(backslash, s.ToList());
    }

    /// <summary>
    /// For all types
    /// </summary>
    /// <param name="times"></param>
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
        if (list.Count > 0)
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
            else if (item.ToString() == string.Empty)
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
    public static List<string> Trim(List<string> l)
    {
        for (int i = 0; i < l.Count; i++)
        {
            l[i] = l[i].Trim();
        }
        return l;
    }

    /// <summary>
    /// Direct edit
    /// </summary>
    /// <param name="files_in"></param>
    /// <param name="what"></param>
    /// <param name="forWhat"></param>
    public static void Replace(List<string> files_in, string what, string forWhat)
    {
        CA.ChangeContent(null,files_in, SH.Replace, what, forWhat);
    }

    /// <summary>
    /// ContainsAnyFromElement - Contains string elements of list. Return List<string>
    ///IsEqualToAnyElement - same as ContainsElement, only have switched elements. return bool
    ///IsEqualToAllElement - takes two generic list. return bool
    ///ContainsElement - at least one element must be equaled. generic. bool
    ///IsSomethingTheSame - only for string. as List.Contains. bool
    ///IsAllTheSame() - takes element and list.generic. bool
    ///IndexesWithValue() - element and list.generic. return list<int>
    ///ReturnWhichContainsIndexes() - takes two list or element and list. return List<int>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="videoCodes"></param>
    /// <param name="empty"></param>
    /// <returns></returns>
    public static List<int> IndexesWithValue<T>(List<T> videoCodes, T empty)
    {
        var result = videoCodes.Select((r, index) => new { dx = index, value = r }).Where(d => EqualityComparer<T>.Default.Equals( d.value,empty)).Select(d =>d.dx).ToList();
        return result;
    }

    
   
    public static List<T> JoinIEnumerable<T>(params IEnumerable<T>[] enumerable)
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
    /// not direct edit
    /// </summary>
    /// <param name="v"></param>
    /// <param name="dirs"></param>
    public static List<string> PostfixIfNotEnding(string v, List<string> dirs)
    {
        return ChangeContent(dirs, d => SH.PostfixIfNotEmpty(d, v));
    }

   



   
    

    /// <summary>
    /// Direct edit
    /// </summary>
    /// <param name="whereIsUsed2"></param>
    /// <param name="v"></param>
    public static List<string> WrapWith(List<string> whereIsUsed2, string v)
    {
        return WrapWith(whereIsUsed2, v, v);
    }

    /// <summary>
    /// direct edit
    /// </summary>
    /// <param name="whereIsUsed2"></param>
    /// <param name="v"></param>
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
    /// Dont use 
    /// </summary>
    /// <param name="para"></param>
    public static List<object> TwoDimensionParamsIntoOne(params object[] para)
    {
        return TwoDimensionParamsIntoOne<object>(para);
    }

    /// <summary>
    /// Join elements of inner IEnumerable to single list
    /// T is object, not IEnumerable
    /// Multi deep array is not suppported
    /// For convert into string use ListToString
    /// </summary>
    /// <param name="para"></param>
    public static List<T> TwoDimensionParamsIntoOne<T>(params object[] para)
    {
        List<T> result = new List<T>();
        foreach (var item in para)
        {
            if (item == null)
            {
                continue;
            }

            if (item is IEnumerable && item.GetType() != typeof(string))
            {
                foreach (T r in (IEnumerable)item)
                {
                    result.Add(r);
                }
            }
            else
            {
                result.Add((T)item);
            }
        }
        return result;
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

    public static IEnumerable OneElementCollectionToMulti(IEnumerable deli2)
    {
        if (deli2.Count() == 1)
        {
            var first = deli2.FirstOrNull();
            var ien = first as IEnumerable;

            if (ien != null)
            {
                return ien;
            }
            return CA.ToList<object>(first);
        }
        return deli2;
    }

    /// <summary>
    /// Direct edit
    /// </summary>
    /// <param name="slova"></param>
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
    public static List<string> ToLower(List<string> ss, bool createNewArray = false)
    {
        List<string> outArr = ss;

        if (createNewArray)
        {
            outArr = new List<string>( ss.Count);
            CA.InitFillWith(outArr, ss.Count);
        }

        for (int i = 0; i < ss.Count; i++)
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
    /// For use with mustBeAllNumbers, must use other parse func than default .net
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="parse"></param>
    /// <param name="enumerable"></param>
    /// <param name="mustBeAllNumbers"></param>
    /// <returns></returns>
    public static List<T> ToNumber<T>(Func<string, T> parse, IEnumerable enumerable)
    {
        List<T> result = new List<T>();
        foreach (var item in enumerable)
        {
            if (item.ToString() == "NA")
            {
                continue;
            }

            if (SH.IsNumber(item.ToString(), AllChars.comma, AllChars.dot, AllChars.dash))
            {
                var number = parse.Invoke(item.ToString());

                result.Add(number);
            }
        }
        return result;
    }

    /// <summary>
    /// U will be use when parsed element wont be number to return never-excepted value and recognize bad value
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="parse"></param>
    /// <param name="enumerable"></param>
    /// <param name="mustBeAllNumbers"></param>
    /// <returns></returns>
    public static List<T> ToNumber<T>(Func<string, T, T> parse, IEnumerable enumerable, T defVal, bool mustBeAllNumbers = true)
    {
        List<T> result = new List<T>();
        foreach (var item in enumerable)
        {
            var number = parse.Invoke(item.ToString(), defVal);
            if (mustBeAllNumbers)
            {
                if (EqualityComparer<T>.Default.Equals(number, defVal))
                {
                    ThrowExceptions.BadFormatOfElementInList(Exc.GetStackTrace(), type, Exc.CallingMethod(), item, nameof(enumerable));
                    return null;
                }
            }

            if (!EqualityComparer<T>.Default.Equals(number, defVal))
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
    public static List<int> ToInt(IEnumerable enumerable)
    {
        var ts = ToListString2(enumerable);
        CA.ChangeContent(null,ts, d => SH.RemoveAfterFirst(d.Replace(AllChars.comma, AllChars.dot), AllChars.dot));

        return ToNumber<int>(int.Parse, ts);
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
            if (!EqualityComparer<T>.Default.Equals(yy, y))
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
    public static List<T> ToNumber<T>(Func<string, T, T> tryParse, IEnumerable altitudes, int requiredLength, T startFrom) where T : IComparable
    {
        int finalLength = altitudes.Count() - int.Parse(startFrom.ToString());
        if (finalLength < requiredLength)
        {
            return null;
        }
        List<T> vr = new List<T>(finalLength);

        T i = default(T);
        foreach (var item in altitudes)
        {
            if (i.CompareTo(startFrom) != 0)
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
            ThrowExceptions.Custom(Exc.GetStackTrace(), type, Exc.CallingMethod(),"Chybn\u00FD parametr p");
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
    /// ContainsAnyFromElement - Contains string elements of list. Return List<string>
    ///IsEqualToAnyElement - same as ContainsElement, only have switched elements. return bool
    ///IsEqualToAllElement - takes two generic list. return bool
    ///ContainsElement - at least one element must be equaled. generic. bool
    ///IsSomethingTheSame - only for string. as List.Contains. bool
    ///IsAllTheSame() - takes element and list.generic. bool
    ///IndexesWithValue() - element and list.generic. return list<int>
    ///ReturnWhichContainsIndexes() - takes two list or element and list. return List<int>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="p"></param>
    /// <param name="list"></param>
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
    /// <summary>
    /// CA.ContainsAnyFromElement - Contains string elements of list. Return List<string>
    /// CA.IsEqualToAnyElement - same as ContainsElement, only have switched elements. return bool
    /// CA.IsEqualToAllElement - takes two generic list. return bool
    /// CA.ContainsElement - at least one element must be equaled. generic. bool
    /// CA.IsSomethingTheSame - only for string. as List.Contains. bool
    /// CA.IsAllTheSame() - takes element and list.generic. bool
    /// CA.IndexesWithValue() - element and list.generic. return list<int>
    /// CA.ReturnWhichContainsIndexes() - takes two list or element and list. return List<int>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="p"></param>
    /// <param name="prvky"></param>
    /// <returns></returns>
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

    /// <summary>
    /// Direct edit
    /// </summary>
    /// <param name="sf"></param>
    /// <param name="toTrim"></param>
    public static List<string> TrimEnd(List<string> sf, params char[] toTrim)
    {
        for (int i = 0; i < sf.Count; i++)
        {
            sf[i] = sf[i].TrimEnd(toTrim);
        }
        return sf;
    }


    /// <summary>
    /// better is use first or default, because here I also have to use default(T)
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="list"></param>
    public static T FirstOrNull<T>(List<T> list)
    {
        if (list.Count > 0)
        {
            return list[0];
        }
        return default(T);
    }

    /// <summary>
    /// ContainsAnyFromElement - return string elements of list which is contained
    /// IsEqualToAnyElement - same as ContainsElement, only have switched elements
    /// ContainsElement - at least one element must be equaled. generic
    /// IsSomethingTheSame - only for string. 
    /// ContainsElement - bool, generic, check for equal.
    /// </summary>
    /// <param name="s"></param>
    /// <param name="list"></param>
    /// <returns></returns>
    public static List<int> ContainsAnyFromElement(string s, IEnumerable<string> list)
    {
        List<int> result = new List<int>();

        int i = 0;

        foreach (var item in list)
        {
            if (s.Contains(item))
            {
                result.Add(i);
            }
            i++;
        }

        return result;
    }

    public static bool ContainsAnyFromElementBool(string s, IEnumerable<string> list)
    {
        List<int> result = new List<int>();

        foreach (var item in list)
        {
            if (s.Contains(item))
            {
                return true;
            }
        }

        return false;
    }

    public static List<int> StartWithAnyInElement(string s, List<string> list, bool _trimBefore)
    {
        List<int> result = new List<int>();

        int i = 0;
        foreach (var item in list)
        {
            var item2 = item;
            if (_trimBefore)
            {
                item2 = item2.Trim();
            }

            if (s.StartsWith(item2))
            {
                result.Add(i);
            }

            i++;
        }

        return result;
    }

    /// <summary>
    /// ContainsAnyFromElement - Contains string elements of list. Return List<string>
    ///IsEqualToAnyElement - same as ContainsElement, only have switched elements. return bool
    ///IsEqualToAllElement - takes two generic list. return bool
    ///ContainsElement - at least one element must be equaled. generic. bool
    ///IsSomethingTheSame - only for string. as List.Contains. bool
    ///IsAllTheSame() - takes element and list.generic. bool
    ///IndexesWithValue() - element and list.generic. return list<int>
    ///ReturnWhichContainsIndexes() - takes two list or element and list. return List<int> 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="list"></param>
    /// <param name="t"></param>
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

    /// <summary>
    /// Direct edit
    /// </summary>
    /// <param name="nazev"></param>
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

/// <summary>
    /// Remove elements starting with A1
    /// Direct edit
    /// </summary>
    /// <param name="start"></param>
    /// <param name="mySites"></param>
    public static List<string> RemoveStartingWith(string start, List<string> mySites, bool _trimBeforeFinding = false)
    {
        for (int i = mySites.Count - 1; i >= 0; i--)
        {
            var val = mySites[i];
            if (_trimBeforeFinding)
            {
                val = val.Trim();
            }
            if (val.StartsWith(start))
            {
                mySites.RemoveAt(i);
            }
        }
        return mySites;
    }

public static string StartWith(List<string> suMethods, string line)
    {
        string element = null;
        return StartWith(suMethods, line, out element);
    }
/// <summary>
    /// Return A2 if start something with A1
    /// Really different method than string, List<string>
    /// </summary>
    /// <param name="suMethods"></param>
    /// <param name="line"></param>
    public static string StartWith(List<string> suMethods, string line, out string element)
    {
        element = null;

        foreach (var method in suMethods)
        {
            if (line.StartsWith(method))
            {
                element = method;
                return line;
            }
        }
        return null;
    }

/// <summary>
    /// Return A1 which contains A2
    /// </summary>
    /// <param name="lines"></param>
    /// <param name="term"></param>
    public static List<string> ReturnWhichContains(List<string> lines, string term,  ContainsCompareMethod parseNegations = ContainsCompareMethod.WholeInput)
    {
        List<int> founded;
        return ReturnWhichContains(lines, term, out founded, parseNegations);
    }

/// <summary>
    /// Return A1 which contains A2
    /// </summary>
    /// <param name="lines"></param>
    /// <param name="term"></param>
    /// <param name="founded"></param>
    public static List<string> ReturnWhichContains(List<string> lines, string term, out List<int> founded, ContainsCompareMethod parseNegations = ContainsCompareMethod.WholeInput)
    {
        founded = new List<int>();
        List<string> result = new List<string>();
        int i = 0;

        List<string> w = null;
        if (parseNegations == ContainsCompareMethod.SplitToWords || parseNegations == ContainsCompareMethod.Negations)
        {
            w = SH.SplitByWhiteSpaces(term);
        }

        if (parseNegations == ContainsCompareMethod.WholeInput)
        {
            foreach (var item in lines)
            {
                if (item.Contains(term))
                {
                    founded.Add(i);
                    result.Add(item);
                }
                i++;
            }
        }
        else if(parseNegations == ContainsCompareMethod.SplitToWords || parseNegations == ContainsCompareMethod.Negations)
        {
            foreach (var item in lines)
            {
                if (SH.ContainsAll(item, w, parseNegations))
                {
                    founded.Add(i);
                    result.Add(item);
                }
                i++;
            }
        }
        else
        {
            ThrowExceptions.NotImplementedCase(Exc.GetStackTrace(),type, Exc.CallingMethod(), parseNegations);
        }

        return result;
    }

public static void Remove(List<string> input, Func<string, string, bool> pred, string arg)
    {
        for (int i = input.Count - 1; i >= 0; i--)
        {
            if (pred.Invoke(input[i], arg))
            {
                input.RemoveAt(i);
            }
        }
    }

/// <summary>
    /// Direct edit collection
    /// Na rozdíl od metody RemoveStringsEmpty i vytrimuje (ale pouze pro porovnání, v kolekci nechá)
    /// </summary>
    /// <param name="mySites"></param>
    public static List<string> RemoveStringsEmpty2(List<string> mySites)
    {
        for (int i = mySites.Count - 1; i >= 0; i--)
        {
            if (mySites[i].Trim() == string.Empty)
            {
                mySites.RemoveAt(i);
            }
        }
        return mySites;
    }

public static void JoinForGoogleSheetRow(StringBuilder sb, IEnumerable en)
    {
        sb.AppendLine(JoinForGoogleSheetRow(en));
    }
public static string JoinForGoogleSheetRow(IEnumerable en)
    {
        return SH.Join(AllChars.tab, en);
    }

public static bool HasNullValue(List<string> idPhotos)
    {
        for (int i = 0; i < idPhotos.Count; i++)
        {
            if (idPhotos[i] == null)
            {
                return true;
            }
        }
        return false;
    }

/// <summary>
    /// Create array with A2 elements, otherwise return null. If any of element has not int value, return also null.
    /// </summary>
    /// <param name="altitudes"></param>
    /// <param name="requiredLength"></param>
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

    /// <summary>
    /// Direct edit
    /// </summary>
    /// <param name="files"></param>
    /// <param name="list"></param>
    /// <param name="wildcard"></param>
public static void RemoveWhichContains(List<string> files, List<string> list, bool wildcard)
    {
        foreach (var item in list)
        {
            RemoveWhichContains(files, item, wildcard);
        }
    }
    /// <summary>
    /// Direct editr
    /// </summary>
    /// <param name="files1"></param>
    /// <param name="item"></param>
    /// <param name="wildcard"></param>
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

    public static List<string> CompareList(List<string> c1, List<string> c2)
    {
        return CompareList<string>(c1, c2);
    }

    /// <summary>
    /// Return what exists in both
    /// Modify both A1 and A2 - keep only which is only in one
    /// </summary>
    /// <param name="c1"></param>
    /// <param name="c2"></param>
    public static List<T> CompareList<T>(List<T> c1, List<T> c2) where T : IEquatable<T>
    {
        List<T> existsInBoth = new List<T>();

        int dex = -1;

        for (int i = c2.Count - 1; i >= 0; i--)
        {
            T item = c2[i];
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
            T item = c1[i];
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
        InitFillWith<string>(datas, pocet, initWith);
    }

    public static void InitFillWith<T>(List<T> datas, int pocet, T initWith )
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

public static bool HasPostfix(string key, params string[] v1)
    {
        foreach (var item in v1)
        {
            if (key.EndsWith(item))
            {
                return true;
            }
        }
        return false;
    }

/// <summary>
    /// Direct edit
    /// </summary>
    /// <param name="numbered"></param>
    /// <param name="input"></param>
    private static void Prepend(List<string> numbered, List<string> input)
    {
        ThrowExceptions.DifferentCountInLists(Exc.GetStackTrace(),type, SunamoPageHelperSunamo.i18n(XlfKeys.Prepend), "numbered", numbered.Count(), "input", input.Count);
        for (int i = 0; i < input.Count; i++)
        {
            input[i] = numbered[i] + input[i];
        }
    }
/// <summary>
    /// Direct edit input collection
    /// </summary>
    /// <param name="v"></param>
    /// <param name="toReplace"></param>
    public static List<string> Prepend(string v, List<string> toReplace)
    {
        for (int i = 0; i < toReplace.Count; i++)
        {
            if (!toReplace[i].StartsWith(v))
            {
                toReplace[i] = v + toReplace[i];
            }
        }
        return toReplace;
    }
/// <summary>
    /// Direct edit input collection
    /// </summary>
    /// <param name="v"></param>
    /// <param name="toReplace"></param>
    public static List<string> Prepend(string v, String[] toReplace)
    {
        return Prepend(v, toReplace.ToList());
    }

    /// <summary>ContainsAnyFromElement - Contains string elements of list
    /// IsEqualToAnyElement - same as ContainsElement, only have switched elements
    /// ContainsElement - at least one element must be equaled. generic
    /// IsSomethingTheSame - only for string. 
    /// AnySpaces - split A2 by spaces and A1 must contains all parts
    /// ExactlyName - ==
    /// FixedSpace - simple contains
    /// 
    /// ContainsAnyFromElement - Contains string elements of list. Return List<string>
    ///IsEqualToAnyElement - same as ContainsElement, only have switched elements. return bool
    ///IsEqualToAllElement - takes two generic list. return bool
    ///ContainsElement - at least one element must be equaled. generic. bool
    ///IsSomethingTheSame - only for string. as List.Contains. bool
    ///IsAllTheSame() - takes element and list.generic. bool
    ///IndexesWithValue() - element and list.generic. return list<int>
    ///ReturnWhichContainsIndexes() - takes two list or element and list. return List<int>
    /// </summary>
    /// <param name="value"></param>
    /// <param name="term"></param>
    /// <param name="searchStrategy"></param>
    public static List<int> ReturnWhichContainsIndexes(IEnumerable<string> value, string term, SearchStrategy searchStrategy = SearchStrategy.FixedSpace)
    {
        List<int> result = new List<int>();
        int i = 0;
        foreach (var item in value)
        {
            if (SH.Contains(item, term, searchStrategy))
            {
                result.Add(i);
            }
            i++;
        }
        return result;
    }

    /// <summary>
    /// CA.ContainsAnyFromElement - Contains string elements of list. Return List<string>
    /// CA.IsEqualToAnyElement - same as ContainsElement, only have switched elements. return bool
    /// CA.IsEqualToAllElement - takes two generic list. return bool
    /// CA.ContainsElement - at least one element must be equaled. generic. bool
    /// CA.IsSomethingTheSame - only for string. as List.Contains. bool
    /// CA.IsAllTheSame() - takes element and list.generic. bool
    /// CA.IndexesWithValue() - element and list.generic. return list<int>
    /// CA.ReturnWhichContainsIndexes() - takes two list or element and list. return List<int>
    /// </summary>
    /// <param name="parts"></param>
    /// <param name="mustContains"></param>
    /// <returns></returns>
    public static IList<int> ReturnWhichContainsIndexes(IEnumerable<string> parts, IEnumerable<string> mustContains)
    {
        CollectionWithoutDuplicates<int> result = new CollectionWithoutDuplicates<int>();
        foreach (var item in mustContains)
        {
            result.AddRange(ReturnWhichContainsIndexes(parts, item));
        }
        return result.c;
    }

public static string FindOutLongestItem(List<string> list, params string[] delimiters)
    {
        int delkaNejdelsiho = 0;
        string nejdelsi = "";
        foreach (var item in list)
        {
            string tem = item;
            if (delimiters.Length != 0)
            {
                tem = SH.Split(item, delimiters)[0].Trim();
            }
            if (delkaNejdelsiho < tem.Length)
            {
                nejdelsi = tem;
                delkaNejdelsiho = tem.Length;
            }
        }
        return nejdelsi;
    }
}