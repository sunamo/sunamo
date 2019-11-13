using System.IO;
using System.Collections.Generic;
using System;
using AllProjectsSearch;
using sunamo;
using System.Linq;
using sunamo.Essential;
using System.Diagnostics;

public class FoldersWithSolutions
{
    #region data fields
    public static List<SolutionFolder> solutions = null;
    /// <summary>
    /// d:\Documents
    /// </summary>
    public string documentsFolder = null;
    #endregion
    static Type type = typeof(FoldersWithSolutions);

    /// <summary>
    /// Složka ve které se má hledat na složku Projects a složky Visual Studia
    /// </summary>
    public static List<FoldersWithSolutions> fwss = new List<FoldersWithSolutions>();

    //TODO: repair diacritic in ctor

    #region ctor
    /// <summary>
    /// A1 = d:\Documents
    /// This class should be instaniate only once and then call reload by needs
    /// </summary>
    public FoldersWithSolutions(string documentsFolder, PpkOnDrive toSelling)
    {
        
        this.documentsFolder = documentsFolder;
         Reload(documentsFolder, toSelling);
    }
    #endregion

    #region Returns solutions in various objects
    public List<SolutionFolderWithFiles> SolutionsWithFiles()
    {
        List<SolutionFolderWithFiles> vr = new List<SolutionFolderWithFiles>();
        foreach (var item in solutions)
        {
            vr.Add(new SolutionFolderWithFiles(item));
        }
        return vr;
    }


    #endregion

    /// <summary>
    /// Get all projects in A1(Visual Studio Projects *) and GitHub folder and insert to global variable solutions
    /// </summary>
    /// <param name="documentsFolder"></param>
    /// <returns></returns>
    public List<SolutionFolder> Reload(string documentsFolder, PpkOnDrive toSelling, bool ignorePartAfterUnderscore = false)
    {
        

        // Get all projects in A1(Visual Studio Projects *) and GitHub folder
        List<string> solutionFolders = ReturnAllProjectFolders(documentsFolder, FS.Combine(documentsFolder, SolutionsIndexerStrings.GitHubMy));

        

        List<string> projOnlyNames = new List<string>(solutionFolders.Count);
        projOnlyNames.AddRange(FS.OnlyNames(solutionFolders.ToArray()));
        // Initialize global variable solutions
        solutions = new List<SolutionFolder>(solutionFolders.Count);
        
        

        for (int i = 0; i < solutionFolders.Count; i++)
        {
            var solutionFolder = solutionFolders[i];
            
            
            SolutionFolder sf = CreateSolutionFolder(solutionFolder, toSelling, projOnlyNames[i]);
            
            solutions.Add(sf);
        }

        

        return solutions;
    }

    public static List<string> onlyRealLoadedSolutionsFolders = new List<string>();

    

    public static SolutionFolder CreateSolutionFolder(SolutionFolderSerialize t)
    {
        return CreateSolutionFolder(t, t.fullPathFolder, null);
    }

    public static SolutionFolder CreateSolutionFolder(string solutionFolder, PpkOnDrive toSelling, string projName = null)
    {
        return CreateSolutionFolder(null, solutionFolder, toSelling,  projName);
    }

    public static SolutionFolder CreateSolutionFolder(SolutionFolderSerialize sfs, string solutionFolder, PpkOnDrive toSelling, string projName = null)
    {
        
        if (projName == null)
        {
            projName = FS.GetFileName(solutionFolder);
        }
        SolutionFolder sf = null;
        if (sfs != null)
        {
            sf = new SolutionFolder(sfs);
        }
        else
        {
            sf = new SolutionFolder();
        }
        sf.InVsFolder = solutionFolder.Contains(SolutionsIndexerStrings.VisualStudio2017);
        sf.displayedText = GetDisplayedName(solutionFolder);
        sf.fullPathFolder = solutionFolder;
        sf.projects = SolutionsIndexerHelper.ProjectsInSolution(true, sf.fullPathFolder);
        sf.modulesSelling = SolutionsIndexerHelper.ModulesInSolution(sf.projects, sf.fullPathFolder, true, toSelling);
        sf.modulesNotSelling = SolutionsIndexerHelper.ModulesInSolution(sf.projects, sf.fullPathFolder, false, toSelling);
        sf.nameSolutionWithoutDiacritic = SH.TextWithoutDiacritic(projName);
        return sf;
    }

