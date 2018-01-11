using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using System.Web.UI.HtmlControls;
public static class StyleInjection
{
    public static void RegisterClientStyleExclude(Page page, string src)
    {
        System.Web.UI.HtmlControls.HtmlGenericControl si = new System.Web.UI.HtmlControls.HtmlGenericControl();

        si.TagName = "link";

        si.Attributes.Add("type", "text/css");
        si.Attributes.Add("rel", "stylesheet");
        si.Attributes.Add("href", src);

        page.Header.Controls.Add(si);
    }

    public static void InjectExternalStyle(Page page, List<string> p1, string hostWithHttp)
    {
        foreach (var item in p1)
        {
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
