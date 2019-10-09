//using System;
//using System.Collections.Generic;
//using System.Text;


//public class TidyHtml5ManagedRepackHelper
//{
//    /// <summary>
//    /// Add ending tags!
//    /// Not working, always working the same as input
//    /// </summary>
//    /// <param name="input"></param>
//    public static string FormatHtml(string dirtyHtml)
//    {
//        //TidyHtml5Managed.

//        using (Document d = Document.FromString(dirtyHtml))
//        {
            
            

//            #region Added by me from original code
//            d.AnchorAsName = false;
//            d.DocType = DocTypeMode.Omit;
//            d.DropEmptyParagraphs = false;

//            d.FixUrlBackslashes = false; // fix-backslash
//            d.FixBadComments = false;
//            // fix-uri
//            d.FixAttributeUris = false;

            
//            d.RemoveEndTags = true; // hide-endtags
//            d.JoinStyles = false;
//            d.EnsureLiteralAttributes = true; // literal-attributes
//            d.LowerCaseLiterals = false;
//            d.MergeDivs = AutoBool.No;
//            d.MergeSpans = AutoBool.No;


//            d.OutputHtml = true;
//            d.PreserveEntities = true;
//            d.QuoteAmpersands = false;
//            d.QuoteNonBreakingSpaces = false;
//            d.OutputBodyOnly = AutoBool.Auto;

//            // #Diagnostics Options Reference
//            //show-errors
//            d.ShowWarnings = false;

//            // #Pretty Print Options Reference
//            d.LineBreakBeforeBR = true;
//            // indent
//            d.IndentAttributes = false;
//            d.IndentSpaces = 4;
//            d.TabSize = 4;
//            // wrap
//            d.WrapAsp = false;
//            d.WrapJste = false;
//            d.WrapPhp = false;
//            d.WrapSections = false;

//            // #Character Encoding Options Reference
//            d.CharacterEncoding = EncodingType.Utf8;

//            // #Miscellaneous Options Reference
//            d.ForceOutput = true;
//            d.Quiet = true;
//            // tidy-mark

//            //d.Quiet = true;
//            //d.OutputXhtml = true;
//            //d.IndentBlockElements = AutoBool.Yes;
//            //d.MakeClean = true;
//            //d.NewLine = NewlineType.CarriageReturnLinefeed;


//            #endregion

//            d.CleanAndRepair();
//            string cleanHtml = d.Save();

//            return cleanHtml;
//        }
//    }
//}

