using System;
using System.Collections.Generic;
public class ABC : List<AB>//, IEnumerable<AB>
{
    public ABC()
    {

    }

    public ABC(params object[] setsNameValue)
    {
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

    public List<object> OnlyBs()
    {
        List<object> o = new List<object>(this.Count);
        for (int i = 0; i < this.Count; i++)
        {
            o.Add( this[i].B);
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
}
