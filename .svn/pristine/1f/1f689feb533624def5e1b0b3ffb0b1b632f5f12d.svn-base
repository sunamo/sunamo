/// <summary>
/// 
/// </summary>
using System.Collections.Generic;
public class ComplexInfoString
{
    int quantityNumbers = 0;
    int quantityUpperChars = 0;
    int quantityLowerChars = 0;
    int quantitySpecialChars = 0;
    Dictionary<char, int> znakyPocty = new Dictionary<char, int>();
    public int this[char ch]
    {
        get
        {
            if (znakyPocty.ContainsKey(ch))
            {
                return znakyPocty[ch];
            }
            return 0;
        }
    }

    public int QuantityNumbers
    {
        get
        {
            return quantityNumbers;
        }
    }

    public int QuantityLowerChars
    {
        get
        {
            return quantityLowerChars;
        }
    }

    public int QuantitySpecialChars
    {
        get
        {
            return quantitySpecialChars;
        }
    }

    public int QuantityUpperChars
    {
        get
        {
            return quantityUpperChars;
        }
    }

    public ComplexInfoString(string s)
    {
        foreach (char item in s)
        {
            int nt = (int)item;
            if (AllChars.lowerKeyCodes.Contains(nt))
            {
                quantityLowerChars++;
            }
            else if (AllChars.upperKeyCodes.Contains(nt))
            {
                quantityUpperChars++;
            }
            else if (AllChars.numericKeyCodes.Contains(nt))
            {
                quantityNumbers++;
            }
            else if (AllChars.specialKeyCodes.Contains(nt))
            {
                quantitySpecialChars++;
            }

            if (znakyPocty.ContainsKey(item))
            {
                znakyPocty[item]++;
            }
            else
            {
                znakyPocty.Add(item, 1);
            }
        }
    }
}
