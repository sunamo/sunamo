using sunamo.Enums;
using sunamo.Generators;
using sunamo.Helpers;
using sunamo.Interfaces;
using System;
using System.Text;

public class HtmlGeneratorExtended<T, Attr> 
{
    public IXmlGenerator<string, string> generator = null;
    HtmlGeneratorShared html = null;
    XmlGeneratorShared xml = null;

    public HtmlGeneratorExtended(IXmlGenerator<string, string> generator, HtmlGeneratorShared html, XmlGeneratorShared xml, StringBuilder sb)
    {
        this.generator = generator;
        this.html = html;
        this.xml = xml;
    }

    ResourcesHelper resourcesHelper = ResourcesHelper.Create(typeof(ResourcesHelper));

    

    /// <summary>
    /// Allow write tags in head
    /// </summary>
    public void BoilerplateStart()
    {
        xml.WriteRaw(resourcesHelper.GetString(ResourceKeys.Html5BoilerplateStart));
    }

    /// <summary>
    /// End head and start body
    /// </summary>
    public void BoilerplateMiddle()
    {
        xml.WriteRaw(resourcesHelper.GetString(ResourceKeys.Html5BoilerplateMiddle));
    }

    public void BoilerplateEnd()
    {
        xml.WriteRaw(resourcesHelper.GetString(ResourceKeys.Html5BoilerplateEnd));
    }

    public void StyleDirect()
    {
        generator.WriteTagWithAttr(HtmlTags.style, HtmlAttrs.type, HtmlAttrsValues.TextCss);
    }

    public void DetailAnchor(string label, string oUriYouthProfile, string oNameYouthProfile)
    {
        if (!string.IsNullOrEmpty(oNameYouthProfile))
        {
            generator.WriteElement("b", label + ":");
            xml.WriteRaw(" ");
            if (string.IsNullOrEmpty(oUriYouthProfile))
            {
                xml.WriteRaw(oNameYouthProfile);
            }
            else
            {
                generator.WriteTagWithAttr("a", "href", oUriYouthProfile);
                xml.WriteRaw(oNameYouthProfile);
                generator.TerminateTag("a");
            }
            html.WriteBr();
        }
    }

    /// <summary>
    /// A1 can be null
    /// </summary>
    /// <param name="cssFile"></param>
    /// <param name="injectToHtml"></param>
    public void BoilerplateStart(string cssFile, bool injectToHtml)
    {
        xml.WriteRaw(resourcesHelper.GetString(ResourceKeys.Html5BoilerplateStart));
        if (injectToHtml)
        {
            #region Style
            StyleDirect();
            string content = TF.ReadFile(cssFile);
            generator.WriteRaw( content);
            generator.TerminateTag(HtmlTags.style);
            #endregion
        }
        else
        {
            generator.WriteTagWith2Attrs(HtmlAttrs.link, HtmlAttrs.rel, HtmlAttrsValues.Stylesheet, HtmlAttrs.href, cssFile);
        }
        
    }

    public void Detail(string label, string timeInterval)
    {
        if (!string.IsNullOrEmpty(timeInterval))
        {
            generator.WriteElement("b", label + ":");
            xml.WriteRaw(" ");
            xml.WriteRaw(timeInterval);
            html.WriteBr();
        }
    }

    public void DetailNewLine(string label, string oDescriptionHtml)
    {
        if (!string.IsNullOrEmpty(oDescriptionHtml))
        {
            generator.WriteElement("b", label);
            html.WriteBr();
            xml.WriteRaw(oDescriptionHtml);
            html.WriteBr();
        }
    }

    public void DetailMailto(string label, string oMail)
    {
        if (!string.IsNullOrEmpty(oMail))
        {
            generator.WriteElement("b", label + ":");
            xml.WriteRaw(" ");
            generator.WriteTagWithAttr("a", "href", "mailto:"+ oMail);
            xml.WriteRaw(oMail);
            generator.TerminateTag("a");
            html.WriteBr();
        }
    }
}
