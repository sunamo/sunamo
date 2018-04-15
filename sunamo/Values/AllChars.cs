using System;
using System.Collections.Generic;
using System.Diagnostics;
/// <summary>
/// Je to všechno v jedné třídě, aby se mi změny udělané zde projevili ve všech třídách.
/// </summary>
public static class AllChars
{
    public static readonly List<int> specialKeyCodes = new List<int>(new int[] { 33, 64, 35, 36, 37, 94, 38, 42, 63, 95, 126 });
    public static readonly List<int> numericKeyCodes = new List<int>(new int[] { 49, 50, 51, 52, 53, 54, 55, 56, 57, 48 });
    // , 55296 mi taky vrátila metoda IsWhiteSpace vrátila, ale při znovu vytvoření pomocí tohoto kódu to vyhazovalo výjimku
    public static readonly List<int> whiteSpacesCodes = new List<int>(new int[] { 9, 10, 11, 12, 13, 32, 133, 160, 5760, 6158, 8192, 8193, 8194, 8195, 8196, 8197, 8198, 8199, 8200, 8201, 8202, 8232, 8233, 8239, 8287, 12288 });
    public static readonly List<int> lowerKeyCodes = new List<int>(new int[] { 97, 98, 99, 100, 101, 102, 103, 104, 105, 106, 107, 108, 109, 110, 111, 112, 113, 114, 115, 116, 117, 118, 119, 120, 121, 122 });
    public static readonly List<int> upperKeyCodes = new List<int>(new int[] { 65, 66, 67, 68, 69, 70, 71, 72, 73, 74, 75, 76, 77, 78, 79, 80, 81, 82, 83, 84, 85, 86, 87, 88, 89, 90 });

    public static readonly List<char> specialChars = new List<char>(new char[] { '!', '@', '#', '$', '%', '^', '&', '*', '?', '_', '~' });
    public static readonly List<char> numericChars = new List<char>(new char[] { '1', '2', '3', '4', '5', '6', '7', '8', '9', '0' });
    public static List<char> whiteSpacesChars = null;
    
    public static readonly List<char> lowerChars = new List<char>(new char[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' });
    public static readonly List<char> upperChars = new List<char>(new char[] { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' });

    #region Names here must be the same as in Consts
    public const char space = ' ';
    public const char dash = '-';
    public const char colon = ':';
    public const char us = '_';

    public const char tab = '\t';
    public const char nl = '\n';
    public const char cr = '\t';
    public const char bs = '\\';
    
    public const char comma = ',';
    public const char dot = '.';
    public const char asterisk = '*';
    
    public const char sc = ';';
    public const char qm = '"';
    /// <summary>
    /// Left bracket
    /// </summary>
    public const char lb = '(';
    public const char rb = ')';
    
    #endregion

    static AllChars()
    {
        ConvertWhiteSpaceCodesToChars();
    }

    public static void ConvertWhiteSpaceCodesToChars()
    {
        
        whiteSpacesChars = new List<char>();
        foreach (int item in whiteSpacesCodes)
        {
            string s = "";
            s = char.ConvertFromUtf32(item);
            whiteSpacesChars.Add(Convert.ToChar(s));
        }
        
    }

    
}
