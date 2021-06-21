using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class SolutionsIndexerStrings
{
    public const string VisualStudio2017 = @"vs";
    public const string GitHub = "GitHub";
    public const string GitHubMy = "GitHubMy";
    public const string BitBucket = "BitBucket";
    //
    public const string ProjectPostfix = "_Projects";


    /// <summary>
    /// Is used to get relative paths
    /// </summary>
    public static readonly List<string> withDirectSolutions = new List<string> { GitHubMy, SolutionsIndexerConsts.ProjectsFolderName };
}