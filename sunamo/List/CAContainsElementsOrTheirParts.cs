using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// Keep in separatly file, I have still chaos in it
/// Only 1:M, therefore not CompareListResult
/// 1) CA.ContainsAnyFromElement - Contains string elements of list. Return List<string>
/// 2) CA.IsEqualToAnyElement - same as ContainsElement, only have switched elements. return bool
/// 3) CA.IsEqualToAllElement - takes two generic list. return bool
/// 4) CA.ContainsElement - at least one element must be equaled. generic. bool
/// 5) CA.IsSomethingTheSame - only for string. as List.Contains. bool
/// 6) CA.IsAllTheSame() - takes element and list.generic. bool
/// 7) CA.IndexesWithValue() - element and list.generic. return list<int>
/// 8) CA.ReturnWhichContainsIndexes() - takes two list or element and list. return List<int>
/// 9) ReturnWhichAreEqualIndexes
/// 10) IndexOfValue
/// </summary>
public static partial class CA
{
    #region CAContainsElementsOrTheitParts
    #region 3) IsEqualToAllElement
    /// <summary>
    /// Return whether all of A1 are in A2
    /// Not all from A2 must be A1ContainsAnyFromElement - Contains string elements of list. Return List<string>
    /// ) CA.ContainsAnyFromElement - Contains string elements of list. Return List<string>
    /// ) CA.IsEqualToAnyElement - same as ContainsElement, only have switched elements. return bool
    /// ) CA.IsEqualToAllElement - takes two generic list. return bool
    /// ) CA.ContainsElement - at least one element must be equaled. generic. bool
    /// ) CA.IsSomethingTheSame - only for string. as List.Contains. bool
    /// ) CA.IsAllTheSame() - takes element and list.generic. bool
    /// ) CA.IndexesWithValue() - element and list.generic. return list<int>
    /// ) CA.ReturnWhichContainsIndexes() - takes two list or element and list. return List<int>
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
    #endregion

    #region 5) IsSomethingTheSame
    /// <summary>
    /// 1) CA.ContainsAnyFromElement - Contains string elements of list. Return List<string>
    /// 2) CA.IsEqualToAnyElement - same as ContainsElement, only have switched elements. return bool
    /// 3) CA.IsEqualToAllElement - takes two generic list. return bool
    /// 4) CA.ContainsElement - at least one element must be equaled. generic. bool
    /// 5) CA.IsSomethingTheSame - only for string. as List.Contains. bool
    /// 6) CA.IsAllTheSame() - takes element and list.generic. bool
    /// 7) CA.IndexesWithValue() - element and list.generic. return list<int>
    /// 8) CA.ReturnWhichContainsIndexes() - takes two list or element and list. return List<int>
    /// </summary>
    /// <param name="ext"></param>
    /// <param name="p1"></param>
    public static bool IsSomethingTheSame(string ext, IEnumerable<string> p1)
    {
        string contained = null;
        return IsSomethingTheSame(ext, p1, ref contained);
    }
    /// <summary>
    /// 1) CA.ContainsAnyFromElement - Contains string elements of list. Return List<string>
    /// 2) CA.IsEqualToAnyElement - same as ContainsElement, only have switched elements. return bool
    /// 3) CA.IsEqualToAllElement - takes two generic list. return bool
    /// 4) CA.ContainsElement - at least one element must be equaled. generic. bool
    /// 5) CA.IsSomethingTheSame - only for string. as List.Contains. bool
    /// 6) CA.IsAllTheSame() - takes element and list.generic. bool
    /// 7) CA.IndexesWithValue() - element and list.generic. return list<int>
    /// 8) CA.ReturnWhichContainsIndexes() - takes two list or element and list. return List<int>
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
    #endregion

    #region 6) IsAllTheSame
    /// <summary>
    /// 1) CA.ContainsAnyFromElement - Contains string elements of list. Return List<string>
    /// 2) CA.IsEqualToAnyElement - same as ContainsElement, only have switched elements. return bool
    /// 3) CA.IsEqualToAllElement - takes two generic list. return bool
    /// 4) CA.ContainsElement - at least one element must be equaled. generic. bool
    /// 5) CA.IsSomethingTheSame - only for string. as List.Contains. bool
    /// 6) CA.IsAllTheSame() - takes element and list.generic. bool
    /// 7) CA.IndexesWithValue() - element and list.generic. return list<int>
    /// 8) CA.ReturnWhichContainsIndexes() - takes two list or element and list. return List<int>
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
    #endregion

    #region 9) ReturnWhichAreEqualIndexes
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

    private static IList<int> ReturnWhichAreEqualIndexes<T>(IEnumerable<T> parts, IEnumerable<T> mustBeEqual)
    {
        CollectionWithoutDuplicates<int> result = new CollectionWithoutDuplicates<int>();
        foreach (var item in mustBeEqual)
        {
            result.AddRange(ReturnWhichAreEqualIndexes<T>(parts, item));
        }
        return result.c;
    }

    public static void Remove(List<string> l, List<string> ig)
    {
        int dx = -1;

        foreach (var item in ig)
        {
            dx = l.IndexOf(item);

            if (dx != -1)
            {
                l.RemoveAt(dx);
            }
        }

        
    }
    #endregion

    #region 10) IndexOfValue
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
    #endregion
    #endregion

    

}