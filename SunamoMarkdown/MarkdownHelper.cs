using System;
using Html2Markdown;
using sunamo.Html;

namespace SunamoMarkdown
{
    public class MarkdownHelper
    {
        public static string ConvertToMarkDown(string html)
        {
            var converter = new Converter();
            var markdown = converter.Convert(html);
            return markdown;
        }

        public static string ConvertToMarkDownMy(string input)
        {
            var hd = HtmlAgilityHelper.CreateHtmlDocument();
            hd.LoadHtml(input);

            var nodes = HtmlAgilityHelper.Nodes(hd.DocumentNode, true, "*");
            HtmlHelper.DeleteAttributesFromAllNodes(nodes);

            input = hd.DocumentNode.OuterHtml;

            input = ReplacePairTag(input, "bold", "*");
            input = ReplacePairTag(input, "strong", "*");
            input = ReplacePairTag(input, "b", "*");
            input = ReplacePairTag(input, "i", "_");
            input = ReplacePairTag(input, "strike", "-");
            input = HtmlHelper.StripAllTags(input);

            input = SH.ReplaceWhiteSpaces(input, " ");
            input = SH.ReplaceAllDoubleSpaceToSingle2(input);



            input = input.Trim();

            ClipboardHelper.SetText(input);

            return input;
        }

        private static string ReplacePairTag(string input, string v1, string v2)
        {
            return HtmlHelperText.ReplacePairTag(input, v1, v2);
        }
    }
}
