using sunamo.Constants;
using sunamo.Generators.Text;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace SunamoCode
{
    /// <summary>
    /// General methods for working with XML
    /// </summary>
    public static class XmlLocalisationInterchangeFileFormat
    {
        static Dictionary<string, string> unallowedEnds = new Dictionary<string, string>();

        public static Langs GetLangFromFilename(string s)
        {
            s = FS.GetFileNameWithoutExtension(s);
            var parts = SH.Split(s, AllChars.dot);
            string last = parts[parts.Count - 1].ToLower();
            if (last.StartsWith("cs"))
            {
                return Langs.cs;
            }
            return Langs.en;
        }

        public static void TrimUnallowedChars(Langs toL, string fn)
        {
            if (unallowedEnds.Count == 0)
            {
                unallowedEnds.Add(":", "c");
                unallowedEnds.Add(";", "S"); // Semicolon
                unallowedEnds.Add("?", "Q");
                unallowedEnds.Add("!", "E"); // exclamation 
                unallowedEnds.Add(".", "D");
                //unallowedEnds.Add("", "");
                //unallowedEnds.Add("", "");
                //unallowedEnds.Add("", "");
                //unallowedEnds.Add("", "");
                //unallowedEnds.Add("", "");
                //unallowedEnds.Add("", "");
                //unallowedEnds.Add("", "");

            }

            var d = GetTransUnits(fn);
            List<XElement> tus = new List<XElement>();
            foreach (XElement item in d.trans_units)
            {
                var el = SourceTarget(item);

                //TrimUnallowedChars(el.Item1);
                TrimUnallowedChars(el.Item2);
            }

            d.xd.Save(fn);
        }

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

            list = CA.ChangeContent(list, t => SH.RemoveAfterFirst(t, AllChars.space));

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

        public static char? GetLastLetter(XElement item)
        {
            string id = null;
            return GetLastLetter(item, out id);
        }

        static Tuple<string, string> GetTransUnit(XElement item)
        {
            var id = XHelper.Attr(item, "id");
            XElement target = GetTarget(item);
            return new Tuple<string, string>(id, target.Value);
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

        private static XElement GetTarget(XElement item)
        {
            return XHelper.GetElementOfName(item, "target");
        }

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

        static Tuple<XElement, XElement> SourceTarget(XElement item)
        {
            XElement source = XHelper.GetElementOfName(item, "source");
            XElement target = XHelper.GetElementOfName(item, "target");

            return new Tuple<XElement, XElement>(source, target);
        }

        public static void RemoveFromXlfWhichHaveEmptyTarget(string fn)
        {
            var d = GetTransUnits(fn);
            List<XElement> tus = new List<XElement>();

            for (int i = d.trans_units.Count() - 1; i >= 0; i--)
            {
                var item = d.trans_units[i];
                var el = SourceTarget(item);


                if (el.Item2.Value.Trim() == string.Empty)
                {
                    item.Remove();
                }
            }

            d.xd.Save(fn);
        }

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

        /// <summary>
        /// A1 is possible to obtain with XmlLocalisationInterchangeFileFormat.GetLangFromFilename
        /// </summary>
        /// <param name="enS"></param>
        public static void TrimStringResources(Langs toL, string fn)
        {
            var d = GetTransUnits(fn);
            List<XElement> tus = new List<XElement>();
            foreach (XElement item in d.trans_units)
            {
                XElement source = item.Element(XName.Get("source"));
                XElement target = item.Element(XName.Get("target"));

                TrimValueIfNot(source);
                TrimValueIfNot(target);
            }

            d.xd.Save(fn);
        }

        /// <summary>
        /// A1 is possible to obtain with XmlLocalisationInterchangeFileFormat.GetLangFromFilename
        /// </summary>
        /// <param name="fn"></param>
        /// <param name="xd"></param>
        public static XlfData GetTransUnits(string fn)
        {
            Langs toL = GetLangFromFilename(fn);

            string enS = File.ReadAllText(fn);
            XlfData d = new XlfData();

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

        private static void TrimValueIfNot(XElement source)
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

        public static void RemoveFromXlfAndXlfKeys(string fn, List<string> idsEndingEnd)
        {
            var d = GetTransUnits(fn);

            foreach (var item in d.trans_units)
            {
                string idTransUnit = null;
                GetLastLetter(item, out idTransUnit);

                for (int i = idsEndingEnd.Count - 1; i >= 0; i--)
                {
                    var id = idsEndingEnd[i];

                    if (id == idTransUnit)
                    {
                        item.Remove();
                    }
                }
            }

            CSharpParser.RemoveConsts(@"d:\Documents\Visual Studio 2017\Projects\sunamo\sunamo\Constants\XlfKeys.cs", idsEndingEnd);

            d.xd.Save(fn);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="toL"></param>
        /// <param name="originalSource"></param>
        /// <param name="translated"></param>
        /// <param name="pascal"></param>
        /// <param name="fn"></param>
        public static void Append(string originalSource, string translated, string pascal, string fn)
        {
            var d = GetTransUnits(fn);

            var exists = XHelper.GetElementOfNameWithAttr(d.group, TransUnit.tTransUnit, "id", pascal);

            if (exists != null)
            {
                return;
            }

            TransUnit tu = new TransUnit();
            tu.id = pascal;
            tu.source = originalSource;
            tu.translate = true;
            tu.target = translated;

            var xml = tu.ToString();
            XElement xe = XElement.Parse(xml);
            xe = XHelper.MakeAllElementsWithDefaultNs(xe);

            d.group.Add(xe);
            d.xd.Save(fn);

            XHelper.FormatXml(fn);
        }

        public static void ReplaceXlfKeysForString(string path, List<string> ids, List<string> solutionsExcludeWhileWorkingOnSourceCode, out CollectionWithoutDuplicates<string> addToNotToTranslateStrings)
        {
            addToNotToTranslateStrings = new CollectionWithoutDuplicates<string>();
            solutionsExcludeWhileWorkingOnSourceCode.Add("AllProjectsSearchTestFiles");

            CA.WrapWith(solutionsExcludeWhileWorkingOnSourceCode, "\\");

            const string XlfKeysDot = "XlfKeys.";

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
                if (content.Contains(XlfKeysDot))
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
                    var item2 = XlfKeysDot + item + "]";
                    var toReplace = "RLData.en[" + item2;

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

                        var dxNextChar = dx + toReplace.Length;

                        sb.Remove(dx, toReplace.Length);
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
    }
}