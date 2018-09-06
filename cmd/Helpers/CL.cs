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

public static class CL 
{
    readonly static string charOfHeader = "*";

    #region base
    static CL()
    {
    }
    #endregion

    /// <summary>
    /// Return null when user force stop 
    /// </summary>
    /// <param name="what"></param>
    /// <param name="textFormat"></param>
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

    #region User must type
    #region Main

    #region Text
    /// <summary>
    /// 
    /// </summary>
    /// <param name="what"></param>
    /// <returns></returns>
    public static string UserMustType(string what)
    {
        return UserMustType(what, true);
    }

    /// <summary>
    /// Return null when user force stop 
    /// A2 are acceptable chars. Can be null/empty for anything 
    /// </summary>
    /// <param name="whatOrTextWithoutEndingDot"></param>
    /// <param name="append"></param>
    /// <returns></returns>
    static string UserMustType(string whatOrTextWithoutEndingDot, bool append, params string[] acceptableTyping)
    {
        string z = "";
        if (append)
        {
            whatOrTextWithoutEndingDot = "Enter " + whatOrTextWithoutEndingDot + "";
        }
        whatOrTextWithoutEndingDot += ". For loading from clipboard leave empty input. For exit press esc.";
        Console.WriteLine();
        Console.WriteLine(whatOrTextWithoutEndingDot + ": ");
        StringBuilder sb = new StringBuilder();
        int zad = 0;
        
        while (true)
        {
            zad = (int) Console.ReadKey().KeyChar;

            if (zad == 27)
            {
                z = null;
                break;
            }
            else if(zad == 13)
            {
                if (acceptableTyping != null && acceptableTyping.Length != 0)
                {
                    if (SH.EqualsOneOfThis(sb.ToString(), acceptableTyping))
                    {
                        z = sb.ToString();
                        break;
                    }
                }
                string ulozit = sb.ToString().Trim();
                if (ulozit != "")
                {
                    ulozit = ulozit.Replace("\b", "").Trim();
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
            TypedConsoleLogger.Instance.Information("App loaded from clipboard : " + z);
        }
        return z;
    }
    #endregion

    #region Numbers
    /// <summary>
    /// Return int.MinValue when user force stop operation
    /// </summary>
    /// <param name="what"></param>
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
    /// <param name="vyzva"></param>
    /// <returns></returns>
    private static int UserMustTypeNumber( int max)
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
    /// </summary>
    /// <param name="what"></param>
    /// <param name="max"></param>
    /// <returns></returns>
    private static int UserMustTypeNumber(string what, int max)
    {
        int parsed = 1;
        string entered = UserMustType(what, false, CA.ToListString(BT.GetNumberedListFromTo( 0, max)).ToArray());
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
    #endregion

    #region YesNo
    /// <summary>
    /// Pokud uz. zada A,GT, JF.
    /// </summary>
    /// <param name="text"></param>
    /// <returns></returns>
    public static bool UserMustTypeYesNo(string text)
    {
        string entered = UserMustType(text + " (Yes/No) ", false);
        char znak = entered[0];
        if (char.ToLower(entered[0]) == 'y')
        {
            return true;
        }
        return false;
    }

    /// <summary>
    /// Ask user whether want to continue
    /// </summary>
    /// <param name="text"></param>
    /// <returns></returns>
    public static DialogResult DoYouWantToContinue(string text)
    {
        text = "Do you want to continue?";

        TypedConsoleLogger.Instance.Warning(text);
        bool z = UserMustTypeYesNo(text);
        if (z)
        {
            return DialogResult.Yes;
        }
        return DialogResult.No;
    }
    #endregion

    #endregion


    #endregion

    #region Dalsi vyskyty
    /// <summary>
    /// 
    /// </summary>
    /// <param name="appeal"></param>
    public static void Appeal(string appeal)
    {
        ConsoleLogger.ChangeColorOfConsoleAndWrite(TypeOfMessage.Appeal,appeal);
    }

    /// <summary>
    /// Print 
    /// </summary>
    /// <param name="appeal"></param>
    public static void AppealEnter(string appeal)
    {
        Appeal(appeal + ". Then press enter.");
        Console.ReadLine();
    }

    /// <summary>
    /// Return full path of selected file
    /// or null when operation will be stopped
    /// </summary>
    /// <param name="folder"></param>
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

    /// <summary>
    /// Return index of selected action
    /// Or int.MinValue when user force stop operation
    /// </summary>
    /// <param name="hodnoty"></param>
    /// <param name="what"></param>
    /// <returns></returns>
    public static int SelectFromVariants(List<string> variants, string what)
    {
        Console.WriteLine();
        for (int i = 0; i < variants.Count; i++)
        {
            Console.WriteLine("[" + i + "]" + "    " + variants[i]);
        }
        
        return UserMustTypeNumber(what, variants.Count - 1);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="actions"></param>
    /// <param name="vyzva"></param>
    public static void SelectFromVariants(Dictionary<string, EmptyHandler> actions)
    {
        #region Print on console avialable operations
        string appeal = "Select action:";
        int i = 0;
        foreach (KeyValuePair<string, EmptyHandler> kvp in actions)
        {
            Console.WriteLine("[" + i + "]" + "    " + kvp.Key);
            i++;
        }
        #endregion

        #region Find selected
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
        #endregion

        #region Run
        actions[operation].Invoke();
        #endregion
    }
    #endregion

    #region Actions
    /// <summary>
    /// Let user select action and run with A2 arg
    /// </summary>
    /// <param name="akce"></param>
    public static void PerformAction(Dictionary<string, EventHandler> actions, object sender)
    {
        var listOfActions = NamesOfActions(actions);
        int selected = SelectFromVariants(listOfActions, "Select action to proceed:");
        string ind = listOfActions[selected];
        EventHandler eh = actions[ind];
        eh.Invoke(sender, EventArgs.Empty);
    }

    /// <summary>
    /// Return names of actions passed from keys
    /// </summary>
    /// <param name="actions"></param>
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
    #endregion

    #region Print dynamic text
    public static void WriteLineFormat(string text, params object[] p)
    {
        Console.WriteLine();
        Console.WriteLine(string.Format(text, p));
    }

    /// <summary>
    /// Return printed text
    /// </summary>
    /// <param name="text"></param>
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
    #endregion

    #region Print static text
    public static void OperationWasStopped()
    {
        ConsoleTemplateLogger.Instance.OperationWasStopped();
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
    #endregion
}

