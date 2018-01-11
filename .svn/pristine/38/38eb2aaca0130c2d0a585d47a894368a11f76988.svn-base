using System;
using System.Collections.Generic;

public class GeneralLayer //: ISpecificLayer
{
    public static Dictionary<string, MSColumnsDB> s = new Dictionary<string, MSColumnsDB>();
    #region MyRegion
    /// <summary>
    /// například když měním tabulky tak že musím přejmenovávat, aby se něco nedělalo a nebyla z toho unhalted exception
    /// </summary>
    public static bool AllTablesOk = true;
    public static bool AllowedRegLogSys
    {
        get
        {
            return AllTablesOk;
        }
    }
    /// <summary>
    /// Zobtrazuje se pouze, když je AllowedRegLogSys false
    /// </summary>
    public static string RegLogSysStatus = "Omlouvám se, ale přihlašovácí a registrační systém právě probíhá aktualizací. Zkuste to prosím znovu zítra nebo za pár hodin."; 
    #endregion

    public static void ClearAndCreateTables()
    {
        #region Tabulky komentářů
        #endregion

        #region MyRegion
        #endregion
    }

    private static void ReplaceHostAtGoVisitorID(params int[] p1)
    {
        foreach (var item in p1)
        {
            MSStoredProceduresI.ci.Update(Tables.GoVisitors, "IDHostName", 0, "ID", item);
        }
    }

    public static void DeleteAndCreateTable(string tn)
    {
        MSStoredProceduresI.ci.DropTableIfExists(tn);
        s[tn].GetSqlCreateTable(tn, true).ExecuteNonQuery();
    }

    /// <summary>
    /// A2 je název tabulky v Layer
    /// </summary>
    /// <param name="nts"></param>
    /// <param name="p"></param>
    public static void DeleteAndCreateTable(string tn, string p)
    {
        MSStoredProceduresI.ci.DropTableIfExists(tn);
        s[p].GetSqlCreateTable(tn, true).ExecuteNonQuery();
    }

