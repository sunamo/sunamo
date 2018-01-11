using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using webforms;

/// <summary>
/// Při používání této třídy musíš mít ve stránce jQuery
/// Snaž se tuto třídu využívat co nejméně, protože pak to dělá neplechu a nad to to ani nejde ladit atd..
/// </summary>
public class JavaScriptInjection
{
    

    /// <summary> 
    /// Shows a client-side JavaScript alert in the browser. 
    /// </summary> 
    /// <param name="message">The message to appear in the alert.</param> 
    public static void alert(string message, Page page)
    {
        alert("alert", message, page);
    }

    public static void alertWithCloseWindow(string message, Page page)
    {
        string nameOfBlock = "alert";
        if (page != null && !page.ClientScript.IsClientScriptBlockRegistered(nameOfBlock))
        {
            page.ClientScript.RegisterClientScriptBlock(page.GetType(), nameOfBlock, JavaScriptGenerator2.alertWithCloseWindow(message), true);

        }
    }

    public static void alertIfNotEmpty(string errorCat, Page koc_Default)
    {
        if (errorCat != "")
        {
            alert("alert", errorCat, koc_Default);
        }
    }

    public static void InjectGooglePieChart3D(Page page, string idElement, string title, string coSePorovnava, string jednotkaHodnotyPorovnavani, IEnumerable<KeyValuePair<string, int>> d)
    {   
        RegisterClientScriptInnerHtml(page, JavaScriptGenerator2.GooglePieChart3D(idElement, title, coSePorovnava, jednotkaHodnotyPorovnavani, d));
    }

    public static void InjectGooglePieChart3D(Page page, string idElement, string title, string coSePorovnava, string jednotkaHodnotyPorovnavani, List<string> coSePorovnavaHodnoty, List<string> jednotkaHodnotyPorovnavaniHodnoty)
    { 
        RegisterClientScriptInnerHtml(page, JavaScriptGenerator2.GooglePieChart3D(idElement, title, coSePorovnava, jednotkaHodnotyPorovnavani, coSePorovnavaHodnoty, jednotkaHodnotyPorovnavaniHodnoty));
    }

    /// <summary>
    /// A6 jsou popisky pro dolní část. Horní část se doplní automaticky podle toho kolik bude v A7
    /// A7 jsou hodnoty pro dolní oblast. 
    /// </summary>
    /// <param name="page"></param>
    /// <param name="idElement"></param>
    /// <param name="title"></param>
    /// <param name="osaX"></param>
    /// <param name="osaY"></param>
    /// <param name="osaXPopisky"></param>
    /// <param name="osaXHodnoty"></param>
    public static void InjectGoogleLineChart(Page page, string idElement, string title, string osaX, string osaY, List<string> osaXPopisky, List<string> osaXHodnoty)
    {
        RegisterClientScriptInnerHtml(page, JavaScriptGenerator2.GoogleLineChart(idElement, title, osaX, osaY, osaXPopisky, osaXHodnoty));
    }

    /// <summary>
    /// Tuto metoda nevyužívat, když do stránky injektuješ pouze jeden skript. 
    /// </summary>
    /// <param name="nameOfBlock"></param>
    /// <param name="message"></param>
    /// <param name="page"></param>
    public static void alert(string nameOfBlock, string message, Page page)
    {
        // Checks if the handler is a Page and that the script isn't allready on the Page 
        if (page != null && !page.ClientScript.IsClientScriptBlockRegistered(nameOfBlock))
        {
            page.ClientScript.RegisterClientScriptBlock(page.GetType(), nameOfBlock, JavaScriptGenerator2.alert(message), true);

        }

    }

    public static void alert(Page page, string appendToStart, string message)
    {
        alert(appendToStart + message, page);

    }

    public static void InjectFunctionOpenNewTab(Page page, string functionName, string uri)
    {
        RegisterClientScriptInnerHtml(page, functionName, JavaScriptGenerator2.FunctionOpenNewTab(uri));
    }

    public static void InjectJQueryAjaxForHandler( Page page, int countUp, MySitesShort ms, string nameOfFunction, params string[] args)
    {
            RegisterClientScriptInnerHtml(page, JavaScriptGenerator2WebForms.JQueryAjaxForHandler(ms, countUp, nameOfFunction, args));
    }

    public static void InjectJQueryAjaxForHandlerShowMessage(string successMessage, int countUp, Page page, MySitesShort ms, string nameOfFunction, params string[] args)
    {
        RegisterClientScriptInnerHtml(page, JavaScriptGenerator2WebForms.JQueryAjaxForHandlerShowMessage(successMessage, countUp, ms, nameOfFunction, args));
    }

    public static void RegisterClientScriptInnerHtml(Page page, string functionName, string src)
    {
        RegisterClientScriptInnerHtml(page, "function " + functionName + @"() {
" + src + "}");
    }

    public static void RegisterClientScriptInnerHtml(Page page, string src)
    {
        System.Web.UI.HtmlControls.HtmlGenericControl si = new System.Web.UI.HtmlControls.HtmlGenericControl();

        si.TagName = "script";

        si.Attributes.Add("type", "text/javascript");

        si.InnerHtml = src;

        page.Header.Controls.Add(si);
    }

    

