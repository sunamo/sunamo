
using sunamo.Essential;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Reflection;

namespace sunamo.Essential
{ }

public class ThrowExceptions
{
    #region Must be as first - newly created method fall into this

    public static void BadMappedXaml(object type, string methodName, string nameControl, string additionalInfo)
    {
        ThrowIsNotNull(Exceptions.BadMappedXaml(FullNameOfExecutedCode(type, methodName, true), nameControl, additionalInfo));
    }

    public static void FileExists(object type, string methodName, string fulLPath)
    {
        ThrowIsNotNull(Exceptions.FileExists(FullNameOfExecutedCode(type, methodName, true), fulLPath));
    }

    /// <summary>
    /// Return & throw exception whether directory exists
    /// </summary>
    /// <param name="type"></param>
    /// <param name="v"></param>
    /// <param name="photosPath"></param>
    /// <returns></returns>
    public static bool DirectoryExists(Type type, string methodName, string path)
    {
        return ThrowIsNotNull(Exceptions.DirectoryExists(FullNameOfExecutedCode(type, methodName, true), path));
    }

    public static void IsWhitespaceOrNull(Type type, string methodName, string variable, object data)
    {
        ThrowIsNotNull(Exceptions.IsWhitespaceOrNull(FullNameOfExecutedCode(type, methodName, true), variable, data));
    }



    /// <summary>
    /// A1 have to be Dictionary<T,U>, not IDictionary without generic
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="U"></typeparam>
    /// <param name="type"></param>
    /// <param name="v"></param>
    /// <param name="en"></param>
    /// <param name="dictName"></param>
    /// <param name="key"></param>
    public static void KeyNotFound<T,U>(Type type, string v, IDictionary<T,U> en, string dictName, T key)
    {
        ThrowIsNotNull(Exceptions.KeyNotFound( FullNameOfExecutedCode(type, v), en, dictName, key));
    }

    public static void HaveAllInnerSameCount(Type type, string methodName, List<List<string>> elements)
    {
        ThrowIsNotNull(Exceptions.HaveAllInnerSameCount(FullNameOfExecutedCode(type, methodName, true), elements));
    }

    /// <summary>
    /// Must be string due to in sunamo is not NamespaceElement
    /// </summary>
    /// <param name="name"></param>
    public static void NameIsNotSetted(object type, string methodName, string nameControl, string nameFromProperty)
    {
        ThrowIsNotNull(Exceptions.NameIsNotSetted(FullNameOfExecutedCode(type, methodName, true), nameControl, nameFromProperty));
    }

    public static void IsOdd(Type s_type, string methodName, string colName, IEnumerable col)
    {
        ThrowIsNotNull(Exceptions.IsOdd(FullNameOfExecutedCode(s_type, methodName), colName, col));
    }

    public static void DifferentCountInLists(object type, string methodName, string namefc, int countfc, string namesc, int countsc)
    {
        ThrowIsNotNull(Exceptions.DifferentCountInLists(FullNameOfExecutedCode(type, methodName, true), namefc, countfc, namesc, countsc));
    }

    public static void DoesntHaveRequiredType(object type, string methodName, string variableName)
    {
        ThrowIsNotNull(Exceptions.DoesntHaveRequiredType(FullNameOfExecutedCode(type, methodName, true), variableName));
    }

    public static void DifferentCountInLists(Type type, string methodName, string namefc, IEnumerable replaceFrom, string namesc, IEnumerable replaceTo)
    {
        DifferentCountInLists(type, methodName, namefc, replaceFrom.Count(), namesc, replaceTo.Count());
    }

    

    public static void IsNotAllowed(Type type, string methodName, string what)
    {
        ThrowIsNotNull(Exceptions.IsNotAllowed(FullNameOfExecutedCode(type, methodName, true), what));
    }

    public static void MoreThanOneElement(Type type, string methodName, string listName, int count)
    {
        ThrowIsNotNull(Exceptions.MoreThanOneElement(FullNameOfExecutedCode(type, methodName, true), listName, count));
    }

    public static void IsNull(object type, string methodName, string variableName, object variable)
    {
        ThrowIsNotNull(Exceptions.IsNull(FullNameOfExecutedCode(type, methodName, true), variableName, variable));
    }

    public static void IsNotNull(Type type, string methodName, string variableName, object variable)
    {
        ThrowIsNotNull(Exceptions.IsNotNull(FullNameOfExecutedCode(type, methodName, true), variableName, variable));
    }

    public static void ArrayElementContainsUnallowedStrings(object type, string methodName, string arrayName, int dex, string valueElement, params string[] unallowedStrings)
    {
        ThrowIsNotNull(Exceptions.ArrayElementContainsUnallowedStrings(FullNameOfExecutedCode(type, methodName, true), arrayName, dex, valueElement, unallowedStrings));
    }

    public static void OnlyOneElement(object type, string methodName, string colName, IEnumerable list)
    {
        ThrowIsNotNull(Exceptions.OnlyOneElement(FullNameOfExecutedCode(type, methodName, true), colName, list));
    }

    public static void StringContainsUnallowedSubstrings(object type, string methodName, string input, params string[] unallowedStrings)
    {
        ThrowIsNotNull(Exceptions.StringContainsUnallowedSubstrings(FullNameOfExecutedCode(type, methodName, true), input, unallowedStrings));
    }

    public static void InvalidParameter(object type, string methodName, string mayUrlDecoded, string typeOfInput)
    {
        ThrowIsNotNull(Exceptions.InvalidParameter(FullNameOfExecutedCode(type, methodName, true), mayUrlDecoded, typeOfInput));
    }

