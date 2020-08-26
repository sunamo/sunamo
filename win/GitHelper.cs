using sunamo.Generators;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using win.Helpers.Powershell;

/// <summary>
/// Must be in Win because use powershell
/// In shared cannot because win derife from shared.
/// If I have abstract layer for shared, then yes
/// </summary>
public class GitHelper
{
    public static bool PushSolution(bool release, GitBashBuilder gitBashBuilder, string pushArgs, string commitMessage, string fullPathFolder, PushSolutionsData pushSolutionsData, GitBashBuilder gitStatus)
    {
        // 1. better solution is commented only getting files
        int countFiles = 0;
        if (release)
        {
            countFiles = FS.GetFiles(fullPathFolder, FS.MascFromExtension(), SearchOption.AllDirectories).Count;
        }

        if (countFiles > 0)
        {
            gitStatus.Clear();
            gitStatus.Cd(fullPathFolder);
            gitStatus.Status();

            var result = new List<List<string>>(CA.ToList<List<string>>(CA.ToListString(), CA.ToListString()));
            // 2. or powershell
            if (release)
            {
                result = PowershellRunner.Invoke(gitStatus.Commands);
            }

            var statusOutput = result[1];
            // If solution has changes
            var hasChanges = CA.ReturnWhichContains(statusOutput, "nothing to commit").Count == 0;
            if (!hasChanges)
            {
                foreach (var lineStatus in statusOutput)
                {
                    string statusLine = lineStatus.Trim();
                    if (statusOutput.Contains("modified:"))
                    {
                        if (statusOutput.Contains(".gitignore"))
                        {
                            hasChanges = true;
                            break;
                        }
                    }
                }
            }

            if (!hasChanges)
            {
                foreach (var lineStatus in statusOutput)
                {
                    //
                    string statusLine = lineStatus.Trim();
                    if (statusOutput.Contains("but the upstream is gone"))
                    {
                        hasChanges = true;
                        break;
                    }
                }

            }

            // or/and is a git repository
            var isGitRepository = CA.ReturnWhichContains(statusOutput, "not a git repository").Count == 0;
            if (hasChanges && isGitRepository)
            {
                gitBashBuilder.Cd(fullPathFolder);

                if (pushSolutionsData.mergeAndFetch)
                {
                    gitBashBuilder.Fetch();
                }

                gitBashBuilder.Add(AllStrings.asterisk);

                gitBashBuilder.Commit(false, commitMessage);

                if (pushSolutionsData.mergeAndFetch)
                {
                    gitBashBuilder.Merge("--allow-unrelated-histories");
                }

                if (pushSolutionsData.addGitignore)
                {
                    gitBashBuilder.Add(".gitignore");
                }

                gitBashBuilder.Push(pushArgs);

                gitBashBuilder.AppendLine();

                // Dont run, better is paste into powershell due to checking errors
                //var git = gitBashBuilder.Commands;
                //PowershellRunner.Invoke(git);

                return true;
            }
        }

        return false;
    }
}