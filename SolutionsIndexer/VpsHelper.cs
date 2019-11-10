using sunamo.Generators;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class VpsHelper
{
    public static bool IsVps
    {
        get
        {
            return VpsHelperSunamo.IsVps;
        }
    }

    public static string path
    {
        get => VpsHelperSunamo.path;

    }

    static PushSolutionsData pushSolutionsData = new PushSolutionsData();
    static List<string> list = CA.ToListString("ConsoleApp1", "Credentials", "Credentials.web", "ParseChromeAPIs", "PowershellScripts", "SqlCodeSnippets", "sunamo", "sunamo.cz", "sunamo.notmine", "sunamo.teamwork", "sunamo.teamwork.web", "sunamo.web", "SunamoCzAdmin", "SunamoSmartTools", "webelieve.cz");

    public static void PushAll()
    {
        pushSolutionsData.Set(false);
        PushPullAll();
    }

    private static void PushPullAll()
    {
        if (IsVps)
        {
            var folders = FS.GetFolders(path, SearchOption.TopDirectoryOnly);
            bool release = true;
            string pushArgs = string.Empty;
            string commitMessage = "From VPS " + DateTime.Today.ToShortDateString();

            var gitBashBuilder = new GitBashBuilder();
            var gitStatus = new GitBashBuilder();
            foreach (var item in folders)
            {
                GitHelper.PushSolution(release, gitBashBuilder, pushArgs, commitMessage, item, pushSolutionsData, gitStatus);
            }

            ClipboardHelper.SetText(gitBashBuilder.ToString());
        }
        else
        {
            
                bool release = true;
                string pushArgs = string.Empty;
                string commitMessage = "Before publishing to VPS " + DateTime.Today.ToShortDateString();

            

                var gitBashBuilder = new GitBashBuilder();
                var gitStatus = new GitBashBuilder();
                foreach (var sln in list)
                {
                    var sln2 = SolutionsIndexerHelper.SolutionWithName(sln);
                    var item = sln2.fullPathFolder;
                    GitHelper.PushSolution(release, gitBashBuilder, pushArgs, commitMessage, item, pushSolutionsData, gitStatus);
                }

                ClipboardHelper.SetText(gitBashBuilder.ToString());
            
        }
    }

    public static string PullAll()
    {
        string result = null;

        if (IsVps)
        {
            var gitBashBuilder = new GitBashBuilder();

            var folders = FS.GetFolders(path, SearchOption.TopDirectoryOnly);
            foreach (var item in folders)
            {
                gitBashBuilder.Cd(item);
                gitBashBuilder.Pull();
            }

            result = gitBashBuilder.ToString();
        }
        else
        {
            

            GitBashBuilder gitBashBuilder = new GitBashBuilder();

            foreach (var item in list)
            {
                var sln = SolutionsIndexerHelper.SolutionWithName(item);
                gitBashBuilder.Cd(sln.fullPathFolder);
                gitBashBuilder.Pull();
            }

            result = gitBashBuilder.ToString();
        }
        ClipboardHelper.SetText(result);
        return result;
    }
}