    public static void ElementCantBeFound(object type, string methodName, string nameCollection, string element)
    {
        ThrowIsNotNull(Exceptions.ElementCantBeFound(FullNameOfExecutedCode(type, methodName, true), nameCollection, element));
    }

    //IsNotWindowsPathFormat
    public static void IsNotWindowsPathFormat(object type, string methodName, string argName, string argValue)
    {
        ThrowIsNotNull(Exceptions.IsNotWindowsPathFormat(FullNameOfExecutedCode(type, methodName, true), argName, argValue));
    }

    public static void IsNullOrEmpty(object type, string methodName, string argName, string argValue)
    {
        ThrowIsNotNull(Exceptions.IsNullOrEmpty(FullNameOfExecutedCode(type, methodName, true), argName, argValue));
    }
    #endregion

    #region Without parameters
    public static void NotImplementedCase(object type, string methodName)
    {
        ThrowIsNotNull(Exceptions.NotImplementedCase(FullNameOfExecutedCode(type, methodName, true)));
    }


    #endregion

    #region Without locating executing code
    public static void CheckBackslashEnd(string r)
    {
        ThrowIsNotNull(Exceptions.CheckBackslashEnd("", r));
    }
    #endregion

    #region Helpers
    /// <summary>
    /// First can be Method base, then A2 can be anything
    /// </summary>
    /// <param name="type"></param>
    /// <param name="methodName"></param>
    /// <returns></returns>
    public static string FullNameOfExecutedCode(object type, string methodName, bool fromThrowExceptions = false)
    {
        if (methodName == null)
        {
            int depth = 2;
            if (fromThrowExceptions)
            {
                depth++;
            }
            methodName = RH.CallingMethod(depth);
        }

        string typeFullName = string.Empty;
        if (type is Type)
        {
            var type2 = ((Type)type);
            typeFullName = type2.FullName;
        }
        else if (type is MethodBase)
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

        return string.Concat(typeFullName, AllStrings.dot, methodName);
    }

    /// <summary>
    /// Default use here method with one argument
    /// </summary>
    /// <param name="type"></param>
    /// <param name="methodName"></param>
    /// <param name="exception"></param>
    public static void ThrowIsNotNull(object type, string methodName, string exception)
    {
        if (exception != null)
        {
            throw new Exception(exception);
        }
    }

    /// <summary>
    /// true if everything is OK
    /// false if some error occured
    /// </summary>
    /// <param name="exception"></param>
    public static bool ThrowIsNotNull(string exception)
    {
        if (exception != null)
        {
            if (ThisApp.aspnet)
            {
                //if (HttpRuntime.AppDomainAppId != null)
                //{
                Debugger.Break();
                //}
            }

            //DebugLogger.Instance.WriteLine(exception);
            throw new Exception(exception);
            return false;
        }
        return true;
    }

    public static void NoPassedFolders(Type type, string v, IEnumerable folders)
    {
        ThrowIsNotNull(Exceptions.NoPassedFolders(FullNameOfExecutedCode(type, v, true), folders));
    }

    private static void ThrowIsNotNull(object v)
    {
        if (v != null)
        {
            ThrowIsNotNull(v.ToString());
        }
    }

    /// <summary>
    /// Verify whether A3 contains A4
    /// true if everything is OK
    /// false if some error occured
    /// </summary>
    /// <param name="type"></param>
    /// <param name="v"></param>
    /// <param name="p"></param>
    /// <param name="after"></param>
    public static bool NotContains(Type type, string v, string p, params string[] after)
    {
        return ThrowIsNotNull(Exceptions.NotContains(FullNameOfExecutedCode(type, v, true), p, after));
    }

    /// <summary>
    /// Return & throw exception whether directory NOT exists
    /// </summary>
    /// <param name="type"></param>
    /// <param name="methodName"></param>
    /// <param name="folder1"></param>
    public static void DirectoryWasntFound(Type type, string methodName, string folder1)
    {
        ThrowIsNotNull(Exceptions.DirectoryWasntFound(FullNameOfExecutedCode(type, methodName, true), folder1));
    }

    public static void Custom(object type, string methodName, string message)
    {
        ThrowIsNotNull(Exceptions.Custom(FullNameOfExecutedCode(type, methodName, true), message));
    }

    /// <summary>
    /// Throw exc A4,5 is same count of elements
    /// </summary>
    /// <param name="type"></param>
    /// <param name="methodName"></param>
    /// <param name="detailLocation"></param>
    /// <param name="before"></param>
    /// <param name="after"></param>
    public static void ElementWasntRemoved(Type type, string methodName, string detailLocation, int before, int after)
    {
        ThrowIsNotNull(Exceptions.ElementWasntRemoved(FullNameOfExecutedCode(type, methodName, true), detailLocation, before, after));
    }

    public static void FolderCantBeRemoved(Type type, string methodName, string folder)
    {
        ThrowIsNotNull(Exceptions.FolderCantBeRemoved(FullNameOfExecutedCode(type, methodName, true), folder));
    }

    public static void FileHasExtensionNotParseableToImageFormat(Type type, string methodName, string fnOri)
    {
        ThrowIsNotNull(Exceptions.FileHasExtensionNotParseableToImageFormat(FullNameOfExecutedCode(type, methodName), fnOri));
    }

    public static void FileSystemException(Type type, string methodName, Exception ex)
    {
        ThrowIsNotNull(Exceptions.FileSystemException(FullNameOfExecutedCode(type, methodName), ex));
    }

    public static void FuncionalityDenied(Type type, string methodName, string description)
    {
        ThrowIsNotNull(Exceptions.FuncionalityDenied(FullNameOfExecutedCode(type, methodName), description));
    }
    #endregion
}
