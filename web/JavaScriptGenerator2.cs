using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
namespace webforms
{
    /// <summary>
    /// V této třídě budou ty samé metody co v JavascriptInjection, akorát v JavascriptInjection přímo vkládají do stránky, 
    /// </summary>
    public class JavaScriptGenerator2
    {
        public static string GenerateItemsForPhotoSwipe(List<ImageWithSize> iws)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("items = [");
            int poradi = 0;
            //sb.Append(@"{src:'',w:0,h:0,pid:0},");
            for (int i = 0; i < iws.Count - 1; i++)
            {
                var ws = iws[i];
                sb.Append(@"{src:'" + ws.uri + "',w:" + ws.width + ",h:" + ws.height + ",pid:" + poradi++ + ",idb:" + ws.id + "},");
                if (ws.id == -1)
                {

                }
                //ws.id.ToString());
            }
            if (iws.Count > 0)
            {
                var ws = iws[iws.Count - 1];
                sb.Append(@"{src:'" + ws.uri + "',w:" + ws.width + ",h:" + ws.height + ",pid:" + poradi++ + ",idb:" + ws.id + "}");
                if (ws.id == -1)
                {

                }
                //ws.id.ToString());
            }
            sb.Append("];");
            return sb.ToString();
        }



        /// <summary>
        /// A1 = green(ViewLastWeek) nebo blue(Views)
        /// </summary>
        /// <param name="theme"></param>
        /// <param name="idBadges"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public static string JsForIosBadge(string theme, List<string> idBadges, List<string> values)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("$(document).ready(function() {");
            for (int i = 0; i < idBadges.Count; i++)
            {
                sb.Append("$('#iosBadge" + idBadges[i] + "').iosbadge({ theme: '" + theme + "', size: 22, content: '" + values[i] + "' });");
            }
            sb.Append("});");
            return sb.ToString();
        }

        /// <summary>
        /// Položky se číslují od 0
        /// </summary>
        /// <param name="nameArray"></param>
        /// <param name="base64"></param>
        /// <returns></returns>
        public static string ImagesForSunamoPhotoGalleryViewer(string nameArray, List<string> base64)
        {
            StringBuilder vr = new StringBuilder();
            vr.Append(@"" + nameArray + @" = new Array();");
            int i = 0;
            foreach (var item in base64)
            {
                vr.Append(nameArray + "[" + (i++) + "] = '" + item + "';");
            }

            return vr.ToString();
        }

        /// <summary>
        /// Dříve bylo A3 vždy nastaveno na True
        /// </summary>
        /// <param name="sameTextInIDs"></param>
        /// <param name="nameArray"></param>
        /// <param name="createNewArray"></param>
        /// <returns></returns>
        public static string AlternatingImages(string sameTextInIDs, string nameArray, bool createNewArray)
        {
            string bef = "";
            if (createNewArray)
            {
                bef = "var " + nameArray + @" = new Array();var vsechnyObrazky = null;
var nameArray = '" + sameTextInIDs + @"';";
            }
            return bef + @"$(document).ready(function () {
    setInterval(changeImage, 5000);
    changeImage();
});

