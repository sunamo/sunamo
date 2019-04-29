﻿using System.Drawing;
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
        if (FS.ExistsFile(GetGravatarPath(nick)))
        {
            return UA.GetWebUri(p, folderAvatars, nick + avatareExt);
        }
        return UA.GetWebUri(p, "img", "gravatar_logo_28x28.jpg");
    }

    public static string GetGravatarPath(string nick)
    {
        string cesta = nick + avatareExt;
        
        return GeneralHelper.MapPath(folderAvatars + AllStrings.slash + cesta);
    }
}
