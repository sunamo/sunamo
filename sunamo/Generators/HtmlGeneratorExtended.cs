using ResourcesShared;
using sunamo.Constants;
using System;

public class HtmlGeneratorExtended : HtmlGenerator
{
    public void DetailAnchor(string label, string oUriYouthProfile, string oNameYouthProfile)
    {
        if (!string.IsNullOrEmpty(oNameYouthProfile))
        {
            WriteElement("b", label + AllStrings.colon);
            WriteRaw(AllStrings.space);
            if (string.IsNullOrEmpty(oUriYouthProfile))
            {
                WriteRaw(oNameYouthProfile);
            }
            else
            {
                WriteTagWithAttr("a", "href", oUriYouthProfile);
                WriteRaw(oNameYouthProfile);
                TerminateTag("a");
            }
            WriteBr();
        }
    }

    public void Detail(string label, string timeInterval)
    {
        if (!string.IsNullOrEmpty(timeInterval))
        {
            WriteElement("b", label + AllStrings.colon);
            WriteRaw(AllStrings.space);
            WriteRaw(timeInterval);
            WriteBr();
        }
    }

    public void DetailNewLine(string label, string oDescriptionHtml)
    {
        if (!string.IsNullOrEmpty(oDescriptionHtml))
        {
            WriteElement("b", label);
            WriteBr();
            WriteRaw(oDescriptionHtml);
            WriteBr();
        }
    }

    public void DetailMailto(string label, string oMail)
    {
        if (!string.IsNullOrEmpty(oMail))
        {
            WriteElement("b", label + AllStrings.colon);
            WriteRaw(AllStrings.space);
            WriteTagWithAttr("a", "href", "mailto" + ":" + oMail);
            WriteRaw(oMail);
            TerminateTag("a");
            WriteBr();
        }
    }

    public void BoilerplateStart(string css, bool directInject)
    {
        WriteRaw(ResourcesDuo.Html5BoilerplateStart);
        if (directInject)
        {
            WriteTagWithAttr(HtmlTags.style, HtmlAttrs.type, HtmlAttrValue.textCss);
            WriteRaw(TF.ReadFile(css));
            TerminateTag(HtmlTags.style);
        }
        else
        {
            WriteTagWith2Attrs(HtmlTags.link, HtmlAttrs.rel, HtmlAttrValue.stylesheet, HtmlAttrs.href, css);
        }
    }

    public void BoilerplateMiddle()
    {
        //WriteRaw(Resources.)
    }

    public void BoilerplateEnd()
    {
        throw new NotImplementedException();
    }
}
