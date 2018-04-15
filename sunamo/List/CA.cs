
using sunamo.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;


public static class CA
{
    /// <summary>
    /// 
    /// Modify both A1 and A2 - keep only which is only in one
    /// </summary>
    /// <param name="c1"></param>
    /// <param name="c2"></param>
    public static List<string> CompareList( List<string> c1,  List<string> c2)
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

    public static int CountOfEnding(List<string> winrarFiles, string v)
    {
        int count = 0;
        for (int i = 0; i < winrarFiles.Count; i++)
        {
            if (winrarFiles[i].EndsWith(v))
            {
                count++;
            }
        }
        return count;
    }

    public static bool HasIndex(int dex, Array col)
    {
        return col.Length > dex;
    }

    public static List<T> ToList<T>(params T[] f)
    {
        return new List<T>(f);
    }

    public static bool IsInRange(int od, int to, int index)
    {
        return od >= index && to <= index;
    }

    public static List<int> IndexesWithNull<T>(List<Nullable< T>> times) where T : struct
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

    public static List<T> CreateListAndInsertElement<T>(T el)
    {
        List<T> t = new List<T>();
        t.Add(el);
        return t;
    }

    public static bool ContainsElement<T>(IEnumerable<T> list, T t)
    {
        foreach (T item in list)
        {
            if (!Comparer<T>.Equals(item, t))
            {
                return false;
            }
        }
        return true;
    }

    public static List<string> ReturnWhichContains(List<string> lines, string item)
    {
        return lines.FindAll(d => d.Contains(item));
    }

    /// <summary>
    /// Is useful when want to wrap and also join with string. Also last element will have delimiter
    /// </summary>
    /// <param name="list"></param>
    /// <param name="wrapWith"></param>
    /// <param name="delimiter"></param>
    /// <returns></returns>
    public static List<string> WrapWithAndJoin(IEnumerable<string> list, string wrapWith, string delimiter)
    {
        return list.Select(i => wrapWith + i + wrapWith + delimiter).ToList();
    }

