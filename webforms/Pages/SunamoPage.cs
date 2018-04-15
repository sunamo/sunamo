using sunamo.Values;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Web.UI;
using System.Web.UI.HtmlControls;

public  class SunamoPage : System.Web.UI.Page
{
    //protected PageArgument[] pageArguments = new PageArgument[0];
    public string hfs = "";
    protected string descriptionPage = "";

    protected Langs GetLang()
    {
        return Langs.cs;
    }

    protected void InsertPageSnippet(PageSnippet ps)
    {
        SchemaOrgHelper.InsertBasicToPageHeader(this, ps, sa);
        OpenGraphHelper.InsertBasicToPageHeader(this, ps, sa);
    }

    protected PageSnippet InsertPageSnippet( string pageName, string desc)
    {
        if (desc == "")
        {
            desc = SunamoPageHelper.DescriptionOfSite((byte)sa);
        }
        PageSnippet ps = new PageSnippet { title = pageName, description = desc };
        SchemaOrgHelper.InsertBasicToPageHeader(this, ps, sa);
        OpenGraphHelper.InsertBasicToPageHeader(this, ps, sa);
        return ps;
    }

    public void InsertPageSnippet(string pageName, MySitesShort sda)
    {
        string desc = "";
        desc = SunamoPageHelper.DescriptionOfSite((byte)sda);
        InsertPageSnippet(pageName, desc);
    }

    #region Odstranění ViewState
    #endregion

    /// <summary>
    /// Do A1 se nemůže předat Consts.tString, ta se musí validovat samostatně
    /// </summary>
    /// <param name="ddlStates"></param>
    /// <param name="type"></param>
    protected void RegisterForEventValidation(Control ddlStates, Type type)
    {
        string r = Request.Form[ddlStates.UniqueID];
        bool b = false;
        if (r != null)
        {
            if (type == Consts.tString)
            {
                r = RegexHelper.rHtmlScript.Replace(r, "");
                r = RegexHelper.rHtmlComment.Replace(r, "");
                r = SH.ReplaceAll2(r, " ", "  ");
                b = true;
            }
            else if (type == Consts.tInt)
            {
                int nt = 0;
                b = int.TryParse(r, out nt);
            }
            else if (type == Consts.tDateTime)
            {
                DateTime dt;
                b = DateTime.TryParse(r, out dt);
            }
            else if (type == Consts.tDouble)
            {
                double d = 0;
                b = double.TryParse(r, out d);
            }
            else if (type == Consts.tFloat)
            {
                float f = 0;
                b = float.TryParse(r, out f);
            }
            else if (type == Consts.tBool)
            {
                bool b2 = false;
                b = bool.TryParse(r, out b2);
            }
            else if (type == Consts.tByte)
            {
                byte by = 0;
                b = byte.TryParse(r, out by);
            }
            else if (type == Consts.tShort)
            {
                short sh = 0;
                b = short.TryParse(r, out sh);
            }
            else if (type == Consts.tLong)
            {
                long l = 0;
                b = long.TryParse(r, out l);
            }
            else if (type == Consts.tDecimal)
            {
                decimal d = 0;
                b = decimal.TryParse(r, out d);
            }
            else if (type == Consts.tSbyte)
            {
                sbyte sb = 0;
                b = sbyte.TryParse(r, out sb);
            }
            else if (type == Consts.tUshort)
            {
                ushort us = 0;
                b = ushort.TryParse(r, out us);
            }
            else if (type == Consts.tUint)
            {
                uint ui = 0;
                b = uint.TryParse(r, out ui);
            }
            else if (type == Consts.uUlong)
            {
                ulong ul = 0;
                b = ulong.TryParse(r, out ul);
            }
        }
        if (b)
        {
            ClientScript.RegisterForEventValidation(ddlStates.UniqueID, r);
        }
        else
        {
            ClientScript.RegisterForEventValidation(ddlStates.UniqueID, "");
        }
    }

