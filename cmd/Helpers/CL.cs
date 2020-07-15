using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.IO;
using sunamo;
using sunamo.Enums;
using cmd.Essential;
using sunamo.Essential;
using sunamo.Constants;
using System.Linq;

public static partial class CL{
    readonly static string charOfHeader = AllStrings.asterisk;
    static CL()
    {
    }

    private static void AddToAllActions(string v, Dictionary<string, VoidVoid> actions, Dictionary<string, VoidVoid> allActions)
    {
        foreach (var item in actions)
        {
            if (item.Key != sess.i18n(XlfKeys.None))
            {
                allActions.Add(v + AllStrings.swd + item.Key, item.Value);
            }
        }
    }

    public static void PressEnterAfterInsertDataToClipboard(string what)
    {
        if (CmdApp.loadFromClipboard)
        {
            AppealEnter("Insert " + what + " to clipboard");
        }
    }

    public static void Success(string v)
    {
        TypedConsoleLogger.Instance.Success(v);
    }

    public static void ClearCurrentConsoleLine()
    {
        int currentLineCursor = Console.CursorTop;
        Console.SetCursorPosition(0, Console.CursorTop);
        Console.Write(new string(AllChars.space, Console.WindowWidth));
        Console.SetCursorPosition(0, currentLineCursor);
    }

    public static void Information(string v)
    {
        TypedConsoleLogger.Instance.Information(v);
    }

    /// <summary>
    /// Ask user whether want to continue
    /// </summary>
    /// <param name = "text"></param>
    public static DialogResult DoYouWantToContinue(string text)
    {
        text = sess.i18n(XlfKeys.DoYouWantToContinue) + "?";
        TypedConsoleLogger.Instance.Warning(text);
        bool z = BTS.GetValueOfNullable( UserMustTypeYesNo(text));
        if (z)
        {
            return DialogResult.Yes;
        }

        return DialogResult.No;
    }

    

   

    public static void CmdTable(IEnumerable<List<string>> last)
    {
        StringBuilder formattingString = new StringBuilder();

        var f = last.First();
        for (int i = 0; i < f.Count; i++)
        {
            formattingString.Append("{" + i + ",5}|");
        }
        formattingString.Append("|");

        var fs = formattingString.ToString();

        foreach (var item in last)
        {
            Console.WriteLine(string.Format(fs, item.ToArray()));
        }
    }

    static void ChangeColorOfConsoleAndWrite(string text, TypeOfMessage tz)
    {
        ConsoleLogger.SetColorOfConsole(tz);
        Console.WriteLine(); Console.WriteLine(text);
        ConsoleLogger.SetColorOfConsole(TypeOfMessage.Ordinal);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name = "appeal"></param>
    public static void Appeal(string appeal)
    {
        ConsoleLogger.ChangeColorOfConsoleAndWrite(TypeOfMessage.Appeal, appeal);
    }

    public static void Success(string text, params string[] p)
    {
        ChangeColorOfConsoleAndWrite(string.Format(text, p), TypeOfMessage.Success);
    }

    public static void Error(string text, params string[] p)
    {
        ChangeColorOfConsoleAndWrite(string.Format(text, p), TypeOfMessage.Error);
    }
    public static void Warning(string text, params string[] p)
    {
        ChangeColorOfConsoleAndWrite(string.Format(text, p), TypeOfMessage.Warning);
    }
    public static void Information(string text, params string[] p)
    {
        ChangeColorOfConsoleAndWrite(string.Format(text, p), TypeOfMessage.Information);
    }

    /// <summary>
    /// Print 
    /// </summary>
    /// <param name = "appeal"></param>
    public static void AppealEnter(string appeal)
    {
        Appeal(appeal + ". " + sess.i18n(XlfKeys.ThenPressEnter) + ".");
        Console.ReadLine();
    }

    

    /// <summary>
    /// Return full path of selected file
    /// or null when operation will be stopped
    /// </summary>
    /// <param name = "folder"></param>
    public static string SelectFile(string folder)
    {
        var soubory = FS.GetFiles(folder);
        string output = "";
        int selectedFile = SelectFromVariants(soubory, "file which you want to open");
        if (selectedFile == -1)
        {
            return null;
        }

        output = soubory[selectedFile];
        return output;
    }

    public static void WriteLineFormat(string text, params object[] p)
    {
        Console.WriteLine();
        Console.WriteLine(SH.Format2(text, p));
    }

    /// <summary>
    /// Return printed text
    /// </summary>
    /// <param name = "text"></param>
    public static string StartRunTime(string text)
    {
        int textLength = text.Length;
        string stars = "";
        stars = new string(charOfHeader[0], textLength);
        StringBuilder sb = new StringBuilder();
        sb.AppendLine(stars);
        sb.AppendLine(text);
        sb.AppendLine(stars);
        string result = sb.ToString();
        TypedConsoleLogger.Instance.Information(result);
        return result;
    }

    /// <summary>
    /// Print and wait
    /// </summary>
    public static void EndRunTime(bool attempToRepairError = false)
    {
        if (attempToRepairError)
        {
            TypedConsoleLogger.Instance.Information(Messages.RepairErrors);
        }

        TypedConsoleLogger.Instance.Information(Messages.AppWillBeTerminated);
        Console.ReadLine();
    }

    /// <summary>
    /// Just print and wait
    /// </summary>
    public static void NoData()
    {
        ConsoleTemplateLogger.Instance.NoData();
    }
}