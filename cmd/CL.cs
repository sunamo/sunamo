using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.IO;
using sunamo;
using cmd;

public static class CL
{ 
    static Type type = typeof(CL);

    readonly static string znakNadpisu = "*";
    public static int zad = 0;

    #region ctor
    static CL()
    {
    }
    #endregion

    #region TypeOfMessage
    public static void Success(string text, params string[] p)
    {
        ChangeColorOfConsoleAndWrite(string.Format(text, p), TypeOfMessage.Success);
    }

    public static void Error(string text, params string[] p)
    {
        ChangeColorOfConsoleAndWrite(string.Format( text, p), TypeOfMessage.Error);
    }
    public  static void Warning(string text, params string[] p)
    {
        ChangeColorOfConsoleAndWrite(string.Format(text, p), TypeOfMessage.Warning);
    }
    public static void Information(string text, params string[] p)
    {
        ChangeColorOfConsoleAndWrite(string.Format(text, p), TypeOfMessage.Information);
    }
    #endregion

    #region Change text color
    static void ChangeColorOfConsoleAndWrite(string text, TypeOfMessage tz)
    {
        SetColorOfConsole(tz);
        Console.WriteLine();Console.WriteLine(text);
        SetColorOfConsole(TypeOfMessage.Ordinal);
    }

    private static void SetColorOfConsole(TypeOfMessage tz)
    {
        ConsoleColor bk = ConsoleColor.White;

        switch (tz)
        {
            case TypeOfMessage.Error:
                bk = ConsoleColor.Red;
                break;
            case TypeOfMessage.Warning:
                bk = ConsoleColor.Yellow;
                break;
            case TypeOfMessage.Information:

            case TypeOfMessage.Ordinal:
                bk = ConsoleColor.White;
                break;
            case TypeOfMessage.Appeal:
                bk = ConsoleColor.Magenta;
                break;
            case TypeOfMessage.Success:
                bk = ConsoleColor.Green;
                break;
            default:
                ThrowExceptions.NotImplementedCase(type, "SetColorOfConsole");
                break;
        }
        if (bk != ConsoleColor.Black)
        {
            Console.ForegroundColor = bk;
        }
        else
        {
            Console.ResetColor();
        }
    }
    #endregion

    #region UserMustType

    #region Text
    /// <summary>
    /// Do A1 zadejte text mezi "Zadejte " a textem ": "
    /// </summary>
    /// <param name="text"></param>
    /// <returns></returns>
    public static string UserMustType(string text)
    {
        return UserMustType(text, null);
    }

