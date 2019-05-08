
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Reflection;

namespace sunamo.Essential
{ }

public class ThrowExceptions
{
    #region Must be as first - newly created method fall into this
    public static void BadMappedXaml(object type, string methodName, string nameControl, string additionalInfo)
    {
        ThrowIsNotNull(Exceptions.BadMappedXaml(FullNameOfExecutedCode(type, methodName), nameControl, additionalInfo));
    }

    public static void FileExists(object type, string methodName, string fulLPath)
    {
        ThrowIsNotNull(Exceptions.FileExists(FullNameOfExecutedCode(type, methodName), fulLPath));
    }

    public static void HaveAllInnerSameCount(Type type, string methodName, List<List<string>> elements)
    {
        ThrowIsNotNull(Exceptions.HaveAllInnerSameCount(FullNameOfExecutedCode(type, methodName), elements));
    }

    /// <summary>
    /// Must be string due to in sunamo is not NamespaceElement
    /// </summary>
    /// <param name="name"></param>
    public static void NameIsNotSetted(object type, string methodName, string nameControl, string nameFromProperty)
    {
            ThrowIsNotNull(Exceptions.NameIsNotSetted(FullNameOfExecutedCode(type, methodName), nameControl, nameFromProperty));
    }

    public static void DifferentCountInLists(object type, string methodName, string namefc, int countfc, string namesc, int countsc)
    {
        ThrowIsNotNull(Exceptions.DifferentCountInLists(FullNameOfExecutedCode(type, methodName), namefc, countfc, namesc, countsc));
    }

    public static void DoesntHaveRequiredType(object type, string methodName, string variableName)
    {
        ThrowIsNotNull(Exceptions.DoesntHaveRequiredType(FullNameOfExecutedCode(type, methodName), variableName));
    }

    public static void DifferentCountInLists(Type type, string methodName, string namefc, IEnumerable replaceFrom, string namesc, IEnumerable replaceTo)
    {
        DifferentCountInLists(type, methodName, namefc, replaceFrom.Count(), namesc, replaceTo.Count());
    }

    public static void IsNotAllowed(Type type, string methodName, string what)
    {
        ThrowIsNotNull(Exceptions.IsNotAllowed(FullNameOfExecutedCode(type, methodName), what));
    }

    public static void MoreThanOneElement(Type type, string methodName, string listName, int count)
    {
        ThrowIsNotNull(Exceptions.MoreThanOneElement(FullNameOfExecutedCode(type, methodName), listName, count));
    }

    public static void IsNull(object type, string methodName, string variableName, object variable)
    {
        ThrowIsNotNull(Exceptions.IsNull(FullNameOfExecutedCode(type, methodName), variableName, variable));
    }

    public static void ArrayElementContainsUnallowedStrings(object type, string methodName, string arrayName, int dex, string valueElement, params string[] unallowedStrings)
    {
        ThrowIsNotNull(Exceptions.ArrayElementContainsUnallowedStrings(FullNameOfExecutedCode(type, methodName), arrayName, dex, valueElement, unallowedStrings));
    }

    public static void StringContainsUnallowedSubstrings(Type type, string methodName, string input, params string[] unallowedStrings)
    {
        ThrowIsNotNull(Exceptions.StringContainsUnallowedSubstrings(FullNameOfExecutedCode(type, methodName), input, unallowedStrings));
    }

    public static void InvalidParameter(object type, string methodName, string mayUrlDecoded, string typeOfInput)
    {
        ThrowIsNotNull(Exceptions.InvalidParameter(FullNameOfExecutedCode(type, methodName), mayUrlDecoded, typeOfInput));
    }

    public static void ElementCantBeFound(object type, string methodName, string nameCollection, string element)
    {
        ThrowIsNotNull(Exceptions.ElementCantBeFound(FullNameOfExecutedCode(type, methodName), nameCollection, element));
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
            
            throw new Exception(exception);
            return false;
        }
        return true;
    }

    public static void NoPassedFolders(Type type, string v, IEnumerable folders)
    {
        ThrowIsNotNull(Exceptions.NoPassedFolders(FullNameOfExecutedCode( type, v), folders));   
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
        return ThrowIsNotNull(Exceptions.NotContains(FullNameOfExecutedCode(type, v), p, after));
    }

    public static void DirectoryWasntFound(Type type, string methodName, string folder1)
    {
        ThrowIsNotNull(Exceptions.DirectoryWasntFound(FullNameOfExecutedCode( type, methodName), folder1));
    }

    public static void Custom(object type, string methodName, string message)
    {
        ThrowIsNotNull(Exceptions.Custom(FullNameOfExecutedCode(type, methodName), message));
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
       
         ThrowIsNotNull(Exceptions.ElementWasntRemoved(FullNameOfExecutedCode(type, methodName), detailLocation, before, after));
        
    }

    public static void FolderCantBeRemoved(Type type, string methodName, string folder)
    {
        ThrowIsNotNull(Exceptions.FolderCantBeRemoved(FullNameOfExecutedCode(type, methodName), folder));
    }


    #endregion


}
