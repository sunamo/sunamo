﻿using sunamo.Constants;
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
        static Dictionary<string, string> unallowedEnds= new Dictionary<string, string>();

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

            var d = GetTransUnits( fn);
            List<XElement> tus = new List<XElement>();
            foreach (XElement item in d.trans_units)
            {
                var el = SourceTarget(item); 

                //TrimUnallowedChars(el.Item1);
                TrimUnallowedChars(el.Item2);
            }

            d.xd.Save(fn);
        }

        public static string ReturnEndingOn(string fn, List<string> list)
        {
            Dictionary<string, StringBuilder> result = new Dictionary<string, StringBuilder>();

            TextOutputGenerator tb = new TextOutputGenerator();
            var d = GetTransUnits(fn);

            foreach (var item in list)
            {
                result.Add(item, new StringBuilder());
            }

            foreach (var item in d.trans_units)
            {
                var lastLetter = GetLastLetter(item).ToString();

                if (CA.IsEqualToAnyElement<string>(lastLetter, list))
                {
                    result[lastLetter].AppendLine(GetTarget(item).Value);
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

        public static List<string> GetAllLastLetterFromEnd( string fn, bool saveAllLastLetterToClipboard)
        {
            List<string> ids = new List<string>();
            CollectionWithoutDuplicates<char> allLastLetters = new CollectionWithoutDuplicates<char>();

            var d = GetTransUnits( fn);
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
            var d = GetTransUnits( fn);
            List<XElement> tus = new List<XElement>();

            for (int i = d.trans_units.Count() - 1; i >= 0; i--)
            {
                var item = d.trans_units[i];
                var el = SourceTarget( item);


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
        /// <returns></returns>
        public static void TrimStringResources(Langs toL, string fn)
        {
            var d = GetTransUnits( fn);
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
        /// <returns></returns>
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="toL"></param>
        /// <param name="originalSource"></param>
        /// <param name="translated"></param>
        /// <param name="pascal"></param>
        /// <param name="fn"></param>
        public static void Append(Langs toL, string originalSource, string translated, string pascal, string fn)
        {
            var d = GetTransUnits( fn);

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

        public static void ReplaceXlfKeysForString(string path, List<string> ids)
        {
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
                var content = TF.ReadFile(item);
                if (content.Contains(XlfKeysDot))
                {
                    filesWithXlf.Add(item, content);
                }
            }

            


            foreach (var kv in filesWithXlf)
            {
                var content = kv.Value;
                StringBuilder sb = new StringBuilder(content);

                foreach (var item in ids)
                {
                    var toReplace = XlfKeysDot + item;
                    
                    var points = SH.ReturnOccurencesOfString(sb.ToString(), toReplace);

                    for (int i = points.Count - 1; i >= 0; i--)
                    {
                        var dx = points[i];

                        var dxNextChar = dx + toReplace.Length;

                        sb.Remove(dx, toReplace.Length);
                        sb.Insert(dx, SH.WrapWithQm(idTarget[item]);
                    }
                }

                TF.WriteAllText(kv.Value, sb.ToString());
            }

            // Nepřidávat znovu pokud již končí na postfix
        }

       
    }
}
