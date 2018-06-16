using System;
using System.Collections.Generic;
using System.Text;

namespace sunamo.Constants
{
    public class CodeElementsConstants
    {
        public const string NopeValue = "Nope";
        #region Bool values
        public const string True = "True";
        public const string False = "False";
        public const string @true = "true";
        public const string @false = "false";

        public readonly static List<string> BoolToString = CA.ToList<string>(True, False);
        #endregion

        #region Types
        public const string @string = "string";
        #endregion
    }
}