function changeImage() {
    vsechnyObrazky = $('.alternatingImage');
    //
        for (var i = 0; i < vsechnyObrazky.length; i++) {
            var jedenImg = vsechnyObrazky.get(i);
            var j = $(jedenImg);
            var id = parseInt(j.attr('id').replace(nameArray, ''));
            var src = j.attr('src');
            if (src == '') {
                if (" + nameArray + @"[id][0]) {
                    j.attr('src', GetWebUri(" + nameArray + @"[id][0]));
                }
                else {
                    // Inicializační obrázek nastavuje již metoda TopListWithImages
                }
            }
            else {
                if (" + nameArray + @"[id]) {
                    var pocetObrazku = " + nameArray + @"[id].length;
                    if (pocetObrazku != 0) {
                        var nastaveno = false;

                        for (var y = 0; y < pocetObrazku; y++) {
                            if (src.indexOf(" + nameArray + @"[id][y]) != -1) {
                                if (y + 1 != pocetObrazku) {
                                    j.attr('src', GetWebUri(" + nameArray + @"[id][++y]));
                                    nastaveno = true;
                                    break;
                                }
                            }
                        }
                        if (!nastaveno) {
                            j.attr('src', GetWebUri(" + nameArray + @"[id][0]));
                        }
                    }
                }
            }
        }}";
        }



        public static string GooglePieChart3D(string idElement, string title, string coSePorovnava, string jednotkaHodnotyPorovnavani, IEnumerable<KeyValuePair<string, int>> d)
        {
            StringBuilder lineChart = new StringBuilder();
            lineChart.AppendLine(@"google.load('visualization', '1', { packages: ['corechart'] });
        google.setOnLoadCallback(drawChart);
        function drawChart() {
            var data = google.visualization.arrayToDataTable([");
            lineChart.AppendLine(string.Format("['{0}', '{1}'],", coSePorovnava, jednotkaHodnotyPorovnavani));
            foreach (var item in d)
            {
                lineChart.AppendLine(string.Format("['{0}', {1}],", item.Key, item.Value));
            }
            lineChart.Append(@"]);

            var options = {
                title: '");
            lineChart.Append(title);
            lineChart.Append(@"',
                is3D: true,
            };

            var chart = new google.visualization.PieChart(document.getElementById('");
            lineChart.Append(idElement);
            lineChart.Append(@"'));
            chart.draw(data, options);
        }");
            return lineChart.ToString();
        }

        public static string GooglePieChart3D(string idElement, string title, string coSePorovnava, string jednotkaHodnotyPorovnavani, List<string> coSePorovnavaHodnoty, List<string> jednotkaHodnotyPorovnavaniHodnoty)
        {
            StringBuilder lineChart = new StringBuilder();
            lineChart.AppendLine(@"google.load('visualization', '1', { packages: ['corechart'] });
        google.setOnLoadCallback(drawChart);
        function drawChart() {
            var data = google.visualization.arrayToDataTable([");
            lineChart.AppendLine(string.Format("['{0}', '{1}'],", coSePorovnava, jednotkaHodnotyPorovnavani));
            for (int i = 0; i < coSePorovnavaHodnoty.Count; i++)
            {
                lineChart.AppendLine(string.Format("['{0}', {1}],", coSePorovnavaHodnoty[i], jednotkaHodnotyPorovnavaniHodnoty[i]));
            }
            lineChart.Append(@"]);

            var options = {
                title: '");
            lineChart.Append(title);
            lineChart.Append(@"',
                is3D: true,
            };

            var chart = new google.visualization.PieChart(document.getElementById('");
            lineChart.Append(idElement);
            lineChart.Append(@"'));
            chart.draw(data, options);
        }");
            return lineChart.ToString();
        }

        public static string GoogleLineChart(string idElement, string title, string osaX, string osaY, List<string> osaXPopisky, List<string> osaXHodnoty)
        {
            StringBuilder lineChart = new StringBuilder();
            lineChart.AppendLine(@"google.load('visualization', '1', { packages: ['corechart'] });
        google.setOnLoadCallback(drawChart);
        function drawChart() {
            var data = google.visualization.arrayToDataTable([");
            lineChart.AppendLine(string.Format("['{0}', '{1}'],", osaX, osaY));
            for (int i = 0; i < osaXPopisky.Count; i++)
            {
                lineChart.AppendLine(string.Format("['{0}', {1}],", osaXPopisky[i], osaXHodnoty[i]));
            }
            lineChart.Append(@"]);

            var options = {
                title: '");
            lineChart.Append(title);
            lineChart.Append(@"'
            };

            var chart = new google.visualization.LineChart(document.getElementById('");
            lineChart.Append(idElement);
            lineChart.Append(@"'));
            chart.draw(data, options);
        }");
            return lineChart.ToString();
        }

        public static string alert(string message)
        {
            // Cleans the message to allow single quotation marks 
            string cleanMessage = message.Replace("'", "\\'");
            string script = "alert('" + cleanMessage + "');";
            return script;
        }

        public static string alertWithCloseWindow(string message)
        {
            string cleanMessage = message.Replace("'", "\\'");
            string script = "alert('" + cleanMessage + "');window.close();";
            return script;
        }

        public static string FunctionOpenNewTab(string uri)
        {
            return $"window.open('{uri}', '_blank'); return false;";
        }





        public static string Redirect(string p, string uri)
        {
            return "<script type='text/javascript'>function " + p + "() { window.location.href='" + uri + "'; }</script>";
        }

        public static string ScriptTag(string p)
        {
            return Environment.NewLine + Environment.NewLine + "<script type='text/javascript'>" + p + "</script>" + Environment.NewLine + Environment.NewLine;
        }

        public static string jQueryAutocompleteData(string p, List<AB> abc)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(" " + p + " = [ ");
            foreach (var item in abc)
            {
                sb.AppendLine("{");
                sb.AppendLine(" value: \"" + item.A + "\",");
                sb.AppendLine(" label: \"" + item.B.ToString() + "\"");
                sb.AppendLine("},");
            }
            return sb.ToString().Substring(0, sb.Length - 3) + "];";
        }


    }
}
