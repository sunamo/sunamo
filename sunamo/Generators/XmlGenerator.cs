using System;
using System.Collections.Generic;


using System.Text;
using System.IO;
using System.Diagnostics;
using HtmlAgilityPack;
using sunamo.Interfaces;
using sunamo.Generators;

/// <summary>
/// Našel jsem ještě třídu DotXml ale ta umožňuje vytvářet jen dokumenty ke bude root ThisApp.Name
/// A nebo moje vlastní XML třídy, ale ty umí vytvářet jen třídy bez rozsáhlejšího xml vnoření.
/// Element - prvek kterému se zapisují ihned i innerObsah. Může být i prázdný.
/// Tag - prvek kterému to mohu zapsat později nebo vůbec.
/// </summary>
public class XmlGenerator : IXmlGenerator<string, string>, IXmlGeneratorShared
{
    protected StringBuilder sb = null;

    public StringBuilder TextBuilder
    {
        get
        {
            return sb;
        }
    }

    bool useStack = false;
    Stack<string> stack = null;
    protected XmlGeneratorShared xml = null;

    public XmlGenerator()
    {
        sb = new StringBuilder();
        xml = new XmlGeneratorShared(this);
    }

    public XmlGenerator(bool useStack) : this()
	{
        this.useStack = useStack;

        if (useStack)
        {
            stack = new Stack<string>();
        }
    }

    public void WriteNonPairTagWithAttrs(string tag, params string[] args)
    {
        sb.AppendFormat("<{0} ", tag);
        for (int i = 0; i < args.Length; i++)
        {
            string text = args[i];
            object hodnota = args[++i];
                sb.AppendFormat("{0}=\"{1}\" ", text, hodnota);
        }
        sb.Append(" />");
    }

    public void WriteTagWithAttr(string tag, string attr, string value)
    {
        string r = string.Format("<{0} {1}=\"{2}\">", tag, attr, value);
        if (useStack)
        {
            stack.Push(r);
        }
        sb.Append(r);
    }

    public void TerminateTag(string tag)
    {
        sb.AppendFormat("</{0}>", tag);
    }

    public void WriteTag(string tag)
    {
        string r = $"<{tag}>";
        if (useStack)
        {
            stack.Push(r);
        }
        sb.Append(r);
    }

    public override string ToString()
    {
        return sb.ToString();
    }

    /// <summary>
    /// NI
    /// </summary>
    /// <param name="tag"></param>
    /// <param name="p_2"></param>
    public void WriteTagWithAttrs(string tag, params string[] p_2)
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendFormat("<{0} ", tag);
        for (int i = 0; i < p_2.Length; i++)
        {
            sb.AppendFormat("{0}=\"{1}\" ", p_2[i], p_2[++i]);
        }
        sb.Append(">");
        string r = sb.ToString();
        if (useStack)
        {
            stack.Push(r);
        }
        this.sb.Append(r);
    }

    public void WriteElement(string tag, string inner)
    {
        sb.AppendFormat("<{0}>{1}</{0}>", tag, inner);
    }

    public void WriteTagWith2Attrs(string tag, string attr1, string val1, string attr2, string val2)
    {
        string r = string.Format("<{0} {1}=\"{2}\" {3}=\"{4}\">", tag, attr1,val1, attr2, val2);
        if (useStack)
        {
            stack.Push(r);
        }
        this.sb.Append(r);
    }

    public void WriteNonPairTag(string tag)
    {
        sb.AppendFormat("<{0} />", tag);
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
}
