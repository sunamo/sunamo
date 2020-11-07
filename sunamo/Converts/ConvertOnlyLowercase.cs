using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class ConvertOnlyLowercase
{
    /// <summary>
    /// % - HTTP Error 400. The request URL is invalid.
    /// * - potentionally dangerous
    /// </summary>
    const char nextUpper = '$';

    public static string To(string s)
    {
        StringBuilder sb = new StringBuilder();
        foreach (var item in s)
        {
            if (char.IsUpper(item))
            {
                sb.Append(nextUpper);
                sb.Append(char.ToLower(item));
            }
            else
            {
                sb.Append(item);
            }
        }

        return sb.ToString();
    }

    public static string From(string s)
    {
        bool b = false;
        StringBuilder sb = new StringBuilder();
        foreach (var item in s)
        {
            if (b)
            {
                b = false;
                sb.Append(char.ToUpper(item));
                continue;
            }

            if (item == nextUpper)
            {
                b = true;
            }
            else
            {
                sb.Append(item);
            }
        }

        return sb.ToString();
    }

    /// pre processed conversions for letters
    private static Dictionary<char, char> Convert;

    public static string encode(string stringToEncode)
    {

        // approach: pre process a mapping (dictionary) for letter conversions
        // use a Dict for fastest look ups.  The first run, will take a little
        // extra time, subsequent usage will perform even better
        if (Convert == null || Convert.Count == 0) BuildConversionMappings();

        // our return val (efficient Appends)
        StringBuilder sb = new StringBuilder();

        // used for reversing the numbers
        Stack<char> nums = new Stack<char>();

        // iterate the input string
        for (int i = 0; i < stringToEncode.Length; i++)
        {

            char c = stringToEncode[i];

            // we have 3 cases:
            // 1) is alpha ==> convert using mapping
            // 2) is number ==> peek ahead to complete the number
            // 3) is special char / punctunation ==> ignore

            if (Convert.ContainsKey(c))
            {
                sb.Append(Convert[c]);
                continue;
            }

            if (Char.IsDigit(c))
            {
                nums.Push(c);

                // we've reached the end of the input string OR
                // we've reached the end of the number
                if (i == stringToEncode.Length - 1
                   || !Char.IsDigit(stringToEncode[i + 1]))
                {
                    while (nums.Count > 0)
                    {
                        sb.Append(nums.Pop());
                    }
                }

                continue;
            }

            // not letter, not digit
            sb.Append(c);
        }
        return sb.ToString();
    }

    // create our mappings for letters
    private static void BuildConversionMappings()
    {

        Convert = new Dictionary<char, char>();

        // only loop once for both
        for (char c = 'B'; c <= 'Z'; c++)
        {
            // add capitals version
            char val = (char)(c - 1);
            val = Char.ToLower(val);
            Convert.Add(c, val);
            // add lower case version
            Convert.Add(Char.ToLower(c), val);
        }

        // special cases
        Convert['y'] = ' ';
        Convert['Y'] = ' ';
        Convert.Add(' ', 'y');

        // vowels
        char[] vowels = new char[] { 'a', 'e', 'i', 'o', 'u' };
        for (int i = 0; i < vowels.Length; i++)
        {
            var letter = vowels[i];
            var value = (i + 1).ToString()[0];
            Convert[letter] = value;
            Convert[Char.ToUpper(letter)] = value;
        }
    }
}