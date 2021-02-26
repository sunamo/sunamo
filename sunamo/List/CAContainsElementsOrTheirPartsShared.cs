using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static partial class CA
{
    #region 1) ContainsAnyFromElement
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
    #endregion

    #region 2) IsEqualToAnyElement
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

    #region 7) IndexesWithValue
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
        var result = videoCodes.Select((r, index) => new { dx = index, value = r }).Where(d => EqualityComparer<T>.Default.Equals(d.value, empty)).Select(d => d.dx).ToList();
        return result;
    }
    #endregion

    #region 8) ReturnWhichContainsIndexes
    public static List<int> ReturnWhichContainsIndexes(string item, IEnumerable<string> terms, SearchStrategy searchStrategy = SearchStrategy.FixedSpace)
    {
        List<int> result = new List<int>();
        int i = 0;
        foreach (var term in terms)
        {
            if (SH.Contains(item, term, searchStrategy))
            {
                result.Add(i);
            }
            i++;
        }
        return result;
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
    #endregion
}
