using System;
using System.Collections.Generic;
using System.Text;

namespace SunamoExceptions
{
    public class Exceptions
    {
        #region For easy copy in SunamoException project
        public static string ArgumentOutOfRangeException(string before, string paramName, string message)
        {
            if (paramName == null)
            {
                paramName = string.Empty;
            }

            if (message == null)
            {
                message = string.Empty;
            }

            return CheckBefore(before) + paramName + " " + message;
        }
        public static string IsNull(string before, string variableName, object variable)
        {
            if (variable == null)
            {
                return CheckBefore(before) + variable + " " + "is null" + ".";
            }

            return null;
        }
        public static string NotImplementedCase(string before, object niCase)
        {
            string fr = string.Empty;
            if (niCase != null)
            {
                fr = " for ";
                if (niCase.GetType() == typeof(Type))
                {
                    fr += ((Type)niCase).FullName;
                }
                else
                {
                    fr += niCase.ToString();
                }
            }

            return CheckBefore(before) + "Not implemented case" + fr + " . public program error. Please contact developer" + ".";
        }

        /// <summary>
        /// Verify whether A2 contains A3
        /// </summary>
        /// <param name="before"></param>
        /// <param name="originalText"></param>
        /// <param name="shouldContains"></param>
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
            return CheckBefore(before) + originalText + " " + "dont contains" + ": " + SH.Join(notContained, ",");
        }

        public static object Custom(string before, string message)
        {
            return CheckBefore(before) + message;
        }

        public static object NotImplementedMethod(string before)
        {
            return CheckBefore(before) + "Not implemented case. public program error. Please contact developer" + ".";
        }
        private static string CheckBefore(string before)
        {
            if (string.IsNullOrWhiteSpace(before))
            {
                return "";
            }
            return before + ": ";
        }
        #endregion
    }
}