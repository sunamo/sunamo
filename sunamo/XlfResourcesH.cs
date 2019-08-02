using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using sunamo.Constants;
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
            var files = Directory.GetFiles(path, "*.xlf", SearchOption.TopDirectoryOnly);
            foreach (var file in files)
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
}

