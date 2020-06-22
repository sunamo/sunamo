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
public class XlfResourcesH
{
    public static bool initialized = false;

    public static void SaveResouresToRLSunamo()
    {
        SaveResouresToRLSunamo(null);
    }

    /// <summary>
    /// 1. Entry method 
    /// Only for non-UWP apps
    /// </summary>
    public static string SaveResouresToRLSunamo(string key)
    {
        return SaveResouresToRL<string, string>(key, DefaultPaths.sunamoProject, new ExistsDirectory( FS.ExistsDirectoryNull));
    }

    public static string PathToXlfSunamo(Langs l)
    {
        var p = @"D:\Documents\Visual Studio 2017\Projects\sunamo\sunamo\MultilingualResources\sunamo.";
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

    /// <summary>
    /// 2. loading from xlf files
    /// </summary>
    /// <typeparam name="StorageFolder"></typeparam>
    /// <typeparam name="StorageFile"></typeparam>
    /// <param name="basePath"></param>
    /// <param name="existsDirectory"></param>
    /// <param name="appData"></param>
    public static string SaveResouresToRL<StorageFolder, StorageFile>(string key, string basePath, ExistsDirectory existsDirectory)
    {
        if (previousKey == key && previousKey != null)
        {
            return null;
        }

        previousKey = key;

        // cant be inicialized - after cs is set initialized to true and skip english
        //initialized = true;

        var path = Path.Combine(basePath, "MultilingualResources");

        var files = FS.GetFiles(path, "*.xlf", SearchOption.TopDirectoryOnly);
        foreach (var file3 in files)
        {
            var lang = XmlLocalisationInterchangeFileFormatXlf.GetLangFromFilename(file3);
            ProcessXlfFile(path,  lang.ToString(), file3);
        }

    

        return key;
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

    static Type type = typeof(XlfResourcesH);
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
                    ThrowExceptions.Custom(Exc.GetStackTrace(), type, Exc.CallingMethod(),"Unvalid file" + " " + file + ", " + "please delete it");
                }
            }
        }


        
    }
}