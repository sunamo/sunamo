using HtmlAgilityPack;
using sunamo.Html;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace sunamo
{
    // Row/column
    public class HtmlTableParser
    {
        /// <summary>
        /// Pokud se bude v prvku vyskytovat null, jednalo se o colspan
        /// </summary>
        public string[,] data = null;

        public int RowCount => data.GetLength(0);
        public int ColumnCount => data.GetLength(1);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="html"></param>
        public HtmlTableParser(HtmlNode html, bool ignoreFirstRow)
        {
            int startRow = 0;
            if (ignoreFirstRow)
            {
                startRow++;
            }



            
            if (html.Name != "table")
            {
                var htmlFirst = html.FirstChild;
                if (htmlFirst.Name != "table")
                {
                    return;
                }
                html = htmlFirst;
            }

            int maxColumn = 0;

            List<HtmlNode> rows = HtmlHelper.ReturnAllTags(html, "tr");
            int maxRow = rows.Count;
            if (ignoreFirstRow)
            {
                maxRow--;
            }

            for (int r = startRow; r < rows.Count; r++)
            {
                List<HtmlNode> tds = HtmlHelper.ReturnAllTags(rows[r], "td", "th");
                int maxColumnActual = tds.Count;
                foreach (var cellRow in tds)
                {
                    string tdWithColspan = HtmlHelper.GetValueOfAttribute(HtmlAttrValue.colspan, cellRow, true);
                    if (tdWithColspan != "")
                    {
                        int colspan = BTS.TryParseInt(tdWithColspan, 0);
                        if (colspan > 0)
                        {
                            maxColumnActual += colspan;
                            maxColumnActual--;
                        }
                    }
                }
                if (maxColumnActual > maxColumn)
                {
                    maxColumn = maxColumnActual;
                }
            }

            data = new string[maxRow, maxColumn];

            for (int r = startRow; r < rows.Count; r++)
            {
                //List<HtmlNode> tds = HtmlHelper.ReturnAllTags()
                List<HtmlNode> ths = HtmlHelper.ReturnAllTags(rows[r], "th", "td");
                for (int c = 0; c < maxColumn; c++)
                {
                    if (CA.HasIndexWithoutException(c, ths))
                    {
                        HtmlNode cellRow = ths[c];
                        var cell = cellRow.InnerText.Trim();
                        //cell = HtmlHelperText.ConvertTextToHtml(cell);
                        cell = HttpUtility.HtmlDecode(cell);
                        cell = SH.ReplaceAllDoubleSpaceToSingle(cell);
                        

                        data[r - startRow, c] = cell;
                        string tdWithColspan = HtmlHelper.GetValueOfAttribute(HtmlAttrValue.colspan, cellRow, true);
                        if (tdWithColspan != "")
                        {
                            int colspan = BTS.TryParseInt(tdWithColspan, 0);
                            if (colspan > 0)
                            {
                                for (int i = 0; i < colspan; i++)
                                {
                                    c++;
                                    data[r - startRow, c] = null;
                                }
                            }
                        }
                    }
                }
            }
        }

        public static void NormalizeValuesInColumn(List<string> chars, bool removeAlsoInnerHtmlOfSubNodes)
        {
            for (int i = 0; i < chars.Count; i++)
            {
                if (removeAlsoInnerHtmlOfSubNodes)
                {
                    chars[i] = HtmlHelperText.RemoveAllNodes(chars[i]);
                }
                else
                {
                    chars[i] = HtmlHelper.StripAllTags(chars[i]);
                }
                chars[i] = HttpUtility.HtmlDecode(chars[i]);
            }

            
        }

        public List<string> ColumnValues(int dxColumn, bool normalizeValuesInColumn, bool removeAlsoInnerHtmlOfSubNodes, bool skipFirstRow)
        {
            var d0 = data.GetLength(0);
            List<string> vr = new List<string>();

            int i = 0;
            if (skipFirstRow)
            {
                i = 1;
            }

            for (; i < d0; i++)
            {
                vr.Add(data[i, dxColumn]);
            }

            FinalizeColumnValues(normalizeValuesInColumn, removeAlsoInnerHtmlOfSubNodes, vr);

            return vr;
        }

        public List<string> ColumnValues(string v, bool normalizeValuesInColumn, bool removeAlsoInnerHtmlOfSubNodes)
        {
            var d0 = data.GetLength(0);
            var d1 = data.GetLength(1);

            List<string> vr = new List<string>();

            for (int i = 0; i < d1; i++)
            {
                var nameColumn = data[0, i];
                var dxColumn = i;
                if (nameColumn == v)
                {
                    for (i = 1; i < d0; i++)
                    {
                        vr.Add(data[i, dxColumn]);
                    }
                }
                if (vr.Count != 0)
                {
                    break;
                }
                
            }

            FinalizeColumnValues(normalizeValuesInColumn, removeAlsoInnerHtmlOfSubNodes, vr);

            return vr;
        }

        private static void FinalizeColumnValues(bool normalizeValuesInColumn, bool removeAlsoInnerHtmlOfSubNodes, List<string> vr)
        {
            if (normalizeValuesInColumn || removeAlsoInnerHtmlOfSubNodes)
            {
                NormalizeValuesInColumn(vr, removeAlsoInnerHtmlOfSubNodes);
            }
        }
    }
}