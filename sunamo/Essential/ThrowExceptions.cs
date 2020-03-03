
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

    public static void UseRlc(object type, string methodName)
    {
        ThrowIsNotNull(Exceptions.UseRlc(FullNameOfExecutedCode(type, methodName, true)));
    }

    public static void NotImplementedMethod(Type type, string methodName)
    {
        ThrowIsNotNull(Exceptions.NotImplementedMethod(FullNameOfExecutedCode( type, methodName)));
    }

    public static bool OutOfRange(Type type, string methodName, string colName, IEnumerable col, string indexName, int index)
    {
        return ThrowIsNotNull(Exceptions.OutOfRange(FullNameOfExecutedCode(type, methodName), colName, col, indexName, index));
    }

    /// <summary>
    /// Return & throw exception whether directory exists
    /// </summary>
    /// <param name="type"></param>
    /// <param name="v"></param>
    /// <param name="photosPath"></param>
    
    private static bool ThrowIsNotNull(object v)
    {
        if (v != null)
        {
            ThrowIsNotNull(v.ToString());
            return false;
        }
        return true;
    }

    public static void WasNotKeysHandler(Type type, string methodName, string name, object keysHandler)
    {
        ThrowIsNotNull(Exceptions.WasNotKeysHandler( FullNameOfExecutedCode(type, methodName), name, keysHandler));
    }

    /// <summary>
    /// Default use here method with one argument
    /// Return false in case of exception, otherwise true
    /// In console app is needed put in into try-catch error due to there is no globally handler of errors
    /// </summary>
    /// <param name="type"></param>
    /// <param name="methodName"></param>
    /// <param name="exception"></param>
    public static bool ThrowIsNotNull(object type, string methodName, string exception)
    {
        if (exception != null)
        {
            throw new Exception(exception);
            return false;
        }
        return true;
    }
    #endregion

    public static void NoPassedFolders(Type type, string v, IEnumerable folders)
    {
        ThrowIsNotNull(Exceptions.NoPassedFolders(FullNameOfExecutedCode(type, v, true), folders));
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
