using sunamo.Values;
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
    public class Exceptions
    {

    //
    

    /// <summary>
    /// Start with Consts.Exception to identify occur
    /// </summary>
    /// <param name="ex"></param>
    /// <param name="alsoInner"></param>
    /// <returns></returns>
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

        /// <summary>
        /// Zmena: metoda nezapisuje primo na konzoli, misto toho pouze vraci retezec
        /// </summary>
        public static string FileHasWrongExtension(string fnOri)
        {
            return "File " + fnOri + " has wrong file extension";
        }

        public static string WrongCountInList2(int numberOfElementsWithoutPause, int numberOfElementsWithPause, int arrLength)
        {
            return SH.Format2("Array should have {0} or {1} elements, have {2}", numberOfElementsWithoutPause, numberOfElementsWithPause, arrLength);
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

        public static string FileExists(string before, string fulLPath)
        {
            if (FS.ExistsFile(fulLPath))
            {
                return null;
            }
            return CheckBefore(before) + " Doesn't exists: " + fulLPath;
        }

        #region Without parameters
        public static string NotImplementedCase( string before)
        {
            return CheckBefore( before) + "Not implemented case. public program error. Please contact developer.";
        }

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
                return CheckBefore(before) + nameControl + " doesnt have setted Name";
            }
            return null;
        }

    internal static object StringContainsUnallowedSubstrings(string before, string input, string[] unallowedStrings)
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
            return variableName + "Doesn't have required type.";
        }
        #endregion

        public static string CheckBackslashEnd(string before, string r)
        {
            if (r.Length != 0)
            {
                if (r[r.Length - 1] != AllChars.bs)
                {
                    return CheckBefore( before) + "String has not been in path format!";
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
            return CheckBefore(before) + "File "+fileName+" wasn't found in " + directory;
        }

        public static string FileWasntFoundInDirectory(string before, string fullPath)
        {
            string path, fn;
            FS.GetPathAndFileName(fullPath, out path, out fn);
            return FileWasntFoundInDirectory(before, path, fn);
        }


        private static string CheckBefore(string before)
        {
            if (string.IsNullOrWhiteSpace(before))
            {
                return "";
            }
            return before + ": ";
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
                return CheckBefore( before) + " Different count elements in collection " + string.Concat(namefc + AllStrings.swda + countfc) + " vs. " + string.Concat(namesc + AllStrings.swda + countsc);
            }

            return null;
        }

        public static string AnyElementIsNullOrEmpty(string before, string nameOfCollection, List<int> nulled)
        {
            return CheckBefore(before) + $"In {nameOfCollection} has indexes " + SH.Join(AllChars.comma, nulled) + " with null value"; 
        }

        public static string IsNull(string before, string variableName, object variable)
        {
            if (variable == null)
            {
                return CheckBefore(before) + variable + " is null.";
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
                return CheckBefore( before) + " Element of " + arrayName + " with value " + valueElement + " contains unallowed string(" + foundedUnallowed.Count + "): " + SH.Join(AllChars.comma, unallowedStrings);
            }

            return null;
        }

    public static object Custom(string before, string message)
    {
        return CheckBefore(before) + message;
    }

    internal static object FolderCantBeRemoved(string v, string folder)
    {
        return CheckBefore(v) + "Can't delete folder: " + folder;
    }

    /// <summary>
    /// Check whether in A3,4 is same count of elements
    /// </summary>
    /// <param name="before"></param>
    /// <param name="detailLocation"></param>
    /// <param name="before2"></param>
    /// <param name="after"></param>
    /// <returns></returns>
    public static object ElementWasntRemoved(string before, string detailLocation, int before2, int after)
    {
        if (before2 == after)
        {
            return CheckBefore(before) + "Element wasnt removed during: " + detailLocation;
        }
        return null;
    }

    /// <summary>
    /// Verify whether A2 contains A3
    /// </summary>
    /// <param name="before"></param>
    /// <param name="originalText"></param>
    /// <param name="shouldContains"></param>
    /// <returns></returns>
    public static string NotContains(string before, string originalText, params string[] shouldContains)
        {
            List<string> notContained = new List<string>();
            foreach (var item in shouldContains)
            {
                if (!originalText.Contains(item))
                {
                    notContained.Add(item);
                }
            }

        if (notContained.Count == 0)
        {
            return null;
        }
            return CheckBefore(before) + originalText + " dont contains: " + SH.Join(notContained, AllStrings.comma);
        }

        public static string NoPassedFolders(string before, IEnumerable folders)
        {
            if (folders.Count() == 0)
            {
                return CheckBefore(before) + "No passed folder into";
            }
            return null;
        }

        public static string DirectoryWasntFound(string before, string directory)
        {
            if (!FS.ExistsDirectory(directory))
            {
                return CheckBefore(before) + "Directory " + directory + " wasn't found.";
            }

            return null;
        }

        public static string InvalidParameter(string before, string mayUrlDecoded, string typeOfInput)
        {
            if (mayUrlDecoded != WebUtility.UrlDecode(mayUrlDecoded))
            {
                return CheckBefore( before) + mayUrlDecoded + " is url endoded " + typeOfInput;
            }

            return null;
        }

        public static string ElementCantBeFound(string before, string nameCollection, string element)
        {
            return CheckBefore( before) + element + "cannot be found in " + nameCollection;
        }

        public static string MoreCandidates(string before, List<string> list, string item)
        {
            return CheckBefore(before) + "Under " + item + " is more candidates: " + Environment.NewLine + SH.JoinNL(list);
        }

        public static string BadMappedXaml(string before, string nameControl, string additionalInfo)
        {
            return CheckBefore(before) + $"Bad mapped XAML in {nameControl}. {additionalInfo}";
        }
    }
