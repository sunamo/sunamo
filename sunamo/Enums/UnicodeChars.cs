using System;
using System.Collections.Generic;
using System.Text;

namespace sunamo.Enums
{
    public enum UnicodeChars
    {
        #region char.Is*
        Control,
        HighSurrogate,
        Lower,
        LowSurrogate,
        Number,
        Punctaction,
        Separator,
        Surrogate,
        //char.IsSurrogatePair(low, right) - pair is formed by low and high
        //IsSurrogatePair,
        Symbol,
        Upper,
        WhiteSpace,
        #endregion
        Special,
        Generic
    }
}