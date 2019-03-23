using sunamo;
using sunamo.Essential;
using sunamo.Values;
using System;
using System.Collections.Generic;
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

        public override bool IsRootFolderNull()
        {
        var def = default(string);
            return EqualityComparer<string>.Default.Equals(rootFolder, def);
        }

        public override void AppendToFile(string content, string sf)
        {
            TF.AppendToFile(content, sf);
        }

        /// <summary>
        /// Ending with name of app
        /// </summary>
        /// <returns></returns>
        protected override string GetRootFolder()
        {
            string r = AppData.ci.GetFolderWithAppsFiles();
            rootFolder = TF.ReadFile(r);
            if (string.IsNullOrWhiteSpace(rootFolder))
            {
                rootFolder = FS.Combine(SpecialFoldersHelper.AppDataRoaming(), Consts.@sunamo);
            }

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
    }

