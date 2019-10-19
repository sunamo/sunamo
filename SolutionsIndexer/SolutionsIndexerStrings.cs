using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class SolutionsIndexerStrings
{
    public const string VisualStudio2017 = @"Visual Studio 2017";
    public const string GitHub = "GitHub";
    public const string GitHubMy = "GitHubMy";



    /// <summary>
    /// Is used to get relative paths
    /// </summary>
    public static readonly string[] withDirectSolutions = new string[] { GitHubMy, SolutionsIndexerConsts.ProjectsFolderName };
}

