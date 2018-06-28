using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

    /// <summary>
    /// Udává jak musí být vstupní text zformátovaný
    /// </summary>
    public class CharFormatData
    {
        /// <summary>
        /// Nejvhodnější je zde výčet Windows.UI.Text.LetterCase
        /// </summary>
        public bool? upper = false;
    /// <summary>
    /// Nemusí mít žádný prvek, pak může být znak libovolný
    /// </summary>
        public char[] mustBe = null;

    

    public FromTo fromTo = null;

        public CharFormatData(bool? upper, char[] mustBe)
        {
            this.upper = upper;
            this.mustBe = mustBe;

        
        }

    public CharFormatData()
    {

    }

    public static CharFormatData GetOnlyNumbers(FromTo requiredLength)
    {
        CharFormatData data = new CharFormatData();
        data.fromTo = requiredLength;
        data.mustBe = AllChars.numericChars.ToArray();
        return data;
    }

    public static CharFormatData Get(bool? upper, FromTo fromTo, params char[] mustBe)
    {
        CharFormatData data = new CharFormatData(upper, mustBe);
        data.fromTo = fromTo;
        return data;
    }
}

