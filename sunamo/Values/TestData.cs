using System;
using System.Collections.Generic;
using System.Text;


    public class TestData
    {
    const string a = "a";
    const string b = "b";
    const string c = "c";
    public static readonly List<string> listAB1;
    public static readonly List<string> listAB2;
    public static readonly List<string> listA;
    public static readonly List<string> listB;
    public static readonly List<string> listC;

    static TestData()
    {
        listAB1 = new List<string>(CA.ToEnumerable(a,b));
        listAB2 = new List<string>(CA.ToEnumerable(a, b));
        listA = new List<string>(CA.ToEnumerable(a));
        listB = new List<string>(CA.ToEnumerable(b));
        listC = new List<string>(CA.ToEnumerable(c));
    }

    public static readonly List<int> list12 = CA.ToInt(CA.ToList<int>(1, 2));
    public static readonly List<int> list1 = CA.ToInt(CA.ToList<int>(1));
    public static readonly List<int> list2 = CA.ToInt(CA.ToList<int>(2));

}

