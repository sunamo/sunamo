using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using System.Web.UI.HtmlControls;
public static class StyleInjection
{
    public static void RegisterClientStyleExclude(SunamoPage page, string src)
    {
        System.Web.UI.HtmlControls.HtmlGenericControl si = new System.Web.UI.HtmlControls.HtmlGenericControl();

        si.TagName = "link";

        si.Attributes.Add("type", "text/css");
        si.Attributes.Add("rel", "stylesheet");
        si.Attributes.Add("href", src);

        page.Header.Controls.Add(si);
    }

    public static void InjectExternalStyle(SunamoPage page, List<string> p1, string hostWithHttp)
    {
        // řešeno v HexCodeForAwesomeFontChar
        foreach (var item in p1)
        {
            //for (int i = p1.Count - 1; i >= 0; i--)
            //{
            //    var item = p1[i];
            var myHtmlLink = new HtmlLink { Href = hostWithHttp + item };
            myHtmlLink.Attributes.Add("rel", "Stylesheet");
            if (item.EndsWith(".css"))
            {
                myHtmlLink.Attributes.Add("type", "text/css");
            }
            myHtmlLink.Attributes.Add("media", "all");
            page.Header.Controls.AddAt(0, myHtmlLink);
        }
    }
}
