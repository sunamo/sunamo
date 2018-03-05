using System;
using System.Collections.Generic;

using System.Text;
using System.IO;
using System.Diagnostics;

/// <summary>
/// Na�el jsem je�t� t��du DotXml ale ta umo��uje vytv��et jen dokumenty ke bude root ThisApp.Name
/// A nebo moje vlastn� XML t��dy, ale ty um� vytv��et jen t��dy bez rozs�hlej��ho xml vno�en�.
/// Element - prvek kter�mu se zapisuj� ihned i innerObsah. M��e b�t i pr�zdn�.
/// Tag - prvek kter�mu to mohu zapsat pozd�ji nebo v�bec.
/// </summary>
public class XmlGeneratorNS2
{
    protected StringBuilder sb = new StringBuilder();
     string ns = null;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="ns"></param>
    public XmlGeneratorNS2(string ns)
	{
        this.ns = ns;
	}

    public void WriteCData(string innerCData)
    {
        this.WriteRaw($"<![CDATA[{ innerCData }]]>");
    }

    public void WriteElementObject(string p, object o)
    {
        if (o != null)
        {
            WriteElement(p, o.ToString());
        }
    }

    public void WriteTagWithAttr(string tag, string atribut, string hodnota)
    {
        sb.AppendFormat("<" + ns + "{0} {1}=\"{2}\">", tag, atribut, hodnota);
    }

    public void WriteRaw(string p)
    {
        sb.Append(p);
    }

    public void TerminateTag(string p)
    {
        sb.AppendFormat("</" + ns + "{0}>", p);
    }

    public void WriteTag(string p)
    {
        sb.AppendFormat("<" + ns + "{0}>", p);
    }

    public override string ToString()
    {
        return sb.ToString().Replace("  />", " />");
    }

    /// <summary>
    /// NI
    /// </summary>
    /// <param name="p"></param>
    /// <param name="p_2"></param>
    public void WriteTagWithAttrs(string p, params string[] p_2)
    {
        sb.AppendFormat("<" + ns + "{0} ", p);
        for (int i = 0; i < p_2.Length; i++)
        {
            sb.AppendFormat("{0}=\"{1}\" ", p_2[i], p_2[++i]);
        }
        sb.Append(">");

    }

    public void WriteElement(string nazev, string inner)
    {
        sb.AppendFormat("<" + ns + "{0}>{1}</" + ns + "{0}>", nazev, inner);
    }

    public void WriteElementCData(string nazev, string cdata)
    {
        sb.AppendFormat("<" + ns + "{0}><![CDATA[{1}]]></" + ns + "{0}>", nazev, cdata);
    }

    public void WriteXmlDeclaration()
    {
        sb.Append("<?xml version=\"1.0\" encoding=\"utf-8\" ?>");
    }

    public void WriteTagWith2Attrs(string p, string p_2, string p_3, string p_4, string p_5)
    {
        sb.AppendFormat("<" + ns + "{0} {1}=\"{2}\" {3}=\"{4}\">", p, p_2, p_3, p_4, p_5);
    }

    public static string WriteSimpleTagS(string ns, string tag, params string[] p)
    {
        XmlGeneratorNS2 x = new XmlGeneratorNS2(ns);
        if (p.Length == 0)
        {
            x.WriteSimpleTag(tag);
        }
        else
        {
            x.WriteSimpleTag(tag, p);
        }
        return x.ToString();
    }

    public void WriteSimpleTag(string tag)
    {
        sb.AppendFormat("<" + ns + "{0} />", tag);
    }

    public void WriteSimpleTag(string tag, params string[] p_2)
    {
        sb.AppendFormat("<" + ns + "{0} ", tag);
        for (int i = 0; i < p_2.Length; i++)
        {
            sb.AppendFormat("{0}=\"{1}\" ", p_2[i], p_2[++i]);
        }
        sb.Append(" />");
    }
}
