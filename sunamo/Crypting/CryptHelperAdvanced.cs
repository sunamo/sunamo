using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class CryptHelperAdvanced
{
    #region Methods from Encryption
    /// <summary>
    /// Přeskupí znaky v A1 podle A2 a G
    /// A2 i G mají vždy přesně 25 znaků
    /// </summary>
    /// <param name="st"></param>
    /// <param name="MoveBase"></param>
    private static string InverseByBase(string st, int MoveBase)
    {
        StringBuilder sb = new StringBuilder();
        int c;
        for (int i = 0; i < st.Length; i += MoveBase)
        {
            if (i + MoveBase > st.Length - 1)
                c = st.Length - i;
            else
                c = MoveBase;
            sb.Append(InverseString(st.Substring(i, c)));
        }
        return sb.ToString();
    }

    /// <summary>
    /// First letter will be latest and viceversa.
    /// </summary>
    /// <param name="st"></param>
    private static string InverseString(string st)
    {
        StringBuilder SB = new StringBuilder();
        for (int i = st.Length - 1; i >= 0; i--)
        {
            SB.Append(st[i]);
        }
        return SB.ToString();
    }

    /// <summary>
    /// Process letters in A1 and if its digit or letter, transform it to short and add as number, otherwise as original char.
    /// </summary>
    /// <param name="st"></param>
    private static string ConvertToLetterDigit(string st)
    {
        StringBuilder SB = new StringBuilder();
        foreach (char ch in st)
        {
            if (char.IsLetterOrDigit(ch) == false)
                SB.Append(Convert.ToInt16(ch).ToString());
            else
                SB.Append(ch);
        }
        return SB.ToString();
    }

    /// <summary>
    /// moving all characters in string insert then into new index
    /// </summary>
    private static string Boring(string st)
    {
        int NewPlace;
        char ch;
        // Procházím všechny znaky v A1
        for (int i = 0; i < st.Length; i++)
        {
            // Skrze operátory % a * určím novou pozici znaku                
            NewPlace = i * Convert.ToUInt16(st[i]);
            // 
            NewPlace = NewPlace % st.Length;
            ch = st[i];
            // Odstraním z A1 řetězec na AI
            st = st.Remove(i, 1);
            // Vložím ho do A1 na nové místo
            st = st.Insert(NewPlace, ch.ToString());
        }
        return st;
    }

    /// <summary>
    /// By letter in A1 this letter convert to int and plus/minus from its 5 or values in A2
    /// </summary>
    /// <param name="ch"></param>
    /// <param name="EnCode"></param>
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