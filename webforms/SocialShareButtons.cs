using sunamo;
using sunamo.Values;
using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
public class SocialShareButtons
{
    /// <summary>
    /// Nezapomeň že tento tag se dává až na úplný konec stránky, pro tyto účely bude mít každá MasterPage ještě jeden placeholder se jménem last
    /// Toto mi vygeneroval generátor +1 buttonu na google developers před tímto tagem: Place this tag after the last +1 button tag.
    /// </summary>
    /// <param name="page"></param>
    /// <returns></returns>
    public static void SetPlusOneJS(HtmlGenericControl html)
    {
        
    }

    /// <summary>
    /// Nwzapomeň že všude kde chceš mít FB like tlačítko musíš dát hned po tagu body <div id="fb-root"></div>
    /// </summary>
    /// <param name="page"></param>
    public static void InjectLikeJS(Page page)
    {
        //JavascriptInjection.RegisterClientScriptInclude(page, );
    }

    public static void SetPlusOneHtml(Page page, HtmlGenericControl html)
    {
        html.InnerHtml = "<div class=\"g-plusone\" data-annotation=\"bubble\" data-width=\"300\" data-recommendations=\"false\" data-annotation=\"none\" data-href=\""+ GetUri(page.Request.Url) +"\"></div>"
        + Environment.NewLine +
"<script type=\"text/javascript\"" + @">
  window.___gcfg = {lang: 'cs'};

  (function() {
    var po = document.createElement('script'); po.type = 'text/javascript'; po.async = true;
    po.src = 'https://apis.google.com/js/plusone.js';
    var s = document.getElementsByTagName('script')[0]; s.parentNode.insertBefore(po, s);
  })();
</script>";
    }

    private static string GetUri(Uri p)
    {
        string host = SH.ReplaceOnceIfStartedWith(p.Host, "www.", "");
        if (host == Consts.Cz)
        {
            if (p.PathAndQuery.ToLower().StartsWith("/default.aspx"))
            {
                return "http://" + Consts.WwwCz + "/";
            }
        }
        // Toto nevím jestli někdy může nastat, nemělo by
        return p.ToString().ToLower();
    }

    /// <summary>
    /// Toto absolutně nefunguje. Zjistil sjem že do obalujícího divu fbLike se mi taky automaticky přidají některé prvky, jako například data-href="" class="fb-like" data-layout="button" data-action="like" data-show-faces="false" data-share="false". Divné je že data-href je vždy SE, kdežto href v fb:like je nastavený
    /// </summary>
    /// <param name="page"></param>
    /// <param name="first"></param>
    /// <param name="html"></param>
    public static void SetLikeXfbml(Page page, HtmlGenericControl first, HtmlGenericControl html)
    {
        first.InnerHtml = "<div id=\"fb-root\"></div><script>(function(d, s, id) {var js, fjs = d.getElementsByTagName(s)[0];if (d.getElementById(id)) return;js = d.createElement(s); js.id = id;js.src = \"//connect.facebook.net/en_US/all.js#xfbml=1\";fjs.parentNode.insertBefore(js, fjs);}(document, 'script', 'facebook-jssdk'));</script>";
        html.InnerHtml = string.Format("<fb:like href=\"{0}\" layout=\"box_count\" action=\"like\" show_faces=\"false\" share=\"false\"></fb:like>",  GetUri(page.Request.Url));
    }

    public static void SetLikeAttributes(Page page, HtmlGenericControl html)
    {
        string adresa = HttpUtility.HtmlEncode(GetUri(page.Request.Url));
        html.Attributes.Add("data-href", adresa);
        html.Attributes.Add("class", "fb-like");
        html.Attributes.Add("data-layout", "button_count");
        html.Attributes.Add("data-action", "like");
        html.Attributes.Add("data-show-faces", "true");
        html.Attributes.Add("data-share", "false");
    }

    /// <summary>
    /// Toto nefunguje asi nikde, zkus použít metodu SetLikeHtml5
    /// </summary>
    /// <param name="page"></param>
    /// <param name="html"></param>
    public static void SetLikeIframe(Page page, HtmlGenericControl html)
    {
        html.InnerHtml = "<iframe src=\"//www.facebook.com/plugins/like.php?href="+ UH.UrlEncode( GetUri(page.Request.Url)) + "&amp;width&amp;layout=box_count&amp;action=like&amp;show_faces=false&amp;share=false&amp;height=65\" scrolling=\"no\" frameborder=\"0\" style=\"border:none; overflow:hidden; height:65px;\" allowTransparency=\"true\"></iframe>";
    }

    public static void SetLikeHtml5(Page page, HtmlGenericControl html)
    {
        html.Attributes["data-href"] = GetUri(page.Request.Url).Replace("&", "&amp;");
    }

    /// <summary>
    /// Toto můžeš použít pouze na hlavní stránce celého webu. Nikde jinde to nefunguje.
    /// </summary>
    /// <param name="page"></param>
    /// <param name="first"></param>
    /// <param name="html"></param>
    public static void SetLikeHtml5(Page page, HtmlGenericControl first, HtmlGenericControl html)
    {
        first.InnerHtml = "<div id=\"fb-root\"></div>" + @"
<script>(function(d, s, id) {
  var js, fjs = d.getElementsByTagName(s)[0];
  if (d.getElementById(id)) return;
  js = d.createElement(s); js.id = id;
  js.src = " + "\"/" + "/connect.facebook.net/en_US/all.js#xfbml=1\";" + Environment.NewLine +
  @"fjs.parentNode.insertBefore(js, fjs);
}(document, 'script', 'facebook-jssdk'));</script>";
        html.InnerHtml = string.Format("<div class=\"fb-like\" data-href=\"{0}\" data-layout=\"button_count\" data-width=\"{1}\" data-colorscheme=\"light\" data-layout=\"standard\" data-action=\"like\" data-show-faces=\"false\" data-send=\"false\"></div>", GetUri( page.Request.Url), "http://" + page.Request.Url.Host + "/");
    }



    public static void SetTweetHtml(Page page, HtmlGenericControl html)
    {
        html.InnerHtml = "<a href=\"https://twitter.com/share\"  style=\"min-width: 100px;\"  data-url=\"{0}\" class=\"naStred twitter-share-button\">Tweet</a><script>!function(d,s,id){var js,fjs=d.getElementsByTagName(s)[0];if(!d.getElementById(id)){js=d.createElement(s);js.id=id;js.src='//platform.twitter.com/widgets.js';fjs.parentNode.insertBefore(js,fjs);}}(document,'script','twitter-wjs');</script>".Replace("{0}", GetUri(page.Request.Url));
    }
}
