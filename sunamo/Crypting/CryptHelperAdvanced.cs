using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class CryptHelperAdvanced
{
    #region Metody z Encryption
    /// <summary>
    /// Přeskupí znaky v A1 podle A2 a G
    /// A2 i G mají vždy přesně 25 znaků
    /// </summary>
    /// <param name="st"></param>
    /// <param name="MoveBase"></param>
    
    private static char ChangeChar(char ch, int[] EnCode)
    {
        ch = char.ToUpper(ch);
        if (ch >= 'A' && ch <= 'H')
            return Convert.ToChar(Convert.ToInt16(ch) + 2 * EnCode[0]);
        else if (ch >= 'I' && ch <= 'P')
            return Convert.ToChar(Convert.ToInt16(ch) - EnCode[2]);
        else if (ch >= 'Q' && ch <= 'Z')
            return Convert.ToChar(Convert.ToInt16(ch) - EnCode[1]);
        else if (ch >= '0' && ch <= '4')
            return Convert.ToChar(Convert.ToInt16(ch) + 5);
        else if (ch >= '5' && ch <= '9')
            return Convert.ToChar(Convert.ToInt16(ch) - 5);
        else
            return '0';
    }
    #endregion
}

