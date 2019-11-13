using AllProjectsSearch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public partial class SolutionsIndexerHelper{ 
public static SolutionFolder SolutionWithName(string name)
    {
        foreach (var item in FoldersWithSolutions.fwss)
        {
            var slns = item.Solutions();
            foreach (var sln in slns)
            {
                if (sln.nameSolution == name)
                {
                    return sln;
                }
            }
        }

        return null;
    }

/// <summary>
    /// not full path, only name of folder for more accurate deciding
    /// </summary>
    /// <param name = "nameOfFolder"></param>
    /// <returns></returns>
    public static bool IsTheSolutionsFolder(string nameOfFolder)
    {
        return nameOfFolder.Contains(SolutionsIndexerConsts.ProjectsFolderName) || nameOfFolder == SolutionsIndexerStrings.GitHub;
    }

public static List<string> ProjectsInSolution(bool removeVsFolders, string fp, bool onlynames = true)
    {
        // TODO: Filter auto created files, then uncomment
        List<string> d = FS.GetFolders(fp);
        d = FS.OnlyNames(d);
        if (removeVsFolders)
        {
            VisualStudioTempFse.foldersInSolutionToDelete.ToList().ForEach(folder => CA.RemoveWildcard(d, folder));
            //
            VisualStudioTempFse.foldersInProjectDownloaded.ToList().ForEach(folder => CA.RemoveWildcard(d, folder));
        }

        if (!onlynames)
        {
            for (int i = 0; i < d.Count; i++)
            {
                d[i] = FS.Combine(fp, d[i]);
            }
        }

        return d;
    }

public static string GetDisplayedSolutionName(string item)
    {
        List<string> tokens = new List<string>();
        tokens.Add(FS.GetFileName(item.TrimEnd(AllChars.bs)));
        while (true)
        {
            item = FS.GetDirectoryName(item);
            if (CA.ContainsElement<string>(FoldersWithSolutions.onlyRealLoadedSolutionsFolders, item))
            {
                break;
            }

            if (string.IsNullOrEmpty(item))
            {
                break;
            }

            if (FS.GetFileName(item).StartsWith("Visual Studio "))
            {
                tokens.Add(FS.GetFileName(item.TrimEnd(AllChars.bs)).Replace("Visual Studio ", ""));
                break;
            }

            tokens.Add(FS.GetFileName(item.TrimEnd(AllChars.bs)));
        }

        tokens.Reverse();
        return SH.Join(AllChars.slash, tokens.ToArray());
    }

    internal static List<string> ModulesInSolution(List<string> projects, string fullPathFolder, bool selling, PpkOnDrive toSelling)
    {
        List<string> result = new List<string>();
        var sln = FS.GetFileName(fullPathFolder);

        foreach (var item in projects)
        {
            var path = FS.Combine(fullPathFolder, item);

            AddModules(path, sln+AllStrings.bs + item, result, selling, toSelling);
        }

        return result;
    }

    /// <summary>
    /// result a selling are pairing.
    /// </summary>
    /// <param name="path"></param>
    /// <param name="SlnProject"></param>
    /// <param name="result"></param>
    /// <param name="selling"></param>
    /// <param name="toSelling"></param>
    private static void AddModules(string path, string SlnProject, List<string> result, bool selling, PpkOnDrive toSelling)
    {
        path = FS.Combine(path, "UC");
        if (FS.ExistsDirectory(path))
        {
            var files = FS.GetFiles(path, FS.MascFromExtension(AllExtensions.xaml), System.IO.SearchOption.TopDirectoryOnly, true);
            files = FS.GetFileNamesWoExtension(files);
            foreach (var item in files)
            {
                var s = SlnProject + AllStrings.bs + item;
                if (toSelling.Contains(s))
                {
                    if (selling)
                    {
                        result.Add(s);
                    }
                }
                else
                {
                    if (!selling)
                    {
                        result.Add(s);
                    }
                }
                
            }
            
        }
    }
}