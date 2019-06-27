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
        /// A1 je xml s nepreparovaným obsahem
        /// </summary>
        /// <param name="enS"></param>
        /// <returns></returns>
        public static void TrimStringResources(string fn)
        {
            string enS = File.ReadAllText(fn);
            XmlNamespacesHolder h = new XmlNamespacesHolder();
            h.ParseAndRemoveNamespaces(enS);
            //enS = enS.Replace(" version=\"1.2\" xmlns=\"urn:oasis:names:tc:xliff:document:1.2\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xsi:schemaLocation=\"urn:oasis:names:tc:xliff:document:1.2 xliff-core-1.2-transitional.xsd\"", "").Replace("\r\n", "");
            var enB = BTS.ConvertFromUtf8ToBytes(enS);
            XDocument xd;

            using (MemoryStream oStream = new MemoryStream(enB.ToArray()))
            using (XmlReader oReader = XmlReader.Create(oStream))
            {
                xd = XDocument.Load(oReader);
            }

            XHelper.AddXmlNamespaces(h.nsmgr);

            

            XElement xliff = XHelper.GetElementOfName(xd, "xliff");
            XElement file = XHelper.GetElementOfName(xliff,  "file");
            XElement body = XHelper.GetElementOfName(file, "body");
            XElement group = XHelper.GetElementOfName(body, "group");
            IEnumerable<XElement> trans_units = XHelper.GetElementsOfName(group, tTransUnit);
            List<XElement> tus = new List<XElement>();
            foreach (XElement item in trans_units)
            {
                XElement source = item.Element(XName.Get("source"));
                XElement target = item.Element(XName.Get("target"));

                TrimValueIfNot(source);
                TrimValueIfNot(target);
            }

            xd.Save(fn);
        }

        const string tTransUnit = "trans-unit";

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
            string enS = File.ReadAllText(fn);
            XmlNamespacesHolder h = new XmlNamespacesHolder();
            h.ParseAndRemoveNamespaces(enS);
            //enS = enS.Replace(" version=\"1.2\" xmlns=\"urn:oasis:names:tc:xliff:document:1.2\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xsi:schemaLocation=\"urn:oasis:names:tc:xliff:document:1.2 xliff-core-1.2-transitional.xsd\"", "").Replace("\r\n", "");
            var enB = BTS.ConvertFromUtf8ToBytes(enS);
            XDocument xd;

            using (MemoryStream oStream = new MemoryStream(enB.ToArray()))
            using (XmlReader oReader = XmlReader.Create(oStream))
            {
                xd = XDocument.Load(oReader);
            }

            XHelper.AddXmlNamespaces(h.nsmgr);

            XElement xliff = XHelper.GetElementOfName(xd, "xliff");
            var allElements = XHelper.GetElementsOfNameWithAttrContains(xliff, "file", "target-language", toL.ToString(), false);
            var resources = allElements.Where(d => XHelper.Attr(d, "original").Contains("/" + "RESOURCES" + "/"));
            XElement file = resources.First();
            XElement body = XHelper.GetElementOfName(file, "body");
            XElement group = XHelper.GetElementOfName(body, "group");

            var exists = XHelper.GetElementOfNameWithAttr(group, tTransUnit, "id", pascal);

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

            group.Add(xe);
            //var after = group.Value;

            xd.Save(fn);
        }

        
    }
}
