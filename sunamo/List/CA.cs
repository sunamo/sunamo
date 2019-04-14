
using sunamo.Collections;
using sunamo.Data;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

public static partial  class CA
{
    static Type type = typeof(CA);

    public static string SwitchForGoogleSheets(List<string> captions, List<List<string>> exists)
    {
        ValuesTableGrid<string> vtg = new ValuesTableGrid<string>(exists);
        vtg.captions = captions;

        DataTable dt = vtg.SwitchRowsAndColumn();

        StringBuilder sb = new StringBuilder();

        foreach (DataRow item in dt.Rows)
        {
            JoinForGoogleSheetRow(sb, item.ItemArray);
        }



        string vr = sb.ToString();
        DebugLogger.Instance.WriteLine(vr);
        return vr;
    }

    public static void JoinForGoogleSheetRow(StringBuilder sb, IEnumerable en)
    {
        sb.AppendLine(SH.Join(AllChars.tab, en));
    }

    public static string GetNumberedList(List<string> input)
    {
        CA.RemoveStringsEmpty2(input);
        CA.PrependWithNumbered(input);
        return SH.JoinNL(input);
    }

    private static void PrependWithNumbered(List<string> input)
    {
        var numbered = BTS.GetNumberedListFromTo(1, input.Count - 1, ") ");
        Prepend(numbered, input);
    }

    private static void Prepend(List<string> numbered, List<string> input)
    {
        ThrowExceptions.DifferentCountInLists(type, "Prepend", "numbered", numbered.Count(), "input", input.Count);

        for (int i = 0; i < input.Count; i++)
        {
            input[i] = numbered[i] + input[i]; 
        }

    }

    public static ABL<string, string> CompareListDifferent(List<string> c1, List<string> c2)
    {
        List<string> existsIn1 = new List<string>();
        List<string> existsIn2 = new List<string>();

        int dex = -1;

        for (int i = c2.Count - 1; i >= 0; i--)
        {
            string item = c2[i];
            dex = c1.IndexOf(item);
            if (dex == -1)
            {
                existsIn2.Add(item);
            }
        }

        for (int i = c1.Count - 1; i >= 0; i--)
        {
            string item = c1[i];
            dex = c2.IndexOf(item);
            if (dex == -1)
            {
                existsIn1.Add(item);
            }
        }

        ABL<string, string> abl = new ABL<string, string>();
        abl.a = existsIn1;
        abl.b = existsIn2;

        return abl;
    }

