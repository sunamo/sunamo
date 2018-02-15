using sunamo.Essential;
using System;
using System.Collections.Generic;
using System.Net;

public class ThrowExceptions
{
    #region Helpers
    public static string FullNameOfExecutedCode(Type type, string methodName)
    {
        return SH.ConcatIfBeforeHasValue(type.FullName, ".", methodName, ":");
    }
    #endregion

    #region Helper
    public static void ThrowIsNotNull(Type type, string methodName, string exception)
    {
        if (exception != null)
        {
            throw new Exception(exception);
        }
    }

    public static void ThrowIsNotNull(string exception)
    {
        if (exception != null)
        {
            throw new Exception(exception);
        }
    }
    #endregion

    #region Without parameters
    public static void NotImplementedCase()
    {
        ThrowIsNotNull(Exceptions.NotImplementedCase());
    }
    #endregion

    #region Without locating executing code
    public static void CheckBackslashEnd(string r)
    {
        ThrowIsNotNull(Exceptions.CheckBackslashEnd("", r));
    }
    #endregion

    public static void DifferentCountInLists(Type type, string methodName, string namefc, int countfc, string namesc, int countsc)
    {
        ThrowIsNotNull(Exceptions.DifferentCountInLists(FullNameOfExecutedCode(type, methodName), namefc, countfc, namesc, countsc));
    }

    public static void ArrayElementContainsUnallowedStrings(Type type, string methodName, string arrayName, int dex,  string valueElement, params string[] unallowedStrings)
    {
        ThrowIsNotNull(Exceptions.ArrayElementContainsUnallowedStrings(FullNameOfExecutedCode(type, methodName), arrayName, dex, valueElement, unallowedStrings));
    }

    public static void InvalidParameter(Type type, string methodName, string mayUrlDecoded, string typeOfInput)
    {
        ThrowIsNotNull(Exceptions.InvalidParameter(FullNameOfExecutedCode(type, methodName), mayUrlDecoded, typeOfInput));
    }

    public static void ElementCantBeFound(Type type, string methodName, string nameCollection, string element)
    {
        ThrowIsNotNull(Exceptions.ElementCantBeFound(FullNameOfExecutedCode(type, methodName), nameCollection, element));
    }
}
