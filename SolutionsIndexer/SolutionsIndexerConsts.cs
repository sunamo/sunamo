using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class SolutionsIndexerConsts
{
    public const string BitBucket = "BitBucket";
    public const string ProjectsFolderName = "Projects";
    public const string VisualStudio = "Visual Studio";

    public static List<string> SolutionsExcludeWhileWorkingOnSourceCode = CA.ToListString("AllProjectsSearch", "sunamo", "CodeBoxControl");
}