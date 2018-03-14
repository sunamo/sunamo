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

    public object[] OnlyBs()
    {
        object[] o = new object[this.Count];
        for (int i = 0; i < this.Count; i++)
        {
            o[i] = this[i].B;
        }
        return o;
    }

    
}
