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
using sunamo;
using sunamo.Constants;
using sunamo.Helpers;

using XliffParser;

public class XlfResourcesH
{
    public static bool initialized = false;

    public static void SaveResouresToRL<StorageFolder, StorageFile>(string basePath, ExistsDirectory existsDirectory, AppDataBase<StorageFolder, StorageFile> appData)
    {
        SaveResouresToRL<StorageFolder, StorageFile>(basePath, "en", existsDirectory, appData);
        SaveResouresToRL<StorageFolder, StorageFile>(basePath, "cs", existsDirectory, appData);
        
    }

    /// <summary>
    /// Only for non-UWP apps
    /// </summary>
    public static void SaveResouresToRLSunamo()
    {
        SaveResouresToRL<string, string>(FS.ExistsDirectoryNull, AppData.ci);
    }

    /// <summary>
    /// Private to use SaveResouresToRLSunamo
    /// </summary>
    private static void SaveResouresToRL<StorageFolder, StorageFile>( ExistsDirectory existsDirectory, AppDataBase<StorageFolder, StorageFile> appData)
    {
        SaveResouresToRL(Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName), existsDirectory,appData);
    }

    public static void SaveResouresToRLSunamo<StorageFolder, StorageFile>(ExistsDirectory existsDirectory, AppDataBase<StorageFolder, StorageFile> appData)
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

        string path = null;

        if (!PlatformInteropHelper.IsUwpWindowsStoreApp())
        {
            path = DefaultPaths.sunamoProject;
        }
        else
        {
            path = appData.RootFolderCommon(false);
        }

        SaveResouresToRL<StorageFolder, StorageFile>(path, existsDirectory, appData);
    }

   

    /// <summary>
    /// A1 = CS-CZ or CS etc
    /// </summary>
    /// <param name="lang"></param>
    private static void SaveResouresToRL<StorageFolder, StorageFile>(string basePath, string lang, ExistsDirectory existsDirectory, AppDataBase<StorageFolder, StorageFile> appData)
    {
        // cant be inicialized - after cs is set initialized to true and skip english
        //initialized = true;

        var path = Path.Combine(basePath, "MultilingualResources");

        Type type = PlatformInteropHelper.GetTypeOfResources();
        
        //ResourcesHelper rm = ResourcesHelper.Create("standard.Properties.Resources", type.Assembly);
        ResourcesHelper rm = ResourcesHelper.Create("sunamo.Properties.Resources", type.Assembly);

        var exists = false;

        if (PlatformInteropHelper.IsUwpWindowsStoreApp())
        {
            // keep exists on false
        }
        else
        {
            exists = FS.ExistsDirectory(path);
        }

        if (!exists)
        {
                string xlfContent = null;

                var fn = "sunamo_cs_CZ";
                

            var file = appData.GetFileCommonSettings(fn + ".xlf");


            // Cant use StorageFile.ToString - get only name of method
            //pathFile = file.ToString();
            
                xlfContent = rm.GetByteArrayAsString(fn);
                TF.WriteAllText(file, xlfContent);


                fn = "sunamo_en_US";

            file = appData.GetFileCommonSettings(fn + ".xlf"); ;
            

            xlfContent = rm.GetByteArrayAsString(fn);
            TF.WriteAllText(file, xlfContent);

                path = appData.RootFolderCommon(false);
            
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

