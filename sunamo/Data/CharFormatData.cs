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
    /// Null = no matter
    /// Nejvhodnější je zde výčet Windows.UI.Text.LetterCase
    /// </summary>
    public bool? upper = false;
    /// <summary>
    /// Nemusí mít žádný prvek, pak může být znak libovolný
    /// </summary>
    public char[] mustBe = null;

    public static class Templates
    {
        public static CharFormatData dash = CharFormatData.Get(null, new FromTo(1, 1), AllChars.dash);
        public static CharFormatData notNumber = CharFormatData.Get(null, new FromTo(1, 1), AllChars.notNumber);

        /// <summary>
        /// When doesn't contains fixed, is from 0 to number
        /// </summary>
        public static CharFormatData twoLetterNumber;

        static Templates()
        {
            FromTo requiredLength = new FromTo(1, 2);
            twoLetterNumber = CharFormatData.GetOnlyNumbers(requiredLength);
            Any = CharFormatData.Get(null, new FromTo(0, int.MaxValue));
        }

        public static CharFormatData Any;
    }


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

    /// <summary>
    /// A1 Null = no matter
    /// 
    /// </summary>
    /// <param name="upper"></param>
    /// <param name="fromTo"></param>
    /// <param name="mustBe"></param>
    public static CharFormatData Get(bool? upper, FromTo fromTo, params char[] mustBe)
    {
        CharFormatData data = new CharFormatData(upper, mustBe);
        data.fromTo = fromTo;
        return data;
    }
}