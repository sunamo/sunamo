
using System.Web;

public static class HttpHandlerHelper
{
    public static LoginCookie GetLoginCookie(HttpRequest req)
    {
        return SunamoMasterPage.GetLoginCookie(req);
    }

}
