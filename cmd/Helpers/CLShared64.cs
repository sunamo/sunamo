using cmd.Essential;
using sunamo.Essential;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static partial class CL
{
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

    public static bool perform = true;
    static string s = null;
    static StringBuilder sbToClear = new StringBuilder();
    static StringBuilder sbToWrite = new StringBuilder();
    public static void WriteLine()
    {
        Console.WriteLine("");
        Console.WriteLine();
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
            if (ThisApp.Name != "AllProjectsSearch")
            {
                loadFromClipboard = CL.UserMustTypeYesNo(sess.i18n(XlfKeys.DoYouWantLoadDataOnlyFromClipboard) + " " + sess.i18n(XlfKeys.MultiLinesTextCanBeLoadedOnlyFromClipboardBecauseConsoleAppRecognizeEndingWhitespacesLikeEnter));
            }

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


    public static void PerformAction(Dictionary<string, VoidVoid> actions)
    {
        List<string> listOfActions = actions.Keys.ToList();
        PerformAction(actions, listOfActions);
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

        z = SH.ConvertTypedWhitespaceToString(z.Trim(AllChars.st));

        if (zadBefore != 32)
        {
            z = z.Trim();
        }

        return z;
    }

    public static string UserMustTypeMultiLine(string v)
    {
        string line = null;
        TypedConsoleLogger.Instance.Information( AskForEnter(v, true));
        StringBuilder sb = new StringBuilder();
        //string lastAdd = null;
        while ((line = Console.ReadLine()) != null)
        {
            if (line == "-1")
            {
                break;
            }
            sb.AppendLine(line);
            //lastAdd = line;
        }
        //sb.AppendLine(line);
        var s2 = sb.ToString();
        return s2;
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

    private static void PerformAction(Dictionary<string, VoidVoid> actions, List<string> listOfActions)
    {
        int selected = SelectFromVariants(listOfActions, "Select action to proceed:");
        if (selected != -1)
        {
            string ind = listOfActions[selected];
            var eh = actions[ind];
            eh.Invoke();
        }
    }

    public static void WriteLine(string v)
    {
        Console.WriteLine(v);
    }

    public static void ClearCurrentConsoleLine()
    {
        Console.SetCursorPosition(0, Console.CursorTop - 1);
        int currentLineCursor = Console.CursorTop;
        Console.SetCursorPosition(0, Console.CursorTop);
        Console.Write(new string(' ', Console.WindowWidth));
        Console.SetCursorPosition(0, currentLineCursor);
    }

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
}
