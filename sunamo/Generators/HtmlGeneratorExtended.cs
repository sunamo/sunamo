

using Resources;
using sunamo.Constants;
using System;

public class HtmlGeneratorExtended : HtmlGenerator
{
    static Type type = typeof(HtmlGeneratorExtended);

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
            WriteTagWithAttr("a", "href", "mailto:" + oMail);
            WriteRaw(oMail);
            TerminateTag("a");
            WriteBr();
        }
    }

    public void BoilerplateStart( BoilerplateStartArgs a)
    {
        WriteRaw(ResourcesDuo.Html5BoilerplateStart);
        string css = a.css;
        if (a.directInject)
        {
            if (!string.IsNullOrEmpty(css))
            {
                WriteTagWithAttr(HtmlTags.style, HtmlAttrs.type, HtmlAttrValue.textCss);
                if (FS.ExistsFile(css))
                {
                    WriteRaw(TF.ReadFile(css));
                }
                else
                {
                    WriteRaw(css);
                }
                TerminateTag(HtmlTags.style);
            }
        }
        else
        {
            WriteTagWith2Attrs(HtmlTags.link, HtmlAttrs.rel, HtmlAttrValue.stylesheet, HtmlAttrs.href, css);
        }

        if (!string.IsNullOrEmpty(a.js))
        {
            WriteTagWithAttr(HtmlTags.script, HtmlAttrs.type, HtmlAttrValue.textJavascript);
            WriteRaw(a.js);
            TerminateTag(HtmlTags.script);
        }
    }

    public void BoilerplateMiddle(BoilerplateMiddleArgs a = null)
    {
        string gt = "<body";

        var html = Resources.ResourcesDuo.Html5BoilerplateMiddle;
        if (a != null)
        {
            if (!string.IsNullOrEmpty(a.onload))
            {
                html = html.Replace(gt, gt + " onload=\"" + a.onload + "\"");
            }
        }
        WriteRaw(html);
        //ThrowExceptions.NotImplementedMethod(Exc.GetStackTrace(),type, Exc.CallingMethod());
    }

    public void BoilerplateEnd()
    {
        WriteRaw(Resources.ResourcesDuo.Html5BoilerplateEnd);
        //ThrowExceptions.NotImplementedMethod(Exc.GetStackTrace(),type, Exc.CallingMethod());
    }
}