    public static void RemoveWhichContains(List<string> files1, string item, bool wildcard)
    {
        if (wildcard)
        {



            //item = SH.WrapWith(item, AllChars.asterisk);
            for (int i = files1.Count - 1; i >= 0; i--)
            {
                //if (item == @"\\obj\\")
                //{
                //    if (files1[i].Contains(@"\obj\"))
                //    {
                //        Debugger.Break();
                //    }
                //}

                //if (files1[i].Contains(@"\obj\"))
                //{
                //    Debugger.Break();
                //}


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

    public static List<int> ReturnWhichContainsIndexes(List<string> value, string term)
    {
        List<int> result = new List<int>();
        for (int i = 0; i < value.Count; i++)
        {
            if (value[i].Contains(term))
            {
                result.Add(i);
            }
        }
        return result;
    }

    public static int PartsCount(int count, int inPart)
    {
        int celkove = count / inPart;
        if (count % inPart != 0)
        {
            celkove++;
        }
        return celkove;
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

    #region input Object IEnumerable
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

    public static int GetLength(IList where)
    {
        if (where == null)
        {
            return 0;
        }
        return where.Count;
    }

    public static bool HasIndex(int p, IList nahledy)
    {
        if (p < 0)
        {
            throw new Exception("Chybný parametr p");
        }
        if (nahledy.Count > p)
        {
            return true;
        }
        return false;
    }

    public static List<string> WrapWith(IList<string> whereIsUsed2, string v)
    {
        List<string> result = new List<string>();
        for (int i = 0; i < whereIsUsed2.Count; i++)
        {
            result.Add( v + whereIsUsed2[i] + v);
        }
        return result;
    }

    public static string[] WrapWithIf(Func<string, string, bool, bool> f, bool invert, string mustContains, string wrapWith, params string[] whereIsUsed2)
    {
        for (int i = 0; i < whereIsUsed2.Length; i++)
        {
            if (f.Invoke(whereIsUsed2[i], mustContains, invert))
            {
                whereIsUsed2[i] = wrapWith + whereIsUsed2[i] + wrapWith;
            }
        }
        return whereIsUsed2;
    }

    public static bool MatchWildcard(List<string> list, string file)
    {
        return list.Any(d => SH.MatchWildcard(file, d));
    }

    public static object[] JoinVariableAndArray(object p, object[] sloupce)
    {
        List<object> o = new List<object>();
        o.Add(p);
        o.AddRange(sloupce);
        return o.ToArray();
    }
    #endregion

    #region input Numeric IEnumerable
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

    public static int IndexOfValue(List<int> allWidths, int width)
    {
        for (int i = 0; i < allWidths.Count; i++)
        {
            if (allWidths[i] == width)
            {
                return i;
            }
        }
        return -1;
    }

    public static List<byte> JoinBytesArray(byte[] pass, byte[] salt)
    {
        List<byte> lb = new List<byte>(pass.Length + salt.Length);
        lb.AddRange(pass);
        lb.AddRange(salt);
        return lb;
    }

    public static bool AreTheSame(byte[] p, byte[] p2)
    {
        if (p.Length != p2.Length)
        {
            return false;
        }
        for (int i = 0; i < p.Length; i++)
        {
            if (p[i] != p2[i])
            {
                return false;
            }
        }
        return true;
    }
    #endregion

    #region input String IEnumerable
    public static bool HasFirstItemLength(string[] notContains)
    {
        string t = "";
        if (notContains.Length > 0)
        {
            t = notContains[0].Trim();
        }
        return t.Length > 0;
    }

    public static string[] Trim(string[] l)
    {
        for (int i = 0; i < l.Length; i++)
        {
            l[i] = l[i].Trim();
        }
        return l;
    }

    

    public static string[] EnsureBackslash(string[] eb)
    {
        

        for (int i = 0; i < eb.Length; i++)
        {
            string r = eb[i];
            if (r[r.Length - 1] != AllChars.bs)
            {
                eb[i] = r + sunamo.Values.Consts.bs;
            }
        }

        return eb;
    }

    public static List<string> EnsureBackslash(List<string> eb)
    {
        for (int i = 0; i < eb.Count; i++)
        {
            string r = eb[i];
            if (r[r.Length - 1] != AllChars.bs)
            {
                eb[i] = r + sunamo.Values.Consts.bs;
            }
        }

        return eb;
    }

    public static List<string> TrimList(List<string> c)
    {
        for (int i = 0; i < c.Count; i++)
        {
            c[i] = c[i].Trim();
        }
        return c;
    }

    public static string GetTextAfterIfContainsPattern(string input, string ifNotFound, string[] uriPatterns)
    {
        foreach (var item in uriPatterns)
        {
            int nt = input.IndexOf(item);
            if (nt != -1)
            {
                if (input.Length > item.Length + nt)
                {
                    return input.Substring(nt + item.Length);
                }
            }
        }
        return ifNotFound;
    }

    /// <summary>
    /// WithEndSlash - trims backslash and append new
    /// WithoutEndSlash - ony trims backslash
    /// </summary>
    /// <param name="folders"></param>
    /// <returns></returns>
    public static string[] WithEndSlash(string[] folders)
    {
        for (int i = 0; i < folders.Length; i++)
        {
            folders[i] = sunamo.FS.WithEndSlash(folders[i]);
        }
        return folders;
    }

    public static string[] WithoutEndSlash(string[] folders)
    {
        for (int i = 0; i < folders.Length; i++)
        {
            folders[i] = sunamo.FS.WithoutEndSlash(folders[i]);
        }
        return folders;
    }

    /// <summary>
    /// Remove elements starting with A1
    /// </summary>
    /// <param name="start"></param>
    /// <param name="mySites"></param>
    /// <returns></returns>
    public static List<string> RemoveStartingWith(string start, List<string> mySites)
    {
        for (int i = mySites.Count - 1; i >= 0; i--)
        {
            if (mySites[i].StartsWith(start))
            {
                mySites.RemoveAt(i);
            }
        }
        return mySites;
    }

    public static List<string> ToLower(List<string> slova)
    {
        for (int i = 0; i < slova.Count; i++)
        {
            slova[i] = slova[i].ToLower();
        }
        return slova;
    }

    public static bool AnyElementEndsWith(string t, params string[] v)
    {
        foreach (var item in v)
        {
            if (t.EndsWith(item))
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
    public static bool Contains(string value, string[] availableValues)
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

    public static List<string> JoinArrayAndArrayString(string[] a, params string[] p)
    {
        if (a != null)
        {
            List<string> d = new List<string>(a.Length + p.Length);
            d.AddRange(a);
            d.AddRange(p);
            return d;
        }
        return new List<string>(p);
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

    public static bool HasOtherValueThanNull(List<string> idPhotos)
    {
        foreach (var item in idPhotos)
        {
            if (item != null)
            {
                return true;
            }
        }
        return false;
    }

    public static bool HasIndexWithValueWithoutException(int p, string[] nahledy, string item)
    {
        if (p < 0)
        {
            return false;
        }
        if (nahledy.Length > p && nahledy[p] == item)
        {
            return true;
        }
        return false;
    }

  

    public static void AddIfNotContains<T>(List<T> founded, T e)
    {
        if (!founded.Contains(e))
        {
            founded.Add(e);
        }
    }

    public static List<string> GetRowOfTable(List<string[]> _dataBinding, int i2)
    {
        List<string> vr = new List<string>();
        for (int i = 0; i < _dataBinding.Count; i++)
        {
            vr.Add(_dataBinding[i][i2]);
        }
        return vr;
    }

    public static bool HasAtLeastOneElementInArray(string[] d)
    {
        if (d != null)
        {
            if (d.Length != 0)
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

    /// <summary>
    /// Na rozdíl od metody RemoveStringsEmpty2 NEtrimuje před porovnáním
    /// </summary>
    public static List<string> RemoveStringsByScopeKeepAtLeastOne(List<string> mySites, FromTo fromTo, int keepLines)
    {
        mySites.RemoveRange(fromTo.from, fromTo.to - fromTo.from + 1);
        for (int i = fromTo.from; i < fromTo.from - 1+keepLines; i++)
        {
            mySites.Insert(i, "");
        }

        return mySites;
    }

    /// <summary>
    /// Na rozdíl od metody RemoveStringsEmpty2 NEtrimuje před porovnáním
    /// </summary>
    /// <param name="mySites"></param>
    /// <returns></returns>
    public static string[] RemoveStringsEmpty(string[] mySites)
    {
        List<string> dd = new List<string>();
        foreach (string item in mySites)
        {
            if (item != "")
            {
                dd.Add(item);
            }
        }
        return dd.ToArray();
    }

    /// <summary>
    /// Na rozdíl od metody RemoveStringsEmpty2 NEtrimuje před porovnáním
    /// </summary>
    /// <param name="mySites"></param>
    /// <returns></returns>
    public static List<string> RemoveStringsEmpty(List<string> mySites)
    {
        List<string> dd = new List<string>();
        foreach (string item in mySites)
        {
            if (item != "")
            {
                dd.Add(item);
            }
        }
        return dd;
    }

    /// <summary>
    /// Na rozdíl od metody RemoveStringsEmpty i vytrimuje
    /// </summary>
    /// <param name="mySites"></param>
    /// <returns></returns>
    public static List<string> RemoveStringsEmpty2(List<string> mySites)
    {
        List<string> dd = new List<string>();
        foreach (string item in mySites)
        {
            if (item.Trim() != "")
            {
                dd.Add(item);
            }
        }
        return dd;
    }

    /// <summary>
    /// Return first A2 elements of A1 or A1 if A2 is bigger
    /// </summary>
    /// <param name="proj"></param>
    /// <param name="p"></param>
    /// <returns></returns>
    public static List<string> ShortCircuit(List<string> proj, int p)
    {
        List<string> vratit = new List<string>();
        if (p > proj.Count)
        {
            p = proj.Count;

        }
        for (int i = 0; i < p; i++)
        {
            vratit.Add(proj[i]);

        }
        return vratit;

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

    public static List<string> ContainsDiacritic(IEnumerable<string> nazvyReseni)
    {
        List<string> vr = new List<string>(nazvyReseni.Count());
        foreach (var item in nazvyReseni)
        {
            if (SH.ContainsDiacritic(item))
            {
                vr.Add(item);
            }
        }
        return vr;
    }

    public static bool IsSomethingTheSame(string ext, params string[] p1)
    {
        for (int i = 0; i < p1.Length; i++)
        {
            if (p1[i] == ext)
            {
                return true;
            }
        }
        return false;
    }
    #endregion

    #region input Generic IEnumerable
    public static T[,] OneDimensionArrayToTwoDirection<T>(T[] flatArray, int width)
    {
        int height = (int)Math.Ceiling(flatArray.Length / (double)width);
        T[,] result = new T[height, width];
        int rowIndex, colIndex;

        for (int index = 0; index < flatArray.Length; index++)
        {
            rowIndex = index / width;
            colIndex = index % width;
            result[rowIndex, colIndex] = flatArray[index];
        }
        return result;
    }

    public static int IndexOfValue<T>(List<T> allWidths, T width)
    {
        for (int i = 0; i < allWidths.Count; i++)
        {
            if (EqualityComparer<T>.Default.Equals(allWidths[i], width))
            {
                return i;
            }
        }
        return -1;
    }

    public static int CountOfValue<T>(T v, params T[] show)
    {
        int vr = 0;
        foreach (var item in show)
        {
            if (EqualityComparer<T>.Default.Equals(item, v))
            {
                vr++;
            }
        }
        return vr;
    }

    public static bool IsEqualToAnyElement<T>(T p, params T[] prvky)
    {
        foreach (T item in prvky)
        {
            if (EqualityComparer<T>.Default.Equals(p, item))
            {
                return true;
            }
        }
        return false;
    }

    public static bool IsTheSame<T>(IEnumerable<T> sloupce, IEnumerable<T> sloupce2)
    {
        return sloupce.SequenceEqual(sloupce2);
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

    public static T GetElementActualOrBefore<T>(IList<T> tabItems, int indexClosedTabItem)
    {
        if (HasIndex(indexClosedTabItem, (IList)tabItems))
        {
            return tabItems[indexClosedTabItem];
        }
        indexClosedTabItem--;
        if (HasIndexWithoutException(indexClosedTabItem, (IList)tabItems))
        {
            return tabItems[indexClosedTabItem];
        }
        return default(T);
    }

    /// <summary>
    /// V prvním indexu jsou řádky, v druhém sloupce
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="a"></param>
    /// <param name="dex"></param>
    /// <returns></returns>
    public static List<T> GetColumnOfTwoDimensionalArray<T>(T[,] rows, int dex)
    {
        int rowsCount = rows.GetLength(0);
        int columnsCount = rows.GetLength(1);

        List<T> vr = new List<T>(rowsCount);

        if (dex < columnsCount)
        {
            for (int i = 0; i < rowsCount; i++)
            {
                vr.Add(rows[i, dex]);
            }
            return vr;
        }

        throw new ArgumentOutOfRangeException("Invalid row index in method CA.GetRowOfTwoDimensionalArray();");
    }

    public static List<T> RemoveDuplicitiesList<T>(List<T> idKesek)
    {
        List<T> foundedDuplicities;
        return RemoveDuplicitiesList<T>(idKesek, out foundedDuplicities);
    }

    public static List<T> RemoveDuplicitiesList<T>(List<T> idKesek, out List<T> foundedDuplicities)
    {
        foundedDuplicities = new List<T>();
        List<T> h = new List<T>();
        foreach (T item in idKesek)
        {
            if (!h.Contains(item))
            {
                h.Add(item);
            }
            else
            {
                foundedDuplicities.Add(item);
            }
        }
        return h;
    }

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

    public static bool IsAllTheSame<T>(T ext, params T[] p1)
    {
        for (int i = 0; i < p1.Length; i++)
        {
            if (!EqualityComparer<T>.Default.Equals(p1[i], ext))
            {
                return false;
            }
        }
        return true;
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

    /// <summary>
    /// V prvním indexu jsou řádky, v druhém sloupce
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="a"></param>
    /// <param name="dex"></param>
    /// <returns></returns>
    public static List<T> GetRowOfTwoDimensionalArray<T>(T[,] rows, int dex)
    {
        int rowsCount = rows.GetLength(0);
        int columnsCount = rows.GetLength(1);

        List<T> vr = new List<T>(columnsCount);

        if (dex < rowsCount)
        {
            for (int i = 0; i < columnsCount; i++)
            {
                vr.Add(rows[dex, i]);
            }
            return vr;
        }

        throw new ArgumentOutOfRangeException("Invalid row index in method CA.GetRowOfTwoDimensionalArray();");
    }
    #endregion

    #region To Array (without change) - output Generic
    public static T[] ToArrayT<T>(params T[] aB)
    {
        return aB;
    }
    #endregion

    public static IEnumerable ToEnumerable(params object[] p)
    {
        return p;
    }

    public static IEnumerable<string> ToEnumerable(params string[] p)
    {
        return p;
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

    public static List<long> ToLong(IEnumerable enumerable)
    {
        List<long> result = new List<long>();
        foreach (var item in enumerable)
        {
            result.Add(long.Parse(item.ToString()));
        }
        return result;
    }

    public static List<string> ToListString(IEnumerable enumerable)
    {
        List<string> result = new List<string>();
        foreach (var item in enumerable)
        {
            result.Add(item.ToString());
        }
        return result;
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

    public static List<object> ToObject(IEnumerable enumerable)
    {
        List<object> result = new List<object>();
        foreach (var item in enumerable)
        {
            result.Add(item);
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

    /// <summary>
    /// Create array with A2 elements, otherwise return null. If any of element has not int value, return also null.
    /// </summary>
    /// <param name="altitudes"></param>
    /// <param name="requiredLength"></param>
    /// <returns></returns>
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

    #region To Array (without change) - output Object type
    

    public static string[] TrimStart(char backslash, params string[] s)
    {
        for (int i = 0; i < s.Length; i++)
        {
            s[i] = s[i].TrimStart(backslash);
        }
        return s;
    }

    public static string[] ToSize(string[] input, int requiredLength)
    {
        string[] returnArray = null;
        int realLength = input.Length;

        if (realLength > requiredLength)
        {
            returnArray = new string[requiredLength];
            for (int i = 0; i < requiredLength; i++)
            {
                returnArray[i] = input[i];
            }
            return returnArray;
        }
        else if (realLength == requiredLength)
        {
            return input;
        }
        else if (realLength < requiredLength)
        {
            returnArray = new string[requiredLength];
            int i = 0;
            for (; i < realLength; i++)
            {
                returnArray[i] = input[i];
            }
            for (; i < requiredLength; i++)
            {
                returnArray[i] = null;
            }
        }
        return returnArray;
    }

    public static string[] Prepend(string v, string[] toReplace)
    {
        for (int i = 0; i < toReplace.Length; i++)
        {
            toReplace[i] = v + toReplace[i];
        }
        return toReplace;
    }

    public static List<string> Format(string uninstallNpmPackageGlobal, List<string> globallyInstalledTsDefinitions)
    {
        for (int i = 0; i < globallyInstalledTsDefinitions.Count(); i++)
        {
            globallyInstalledTsDefinitions[i] = string.Format(uninstallNpmPackageGlobal, globallyInstalledTsDefinitions[i]);
        }
        return globallyInstalledTsDefinitions;
    }

    public static List<string> ChangeContent(List<string> files_in, Func<string, string> func)
    {
        for (int i = 0; i < files_in.Count; i++)
        {
            files_in[i] = func.Invoke(files_in[i]);
        }
        return files_in;
    }
    #endregion


}
