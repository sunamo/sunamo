using sunamo.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static partial class EnumHelper
{
    public static string EnumToString<T>(T ds) where T : Enum
    {
        const string comma = AllStrings.comma;
        StringBuilder sb = new StringBuilder();
        var v = Enum.GetValues(typeof(T));
        foreach (T item in v)
        {
            if (ds.HasFlag(item))
            {
                var ts = item.ToString();
                if (ts != Consts.Nope)
                {
                    sb.Append(ts + comma);
                }
            }
        }
        return sb.ToString().TrimEnd(comma[0]);
    }

    public static List<string> GetNames(Type type)
    {
        return Enum.GetNames(type).ToList();
    }

    /// <summary>
    /// Get values include zero and All
    /// Pokud bude A1 null nebo nebude obsahovat žádný element T, vrátí A1
    /// Pokud nebude obsahovat všechny, vrátí jen některé - nutno kontrolovat počet výstupních elementů pole
    /// Pokud bude prvek duplikován, zařadí se jen jednou
    /// </summary>
    /// <typeparam name = "T"></typeparam>
    /// <param name = "v"></param>
    
    public static T Parse<T>(string web, T _def)
        where T : struct
    {
        T result;
        if (Enum.TryParse<T>(web, true, out result))
        {
            return result;
        }

        return _def;
    }

    /// <summary>
    /// Tested with EnumA
    /// </summary>
    /// <typeparam name = "T"></typeparam>
    /// <param name = "result"></param>
    private static void CheckForZero<T>(List<T> result)
        where T : struct
    {
        try
        {
            // Here I get None
            var val = Enum.GetName(typeof(T), (T)(object)0);
            if (string.IsNullOrEmpty(val))
            {
                result.Remove((T)(object)0);
            }
        }
        catch
        {
            result.Remove((T)(object)0);
        }
    }

    /// <summary>
    /// If A1, will start from [1]. Otherwise from [0]
    /// Enem values must be castable to int
    /// Cant be use second generic parameter, due to difficult operations like ~v or |=
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="secondIsAll"></param>
    /// <param name="def"></param>
    /// <param name="valuesInverted"></param>
    /// <param name="result"></param>
    /// <param name="max"></param>
    private static void GetValuesOfEnum<T>(bool secondIsAll, out int def, out int[] valuesInverted, out List<T> result, out int max)
        where T : struct
    {
        def = 0;
        if (secondIsAll)
        {
            def = 1;
        }

        if (typeof(T).BaseType != typeof(Enum))
            throw new ArgumentException(" " + " " + "must be an Enum type");
        var values = Enum.GetValues(typeof(T)).Cast<int>().ToArray();
        valuesInverted = values.Select(v => ~v).ToArray();
        result = new List<T>();
        max = def;
        for (int i = def; i < values.Length; i++)
        {
            max |= values[i];
        }
    }

    private static void GetValuesOfEnumByte<T>(bool secondIsAll, out byte def, out byte[] valuesInverted, out List<T> result, out byte max)
    {
        def = 0;
        if (secondIsAll)
        {
            def = 1;
        }

        if (typeof(T).BaseType != typeof(Enum))
            throw new ArgumentException(" " + " " + "must be an Enum type");
        var values = Enum.GetValues(typeof(T)).Cast<byte>().ToArray();
        valuesInverted = values.Select(v => ~v).Cast<byte>().ToArray();
        result = new List<T>();
        max = def;
        for (int i = def; i < values.Length; i++)
        {
            max |= values[i];
        }
    }
}
