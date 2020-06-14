using sunamo.Constants;
using sunamo.Generators.Text;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;

namespace SunamoCode
{


    /// <summary>
    /// General methods for working with XML
    /// </summary>
    public static class XmlLocalisationInterchangeFileFormat
    {
        static Type type = typeof(XmlLocalisationInterchangeFileFormat);

        static List<string> xlfSolutions = new List<string>();
        static Dictionary<string, string> unallowedEnds = new Dictionary<string, string>();

        public static void CopyKeysTrailedWith_()
        {
            #region copy keys trailed with _
            List<string> consts = new List<string>();
            AllLists.InitHtmlEntitiesFullNames();

            var val = AllLists.htmlEntitiesFullNames.Values.ToList();
            int i;
            for (i = 0; i < val.Count; i++)
            {
                val[i] = "_" + val[i];
            }

            var newConsts = new StringBuilder();
            var newConsts2 = new List<string>();
            // 
            foreach (var item in consts)
            {
                var item3 = item;
                // replace all entity 
                foreach (var item2 in val)
                {
                    item3 = item3.Replace(item2, string.Empty);
                }

                if (!consts.Contains(item3) && !newConsts2.Contains(item3))
                {
                    newConsts2.Add(item3);
                    newConsts.AppendLine(string.Format(CSharpTemplates.constant, item3));
                }
            }

            ClipboardHelper.SetText(newConsts.ToString());
            #endregion
        }

