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
        for (int i = 0; i < capacity; i++)
        {
            this.Add(null);
        }
    }

    public ABC(params object[] setsNameValue)
    {
        if (setsNameValue.Length == 0)
        {
            return;
        }

        var o = setsNameValue[0];
        var t = o.GetType();
        Type t2 = t;
        if (o is IEnumerable)
        {
            var s = o as IEnumerable;
            var o2 = s.FirstOrNull();
            t2 = o2.GetType();
        }

        //var t2 = setsNameValue[0][0].GetType();
        if ( t2 == typeof(AB))
        {
            //var abc = null;
            //if (true)
            //{

            //}
            for (int i = 0; i < setsNameValue.Length; i++)
            {
                var snv = setsNameValue[i];
                t2 = snv.GetType();
                if (t2 == AB.type)
                {
                    this.Add((AB)snv);
                }
                else
                {
                    var ie = (IEnumerable)snv;
                    foreach (var item in ie)
                    {
                        var ab = (AB)item;
                        Add(ab);
                    }
                }
                
            }
        }
        else if(t == typeof(ABC))
        {
            var abc = (ABC)o;

            this.AddRange(abc);
        }
        else
        {
            // Dont use like idiot TwoDimensionParamsIntoOne where is not needed - just iterate. Must more use radio and less blindness
            //var setsNameValue = CA.TwoDimensionParamsIntoOne(setsNameValue2);
            for (int i = 0; i < setsNameValue.Length; i++)
            {
                this.Add(AB.Get(setsNameValue[i].ToString(), setsNameValue[++i]));
            }
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

    public List<string> OnlyAs()
    {
        List<string> o = new List<string>( this.Count);
        CA.InitFillWith(o, this.Count);
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
