using sunamo.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace sunamo.Generators
{
    public class XmlGeneratorShared : IXmlGeneratorShared
    {
        StringBuilder sb = null;

        public XmlGeneratorShared(XmlGenerator xml)
        {
            this.sb = xml.TextBuilder;
        }

        public void WriteCData(string innerCData)
        {
            this.WriteRaw(string.Format("<![CDATA[{0}]]>", innerCData));
        }

        public void StartComment()
        {
            sb.Append("<!--");
        }

        public void EndComment()
        {
            sb.Append("-->");
        }

        public void WriteRaw(string p)
        {
            sb.Append(p);
        }

        public void WriteXmlDeclaration()
        {
            sb.Append("<?xml version=\"1.0\" encoding=\"utf-8\" ?>");
        }

        public int Length()
        {
            return sb.Length;
        }

        public void Insert(int index, string text)
        {
            sb.Insert(index, text);
        }
    }
}
