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
    public static bool RaiseIsNotWindowsPathFormat;

    //


    /// <summary>
    /// Start with Consts.Exception to identify occur
    /// </summary>
    /// <param name="ex"></param>
    /// <param name="alsoInner"></param>
    
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
        return CheckBefore(before) + originalText + " " + "dont contains" + ": " + SH.Join(notContained, AllStrings.comma);
    }

    public static string NoPassedFolders(string before, IEnumerable folders)
    {
        if (folders.Count() == 0)
        {
            return CheckBefore(before) + "No passed folder into";
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
            return CheckBefore(before) + "Directory" + " " + directory + " " + "wasn't found" + ".";
        }

        return null;
    }

    public static object FuncionalityDenied(string before, string description)
    {
        return CheckBefore(before) + description;
    }

    public static string InvalidParameter(string before, string mayUrlDecoded, string typeOfInput)
    {
        if (mayUrlDecoded != WebUtility.UrlDecode(mayUrlDecoded))
        {
            return CheckBefore(before) + mayUrlDecoded + " " + "is url endoded" + " " + typeOfInput;
        }

        return null;
    }

    public static string ElementCantBeFound(string before, string nameCollection, string element)
    {
        return CheckBefore(before) + element + "cannot be found in" + " " + nameCollection;
    }

    public static string MoreCandidates(string before, List<string> list, string item)
    {
        return CheckBefore(before) + "Under" + " " + item + " " + "is more candidates" + ": " + Environment.NewLine + SH.JoinNL(list);
    }

    public static string BadMappedXaml(string before, string nameControl, string additionalInfo)
    {
        return CheckBefore(before) + $"Bad mapped XAML in {nameControl}. {additionalInfo}";
    }
}
