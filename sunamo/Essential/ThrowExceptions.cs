using sunamo.Essential;
using System;
using System.Collections.Generic;
using System.Net;
using System.Reflection;

public class ThrowExceptions
{
    #region Helpers
    /// <summary>
    /// First can be Method base, then A2 can be anything
    /// </summary>
    /// <param name="type"></param>
    /// <param name="methodName"></param>
    /// <returns></returns>
    public static string FullNameOfExecutedCode(object type, string methodName)
    {
        string typeFullName = string.Empty;
        if (type is Type)
        {
            typeFullName = ((Type)type).FullName;
        }
        if (type is MethodBase)
        {
            MethodBase method = (MethodBase)type;
            typeFullName = method.ReflectedType.FullName;
            methodName = method.Name;
        }
        else if (type is string)
        {
            typeFullName = type.ToString();
        }
        else
        {
            Type t = type.GetType();
            typeFullName = t.FullName;
        }

        return SH.ConcatIfBeforeHasValue(typeFullName, ".", methodName);
    }
    #endregion

    #region Helper
    public static void ThrowIsNotNull(object type, string methodName, string exception)
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
    public static void NotImplementedCase(object type, string methodName)
    {
        ThrowIsNotNull(Exceptions.NotImplementedCase(FullNameOfExecutedCode( type, methodName)));
    }
    #endregion

    #region Without locating executing code
    public static void CheckBackslashEnd(string r)
    {
        ThrowIsNotNull(Exceptions.CheckBackslashEnd("", r));
    }
    #endregion

    public static void DifferentCountInLists(object type, string methodName, string namefc, int countfc, string namesc, int countsc)
    {
        ThrowIsNotNull(Exceptions.DifferentCountInLists(FullNameOfExecutedCode(type, methodName), namefc, countfc, namesc, countsc));
    }

    public static void ArrayElementContainsUnallowedStrings(object type, string methodName, string arrayName, int dex,  string valueElement, params string[] unallowedStrings)
    {
        ThrowIsNotNull(Exceptions.ArrayElementContainsUnallowedStrings(FullNameOfExecutedCode(type, methodName), arrayName, dex, valueElement, unallowedStrings));
    }

    public static void InvalidParameter(object type, string methodName, string mayUrlDecoded, string typeOfInput)
    {
        ThrowIsNotNull(Exceptions.InvalidParameter(FullNameOfExecutedCode(type, methodName), mayUrlDecoded, typeOfInput));
    }

    public static void ElementCantBeFound(object type, string methodName, string nameCollection, string element)
    {
        ThrowIsNotNull(Exceptions.ElementCantBeFound(FullNameOfExecutedCode(type, methodName), nameCollection, element));
    }
}
