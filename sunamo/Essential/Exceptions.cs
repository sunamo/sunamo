using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace sunamo.Essential
{
    public class Exceptions
    {
        

        #region Without parameters
        public static string NotImplementedCase()
        {
            return "Not implemented case. public program error. Please contact developer.";
        }
        #endregion

        #region Without locating executing code
        public static string CheckBackslashEnd(string before, string r)
        {
            if (r.Length != 0)
            {
                if (r[r.Length - 1] != AllChars.bs)
                {
                    return "String has not been in path format!";
                }
            }

            return null;
        }
        #endregion

        public static string DifferentCountInLists(string before, string namefc, int countfc, string namesc, int countsc)
        {
            if (countfc != countsc)
            {
                return " Different count elements in collection " + SH.ConcatIfBeforeHasValue(namefc + " - " + countfc) + " vs. " + SH.ConcatIfBeforeHasValue(namesc + " - " + countsc);
            }

            return null;
        }

        public static string ArrayElementContainsUnallowedStrings(string before, string arrayName, int dex, string valueElement, params string[] unallowedStrings)
        {
            List<string> foundedUnallowed = SH.ContainsAny(valueElement, false, unallowedStrings);
            if (foundedUnallowed.Count != 0)
            {
                return  " Element of " + arrayName + " with value " + valueElement + " contains unallowed string(" + foundedUnallowed.Count + "): " + SH.Join(',', unallowedStrings);
            }

            return null;
        }

        public static string InvalidParameter(string before, string mayUrlDecoded, string typeOfInput)
        {
            if (mayUrlDecoded != WebUtility.UrlDecode(mayUrlDecoded))
            {
                return mayUrlDecoded + " is url endoded " + typeOfInput;
            }

            return null;
        }

        public static string ElementCantBeFound(string before, string nameCollection, string element)
        {
            return element + "cannot be found in " + nameCollection;
        }
    }
}
