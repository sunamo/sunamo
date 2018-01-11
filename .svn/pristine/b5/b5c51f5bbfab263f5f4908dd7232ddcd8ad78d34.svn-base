using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace webforms
{
    public class JavaScriptGenerator2WebForms
    {
        /// <summary>
        /// To co vrátí handler uloží do proměnné successMessage, aby pak ji mohla zobrazit stránka JS metodou zobrazZpravu, která se zatím vyskytuje hlavně u kočiček
        /// Samotná metoda vrací false, aby se tento handler dal použít hned ve tlačítkách.
        /// Neinjektuje přímo do stránky ale vrací JS který teprve pak vloží
        /// </summary>
        /// <param name="successMessage"></param>
        /// <param name="countUp"></param>
        /// <param name="ms"></param>
        /// <param name="nameOfFunction"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static string JQueryAjaxForHandlerShowMessage(string successMessage, int countUp, MySitesShort ms, string nameOfFunction, params string[] args)
        {
            return @"function " + nameOfFunction + "(" + SH.Join(',', args) + @") {successMessage = ajaxGet3(" + QSHelperWebForms.VratQSSimple(ms, countUp, nameOfFunction + "Handler.ashx", args) + ");zobrazZpravu('" + successMessage + @"'); return false;}";
        }

        public static object jQueryAutocompleteData(DataRowCollection dr)
        {
            StringBuilder sb = new StringBuilder();
            // " + p + " =
            sb.AppendLine(" [ ");
            foreach (DataRow item in dr)
            {
                sb.AppendLine("{");
                sb.AppendLine(" value: \"" + item.ItemArray[0].ToString() + "\",");
                sb.AppendLine(" label: \"" + item.ItemArray[1].ToString() + "\"");
                sb.AppendLine("},");
            }
            string vr = sb.ToString().Substring(0, sb.Length - 3) + "];";
            return vr;
        }

        internal static string JQueryAjaxForHandler(MySitesShort ms, int countUp, string nameOfFunction, string[] args)
        {
            return @"function " + nameOfFunction + "(" + SH.Join(',', args) + @") {ajaxGet3(" + QSHelperWebForms.VratQSSimple(ms, countUp, nameOfFunction + "Handler.ashx", args) + ");return false;}";
        }
    }
}