        static XmlLocalisationInterchangeFileFormat()
        {
            /*
SunamoAdmin
AllProjectsSearch
             */

            var slns = SH.GetLines(@"calc.sunamo.cz
ConsoleApp1
SczClientDesktop
sunamo.cz
sunamo.performance
sunamo.tasks
sunamo2
SunamoXlf
TranslateEngine");

            foreach (var item in slns)
            {
                xlfSolutions.Add(DefaultPaths.vs + item);
            }
        }

        #region Takes XElement
        private static void TrimValueIfNot(XElement source)
        {
            if (source != null)
            {
                string sourceValue = source.Value;

                if (sourceValue.Length != 0)
                {
                    if (char.IsWhiteSpace(sourceValue[sourceValue.Length - 1]) || char.IsWhiteSpace(sourceValue[0]))
                    {
                        source.Value = sourceValue.Trim();
                    }
                }
            }
        }
        public static char? GetLastLetter(XElement item)
        {
            string id = null;
            return GetLastLetter(item, out id);
        }

        static Tuple<string, string> GetTransUnit(XElement item)
        {
            string id = Id(item);
            XElement target = GetTarget(item);
            return new Tuple<string, string>(id, target.Value);
        }

        public static string Id(XElement item)
        {
            return XHelper.Attr(item, "id");
        }

        public static char? GetLastLetter(XElement item, out string id)
        {
            var t = GetTransUnit(item);
            id = t.Item1;
            if (t.Item2.Count() > 0)
            {
                return t.Item2.Last();
            }

            return null;
        }

        public static XElement GetTarget(XElement item)
        {
            return XHelper.GetElementOfName(item, "target");
        }

        static Tuple<XElement, XElement> SourceTarget(XElement item)
        {
            XElement source = XHelper.GetElementOfName(item, "source");
            XElement target = XHelper.GetElementOfName(item, "target");

            return new Tuple<XElement, XElement>(source, target);
        }

        /// <summary>
        /// Trim whitespaces from start/end
        /// </summary>
        /// <param name="source"></param>
        private static void TrimUnallowedChars(XElement source)
        {
            string sourceValue = source.Value;
            if (sourceValue.Length != 0)
            {
                if (char.IsWhiteSpace(sourceValue[sourceValue.Length - 1]) || char.IsWhiteSpace(sourceValue[0]))
                {
                    source.Value = sourceValue.Trim();
                }
            }
        }


        #endregion

        public static List<string> GetFilesCs(string path = null)
        {
            if (path == null)
            {
                path = DefaultPaths.vs;
            }
            return FS.GetFiles(path, "*.cs", System.IO.SearchOption.AllDirectories, new GetFilesArgs { excludeWithMethod = SunamoCodeHelper.RemoveTemporaryFilesVS });
        }

        /// <summary>
        /// Before mu
        /// </summary>
        /// <param name="path"></param>
        public static void ReplaceForWithoutUnderscore(string folder)
        {
            Dictionary<string, string> withWithoutUnderscore = new Dictionary<string, string>();

            var files = XmlLocalisationInterchangeFileFormat.GetFilesCs();

            ReplaceStringKeysWithXlfKeys(files);

            string key = null;

            foreach (var item in files)
            {
                withWithoutUnderscore.Clear();

                var content = TF.ReadFile(item);
                var keys = GetKeysInCsWithRLDataEn(ref key, content);

                if (keys.Count > 0)
                {
                    foreach (var k in keys)
                    {
                        DictionaryHelper.AddOrSet(withWithoutUnderscore, k, ReplacerXlf.Instance.WithoutUnderscore(k));
                    }

                    foreach (var item2 in withWithoutUnderscore)
                    {
                        content = content.Replace(item2.Key + AllChars.lsqb, item2.Value + AllChars.lsqb);
                    }

                    TF.SaveFile(content, item);
                }
            }
        }

        public static List<string> GetKeysInCsWithoutRLDataEn(ref string key, string content)
        {
            CollectionWithoutDuplicates<string> c = new CollectionWithoutDuplicates<string>();

            var occ = SH.ReturnOccurencesOfString(content,  XmlLocalisationInterchangeFileFormatSunamo.XlfKeysDot);

            occ.Reverse();

            StringBuilder sb = new StringBuilder(content);

            foreach (var dx in occ)
            {
                var start = dx +  XmlLocalisationInterchangeFileFormatSunamo.XlfKeysDot.Length;
                var end = -1;
                for (int i = start; i < content.Length; i++)
                {
                    if (!char.IsLetterOrDigit(content[i]))
                    {
                        end = i ;
                        break;
                    }
                }

                key = content.Substring(start, end - start);

                c.Add(key);
            }

            

            return c.c;
        }

        public static List<string> GetKeysInCsWithRLDataEn(ref string key, string content)
        {
            CollectionWithoutDuplicates<string> c = new CollectionWithoutDuplicates<string>();

            var occ = SH.ReturnOccurencesOfString(content, XmlLocalisationInterchangeFileFormatSunamo.RLDataEn + XmlLocalisationInterchangeFileFormatSunamo.XlfKeysDot);

            occ.Reverse();

            StringBuilder sb = new StringBuilder(content);

            foreach (var dx in occ)
            {
                var start = dx + XmlLocalisationInterchangeFileFormatSunamo.RLDataEn.Length + XmlLocalisationInterchangeFileFormatSunamo.XlfKeysDot.Length;
                var end = content.IndexOf(AllChars.rsqb, start);

                key = content.Substring(start, end - start);

                c.Add(key);
            }

            occ = SH.ReturnOccurencesOfString(content, XmlLocalisationInterchangeFileFormatSunamo.SessI18n + XmlLocalisationInterchangeFileFormatSunamo.XlfKeysDot);

            occ.Reverse();

            

            foreach (var dx in occ)
            {
                var start = dx + XmlLocalisationInterchangeFileFormatSunamo.SessI18n.Length + XmlLocalisationInterchangeFileFormatSunamo.XlfKeysDot.Length;
                var end = content.IndexOf(AllChars.rb, start);

                key = content.Substring(start, end - start);

                c.Add(key);
            }

            return c.c;
        }

        #region Manage * edit in *.xlf
        public static string ReturnEndingOn(string fn, List<string> list, out List<string> idsEndingOn)
        {
            /*
 
! - always text
. - Always text
( - more often text
) - more often text
* - 50/50
, -  50/50
- Always text

Into A1 insert:
+ - all code
' - alwyas code
/ - always path
             */

            list = CA.ChangeContent(null,list, t => SH.RemoveAfterFirst(t, AllChars.space));

            idsEndingOn = new List<string>();
            Dictionary<string, StringBuilder> result = new Dictionary<string, StringBuilder>();

            TextOutputGenerator tb = new TextOutputGenerator();
            var d = GetTransUnits(fn);

            foreach (var item in list)
            {
                result.Add(item, new StringBuilder());
            }

            foreach (var item in d.trans_units)
            {
                string id = null;
                var lastLetter = GetLastLetter(item, out id).ToString();

                if (CA.IsEqualToAnyElement<string>(lastLetter, list))
                {
                    result[lastLetter].AppendLine(GetTarget(item).Value);
                    idsEndingOn.Add(id);
                }
            }



            foreach (var item in result)
            {
                tb.Paragraph(item.Value, item.Key);
            }
            return tb.sb.ToString();
        }

        /// <summary>
        /// Is calling in XlfManager.WhichStartEndWithNonDigitNumber
        /// </summary>
        /// <param name="pairsReplace"></param>
        public static void ReplaceInXlfSolutions(string pairsReplace)
        {
            if (pairsReplace == string.Empty)
            {
                Debugger.Break();
            }

            var t = SH.SplitFromReplaceManyFormatList(pairsReplace);
            var from = t.Item1;
            var to = t.Item2;

            foreach (var item in xlfSolutions)
            {
                var files = GetFilesCs(item);

                foreach (var item2 in files)
                {
                    var content = TF.ReadFile(item2);
                    content = content.Replace("\"-\"+\"-\"", "\"-\"");
                    for (int i = 0; i < from.Count; i++)
                    {
                        content = content.Replace(from[i], to[i]);
                    }
                    TF.SaveFile(content, item2);
                    //break;
                }
                //break;
            }
        }

        public static XlfData GetTransUnits(Langs en)
        {
            return GetTransUnits(XlfResourcesH.PathToXlfSunamo(en));
            
        }

        /// <summary>
        /// Is used nowhere 
        /// Was in MainWindow but probably was replaced with GetAllLastLetterFromEnd
        /// </summary>
        /// <param name="fn"></param>
        /// <param name="saveAllLastLetterToClipboard"></param>
        /// <returns></returns>
        public static List<string> GetAllLastLetterFromEnd(string fn, bool saveAllLastLetterToClipboard)
        {

            List<string> ids = new List<string>();
            CollectionWithoutDuplicates<char> allLastLetters = new CollectionWithoutDuplicates<char>();

            var d = GetTransUnits(fn);
            List<XElement> tus = new List<XElement>();
            foreach (XElement item in d.trans_units)
            {
                string id;
                var ch = GetLastLetter(item, out id);

                if (ch.HasValue)
                {
                    allLastLetters.Add(ch.Value);
                }

                ids.Add(id);
            }

            allLastLetters.c.Sort();

            if (saveAllLastLetterToClipboard)
            {
                ClipboardHelper.SetLines(allLastLetters.c);
            }

            return ids;
        }

        /// <summary>
        /// Into A1 insert XlfResourcesH.PathToXlfSunamo
        /// Completely NSN
        /// Remove completely whole Trans-unit
        /// </summary>
        /// <param name="fn"></param>
        public static void RemoveFromXlfWhichHaveEmptyTargetOrSource(string fn, XlfParts xp, bool removeWholeTransUnit = true)
        {
            var d = GetTransUnits(fn);
            List<XElement> tus = new List<XElement>();

            for (int i = d.trans_units.Count() - 1; i >= 0; i--)
            {
                var item = d.trans_units[i];
                var el = SourceTarget(item);

                if (xp == XlfParts.Source)
                {
                    if (el.Item1 != null)
                    {
                        if (el.Item1.Value.Trim() == string.Empty)
                        {
                            if (removeWholeTransUnit)
                            {
                                item.Remove();
                            }
                            else
                            {
                                ThrowExceptions.Custom(Exc.GetStackTrace(), type, Exc.CallingMethod(), "Instead of this use <source>.*</source> in VS!");
                                el.Item1.Remove();
                            }
                        }
                    }
                }
                else if(xp == XlfParts.Target)
                {
                    if (el.Item2 != null)
                    {
                        if (el.Item2.Value.Trim() == string.Empty)
                        {
                            if (removeWholeTransUnit)
                            {
                                item.Remove();
                            }
                            else
                            {
                                ThrowExceptions.Custom(Exc.GetStackTrace(), type, Exc.CallingMethod(), "Instead of this use <source>.*</source> in VS!");
                                el.Item2.Remove();
                            }
                        }
                    }
                }
            }

            d.xd.Save(fn);
        }

        /// <summary>
        /// Trim whitespaces from start/end on source / target
        /// A1 is possible to obtain with XmlLocalisationInterchangeFileFormat.GetLangFromFilename
        /// </summary>
        /// <param name="enS"></param>
        public static void TrimStringResources(string fn)
        {
            var d = GetTransUnits(fn);
            List<XElement> tus = new List<XElement>();
            foreach (XElement item in d.trans_units)
            {
                XElement source = null;
                XElement target = null;

                var t = SourceTarget(item);
                source = t.Item1;
                target = t.Item2;

                var id = Id(item);



                TrimValueIfNot(source);
                TrimValueIfNot(target);
            }

            d.xd.Save(fn);
        }

        /// <summary>
        /// A1 is possible to obtain with XlfResourcesH.PathToXlfSunamo
        /// </summary>
        /// <param name="fn"></param>
        /// <param name="xd"></param>
        public static XlfData GetTransUnits(string fn)
        {
            Langs toL = XmlLocalisationInterchangeFileFormatSunamo.GetLangFromFilename(fn);

            string enS = File.ReadAllText(fn);
            XlfData d = new XlfData();

            d.path = fn;

            XmlNamespacesHolder h = new XmlNamespacesHolder();
            h.ParseAndRemoveNamespacesXmlDocument(enS);

            d.xd = XHelper.CreateXDocument(fn);

            XHelper.AddXmlNamespaces(h.nsmgr);

            XElement xliff = XHelper.GetElementOfName(d.xd, "xliff");
            var allElements = XHelper.GetElementsOfNameWithAttrContains(xliff, "file", "target-language", toL.ToString(), false);
            var resources = allElements.Where(d2 => XHelper.Attr(d2, "original").Contains("/" + "RESOURCES" + "/"));
            XElement file = resources.First();
            XElement body = XHelper.GetElementOfName(file, "body");
            d.group = XHelper.GetElementOfName(body, "group");
            d.trans_units = XHelper.GetElementsOfName(d.group, TransUnit.tTransUnit);

            return d;
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="toL"></param>
        /// <param name="source"></param>
        /// <param name="target"></param>
        /// <param name="pascal"></param>
        /// <param name="fn"></param>
        public static void Append(string source, string target, string pascal, string fn)
        {
            var d = GetTransUnits(fn);

            var exists = XHelper.GetElementOfNameWithAttr(d.group, TransUnit.tTransUnit, "id", pascal);

            if (exists != null)
            {
                return;
            }

            Append(source, target, pascal, d);
            d.xd.Save(fn);

            XHelper.FormatXml(fn);
        }

        public static void Append(string source, string target, string pascal, XlfData d)
        {
            TransUnit tu = new TransUnit();
            tu.id = pascal;
            // Directly set to null due to not inserting into .xlf
            tu.source = null;
            //tu.translate = true;
            tu.target = target;

            var xml = tu.ToString();
            XElement xe = XElement.Parse(xml);
            xe = XHelper.MakeAllElementsWithDefaultNs(xe);

            d.group.Add(xe);
        }

        #region Cooperating XlfKeys and *.xlf
        public static void RemoveFromXlfAndXlfKeys(string fn, List<string> idsEndingEnd)
        {
            RemoveFromXlfAndXlfKeys(fn, idsEndingEnd, XlfParts.Id);
        }

        /// <summary>
        /// AndXlfKeys
        /// </summary>
        /// <param name="fn"></param>
        /// <param name="p"></param>
        /// <param name="saveToClipboard"></param>
        public static List<string> FromXlfWithDiacritic(string fn, XlfParts p, bool saveToClipboard = false)
        {
            // Dont use, its also non czech with diacritic hats tuồng (hats bôi)

            var d = GetTransUnits(fn);

            List<string> r = new List<string>();

            if (p == XlfParts.Id)
            {
                foreach (var item in d.trans_units)
                {
                    string idTransUnit = null;
                    GetLastLetter(item, out idTransUnit);



                    if (SH.ContainsDiacritic(idTransUnit))
                    {
                        r.Add(idTransUnit);
                        // dont remove, just save ID, coz many strings have diac and is not czech hats tuồng (hats bôi)
                        //item.Remove();
                        //; break;

                    }
                }

            }
            else if (p == XlfParts.Target)
            {

                foreach (var item in d.trans_units)
                {
                    var target = GetTarget(item).Value;
                    string idTransUnit = null;
                    GetLastLetter(item, out idTransUnit);

                    if (SH.ContainsDiacritic(target))
                    {

                        r.Add(idTransUnit);
                        // dont remove, just save ID, coz many strings have diac and is not czech hats tuồng (hats bôi)
                        //item.Remove();

                    }
                }

            }

            if (saveToClipboard)
            {
                ClipboardHelper.SetLines(r);
            }

            return r;
        }

        public static void RemoveFromXlfAndXlfKeys(string fn, List<string> idsEndingEnd, XlfParts p)
        {
            var d = GetTransUnits(fn);

            bool removed = false;

            if (p == XlfParts.Id)
            {
                for (int i = idsEndingEnd.Count - 1; i >= 0; i--)
                {
                    foreach (var item in d.trans_units)
                    {
                        string idTransUnit = null;
                        GetLastLetter(item, out idTransUnit);

                        var id = idsEndingEnd[i];

                        if (id == idTransUnit)
                        {
                            item.Remove();
                            break;
                        }
                    }
                }
            }
            else if (p == XlfParts.Target)
            {
                for (int i = idsEndingEnd.Count - 1; i >= 0; i--)
                {
                    removed = false;

                    foreach (var item in d.trans_units)
                    {
                        var target = HtmlAssistant.HtmlDecode(GetTarget(item).Value);
                        var id = idsEndingEnd[i];

                        if (id == target)
                        {
                            try
                            {
                                item.Remove();
                                removed = true;
                            }
                            catch (Exception ex)
                            {

                                // have no parent
                            }

                            break;
                        }
                    }

                    if (!removed)
                    {
                        DebugLogger.Instance.WriteLine(idsEndingEnd[i]);
                    }

                }
            }

            CSharpParser.RemoveConsts(@"d:\Documents\Visual Studio 2017\Projects\sunamo\sunamo\Constants\XlfKeys.cs", idsEndingEnd);

            d.xd.Save(fn);
        }

        public static void RemoveDuplicatesInXlfFile(string xlfPath)
        {
            // There is no way to delete node in xlf file with XlfDocument.
            // XlfDocument is using XDocument but its private
            /*
             1) Use xliffParser in sunamo.notmine
             2) Load in my own XmlDocument and remove throught XPath
             */

            /*
            I HAVE IT IN XDOCUMENT, I WILL USE THEREFORE METHODS OF LINQ
            METHOD REMOVE() IS THERE ISNT FOR FUN!!
             */
            if (false)
            {
                //XlfData d;
                //var ids = GetIds(xlfPath, out d);

                //d.xd.XPathSelectElement("/xliff/file[original=@'WPF.TESTS/RESOURCES/EN-US.RESX']");

                //List<string> duplicated;

                //CA.RemoveDuplicitiesList(ids, out duplicated);

                //var b2 = d.xd.Descendants().Count();


                //foreach (var item in duplicated)
                //{
                //    var elements = d.group.Elements().ToList();
                //    for (int i = 0; i < elements.Count(); i++)
                //    {
                //        var id = XHelper.Attr(elements[i], "id");
                //        if (id == item)
                //        {
                //            elements.Remove(elements[i]);
                //            break;
                //        }
                //    }
                //}

                //var b3 = d.xd.Descendants().Count();

                //d.xd.Save(xlfPath);
            }

            XlfData xlfData;
            var allIds = GetIds(xlfPath, out xlfData);

            List<string> duplicated;
            CA.RemoveDuplicitiesList<string>(allIds, out duplicated);

            foreach (var item in duplicated)
            {
                xlfData.trans_units.First(d => XHelper.Attr(d, "id") == item).Remove();
            }

            var outer = xlfData.xd.ToString();
            xlfData.xd.Save(xlfPath);
        }

        /// <summary>
        /// Into A1 pass XlfResourcesH.PathToXlfSunamo
        /// </summary>
        /// <param name="xlfPath"></param>
        /// <returns></returns>
        public static List<string> GetIds(string xlfPath)
        {
            XlfData d;
            return GetIds(xlfPath, out d);
        }

        /// <summary>
        /// Into A1 pass XlfResourcesH.PathToXlfSunamo
        /// </summary>
        /// <param name="xlfPath"></param>
        /// <param name="d"></param>
        /// <returns></returns>
        public static List<string> GetIds(string xlfPath, out XlfData d)
        {
            
            d = XmlLocalisationInterchangeFileFormat.GetTransUnits(xlfPath);
            d.FillIds();

            return d.allids;
        }



        public static void ReplaceStringKeysWithXlfKeys(string path)
        {


            List<string> files = FS.GetFiles(path, "*.cs", SearchOption.AllDirectories);
            ReplaceStringKeysWithXlfKeys(files);
        }

        public static void ReplaceStringKeysWithXlfKeys(List<string> files)
        {
            string key = null;

            foreach (var item in files)
            {
                var content = TF.ReadFile(item);
                var content2 = ReplaceStringKeysWithXlfKeysWorker(ref key, content);
                if (content != content2)
                {
                    TF.SaveFile(content2, item);
                }
            }
        }



        public static string ReplaceStringKeysWithXlfKeysWorker(ref string key, string content)
        {
            var occ = SH.ReturnOccurencesOfString(content, XmlLocalisationInterchangeFileFormatSunamo.RLDataEn + AllStrings.qm);

            occ.Reverse();

            StringBuilder sb = new StringBuilder(content);

            foreach (var dx in occ)
            {
                var start = dx + 1 + XmlLocalisationInterchangeFileFormatSunamo.RLDataEn.Length;
                var end = content.IndexOf(AllChars.qm, start);
                
                key = content.Substring(start, end - start);

                sb.Remove(start-1, end - start+2);
                sb.Insert(start-1, XmlLocalisationInterchangeFileFormatSunamo.XlfKeysDot + key);
            }

            return sb.ToString();
        }

        static readonly List<string> sunamoStrings = SH.GetLines(@"SunamoStrings.AddAsRsvp
SunamoStrings.EditUserAccount
SunamoStrings.UserDetail
SunamoStrings.ErrorSerie255
SunamoStrings.ErrorSerie0
SunamoStrings.ViewLastWeek
SunamoStrings.YouAreNotLogged
SunamoStrings.YouAreBlocked
SunamoStrings.TurnOnSelectingPhotos
SunamoStrings.TurnOffSelectingPhotos
SunamoStrings.StringNotFound
SunamoStrings.NoRightArgumentsToPage
SunamoStrings.YouAreNotLoggedAsWebAdmin
SunamoStrings.YouHaveNotValidIPv4Address
SunamoStrings.UriTooShort
SunamoStrings.UriTooLong
SunamoStrings.CustomShortUriOccupatedYet
SunamoStrings.LinkSuccessfullyShorted
SunamoStrings.Error
SunamoStrings.Success
SunamoStrings.RemoveFromFavoritesSuccess
SunamoStrings.AddToFavoritesSuccess
SunamoStrings.RemoveFromFavorites
SunamoStrings.AddToFavorites
SunamoStrings.RemoveAsRsvpSuccess
SunamoStrings.RemoveAsRsvp
SunamoStrings.DetailsClickSurveyAspxLabel
SunamoStrings.UnvalidSession
SunamoStrings.ScIsNotTheSame
SunamoStrings.NotImplementedPleaseContactWebAdmin");

        public static string ReplaceRlDataToSessionI18n(string content)
        {
            int dx = -1;

            foreach (var item in sunamoStrings)
            {
                dx = content.IndexOf(item);
                if (dx != -1)
                {
                    content = content.Insert(dx + item.Length, AllStrings.rb);
                    content = content.Remove(dx, XmlLocalisationInterchangeFileFormatSunamo.SunamoStringsDot.Length);
                    content = content.Insert(dx, XmlLocalisationInterchangeFileFormatSunamo.SessI18n + XmlLocalisationInterchangeFileFormatSunamo.XlfKeysDot);
                }
            }

            var l = XmlLocalisationInterchangeFileFormatSunamo.RLDataEn.Length;

            content = content.Replace(XmlLocalisationInterchangeFileFormatSunamo.RLDataEn2, XmlLocalisationInterchangeFileFormatSunamo.RLDataEn);

            var occ = SH.ReturnOccurencesOfString(content, XmlLocalisationInterchangeFileFormatSunamo.RLDataEn);
            List<int> ending = new List<int>();
            foreach (var item in occ)
            {
                var io = content.IndexOf( AllChars.rsqb, item);
                ending.Add(io);
            }

            StringBuilder sb = new StringBuilder(content);

            occ.Reverse();
            ending.Reverse();

            for (int i = 0; i < occ.Count; i++)
            {
                sb.Remove(occ[i], l);
                sb.Insert(occ[i], XmlLocalisationInterchangeFileFormatSunamo.SessI18n);

                var ending2 = ending[i];
                sb.Remove(ending2, 1);
                sb.Insert(ending2, AllStrings.rb);
            }

            var c = sb.ToString();
            //TF.SaveFile(c, )
            return c;
        }

        /// <summary>
        /// ReplaceXlfKeysForString - Convert from XlfKeys to ""
        /// Cooperating with NotToTranslateStrings
        /// </summary>
        /// <param name="path"></param>
        /// <param name="ids"></param>
        /// <param name="solutionsExcludeWhileWorkingOnSourceCode"></param>
        /// <param name="addToNotToTranslateStrings"></param>
        public static void ReplaceXlfKeysForString(string path, List<string> ids, List<string> solutionsExcludeWhileWorkingOnSourceCode, out CollectionWithoutDuplicates<string> addToNotToTranslateStrings)
        {
            addToNotToTranslateStrings = new CollectionWithoutDuplicates<string>();
            solutionsExcludeWhileWorkingOnSourceCode.Add("AllProjectsSearchTestFiles");

            CA.WrapWith(solutionsExcludeWhileWorkingOnSourceCode, "\\");

            Dictionary<string, string> filesWithXlf = new Dictionary<string, string>();

            var files = FS.GetFiles(DefaultPaths.vsProjects, "*.cs", SearchOption.AllDirectories);

            Dictionary<string, string> idTarget = new Dictionary<string, string>();

            var d = GetTransUnits(path);

            foreach (var item in d.trans_units)
            {
                var t = GetTransUnit(item);
                if (ids.Contains(t.Item1))
                {
                    idTarget.Add(t.Item1, t.Item2);
                }
            }

            foreach (var item in files)
            {
                bool continue2 = false;

                foreach (var item2 in solutionsExcludeWhileWorkingOnSourceCode)
                {
                    if (item.Contains(item2))
                    {
                        continue2 = true;
                        break;
                    }
                }

                if (continue2)
                {
                    continue;
                }

                var content = TF.ReadFile(item);
                if (content.Contains(XmlLocalisationInterchangeFileFormatSunamo. XlfKeysDot))
                {
                    filesWithXlf.Add(item, content);
                }
            }

            CollectionWithoutDuplicates<string> replacedKeys = new CollectionWithoutDuplicates<string>();

            foreach (var kv in filesWithXlf)
            {
                var content = kv.Value;
                StringBuilder sb = new StringBuilder(content);

                replacedKeys.c.Clear();

                foreach (var item in ids)
                {
                    var item2 = XmlLocalisationInterchangeFileFormatSunamo.XlfKeysDot + item + "]";
                    var toReplace = XmlLocalisationInterchangeFileFormatSunamo.RLDataEn + item2;

                    var toString = sb.ToString();
                    var points = SH.ReturnOccurencesOfString(toString, toReplace);
                    var points2 = SH.ReturnOccurencesOfString(toString, item2);

                    if (points2.Count > points.Count)
                    {

                    }

                    if (points.Count > 0)
                    {
                        replacedKeys.Add(item);
                        addToNotToTranslateStrings.Add(idTarget[item]);
                    }

                    for (int i = points.Count - 1; i >= 0; i--)
                    {
                        var dx = points[i];

                        var dxNextChar = dx + toReplace.Length();

                        sb.Remove(dx, toReplace.Length());
                        sb.Insert(dx, SH.WrapWithQm(idTarget[item]));
                    }
                }

                if (replacedKeys.c.Count > 0)
                {

                    TF.WriteAllText(kv.Key, sb.ToString());
                }
            }

            // Nepřidávat znovu pokud již končí na postfix
        }


        #endregion

        public static bool IsToBeInXlfKeys(string key)
        {
            var b1 = !SystemWindowsControls.StartingWithShortcutOfControl(key);
            var b2 = !key.StartsWith("Resources\\");
            var b3 = !CA.HasPostfix(key, ".PlaceholderText", ".Content");
            var b4 = !key.Contains(AllStrings.dot);
            var b5 = !key.Contains(AllStrings.bs);
            return b1 && b2 && b3 && b4 && b5;
        }
    }


}