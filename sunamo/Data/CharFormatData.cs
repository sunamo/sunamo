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
    /// If string needn't have any content, can be zero sized
    /// Changed to list because in AllChars is collection in this way
    /// </summary>
        public List<char> mustBe = null;
    public FromTo requiredLength = null;

    // TODO: Only for backward compatibility, can be delete
    public CharFormatData(bool? upper, params char[] mustBe) : this(upper, null, mustBe)
    {

    }

    public CharFormatData(bool? upper, FromTo requiredLength, params char[] mustBe) : this(upper, requiredLength, CA.ToList<char>(mustBe))
    {
        
    }

    public CharFormatData(bool? upper, FromTo requiredLength, List<char> mustBe)
    {
        this.upper = upper;
        this.mustBe = mustBe;
        this.requiredLength = requiredLength;
    }

    public static CharFormatData Get(bool? upper, FromTo requiredLength, List<char> mustBe)
    {
        return Get(upper, requiredLength, mustBe.ToArray());
    }

    public static CharFormatData Get(bool? upper, FromTo requiredLength, params char[] mustBe)
    {
        return new CharFormatData(upper, requiredLength, mustBe);
    }

    public static CharFormatData GetOnlyNumbers(FromTo requiredLength)
    {
        return Get(null, requiredLength, AllChars.numericChars);
    }
    }

