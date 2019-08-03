using System;
using System.Collections.Generic;
using System.Text;


public class TestData
{
    public const string a = "a";
    public const string ab = "ab";
    public const string b = "b";
    public const string c = "c";
    public const string a2 = "a2";

    public static readonly List<string> listAB1;
    public static readonly List<string> listAB2;
    public static readonly List<string> listABC;
    public static readonly List<string> listAC;
    public static readonly List<string> listA;
    public static readonly List<string> listB;
    public static readonly List<string> listC;
    public static readonly List<int> list04;
    public static readonly List<int> list59;

    static TestData()
    {
        listAB1 = new List<string>(CA.ToEnumerable(a, b));
        listAB2 = new List<string>(CA.ToEnumerable(a, b));
        listABC = new List<string>(CA.ToEnumerable(a, b, c));
        listAC = new List<string>(CA.ToEnumerable(a, c));
        listA = new List<string>(CA.ToEnumerable(a));
        listB = new List<string>(CA.ToEnumerable(b));
        listC = new List<string>(CA.ToEnumerable(c));

        list04 = CA.ToList<int>(0, 1, 2, 3, 4);
        list59 = CA.ToList<int>(5, 6, 7, 8, 9);
    }

    public const int one = 1;
    public const int two = 2;
    public const int three = 3;

    public static readonly List<int> list12 = CA.ToInt(CA.ToList<int>(1, 2));
    public static readonly List<int> list1 = CA.ToInt(CA.ToList<int>(1));
    public static readonly List<int> list2 = CA.ToInt(CA.ToList<int>(2));
    public static readonly List<string> list100Items = LinearHelper.GetStringListFromTo(0, 99);
}

