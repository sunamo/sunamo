using System;
using System.Text;
using System.Collections.Generic;
using sunamo.Helpers;
using sunamo.Enums;
using sunamo.Interfaces;
using sunamo.Generators;

/// <summary>
/// Here can be only method which don't passed as arguments any of tags - due to derive w
/// </summary>
public class HtmlGenerator : XmlGenerator, IHtmlGeneratorShared
{
    HtmlGeneratorShared html = null;
    

    public HtmlGeneratorShared Html
    {
        get
        {
            return html;
        }
    }

    public XmlGeneratorShared Xml
    {
        get
        {
            return xml;
        }
    }

    public HtmlGeneratorExtended<string, string> extended = null;

    public HtmlGenerator()
    {
        
        
        html = new HtmlGeneratorShared(this);
        extended = new HtmlGeneratorExtended<string, string>(this, html, xml, sb);
    }

    public void WriteBr()
    {
        html.WriteBr();
    }

    public override string ToString()
    {
        return base.ToString();
    }
}