    protected string GetContent(Control c, string simpleName)
    {
        string sr = c.UniqueID.Substring(0, c.UniqueID.LastIndexOf('$') + 1) + simpleName;
        string dd = Request.Form[sr];
        if (dd == null)
        {
            dd = "";
        }
        return dd;
    }

    protected string GetContent(Control c)
    {
        //string sr = c.UniqueID.Substring(0, c.UniqueID.LastIndexOf('$') + 1) + simpleName;
        string dd = Request.Form[c.UniqueID];
        if (dd == null)
        {
            dd = "";
        }
        return dd;
    }

    /// <summary>
    /// Používá se když mám vypnutou viewstate, ale lepší je použít metodu HasContent která je rychlejší
    /// </summary>
    /// <param name="c"></param>
    /// <returns></returns>
    protected bool IsChecked(Control c)
    {
        string dd = Request.Form[c.UniqueID];
        if (dd == null)
        {
            return false;
        }
        return dd == "on";
    }


    protected bool HasContent(Control c)
    {
        string cont = Request.Form[c.UniqueID];

        return !string.IsNullOrWhiteSpace(cont);
        //}
    }

    protected void Include(List<string> styles, List<string> scripts, List<string> stylesUri,  List<string> scriptsUri)
    {
        string hostWithHttp = "http://" + Request.Url.Host + "/";
        

        if (idLoginedUser == 1)
        {
            if (Request.Url.Host.Contains(Consts.@sunamo))
            {
                scripts.Insert(0, "ts/Web/ShowDebugInfo.js");
            }
        }
        
        if (scriptsUri == null)
        {
            scriptsUri = new List<string>(1);
        }
        scriptsUri.Insert(0, "https://www.google-analytics.com/analytics.js");
        JavaScriptInjection.InjectExternalScriptOnlySpecified(this, scriptsUri, "");
        JavaScriptInjection.InjectExternalScriptOnlySpecified(this, scripts, hostWithHttp);
        
        if (stylesUri != null)
        {
            StyleInjection.InjectExternalStyle(this, stylesUri, "");
        }
        StyleInjection.InjectExternalStyle(this, styles, hostWithHttp);
    }


        /// <summary>
        /// Zapisuje se relativní cesta k aktuální cestě, jsem li třeba v Lyrics/Home a chci do Lyrics/AddSong, zapíšu pouze AddSong
        /// </summary>
        /// <param name="uri"></param>
    public void WriteToDebugWithTime(string uri)
    {
        #region Když ti tato metoda nebude fungovat, zkontoluj zda do ní nepředáváš null, empty nebo whitespace
        #region Toto už vůbec nefungovalo
        #endregion
        #endregion

        // Toto byl původní kód
        try
        {
            ////uri);
            Response.Redirect(uri);
        }
        catch (Exception)
        {
            // Vyhazovalo to chybu: System.Threading.ThreadAbortException Thread was being aborted.
        }
        Response.End();
    }
    public bool showComments = false;
    

    //public StringBuilder stylesAndScripts = new StringBuilder();
    protected bool zapisTitle = true;
    /// <summary>
    /// Je zakázáno tuto proměnnou nastavovat přímo, vžžy se to musí přes nějakou metodu, například //PageArgumentVerifier.SetWriteRows
    /// Tato proměnná se nikde ve SunamoPage nevyužívá, slouží pouze k tomu abych ji nemusel pokaždé definovat znovu v každé třídě *Page
    /// </summary>
    public bool? writeRows = null;
    public MySites sa = MySites.Nope;
    /// <summary>
    /// Naplňuje se v metodách FillLoginVariables a FillIDUsers
    /// </summary>
    public int idLoginedUser = -1;
    public string nameLoginedUser = null;
    public string scLoginedUser = null;