    public static void CreateStaticTables()
    {
        
        s.Add(Tables.State, new MSColumnsDB(
            MSSloupecDB.CI(SqlDbType2.TinyInt, "ID", true),
            MSSloupecDB.CI(SqlDbType2.VarChar, "Name(100)", false, true)
            ));
        s.Add(Tables.Region, new MSColumnsDB(
            MSSloupecDB.CI(SqlDbType2.SmallInt, "ID", true),
            MSSloupecDB.CI(SqlDbType2.NVarChar, "Name(100)", false, true)
            ));
        s.Add(Tables.Location, new MSColumnsDB(
            MSSloupecDB.CI(SqlDbType2.SmallInt, "ID", true),
            MSSloupecDB.CI(SqlDbType2.TinyInt, "IDState"),
            MSSloupecDB.CI(SqlDbType2.SmallInt, "IDRegion"),
            MSSloupecDB.CI(SqlDbType2.TinyInt, "SerieDistrict"),
            MSSloupecDB.CI(SqlDbType2.NVarChar, "District(100)")
            ));

        s.Add(Tables.Cities, MSColumnsDB.IDName(25));
        string delkaLogin = "20";
        string delkaEmail = "40";
        string delkaTelNumber = "20";
        string delkaSecQue = "4000";
        string delkaSecAns = "4000";
  
        
        s.Add(Tables.Users, new MSColumnsDB(
            MSSloupecDB.CI(SqlDbType2.Int, "ID", true),
            MSSloupecDB.CI(SqlDbType2.Char, "SessionID(24)", false, false),
            MSSloupecDB.CI(SqlDbType2.VarChar, "Login(" + delkaLogin + ")", false, true),
            MSSloupecDB.CI(SqlDbType2.VarChar, "Email(" + delkaEmail + ")", false, false),
            MSSloupecDB.CI(SqlDbType2.SmallDateTime, "LastSeen", false, false),
            MSSloupecDB.CI(SqlDbType2.NVarChar, "SecQue(" + delkaSecQue + ")", false, false),
            MSSloupecDB.CI(SqlDbType2.NVarChar, "SecAns(" + delkaSecAns + ")", false, false),
            //MSSloupecDB.CI(SqlDbType2.Bit, "CanReset", false, false),
            MSSloupecDB.CI(SqlDbType2.Bit, "Sex", false, false),
            // Pokud bude nastaveno na MinVal, znamená to že uživatel své datum narození nevyplnil
            MSSloupecDB.CI(SqlDbType2.SmallDateTime, "DateBorn", false, false),
            //MSSloupecDB.CI(SqlDbType2.TinyInt, "Age", false, false),
            MSSloupecDB.CI(SqlDbType2.TinyInt, "MailSettings", false, false),
            MSSloupecDB.CI(SqlDbType2.TinyInt, "IDState", true, false),
            MSSloupecDB.CI(SqlDbType2.SmallInt, "IDRegion", true, false),
            MSSloupecDB.CI(SqlDbType2.TinyInt, "IDDistrict", true, false),
            MSSloupecDB.CI(SqlDbType2.Int, "IDCity", true, false),
            // Jedná se o hodnotu výčtu StatusOfUser
            MSSloupecDB.CI(SqlDbType2.TinyInt, "Status", false, false),
            MSSloupecDB.CI(SqlDbType2.Bit, "BlockNewComment"),
            MSSloupecDB.CI(SqlDbType2.VarChar, "FacebookNick(50)"),
            MSSloupecDB.CI(SqlDbType2.VarChar, "TwitterNick(15)"),
            MSSloupecDB.CI(SqlDbType2.VarChar, "GoogleNick(50)"),
            MSSloupecDB.CI(SqlDbType2.SmallInt, "WidthUserPage"),
            // Maximální délka bude 100 000 znaků, musím to ověřovat přímo v kódu
            MSSloupecDB.CI(SqlDbType2.NVarChar, "HtmlAbout(MAX)")
            //MSSloupecDB.CI(SqlDbType2.Text, "Comment")
            ));

        s.Add(Tables.UsersReactivates, new MSColumnsDB(
            // Pokud bude sloupec existovat v tomto sloupci
            MSSloupecDB.CI(SqlDbType2.Int, "IDUsers", false, false),
            // Před uložením musíš zkontrolovat zda kód již v DB není, poté co se už. znovu aktivuje obsah tohoto sloupečku vymažu
            MSSloupecDB.CI(SqlDbType2.VarChar, "Code(24)", false, false),
            // Emaily zde budou samozřejmě malými písmeny tak jako v tabulce Users
            MSSloupecDB.CI(SqlDbType2.VarChar, "Email(40)", false, false),
            // Datum kdy byl email v tomto sloupečku změněn
            MSSloupecDB.CI(SqlDbType2.SmallDateTime, "DateChanged", false, false),
            // Každému povolíme změnu jednoho emailu maximálně 3x
            MSSloupecDB.CI(SqlDbType2.TinyInt, "ChangedTimes", false, false)
            ));

       


        s.Add(Tables.UsersActivates, new MSColumnsDB(
            MSSloupecDB.CI(SqlDbType2.Int, "ID", true),
            // Před uložením kódu vždy zkontroluj, zda kód není již v DB. Posílat se bude pouze kód
            MSSloupecDB.CI(SqlDbType2.Char, "Code(24)", false, true),
            MSSloupecDB.CI(SqlDbType2.VarChar, "Login(" + delkaLogin + ")"),
            MSSloupecDB.CI(SqlDbType2.VarChar, "Olseh(20)"),
            MSSloupecDB.CI(SqlDbType2.VarChar, "Email(" + delkaEmail + ")"),
            MSSloupecDB.CI(SqlDbType2.SmallDateTime, "DeleteOn"),
            MSSloupecDB.CI(SqlDbType2.NVarChar, "SecQue(" + delkaSecQue + ")"),
            MSSloupecDB.CI(SqlDbType2.NVarChar, "SecAns(" + delkaSecAns + ")"),
            //MSSloupecDB.CI(SqlDbType2.Bit, "CanReset"),
            MSSloupecDB.CI(SqlDbType2.Bit, "Sex"),
            MSSloupecDB.CI(SqlDbType2.SmallDateTime, "DateBorn"),
            MSSloupecDB.CI(SqlDbType2.TinyInt, "MailSettings"),
            MSSloupecDB.CI(SqlDbType2.TinyInt, "IDState", true, false),
            MSSloupecDB.CI(SqlDbType2.SmallInt, "IDRegion", true, false),
            MSSloupecDB.CI(SqlDbType2.TinyInt, "IDDistrict", true, false),
            MSSloupecDB.CI(SqlDbType2.Int, "IDCity", true, false)
            ));

        s.Add(Tables.PageName, new MSColumnsDB(true,
            // Zde se to bude zapisovat od int.MinValue
            MSSloupecDB.CI(SqlDbType2.Int, "ID", true),
            MSSloupecDB.CI(SqlDbType2.VarChar, "Name(5120)")
            ));
        // 
        s.Add(Tables.PageArgument, new MSColumnsDB(true,
            // Zde se to bude zapisovat od int.MinValue
            MSSloupecDB.CI(SqlDbType2.Int, "ID", true),
            MSSloupecDB.CI(SqlDbType2.NVarChar, "Arg(4000)")
            ));
        s.Add(Tables.Page, new MSColumnsDB(true,
            MSSloupecDB.CI(SqlDbType2.Int, "ID", true),
            MSSloupecDB.CI(SqlDbType2.Bit, "IsOld"),
            MSSloupecDB.CI(SqlDbType2.Int, "OverallViews"),
            MSSloupecDB.CI(SqlDbType2.Bit, "AllowNewComments")
            ));

        s.Add(Tables.PageNew, new MSColumnsDB(true,
            MSSloupecDB.CI(SqlDbType2.Int, "IDPage"),
            MSSloupecDB.CI(SqlDbType2.TinyInt, "IDTable"),
            MSSloupecDB.CI(SqlDbType2.Int, "IDItem"),
            MSSloupecDB.CI(SqlDbType2.Date, "Day"),
            MSSloupecDB.CI(SqlDbType2.Int, "Views")
            ));

        s.Add(Tables.PageOld, new MSColumnsDB(true,
            MSSloupecDB.CI(SqlDbType2.Int, "IDPage"),
            // IDWeb bude číslo entity v rozmezí 1-255
            MSSloupecDB.CI(SqlDbType2.TinyInt, "IDWeb"),
            // Kontrolovat na to zda nemá více stránek stejné ID by měl HostingManager
            MSSloupecDB.CI(SqlDbType2.Int, "IDPageName", false, false),
            MSSloupecDB.CI(SqlDbType2.Int, "IDPageArg", false, false),
            MSSloupecDB.CI(SqlDbType2.Date, "Day"),
            // Zde se to bude nově zapisovat od int.MinValue
            MSSloupecDB.CI(SqlDbType2.Int, "Views")
            ));
        s.Add(Tables.Favorites, new MSColumnsDB(
            MSSloupecDB.CI(SqlDbType2.Int, "IDPages"),
            MSSloupecDB.CI(SqlDbType2.Int, "IDUsers")
            ));

        s.Add(Tables.CzechNameDays, new MSColumnsDB(
            MSSloupecDB.CI(SqlDbType2.SmallInt, "ID", true),
            // Nejdelší je Cyril a Metoděj
            MSSloupecDB.CI(SqlDbType2.Char, "Name(15)", false, true),
            MSSloupecDB.CI(SqlDbType2.TinyInt, "Day", false, false),
            MSSloupecDB.CI(SqlDbType2.TinyInt, "Month", false, false)
            ));
        s.Add(Tables.TypesOfContacts, new MSColumnsDB(
            MSSloupecDB.CI(SqlDbType2.TinyInt, "ID", true),
            MSSloupecDB.CI(SqlDbType2.Char, "Name(14)", false, true)));

        s.Add(Tables.FileExts, new MSColumnsDB(
            MSSloupecDB.CI(SqlDbType2.SmallInt, "ID", true),
            MSSloupecDB.CI(SqlDbType2.Char, "Ext(4)", false, true)
            ));
        #region Tabulky zkracovače
        s.Add(Tables.Go, new MSColumnsDB(
            // Vždycky si zkontroluji nejvyšší číslo v DB a podle jeho kódu budu dále pracovat
            MSSloupecDB.CI(SqlDbType2.Int, "ID", true),
            MSSloupecDB.CI(SqlDbType2.Int, "IDUsers", false, false),
            MSSloupecDB.CI(SqlDbType2.VarChar, "Code(100)", false, true),
            // Zde budu moci uložit jen 1http nebo 2https ale ne 0Unknown
            MSSloupecDB.CI(SqlDbType2.TinyInt, "Protocol", false, false),
            // Bude se vždy na konec dávat /, aby se předešlo duplicitám
            MSSloupecDB.CI(SqlDbType2.VarChar, "Uri(512)", false, false),
            MSSloupecDB.CI(SqlDbType2.Bit, "Enabled", false, false),
            MSSloupecDB.CI(SqlDbType2.SmallInt, "ViewCount", false, false),
            MSSloupecDB.CI(SqlDbType2.SmallDateTime, "CreatedDT", false, false),
            MSSloupecDB.CI(SqlDbType2.NVarChar, "Comment(512)", false, false)

            ));

        s.Add(Tables.GoVisitors, new MSColumnsDB(
            MSSloupecDB.CI(SqlDbType2.BigInt, "ID", true),
            MSSloupecDB.CI(SqlDbType2.SmallDateTime, "DT", false, false),
            MSSloupecDB.CI(SqlDbType2.Int, "IDGo", false, false),
            MSSloupecDB.CI(SqlDbType2.TinyInt, "IDBrowser", false, false),
            // Hostname pokud uživatel zadá adresu přímo bude 0
            MSSloupecDB.CI(SqlDbType2.BigInt, "IDHostName", false, false),
            // Pokud platforma nebude zadaná nebo bude prázdná, uložím zde 0
            MSSloupecDB.CI(SqlDbType2.TinyInt, "IDPlatform", false, false),
            // Pokud se mi nepodaří zjistit jazyk, uložím zde 0
            MSSloupecDB.CI(SqlDbType2.TinyInt, "IDLang", false, false),
            // Bude 0 v případě že se verzi prohlížeče nepodaří zjistit
            MSSloupecDB.CI(SqlDbType2.TinyInt, "BrowserVersion", false, false)
            ));

        s.Add(Tables.Hostnames, new MSColumnsDB(
            MSSloupecDB.CI(SqlDbType2.BigInt, "ID", true),
            // Zde se bude ukládat vždy bez www.
            MSSloupecDB.CI(SqlDbType2.NVarChar, "Name(255)", false, true)
            ));
        s.Add(Tables.Platforms, new MSColumnsDB(
            MSSloupecDB.CI(SqlDbType2.TinyInt, "ID", true),
            MSSloupecDB.CI(SqlDbType2.NVarChar, "Name(255)", false, true)
            ));
        s.Add(Tables.Langs, new MSColumnsDB(
            MSSloupecDB.CI(SqlDbType2.TinyInt, "ID", true),
            MSSloupecDB.CI(SqlDbType2.NVarChar, "Name(10)", false, true)
            )); 
        #endregion
        s.Add(Tables.Colors, new MSColumnsDB(
            MSSloupecDB.CI(SqlDbType2.SmallInt, "ID", true),
            MSSloupecDB.CI(SqlDbType2.Char, "Name(25)", false, true),
            MSSloupecDB.CI(SqlDbType2.TinyInt, "R", false, false),
            MSSloupecDB.CI(SqlDbType2.TinyInt, "G", false, false),
            MSSloupecDB.CI(SqlDbType2.TinyInt, "B", false, false)
            ));
        
        s.Add(Tables.OS, new MSColumnsDB(
            MSSloupecDB.CI(SqlDbType2.SmallInt, "ID", true),
            MSSloupecDB.CI(SqlDbType2.NVarChar, "Name(40)", false, true)
            ));
        s.Add(Tables.IPAddress, new MSColumnsDB(true,
            MSSloupecDB.CI(SqlDbType2.Int, "ID", false, false),
            //MSSloupecDB.CI(SqlDbType2.SmallDateTime, "Date", false, false),
            MSSloupecDB.CI(SqlDbType2.TinyInt, "IP1", false, false),
            MSSloupecDB.CI(SqlDbType2.TinyInt, "IP2", false, false),
            MSSloupecDB.CI(SqlDbType2.TinyInt, "IP3", false, false),
            MSSloupecDB.CI(SqlDbType2.TinyInt, "IP4", false, false),
            MSSloupecDB.CI(SqlDbType2.Bit, "IsBlocked", false, false)

            //MSSloupecDB.CI(SqlDbType2.TinyInt, "Views", false, false)
            ));

        s.Add(Tables.LoginAttempt, new MSColumnsDB(
            MSSloupecDB.CI(SqlDbType2.Int, "ID"),
            MSSloupecDB.CI(SqlDbType2.NVarChar, "Login(" + delkaLogin + ")", false, false),
            MSSloupecDB.CI(SqlDbType2.SmallDateTime, "DT"),
            MSSloupecDB.CI(SqlDbType2.TinyInt, "Count")
            ));

        #region Tyto tabulky se také nikde nepoužívájí, komentáře každého webu jsou v každíém layer webu zvlášť
        s.Add(Tables.Comments, new MSColumnsDB(
            MSSloupecDB.CI(SqlDbType2.Int, "ID", true),
            MSSloupecDB.CI(SqlDbType2.Int, "IDPages", false, false),
            MSSloupecDB.CI(SqlDbType2.Int, "IDUsers"),
            MSSloupecDB.CI(SqlDbType2.SmallDateTime, "DT", false, false),
            // NAHRAZUJE IsDeleted
            MSSloupecDB.CI(SqlDbType2.Bit, "IsDeletedByAdmin", false, false),
            // NOVÉ
            MSSloupecDB.CI(SqlDbType2.Bit, "IsDeletedByUser", false, false),
            MSSloupecDB.CI(SqlDbType2.SmallInt, "CountThumbsUp", false, false),
            MSSloupecDB.CI(SqlDbType2.SmallInt, "CountThumbsDown", false, false),
            // NOVÉ
            MSSloupecDB.CI(SqlDbType2.SmallInt, "CountMarkedSpam"),
            // NOVÉ
            MSSloupecDB.CI(SqlDbType2.Bit, "PhotoGallery"),
            // NOVÉ
            MSSloupecDB.CI(SqlDbType2.SmallDateTime, "Edited"),
            MSSloupecDB.CI(SqlDbType2.TinyInt, "IDSeries", false, false),
                MSSloupecDB.CI(SqlDbType2.NVarChar, "Content(MAX)"),
                MSSloupecDB.CI(SqlDbType2.Int, "IDIP")
            ));

        #region Zakomentované tabulky Comments které se nikde nevyužívali
        #endregion
        // Bude se to nastavovat globálně pro všechny komentáře(vč. těch na cizích stránkách) ale možná ani to ne protože uživatel by byl pak zavalen komentáři, které by byly označeny jen jejich obsahem a názvem stránky v URI
        s.Add(Tables.CommentSubscribe, new MSColumnsDB(
            MSSloupecDB.CI(SqlDbType2.Int, "IDPage"),
            MSSloupecDB.CI(SqlDbType2.Int, "IDUser"),
            MSSloupecDB.CI(SqlDbType2.TinyInt, "Serie")
            ));
        #endregion
    }

    public static void DeleteAndCreateTable2(string t)
    {
        MSStoredProceduresI.ci.DropAndCreateTable2(t, s);
    }
}
