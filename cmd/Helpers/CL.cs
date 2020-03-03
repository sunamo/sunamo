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
            if (item.Key != "None")
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