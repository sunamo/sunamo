using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;


public partial class UriWebServices
{
    public static class UriWebServicesDontTranslate
    {
        private static List<string> s_list = null;

        public static void SearchInAll(string spicyName)
        {
            if (s_list == null)
            {
                s_list = new List<string>(CA.ToEnumerable("kotanyi", "avok\u00E1do", "nadir", sess.i18n(XlfKeys.Orient), sess.i18n(XlfKeys.Drago), "v\u00EDtana", "sv\u011Bt bylinek"));
            }

            foreach (var item in s_list)
            {
                Process.Start(UriWebServices.GoogleSearch($"{item} koření {spicyName}"));
            }
        }
    }

    public static bool IsToOpen(string item)
    {
        return !CA.IsEqualToAnyElement<string>(item, Consts.NA, Consts.na);
    }
}