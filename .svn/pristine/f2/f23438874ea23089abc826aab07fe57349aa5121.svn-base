using System;

public class HtmlGeneratorExtended : HtmlGenerator
{
    public void DetailAnchor(string label, string oUriYouthProfile, string oNameYouthProfile)
    {
        if (!string.IsNullOrEmpty(oNameYouthProfile))
        {
            WriteElement("b", label + ":");
            WriteRaw(" ");
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
            WriteElement("b", label + ":");
            WriteRaw(" ");
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
            WriteElement("b", label + ":");
            WriteRaw(" ");
            WriteTagWithAttr("a", "href", "mailto:"+ oMail);
            WriteRaw(oMail);
            TerminateTag("a");
            WriteBr();
        }
    }
}