    /// <summary>
    /// Get name based on relative but always fully recognized project
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    private static string GetDisplayedName(string item)
    {
        return SolutionsIndexerHelper.GetDisplayedSolutionName(item);
    }

    public IEnumerable<SolutionFolder> SolutionsUap(IEnumerable<string> skipThese = null)
    {
        var slns = Solutions(skipThese);
        var uap = slns.Where(d => d.fullPathFolder.Contains(@"\_Uap\"));
        return uap;
    }

    /// <summary>
    /// Simple returns global variable solutions
    /// Exclude from SolutionsIndexerConsts.SolutionsExcludeWhileWorkingOnSourceCode if Debugger is attached
    /// </summary>
    /// <returns></returns>
    public List<SolutionFolder> Solutions(IEnumerable<string> skipThese = null)
    {        
        var result = new List<SolutionFolder>( solutions);

        List<string> skip = null;
        if (skipThese != null)
        {
            skip = skipThese.ToList();
        }
        else
        {
            skip = new List<string>();
        }

        if (Debugger.IsAttached)
        {
            skip.AddRange(SolutionsIndexerConsts.SolutionsExcludeWhileWorkingOnSourceCode);
        }

        result.RemoveAll(d => CA.IsEqualToAnyElement(d.nameSolution, skip));

        ////DebugLogger.Instance.WriteCount("Solutions in " + documentsFolder, solutions);
        return result;
    }

    /// <summary>
    /// Return fullpath for all folder recursively - specific and ordi
    /// </summary>
    /// <param name="folderWithVisualStudioFolders"></param>
    /// <param name="alsoAdd"></param>
    /// <returns></returns>
    private List<string> ReturnAllProjectFolders(string folderWithVisualStudioFolders, params string[] alsoAdd)
    {
        List<string> projs = new List<string>();
        if (FS.ExistsDirectory(folderWithVisualStudioFolders))
        {
            List<string> visualStudioFolders = new List<string>(FS.GetFolders(folderWithVisualStudioFolders, SolutionsIndexerStrings.VisualStudio2017, SearchOption.TopDirectoryOnly));
            foreach (var item in alsoAdd)
            {
                AddProjectsFolder(projs, item);
            }
            foreach (var item in visualStudioFolders)
            {
                List<string> slozkySJazyky = null;
                    try
                {
                    slozkySJazyky = FS.GetFolders(item);
                }
                catch (Exception ex)
                {
                    continue;
                }
                foreach (var item2 in slozkySJazyky)
                {
                    #region New
                    string pfn = FS.GetFileName(item2);
                    if (SolutionsIndexerHelper.IsTheSolutionsFolder( pfn))
                    {
                        AddProjectsFolder(projs, item2);
                    }
                    #endregion
                }
            }
        }
        return projs;
    }

    /// <summary>
    /// Projde nerek slozky v A1 a vrati mi do A2 ty ktere zacinali na _ a do A3 zbytek.
    /// </summary>
    /// <param name="sloz"></param>
    /// <param name="Specialni"></param>
    /// <param name="normal2"></param>
    void ReturnNormalAndSpecialFolders(string sloz, out List<string> spec, out List<string> normal)
    {
        spec = new List<string>();
        normal = new List<string>();
        try
        {
            var slo = FS.GetFolders(sloz);
            foreach (string var in slo)
            {
                string nazev = FS.GetFileName(var);
                if (nazev.StartsWith(AllStrings.us))
                {
                    spec.Add(var);
                }
                else
                {
                    normal.Add(var);
                }
            }
        }
        catch (Exception ex)
        {
            
            
        }
    }

    /// <summary>
    /// Find out usuall folder and specific (which starting on _) and process then to any level
    /// </summary>
    /// <param name="proj"></param>
    /// <param name="slozka"></param>
    void AddProjectsFolder(List<string> proj, string slozka)
    {
        List<string> spec, norm;
        ReturnNormalAndSpecialFolders(slozka, out spec, out norm);

        norm = CA.EnsureBackslash(norm);
        proj.AddRange(norm);
        foreach (string var2 in spec)
        {
            AddProjectsFolder(proj, var2);
        }
    }

   
}
