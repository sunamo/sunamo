using sunamo.Data;
using sunamo.Essential;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllProjectsSearch.Data.ProjectsFolders
{
    public class ProjectFoldersSerialize
    {
        public List<ProjectFolderSerialize> projects = new List<ProjectFolderSerialize>();

        public ResultWithException<ProjectFoldersSerialize> GetWithName(List<string> projectsNamesFounded, bool canMissing)
        {
            ResultWithException<ProjectFoldersSerialize> result = new ResultWithException<ProjectFoldersSerialize>();
            result.Data = new ProjectFoldersSerialize();

            foreach (var item in projectsNamesFounded)
            {
                ProjectFolderSerialize projectFolder = projects.Find(d => {
                    if (d.NameProject == item)
                    {
                        return true;
                    }

                    return false;
                });

                if (projectFolder == null)
                {
                    if (!canMissing)
                    {
                        result.exc = Exceptions.ElementCantBeFound("", "solutionNamesFounded", item);
                    }
                }
                else
                {
                    result.Data.projects.Add(projectFolder);
                }
            }

            return result;
        }

        public void RemoveWithName(List<string> projectNamesFounded)
        {
            int dex = -1;
            foreach (var item in projectNamesFounded)
            {
                if ((dex = projects.FindIndex(d => d.NameProject == item)) != -1)
                {
                    projects.RemoveAt(dex);
                }
            }
        }
    }
}
