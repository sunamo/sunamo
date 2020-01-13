using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class ABC : List<AB>//, IEnumerable<AB>
{
    public ABC()
    {
    }

    public int Length
    {
        get
        {
            return Count;
        }
    }

    public ABC(int capacity) : base(capacity)
    {

    }

    public ABC(params object[] setsNameValue)
    {
        // Dont use like idiot TwoDimensionParamsIntoOne where is not needed - just iterate. Must more use radio and less blindness
        //var setsNameValue = CA.TwoDimensionParamsIntoOne(setsNameValue2);
        for (int i = 0; i < setsNameValue.Length; i++)
        {
            this.Add(AB.Get(setsNameValue[i].ToString(), setsNameValue[++i]));
        }
    }

    public ABC(params AB[] abc)
    {
        // TODO: Complete member initialization
        this.AddRange(abc);
    }

    /// <summary>
    /// Must be [] due to SQL viz  https://stackoverflow.com/questions/9149919/no-mapping-exists-from-object-type-system-collections-generic-list-when-executin
    /// </summary>
    /// <returns></returns>
    public object[] OnlyBs()
    {
        return OnlyBsList().ToArray();   
    }

    public List<object> OnlyBsList()
    {
        List<object> o = new List<object>(this.Count);
        for (int i = 0; i < this.Count; i++)
        {
            o.Add(this[i].B);
        }
        return o;
    }

    public string[] OnlyAs()
    {
        string[] o = new string[this.Count];
        for (int i = 0; i < this.Count; i++)
        {
            o[i] = this[i].A;
        }
        return o;
    }

    public static IEnumerable OnlyBs(List<AB> arr)
    {
        return arr.Select(d=> d.B);
    }
}
