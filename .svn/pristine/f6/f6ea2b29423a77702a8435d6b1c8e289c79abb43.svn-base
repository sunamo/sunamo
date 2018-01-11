using System;
using System.Collections.Generic;

public static class ControlNameGenerator
{
    static Dictionary<Type, uint> actual = new Dictionary<Type, uint>();

    public static string GetSeries(Type t)
    {
        if (actual.ContainsKey(t))
        {
            return t.Name + (++actual[t]).ToString();
        }

        actual.Add(t, 0);
        return t.Name + "0";
    }
}
