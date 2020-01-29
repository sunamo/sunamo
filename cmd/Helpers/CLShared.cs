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
    

    public static void PerformAction(Dictionary<string, VoidVoid> actions)
    {
        List<string> listOfActions = actions.Keys.ToList();
        PerformAction(actions, listOfActions);
    }

    private static void PerformAction(Dictionary<string, VoidVoid> actions, List<string> listOfActions)
    {
        int selected = SelectFromVariants(listOfActions, RLData.en["SelectActionToProceed"]);
        if (selected != -1)
        {
            string ind = listOfActions[selected];
            var eh = actions[ind];
            eh.Invoke();
        }
    }

    /// <summary>
    /// Let user select action and run with A2 arg
    /// </summary>
    /// <param name = "akce"></param>
    public static void PerformAction(Dictionary<string, EventHandler> actions, object sender)
    {
        var listOfActions = NamesOfActions(actions);
        int selected = SelectFromVariants(listOfActions, RLData.en["SelectActionToProceed"] + ":");
        string ind = listOfActions[selected];
        EventHandler eh = actions[ind];

        if (sender == null)
        {
            sender = selected;
        }

        eh.Invoke(sender, EventArgs.Empty);
    }




/// <summary>
    /// Pokud uz. zada Y,GT.
    /// When N, return false.
    /// When -1, return null
    /// </summary>
    /// <param name = "text"></param>
    /// <returns></returns>
    public static bool? UserMustTypeYesNo(string text)
    {
        string entered = UserMustType(text + " (" + "Yes/No" + ") ", false);
        // was pressed esc etc.
        if (entered == null)
        {
            return false;
        }

        if (entered == "-1")
        {
            return null;
        }

        char znak = entered[0];
        if (char.ToLower(entered[0]) == 'y')
        {
            return true;
        }

        return false;
    }

/// <summary>
    /// Return names of actions passed from keys
    /// </summary>
    /// <param name = "actions"></param>
    /// <returns></returns>
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
    /// A2 without ending :
    /// Return index of selected action
    /// Or int.MinValue when user force stop operation
    /// </summary>
    /// <param name = "hodnoty"></param>
    /// <param name = "what"></param>
    /// <returns></returns>
    public static int SelectFromVariants(List<string> variants, string what)
    {
        Console.WriteLine();
        for (int i = 0; i < variants.Count; i++)
        {
            Console.WriteLine(AllStrings.lsf + i + AllStrings.rsf + "    " + variants[i]);
        }

        return UserMustTypeNumber(what, variants.Count - 1);
    }

/// <summary>
    /// 
    /// </summary>
    /// <param name = "actions"></param>
    /// <param name = "vyzva"></param>
    public static void SelectFromVariants(Dictionary<string, EmptyHandler> actions)
    {
        string appeal = RLData.en["SelectAction"] + ":";
        int i = 0;
        foreach (KeyValuePair<string, EmptyHandler> kvp in actions)
        {
            Console.WriteLine(AllStrings.lsf + i + AllStrings.rsf + "    " + kvp.Key);
            i++;
        }

        int entered = UserMustTypeNumber(appeal, actions.Count - 1);
        if (entered == -1)
        {
            OperationWasStopped();
            return;
        }

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

        actions[operation].Invoke();
    }

/// <summary>
    /// if fail, return empty string.
    /// </summary>
    /// <param name = "what"></param>
    /// <returns></returns>
    public static string UserMustType(string what)
    {
        return UserMustType(what, true);
    }
/// <summary>
    /// if fail, return empty string.
    /// In A1 not end with :
    /// Return null when user force stop 
    /// A2 are acceptable chars. Can be null/empty for anything 
    /// </summary>
    /// <param name = "whatOrTextWithoutEndingDot"></param>
    /// <param name = "append"></param>
    /// <returns></returns>
    static string UserMustType(string whatOrTextWithoutEndingDot, bool append, params string[] acceptableTyping)
    {
        string z = "";
        if (append)
        {
            whatOrTextWithoutEndingDot = RLData.en[RLData.en["Enter"]] + " " + whatOrTextWithoutEndingDot + "";
        }

        whatOrTextWithoutEndingDot += ". " + RLData.en["ForExitEnter"] + " -1" + ".";
        Console.WriteLine();
        Console.WriteLine(whatOrTextWithoutEndingDot);
        StringBuilder sb = new StringBuilder();
        int zad = 0;
        while (true)
        {
            zad = (int)Console.ReadKey().KeyChar;
            if (zad == 8)
            {
                sb.Remove(sb.Length - 1, 1);
                // not delete visually, only move cursor about two back
                //Console.Write(AllChars.bs2);
                ClearBehindLeftCursor(-1);
            }
            else if (zad == 27)
            {
                z = null;
                break;
            }
            else if (zad == 13)
            {
                if (acceptableTyping != null && acceptableTyping.Length != 0)
                {
                    if (SH.EqualsOneOfThis(sb.ToString(), acceptableTyping))
                    {
                        z = sb.ToString();
                        break;
                    }
                }

                string ulozit = sb.ToString();
                if (ulozit != "")
                {
                    /// Cant call trim or replace \b (any whitespace character), due to situation when insert "/// " for insert xml comments
                    //ulozit = ulozit.Replace("\b", "");
                    z = ulozit;
                    break;
                }
                else
                {
                    sb = new StringBuilder();
                }
            }
            else
            {
                sb.Append((char)zad);
            }
        }

        if (z == string.Empty)
        {
            z = ClipboardHelper.GetText();
            TypedConsoleLogger.Instance.Information(RLData.en["AppLoadedFromClipboard"] + " " + ": " + z);
        }

        return z;
    }

/// <summary>
    /// Return int.MinValue when user force stop operation
    /// </summary>
    /// <param name = "what"></param>
    /// <returns></returns>
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
    /// <returns></returns>
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
/// <summary>
    /// Return int.MinValue when user force stop operation
    /// A1 without ending :
    /// </summary>
    /// <param name = "what"></param>
    /// <param name = "max"></param>
    /// <returns></returns>
    private static int UserMustTypeNumber(string what, int max)
    {
        int parsed = 1;
        string entered = UserMustType(what, false, CA.ToListString(BTS.GetNumberedListFromTo(0, max)).ToArray());
        if (what == null)
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

        return UserMustTypeNumber(what, max);
    }

public static void OperationWasStopped()
    {
        ConsoleTemplateLogger.Instance.OperationWasStopped();
    }

/// <summary>
    /// Is A1 is negative => chars to remove
    /// </summary>
    /// <param name = "leftCursorAdd"></param>
    public static void ClearBehindLeftCursor(int leftCursorAdd)
    {
        int currentLineCursor = Console.CursorTop;
        int leftCursor = Console.CursorLeft + leftCursorAdd + 1;
        Console.SetCursorPosition(leftCursor, Console.CursorTop);
        Console.Write(new string(AllChars.space, Console.WindowWidth + leftCursorAdd));
        Console.SetCursorPosition(leftCursor, currentLineCursor);
    }
}