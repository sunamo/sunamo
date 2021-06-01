using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public partial class Exceptions
{
    public static StringBuilder sbAdditionalInfoInner = new StringBuilder();
    public static StringBuilder sbAdditionalInfo = new StringBuilder();

    internal static string IsTheSame(string before, string fst, string sec)
    {
        return before + $"{fst} and {sec} has the same value";
    }

    public static string NotImplementedMethod(string before)
    {
        return CheckBefore(before) + SunamoPageHelperSunamo.i18n(XlfKeys.NotImplementedCasePublicProgramErrorPleaseContactDeveloper) + ".";
    }

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
        return addParams + addParamsInner;
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

    public static bool RaiseIsNotWindowsPathFormat;

    public static string IsNotWindowsPathFormat(string before, string argName, string argValue)
    {
        if (RaiseIsNotWindowsPathFormat)
        {
            var badFormat = !FS.IsWindowsPathFormat(argValue);

            if (badFormat)
            {
                return CheckBefore(before) + " " + argName + " " + SunamoPageHelperSunamo.i18n(XlfKeys.isNotInWindowsPathFormat);
            }
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

   
    internal static string DivideByZero(string before)
    {
        return CheckBefore(before) + " is dividing by zero.";
    }
    public static string IsNull(string before, string variableName, object variable)
    {
        if (variable == null)
        {
            return CheckBefore(before) + variable + " is null.";
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

        return CheckBefore(before) + SunamoPageHelperSunamo.i18n(XlfKeys.NotImplementedCase) + fr + ". " + SunamoPageHelperSunamo.i18n(XlfKeys.publicProgramErrorPleaseContactDeveloper) + ".";
    }
    public static string Custom(string before, string message)
    {
        return CheckBefore(before) + message;
    }


    

    private static string CheckBefore(string before)
    {
        if (string.IsNullOrWhiteSpace(before))
        {
            return "";
        }
        return before + ": ";
    }

    public static string AnyElementIsNullOrEmpty(string before, string nameOfCollection, List<int> nulled)
    {
        return CheckBefore(before) + $"In {nameOfCollection} has indexes " + SH.Join(AllChars.comma, nulled) + " with null value";
    }

    #region Called from TemplateLoggerBase
    public static string NotEvenNumberOfElements(string before, string nameOfCollection)
    {
        return CheckBefore(before) + nameOfCollection + " have odd elements count";
    }
    #endregion
}
