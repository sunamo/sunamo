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
    public static bool perform = true;

    public static void ClearCurrentConsoleLine()
    {
        Console.SetCursorPosition(0, Console.CursorTop - 1);
        int currentLineCursor = Console.CursorTop;
        Console.SetCursorPosition(0, Console.CursorTop);
        Console.Write(new string(' ', Console.WindowWidth));
        Console.SetCursorPosition(0, currentLineCursor);
    }
    public static void PerformAction(Dictionary<string, VoidVoid> actions)
    {
        List<string> listOfActions = actions.Keys.ToList();
        PerformAction(actions, listOfActions);
    }

    static string s = null;
    static StringBuilder sbToClear = new StringBuilder();
    static StringBuilder sbToWrite = new StringBuilder();

    #region Progress bar
    const char _block = '■';
    const string _back = "\b\b\b\b\b\b\b\b\b\b\b\b\b\b\b\b\b";
    static int _backL = 0;
    const string _twirl = "-\\|/";

    /// <summary>
    /// 1
    /// </summary>
    public static void WriteProgressBarInit()
    {
        _backL = _back.Length;
    }

    /// <summary>
    /// 2
    /// </summary>
    /// <param name="percent"></param>
    /// <param name="a"></param>
    public static void WriteProgressBar(double percent, WriteProgressBarArgs a = null)
    {
        WriteProgressBar((int)percent, a);
    }

    /// <summary>
    /// 3
    /// Usage:
    /// WriteProgressBar(0);
    /// WriteProgressBar(i, true);
    /// </summary>
    /// <param name="percent"></param>
    /// <param name="update"></param>
    public static void WriteProgressBar(int percent, WriteProgressBarArgs a = null)
    {
        if (a == null)
        {
            a = WriteProgressBarArgs.Default;
        }

        if (a.update)
        {
            sbToClear.Clear();

            //sbToClear.Append( string.Empty.PadRight(s.Length, '\b'));

            sbToClear.Append(_back);
            sbToClear.Append(string.Empty.PadRight(s.Length - _backL, '\b'));

            var ts = sbToClear.ToString();

            Console.Write(ts);
        }

        Console.Write("[");
        var p = (int)((percent / 10f) + .5f);
        for (var i = 0; i < 10; ++i)
        {
            if (i >= p)
                Console.Write(' ');
            else
                Console.Write(_block);
        }

        if (a.writePieces)
        {
            s = "] {0,3:##0}%" + $" {a.actual} / {a.overall}";
        }
        else
        {
            s = "] {0,3:##0}%";
        }

        string fr = string.Format(s, percent);

        Console.Write(fr);
    }

    /// <summary>
    /// 4
    /// </summary>
    public static void WriteProgressBarEnd()
    {
        CL.WriteProgressBar(100, new WriteProgressBarArgs(true));
        CL.WriteLine();
    }
    #endregion

    private static void PerformAction(Dictionary<string, VoidVoid> actions, List<string> listOfActions)
    {
        int selected = SelectFromVariants(listOfActions, sess.i18n(XlfKeys.SelectActionToProceed));
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
    /// Pokud uz. zada Y,GT.
    /// When N, return false.
    /// When -1, return null
    /// </summary>
    /// <param name = "text"></param>
    public static bool? UserMustTypeYesNo(string text)
    {
        string entered = UserMustType(text + " (Yes/No) ", false);
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
        if (char.ToLower(entered[0]) == 'y' || znak == '1')
        {
            return true;
        }

        return false;
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
    /// A2 without ending :
    /// Return index of selected action
    /// Or int.MinValue when user force stop operation
    /// </summary>
    /// <param name = "hodnoty"></param>
    /// <param name = "what"></param>
    public static int SelectFromVariants(List<string> variants, string what)
    {
        CL.WriteLine();
        for (int i = 0; i < variants.Count; i++)
        {
            CL.WriteLine(AllStrings.lsqb + i + AllStrings.rsqb + "  " + variants[i]);
        }

        return UserMustTypeNumber(what, variants.Count - 1);
    }

    public static void WriteLine()
    {
        Console.WriteLine();
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
    /// if fail, return empty string.
    /// Cant load multi line
    /// Use Load
    /// </summary>
    /// <param name = "what"></param>
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
    static string UserMustType(string whatOrTextWithoutEndingDot, bool append, params string[] acceptableTyping)
    {
        string z = "";
        whatOrTextWithoutEndingDot = AskForEnter(whatOrTextWithoutEndingDot, append);
        CL.WriteLine();
        CL.WriteLine(whatOrTextWithoutEndingDot);
        StringBuilder sb = new StringBuilder();
        int zadBefore = 0;
        int zad = 0;
        while (true)
        {
            zadBefore = zad;
            zad = (int)Console.ReadKey().KeyChar;
            if (zad == 8)
            {
                if (sb.Length > 0)
                {
                    sb.Remove(sb.Length - 1, 1);
                    // not delete visually, only move cursor about two back
                    //Console.Write(AllChars.bs2);
                    ClearBehindLeftCursor(-1);
                }
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
            TypedConsoleLogger.Instance.Information(sess.i18n(XlfKeys.AppLoadedFromClipboard) + " : " + z);
        }

        if (zadBefore != 32)
        {
            z = z.Trim();
        }

        z = SH. ConvertTypedWhitespaceToString( z.Trim(AllChars.st));

        if (zadBefore != 32)
        {
            z = z.Trim();
        }

        return z;
    }

    private static string AskForEnter(string whatOrTextWithoutEndingDot, bool append)
    {
        if (append)
        {
            whatOrTextWithoutEndingDot = sess.i18n(XlfKeys.Enter) + " " + whatOrTextWithoutEndingDot + "";
        }

        whatOrTextWithoutEndingDot += ". " + sess.i18n(XlfKeys.ForExitEnter) + " -1.";
        return whatOrTextWithoutEndingDot;
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
/// <summary>
    /// Return int.MinValue when user force stop operation
    /// A1 without ending :
    /// </summary>
    /// <param name = "what"></param>
    /// <param name = "max"></param>
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
            CL.AskForEnter(what, true);
            CL.WriteLine(sess.i18n(XlfKeys.PressEnterWhenDataWillBeInClipboard));
            Console.ReadLine();
            imageFile = ClipboardHelper.GetText();
        }

        return imageFile;
    }

    public static void WriteLine(string v)
    {
        Console.WriteLine(v);
    }

    /// <summary>
    /// Return None if !A1
    /// If allActions will be null, will not automatically run action
    /// </summary>
    /// <param name="askUser"></param>
    /// <param name="AddGroupOfActions"></param>
    /// <param name="allActions"></param>
    public static string AskUser(bool askUser, Func<Dictionary<string, VoidVoid>> AddGroupOfActions, Dictionary<string, VoidVoid> allActions, string mode)
    {
        if (askUser)
        {
            //repair, I have 168-0-143-16 but always bad format
            //168-0-143-16

            bool? loadFromClipboard = false;
            loadFromClipboard = CL.UserMustTypeYesNo(sess.i18n(XlfKeys.DoYouWantLoadDataOnlyFromClipboard) + " " + sess.i18n(XlfKeys.MultiLinesTextCanBeLoadedOnlyFromClipboardBecauseConsoleAppRecognizeEndingWhitespacesLikeEnter));

            CmdApp.loadFromClipboard = loadFromClipboard.Value;

            //CL.WriteLine(loadFromClipboard.Value);

            if (loadFromClipboard.HasValue)
            {
                var whatUserNeed = "format";
                whatUserNeed = CL.UserMustType("you need or enter -1 for select from all groups");

                Dictionary<string, VoidVoid> groupsOfActions = AddGroupOfActions();

                if (whatUserNeed == "-1")
                {
                    CL.PerformAction(groupsOfActions);
                }
                else
                {
                    perform = false;
                    AddGroupOfActions();

                    foreach (var item in groupsOfActions)
                    {
                        item.Value.Invoke();
                    }

                    Dictionary<string, VoidVoid> potentiallyValid = new Dictionary<string, VoidVoid>();
                    foreach (var item in allActions)
                    {

                        if (SH.Contains(item.Key, whatUserNeed, SearchStrategy.AnySpaces, false))
                        {
                            potentiallyValid.Add(item.Key, item.Value);
                        }
                    }

                    if (potentiallyValid.Count == 0)
                    {
                        ThisApp.SetStatus(TypeOfMessage.Information, sess.i18n(XlfKeys.NoActionWasFound));
                    }
                    else
                    {
                        CL.PerformAction(potentiallyValid);
                    }


                }
            }
            return mode;
        }
        else
        {
            return mode;
        }
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