using System;
using sunamo.Html;

namespace SunamoMarkdown
{
    public class MarkdownHelper
    {
        static Type type = typeof(MarkdownHelper);

        /// <summary>
        /// Uses Html2Markdown which has dependency HtmlAgilityPack 1.5
        /// Therefore I Cant replace with 1s standard 1.11.2 and cant compile these project
        /// Therefore commented and remove nuget package
        /// </summary>
        /// <param name="html"></param>
        public static string ConvertToMarkDown(string html)
        {
            ThrowExceptions.Custom(RuntimeHelper.GetStackTrace(),type, RH.CallingMethod(), "See method comment.");
            return null;
            //var converter = new Converter();
            //var markdown = converter.Convert(html);
            //return markdown;
        }

        public static string ConvertToMarkDownMy(string input)
        {
            dynamic hd = null;
            hd = HtmlAgilityHelper.CreateHtmlDocument(); 
            hd.LoadHtml(input);

            dynamic nodes = null;
            nodes = HtmlAgilityHelper.Nodes(hd.DocumentNode, true, "*");
            HtmlHelper.DeleteAttributesFromAllNodes(nodes);

            input = hd.DocumentNode.OuterHtml;

            input = ReplacePairTag(input, "bold", "**");
            input = ReplacePairTag(input, "strong", "**");
            input = ReplacePairTag(input, "b", "**");
            input = ReplacePairTag(input, "i", "_");
            input = ReplacePairTag(input, "strike", "-");
            input = HtmlHelper.StripAllTags(input);

            input = SH.ReplaceWhiteSpaces(input, " ");
            input = SH.ReplaceAllDoubleSpaceToSingle2(input);



            input = input.Trim();

            ClipboardHelper.SetText(input);

            return input;
        }

        public static string ReplacePairTag(string input, string tag, string forWhat)
        {
            input = input.Replace("<" + tag + ">",  forWhat );
            input = input.Replace("<" + tag + " ", forWhat );
            input = input.Replace("</" + tag + ">", forWhat );
            return input;
        }
    }
}