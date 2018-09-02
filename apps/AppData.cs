using sunamo;
using sunamo.Essential;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Search;

namespace apps
{
    public class AppData
    {
         static AppData()
        {
            //CreateAppFoldersIfDontExists();
        }

        public async static Task<bool> CreateAppFoldersIfDontExists()
        {
            if (!string.IsNullOrEmpty(ThisApp.Name))
            {
                foreach (AppFolders item in Enum.GetValues(typeof(AppFolders)))
                {
                    StorageFolder sf = await GetFolder(item);
                }
            }
            else
            {
                    throw new Exception("The application name is not specified!");
            }
            return true;
        }

        

        /// <summary>
        /// If file A1 dont exists or have empty content, then create him with empty content and G false
        /// </summary>
        /// <param name="path"></param>
        public static async Task<bool> ReadFileOfSettingsBool(string path, bool _def)
        {
            StorageFile sf = null;
            sf = await AppData.GetFile(AppFolders.Settings, path);
            string content = await apps.TF.ReadFile(sf);
            bool vr = false;
            if (bool.TryParse(content.Trim(), out vr))
            {
                return vr;
            }
            return _def;
        }

        public async static Task<int> ReadFileOfSettingsInt(string name, int def)
        {
            return sunamo.BT.TryParseInt(await ReadFile(AppFolders.Settings, name), def);
        }

        public async static Task<int> ReadFileOfSettingsIntValues(string name, int def, List<int> c, bool lowerOrEqual, bool larger)
        {
            int nt = sunamo.BT.TryParseInt(await ReadFile(AppFolders.Settings, name), def);
            if (!c.Contains(nt))
            {
                nt = def;
            }
            else
            {
                if (c.Contains(nt))
                {
                    return nt;
                }
                if (c.Count > 1)
                {
                    // Nejdřív musím zjistit mezi kterými 2mi čísly to je valstně
                    for (int i = 0; i < c.Count - 1; i++)
                    {
                        if (nt >= c[i] && nt <= c[i + 1])
                        {
                            if (larger)
                            {
                                nt = c[i + 1];
                                break;
                            }
                            else
                            {
                                nt = c[i];
                                break;
                            }
                        }
                    }
                }
                else
                {
                    if (larger)
                    {
                        nt = c[1];
                    }
                    else
                    {
                        nt = c[0];
                    }
                }
            }
            if (lowerOrEqual)
            {
                if (nt > def)
                {
                    nt = def;
                }
            }
            return nt;
        }

        public async static Task<int> ReadFileOfControlsInt(string name, int def)
        {
            return sunamo.BT.TryParseInt(await ReadFile(AppFolders.Controls, name), def);
        }

        public async static Task<bool> ReadFileOfControlsBool(string name, bool def)
        {
            return sunamo.BT.TryParseBool(await ReadFile(AppFolders.Controls, name), def);
        }

        public async static Task< string> ReadFileOfControls(string name)
        {
            return await AppData.ReadFile(AppFolders.Controls, name);
        }

        /// <summary>
        /// If file A1 dont exists or have empty content, then create him with empty content and G SE
        /// </summary>
        /// <param name="path"></param>
        public static async Task<string> ReadFileOfSettingsOther(string filename)
        {
            StorageFile sf = null;
                sf = await AppData.GetFile(AppFolders.Settings, filename);
            string vr =await TF.ReadFile(sf);
            return vr;
        }

        public async static  Task<string> ReadFile(AppFolders af, string filename)
        {
            StorageFile sf = null;
                sf = await AppData.GetFile(af, filename);

            

            //TF.CreateEmptyFileWhenDoesntExists(path);
            return await TF.ReadFile(sf);
        }

        /// <summary>
        /// Save file A1 to folder AF Settings with value A2.
        /// </summary>
        /// <param name="file"></param>
        /// <param name="value"></param>
        public async static  Task SaveFileOfSettings(string file, string value)
        {
            StorageFile fileToSave = await GetFile(AppFolders.Settings, file);
            await TF.SaveFile(value, fileToSave);
        }

