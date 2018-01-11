using System.Web.UI;
using System.Web.UI.HtmlControls;
public static class SchemaOrgHelper
{
    public static void InsertBasicToPageHeader(ISetSchema ss, PageSnippet pageSnippet, MySites ms)
    {
        InsertBasicToPageHeader(((MasterPage)ss).Page, pageSnippet, ms);
    }

    public static void InsertBasicToPageHeader(Page ss, PageSnippet ps, MySites ms)
    {
        string image = "";

        if (ps.image == "")
        {
            ps.image = web.UH.GetWebUri3(ss.Request, "img/" + ms.ToString() + "/" + "ImplicitShareImage.jpg");
        }

        //pageSnippet.image = UH.GetWebUri3(ss.Request, "img/EmptyPixel.gif");
        if (ps.image != "")
        {
            image = ",\"image\": \"" + ps.image + "\"";
        }
        


        //<script type='application/ld+json'></script>
        string s = "{\"@context\": \"http://schema.org\",\"@type\": \"Thing\",\"name\": \"" + ps.title + "\",\"description\": \"" + ps.description + "\""+image+"}";
        JavaScriptInjection.InjectInternalScript(ss, s, "application/ld+json");
    }

}