    /// <summary>
    /// Před zavoláním této metody musí být voláno alespoň FillIDUsers, protože tato metoda žádnou takovou metodu nevolá
    /// </summary>
    /// <returns></returns>
    protected bool IsLoginedUserAdmin()
    {
        return idLoginedUser == 1;
    }

    public uint overall = 0;
    public uint today = 0;
    /// <summary>
    /// Toto jde použít pouze v General stránkách protože v stránkách specifického webu budu mít samostatnou tabulku jako je Sda_Youths nebo Koc_Misters, proto se snaž spíše používat metody IsLoginedYouthWithID nebo IsLoginedMisterWithID
    /// Před použitím této metody je třeba nějak prvně naplnit proměnnou idUsers v SunamoPage. S dalšími podobnými proměnnýma metoda nepracuje.
    /// </summary>
    /// <param name="p"></param>
    /// <returns></returns>
    protected bool IsLoginedUserWithID(int p)
    {
        if (idLoginedUser == p)
        {
            return true;
        }
        return false;
    }


     HtmlGenericControl _errors = null;
    public HtmlGenericControl errors
    {
        get
        {
            return _errors;
        }
        set
        {
            _errors = value;
            _errors.Visible = false;
        }
    }

    public bool FillIDUsers()
    {
        if (idLoginedUser == -1)
        {
            LoginedUser lu = SessionManager.GetLoginedUser(this);

            int id = lu.ID(this);
            if (id != -1)
            {
                idLoginedUser = id;
                return true;
            }
            return false;
        }
        return true;
    }

    /// <summary>
    /// Do A1 můžeš předat cokoliv, nic se s tím dále nedělá.
    /// Je to pouze pro připomenutí abys vložil do PP errors prvek error, aby s ním mohli metody Error/Warning/Info pracovat.
    /// Také nezapomeň ve stránkách kde máš errors při každém reloadu prvek errors skrýt, aby se pořád neukazovala tatéž chyba i když už nebude platná
    /// </summary>
    public SunamoPage()
    {
    }

    /// <summary>
    /// Pokud idPage != -1, nemělo by být ani idUsers -1. Pokud idUsers == -1, tak uživatele se nepodařilo autorizovat.
    /// </summary>
    public int idPage = -1;
    public int entityId = int.MaxValue;
    public byte IDWeb = 8;
    public string namePage = "";
    public string args = "";
    /// <summary>
    /// Naplňuje se v WriteRowToPagesAndLstVisits(), pokud i po volání této metody je -1, tak se uživatele nepodařilo autorizovat.
    /// </summary>
    //protected int idLoginedUser = -1;
    public int idPageName = int.MaxValue;
    public IPAddress ipAddress = null;
    public bool writeVisit = false;
    /// <summary>
    /// ID stránky pod kterým se stránka vede v tabulce PageArgument
    /// </summary>
    public int idPageArgument = int.MaxValue;
    public bool writeToLastVisitsLogined = false;

    public void WriteOld(PageArgumentName[] pans = null)
    {
        PageArgumentVerifier.GetIDWebAndNameOfPage(out IDWeb, out namePage, this.Request.FilePath);
        if (pans != null && pans != PageArgumentName.EmptyArray)
        {
            PageArgumentVerifier.SetWriteRows(this, pans);   
        }
        else
        {
            PageArgumentVerifier.SetWriteRows(this, PageArgumentName.EmptyArray);   
        }
        
        if (writeRows.HasValue)
        {
            if (writeRows.Value)
            {
                DayViewManager.IncrementOrInsertOld(this);
            }
        }
    }

