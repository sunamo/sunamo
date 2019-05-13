using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XliffParser;

public class XlfResourcesH
{

    public static void SaveResouresToRL()
    {
        SaveResouresToRL("cs");
        SaveResouresToRL("en");
        }

        /// <summary>
        /// A1 = CS-CZ or CS etc
        /// </summary>
        /// <param name="lang"></param>
        private static void SaveResouresToRL( string lang)
    {
        var path = Path.Combine(Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName), "MultilingualResources");
        var files = Directory.GetFiles(path);
        foreach (var file in files)
        {


            var doc = new XlfDocument(file);
            lang = lang.ToLower();
            var xlfFiles = doc.Files.Where(d => d.Original.ToLower().Contains(lang));
            if (xlfFiles.Count() != 0)
            {
                var xlfFile = xlfFiles.First();

                foreach (var u in xlfFile.TransUnits)
                {
                    if (lang.StartsWith("cs"))
                    {
                        RLData.cs.Add(u.Id, u.Target);
                    }
                    else if (lang.StartsWith("en"))
                    {
                        RLData.en.Add(u.Id, u.Target);
                    }
                    else
                    {
                        throw new Exception("Unvalid file " + file + ", please delete it");
                    }
                }
            }
        }
    }
}

