using System.Web;
using System.Web.UI;
/// <summary>
/// Některé metody jsou v GeneralUri
/// </summary>
public static class MeUri
{
    public static string Login(Page p)
    {
        return "http://" + p.Request.Url.Host + "/Me/Login.aspx";
    }

    public static string Logout(Page p)
    {
        return "http://" + p.Request.Url.Host + "/Me/Logout.aspx";
    }

    public static string User(HttpRequest req, string un)
    {
        return "http://" + req.Url.Host + "/Me/User.aspx?un=" + un;
    }





    private static string GetWebUri(SunamoPage sp, string p)
    {
        return web.UH.GetWebUri(sp, "Me/" + p);
    }

    public static string ChangeProfilePicture(SunamoPage sp)
    {
        return GetWebUri(sp, "ChangeProfilePicture.aspx");
    }

    public static string LoginWithReturnUrl(Page p)
    {
        return SunamoCzMetroUIHelper.GetAnchorWithReturnUri(MeUri.Login(p), p.Request);
    }

    public static string LogoutWithReturnUrl(Page p)
    {
        return SunamoCzMetroUIHelper.GetAnchorWithReturnUri(MeUri.Logout(p), p.Request);
    }
}
