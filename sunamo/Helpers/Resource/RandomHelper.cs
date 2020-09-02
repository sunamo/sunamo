using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
/// <summary>
/// M pro gen. nahodnych ruznych typu dat bez pretypovani
/// </summary>
public static partial class RandomHelper
{
    public static void RemoveChars(List<string> p)
    {
        foreach (string item in p)
        {
            if (p.Count == 1)
            {
                vsZnaky.Remove(item[0]);
            }
        }
    }

    public static IntPtr RandomIntPtr()
    {
        IntPtr p = new IntPtr(RandomInt());
        return p;
    }

    public static List<string> RandomElementsOfCollection(IList sou, int pol)
    {
        List<string> vr = new List<string>();
        for (int i = 0; i < pol; i++)
        {
            vr.Add(RandomElementOfCollection(sou));
        }
        return vr;
    }


 

    
    public static DateTime RandomSmallDateTime(int minDaysAdd, int maxDaysAdd)
    {
        DateTime dt = DateTime.Today;
        int pridat = RandomInt(minDaysAdd, maxDaysAdd);
        dt = dt.AddDays(pridat);
        return dt;
    }

    

    
    

    
}