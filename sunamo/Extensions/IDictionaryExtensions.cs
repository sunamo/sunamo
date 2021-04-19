using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static class IDictionaryExtensions
{
    public static void AddIfNotExists<T, U>(this IDictionary<T,U> d, T t, U u)
    {
        if (!d.ContainsKey(t))
        {
            d.Add(t, u);
        }
    }
}