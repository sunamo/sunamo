using System.Web.UI;
using System.Web.UI.HtmlControls;
public static class SchemaOrgHelper
{
    public static void InsertBasicToPageHeader(SunamoPage ss, PageSnippet ps, MySites ms)
    {
        string image = "";

        if (ps.image == "")
        {
            ps.image = UA.GetWebUri3(ss.Request, "img" + "/" + ms.ToString() + "/" + "ImplicitShareImage.jpg");
        }

        //pageSnippet.image = UA.GetWebUri3(ss.Request, "img/EmptyPixel.gif");
        if (ps.image != "")
        {
            image = ",\\\\\\\\\"image\\\\\\\\\": \\\\\\\\\"" + ps.image + "\\\\\\\\\"";
        }



        //<script type='application/ld+json'></script>
        string s = "{\\\\\\\\\"@context\\\\\\\\\": \\\\\\\\\"http://schema.org\\\\\\\\\",\\\\\\\\\"@type\\\\\\\\\": \\\\\\\\\"Thing\\\\\\\\\",\\\\\\\\\"name\\\\\\\\\": \\\\\\\\\"" + ps.title + "\\\\\\\\\",\\\\\\\\\"description\\\\\\\\\": \\\\\\\\\"" + ps.description + "\\\\\\\\\"" + image + "}";
        JavaScriptInjection.InjectInternalScript(ss, s, "application/ld+json");
    }

}
