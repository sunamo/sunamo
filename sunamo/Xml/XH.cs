using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Linq;

namespace sunamo.Xml
{
    /// <summary>
    /// XH = XmlElement
    /// XHelper = XElement
    /// </summary>
    public partial class XH
    {
        public static void RemoveFirstElement(string xml, string elem)
        {
            var xd = XDocument.Parse(xml);
            //xd.Descendants("")
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="xml"></param>
        public static string InnerXml(string xml)
        {
            XmlDocument xdoc = new XmlDocument();
            xdoc.LoadXml(xml);
            return xdoc.DocumentElement.InnerXml;
        }

        /// <summary>
        /// 
        /// </summary>
        public static string ReplaceSpecialHtmlEntity(string vstup)
        {
            vstup = vstup.Replace("&rsquo;", "'");//

            vstup = vstup.Replace("&lsquo;", "'"); //¢
            return vstup;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="xml"></param>
        public static string ReplaceAmpInString(string xml)
        {
            Regex badAmpersand = new Regex("&(?![a-zA-Z]{2,6};|#[0-9]{2,4};)");
            const string goodAmpersand = "&amp;";
            return badAmpersand.Replace(xml, goodAmpersand);
        }

        /// <summary>
        /// Do A2 se vklzda jiz hotove xml, nikoliv soubor.
        /// G posledni dite, to znamena ze pri parsovani celeho dokumentu vraci root.
        /// </summary>
        /// <param name="soubor"></param>
        public static XmlNode ReturnXmlRoot(string xml)
        {
            XmlDocument xdoc = new XmlDocument();
            xdoc.LoadXml(xml);
            return (XmlNode)xdoc.LastChild;
        }

        /// <summary>
        /// Vraci FirstChild, pri parsaci celeho dokumentu tak vraci xml deklaraci.
        /// A2 should be entered otherwise can occur error "different XmlDocument context"
        /// </summary>
        /// <param name="soubor"></param>
        /// <param name="xnm"></param>
        public static XmlNode ReturnXmlNode(string xml, XmlDocument xdoc2 = null)
        {
            XmlDocument xdoc = null;
            //XmlTextReader xtr = new XmlTextReader(
            if (xdoc == null)
            {
                xdoc = new XmlDocument();
            }


            xdoc.LoadXml(xml);

            //xdoc.Load(soubor);
            return (XmlNode)xdoc.FirstChild;
        }

        static Type type = typeof(XH);

        /// <summary>
        /// Remove illegal XML characters from a string.
        /// </summary>
        public static string SanitizeXmlString(string xml)
        {
            if (xml == null)
            {
                ThrowExceptions.IsNull(Exc.GetStackTrace(), type, Exc.CallingMethod(),sess.i18n(XlfKeys.AtributteXmlIsNull));
            }
            //xml = xml.Replace("&", " and ");
            StringBuilder buffer = new StringBuilder(xml.Length);

            foreach (char c in xml)
            {
                if (IsLegalXmlChar(c))
                {
                    buffer.Append(c);
                }
            }

            return buffer.ToString();
        }

        public static XmlDocument xd = new XmlDocument();



        /// <summary>
        /// Whether a given character is allowed by XML 1.0.
        /// </summary>
        static bool IsLegalXmlChar(int character)
        {
            return
            (
                 character == 0x9 /* == '\t' == 9   */          ||
                 character == 0xA /* == '\n' == 10  */          ||
                 character == 0xD /* == '\r' == 13  */          ||
                (character >= 0x20 && character <= 0xD7FF) ||
                (character >= 0xE000 && character <= 0xFFFD) ||
                (character >= 0x10000 && character <= 0x10FFFF)
            );
        }

        /// <summary>
        /// A1 can be XML or path
        /// </summary>
        /// <param name="xml"></param>
        public static XmlDocument LoadXml(string xml)
        {
            if (FS.ExistsFile(xml))
            {
                xml = TF.ReadFile(xml);
            }

            XmlDocument xd = new XmlDocument();
            try
            {
                xd.LoadXml(xml);
            }
            catch (Exception ex)
            {

                return null;
            }
            return xd;
        }

        public static string RemoveXmlDeclaration(string vstup)
        {
            vstup = Regex.Replace(vstup, @"<\?xml.*?\?>", "");
            vstup = Regex.Replace(vstup, @"<\?xml.*?\>", "");
            vstup = Regex.Replace(vstup, @"<\?xml.*?\/>", "");
            return vstup;
        }

        public static string FormatXml(string xml)
        {
            string result = "";

            MemoryStream mStream = new MemoryStream();
            XmlTextWriter writer = new XmlTextWriter(mStream, Encoding.Unicode);

            XmlNamespacesHolder h = new XmlNamespacesHolder();

            XmlDocument document = null;
            //document = h.ParseAndRemoveNamespacesXmlDocument(xml);

            document = new XmlDocument();
            document.LoadXml(xml);

            try
            {
                writer.Formatting = Formatting.Indented;

                // Write the XML into a formatting XmlTextWriter
                document.WriteContentTo(writer);
                writer.Flush();
                mStream.Flush();

                // Have to rewind the MemoryStream in order to read
                // its contents.
                mStream.Position = 0;

                // Read MemoryStream contents into a StreamReader.
                StreamReader sReader = new StreamReader(mStream);

                // Extract the text from the StreamReader.
                string formattedXml = sReader.ReadToEnd();

                result = formattedXml;
            }
            catch (XmlException ex)
            {
                // Handle the exception
            }

            mStream.Close();
            // 'Cannot access a closed Stream.'
            //writer.Close();

            return result;
        }
    }
}