    /// <summary>
    /// Return whether all of A1 are in A2
    /// Not all from A2 must be A1
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="searchTerms"></param>
    /// <param name="key"></param>
    /// <returns></returns>
    public static bool IsEqualToAllElement<T>(List<T> searchTerms, List<T> key)
    {
        foreach (var item in searchTerms)
        {
            if (!CA.IsEqualToAnyElement<T>(item, key))
            {
                return false;
            }
        }
        return true;
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

    public static List<string> PaddingByEmptyString(List<string> list, int columns)
    {
        for (int i = list.Count - 1; i < columns-1; i++)
        {
            list.Add(string.Empty);
        }
        return list;
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

    public static List<string> OnlyFirstCharUpper(List<string> list)
    {
        return ChangeContent(list, SH.OnlyFirstCharUpper);
    }

    public static bool IsInRange(int od, int to, int index)
    {
        return od >= index && to <= index;
    }


    public static void Remove(List<string> input, Func<string, string, bool> pred, string arg)
    {
        for (int i = input.Count - 1; i >= 0; i--)
        {
            if (pred.Invoke( input[i], arg))
            {
                input.RemoveAt(i);
            }
        }

    }

    /// <summary>
    /// Return A2 if start something with A1
    /// </summary>
    /// <param name="suMethods"></param>
    /// <param name="line"></param>
    /// <returns></returns>
    public static string StartWith(List<string> suMethods, string line)
    {
        foreach (var method in suMethods)
        {
            if (line.StartsWith(method))
            {
                return line;
            }
        }
        return null;
    }

    public static List<T> CreateListAndInsertElement<T>(T el)
    {
        List<T> t = new List<T>();
        t.Add(el);
        return t;
    }

    public static List<string> DummyElementsCollection(int count)
    {
        return Enumerable.Repeat<string>(string.Empty, count).ToList();   
    }

    /// <summary>
    /// Is same as IsEqualToAnyElement, only have switched elements
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="list"></param>
    /// <param name="t"></param>
    /// <returns></returns>
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

    public static List<string> ReturnWhichContains(List<string> lines, string term)
    {
        List<int> founded;
        return ReturnWhichContains(lines, term, out founded);
    }

    public static List<string> ReturnWhichContains(List<string> lines, string term, out List<int> founded)
    {
        founded = new List<int>();
        List<string> result = new List<string>();
        int i = 0;
        foreach (var item in lines)
        {
            if (item.Contains(term))
            {
                founded.Add(i);
                result.Add(item);
            }
            i++;
        }

        return result;
    }

    private static IEnumerable<int> ReturnWhichAreEqualIndexes<T>(IEnumerable<T> parts, T value)
    {
        List<int> result = new List<int>();
        int i = 0;
        foreach (var item in parts)
        {
            if (EqualityComparer<T>.Default.Equals(item, value))
            {
                result.Add(i);
            }
            i++;
        }

        return result;
    }

    public static List<int> ReturnWhichContainsIndexes(IEnumerable<string> value, string term, SearchStrategy searchStrategy = SearchStrategy.FixedSpace)
    {
        List<int> result = new List<int>();
        int i = 0;
            foreach (var item in value)
            {
                if (SH.Contains( item, term, searchStrategy))
                {
                    result.Add(i);
                }
                i++;
            }
        
        

        return result;
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

    
    public static int PartsCount(int count, int inPart)
    {
        int celkove = count / inPart;
        if (count % inPart != 0)
        {
            celkove++;
        }
        return celkove;
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


    public static bool HasFirstItemLength(string[] notContains)
    {
        string t = "";
        if (notContains.Length > 0)
        {
            t = notContains[0].Trim();
        }
        return t.Length > 0;
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
    /// Direct edit 
    /// WithEndSlash - trims backslash and append new
    /// WithoutEndSlash - ony trims backslash
    /// </summary>
    /// <param name="folders"></param>
    /// <returns></returns>
    public static List<string> WithEndSlash(List<string> folders)
    {
        List<string> list = folders as List<string>;
        if (list == null)
        {
            list = folders.ToList();
        }
        for (int i = 0; i < list.Count; i++)
        {
            list[i] = FS.WithEndSlash(list[i]);
        }
        return folders;
    }

    public static string[] WithoutEndSlash(string[] folders)
    {
        for (int i = 0; i < folders.Length; i++)
        {
            folders[i] = FS.WithoutEndSlash(folders[i]);
        }
        return folders;
    }

    /// <summary>
    /// Remove elements starting with A1
    /// Direct edit
    /// </summary>
    /// <param name="start"></param>
    /// <param name="mySites"></param>
    /// <returns></returns>
    public static List<string> RemoveStartingWith(string start, List<string> mySites, bool trimBeforeFinding = false)
    {
        for (int i = mySites.Count - 1; i >= 0; i--)
        {
            var val = mySites[i];
            if (trimBeforeFinding)
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

    public static List<int> ReturnWhichContainsIndexes(IEnumerable<string> parts, IEnumerable<string> mustContains)
    {
        CollectionWithoutDuplicates<int> result = new CollectionWithoutDuplicates<int>();
        foreach (var item in mustContains)
        {
            result.AddRange(ReturnWhichContainsIndexes(parts, item));
        }
        return result.c;
    }

    private static List<int> ReturnWhichAreEqualIndexes<T>(IEnumerable<T> parts, IEnumerable<T> mustBeEqual)
    {
        CollectionWithoutDuplicates<int> result = new CollectionWithoutDuplicates<int>();
        foreach (var item in mustBeEqual)
        {
            result.AddRange(ReturnWhichAreEqualIndexes<T>(parts, item));
        }
        return result.c;
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

    public static List<string> JoinArrayAndArrayString(IEnumerable<string> a, IEnumerable<string> p)
    {
        if (a != null)
        {
            List<string> d = new List<string>(a.Length() + p.Length());
            d.AddRange(a);
            d.AddRange(p);
            return d;
        }
        return new List<string>(p);
    }

    public static List<string> JoinArrayAndArrayString(IEnumerable<string> a, params string[] p)
    {
        return JoinArrayAndArrayString(a, p.ToList());
    }

    public static void CheckExists(List<bool> photoFiles, List<string> allFilesRelative, List<string> value)
    {

        foreach (var item in allFilesRelative)
        {
            photoFiles.Add(value.Contains(item));
        }
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
        for (int i = fromTo.from; i < fromTo.from - 1 + keepLines; i++)
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

    /// <summary>
    /// Direct edit collection
    /// Na rozdíl od metody RemoveStringsEmpty i vytrimuje (ale pouze pro porovnání, v kolekci nechá)
    /// </summary>
    /// <param name="mySites"></param>
    /// <returns></returns>
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

    public static bool IsSomethingTheSame(string ext, IEnumerable<string> p1)
    {
        foreach (var item in p1)
        {
            if (item == ext)
            {
                return true;
            }
        }

        return false;
    }

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


    /// <summary>
    /// Change elements count in collection to A2
    /// </summary>
    /// <param name="input"></param>
    /// <param name="requiredLength"></param>
    /// <returns></returns>
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

    /// <summary>
    /// Direct edit input collection
    /// </summary>
    /// <param name="v"></param>
    /// <param name="toReplace"></param>
    /// <returns></returns>
    public static List<string> Prepend(string v, List<string> toReplace)
    {
        for (int i = 0; i < toReplace.Count; i++)
        {
            toReplace[i] = v + toReplace[i];
        }
        return toReplace;
    }

    /// <summary>
    /// Direct edit input collection
    /// </summary>
    /// <param name="v"></param>
    /// <param name="toReplace"></param>
    /// <returns></returns>
    public static string[] Prepend(string v, string[] toReplace)
    {
        return Prepend(v, toReplace.ToList()).ToArray();
    }

    public static List<string> Format(string uninstallNpmPackageGlobal, List<string> globallyInstalledTsDefinitions)
    {
        for (int i = 0; i < globallyInstalledTsDefinitions.Count(); i++)
        {
            globallyInstalledTsDefinitions[i] = SH.Format2(uninstallNpmPackageGlobal, globallyInstalledTsDefinitions[i]);
        }
        return globallyInstalledTsDefinitions;
    }

    


}