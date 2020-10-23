using sunamo.Data;
using sunamo.Essential;
using System;
using System.Collections.Generic;
using System.Text;

namespace sunamo
{
    public class GoogleMyMapsHelper
    {
        static Type type = typeof(GoogleMyMapsHelper);

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

        /// <summary>
        /// Is better manually select two row and run from clipboard
        /// With joining in app there is formatting error
        /// </summary>
        /// <param name="appendCity"></param>
        /// <returns></returns>
        public static string CreateForGoogleMyMapsFromAddressRow(string appendCity, bool allowEmptyCity = false)
        {
            return CreateForGoogleMyMapsFromAddressRow(appendCity, ClipboardHelper.GetText(), allowEmptyCity);
        }

        /// <summary>
        /// Don't enter A1, it's for future improvements
        /// City is important because there is many same streets in state
        /// </summary>
        /// <param name="appendCity"></param>
        /// <param name="input"></param>
        public static string CreateForGoogleMyMapsFromAddressRow(string appendCity, string input, bool allowEmptyCity = false)
        {
            if (string.IsNullOrEmpty(input))
            {
                if (!allowEmptyCity)
                {
                    //input = CL.LoadFromClipboardOrConsole("2 rows from sheets");
                    ThrowExceptions.IsNull(Exc.GetStackTrace(), type, Exc.CallingMethod(), input);
                }
            }

            List<ABS> abs = new List<ABS>();

            // Is better manually select two row and run from clipboard
            // With joining in app there is formatting error
            var lines = SheetsHelper.Rows(input);

            if (lines.Count > 1)
            {
                var names = SheetsHelper.GetRowCells(lines[0]);
                var addresses = SheetsHelper.GetRowCells(lines[1]);

                appendCity = ", " + appendCity;

                ThrowExceptions.DifferentCountInLists(Exc.GetStackTrace(), type, "CreateForGoogleMyMapsFromAddressRow", "names", names, "addresses", addresses);

                for (int i = 0; i < names.Count; i++)
                {
                    ABS a = new ABS(names[i], addresses[i] + appendCity);
                    abs.Add(a);
                }
            }
            else
            {
                ThisApp.SetStatus(TypeOfMessage.Warning, "In clipboard must be two rows - name and address");
            }

            return GoogleMyMapsHelper.CreateExportForGoogleMyMaps(abs);
        }
    }
}