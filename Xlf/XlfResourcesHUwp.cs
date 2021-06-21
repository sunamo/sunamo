//using System;
//using System.Collections;
//using System.Collections.Generic;
//using System.Diagnostics;
//using System.Globalization;
//using System.IO;
//using System.Linq;
//using System.Resources;
//using System.Text;
//using System.Threading.Tasks;
//using SunamoExceptions;
//using XliffParser;

///// <summary>
///// Must be in shared
///// In sunamo is not XliffParser and fmdev.ResX - these projects requires .net fw due to CodeDom
///// </summary>
//public class XlfResourcesHUwp
//{
//    public static bool initialized = false;

//    /// <summary>
//    /// 1. Entry method 
//    /// </summary>
//    /// <typeparam name="StorageFolder"></typeparam>
//    /// <typeparam name="StorageFile"></typeparam>
//    /// <param name="existsDirectory"></param>
//    /// <param name="appData"></param>
//    public static void SaveResouresToRLSunamo<StorageFolder, StorageFile>(ExistsDirectory existsDirectory, AppDataBase<StorageFolder, StorageFile> appData)
//    {
//        //var sunamoAssembly = typeof(Resources).Assembly;

//        //var resources2 = sunamoAssembly.GetManifestResourceNames();

//        //var resourceManager = new ResourceManager("sunamo.Properties.Resources", sunamoAssembly);
//        //var resources = resourceManager.GetResourceSet(CultureInfo.CurrentUICulture, true, true);
//        //foreach (var res in resources)
//        //{
//        //    var v = ((DictionaryEntry)res).Key;
//        //    System.Console.WriteLine(v);
//        //}

//        string path = null;

//        if (!PlatformInteropHelper.IsUwpWindowsStoreApp())
//        {
//            path = DefaultPaths.sunamoProject;
//        }
//        else
//        {
//            path = appData.RootFolderCommon(false);
//        }

//        SaveResouresToRL<StorageFolder, StorageFile>(path, existsDirectory, appData);
//    }

//    /// <summary>
//    /// 1. Entry method 
//    /// Only for non-UWP apps
//    /// </summary>
//    public static void SaveResouresToRLSunamo()
//    {
//        SaveResouresToRL<string, string>(new ExistsDirectory(FS.ExistsDirectoryNull), AppData.ci);
//    }

//    public static string PathToXlfSunamo(Langs l)
//    {
//        var p = @"E:\Documents\vs\Projects\sunamo\sunamo\MultilingualResources\sunamo.";
//        switch (l)
//        {
//            case Langs.cs:
//                p += "cs-CZ";
//                break;
//            case Langs.en:
//                p += "en-US";
//                break;
//            default:
//                ThrowExceptions.NotImplementedCase(Exc.GetStackTrace(), type, Exc.CallingMethod(), l);
//                break;
//        }

//        return p + AllExtensions.xlf;
//    }

//    public static void SaveResouresToRL(string path)
//    {
//        SaveResouresToRL<string, string>(path, new ExistsDirectory(FS.ExistsDirectoryNull), AppData.ci);
//    }

//    /// <summary>
//    /// 2. loading from xlf files
//    /// </summary>
//    /// <typeparam name="StorageFolder"></typeparam>
//    /// <typeparam name="StorageFile"></typeparam>
//    /// <param name="basePath"></param>
//    /// <param name="existsDirectory"></param>
//    /// <param name="appData"></param>
//    public static void SaveResouresToRL<StorageFolder, StorageFile>(string basePath, ExistsDirectory existsDirectory, AppDataBase<StorageFolder, StorageFile> appData)
//    {
//        // cant be inicialized - after cs is set initialized to true and skip english
//        //initialized = true;

//        var path = Path.Combine(basePath, "MultilingualResources");

//        Type type = PlatformInteropHelper.GetTypeOfResources();

//        //ResourcesHelper rm = ResourcesHelper.Create("standard.Properties.Resources", type.Assembly);
//        ResourcesHelper rm = ResourcesHelper.Create("Resources.ResourcesDuo", type.Assembly);

//        var exists = false;

//        if (PlatformInteropHelper.IsUwpWindowsStoreApp())
//        {
//            // keep exists on false
//        }
//        else
//        {
//            exists = FS.ExistsDirectory(path);
//        }

//        // This is totally important
//        // Otherwise is loading in non UWP apps from resx
//        if (!exists)
//        {
//            String xlfContent = null;

//            var fn = "sunamo_cs_CZ";


//            var file = appData.GetFileCommonSettings(fn + ".xlf");


