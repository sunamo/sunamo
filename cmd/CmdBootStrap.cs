using cmd.Essential;
using sunamo.Essential;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class CmdBootStrap
{
    /// <summary>
    /// If user cannot select, A4,5 can be empty
    /// </summary>
    /// <param name="appName"></param>
    /// <param name="clipboardHelperWin"></param>
    /// <param name="runInDebug"></param>
    /// <param name="AddGroupOfActions"></param>
    /// <param name="allActions"></param>
    public static void Run(string appName, IClipboardHelper clipboardHelperWin, Action runInDebug, Func<Dictionary<string, VoidVoid>> AddGroupOfActions, Dictionary<string, VoidVoid> allActions)
    {
        ThisApp.Name = appName;
        AppData.ci.CreateAppFoldersIfDontExists();

        CmdApp.Init();
        CmdApp.EnableConsoleLogging(true);

        InitApp.Clipboard = clipboardHelperWin;
        InitApp.Logger = ConsoleLogger.Instance;
        InitApp.TemplateLogger = ConsoleTemplateLogger.Instance;
        InitApp.TypedLogger = TypedConsoleLogger.Instance;

        //var typeResources = typeof(Resources.ResourcesDuo);
        //ResourcesHelper rm = ResourcesHelper.Create(typeResources.FullName, typeResources.Assembly);

        XlfResourcesHSunamo.SaveResouresToRLSunamo();
        WriterEventLog.CreateMainAppLog(ThisApp.Name);

        bool askUser = false;

        string arg = string.Empty;

#if !DEBUG
            askUser = true;
#elif DEBUG
        runInDebug();
#endif
        if (AddGroupOfActions != null && allActions != null)
        {
            //CreateShortcutsOfPaExeInStartMenu();
            arg = CL.AskUser(askUser, AddGroupOfActions, allActions, arg);
            CmdApp.loadFromClipboard = true;

            if (askUser)
            {
                //var processToDie = TF.ReadFile(AppData.ci.GetFile(AppFolders.Data, "processToDieContains.txt"));
                //TerminateProcessesWithNameContains(processToDie);

                Console.WriteLine("App finished its running");
                Console.ReadLine();
            }
        }
    }
}