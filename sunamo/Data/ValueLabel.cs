using System;
using System.Collections.Generic;
using System.Linq;

public class ValueLabel
{
    private string _value2 = null;
    private string _label2 = null;

    public string value
    {
        get
        {
            return _value2;
        }
        set
        {
            _value2 = value;
        }
    }

    public string label
    {
        get
        {
            return _label2;
        }
        set
        {
            _label2 = value;
        }
    }

    public ValueLabel(string value, string label)
    {
        _value2 = value;
        _label2 = label;
    }

    /// <summary>
    /// Ginstantion O AB
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    public static ValueLabel Get(string value, string label)
    {
        return new ValueLabel(value, label);
    }
}