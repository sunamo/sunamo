using System.Collections.Generic;

public static class AngularJsHelper
{
    /// <summary>
    /// Před použitím této metody si ověř že všechny soubory js máš v projektu
    /// </summary>
    /// <param name="scripts"></param>
    /// <param name="styles"></param>
    public static void Include(List<string> scripts, List<string> styles)
    {
        string[] arr = new string[] { "Scripts/angular-material.min.js", "Scripts/angular-messages.min.js", "Scripts/angular-aria.min.js", "Scripts/angular-route.min.js", "Scripts/angular-animate.min.js", "js/Shared.js", "Scripts/angular.min.js" };

        for (int i = arr.Length - 1; i >= 0; i--)
        {
            scripts.Insert(0, arr[i]);
        }
        styles.Add("Content/angular-material/angular-material.min.css");
    }
}