        /// <summary>
        /// Save file A2 to AF A1 with contents A3
        /// </summary>
        /// <param name="af"></param>
        /// <param name="file"></param>
        /// <param name="value"></param>
        public async static Task SaveFile(AppFolders af, string file, string value)
        {
            StorageFile fileToSave = await GetFile(af, file);
            await TF.SaveFile(value, fileToSave);
        }

        public async static Task<T> ReadFileOfSettingsEnum<T>(string fnAudioType, T def) where T : struct, IConvertible
        {
            T t = default(T);
            string e = await ReadFileOfSettingsOther(fnAudioType);
            if( Enum.TryParse<T>(fnAudioType, out t))
            {
                return t;
            }
            return def;
        }

        /// <summary>
        /// Just call TF.SaveFile
        /// </summary>
        /// <param name="file"></param>
        /// <param name="value"></param>
        public async static void SaveFile(StorageFile file, string value)
        {
            await TF.SaveFile(value, file);
        }

        /// <summary>
        /// Append to file A2 in AF A1 with contents A3
        /// </summary>
        /// <param name="af"></param>
        /// <param name="file"></param>
        /// <param name="value"></param>
        public static async void AppendToFile(AppFolders af, string file, string value)
        {
            StorageFile fileToSave = await GetFile(af, file);
            await TF.AppendToFile(value, fileToSave);
        }

        public static async Task<StorageFolder> GetFolder(AppFolders af)
        {
            StorageFolder sunamo = await FSApps.ExistsFolderCreateIfNot( Windows.Storage.ApplicationData.Current.LocalFolder, "sunamo");
            StorageFolder ja = await FSApps.ExistsFolderCreateIfNot(sunamo, ThisApp.Name);
            StorageFolder appFolder = await FSApps.ExistsFolderCreateIfNot(ja, af.ToString());
            return appFolder;
        }

        /// <summary>
        /// G path file A2 in AF A1.
        /// Automatically create upfolder if there dont exists.
        /// </summary>
        /// <param name="af"></param>
        /// <param name="file"></param>
        /// <returns></returns>
        public static async Task<StorageFile> GetFile(AppFolders af, string file)
        {
            StorageFolder sunamo = await Windows.Storage.ApplicationData.Current.LocalFolder.GetFolderAsync("sunamo");
            StorageFolder ja = await sunamo.GetFolderAsync(ThisApp.Name);
            StorageFolder appFolder = await ja.GetFolderAsync(af.ToString());
            StorageFile vr = await appFolder.CreateFileAsync(file, CreationCollisionOption.OpenIfExists);
            return vr;
        }

        public static async Task<StorageFile> Combine(AppFolders appFolders, string p1, string p2)
        {
            StorageFolder af = await GetFolder(appFolders);
            StorageFolder q1 = await FSApps.ExistsFolderCreateIfNot(af, p1);
            StorageFile q2 = await FSApps.ExistsFileCreateIfNot(q1, p2);
            return q2;
        }

        public async static Task<List<StorageFile>> GetFiles(AppFolders cache, string mask, string ext)
        {
            List<StorageFile> vr = new List<StorageFile>();
            mask = mask + ext;
            StorageFolder sfCache = await GetFolder(cache);
            QueryOptions qo = new QueryOptions(CommonFileQuery.DefaultQuery, CA.ToListString(ext));
            StorageFileQueryResult sfqr = sfCache.CreateFileQueryWithOptions(qo);
            var files = await sfqr.GetFilesAsync();
            foreach (var item in files)
            {
                //if (Wildcard.IsMatch(item.Name, mask))
                if(SH.MatchWildcard(item.Name, mask))
                {
                    vr.Add(item);
                }
            }
            return vr;
        }
    }
}
