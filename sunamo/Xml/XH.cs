using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;

namespace sunamo.Xml
{
    public partial class XH
    {
        /// <summary>
        /// 
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
    }

    public partial class XH
    {
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
