using sunamo.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace sunamo.Generators
{
    public class HtmlGeneratorEnum : IXmlGenerator<HtmlTag, HtmlAttr>, IHtmlGeneratorShared
    {
        

        HtmlGenerator generator = new HtmlGenerator();
        public HtmlGeneratorExtended<string, string> extended = null;
        XmlGeneratorShared xml = null;
        HtmlGeneratorShared html = null;

        public HtmlGeneratorEnum()
        {
            xml = generator.Xml;
            html = generator.Html;
            extended = new HtmlGeneratorExtended<string, string>(generator, html, xml, generator.TextBuilder);
        }

        public override string ToString()
        {
            return generator.ToString();
        }

        public void TerminateTag(HtmlTag tag)
        {
            generator.TerminateTag(tag.ToString());
        }

        public void WriteElement(HtmlTag tag, string inner)
        {
            generator.WriteElement(tag.ToString(), inner);
        }

        public void WriteNonPairTag(HtmlTag tag)
        {
            generator.WriteNonPairTag(tag.ToString());
        }

        public void WriteNonPairTagWithAttrs(HtmlTag tag, params string[] args)
        {
            generator.WriteNonPairTagWithAttrs(tag.ToString(), args);
        }

        public void WriteTag(HtmlTag tag)
        {
            generator.WriteTag(tag.ToString());
        }

        public void WriteTagWith2Attrs(HtmlTag tag, HtmlAttr attr1, string val1, HtmlAttr attr2, string val2)
        {
            generator.WriteTagWith2Attrs(tag.ToString(), attr1.ToString(), val1, attr2.ToString(), val2);
        }

        public void WriteTagWithAttr(HtmlTag tag, HtmlAttr attr, string value)
        {
            generator.WriteTagWithAttr(tag.ToString(), attr.ToString(), value);
        }

        public void WriteTagWithAttrs(HtmlTag tag, params string[] p_2)
        {
            generator.WriteTagWithAttrs(tag.ToString(), p_2);
        }

        public void WriteCData(string innerCData)
        {
            xml.WriteCData(innerCData);
        }

        public void StartComment()
        {
            xml.StartComment();
        }

        public void EndComment()
        {
            xml.EndComment();
        }

        public void WriteRaw(string p)
        {
            xml.WriteRaw(p);
        }

        public void WriteXmlDeclaration()
        {
            xml.WriteXmlDeclaration();
        }

        public int Length()
        {
            return xml.Length();
        }

        public void Insert(int index, string text)
        {
            xml.Insert(index, text);
        }

        public void WriteBr()
        {
            generator.WriteBr();
        }
    }
}
