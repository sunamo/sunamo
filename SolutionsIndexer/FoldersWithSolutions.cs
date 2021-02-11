using System.IO;
using System.Collections.Generic;
using System;
using AllProjectsSearch;
using sunamo;
using System.Linq;
using sunamo.Essential;
using System.Diagnostics;
using sunamo.Collections;
using sunamo.Constants;

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
    public FoldersWithSolutions(string documentsFolder, PpkOnDrive toSelling, bool addAlsoSolutions = true)
    {
        this.documentsFolder = documentsFolder;
        if (addAlsoSolutions)
	{
            Reload(documentsFolder, toSelling);
        }
         

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
    public List<SolutionFolder> Reload(string documentsFolder, PpkOnDrive toSelling, bool ignorePartAfterUnderscore = false)
    {
        PairProjectFolderWithEnum();

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

    static TwoWayDictionary<ProjectsTypes, string> projectTypes = new TwoWayDictionary<ProjectsTypes, string>();

    public static void PairProjectFolderWithEnum()
    {
        if (projectTypes._d1.Count > 0)
        {
            return;
        }

        var folders = FS.GetFolders(DefaultPaths.VisualStudio2017, "*", SearchOption.TopDirectoryOnly);

        foreach (var item in folders)
        {
            var fn = FS.GetFileName(item);
            if (fn.EndsWith(SolutionsIndexerStrings.ProjectPostfix))
            {
                ProjectsTypes p = ProjectsTypes.None;

                var l = fn.Replace(SolutionsIndexerStrings.ProjectPostfix, string.Empty);
                var l2 = l.Replace(AllStrings.lowbar, string.Empty).Trim();
                switch (l2)
                {
                    case "C++":
                        p = ProjectsTypes.Cpp;
                        break;
                    //case "":
                    //    p = ProjectsTypes.Cs;
                    //    break;
                    default:
                        p = EnumHelper.Parse<ProjectsTypes>(l2, ProjectsTypes.None);
                        break;
                }

                if (p == ProjectsTypes.None)
                {
                    ThrowExceptions.Custom(Exc.GetStackTrace(), type, Exc.CallingMethod(), sess.i18n(XlfKeys.CanTAssignToEnumTypeOfFolder)+" " + item);
                }

                projectTypes.Add(p, l);
            }
        }
        projectTypes.Add(ProjectsTypes.Cs, XlfKeys.Projects);
    }

    public static List<string> onlyRealLoadedSolutionsFolders = new List<string>();

    /// <summary>
    /// Pass into toSelling null! While working with SellingUC must use other CreateSolutionFolder
    /// Dont uncomment, because still forgetting insert toSelling
    /// </summary>
    /// <param name="t"></param>
    //public static SolutionFolder CreateSolutionFolder(SolutionFolderSerialize t)
    //{
    //    return CreateSolutionFolder(t, t.fullPathFolder, null);
    //}

    public static SolutionFolder CreateSolutionFolder(SolutionFolderSerialize solutionFolder, PpkOnDrive toSelling, string projName = null)
    {
        return CreateSolutionFolder(null, solutionFolder.fullPathFolder, toSelling, projName);
    }

    /// <summary>
    /// toSelling can be null
    /// </summary>
    /// <param name="solutionFolder"></param>
    /// <param name="toSelling"></param>
    /// <param name="projName"></param>
    /// <returns></returns>
    public static SolutionFolder CreateSolutionFolder(string solutionFolder, PpkOnDrive toSelling, string projName = null)
    {
        return CreateSolutionFolder(null, solutionFolder, toSelling,  projName);
    }

    /// <summary>
    /// toSelling can be null
    /// </summary>
    /// <param name="sfs"></param>
    /// <param name="solutionFolder"></param>
    /// <param name="toSelling"></param>
    /// <param name="projName"></param>
    /// <returns></returns>
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
        sf.repository = RepositoryFromFullPath(solutionFolder);
        IdentifyProjectType(solutionFolder, sf);
        sf.displayedText = GetDisplayedName(solutionFolder);
        sf.fullPathFolder = solutionFolder;
        sf.projects = SolutionsIndexerHelper.ProjectsInSolution(true, sf.fullPathFolder);
        sf.UpdateModules(toSelling);
        sf.nameSolutionWithoutDiacritic = SH.TextWithoutDiacritic(projName);
        return sf;
    }

    protected static void IdentifyProjectType(string solutionFolder, SolutionFolder sf)
    {
        // SolutionFolderSerialize doesn't have InVsFolder or typeProjectFolder
        sf.InVsFolder = solutionFolder.Contains(SolutionsIndexerStrings.VisualStudio2017);
        if (sf.InVsFolder)
        {
            var p = SH.Split(solutionFolder, AllChars.bs);
            var dx = p.IndexOf(SolutionsIndexerStrings.VisualStudio2017);
            var pr = p[dx + 1];
            pr = pr.Replace(SolutionsIndexerStrings.ProjectPostfix, string.Empty);
            if (projectTypes._d2.ContainsKey(pr))
            {
                sf.typeProjectFolder = projectTypes._d2[pr];
            }
            else
            {
                ThrowExceptions.KeyNotFound<string, ProjectsTypes>(Exc.GetStackTrace(), type, Exc.CallingMethod(), projectTypes._d2, "projectTypes._d2", pr);
            }
        }
    }

    private static Repository RepositoryFromFullPath(string fullPathFolder)
    {
        if (fullPathFolder.Contains(SolutionsIndexerStrings.VisualStudio2017))
        {
            return Repository.Vs17;
        }
        else if (fullPathFolder.Contains(SolutionsIndexerConsts.BitBucket))
        {
            return Repository.BitBucket;
        }
        ThrowExceptions.NotImplementedCase(Exc.GetStackTrace(), type, Exc.CallingMethod(), fullPathFolder);
        return Repository.All;
    }



    /// <summary>
    /// Get name based on relative but always fully recognized project
    /// </summary>
    /// <param name="item"></param>
    private static string GetDisplayedName(string item)
    {
        return SolutionsIndexerHelper.GetDisplayedSolutionName(item);
    }

    public IEnumerable<SolutionFolder> SolutionsUap(IEnumerable<string> skipThese = null)
    {
        var slns = Solutions(Repository.Vs17, false, skipThese);
        var uap = slns.Where(d => d.fullPathFolder.Contains(@"\_Uap\"));
        return uap;
    }

    /// <summary>
    /// A1 not have to be wildcard
    /// </summary>
    /// <param name="vs17"></param>
    /// <param name="toFind"></param>
    /// <returns></returns>
    public IEnumerable<SolutionFolder> SolutionsWildcard(Repository r, string mayWildcard)
    {
        var result = Solutions(r);

       
            for (int i = result.Count - 1; i >= 0; i--)
            {
            var ns = result[i].nameSolution;
                if (!SH.MatchWildcard(ns, mayWildcard))
                {
                    result.RemoveAt(i);
                }
            }
        

        return result;
    }

    /// <summary>
    /// Simple returns global variable solutions
    /// Exclude from SolutionsIndexerConsts.SolutionsExcludeWhileWorkingOnSourceCode if Debugger is attached
    /// A3 - can use wildcard
    /// </summary>
    public List<SolutionFolder> Solutions(Repository r, bool loadAll = true, IEnumerable<string> skipThese = null)
    {        
        var result = new List<SolutionFolder>( solutions);

        if (r != Repository.All)
        {
            result.RemoveAll(d => d.repository != r);
        }

        List<string> skip = null;
        if (skipThese != null)
        {
            skip = skipThese.ToList();
        }
        else
        {
            skip = new List<string>();
        }

        if (!loadAll)
        {
            if (Debugger.IsAttached)
            {
                skip.AddRange(SolutionsIndexerConsts.SolutionsExcludeWhileWorkingOnSourceCode);
            }
        }

        Dictionary<string, Wildcard> dict = new Dictionary<string, Wildcard>();
        foreach (var item in skip)
        {
            dict.Add(item, new Wildcard(item));
        }

        var l = result.Count;
        for (int i = result.Count - 1; i >= 0; i--)
        {
            var it = result[i];
            foreach (var item in dict)
            {
                if (item.Value.IsMatch( it.nameSolution))
                {
                    result.RemoveAt(i);
                    break;
                }
            }
        }

        
        var l2 = result.Count;

        //result.RemoveAll(d => CA.IsEqualToAnyElement(d.nameSolution, skip));

        ////////DebugLogger.Instance.WriteCount("Solutions in " + documentsFolder, solutions);
        return result;
    }

    /// <summary>
    /// Return fullpath for all folder recursively - specific and ordi
    /// </summary>
    /// <param name="folderWithVisualStudioFolders"></param>
    /// <param name="alsoAdd"></param>
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
                List<string> slozkySJazykyOutsideVs17 = new List<string>();
                try
                {
                    slozkySJazyky = FS.GetFolders(item);
                }
                catch (Exception ex)
                {
                    continue;
                }

                slozkySJazykyOutsideVs17.Leading(FS.Combine(folderWithVisualStudioFolders, SolutionsIndexerConsts.BitBucket));

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

                foreach (var item2 in slozkySJazykyOutsideVs17)
                {
                    #region New
                    string pfn = FS.GetFileName(item2);
                   
                        AddProjectsFolder(projs, item2);
                    
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
                if (nazev.StartsWith(AllStrings.lowbar))
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