using sunamo.Collections;
using sunamo.Data;
using sunamo.Helpers.Number;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
public static partial class CA
{
   

    /// <summary>
    /// jagged = zubaty
    /// Change from array where every element have two spec of location to ordinary array with inner array
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="value"></param>
    public static T[][] ToJagged<T>( T[,] value)
    {
        if (Object.ReferenceEquals(null, value))
            return null;
        // Jagged array creation
        T[][] result = new T[value.GetLength(0)][];
        for (int i = 0; i < value.GetLength(0); ++i)
            result[i] = new T[value.GetLength(1)];
        // Jagged array filling
        for (int i = 0; i < value.GetLength(0); ++i)
            for (int j = 0; j < value.GetLength(1); ++j)
                result[i][j] = value[i, j];
        return result;
    }
    // In order to convert any 2d array to jagged one
    // let's use a generic implementation
    public static List<List<int>> ToJagged( bool[,] value)
    {
        List<List<int>> result = new List<List<int>>();
        for (int i = 0; i < value.GetLength(0); i++)
        {
            List<int> ca = new List<int>();
            for (int y = 0; y < value.GetLength(1); y++)
            {
                ca.Add(BTS.BoolToInt( value[i, y]));
            }
            result.Add(ca);
        }
        return result;
    }
    public static void RemoveLines(List<string> lines, List<int> removeLines)
    {
        removeLines.Sort();
        for (int i = removeLines.Count - 1; i >= 0; i--)
        {
            var dx = removeLines[i];
            lines.RemoveAt(dx);
        }
    }
    /// <summary>
    /// A1 are column names for ValuesTableGrid (not letter sorted a,b,.. but left column (Name, Rating, etc.)
    /// A2 are data
    /// </summary>
    /// <param name="captions"></param>
    /// <param name="exists"></param>
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
        //////DebugLogger.Instance.WriteLine(vr);
        return vr;
    }
    /// <summary>
    /// Direct edit
    /// </summary>
    /// <param name="input"></param>
    public static string GetNumberedList(List<string> input, int startFrom)
    {
        CA.RemoveStringsEmpty2(input);
        CA.PrependWithNumbered(input, startFrom);
        return SH.JoinNL(input);
    }
    /// <summary>
    /// Direct edit
    /// </summary>
    /// <param name="input"></param>
    private static void PrependWithNumbered(List<string> input, int startFrom)
    {
        var numbered = BTS.GetNumberedListFromTo(startFrom, input.Count - 1, ") ");
        Prepend(numbered, input);
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
    /// Get every duplicated item once
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="clipboardL"></param>
    /// <returns></returns>
    public static List<T> GetDuplicities<T>(List<T> clipboardL)
    {
        List<T> alreadyProcessed;
        return GetDuplicities<T>(clipboardL, out alreadyProcessed);
    }

    /// <summary>
    /// Get every item once
    /// A2 = more duplicities = more items
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="clipboardL"></param>
    /// <param name="alreadyProcessed"></param>
    /// <returns></returns>
        public static List<T> GetDuplicities<T>(List<T> clipboardL, out List<T> alreadyProcessed)
    {
        alreadyProcessed = new List<T>(clipboardL.Count);
        CollectionWithoutDuplicates<T> duplicated = new CollectionWithoutDuplicates<T>();
        foreach (var item in clipboardL)
        {
            if (alreadyProcessed.Contains(item))
            {
                duplicated.Add(item);
            }
            else
            {
                alreadyProcessed.Add(item);
            }
        }
        return duplicated.c;
    }



    /// <summary>
    /// Return whether all of A1 are in A2
    /// Not all from A2 must be A1ContainsAnyFromElement - Contains string elements of list. Return List<string>
    ///IsEqualToAnyElement - same as ContainsElement, only have switched elements. return bool
    ///IsEqualToAllElement - takes two generic list. return bool
    ///ContainsElement - at least one element must be equaled. generic. bool
    ///IsSomethingTheSame - only for string. as List.Contains. bool
    ///IsAllTheSame() - takes element and list.generic. bool
    ///IndexesWithValue() - element and list.generic. return list<int>
    ///ReturnWhichContainsIndexes() - takes two list or element and list. return List<int>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="searchTerms"></param>
    /// <param name="key"></param>
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
    /// A2,3 can be null, then no header will be append
    /// A4 nameOfSolution - main header, also can be null
    /// 
    /// </summary>
    /// <param name="alsoFileNames"></param>
    /// <param name="nameForFirstFolder"></param>
    /// <param name="nameForSecondFolder"></param>
    /// <param name="nameOfSolution"></param>
    /// <param name="files1"></param>
    /// <param name="files2"></param>
    /// <param name="inBoth"></param>
    public static string CompareListResult(bool alsoFileNames, string nameForFirstFolder, string nameForSecondFolder, string nameOfSolution, List<string> files1, List<string> files2, List<string> inBoth)
    {
        int files1Count = files1.Count;
        int files2Count = files2.Count;
        string result;
        TextOutputGenerator textOutput = new TextOutputGenerator();
        int inBothCount = inBoth.Count;
        double sumBothPlusManaged = inBothCount + files2Count;
        PercentCalculator percentCalculator = new PercentCalculator(sumBothPlusManaged);
        if (nameOfSolution != null)
        {
            textOutput.sb.AppendLine(nameOfSolution);
        }
        textOutput.sb.AppendLine("Both (" + inBothCount + AllStrings.swda + percentCalculator.PercentFor(inBothCount, false) + "%):");
        if (alsoFileNames)
        {
            textOutput.List(inBoth);
        }
        if (nameForFirstFolder != null)
        {
            textOutput.sb.AppendLine(nameForFirstFolder + AllStrings.lb + files1Count + AllStrings.swda + percentCalculator.PercentFor(files1Count, true) + "%):");
        }
        if (alsoFileNames)
        {
            textOutput.List(files1);
        }
        if (nameForSecondFolder != null)
        {
            textOutput.sb.AppendLine(nameForSecondFolder + AllStrings.lb + files2Count + AllStrings.swda + percentCalculator.PercentFor(files2Count, true) + "%):");
        }
        if (alsoFileNames)
        {
            textOutput.List(files2);
        }
        textOutput.SingleCharLine(AllChars.asterisk, 10);
        result = textOutput.ToString();
        return result;
    }

    public static void InitFillWith<T>(List<T> arr, int columns)
    {
        for (int i = 0; i < columns; i++)
        {
            arr.Add(default(T));
        }
    }
    public static List<string> PaddingByEmptyString(List<string> list, int columns)
    {
        for (int i = list.Count - 1; i < columns - 1; i++)
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
    /// Direct edit
    /// </summary>
    /// <param name="list"></param>
    public static List<string> OnlyFirstCharUpper(List<string> list)
    {
        return ChangeContent(list, SH.OnlyFirstCharUpper);
    }
    public static bool IsInRange(int od, int to, int index)
    {
        return od >= index && to <= index;
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
    /// <summary>
    /// Return equal ranges of in A1  
    /// </summary>
    /// <param name="contentOneSpace"></param>
    /// <param name="r"></param>
    public static List<FromTo> EqualRanges<T>(List<T> contentOneSpace, List<T> r)
    {
        List<FromTo> result = new List<FromTo>();
        int? dx = null;
        var r_first = r[0];
        int startAt = 0;
        int valueToCompare = 0;
        for (int i = 0; i < contentOneSpace.Count; i++)
        {
            var _contentOneSpace = contentOneSpace[i];
            if (!dx.HasValue)
            {
                if (EqualityComparer<T>.Default.Equals(_contentOneSpace, r_first))
                {
                    dx = i + 1; // +2;
                    startAt = i;
                }
            }
            else
            {
                valueToCompare = dx.Value - startAt;
                if (r.Count > valueToCompare)
                {
                    if (EqualityComparer<T>.Default.Equals(_contentOneSpace, r[valueToCompare]))
                    {
                        dx++;
                    }
                    else
                    {
                        dx = null;
                        i--;
                    }
                }
                else
                {
                    int dx2 = (int)dx;
                    result.Add(new FromTo(dx2 - r.Count + 1, dx2, false));
                    dx = null;
                }
            }
        }
        foreach (var item in result)
        {
            item.from--;
            item.to--;
        }
        return result;
    }
    /// <summary>
    /// Is useful when want to wrap and also join with string. Also last element will have delimiter
    /// </summary>
    /// <param name="list"></param>
    /// <param name="wrapWith"></param>
    /// <param name="delimiter"></param>
    public static List<string> WrapWithAndJoin(IEnumerable<string> list, string wrapWith, string delimiter)
    {
        return list.Select(i => wrapWith + i + wrapWith + delimiter).ToList();
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
    public static List<string> WrapWithIf(Func<string, string, bool, bool> f, bool invert, string mustContains, string wrapWith, params string[] whereIsUsed2)
    {
        for (int i = 0; i < whereIsUsed2.Length; i++)
        {
            if (f.Invoke(whereIsUsed2[i], mustContains, invert))
            {
                whereIsUsed2[i] = wrapWith + whereIsUsed2[i] + wrapWith;
            }
        }
        return whereIsUsed2.ToList();
    }
    /// <summary>
    /// If some of A1 is match with A2
    /// </summary>
    /// <param name="list"></param>
    /// <param name="file"></param>
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
    public static bool HasFirstItemLength(List<string> notContains)
    {
        string t = "";
        if (notContains.Count > 0)
        {
            t = notContains[0].Trim();
        }
        return t.Length > 0;
    }
    
    public static List<string> TrimList(List<string> c)
    {
        for (int i = 0; i < c.Count; i++)
        {
            c[i] = c[i].Trim();
        }
        return c;
    }
    public static string GetTextAfterIfContainsPattern(string input, string ifNotFound, List<string> uriPatterns)
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
    public static List<string> WithoutEndSlash(List<string> folders)
    {
        for (int i = 0; i < folders.Count; i++)
        {
            folders[i] = FS.WithoutEndSlash(folders[i]);
        }
        return folders;
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
        return AnyElementEndsWith(t, v.ToList());
    }
    /// <summary>
    /// Return whether A1 contains with any of A2
    /// </summary>
    /// <param name="t"></param>
    /// <param name="v"></param>
    public static bool AnyElementEndsWith(string t, IEnumerable<string> v)
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
    public static void AddIfNotContains<T>(List<T> founded, T e)
    {
        if (!founded.Contains(e))
        {
            founded.Add(e);
        }
    }
    public static List<string> GetRowOfTable(List<List<string>> _dataBinding, int i2)
    {
        List<string> vr = new List<string>();
        for (int i = 0; i < _dataBinding.Count; i++)
        {
            vr.Add(_dataBinding[i][i2]);
        }
        return vr;
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
    /// Return first A2 elements of A1 or A1 if A2 is bigger
    /// </summary>
    /// <param name="proj"></param>
    /// <param name="p"></param>
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
    /// <param name="ext"></param>
    /// <param name="p1"></param>
    public static bool IsSomethingTheSame(string ext, IEnumerable<string> p1)
    {
        string contained = null;
        return IsSomethingTheSame(ext, p1, ref contained);
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
    /// <param name="ext"></param>
    /// <param name="p1"></param>
    /// <param name="contained"></param>
    public static bool IsSomethingTheSame(string ext, IEnumerable<string> p1, ref string contained)
    {
        foreach (var item in p1)
        {
            if (item == ext)
            {
                contained = item;
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
        ThrowExceptions.Custom(Exc.GetStackTrace(), type, Exc.CallingMethod(),"Invalid row index in method CA.GetRowOfTwoDimensionalArray()" + ";");
        return null;
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
    /// <param name="ext"></param>
    /// <param name="p1"></param>
    /// <returns></returns>
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
        ThrowExceptions.ArgumentOutOfRangeException(Exc.GetStackTrace(), type, Exc.CallingMethod(),"Invalid row index in method CA.GetRowOfTwoDimensionalArray()" + ";");
        return null;
    }
    /// <summary>
    /// Change elements count in collection to A2
    /// </summary>
    /// <param name="input"></param>
    /// <param name="requiredLength"></param>
    public static List<string> ToSize(List<string> input, int requiredLength)
    {
        List<string> returnArray = null;
        int realLength = input.Count;
        if (realLength > requiredLength)
        {
            returnArray = new List<string>( requiredLength);
            CA.InitFillWith(returnArray, requiredLength);
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
            returnArray = new List<string>(requiredLength);
            CA.InitFillWith(returnArray, requiredLength);
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
    public static List<string> Format(string uninstallNpmPackageGlobal, List<string> globallyInstalledTsDefinitions)
    {
        for (int i = 0; i < globallyInstalledTsDefinitions.Count(); i++)
        {
            globallyInstalledTsDefinitions[i] = SH.Format2(uninstallNpmPackageGlobal, globallyInstalledTsDefinitions[i]);
        }
        return globallyInstalledTsDefinitions;
    }

    
}