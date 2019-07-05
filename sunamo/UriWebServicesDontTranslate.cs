using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;


public partial class UriWebServices
{
    public static class UriWebServicesDontTranslate
    {
        static List<string> list = null;

        public static void SearchInAll(string spicyName)
        {
            if (list == null)
            {
                list = new List<string>(CA.ToEnumerable("kotanyi", "avokádo", "nadir", "Orient", "Drago", "vítana", "svět bylinek"));
            }

            foreach (var item in list)
            {
                Process.Start(UriWebServices.GoogleSearch($"{item} koření {spicyName}"));
            }
        }
    }
}

