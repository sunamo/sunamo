
using System;
using System.Collections.Generic;
using System.Linq;

using System.Web.UI;
using System.Web.UI.HtmlControls;

public static class OpenGraphHelper
{
    public static void InsertBasicToPageHeader(SunamoPage page, PageSnippet ps, MySites ms)
    {
        if (page.Header != null)
        {
            var type = new HtmlMeta { Content = "article" };
            type.Attributes.Add("property", "og:type");
            page.Header.Controls.AddAt(0, type);

            var locale = new HtmlMeta { Content = "cs_cz" };
            locale.Attributes.Add("property", "og:locale");
            page.Header.Controls.AddAt(0, locale);

            if (ps.image == "")
            {
                ps.image = web.UH.GetWebUri3(page.Request, "img/" + ms.ToString() + "/" + "ImplicitShareImage.jpg");
            }

            var imageUri = new HtmlMeta { Content = ps.image };
            imageUri.Attributes.Add("property", "og:image:url");
            page.Header.Controls.AddAt(0, imageUri);

            var image = new HtmlMeta { Content = ps.image };
            image.Attributes.Add("property", "og:image");
            page.Header.Controls.AddAt(0, image);

            var site_name = new HtmlMeta { Content = ps.title };
            site_name.Attributes.Add("property", "og:site_name");
            page.Header.Controls.AddAt(0, site_name);

            var description = new HtmlMeta { Content = ps.description };
            description.Attributes.Add("property", "og:description");
            page.Header.Controls.AddAt(0, description);

            var title = new HtmlMeta { Content = ps.title };
            title.Attributes.Add("property", "og:title");
            page.Header.Controls.AddAt(0, title);

        }
    }
}
