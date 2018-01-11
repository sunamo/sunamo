using sunamo.Essential;
using System;
using System.Collections.Generic;
using System.Net;

public class ThrowExceptions
{
    public static void NotImplementedCase()
    {
        throw new NotImplementedException("Not implemented case. public program error. Please contact developer.");
    }

    public static void DifferentCountInLists(Type type, string methodName, string namefc, int countfc, string namesc, int countsc)
    {
        if (countfc != countsc)
        {
            throw new Exception(SH.ConcatIfBeforeHasValue( type.FullName, ".", methodName, ":") + " Different count elements in collection " + SH.ConcatIfBeforeHasValue(namefc + " - " + countfc) + " vs. " + SH.ConcatIfBeforeHasValue(namesc + " - " + countsc));
        }
    }

    public static void ArrayElementContainsUnallowedStrings(Type type, string methodName, string arrayName, int dex,  string valueElement, params string[] unallowedStrings)
    {
        List<string> foundedUnallowed = SH.ContainsAny(valueElement, false, unallowedStrings);
        if (foundedUnallowed.Count != 0)
        {
            throw new Exception(SH.ConcatIfBeforeHasValue(methodName, ":") + " Element of " + arrayName + " with value " + valueElement + " contains unallowed string("+ foundedUnallowed.Count +"): " + SH.Join(',', unallowedStrings));
        }
    }

    public static void CheckBackslashEnd(string r)
    {
        if (r.Length != 0)
        {
            if (r[r.Length - 1] != '\\')
            {
                throw new Exception("String has not been in path format!");
            }
        }
    }

    public static void InvalidParameter(Type type, string methodName, string mayUrlDecoded, string typeOfInput)
    {
        if (mayUrlDecoded != WebUtility.UrlDecode(mayUrlDecoded))
        {
            throw new Exception(mayUrlDecoded + " is url endoded " + typeOfInput);
        }
    }
}
