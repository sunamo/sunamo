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

    /// <summary>
    /// Return null when user force stop 
    /// </summary>
    /// <param name = "what"></param>
    /// <param name = "textFormat"></param>
    /// <returns></returns>
    public static string UserMustTypeInFormat(string what, TextFormatData textFormat)
    {
        string entered = "";
        while (true)
        {
            entered = UserMustType(what);
            if (entered == null)
            {
                return null;
            }

            if (SH.HasTextRightFormat(entered, textFormat))
            {
                return entered;
            }
            else
            {
                ConsoleTemplateLogger.Instance.UnfortunatelyBadFormatPleaseTryAgain();
            }
        }

        return null;
    }

    public static void ClearCurrentConsoleLine()
    {
        int currentLineCursor = Console.CursorTop;
        Console.SetCursorPosition(0, Console.CursorTop);
        Console.Write(new string(AllChars.space, Console.WindowWidth));
        Console.SetCursorPosition(0, currentLineCursor);
    }

    /// <summary>
    /// Ask user whether want to continue
    /// </summary>
    /// <param name = "text"></param>
    /// <returns></returns>
    public static DialogResult DoYouWantToContinue(string text)
    {
        text = RLData.en["DoYouWantToContinue"] + "?";
        TypedConsoleLogger.Instance.Warning(text);
        bool z = BTS.GetValueOfNullable( UserMustTypeYesNo(text));
        if (z)
        {
            return DialogResult.Yes;
        }

        return DialogResult.No;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name = "appeal"></param>
    public static void Appeal(string appeal)
    {
        ConsoleLogger.ChangeColorOfConsoleAndWrite(TypeOfMessage.Appeal, appeal);
    }

    /// <summary>
    /// Print 
    /// </summary>
    /// <param name = "appeal"></param>
    public static void AppealEnter(string appeal)
    {
        Appeal(appeal + ". " + RLData.en["ThenPressEnter"] + ".");
        Console.ReadLine();
    }

    /// <summary>
    /// Return full path of selected file
    /// or null when operation will be stopped
    /// </summary>
    /// <param name = "folder"></param>
    /// <returns></returns>
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
    /// <returns></returns>
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