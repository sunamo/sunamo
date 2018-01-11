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
        /// <returns></returns>
        static public string InverseByBase(string st, int MoveBase)
        {
            StringBuilder SB = new StringBuilder();
            //st = ConvertToLetterDigit(st);
            int c;
            for (int i = 0; i < st.Length; i += MoveBase)
            {
                if (i + MoveBase > st.Length - 1)
                    c = st.Length - i;
                else
                    c = MoveBase;
                SB.Append(InverseString(st.Substring(i, c)));
            }
            return SB.ToString();
        }

        /// <summary>
        /// Převrátím řetězec - první písmeno bude poslední atd.
        /// </summary>
        /// <param name="st"></param>
        /// <returns></returns>
        static public string InverseString(string st)
        {
            StringBuilder SB = new StringBuilder();
            for (int i = st.Length - 1; i >= 0; i--)
            {
                SB.Append(st[i]);
            }
            return SB.ToString();
        }

        /// <summary>
        /// Procházím znaky v A1 a pokud to je znak nebo číslice, převedu ji na short a přidám jako číslo, jinak jako původní znak.
        /// </summary>
        /// <param name="st"></param>
        /// <returns></returns>
        static public string ConvertToLetterDigit(string st)
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
        /// Přesune všechny znaky ve řetězci vložením jich na nový index
        /// Procházím všechny znaky v A1
        /// Skrze operátory % a * určím novou pozici znaku                
        /// Odstraním z A1 řetězec na AI
        /// Vložím ho do A1 na nové místo
        /// moving all characters in string insert then into new index
        /// </summary>
        /// <param name="st">string to moving characters</param>
        /// <returns>moved characters string</returns>
        static public string Boring(string st)
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
        /// Podle znaku v A1 tento znak převedu na int a přičtu/odečtu z něho 5 nebo hodnoty v A2
        /// </summary>
        /// <param name="ch"></param>
        /// <param name="EnCode"></param>
        /// <returns></returns>
        static public char ChangeChar(char ch, int[] EnCode)
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

