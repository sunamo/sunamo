using sunamo;
using sunamo.Essential;
using sunamo.Values;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

    public class AppData : AppDataAbstractBase<string, string>
    {
        public static AppData ci = new AppData();

        private AppData() 
        {
            
        }

        public override string GetFile(AppFolders af, string file)
        {
                string slozka2 = FS.Combine(RootFolder, af.ToString());
                string soubor = FS.Combine(slozka2, file);
                return soubor;
        }

        public override string GetFolder(AppFolders af)
        {
                string vr = FS.Combine(RootFolder, af.ToString());
                return vr;   
        }

        public override bool IsRootFolderOk()
        {
            if (string.IsNullOrEmpty(rootFolder))
            {
                return false;
            }

            return FS.ExistsDirectory(rootFolder);
        }

    public string ReadFolderWithAppsFilesOrDefault(string s)
    {
        var content = TF.ReadFile(s);
        if (content == string.Empty)
        {
            return RootFolderCommon(false);
        }
        return content;
    }

    public override bool IsRootFolderNull()
        {
            var def = default(string);
            if(! EqualityComparer<string>.Default.Equals(rootFolder, def))
            {
            // is not null
            return rootFolder == string.Empty;
            }
        return true;
        }

        public override void AppendToFile(string content, string sf)
        {
            TF.AppendToFile(content, sf);
        }



    /// <summary>
    /// Ending with name of app
    /// </summary>
    /// <returns></returns>
    public override string GetRootFolder()
        {
        rootFolder = GetSunamoFolder();

            RootFolder = FS.Combine(rootFolder, ThisApp.Name);
            FS.CreateDirectory(RootFolder);
            return RootFolder;
        }

        protected override void SaveFile(string content, string sf)
        {
            TF.SaveFile(content, sf);
        }

        public override void AppendToFile(AppFolders af, string file, string value)
        {
            throw new NotImplementedException();
        }

    public override string GetSunamoFolder()
    {
        string r = AppData.ci.GetFolderWithAppsFiles();
        string sunamoFolder = TF.ReadFile(r);

        

        if (string.IsNullOrWhiteSpace(sunamoFolder))
        {
            sunamoFolder = FS.Combine(SpecialFoldersHelper.AppDataRoaming(), Consts.@sunamo);
        }
        return sunamoFolder;
    }
    //
    /// <summary>
    /// Without ext because all is crypted and in bytes
    /// </summary>
    /// <param name="filename"></param>
    /// <returns></returns>
    public override string GetFileCommonSettings(string filename)
    {
        return FS.Combine(GetSunamoFolder(), "Common", AppFolders.Settings.ToString(), filename);
    
    }

    public override string GetCommonSettings(string key)
    {
        var file = GetFileCommonSettings(key);
        return Encoding.UTF8.GetString( CryptHelper.RijndaelBytes.Instance.Decrypt(TF.ReadAllBytes(file)).ToArray());
        
    }

    public override void SetCommonSettings(string key, string value)
    {
        var file = GetFileCommonSettings(key);
        TF.WriteAllBytes(file, CryptHelper.RijndaelBytes.Instance.Encrypt(Encoding.UTF8.GetBytes( value).ToList()));
    }
}

