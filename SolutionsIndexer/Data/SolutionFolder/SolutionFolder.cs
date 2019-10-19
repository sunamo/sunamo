using sunamo.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllProjectsSearch
{
    
    public class SolutionFolder : SolutionFolderSerialize
    {
        public SolutionFolder(SolutionFolderSerialize t)
        {
            this.displayedText = t.displayedText;
            _fullPathFolder = t._fullPathFolder;
            _nameSolution = t._nameSolution;
            projectFolder = t.projectFolder;
            slnFullPath = t.slnFullPath;

        }

        public SolutionFolder()
        {
        }

        /// <summary>
        /// Only subfolders. csproj files must be find out manually
        /// Csproj are available to get with APSH.GetCsprojs()
        /// </summary>
        public List<string> projects = new List<string>();
        public string nameSolutionWithoutDiacritic = "";

        /// <summary>
        /// 
        /// </summary>
        public int countOfImages = 0;

        public bool InVsFolder = false;

        public override string ToString()
        {
            if (countOfImages != 0)
            {
                return displayedText + " (" + countOfImages.ToString() + " images)";
            }
            return displayedText;
        }

        public static bool operator >(SolutionFolder a, SolutionFolder b)
        {
            if (a.countOfImages > b.countOfImages)
            {
                return true;
            }
            return false;
        }

        public static bool operator <(SolutionFolder a, SolutionFolder b)
        {
            if (a.countOfImages < b.countOfImages)
            {
                return true;
            }
            return false;
        }

        public string ExeToRelease(SolutionFolder sln, string projectDistinction)
        {
            return FS.Combine(sln.fullPathFolder, sln.nameSolution+projectDistinction, @"bin\Release\" + sln.nameSolution + projectDistinction + ".exe");
        }

        /// <summary>
        /// Working
        /// </summary>
        /// <returns></returns>
        public bool HaveGitFolder()
        {
            var f = FS.Combine(this.fullPathFolder, ".git");
            bool vr = FS.ExistsDirectory(f);
            
            return vr;
        }
    }
}
