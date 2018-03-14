using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

using System.IO;
using System.Windows.Forms;
using sunamo;

public static class CL //:  IZpravaUzivatelovi
{
    readonly static string znakNadpisu = "*";

    #region base
    static CL()
    {
    }
    #endregion

    #region IZpravaUzivatelovi Members
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

    #region Zmena barvy konzoly
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
                throw new Exception("Neinplementovana vetev");
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

    #region UzivatelMusiZadat
    #region Main
    #region Text
    /// <summary>
    /// Do A1 zadejte text mezi "Zadejte " a textem ": "
    /// </summary>
    /// <param name="text"></param>
    /// <returns></returns>
    public static string UserMustType(string text)
    {
        return UserMustType(text, true);
    }

    public static int zad = 0;

    /// <summary>
    /// A2 zda se m� A1 p�ipojit p�ed A1 "Zadejte " a za text ": "
    /// Pokud !A2, vyp�e se "A1:"
    /// </summary>
    /// <param name="text"></param>
    /// <param name="pridat"></param>
    /// <returns></returns>
    static string UserMustType(string text, bool pridat, params string[] acceptableTyping)
    {
        string z = "";
        if (pridat)
        {
            text = "Zadejte " + text + ". Pro nacteni ze schranky stisknete esc. ";
        }
        Console.WriteLine();
        Console.WriteLine(text + ": ");
        StringBuilder sb = new StringBuilder();
        
        while (true)
        {

            zad = (int) Console.ReadKey().KeyChar;

            if (zad == 27)
            {
                z = Clipboard.GetText();
                break;
            }
            else if(zad == 13)
            {
                if (acceptableTyping.Length != 0)
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

    #region Cislo
    /// <summary>
    /// 
    /// </summary>
    /// <param name="vyzva"></param>
    /// <returns></returns>
    public static int UserMustTypeNumber(string vyzva, int max, int min)
    {
        int monicka = 1;
        string str = null;
        bool jednaSeOCislo = false;
        str = UserMustType(vyzva, false);
        jednaSeOCislo = int.TryParse(str, out monicka);
        while (!jednaSeOCislo)
        {
            str = UserMustType(vyzva, false);
            jednaSeOCislo = int.TryParse(str, out monicka);
            if (monicka <= max && monicka >= min)
            {
                break;
            }

        }
        return monicka;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="vyzva"></param>
    /// <returns></returns>
    private static int UserMustTypeNumber( int max)
    {
        int monicka = 1;
        string str = UserMustType("ciselnou hodnotu Vaseho vyberu", true);
        if (int.TryParse(str, out monicka))
        {
            if (monicka <= max)
            {
                return monicka;
            }
        }
        return UserMustTypeNumber("ciselnou hodnotu Vaseho vyberu", max);
    }

    private static int UserMustTypeNumber(string vyzva, int max)
    {
        int monicka = 1;
        string str = UserMustType(vyzva, false, CA.ToListString(BT.GetNumberedListFromTo( 0, max)).ToArray());
        if (int.TryParse(str, out monicka)) 
        {
            if (monicka <= max)
            {
                return monicka;
            }
        }
        return UserMustTypeNumber(vyzva, max);
    }

    /// <summary>
    /// Vyp�e "Thank you for using my app. Press enter to app will be terminated."
    /// </summary>
    public static void EndRunTime()
    {
        Information("Thank you for using my app. Press enter to app will be terminated.");
        Console.ReadLine();
    }

    /// <summary>
    /// Pouze vyp�e "Az budete mit vstupn� data, spus�te program znovu."
    /// </summary>
    public static void NoData()
    {
        Information("Az budete mit vstupn� data, spus�te program znovu.");
    }
    #endregion

    #region AnoNe
    /// <summary>
    /// Pokud uz. zada A,GT, JF.
    /// </summary>
    /// <param name="text"></param>
    /// <returns></returns>
    public static bool UserMustTypeYesNo(string text)
    {
        string zadani = UserMustType(text + " (Yes/No) ", false);
        char znak = zadani[0];
        if (zadani[0] == 'y')
        {
            return true;
        }
        return false;
    }

    #endregion
    #endregion

    
    #endregion

    #region Dalsi vyskyty
    /// <summary>
    /// 
    /// </summary>
    /// <param name="vyzva"></param>
    public static void Appeal(string vyzva)
    {
        ChangeColorOfConsoleAndWrite(vyzva, TypeOfMessage.Appeal);
    }

    /// <summary>
    /// Zap�e v�zvu za kterou automaticky zap�e ". Po nahrani stisknete enter"
    /// </summary>
    /// <param name="vyzva"></param>
    public static void AppealEnter(string vyzva)
    {
        Appeal(vyzva + ". Po nahrani stisknete enter");
        Console.ReadLine();
    }

    /// <summary>
    /// Z�sk� soubory z A1 nerek a vr�c� celou cestu k n�mu
    /// </summary>
    /// <param name="slozka"></param>
    /// <returns></returns>
    public static string SelectFile(string slozka)
    {
        string[] soubory = Directory.GetFiles(slozka);
        string vystup = "";
        vystup = soubory[SelectFromVariants(soubory, "Vyberte si ktery soubor chcete otevrit")];

        return vystup;
    }

    /// <summary>
    /// Zobrazi ocislovane  moznosti dle A1.
    /// G index te, ktere si uz. vybral.
    /// </summary>
    /// <param name="hodnoty"></param>
    /// <param name="vyzva"></param>
    /// <returns></returns>
    public static int SelectFromVariants(string[] soubory, string vyzva)
    {
        Console.WriteLine();
        for (int i = 0; i < soubory.Length; i++)
        {
            Console.WriteLine("[" + i + "]" + "    " + soubory[i]);
        }
        // TODO: Slo by zde impl. i tu, ktera vraci retezec, ale musela by brat ine parametry
        return UserMustTypeNumber(vyzva, soubory.Length - 1);
    }

    /// <summary>
    /// Zepta se uz. na op a provede ji
    /// Tato metoda se pou��v� pokud nechci metod� kterou budu volat p�ed�vat ��dn� argumenty. 
    /// </summary>
    /// <param name="soubory"></param>
    /// <param name="vyzva"></param>
    public static void SelectFromVariants(Dictionary<string, EmptyHandler> soubory)
    {
        #region VYpisu na konzoli vl. metodou typy operaci
        string vyzva = "Vyberte si mod programu";
        int i = 0;
        foreach (KeyValuePair<string, EmptyHandler> kvp in soubory)
        {
            //KeyValuePair<string, AppDomainInitializer> kvp = soubory[i];
            Console.WriteLine();Console.WriteLine("[" + i + "]" + "    " + kvp.Key);
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
    /// Nap�e nadpis A1 do konzole 
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
        Console.WriteLine();Console.WriteLine(hvezdicky);
        Information(text);
        Console.WriteLine();Console.WriteLine(hvezdicky);
        return sb.ToString();
    }
    #endregion

    #region IZpravaUzivatelovi Members

    /// <summary>
    /// Uzivatelo vypise varovani, ktere potvrdi pomoci Ano/Ne.
    /// </summary>
    /// <param name="text"></param>
    /// <returns></returns>
    public static DialogResult DoYouWantToContinue(string text)
    {
        text = "Prejete si pokracovat?";
        
        Warning(text);
        bool z = UserMustTypeYesNo(text);
        if (z)
        {
            return DialogResult.Yes;
        }
        return DialogResult.No;
    }

    #endregion

    #region Akce
    /// <summary>
    /// ZObrazi na konzoli moznosti a po vyberu uzivatele ji vyvola s arg. A2.
    /// </summary>
    /// <param name="akce"></param>
    public  static void PerformAction(Dictionary<string, EventHandler> akce, object sender)
    {
        string[] ss = NamesOfActions(akce);
        int vybrano = SelectFromVariants(ss, "Akce kterou chcete provest?");
        //Program.VytvoritScreenshot();
        string ind = ss[vybrano];
        EventHandler eh = akce[ind];
        eh.Invoke(sender, EventArgs.Empty);
    }

    /// <summary>
    /// Ulozim nazvy do pole, abych je mohl vypsat v ProvestAkci.
    /// V kl��i jsou n�zvy t�ch metod.
    /// Mus� to b�t EventHandler, proto�e v metod� ProvestAkci se pak metoda vyvol�v� s t�mto.
    /// </summary>
    /// <param name="akce"></param>
    /// <returns></returns>
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
        Console.WriteLine();Console.WriteLine(string.Format(text, p));
    }

    
}


