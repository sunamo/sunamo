using AllProjectsSearch;
using sunamo.Essential;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public partial class SolutionsIndexerHelper{ 
public static SolutionFolder SolutionWithName(string name)
    {
        IEnumerable<SolutionFolder> wpf = null;

        if (FoldersWithSolutions.fwss.Count > 1)
        {
            Debugger.Break();
        }

        foreach (var item in FoldersWithSolutions.fwss)
        {
            var slns = item.Solutions();
            wpf = slns.Where(d => d.nameSolution.StartsWith(name[0].ToString().ToUpper()));

            foreach (var sln in slns)
            {
                if (sln.nameSolution == name)
                {
                    return sln;
                }
            }
        }

        ThisApp.SetStatus(TypeOfMessage.Warning, name + " solution was not found");
        return null;
    }

/// <summary>
    /// not full path, only name of folder for more accurate deciding
    /// </summary>
    /// <param name = "nameOfFolder"></param>
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
            VisualStudioTempFse.foldersInSolutionDownloaded.ToList().ForEach(folder => CA.RemoveWildcard(d, folder));
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

    public static List<string> ModulesInSolution(List<string> projects, string fullPathFolder, bool selling, PpkOnDrive toSelling)
    {
        List<string> result = new List<string>();
        var slnName = FS.GetFileName(fullPathFolder);

        foreach (var item in projects)
        {
            var path = FS.Combine(fullPathFolder, FS.GetFileNameWithoutExtension(item));
            var projectName = FS.GetFileNameWithoutExtension(item);

            slnName = FS.GetFileName(fullPathFolder);
            AddModules(selling, toSelling, result, slnName, projectName, path, "UserControl");
            slnName = FS.GetFileName(fullPathFolder);
            AddModules(selling, toSelling, result, slnName, projectName, path, "UC");
            slnName = FS.GetFileName(fullPathFolder);
            AddModules(selling, toSelling, result, slnName, projectName, path, "UserControls");

        }

        return result;
    }

    private static string AddModules(bool selling, PpkOnDrive toSelling, List<string> result, string slnName, string projectName, string path, string nameFolder)
    {
        var path2 = FS.Combine(path, nameFolder);
        AddModules(path2, slnName + AllStrings.bs + projectName, result, selling, toSelling);
        return path2;
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
        
        if (FS.ExistsDirectory(path))
        {
            var files = FS.GetFiles(path, FS.MascFromExtension(AllExtensions.xaml), System.IO.SearchOption.TopDirectoryOnly, new GetFilesArgs { _trimA1 = true });
            files = FS.GetFileNamesWoExtension(files);
            foreach (var item in files)
            {
                var module = FS.GetFileName(item);
                var s = SlnProject + AllStrings.bs + module;
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