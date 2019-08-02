using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace SunamoCode
{
    public class XmlLocalisationInterchangeFileFormat
    {
        public static Langs GetLangFromFilename(string s)
        {
            s = Path.GetFileNameWithoutExtension(s);
            var parts = SH.Split(s, AllChars.dot);
            string last = parts[parts.Count - 1].ToLower();
            if (last.StartsWith("cs"))
            {
                return Langs.cs;
            }
            return Langs.en;
        }



        /// <summary>
        /// A1 is possible to obtain with XmlLocalisationInterchangeFileFormat.GetLangFromFilename(
        /// A2 je xml s nepreparovaným obsahem
        /// </summary>
        /// <param name="enS"></param>
        /// <returns></returns>
        public static void TrimStringResources(Langs toL, string fn)
        {
            var d= GetTransUnits(toL, fn);
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
        /// A1 is possible to obtain with XmlLocalisationInterchangeFileFormat.GetLangFromFilename(
        /// A2 can be null
        /// </summary>
        /// <param name="fn"></param>
        /// <param name="xd"></param>
        /// <returns></returns>
        public static XlfData GetTransUnits(Langs toL, string fn)
        {
            string enS = File.ReadAllText(fn);
            XlfData d = new XlfData();

            XmlNamespacesHolder h = new XmlNamespacesHolder();
            h.ParseAndRemoveNamespaces(enS);

            d. xd = XHelper.CreateXDocument(fn);

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
        /// originalSource(always english - same in all),translated,pascal
        /// </summary>
        /// <param name="toL"></param>
        /// <param name="originalSource"></param>
        /// <param name="translated"></param>
        /// <param name="pascal"></param>
        /// <param name="fn"></param>
        public static void Append(Langs toL, string originalSource, string translated, string pascal, string fn)
        {
            var d = GetTransUnits(toL, fn);

           
            var exists = XHelper.GetElementOfNameWithAttr(d. group, TransUnit.tTransUnit, "id", pascal);

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
            //var before = group.Value;

            //xd.CreateWriter();
            XElement xe = XElement.Parse(xml);
            xe = XHelper.MakeAllElementsWithDefaultNs(xe);

            d.group.Add(xe);
            //var after = group.Value;

            d.xd.Save(fn);

            XHelper.FormatXml(fn);
        }
    }
}
