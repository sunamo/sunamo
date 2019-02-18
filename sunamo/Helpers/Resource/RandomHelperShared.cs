using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

public static partial class RandomHelper
{
    static Random rnd = new Random();

    public static T RandomElementOfCollectionT<T>(IList<T> ppk)
    {
        int nt = RandomInt(ppk.Count);
        return ppk[nt];
    }

    /// <summary>
    /// Vr�t� ��slo mezi 0 a A1-1
    /// </summary>
    /// <param name="to"></param>
    /// <returns></returns>
    public static int RandomInt(int to)
    {

        return rnd.Next(0, to);
    }

    public static T RandomElementOfCollectionT<T>(IEnumerable<T> ppk)
    {
        List<T> col = new List<T>();
        foreach (var item in ppk)
        {
            col.Add(item);
        }

        return RandomElementOfCollectionT<T>(col);
    }

    public static string RandomElementOfCollection(IList ppk)
    {
        int nt = RandomInt(ppk.Count);
        return ppk[nt].ToString();
    }

/// <summary>
    /// Vrac� ��slo od A1 do A2-1
    /// </summary>
    /// <param name="od"></param>
    /// <param name="to"></param>
    /// <returns></returns>
    public static int RandomInt2(int od, int to)
    {
        return rnd.Next(od, to);
    }

public static string RandomElementOfArray(Array ppk)
    {
        int nt = RandomInt(ppk.Length);
        return ppk.GetValue(nt).ToString();
    }

public static int RandomInt()
    {
        return rnd.Next(0, int.MaxValue);
    }
/// <summary>
    /// Vr�t� ��slo mezi A1 a A2 v�etn�
    /// </summary>
    /// <param name="od"></param>
    /// <param name="to"></param>
    /// <returns></returns>
    public static int RandomInt(int od, int to)
    {

        return rnd.Next(od, to+1);
    }
}