using sunamo.Constants;
using System;
using System.Collections.Generic;
using System.Text;


    public static class TestData
    {
    /// <summary>
    /// a, a
    /// </summary>
    public static readonly List<string> listAB1 = new List<string>(CA.ToEnumerable(a,b));
    /// <summary>
    /// a, b
    /// </summary>
    public static readonly List<string> listAB2 = new List<string>(CA.ToEnumerable(a, b));
    public static readonly List<string> listA = new List<string>(CA.ToEnumerable(a));
    public static readonly List<string> listB = CA.ToListString(b);
    public static readonly List<string> listC = CA.ToListString(c);
    public static readonly List<string> listAC = CA.ToListString(a,c);
    public static readonly List<string> listEmpty = CA.ToListString();

    public static readonly List<int> list1 = CA.ToList<int>(first);
    public static readonly List<int> list12 = CA.ToList<int>(first, second);
    public static readonly List<int> list2 = CA.ToList<int>(second);
    public static readonly List<int> list3 = CA.ToList<int>(third);



    public static Dictionary<string, List<string>> dictionary = new Dictionary<string, List<string>>();
    public static List<string> keysDictionary = new List<string>();

    public const int first = 1;
    public const int second = 2;
    public const int third = 3;


    public const string a = "a";
    public const string b = "b";
    public const string c = "c";

    static TestData()
    {
        keysDictionary.Add(AllStrings._1);
        keysDictionary.Add(AllStrings._2);

        dictionary.Add(AllStrings._1, TestData.listAB1);
        dictionary.Add(AllStrings._2, TestData.listAB2);

    }

    }

