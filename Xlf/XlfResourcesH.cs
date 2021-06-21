using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using SunamoExceptions;
using Xlf;
using XliffParser;

/// <summary>
/// Must be in shared
/// In sunamo is not XliffParser and fmdev.ResX - these projects requires .net fw due to CodeDom
/// </summary>
public partial class XlfResourcesH
{
    public static bool initialized = false;
    static Type type = typeof(XlfResourcesH);



    public static string PathToXlfSunamo(Langs l)
    {
        var p = @"E:\Documents\vs\Projects\sunamo\sunamo\MultilingualResources\sunamo.";
        switch (l)
        {
            case Langs.cs:
                p += "cs-CZ";
                break;
            case Langs.en:
                p += "en-US";
                break;
            default:
                ThrowExceptions.NotImplementedCase(Exc.GetStackTrace(), type, Exc.CallingMethod(), l);
                break;
        }

        return p + AllExtensions.xlf;
    }

    static string previousKey = null;

    #region Main worker
    #region Less sophisficated - Loading always from file
    ///// <summary>
    ///// 2. loading from xlf files
    ///// </summary>
    ///// <typeparam name="StorageFolder"></typeparam>
    ///// <typeparam name="StorageFile"></typeparam>
    ///// <param name="basePath"></param>
    ///// <param name="existsDirectory"></param>
    ///// <param name="appData"></param>
    //public static string SaveResouresToRL<StorageFolder, StorageFile>(string key, string basePath, ExistsDirectory existsDirectory)
    //{
    //    if (previousKey == key && previousKey != null)
    //    {
    //        return null;
    //    }

    //    previousKey = key;

    //    // cant be inicialized - after cs is set initialized to true and skip english
    //    //initialized = true;

    //    var path = Path.Combine(basePath, "MultilingualResources");

    //    var files = FS.GetFiles(path, "*.xlf", SearchOption.TopDirectoryOnly);
    //    foreach (var file3 in files)
    //    {
    //        var lang = XmlLocalisationInterchangeFileFormatXlf.GetLangFromFilename(file3);
    //        ProcessXlfFile(path, lang.ToString(), file3);
    //    }



    //    return key;
    //}
    #endregion

    #region More sophisficated - If is not my computer, reading from resources
    /// <summary>
    /// A2 can be string.Empty
    /// </summary>
    /// <typeparam name="StorageFolder"></typeparam>
    /// <typeparam name="StorageFile"></typeparam>
    /// <param name="key"></param>
    /// <param name="basePath"></param>
    /// <param name="existsDirectory"></param>
    /// <param name="appData"></param>
    /// <returns></returns>
    public static string SaveResouresToRL<StorageFolder, StorageFile>(string key, string basePath, ExistsDirectory existsDirectory, IAppDataBase<StorageFolder, StorageFile> appData)
    {
        
        if (previousKey == key && previousKey != null)
        {
            return null;
        }

        previousKey = key;

        // cant be inicialized - after cs is set initialized to true and skip english
        //initialized = true;

        var path = Path.Combine(basePath, "MultilingualResources");

        Type type = typeof(Resources.ResourcesDuo);

        //ResourcesHelper rm = ResourcesHelper.Create("standard.Properties.Resources", type.Assembly);
        ResourcesHelperXlf rm = ResourcesHelperXlf.Create("Resources.ResourcesDuo", type.Assembly);

        var exists = false;

        exists = MyPc.Instance.IsMyComputerOrVps();


        if (appData == null)
        {
            // Is web app
            exists = true;
        }

        //exists = false;

        // This is totally important
        // Otherwise is loading in non UWP apps from resx
        if (!exists)
        {
            String xlfContent = null;

            var fn = "sunamo_cs_CZ";

            if (true) //PlatformInteropHelperXlf.IsSellingApp())
            {
                fn += "_min";
            }

            var file = appData.GetFileCommonSettings(fn + ".xlf");

            // Cant use StorageFile.ToString - get only name of method
            //pathFile = file.ToString();

            var enc = Encoding.GetEncoding(65001);

            xlfContent = rm.GetByteArrayAsString(fn);

            FS.CreateUpfoldersPsysicallyUnlessThere(file);
            //xlfContent = xlfContent.Skip(3);
            File.WriteAllText(file, xlfContent, enc);
            TFXlf.RemoveDoubleBomUtf8(file);

            fn = "sunamo_en_US";

            if (true) //PlatformInteropHelperXlf.IsSellingApp())
            {
                fn += "_min";
            }

            var file2 = appData.GetFileCommonSettings(fn + ".xlf");

            xlfContent = rm.GetByteArrayAsString(fn);
            //xlfContent = xlfContent.Skip(3);
            File.WriteAllText(file2, xlfContent, enc);
            TFXlf.RemoveDoubleBomUtf8(file2);

            path = Path.Combine(appData.RootFolderCommon(true), "Settings");
            //path = appData.RootFolderCommon(false);
        }

        ProcessXlfFiles(path);

        return key;
    }

