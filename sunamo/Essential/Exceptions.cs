﻿using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace sunamo.Essential
{
    public class Exceptions
    {
        /// <summary>
        /// Zmena: metoda nezapisuje primo na konzoli, misto toho pouze vraci retezec
        /// </summary>
        /// <param name="fn"></param>
        public static string SuccessfullyResized(string fn)
        {
            return "Successfully resized to " + fn;
        }

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

        public static string DifferentCountInLists(string before, string namefc, int countfc, string namesc, int countsc)
        {
            if (countfc != countsc)
            {
                return CheckBefore( before) + " Different count elements in collection " + string.Concat(namefc + " - " + countfc) + " vs. " + string.Concat(namesc + " - " + countsc);
            }

            return null;
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
            return CheckBefore(before) + "Under " + item + " is more candidates: " + SH.JoinNL(list);
        }
    }
}
