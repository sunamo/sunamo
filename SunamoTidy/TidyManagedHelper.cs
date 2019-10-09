//using System;
//using TidyManaged;

//namespace SunamoTidy
//{
//    /// <summary>
//    /// For https://www.nuget.org/packages/TidyManaged/
//    /// </summary>
//    public class TidyManagedHelper
//    {
//        /// <summary>
//        /// 'Unable to load DLL 'libtidy.dll' or one of its dependencies: 
//        /// Cant add in core and fw project
//        /// In wpf app was added in csproj, in vs reference dont visible
//        /// running without exception, but not format
//        /// </summary>
//        /// <param name="html"></param>
//        /// <returns></returns>
//        public static string FormatHtml(string html)
//        {
//            string parsed = string.Empty;

//            using (Document doc = Document.FromString(html))
//            {
//                doc.ShowWarnings = false;
//                doc.Quiet = true;
//                doc.DocType = TidyManaged.DocTypeMode.Strict;
//                doc.DropFontTags = true;
//                doc.UseLogicalEmphasis = true;
//                doc.OutputXhtml = false;
//                doc.OutputXml = false;
//                doc.MakeClean = true;
//                doc.DropEmptyParagraphs = true;
//                doc.CleanWord2000 = true;
//                doc.QuoteAmpersands = true;
//                doc.JoinStyles = false;
//                doc.JoinClasses = false;
//                doc.Markup = true;
//                doc.IndentSpaces = 4;
//                doc.IndentBlockElements = TidyManaged.AutoBool.Yes;
//                doc.CharacterEncoding = TidyManaged.EncodingType.Utf8;
//                doc.WrapSections = false;
//                doc.WrapAttributeValues = false;
//                doc.WrapScriptLiterals = false;

//                doc.OutputBodyOnly = AutoBool.Yes;
//                // To prevent the lines from wrapping add the following
//                doc.WrapAt = 0;
//                // Must be last!
//                doc.CleanAndRepair();

//                parsed = doc.Save();
//            }

//            return parsed;
//        }
//    }
//}
