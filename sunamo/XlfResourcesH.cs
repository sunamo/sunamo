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
using sunamo.Constants;
using sunamo.Helpers;
using sunamo.Properties;
using XliffParser;

public class XlfResourcesH
{
    public static bool initialized = false;

    public static void SaveResouresToRL(string basePath)
    {
        SaveResouresToRL(basePath, "cs");
        SaveResouresToRL(basePath, "en");
    }

    /// <summary>
    /// Private to use SaveResouresToRLSunamo
    /// </summary>
    private static void SaveResouresToRL()
    {
        SaveResouresToRL(Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName));
    }

    public static void SaveResouresToRLSunamo()
    {
        //var sunamoAssembly = typeof(Resources).Assembly;

        //var resources2 = sunamoAssembly.GetManifestResourceNames();

        //var resourceManager = new ResourceManager("sunamo.Properties.Resources", sunamoAssembly);
        //var resources = resourceManager.GetResourceSet(CultureInfo.CurrentUICulture, true, true);
        //foreach (var res in resources)
        //{
        //    var v = ((DictionaryEntry)res).Key;
        //    System.Console.WriteLine(v);
        //}

        SaveResouresToRL(DefaultPaths.sunamoProject);
    }

    /// <summary>
    /// A1 = CS-CZ or CS etc
    /// </summary>
    /// <param name="lang"></param>
    private static void SaveResouresToRL(string basePath, string lang)
    {
        // cant be inicialized - after cs is set initialized to true and skip english
        //initialized = true;

        var path = Path.Combine(basePath, "MultilingualResources");
        var type = typeof(Resources);
        
        ResourcesHelper rm = ResourcesHelper.Create("sunamo.Properties.Resources", type.Assembly);
        
        if (!FS.ExistsDirectory(path))
        {
            string xlfContent = null;

            var fn = "sunamo_cs_CZ";
            string pathFile = null;

            pathFile = AppData.ci.GetFileCommonSettings(fn + ".xlf");
            xlfContent = rm.GetByteArrayAsString(fn);
            TF.WriteAllText(pathFile, xlfContent);

            fn = "sunamo_en_US";

            pathFile = AppData.ci.GetFileCommonSettings(fn + ".xlf");
            xlfContent = rm.GetByteArrayAsString(fn);
            TF.WriteAllText(pathFile, xlfContent);

            path = AppData.ci.CommonFolder();
        }


        var files = FS.GetFiles(path, "*.xlf", SearchOption.TopDirectoryOnly);
            foreach (var file in files)
            {
                ProcessXlfFile(path, lang, file);
            }
        
       
    }

    private static void ProcessXlfFile(string basePath, string lang, string file)
    {

        var fn = FS.GetFileName(file).ToLower();
        bool isCzech = fn.Contains("cs");
        bool isEnglish = fn.Contains("en");

        var doc = new XlfDocument(file);
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
                    throw new Exception("Unvalid file" + " " + file + ", " + "please delete it");
                }
            }
        }

        
    }
}

