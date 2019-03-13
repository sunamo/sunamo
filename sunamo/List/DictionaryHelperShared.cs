using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public partial class DictionaryHelper{ 
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
}