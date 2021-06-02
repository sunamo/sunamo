using cmd.Essential;
using sunamo.Essential;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class CmdBootStrap
{
    public static void AddToAllActions(string v, Dictionary<string, VoidVoid> actions, Dictionary<string, VoidVoid> allActions)
    {
        string key = null;

        foreach (var item in actions)
        {
            key = v + AllStrings.swd + item.Key;

            if (allActions.ContainsKey(key))
            {
                break;
            }

            if (item.Key != "None")
            {
                allActions.Add(key, item.Value);
            }
        }
    }

    /// <summary>
    /// If user cannot select, A4,5 can be empty
    /// askUserIfRelease = null - ask user even in debug
    /// </summary>
    /// <param name="appName"></param>
    /// <param name="clipboardHelperWin"></param>
    /// <param name="runInDebug"></param>
    /// <param name="AddGroupOfActions"></param>
    /// <param name="allActions"></param>
    public static string Run(string appName, Func<IClipboardHelper> createInstanceClipboardHelper, Action runInDebug, Func<Dictionary<string, VoidVoid>> AddGroupOfActions, Dictionary<string, VoidVoid> allActions, bool? askUserIfRelease, Action InitSqlMeasureTime, Action customInit, Action assingSearchInAll)
    {
        if (assingSearchInAll != null)
        {
            
            assingSearchInAll();
        }
    //}

    //public static string Run(CmdBootStrapArgs a)
    //{ 
        ThisApp.Name = appName;

        if (InitSqlMeasureTime != null)
        {
            InitSqlMeasureTime();
        }

        AppData.ci.CreateAppFoldersIfDontExists();

        CmdApp.Init();
        CmdApp.EnableConsoleLogging(true);

        if (createInstanceClipboardHelper != null)
        {
            InitApp.Clipboard = createInstanceClipboardHelper();
        }
        InitApp.Logger = ConsoleLogger.Instance;
        InitApp.TemplateLogger = ConsoleTemplateLogger.Instance;
        InitApp.TypedLogger = TypedConsoleLogger.Instance;

        //var typeResources = typeof(Resources.ResourcesDuo);
        //ResourcesHelper rm = ResourcesHelper.Create(typeResources.FullName, typeResources.Assembly);

        XlfResourcesHSunamo.SaveResouresToRLSunamo();
        WriterEventLog.CreateMainAppLog(ThisApp.Name);

        bool askUser = false;

        string arg = string.Empty;

        if (!askUserIfRelease.HasValue)
        {
            askUser = true;
        }

        if (customInit != null)
        {
            customInit();
        }

#if !DEBUG
            askUser = askUserIfRelease.Value;
#elif DEBUG
        runInDebug();
        askUser = true;
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

                CL.WriteLine("App finished its running");
                Console.ReadLine();
            }
        }

        return arg;
    }
}