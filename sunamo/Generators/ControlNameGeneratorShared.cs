using System;
using System.Collections.Generic;

public static partial class ControlNameGenerator{
    private static Dictionary<Type, uint> s_actual = new Dictionary<Type, uint>();

    public static string GetSeries(Type t)
    {
        if (s_actual.ContainsKey(t))
        {
            return t.Name + (++s_actual[t]).ToString();
        }

        s_actual.Add(t, 0);
        var r= t.Name + "0";
        return r;
    }
}