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

public static partial class CL
{
    

    
    

    /// <summary>
    /// Let user select action and run with A2 arg
    /// </summary>
    /// <param name = "akce"></param>
    public static void PerformAction(Dictionary<string, EventHandler> actions, object sender)
    {
        var listOfActions = NamesOfActions(actions);
        int selected = SelectFromVariants(listOfActions, sess.i18n(XlfKeys.SelectActionToProceed) + ":");
        string ind = listOfActions[selected];
        EventHandler eh = actions[ind];

        if (sender == null)
        {
            sender = selected;
        }

        eh.Invoke(sender, EventArgs.Empty);
    }



    

    /// <summary>
    /// Return names of actions passed from keys
    /// </summary>
    /// <param name = "actions"></param>
    private static List<string> NamesOfActions(Dictionary<string, EventHandler> actions)
    {
        List<string> ss = new List<string>();
        foreach (KeyValuePair<string, EventHandler> var in actions)
        {
            ss.Add(var.Key);
        }

        return ss;
    }



    /// <summary>
    /// 
    /// </summary>
    /// <param name = "actions"></param>
    /// <param name = "vyzva"></param>
    public static void SelectFromVariants(Dictionary<string, EmptyHandler> actions)
    {
        string appeal = sess.i18n(XlfKeys.SelectAction) + ":";
        int i = 0;
        foreach (KeyValuePair<string, EmptyHandler> kvp in actions)
        {
            CL.WriteLine(AllStrings.lsqb + i + AllStrings.rsqb + "  " + kvp.Key);
            i++;
        }

        int entered = UserMustTypeNumber(appeal, actions.Count - 1);
        if (entered == -1)
        {
            OperationWasStopped();
            return;
        }

        i = 0;
        string operation = null;
        foreach (string var in actions.Keys)
        {
            if (i == entered)
            {
                operation = var;
                break;
            }

            i++;
        }


        var act = actions[operation];
        act.Invoke();
    }




    /// <summary>
    /// Return int.MinValue when user force stop operation
    /// </summary>
    /// <param name = "what"></param>
    public static int UserMustTypeNumber(string what, int max, int min)
    {
        int parsed = 1;
        string entered = null;
        bool isNumber = false;
        entered = UserMustType(what, false);
        if (entered == null)
        {
            return int.MinValue;
        }

        isNumber = int.TryParse(entered, out parsed);
        while (!isNumber)
        {
            entered = UserMustType(what, false);
            isNumber = int.TryParse(entered, out parsed);
            if (parsed <= max && parsed >= min)
            {
                break;
            }
        }

        return parsed;
    }
/// <summary>
    /// Return int.MinValue when user force stop operation
    /// </summary>
    /// <param name = "vyzva"></param>
    private static int UserMustTypeNumber(int max)
    {
        const string whatUserMustEnter = "your choice as number";
        int parsed = 1;
        string entered = UserMustType(whatUserMustEnter, true);
        if (entered == null)
        {
            return int.MinValue;
        }

        if (int.TryParse(entered, out parsed))
        {
            if (parsed <= max)
            {
                return parsed;
            }
        }

        return UserMustTypeNumber(whatUserMustEnter, max);
    }


public static void OperationWasStopped()
    {
        ConsoleTemplateLogger.Instance.OperationWasStopped();
    }



/// <summary>
    /// First I must ask which is always from console - must prepare user to load data to clipboard. 
    /// </summary>
    /// <param name="format"></param>
    /// <param name="textFormat"></param>
    public static string LoadFromClipboardOrConsoleInFormat(string format, TextFormatData textFormat)
    {
        string s = null;

        if (!CmdApp.loadFromClipboard)
        {
            s = CL.UserMustTypeInFormat(format, textFormat);
        }
        else
        {
            s = ClipboardHelper.GetText();
        }

        return s;
    }

/// <summary>
    /// Will ask before getting data
    /// First I must ask which is always from console - must prepare user to load data to clipboard. 
    /// </summary>
    /// <param name="what"></param>
    public static string LoadFromClipboardOrConsole(string what)
    {
        string imageFile = @"";

        if (!CmdApp.loadFromClipboard)
        {
            imageFile = CL.UserMustType(what);
        }
        else
        {
            CL.AskForEnterWrite(what, true);
            CL.WriteLine(sess.i18n(XlfKeys.PressEnterWhenDataWillBeInClipboard));
            Console.ReadLine();
            imageFile = ClipboardHelper.GetText();
        }

        return imageFile;
    }

    private static void AskForEnterWrite(string what, bool v)
    {
        CL.WriteLine(AskForEnter( what, v));
    }




    /// <summary>
    /// Return null when user force stop 
    /// </summary>
    /// <param name = "what"></param>
    /// <param name = "textFormat"></param>
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
}