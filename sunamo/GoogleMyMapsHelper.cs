using sunamo.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace sunamo
{
    public class GoogleMyMapsHelper
    {
        public static string CreateExportForGoogleMyMaps(List<ABS> s)
        {
            StringBuilder sb = new StringBuilder();

            var captions = CA.ToListString("Name", sess.i18n(XlfKeys.Address));
            CA.JoinForGoogleSheetRow(sb, captions);

            List<List<string>> exists = new List<List<string>>();
            foreach (var i in s)
            {
                CA.JoinForGoogleSheetRow(sb, CA.Trim(CA.ToListString(i.A, i.B)));
            }

            return sb.ToString();
        }
    }
}