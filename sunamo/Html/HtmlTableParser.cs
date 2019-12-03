using HtmlAgilityPack;
using sunamo.Html;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                        data[r - startRow, c] = cellRow.InnerHtml;
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
    }
}
