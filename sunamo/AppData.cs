using sunamo;
using sunamo.Essential;
using sunamo.Values;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class AppData : AppDataAbstractBase<string, string>
{
    public static AppData ci = new AppData();
    static Type type = typeof(AppData);
    private AppData()
    {
    }

    public override string GetFileInSubfolder(AppFolders output, string subfolder, string file, string ext)
    {
        return AppData.ci.GetFile(AppFolders.Output, subfolder + "\\" + file + ext);
    }

    /// <summary>
    /// Return always in User's AppData
    /// </summary>
    /// <param name="inFolderCommon"></param>
    
    public override string GetFileCommonSettings(string filename)
    {
        var vr = FS.Combine(RootFolderCommon(true), AppFolders.Settings.ToString(), filename);
        return vr;
    }

    public override string GetCommonSettings(string key)
    {
        var file = GetFileCommonSettings(key);
        var vr = Encoding.UTF8.GetString(CryptHelper.RijndaelBytes.Instance.Decrypt(TF.ReadAllBytes(file)).ToArray());
        vr = vr.Replace("\0", "");
        return vr;
    }

    public override void SetCommonSettings(string key, string value)
    {
        var file = GetFileCommonSettings(key);
        TF.WriteAllBytes(file, CryptHelper.RijndaelBytes.Instance.Encrypt(Encoding.UTF8.GetBytes(value).ToList()));
    }

   

}

