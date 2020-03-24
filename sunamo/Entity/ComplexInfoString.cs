/// <summary>
/// 
/// </summary>


using System.Collections.Generic;
public class ComplexInfoString
{
    private int _quantityNumbers = 0;
    private int _quantityUpperChars = 0;
    private int _quantityLowerChars = 0;
    private int _quantitySpecialChars = 0;
    private Dictionary<char, int> _znakyPocty = new Dictionary<char, int>();

    public int this[char ch]
    {
        get
        {
            if (_znakyPocty.ContainsKey(ch))
            {
                return _znakyPocty[ch];
            }
            return 0;
        }
    }

    public int QuantityNumbers
    {
        get
        {
            return _quantityNumbers;
        }
    }

    public int QuantityLowerChars
    {
        get
        {
            return _quantityLowerChars;
        }
    }

    public int QuantitySpecialChars
    {
        get
        {
            return _quantitySpecialChars;
        }
    }

    public int QuantityUpperChars
    {
        get
        {
            return _quantityUpperChars;
        }
    }

    public ComplexInfoString(string s)
    {
        foreach (char item in s)
        {
            int nt = (int)item;
            if (AllChars.lowerKeyCodes.Contains(nt))
            {
                _quantityLowerChars++;
            }
            else if (AllChars.upperKeyCodes.Contains(nt))
            {
                _quantityUpperChars++;
            }
            else if (AllChars.numericKeyCodes.Contains(nt))
            {
                _quantityNumbers++;
            }
            else if (AllChars.specialKeyCodes.Contains(nt))
            {
                _quantitySpecialChars++;
            }

            if (_znakyPocty.ContainsKey(item))
            {
                _znakyPocty[item]++;
            }
            else
            {
                _znakyPocty.Add(item, 1);
            }
        }
    }
}