    protected bool RedirectOnRevitalization()
    {
        FillIDUsers();
        if (idLoginedUser == 1 || SessionManager.GetLoginedUser(this).login == "katie91")
        {

        }
        else
        {
            if (Request.Url.Host != "localhost")
            {
                WriteToDebugWithTime("/default.aspx");
                return true;
            }
        }
        return false;
    }

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
    }

    protected override void OnLoad(EventArgs e)
    {
        

        #region Zakomentováno, toto se stejně děje v každé stránce vyžadující přihlášení
        #endregion


        base.OnLoad(e);

        // Musí to tu být, protože pak se zpracovává kód MasterPage a tam potřebuji ID uživatele. NEMĚNIT!!!
        FillIDUsers();

        #region MyRegion
        #endregion
        // Spouští se jako 2. 
        if (GeneralLayer.AllowedRegLogSys)
        {
            if (MSStoredProceduresI.ci.SelectExistsTable(Tables.Users))
            {
                MSStoredProceduresI.ci.Update(Tables.Users, "LastSeen", DateTime.Now, "ID", idLoginedUser);
            }
        }
        
        CreateTitle();
    }

    public void CreateTitle()
    {
        if (zapisTitle)
        {
                try
                {
                    Title = Title + SunamoPageHelper.WebTitle(sa, Request, sa);
                    zapisTitle = false;
                }
                catch (Exception)
                {
                    // Stránka neměla <head runat='server'>
                }
        }

        #region MyRegion
        #endregion
    }

    #region Events variables
    public event VoidVoid ErrorEvent;
    public event VoidVoid WarningEvent;
    // Pro info to skutečně nevytvářej, radši to dej jako warning s událostí, zbylé warningy bez událostí
    public event VoidVoid InfoEvent;
    public event VoidVoid SuccessEvent;
    protected bool callEventInfo = true;
    protected bool callEventWarning = true;
    protected bool callEventSuccess = true;
    protected bool callEventError = true; 
    #endregion

    #region Events method
    public new void Error(string message)
    {
        var page = this;
        errors.Visible = true;

        StringBuilder sb = new StringBuilder();
        sb.Append(SunamoRoutePage.GetRightUpRoot(Request));
        sb.Append("img/ko.gif");
        string img = HtmlTemplates.Img(sb.ToString());
        errors.InnerHtml = img + message;
        errors.Attributes.Remove("class");
        errors.Attributes.Add("class", "error");
        if (callEventError)
        {
            if (ErrorEvent != null)
            {
                ErrorEvent();
            }
        }
    }

    public TypeOfMessage Warning(string message)
    {
        var page = this;
        errors.Visible = true;
        StringBuilder sb = new StringBuilder();
        sb.Append(SunamoRoutePage.GetRightUpRoot(Request));
        sb.Append("img/warning.gif");
        string img = HtmlTemplates.Img(sb.ToString());
        errors.InnerHtml = img + message;
        errors.Attributes.Remove("class");
        errors.Attributes.Add("class", "varovani");
        if (callEventWarning)
        {
            if (WarningEvent != null)
            {
                WarningEvent();
            }
        }
        return TypeOfMessage.Warning;
    }

    public void Info(string message)
    {
        var page = this;
        errors.Visible = true;
        StringBuilder sb = new StringBuilder();
        sb.Append(SunamoRoutePage.GetRightUpRoot(Request));
        sb.Append("img/info.gif");
        string img = HtmlTemplates.Img(sb.ToString());
        errors.InnerHtml = img + message;
        errors.Attributes.Remove("class");
        errors.Attributes.Add("class", "info");
        if (callEventInfo)
        {
            if (InfoEvent != null)
            {
                InfoEvent();
            }
        }
    }

    public void Success(string message)
    {
        var page = this;
        errors.Visible = true;
        StringBuilder sb = new StringBuilder();
        sb.Append(SunamoRoutePage.GetRightUpRoot(Request));
        sb.Append("img/ok.gif");
        string img = HtmlTemplates.Img(sb.ToString());
        errors.InnerHtml = img + message;
        errors.Attributes.Remove("class");
        errors.Attributes.Add("class", "success");
        if (callEventSuccess)
        {
            if (SuccessEvent != null)
            {
                SuccessEvent();
            }
        }
    } 
    #endregion

    
}
