using sunamo.Data;
using sunamo.Values;
using System;
/// <summary>
/// Number Helper Class
/// </summary>
using System.Collections.Generic;
using System.Linq;
using System.Text;

public static partial class NH
{
    private static Type s_type = typeof(NH);




    /// <summary>
    /// Vytvoří interval od A1 do A2 včetně
    /// </summary>
    /// <param name="od"></param>
    /// <param name="to"></param>
    
    private static object ReturnZero<T>()
    {
        var t = typeof(T);
        if (t == Types.tDouble)
        {
            return Consts.zeroDouble;
        }
        else if (t == Types.tInt)
        {
            return Consts.zeroInt;
        }
        else if (t== Types.tFloat)
        {
            return Consts.zeroFloat;
        }
        ThrowExceptions.NotImplementedCase(s_type, "ReturnZero", t.FullName);
        return null;
    }

public static double Sum(List<string> list)
    {
        double result = 0;
        foreach (var item in list)
        {
            var d = double.Parse(item);
            result += d;
        }
        return result;
    }
public static T Sum<T>(List<T> list)
    {
        dynamic sum = 0;
        foreach (var item in list)
        {
            sum += item;
        }
        return sum;
    }
}