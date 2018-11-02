using sunamo.Values;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Web.UI;
using System.Web.UI.HtmlControls;

public class SunamoPage : System.Web.UI.Page
{
    public string hfs = "";
    protected string descriptionPage = "";
    public bool showComments = false;
    protected bool zapisTitle = true;
    /// <summary>
    /// Ïs forbidden use this variable direct, always have to be used througt any method, e.g. PageArgumentVerifier.SetWriteRows() 
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
        SchemaOrgHelper.InsertBasicToPageHeader(this, ps, sa);
        OpenGraphHelper.InsertBasicToPageHeader(this, ps, sa);
    }

    protected PageSnippet InsertPageSnippet(string pageName, string desc)
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

    protected void Include(List<string> styles, List<string> scripts, List<string> stylesUri, List<string> scriptsUri)
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
    /// Before calling this method must be called FillIDUsers() to fill idLoginedUser variable
    /// </summary>
    /// <returns></returns>
    protected bool IsLoginedUserAdmin()
    {
        return idLoginedUser == 1;
    }

    /// <summary>
    /// Can be used only in General pages because in pages of specific web I'll have site-specific Page is method like IsLoginedMisterWithID with table Koc_Misters / IsLoginedYouthWithID with Sda_Youths etc.
    /// Before calling this method must be called FillIDUsers() to fill idLoginedUser variable
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

    public SunamoPage()
    {
    }

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
        base.OnLoad(e);

        // Must be here because then is processing MasterPage and there I need user ID. Dont change!
        FillIDUsers();

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
                // Page dont have <head runat='server'>
            }
        }
    }

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
