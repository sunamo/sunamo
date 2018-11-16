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
        string[] arr = new string[] { "ts/angular-material.min.js", "ts/angular-messages.min.js", "ts/angular-aria.min.js", "ts/angular-route.min.js", "ts/angular-animate.min.js", "js/Shared.js", "ts/angular.min.js" };

        for (int i = arr.Length - 1; i >= 0; i--)
        {
            scripts.Insert(0, arr[i]);
        }
        styles.Add("Content/angular-material/angular-material.min.css");
    }
}
