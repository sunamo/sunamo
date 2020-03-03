using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.IO;
using sunamo;
using sunamo.Enums;
using cmd.Essential;
using sunamo.Essential;
using sunamo.Constants;
using System.Linq;

public static partial class CL
{
    public static bool perform = true;

    public static void PerformAction(Dictionary<string, VoidVoid> actions)
    {
        List<string> listOfActions = actions.Keys.ToList();
        PerformAction(actions, listOfActions);
    }

    private static void PerformAction(Dictionary<string, VoidVoid> actions, List<string> listOfActions)
    {
        int selected = SelectFromVariants(listOfActions, RLData.en["SelectActionToProceed"]);
        if (selected != -1)
        {
            string ind = listOfActions[selected];
            var eh = actions[ind];
            eh.Invoke();
        }
    }

    /// <summary>
    /// Let user select action and run with A2 arg
    /// </summary>
    /// <param name = "akce"></param>
    public static void PerformAction(Dictionary<string, EventHandler> actions, object sender)
    {
        var listOfActions = NamesOfActions(actions);
        int selected = SelectFromVariants(listOfActions, RLData.en["SelectActionToProceed"] + ":");
        string ind = listOfActions[selected];
        EventHandler eh = actions[ind];

        if (sender == null)
        {
            sender = selected;
        }

        eh.Invoke(sender, EventArgs.Empty);
    }




/// <summary>
    /// Pokud uz. zada Y,GT.
    /// When N, return false.
    /// When -1, return null
    /// </summary>
    /// <param name = "text"></param>
    
    public static string UserMustTypeInFormat(string what, TextFormatData textFormat)
    {
        string entered = "";
        while (true)
        {
            entered = UserMustType(what);
            if (entered == null)
            {
                return null;
            }

            if (SH.HasTextRightFormat(entered, textFormat))
            {
                return entered;
            }
            else
            {
                ConsoleTemplateLogger.Instance.UnfortunatelyBadFormatPleaseTryAgain();
            }
        }

        return null;
    }
}