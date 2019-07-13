using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public partial class DictionaryHelper{
    static Type type = typeof(DictionaryHelper);

    public static Value GetFirstItem<Value>(Dictionary<string, Value> dict)
    {
        foreach (var item in dict)
        {
            return item.Value;
        }

        return default(Value);
    }

public static short AddToIndexAndReturnIncrementedShort<T>(short i, Dictionary<short, T> colors, T colorOnWeb)
    {
        colors.Add(i, colorOnWeb);
        i++;
        return i;
    }

public static Dictionary<Key, Value> GetDictionary<Key, Value>(List<Key> keys, List<Value> values)
    {
        ThrowExceptions.DifferentCountInLists(type, "GetDictionary", "keys", keys.Count, "values", values.Count);
        Dictionary<Key, Value> result = new Dictionary<Key, Value>();
        for (int i = 0; i < keys.Count; i++)
        {
            result.Add(keys[i], values[i]);
        }

        return result;
    }
}