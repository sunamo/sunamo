using System;
using System.Collections.Generic;


using System.Text;
using System.IO;
using System.Diagnostics;
using HtmlAgilityPack;

/// <summary>
/// Našel jsem ještě třídu DotXml ale ta umožňuje vytvářet jen dokumenty ke bude root ThisApp.Name
/// A nebo moje vlastní XML třídy, ale ty umí vytvářet jen třídy bez rozsáhlejšího xml vnoření.
/// Element - prvek kterému se zapisují ihned i innerObsah. Může být i prázdný.
/// Tag - prvek kterému to mohu zapsat později nebo vůbec.
/// </summary>
public class XmlGenerator
{
    protected StringBuilder sb = new StringBuilder();
    bool useStack = false;
    Stack<string> stack = null;

	public int Length()
    {
        return sb.Length;
    }

    public void Insert(int index, string text)
    {
        sb.Insert(index, text);
    }

    public XmlGenerator() : this(false)
    {

    }

    public XmlGenerator(bool useStack)
	{
        this.useStack = useStack;

        if (useStack)
        {
            stack = new Stack<string>();
        }
    }

    public void StartComment()
    {
        sb.Append("<!--");
    }

    public void EndComment()
    {
        sb.Append("-->");
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

    #region CheckNull - Zakomentované, kdykoliv je můžeš odkomentovat 
    #endregion

    public void WriteCData(string innerCData)
    {
        this.WriteRaw(string.Format("<![CDATA[{0}]]>", innerCData));
    }

    public void WriteTagWithAttr(string tag, string atribut, string hodnota)
    {
        string r = string.Format("<{0} {1}=\"{2}\">", tag, atribut, hodnota);
        if (useStack)
        {
            stack.Push(r);
        }
        sb.Append(r);
    }

    public void WriteRaw(string p)
    {
        sb.Append(p);
    }

    public void TerminateTag(string p)
    {
        sb.AppendFormat("</{0}>", p);
    }

    public void WriteTag(string p)
    {
        string r = $"<{p}>";
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
    /// <param name="p"></param>
    /// <param name="p_2"></param>
    public void WriteTagWithAttrs(string p, params string[] p_2)
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendFormat("<{0} ", p);
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

    public void WriteElement(string nazev, string inner)
    {
        sb.AppendFormat("<{0}>{1}</{0}>", nazev, inner);
    }

    public void WriteXmlDeclaration()
    {
        sb.Append("<?xml version=\"1.0\" encoding=\"utf-8\" ?>");
    }

    public void WriteTagWith2Attrs(string p, string p_2, string p_3, string p_4, string p_5)
    {
        
        string r = string.Format("<{0} {1}=\"{2}\" {3}=\"{4}\">", p, p_2,p_3, p_4, p_5);
        if (useStack)
        {
            stack.Push(r);
        }
        this.sb.Append(r);
    }

    public void WriteNonPairTag(string p)
    {
        sb.AppendFormat("<{0} />", p);
    }

    
}
