
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;


/// <summary>
/// TODO: Don't add anything here and just use TemplateLoggerBase and ThisApp.DefaultLogger (dependent in type of app - Console, WPF, web etc.)
/// Here only errors and so where is needed define location of code
/// </summary>
public partial class Exceptions
{
    public static bool RaiseIsNotWindowsPathFormat;
    
    //


    /// <summary>
    /// Start with Consts.Exception to identify occur
    /// </summary>
    /// <param name="ex"></param>
    /// <param name="alsoInner"></param>
    public static string TextOfExceptions(Exception ex, bool alsoInner = true)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append(Consts.Exception);
        sb.AppendLine(ex.Message);
        while (ex.InnerException != null)
        {
            ex = ex.InnerException;
            sb.AppendLine(ex.Message);
        }
        return sb.ToString();
    }





    public static object UseRlc(string before)
    {
        return CheckBefore(before) + SunamoPageHelperSunamo.i18n(XlfKeys.DonTImplementUseMethodsInRlc);
    }

    public static object IsWhitespaceOrNull(string before, string variable, object data)
    {
        bool isNull = false;

        if (data == null)
        {
            isNull = true;
        }
        else if (data.ToString().Trim() == string.Empty)
        {
            isNull = true;
        }

        if (isNull)
        {
            return CheckBefore(before) + variable + " is null";
        }
        return null;
    }

    public static object OutOfRange(string v, string colName, IEnumerable col, string indexName, int index)
    {
        if (col.Count() <= index)
        {
            return CheckBefore(v) + $"{index} (variable {indexName}) is out of range in {colName}";
        }
        return null;
    }

    /// <summary>
    /// Zmena: metoda nezapisuje primo na konzoli, misto toho pouze vraci retezec
    /// </summary>
    public static string FileHasExtensionNotParseableToImageFormat(string before, string fnOri)
    {
        return CheckBefore(before) + SunamoPageHelperSunamo.i18n(XlfKeys.File) + " " + fnOri + " has wrong file extension";
    }

    public static string WrongCountInList2(int numberOfElementsWithoutPause, int numberOfElementsWithPause, int arrLength)
    {
        return SH.Format2(SunamoPageHelperSunamo.i18n(XlfKeys.ArrayShouldHave0Or1ElementsHave2), numberOfElementsWithoutPause, numberOfElementsWithPause, arrLength);
    }

    public static object IsOdd(string before, string colName, IEnumerable col)
    {
        if (col.Count() % 2 == 1)
        {
            return CheckBefore(before) + colName + " has odd number of elements " + col.Count();
        }
        return null;
    }

    public static string HaveAllInnerSameCount(string before, List<List<string>> elements)
    {
        int first = elements[0].Count;
        List<int> wrongCount = new List<int>();
        for (int i = 1; i < elements.Count; i++)
        {
            if (first != elements[i].Count)
            {
                wrongCount.Add(i);
            }
        }
        if (wrongCount.Count > 0)
        {
            return $"Elements {SH.Join(wrongCount, AllChars.comma)} have different count than 0 (first)";
        }
        return null;
    }

    public static string DirectoryExists(string before, string fulLPath)
    {
        if (FS.ExistsDirectory(fulLPath))
        {
            return null;
        }
        return CheckBefore(before) + " " + SunamoPageHelperSunamo.i18n(XlfKeys.DoesnTExists) + ": " + fulLPath;
    }


    public static string FileExists(string before, string fulLPath)
    {
        if (FS.ExistsFile(fulLPath))
        {
            return null;
        }
        return CheckBefore(before) + " " + SunamoPageHelperSunamo.i18n(XlfKeys.DoesnTExists) + ": " + fulLPath;
    }

    #region Without parameters
    

    public static object MoreThanOneElement(string before, string listName, int count)
    {
        if (count > 1)
        {
            return CheckBefore(before) + listName + " has " + count + " elements, which is more than 1";
        }
        return null;
    }

    public static string NameIsNotSetted(string before, string nameControl, string nameFromProperty)
    {
        if (string.IsNullOrWhiteSpace(nameFromProperty))
        {
            return CheckBefore(before) + nameControl + " " + SunamoPageHelperSunamo.i18n(XlfKeys.doesntHaveSettedName);
        }
        return null;
    }

    public static object OnlyOneElement(string before, string colName, IEnumerable list)
    {
        if (list.Count() == 1)
        {
            return CheckBefore(before) + colName + " has only one element";
        }
        return null;
    }



    public static object StringContainsUnallowedSubstrings(string before, string input, params string[] unallowedStrings)
    {
        List<string> foundedUnallowed = new List<string>();
        foreach (var item in unallowedStrings)
        {
            if (input.Contains(item))
            {
                foundedUnallowed.Add(item);
            }
        }

        if (foundedUnallowed.Count > 0)
        {
            return CheckBefore(before) + input + " contains unallowed chars: " + SH.Join(unallowedStrings, AllStrings.space);
        }
        return null;
    }

    public static string DoesntHaveRequiredType(string before, string variableName)
    {
        return variableName + SunamoPageHelperSunamo.i18n(XlfKeys.DoesnTHaveRequiredType) + ".";
    }

    public static string IsNullOrEmpty(string before, string argName, string argValue)
    {
        string addParams = null;

        if (argValue == null)
        {
            addParams = AddParams();
            return CheckBefore(before) + argName + " is null" + addParams;
        }
        else if (argValue == string.Empty)
        {
            addParams = AddParams();
            return CheckBefore(before) + argName + " is empty (without trim)" + addParams;
        }
        else if (argValue.Trim() == string.Empty)
        {
            addParams = AddParams();
            return CheckBefore(before) + argName + " is empty (with trim)" + addParams;
        }

        return null;
    }

    private static string AddParams()
    {
        sbAdditionalInfo.Insert(0, Environment.NewLine);
        sbAdditionalInfo.Insert(0, "Outer:");
        sbAdditionalInfo.Insert(0, Environment.NewLine);

        sbAdditionalInfoInner.Insert(0, Environment.NewLine);
        sbAdditionalInfoInner.Insert(0, "Inner:");
        sbAdditionalInfoInner.Insert(0, Environment.NewLine);

        var addParams = sbAdditionalInfo.ToString();
        var addParamsInner = sbAdditionalInfoInner.ToString();
        return  addParams + addParamsInner;
    }

    public static object IsNotWindowsPathFormat(string before, string argName, string argValue)
    {
        if (RaiseIsNotWindowsPathFormat)
        {
            var badFormat = !FS.IsWindowsPathFormat(argValue);

            if (badFormat)
            {
                return CheckBefore(before) + " " + argName + " "+SunamoPageHelperSunamo.i18n(XlfKeys.isNotInWindowsPathFormat);
            }
        }

        return null;
    }


    #endregion

    public static string CheckBackslashEnd(string before, string r)
    {
        if (r.Length != 0)
        {
            if (r[r.Length - 1] != AllChars.bs)
            {
                return CheckBefore(before) + SunamoPageHelperSunamo.i18n(XlfKeys.StringHasNotBeenInPathFormat) + "!";
            }
        }

        return null;
    }

    public static object IsNotAllowed(string before, string what)
    {
        return CheckBefore(before) + what + " is not allowed.";
    }

    public static string FileWasntFoundInDirectory(string before, string directory, string fileName)
    {
        return CheckBefore(before) + SunamoPageHelperSunamo.i18n(XlfKeys.File) + " " + fileName + " wasn't found in " + directory;
    }

    public static string FileWasntFoundInDirectory(string before, string fullPath)
    {
        string path, fn;
        FS.GetPathAndFileName(fullPath, out path, out fn);
        return FileWasntFoundInDirectory(before, path, fn);
    }

    public static string NotSupported(string v)
    {
        return CheckBefore(v) + SunamoPageHelperSunamo.i18n(XlfKeys.NotSupported);
    }



    #region Called from TemplateLoggerBase
    public static string NotEvenNumberOfElements(string before, string nameOfCollection)
    {
        return CheckBefore(before) + nameOfCollection + " have odd elements count";
    }
    #endregion

    public static string DifferentCountInLists(string before, string namefc, int countfc, string namesc, int countsc)
    {
        if (countfc != countsc)
        {
            return CheckBefore(before) + " " + SunamoPageHelperSunamo.i18n(XlfKeys.DifferentCountElementsInCollection) + " " + string.Concat(namefc + AllStrings.swda + countfc) + " vs. " + string.Concat(namesc + AllStrings.swda + countsc);
        }

        return null;
    }

    public static string AnyElementIsNullOrEmpty(string before, string nameOfCollection, List<int> nulled)
    {
        return CheckBefore(before) + $"In {nameOfCollection} has indexes " + SH.Join(AllChars.comma, nulled) + " with null value";
    }

    

    public static object IsNotNull(string before, string variableName, object variable)
    {
        if (variable != null)
        {
            return CheckBefore(before) + variable + " must be null.";
        }

        return null;
    }

    public static string ToManyElementsInCollection(string before, int max, int actual, string nameCollection)
    {
        return CheckBefore(before) + actual + " elements in " + nameCollection + ", maximum is " + max;
    }

    public static string ArrayElementContainsUnallowedStrings(string before, string arrayName, int dex, string valueElement, params string[] unallowedStrings)
    {
        List<string> foundedUnallowed = SH.ContainsAny(valueElement, false, unallowedStrings);
        if (foundedUnallowed.Count != 0)
        {
            return CheckBefore(before) + " " + SunamoPageHelperSunamo.i18n(XlfKeys.ElementOf) + " " + arrayName + " with value " + valueElement + " contains unallowed string(" + foundedUnallowed.Count + "): " + SH.Join(AllChars.comma, unallowedStrings);
        }

        return null;
    }

    public static string WasNotKeysHandler(string before, string name, object keysHandler)
    {
        if (keysHandler == null)
        {
            return CheckBefore(before) + name + " " + SunamoPageHelperSunamo.i18n(XlfKeys.wasNotIKeysHandler);
        }
        return null;
    }

    

    public static object FolderCantBeRemoved(string v, string folder)
    {
        return CheckBefore(v) + SunamoPageHelperSunamo.i18n(XlfKeys.CanTDeleteFolder) + ": " + folder;
    }

    /// <summary>
    /// Check whether in A3,4 is same count of elements
    /// </summary>
    /// <param name="before"></param>
    /// <param name="detailLocation"></param>
    /// <param name="before2"></param>
    /// <param name="after"></param>
    public static object ElementWasntRemoved(string before, string detailLocation, int before2, int after)
    {
        if (before2 == after)
        {
            return CheckBefore(before) + SunamoPageHelperSunamo.i18n(XlfKeys.ElementWasntRemovedDuring) + ": " + detailLocation;
        }
        return null;
    }

    

    public static string NoPassedFolders(string before, IEnumerable folders)
    {
        if (folders.Count() == 0)
        {
            return CheckBefore(before) + SunamoPageHelperSunamo.i18n(XlfKeys.NoPassedFolderInto);
        }
        return null;
    }

    public static object FileSystemException(string v, Exception ex)
    {
        if (ex != null)
        {
            return CheckBefore(v) + " " + Exceptions.TextOfExceptions(ex);
        }
        return null;
    }

    public static string DirectoryWasntFound(string before, string directory)
    {
        if (!FS.ExistsDirectory(directory))
        {
            return CheckBefore(before) + SunamoPageHelperSunamo.i18n(XlfKeys.Directory) + " " + directory + " wasn't found.";
        }

        return null;
    }

    public static object FuncionalityDenied(string before, string description)
    {
        return CheckBefore(before) + description;
    }

    /// <summary>
    /// Is used when single (not list etc) bad arg is entered to method
    /// </summary>
    /// <param name="before"></param>
    /// <param name="valueVar"></param>
    /// <param name="nameVar"></param>
    /// <returns></returns>
    public static string InvalidParameter(string before, string valueVar, string nameVar)
    {
        if (valueVar != WebUtility.UrlDecode(valueVar))
        {
            return CheckBefore(before) + valueVar + " is url encoded " + nameVar;
        }

        return null;
    }

    public static string ElementCantBeFound(string before, string nameCollection, string element)
    {
        return CheckBefore(before) + element + "cannot be found in " + nameCollection;
    }

    public static string MoreCandidates(string before, List<string> list, string item)
    {
        return CheckBefore(before) + SunamoPageHelperSunamo.i18n(XlfKeys.Under) + " " + item + " is more candidates: " + Environment.NewLine + SH.JoinNL(list);
    }

   

    public static string BadMappedXaml(string before, string nameControl, string additionalInfo)
    {
        return CheckBefore(before) + $"Bad mapped XAML in {nameControl}. {additionalInfo}";
    }
}