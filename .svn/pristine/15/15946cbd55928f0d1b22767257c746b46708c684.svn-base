using System.Drawing;
using System.IO;
using System.Web;
using System.Web.UI;
public static class GravatarHelper
{
    public const string folderAvatars = "_/temp/avatars";
    const string avatareExt = ".jpg";

    /// <summary>
    /// Zadej do A1 true pokud jsi již avatar stáhl na server
    /// </summary>
    /// <param name="nick"></param>
    /// <returns></returns>
    public static string GetGravatarUri(HttpRequest p, string nick)
    {
        if (File.Exists(GetGravatarPath(nick)))
        {
            return web.UH.GetWebUri(p, folderAvatars, nick + avatareExt);
        }
        return web.UH.GetWebUri(p, "img", "gravatar_logo_28x28.jpg");
    }

    public static string GetGravatarPath(string nick)
    {
        string cesta = nick + avatareExt;
        
        return GeneralHelper.MapPath(folderAvatars + "/" + cesta);
    }
}