//            // Cant use StorageFile.ToString - get only name of method
//            //pathFile = file.ToString();

//            var enc = Encoding.GetEncoding(65001);

//            xlfContent = rm.GetByteArrayAsString(fn);
//            //xlfContent = xlfContent.Skip(3);
//            TF.WriteAllText(file, xlfContent, enc);
//            TF.RemoveDoubleBomUtf8(file);


//            fn = "sunamo_en_US";

//            var file2 = appData.GetFileCommonSettings(fn + ".xlf");


//            xlfContent = rm.GetByteArrayAsString(fn);
//            //xlfContent = xlfContent.Skip(3);
//            TF.WriteAllText(file2, xlfContent, enc);
//            TF.RemoveDoubleBomUtf8(file2);

//            path = FS.Combine(appData.RootFolderCommon(true), "Settings");
//            //path = appData.RootFolderCommon(false);
//        }


//        var files = FS.GetFiles(path, "*.xlf", SearchOption.TopDirectoryOnly);
//        foreach (var file3 in files)
//        {
//            var lang = XmlLocalisationInterchangeFileFormatSunamo.GetLangFromFilename(file3);
//            ProcessXlfFile(path, lang.ToString(), file3);
//        }

//        if (RLData.en.ContainsKey(XlfKeys.LocationOfCaches))
//        {

//        }
//    }

//    /// <summary>
//    /// 2. loading from xlf files
//    /// Private to use SaveResouresToRLSunamo
//    /// </summary>
//    private static void SaveResouresToRL<StorageFolder, StorageFile>(ExistsDirectory existsDirectory, AppDataBase<StorageFolder, StorageFile> appData)
//    {
//        // Cant use SolutionsIndexerHelper.SolutionWithName or VPSHelper because is in SolutionsIndexer.web

//        SaveResouresToRL(VpsHelperSunamo.SunamoProject(), existsDirectory, appData);
//    }

//    public static Dictionary<string, string> LoadXlfDocument(string file)
//    {
//        var doc = new XlfDocument(file);
//        return GetTransUnits(doc);
//    }

//    public static Dictionary<string, string> GetTransUnits(XlfDocument doc)
//    {
//        Dictionary<string, string> result = new Dictionary<string, string>();

//        var xlfFiles = doc.Files;
//        if (xlfFiles.Count() != 0)
//        {

//            // Win every xlf will be t least two WPF.TESTS/PROPERTIES/RESOURCES.RESX and WPF.TESTS/RESOURCES/EN-US.RESX


//            foreach (var item in xlfFiles)
//            {
//                // like WPF.TESTS/PROPERTIES/
//                if (item.Original.EndsWith("/RESOURCES.RESX"))
//                {
//                    if (item.TransUnits.Count() > 0)
//                    {

//                        Debugger.Break();
//                    }
//                }

//                foreach (var tu in item.TransUnits)
//                {
//                    if (!result.ContainsKey(tu.Id))
//                    {
//                        result.Add(tu.Id, tu.Target);
//                    }
//                }
//            }
//        }

//        return result;
//    }

//    static Type type = typeof(XlfResourcesH);
//    private static void ProcessXlfFile(string basePath, string lang, string file)
//    {
//        var fn = FS.GetFileName(file).ToLower();
//        bool isCzech = fn.Contains("cs");
//        bool isEnglish = fn.Contains("en");

//        var doc = new XlfDocument(file);
//        //var doc = new XlfDocument(@"C:\Users\w\AppData\Local\Packages\31735sunamo.GeoCachingTool_q65n5amar4ntm\LocalState\sunamo.cs-CZ.xlf");
//        lang = lang.ToLower();

//        var xlfFiles = doc.Files.Where(d => d.Original.ToLower().Contains(lang));
//        if (xlfFiles.Count() != 0)
//        {
//            var xlfFile = xlfFiles.First();

//            foreach (var u in xlfFile.TransUnits)
//            {
//                if (isCzech)
//                {
//                    if (!RLData.cs.ContainsKey(u.Id))
//                    {
//                        RLData.cs.Add(u.Id, u.Target);
//                    }
//                }
//                else if (isEnglish)
//                {
//                    if (!RLData.en.ContainsKey(u.Id))
//                    {
//                        RLData.en.Add(u.Id, u.Target);
//                    }
//                }
//                else
//                {
//                    ThrowExceptions.Custom(Exc.GetStackTrace(), type, Exc.CallingMethod(), sess.i18n(XlfKeys.UnvalidFile) + " " + file + ", please delete it");
//                }
//            }
//        }



//    }
//}