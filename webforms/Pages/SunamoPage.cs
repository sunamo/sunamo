using sunamo.Essential;
using sunamo.Helpers;
using sunamo.Values;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;

using webforms.Interfaces;

public class SunamoPage : System.Web.UI.Page
{
    public string pageName = string.Empty;
    /// <summary>
    /// In many pages is using Title
    /// </summary>
    public new string Title
    {
        get
        {
            return pageName;
        }
        set
        {
            pageName = value;
        }
    }


    public new HttpRequest Request => HttpContext.Current.Request;
    public new HttpResponse Response => HttpContext.Current.Response;
    public new HtmlHead Header
    {
        get
        {
            try
            {
                return Page.Header;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }

    public string Css(object cl)
    {
        // jméno stránky zjistit ze cl, jméno webu ze A1
        string csPage = SunamoPage.GetName(cl, 2);
        string PagePage = MySitesConverter.ConvertFrom(MySitesConverter.ConvertFrom(sa));

        return "css/" + PagePage + "/" + csPage + ".css";
    }

    public string Js(object cl)
    {
        string csPage = SunamoPage.GetName(cl, 2);
        string PagePage = sa.ToString();

        return "ts/" + PagePage + "/" + csPage + ".js";
    }

    

    public static string GetName(object cl, int v)
    {
        string d = cl.GetType().Name;
        string s = d.Substring(0, d.Length - v);
        return s;
    }

    public new Page Page = null;
    public new SunamoPage sunamoPage
    {
        get
        {
            
            return SunamoMasterPage.CastToSunamoPage(Page);
        }
        
    }
    //public new HttpHead Header => HttpContext.Current.

    /// <summary>
    /// Used in Photo.aspx <%= hfs %>
    /// </summary>
    public string hfs = "";
    protected string descriptionPage = "";
    public bool showComments = false;
    protected bool zapisTitle = true;
    /// <summary>
    /// Ïs forbidden use Page variable direct, always have to be used througt any method, e.g. PageArgumentVerifier.SetWriteRows() 
    /// Variable is use nowhere in SunamoPage, it's here just for avoid declare in *Page every time
    /// </summary>
    public bool? writeRows = null;
    public MySites sa = MySites.Nope;
    /// <summary>
    /// Is filling in FillLoginVariables(), FillIDUsers(), WriteRowToPagesAndLstVisits(). If after that is still -1, user didnt be authorize.
    /// </summary>
    public int idLoginedUser = -1;
    public string nameLoginedUser = null;
    public string scLoginedUser = null;
    public uint overall = 0;
    public uint today = 0;

    public SunamoPage()
    {
        //IPageCs cs = Page as IPageCs;

        //if (cs != null)
        //{
        //    sunamoPage.PreInit += cs.SunamoPage_PreInit;
        //    sunamoPage.Init += cs.SunamoPageCs_Init;
        //    sunamoPage.InitComplete += cs.SunamoPageCs_InitComplete;
        //    sunamoPage.PreLoad += cs.SunamoPageCs_PreLoad;
        //    //}

        //    // Load between two ifs
        //    sunamoPage.Load += SunamoPage_Load;

        ////if (cs != null)
        ////{
        //    sunamoPage.LoadComplete += cs.SunamoPageCs_LoadComplete;
        //    sunamoPage.PreRender += cs.SunamoPageCs_PreRender;
        //    sunamoPage.SaveStateComplete += cs.SunamoPageCs_SaveStateComplete;
        //    // Render method, not event https://www.c-sharpcorner.com/uploadfile/61b832/Asp-Net-page-life-cycle-events/
        //    sunamoPage.Unload += cs.SunamoPageCs_Unload;
        //}
    }

    private void SunamoPage_Load(object sender, EventArgs e)
    {
        // Nevyvolá se v Cs třídě
        //OnLoad(e);
    }

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

    HtmlGenericControl _errors = null;
    public HtmlGenericControl errorsPlaceholder
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

    /// <summary>
    /// If idPage != -1, shouldnt be neither idUsers == -1. When idUsers == -1, user wont be autenticate.
    /// </summary>
    public int idPage = -1;
    public int entityId = int.MaxValue;
    public byte IDWeb = 8;
    public string namePage = "";
    public string args = "";

    public int idPageName = int.MaxValue;
    public IPAddress ipAddress = null;
    public bool writeVisit = false;
    /// <summary>
    /// ID stránky pod kterým se stránka vede v tabulce PageArgument
    /// </summary>
    public int idPageArgument = int.MaxValue;
    public bool writeToLastVisitsLogined = false;

    #region Events variables
    public event VoidVoid ErrorEvent;
    public event VoidVoid WarningEvent;
    public event VoidVoid InfoEvent;
    public event VoidVoid SuccessEvent;
    protected bool callEventInfo = true;
    protected bool callEventWarning = true;
    protected bool callEventSuccess = true;
    protected bool callEventError = true;
    #endregion

    protected Langs GetLang()
    {
        return Langs.cs;
    }


    protected void InsertPageSnippet(PageSnippet ps)
    {
        SchemaOrgHelper.InsertBasicToPageHeader(sunamoPage, ps, sa);
        OpenGraphHelper.InsertBasicToPageHeader(sunamoPage, ps, sa);
    }

    protected PageSnippet InsertPageSnippet(string pageName, string desc)
    {
        if (desc == "")
        {
            desc = SunamoPageHelper.DescriptionOfSite((byte)sa);
        }
        PageSnippet ps = new PageSnippet { title = pageName, description = desc };
        SchemaOrgHelper.InsertBasicToPageHeader(sunamoPage, ps, sa);
        OpenGraphHelper.InsertBasicToPageHeader(sunamoPage, ps, sa);
        return ps;
    }

    public void InsertPageSnippet(string pageName, MySitesShort sda)
    {
        string desc = "";
        desc = SunamoPageHelper.DescriptionOfSite((byte)sda);
        InsertPageSnippet(pageName, desc);
    }

    /// <summary>
    /// Into A2 I cant pass Consts.tString, string must be validate in other way
    /// </summary>
    /// <param name="control"></param>
    /// <param name="type"></param>
    protected void RegisterForEventValidation(Control control, Type type)
    {
        string r = Request.Form[control.UniqueID];
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
            ClientScript.RegisterForEventValidation(control.UniqueID, r);
        }
        else
        {
            ClientScript.RegisterForEventValidation(control.UniqueID, "");
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
        string dd = Request.Form[c.UniqueID];
        if (dd == null)
        {
            dd = "";
        }
        return dd;
    }

    /// <summary>
    /// Is used when I have turned off viewstate, but better way is use HasContent() which is moore speedy
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
    }

    /// <summary>
    /// My files should be as trailing - while adding to lately added will be more in front in html
    /// </summary>
    /// <param name="styles"></param>
    /// <param name="scripts"></param>
    /// <param name="stylesUri"></param>
    /// <param name="scriptsUri"></param>
    protected void Include(List<string> styles, List<string> scripts, List<string> stylesUri, List<string> scriptsUri)
    {
        string hostWithHttp = "http://" + Request.Url.Host + "/";

        if (scriptsUri == null)
        {
            scriptsUri = new List<string>(1);
        }
        if (idLoginedUser == 1)
        {
            if (Request.Url.Host.Contains(Consts.@sunamo))
            {
                scripts.Insert(0, "ts/Web/ShowDebugInfo.js");
            }
        }
        else
        {
            scriptsUri.Insert(0, "https://www.google-analytics.com/analytics.js");
        }

        
        
        
        JavaScriptInjection.InjectExternalScriptOnlySpecified(sunamoPage, scriptsUri, "");
        JavaScriptInjection.InjectExternalScriptOnlySpecified(sunamoPage, scripts, hostWithHttp);

        if (stylesUri != null)
        {
            StyleInjection.InjectExternalStyle(sunamoPage, stylesUri, "");
        }
        StyleInjection.InjectExternalStyle(sunamoPage, styles, hostWithHttp);
    }

    /// <summary>
    /// Pass relative path to actual, when I'm in Lyrics/Home and I want to go to Lyrics/AddSong, is enough just AddSong
    /// </summary>
    /// <param name="uri"></param>
    public void WriteToDebugWithTime(string uri)
    {
        try
        {
            Response.Redirect(uri);
        }
        catch (Exception)
        {
            // Often I got error: System.Threading.ThreadAbortException Thread was being aborted.
        }
        Response.End();
    }

    /// <summary>
    /// Before calling Page method must be called FillIDUsers() to fill idLoginedUser variable
    /// </summary>
    /// <returns></returns>
    protected bool IsLoginedUserAdmin()
    {
        return idLoginedUser == 1;
    }

    

    /// <summary>
    /// Can be used only in General pages because in pages of specific web I'll have site-specific Page is method like IsLoginedMisterWithID with table Koc_Misters / IsLoginedYouthWithID with Sda_Youths etc.
    /// Before calling Page method must be called FillIDUsers() to fill idLoginedUser variable
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

    public bool FillIDUsers()
    {
        if (idLoginedUser == -1)
        {
            LoginedUser lu = SessionManager.GetLoginedUser(sunamoPage);

            int id = lu.ID(sunamoPage);
            if (id != -1)
            {
                idLoginedUser = id;
                return true;
            }
            return false;
        }
        return true;
    }

    public void WriteOld(PageArgumentName[] pans = null)
    {
        PageArgumentVerifier.GetIDWebAndNameOfPage(out IDWeb, out namePage, sunamoPage.Request.FilePath);
        if (pans != null && pans != PageArgumentName.EmptyArray)
        {
            PageArgumentVerifier.SetWriteRows(sunamoPage, pans);
        }
        else
        {
            PageArgumentVerifier.SetWriteRows(sunamoPage, PageArgumentName.EmptyArray);
        }

        if (writeRows.HasValue)
        {
            if (writeRows.Value)
            {
                DayViewManager.IncrementOrInsertOld(sunamoPage);
            }
        }
    }

    protected bool RedirectOnRevitalization()
    {
        FillIDUsers();
        if (idLoginedUser == 1 || SessionManager.GetLoginedUser(sunamoPage).login == "katie91")
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


    public void CreateTitle()
    {
        Request.Headers.Add(HttpKnownHeaderNames.CacheControl, "no-cache");
        // if uncommentted -> StackOverflowException
        //Page.OnLoad(EventArgs.Empty);

        //if (Page.Title == string.Empty || Page.Title[0] != AllChars.space)
        //{

        // Must be here because then is processing MasterPage and there I need user ID. Dont change!
        FillIDUsers();

        if (GeneralLayer.AllowedRegLogSys)
        {
            if (MSStoredProceduresI.ci.SelectExistsTable(Tables.Users))
            {
                MSStoredProceduresI.ci.Update(Tables.Users, "LastSeen", DateTime.Now, "ID", idLoginedUser);
            }
        }

        // In OnLoad() CreateTitle(); cannot be - Title wont be saved. Must be in aspx.cs
        //CreateTitle();

        // Page.OnLoad(), OnLoad() - call SunamoPage, not DeveloperPage as need
        MasterPageHelper.AddFavicon(this, sa);
        if (zapisTitle)
        {
            
            try
            {
                Page.Title = pageName + SunamoPageHelper.WebTitle(sa, Request);
                zapisTitle = false;
            }
            catch (Exception ex)
            {
                throw new Exception("Cant set Title: " + Exceptions.TextOfExceptions(ex));
                // Page dont have <head runat='server'>
            }
        }
    }

    #region Events method
    public new void Error(string message)
    {
        var page = sunamoPage;
        errorsPlaceholder.Visible = true;

        StringBuilder sb = new StringBuilder();
        sb.Append(SunamoRoutePage.GetRightUpRoot(Request));
        sb.Append("img/ko.gif");
        string img = HtmlTemplates.Img(sb.ToString());
        errorsPlaceholder.InnerHtml = img + message;
        errorsPlaceholder.Attributes.Remove("class");
        errorsPlaceholder.Attributes.Add("class", "error");
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
        var page = sunamoPage;
        errorsPlaceholder.Visible = true;
        StringBuilder sb = new StringBuilder();
        sb.Append(SunamoRoutePage.GetRightUpRoot(Request));
        sb.Append("img/warning.gif");
        string img = HtmlTemplates.Img(sb.ToString());
        errorsPlaceholder.InnerHtml = img + message;
        errorsPlaceholder.Attributes.Remove("class");
        errorsPlaceholder.Attributes.Add("class", "varovani");
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
        var page = sunamoPage;
        errorsPlaceholder.Visible = true;
        StringBuilder sb = new StringBuilder();
        sb.Append(SunamoRoutePage.GetRightUpRoot(Request));
        sb.Append("img/info.gif");
        string img = HtmlTemplates.Img(sb.ToString());
        errorsPlaceholder.InnerHtml = img + message;
        errorsPlaceholder.Attributes.Remove("class");
        errorsPlaceholder.Attributes.Add("class", "info");
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
        var page = sunamoPage;
        errorsPlaceholder.Visible = true;
        StringBuilder sb = new StringBuilder();
        sb.Append(SunamoRoutePage.GetRightUpRoot(Request));
        sb.Append("img/ok.gif");
        string img = HtmlTemplates.Img(sb.ToString());
        errorsPlaceholder.InnerHtml = img + message;
        errorsPlaceholder.Attributes.Remove("class");
        errorsPlaceholder.Attributes.Add("class", "success");
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
