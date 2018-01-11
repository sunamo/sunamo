using System.IO;
using System.Web;
using System.Web.Hosting;

public static class AF
{
    /// <summary>
    /// G path file A2 in AF A1.
    /// Automatically create upfolder if there dont exists.
    /// </summary>
    /// <param name="af"></param>
    /// <param name="file"></param>
    /// <returns></returns>
    public static string GetFile(AppFolders af, string file)
    {
        string vr = HostingEnvironment.MapPath("~/_");
        vr = Path.Combine(vr, af.ToString());
        vr = Path.Combine(vr, file);
        return vr;
    }

    public static string GetFolder(AppFolders af)
    {
        string vr = HttpContext.Current.Request.MapPath("M");
        vr = Path.Combine(vr, af.ToString());
        return vr;
    }
}