    /// <summary>
    /// Get null if user cancel
    /// </summary>
    /// <param name="whatToEnter"></param>
    /// <param name="acceptableTyping"></param>
    /// <returns></returns>
    static string UserMustType(string whatToEnter, params string[] acceptableTyping)
    {
        string z = null;
        
            whatToEnter = "Enter " + whatToEnter + ".";
        
        Console.WriteLine();
        Console.WriteLine(whatToEnter + ": ");
        StringBuilder sb = new StringBuilder();
        
        while (true)
        {

            zad = (int) Console.ReadKey().KeyChar;

            if (zad == 27)
            {
                
                break;
            }
            else if(zad == 13)
            {
                if (acceptableTyping != null)
                {
                    if (acceptableTyping.Length != 0)
                    {
                        if (SH.EqualsOneOfThis(sb.ToString(), acceptableTyping))
                        {
                            z = sb.ToString();
                            break;
                        }
                    }
                }
                string ulozit = sb.ToString().Trim();
                if (ulozit != "")
                {
                    ulozit = ulozit.Replace("\b", "").Trim();
                    //zad =  Convert.ToChar(ulozit);
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
                
                //zad = Console.Read();
            }
        }
        return z;
    }
    #endregion

    #region Number
    /// <summary>
    /// 
    /// </summary>
    /// <param name="whatEnter"></param>
    /// <returns></returns>
    public static int UserMustTypeNumber(string whatEnter, int max, int min)
    {
        int selectedChoice = 1;
        string str = null;
        bool jednaSeOCislo = false;
        str = UserMustType(whatEnter);
        jednaSeOCislo = int.TryParse(str, out selectedChoice);
        while (!jednaSeOCislo)
        {
            str = UserMustType(whatEnter);
            jednaSeOCislo = int.TryParse(str, out selectedChoice);
            if (selectedChoice <= max && selectedChoice >= min)
            {
                break;
            }

        }
        return selectedChoice;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="vyzva"></param>
    /// <returns></returns>
    private static int UserMustTypeNumber( int max)
    {
        int selectedChoice = 1;
        string str = UserMustType("value of your choice");
        if (int.TryParse(str, out selectedChoice))
        {
            if (selectedChoice <= max)
            {
                return selectedChoice;
            }
        }
        return UserMustTypeNumber("value of your choice", max);
    }

    private static int UserMustTypeNumber(string whatEnter, int max)
    {
        int selectedChoice = 1;
        string str = UserMustType(whatEnter, CA.ToListString(BT.GetNumberedListFromTo( 0, max)).ToArray());
        if (int.TryParse(str, out selectedChoice)) 
        {
            if (selectedChoice <= max)
            {
                return selectedChoice;
            }
        }
        return UserMustTypeNumber(whatEnter, max);
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
        string zadani = UserMustType(text + " (Yes/No) ");
        char znak = zadani[0];
        if (zadani[0] == 'y')
        {
            return true;
        }
        return false;
    }

    #endregion

    #region InFormat
    public static string UserMustTypeInFormat(string whatToEnter, TextFormatData textFormatData)
    {
        string entered = null;
        while (true)
        {
            entered = UserMustType( whatToEnter);
            if (SH.HasTextRightFormat(entered, textFormatData))
            {
                ThankYou();
                break;
            }
            else
            {
                TryAgain();
            }
        }

        return entered;
    } 
    #endregion

    #endregion

    #region Appeals
    public static void Appeal(string vyzva)
    {
        ChangeColorOfConsoleAndWrite(vyzva, TypeOfMessage.Appeal);
    }

    public static void AppealEnter(string vyzva)
    {
        Appeal(vyzva + ". Then press enter.");
        Console.ReadLine();
    }

    /// <summary>
    /// Write warning to user, which confirm this with Yes/No
    /// </summary>
    /// <param name="text"></param>
    /// <returns></returns>
    public static DialogResult DoYouWantToContinue(string text)
    {
        text = "Do you want to continue?";
        
        Warning(text);
        bool z = UserMustTypeYesNo(text);
        if (z)
        {
            return DialogResult.Yes;
        }
        return DialogResult.No;
    }
    #endregion

    #region List
    /// <summary>
    /// Display files from A1 and return path to selected
    /// </summary>
    /// <param name="slozka"></param>
    /// <returns></returns>
    public static string SelectFile(string slozka)
    {
        string[] soubory = Directory.GetFiles(slozka);
        string vystup = "";
        vystup = soubory[SelectFromVariants(soubory, "Select file to open")];

        return vystup;
    }

    /// <summary>
    /// Return index of selected value
    /// </summary>
    /// <param name="soubory"></param>
    /// <param name="vyzva"></param>
    /// <returns></returns>
    public static int SelectFromVariants(string[] soubory, string vyzva)
    {
        Console.WriteLine();
        for (int i = 0; i < soubory.Length; i++)
        {
            Console.WriteLine("[" + i + "]" + "    " + soubory[i]);
        }
        return UserMustTypeNumber(vyzva, soubory.Length - 1);
    }
    #endregion

    #region Actions
    public static void PerformAction(Dictionary<string, EmptyHandler> soubory)
    {
        #region VYpisu na konzoli vl. metodou typy operaci
        string vyzva = "Please select:";
        int i = 0;
        foreach (KeyValuePair<string, EmptyHandler> kvp in soubory)
        {
            Console.WriteLine(); Console.WriteLine("[" + i + "]" + "    " + kvp.Key);
            i++;
        }
        #endregion

        #region Zjistim si nazev polozky kterou uz zadal
        int zadano = UserMustTypeNumber(vyzva, soubory.Count - 1);
        string operace = null;
        foreach (string var in soubory.Keys)
        {
            if (i == zadano)
            {
                operace = var;
                break;
            }
            i++;
        }
        #endregion

        #region Vyvolam M s nul. argumentem.
        soubory[operace].Invoke();
        #endregion
    }

    /// <summary>
    /// Print variables and after selecting do action with A2 argument 
    /// </summary>
    /// <param name="akce"></param>
    public  static void PerformAction(Dictionary<string, EventHandler> akce, object sender)
    {
        string[] ss = NamesOfActions(akce);
        int vybrano = SelectFromVariants(ss, "Please select:");
        //Program.VytvoritScreenshot();
        string ind = ss[vybrano];
        EventHandler eh = akce[ind];
        eh.Invoke(sender, EventArgs.Empty);
    }

    private static string[] NamesOfActions(Dictionary<string, EventHandler> akce)
    {
        List<string> ss = new List<string>();
        foreach (KeyValuePair<string, EventHandler> var in akce)
        {
            ss.Add(var.Key);
        }
        return ss.ToArray();
    }
    #endregion

    public static void WriteLineFormat(string text, params object[] p)
    {
        Console.WriteLine();
        Console.WriteLine(string.Format(text, p));
    }

    #region Only printing
    /// <summary>
    /// Write header A1 into console
    /// </summary>
    /// <param name="text"></param>
    /// <returns></returns>
    public static string StartRunTime(string text)
    {
        int delkaTextu = text.Length;
        string hvezdicky = "";
        hvezdicky = new string(znakNadpisu[0], delkaTextu);
        //hvezdicky.PadLeft(delkaTextu, znakNadpisu[0]);
        StringBuilder sb = new StringBuilder();
        sb.AppendLine(hvezdicky);
        sb.AppendLine(text);
        sb.AppendLine(hvezdicky);
        Console.WriteLine(); Console.WriteLine(hvezdicky);
        Information(text);
        Console.WriteLine(); Console.WriteLine(hvezdicky);
        return sb.ToString();
    }

    public static void EndRunTime()
    {
        Information("Thank you for using my app. Press enter to app will be terminated.");
        Console.ReadLine();
    }

    /// <summary>
    /// Just print "No input data found"
    /// </summary>
    public static void NoData()
    {
        Warning("No input data found");
    }

    public static void ThankYou()
    {
        Success("Thank you");
    } 

    public static void TryAgain()
    {
        Appeal("Try again");
    }
    #endregion
}


