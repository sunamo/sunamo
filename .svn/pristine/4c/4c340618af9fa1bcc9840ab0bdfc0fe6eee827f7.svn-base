using System;
using System.Collections.Generic;
using System.Linq;

    public class ValueLabel
    {
        string value2 = null;
    string label2 = null;

    public string value
    {
        get
        {
            return value2;
        }
        set
        {
            value2 = value;
        }
    }

    public string label
    {
        get
        {
            return label2;
        }
        set
        {
            label2 = value;
        }
    }

    public ValueLabel(string value, string label)
    {
        value2 = value;
        this.label2 = label;
    }

    /// <summary>
    /// Ginstantion O AB
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <returns></returns>
    public static ValueLabel Get(string value, string label)
    {
        return new ValueLabel(value, label);
    }
    }
