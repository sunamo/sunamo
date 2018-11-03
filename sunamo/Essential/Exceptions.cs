using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace sunamo.Essential
{
    /// <summary>
    /// TODO: Don't add anything here and just use TemplateLoggerBase and ThisApp.DefaultLogger (dependent in type of app - Console, WPF, web etc.)
    /// Here only errors and so where is needed define location of code
    /// </summary>
    public class Exceptions
    {
        

        /// <summary>
        /// Zmena: metoda nezapisuje primo na konzoli, misto toho pouze vraci retezec
        /// </summary>
        public static string FileHasWrongExtension(string fnOri)
        {
            return "File " + fnOri + " has wrong file extension";
        }

        #region Without parameters
        public static string NotImplementedCase( string before)
        {
            return CheckBefore( before) + "Not implemented case. public program error. Please contact developer.";
        }

        internal static string NameIsNotSetted(string before, string nameControl, string nameFromProperty)
        {
            if (string.IsNullOrWhiteSpace(nameFromProperty))
            {
                return CheckBefore(before) + nameControl + " doesnt have setted Name";
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
                    return CheckBefore( before) + "String has not been in path format!";
                }
            }

            return null;
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
        internal static string NotEvenNumberOfElements(string before, string nameOfCollection)
        {
            return CheckBefore(before) + nameOfCollection + " have odd elements count";
        } 
        #endregion

        public static string DifferentCountInLists(string before, string namefc, int countfc, string namesc, int countsc)
        {
            if (countfc != countsc)
            {
                return CheckBefore( before) + " Different count elements in collection " + string.Concat(namefc + " - " + countfc) + " vs. " + string.Concat(namesc + " - " + countsc);
            }

            return null;
        }

        internal static string AnyElementIsNullOrEmpty(string before, string nameOfCollection, List<int> nulled)
        {
            return CheckBefore(before) + $"In {nameOfCollection} has indexes " + SH.Join(AllChars.comma, nulled) + " with null value"; 
        }

        internal static string IsNull(string before, string variableName, object variable)
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
                return CheckBefore( before) + " Element of " + arrayName + " with value " + valueElement + " contains unallowed string(" + foundedUnallowed.Count + "): " + SH.Join(',', unallowedStrings);
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

        internal static string BadMappedXaml(string before, string nameControl, string additionalInfo)
        {
            return CheckBefore(before) + $"Bad mapped XAML in {nameControl}. {additionalInfo}";
        }
    }
}
