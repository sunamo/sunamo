using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static partial class CA
{
    public static Func<IEnumerable, int> dCount = null;

    

    /// <summary>
    /// Pokud potřebuješ vrátit null když něco nebude sedět, použij ToInt s parametry nebo ToIntMinRequiredLength
    /// </summary>
    /// <param name="altitudes"></param>
    public static List<int> ToInt(IEnumerable enumerable)
    {
        var ts = ToListString2(enumerable);
        CA.ChangeContent(null, ts, d => SH.RemoveAfterFirst(d.Replace(AllChars.comma, AllChars.dot), AllChars.dot));

        return ToNumber<int>(int.Parse, ts);
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


    public static List<string> ChangeContent(ChangeContentArgs a, List<string> files_in, Func<string, string, string, string> func, string a1, string a2)
    {
        for (int i = 0; i < files_in.Count; i++)
        {
            files_in[i] = func.Invoke(files_in[i], a1, a2);
        }

        RemoveNullOrEmpty(a, files_in);

        return files_in;
    }



    /// <summary>
    /// Direct edit
    /// </summary>
    /// <param name="files_in"></param>
    /// <param name="what"></param>
    /// <param name="forWhat"></param>
    public static void Replace(List<string> files_in, string what, string forWhat)
    {
        CA.ChangeContent(null, files_in, SH.Replace, what, forWhat);
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

    #region 6) IsAllTheSame
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
    /// <param name="ext"></param>
    /// <param name="p1"></param>
    /// <returns></returns>
    public static bool IsAllTheSame<T>(T ext, IList<T> p1)
    {
        for (int i = 0; i < p1.Count; i++)
        {
            if (!EqualityComparer<T>.Default.Equals(p1[i], ext))
            {
                return false;
            }
        }
        return true;
    }
    #endregion

    #region 4) ContainsElement
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
    #endregion

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
    /// Just call ToListString
    /// </summary>
    /// <param name="enumerable"></param>
    public static List<string> ToListString(params object[] enumerable)
    {
        IEnumerable ienum = enumerable;
        return ToListString(ienum);
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

    /// <summary>
    /// not direct edit
    /// </summary>
    /// <param name="v"></param>
    /// <param name="dirs"></param>
    public static List<string> PostfixIfNotEnding(string v, List<string> dirs)
    {
        return ChangeContent(new ChangeContentArgs() { }, dirs, SH.PostfixIfNotEmpty, v);
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


    public static void InitFillWith(List<string> datas, int pocet, string initWith = Consts.stringEmpty)
    {
        InitFillWith<string>(datas, pocet, initWith);
    }

    public static void InitFillWith<T>(List<T> datas, int pocet, T initWith)
    {
        for (int i = 0; i < pocet; i++)
        {
            datas.Add(initWith);
        }
    }

    public static void InitFillWith<T>(List<T> arr, int columns)
    {
        for (int i = 0; i < columns; i++)
        {
            arr.Add(default(T));
        }
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
    public static T[] ToArrayT<T>(params T[] aB)
    {
        return aB;
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
        #region 1
        IEnumerable enu = f;
        return ToList<T>(enu);
        #endregion
        // cant be ToList<T> - StackOverflowException!!!
        #region 2
        //return new List<T>(f); 
        #endregion
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
        else if (b1 && b2 && b3 && b4 && b5)
        {
            result.Add(RuntimeHelper.CastToGeneric<T>(SH.Join(string.Empty, enumerable)));
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
                var t = item.GetType();
                // !(item is string)  - not working
                if (RH.IsOrIsDeriveFromBaseClass(t, Types.tIEnumerable))
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
    public static int Count(IEnumerable e)
    {
        
        if (e == null)
        {
            return 0;
        }

        var t = e.GetType();
        var tName = t.Name;
        if (ThreadHelper.NeedDispatcher(tName))
        {
            var result = CA.dCount(e);
            return result;
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
    public static IEnumerable<string> ToEnumerable(params string[] p)
    {
        return p;
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
}