    private static void ProcessXlfFiles(string path)
    {
        var files = FS.GetFiles(path, "*.xlf", SearchOption.TopDirectoryOnly);
        files.RemoveAll(d => d.EndsWith(".min.xlf"));
        foreach (var file3 in files)
        {
            var lang = XmlLocalisationInterchangeFileFormatXlf.GetLangFromFilename(file3);
            ProcessXlfFile(path, lang.ToString(), file3);
        }
    }
    #endregion
    #endregion

    public static string SaveResouresToRL(string VpsHelperSunamo_SunamoProject)
    {
        return XlfResourcesH.SaveResouresToRL<string, string>(null, VpsHelperSunamo_SunamoProject, FS.ExistsDirectoryNull);
    }

    public static string SaveResouresToRL<StorageFolder, StorageFile>(string key, string basePath, ExistsDirectory existsDirectory)
    {
        return SaveResouresToRL<StorageFolder, StorageFile>(key, basePath, existsDirectory, null);
    }
    public static Dictionary<string, string> LoadXlfDocument(string file)
    {
        var doc = new XlfDocument(file);
        return GetTransUnits(doc);
    }

    public static Dictionary<string, string> GetTransUnits(XlfDocument doc)
    {
        Dictionary<string, string> result = new Dictionary<string, string>();

        var xlfFiles = doc.Files;
        if (xlfFiles.Count() != 0)
        {
            // Win every xlf will be t least two WPF.TESTS/PROPERTIES/RESOURCES.RESX and WPF.TESTS/RESOURCES/EN-US.RESX
            foreach (var item in xlfFiles)
            {
                // like WPF.TESTS/PROPERTIES/
                if (item.Original.EndsWith("/RESOURCES.RESX"))
                {
                    if (item.TransUnits.Count() > 0)
                    {

                        Debugger.Break();
                    }
                }

                foreach (var tu in item.TransUnits)
                {
                    if (!result.ContainsKey(tu.Id))
                    {
                        result.Add(tu.Id, tu.Target);
                    }
                }
            }
        }

        return result;
    }

    private static void ProcessXlfFile(string basePath, string lang, string file)
    {
        var fn = Path.GetFileName(file).ToLower();
        bool isCzech = fn.Contains("cs");
        bool isEnglish = fn.Contains("en");

        var doc = new XlfDocument(file);
        //var doc = new XlfDocument(@"C:\Users\w\AppData\Local\Packages\31735sunamo.GeoCachingTool_q65n5amar4ntm\LocalState\sunamo.cs-CZ.xlf");
        lang = lang.ToLower();

        var xlfFiles = doc.Files.Where(d => d.Original.ToLower().Contains(lang));
        if (xlfFiles.Count() != 0)
        {
            var xlfFile = xlfFiles.First();

            foreach (var u in xlfFile.TransUnits)
            {
                if (isCzech)
                {
                    if (!RLData.cs.ContainsKey(u.Id))
                    {
                        RLData.cs.Add(u.Id, u.Target);
                    }
                }
                else if (isEnglish)
                {
                    if (!RLData.en.ContainsKey(u.Id))
                    {
                        RLData.en.Add(u.Id, u.Target);
                    }
                }
                else
                {
                    ThrowExceptions.Custom(Exc.GetStackTrace(), type, Exc.CallingMethod(), "Invalid file " + file + ", please delete it");
                }
            }
        }
    }
}