    /// <summary>
    /// Automaticky vkládá do stránky všechny JS definované v SunamoPage.includeScripts
    /// Do A2 se dává výstup metody GetRightUpRoot()
    /// Do A3 se zadává bez počátečního lomítka
    /// </summary>
    /// <param name="page"></param>
    /// <param name="getRightUpRoot"></param>
    /// <param name="p1"></param>
    public static void InjectExternalScriptOnlySpecifiedAsync(Page page, List<string> p2, string hostWithHttp)
    {
        foreach (var item in p2)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<script type=\"");
            if (item.EndsWith(".js"))
            {
                sb.Append("text/javascript");
            }
            sb.Append("\"");
            // charset=\"utf-16\"
            sb.Append(" src=\"");
            sb.Append(hostWithHttp + item);
            sb.Append("\" async=\"async\"></script>");
            LiteralControl lc = new LiteralControl(sb.ToString());
            page.Header.Controls.AddAt(0, lc);
        }
    }
    public static void InjectExternalScriptOnlySpecified(Page page, List<string> p2, string hostWithHttp)
    {

        foreach (var item in p2)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<script");
            sb.Append(" type=\"");
                sb.Append("text/javascript");
            //}
            sb.Append("\"");

            if ((hostWithHttp.StartsWith("http://") || item.StartsWith("https://")) && !item.Contains("{") && !item.Contains("["))
            {
                // charset=\"utf-16\"
                sb.Append(" src=\"");
                sb.Append(hostWithHttp + item ); //+ "?nocache"
                sb.Append("\">");
            }
            else
            {
                sb.AppendLine(">");
                sb.Append(item);
            }
            sb.Append("</script>");
            LiteralControl lc = new LiteralControl(sb.ToString());
            page.Header.Controls.AddAt(0, lc);
        }
    }

    public static void InjectInternalScript(Page page, string javaScript)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("<script type=\"");
        sb.Append("text/javascript");
        sb.Append("\">");
        sb.Append(javaScript);
        sb.Append("</script>");
        LiteralControl lc = new LiteralControl(sb.ToString());
        page.Header.Controls.Add(lc);
    }

    public static void InjectInternalScript(Page page, string script, string type)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("<script type=\"");
        sb.Append(type);
        sb.Append("\">");
        sb.Append(script);
        sb.Append("</script>");
        LiteralControl lc = new LiteralControl(sb.ToString());
        page.Header.Controls.Add(lc);
    }

    public static void InitTinyMCE(SunamoPage sp)
    {
        //cleanup_callback : " + string.Format("{0}myCustomCleanup{0},save_callback : {0}myCustomSaveContent{0}", "\"") + @"
        InjectInternalScript(sp, @"$(document).ready(function () {
            tinymce.init({
    selector: '#txtTinymce',
plugins: 'advlist,anchor,autolink,autoresize,autosave,charmap,code,colorpicker,contextmenu,directionality,emoticons,fullpage,fullscreen,hr,image,importcss,insertdatetime,layer,legacyoutput,link,lists,media,nonbreaking,noneditable,pagebreak,paste,preview,print,save,searchreplace,spellchecker,tabfocus,table,template,textcolor,textpattern,visualblocks,visualchars,wordcount'
})
});");
    }

    public static void InitNanoGallery(SunamoPage page, string baseUri)
    {
        InjectInternalScript(page, @"$(document).ready(function () {
var myColorScheme = {
        navigationbar: {
            background: 'transparent',
            
        },
        thumbnail: {
            background: 'transparent',
            
        }
    };

      $('#nanoGallery3').nanoGallery({
          itemsBaseURL:'" + baseUri+ @"',
thumbnailHoverEffect: 'borderLighter',
        colorScheme: myColorScheme,
thumbnailHeight: 168,
thumbnailWidth: 300,
viewerFullscreen: true
      });
  });");
    }

    public static void InitNanoGallery(Page page, string baseUri, List<string> photosUri, List<string> photosUriTn, List<string> photosDesc)
    {
        StringBuilder sb = new StringBuilder();

        for (int i = 0; i < photosUri.Count; i++)
        {
            sb.Append("{src:\"" + photosUri[i] + "\",srct:\"" + photosUriTn[i] + "\",title:\"" + photosDesc[i] + "\"},");
        }

        string items = "";
        if (sb.Length != 0)
        {
            items = sb.ToString(0, sb.Length - 1);
        }

        InjectInternalScript(page, @"$(document).ready(function () {
var myColorScheme = {
        navigationbar: {
            background: 'transparent',
            
        },
        thumbnail: {
            background: 'transparent',
            
        }
    };

      $('#nanoGallery3').nanoGallery({
itemsBaseURL:'" + baseUri + @"',
items:[" +items+ @"],
thumbnailHoverEffect: 'borderLighter',
colorScheme: myColorScheme,
thumbnailHeight: 168,
thumbnailWidth: 300,
viewerFullscreen: true
});
});");
    }

    public static void InitNanoGalleryJustifiedlayout(Page page, string baseUri)
    {
        InjectInternalScript(page, @"$(document).ready(function () {
var myColorScheme = {
        navigationbar: {
            background: 'transparent',
            
        },
        thumbnail: {
            background: 'transparent',
        }
    };

      $('#nanoGallery3').nanoGallery({
thumbnailWidth: 'auto',
thumbnailHeight: 162,
colorScheme: myColorScheme,
itemsBaseURL:'" + baseUri + @"',
thumbnailHoverEffect: [{ name: 'labelAppear75', duration: 300 }],
theme: 'clean',
thumbnailGutterWidth : 0,
thumbnailGutterHeight : 0,
viewerFullscreen: true,
i18n: { thumbnailImageDescription: 'Detail', thumbnailAlbumDescription: 'Otevřít album' },
thumbnailLabel: { display: true, position: 'overImageOnMiddle', align: 'center' }
});
});");

    }
}
