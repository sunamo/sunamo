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
