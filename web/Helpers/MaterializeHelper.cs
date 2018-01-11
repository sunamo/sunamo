using System;
using System.Collections.Generic;

public static class MaterializeHelper
{
    public static string Chips(IList<string> anchors, IList<string> chips)
    {
        HtmlGenerator hg = new HtmlGenerator();
        if (anchors.Count != chips.Count)
        {
            throw new Exception("V kolekci anchors je " + anchors.Count + " prvků, v kolekci chips " + chips.Count);
        }
        for (int i = 0; i < chips.Count; i++)
        {
            hg.WriteTagWithAttrs("div", "onclick", "OpenInNewTab('" + anchors[i] + "');", "class", "chip");
            hg.WriteRaw(chips[i]);
            hg.TerminateTag("div");
        }
        return hg.ToString();
    }

    public static string Cards(List<string> imagesUri, List<string> headers, List<string> description, List<List<string>> anchors, List<string> anchorsLabel)
    {
        HtmlGenerator hg = new HtmlGenerator();

        for (int i = 0; i < imagesUri.Count; i++)
        {
            hg.WriteTagWith2Attrs("div", "class", "card hoverable", "style", "height: auto; width: 100%;max-width: 440px;");

            hg.WriteTagWithAttr("div", "class", "card-image");

            hg.WriteNonPairTagWithAttrs("img", "src", imagesUri[i], "class", "responsive-img");
            hg.WriteTagWithAttr("span", "class", "card-title");
            hg.WriteRaw(headers[i]);
            hg.TerminateTag("span");

            hg.TerminateTag("div");

            hg.WriteTagWithAttr("div", "class", "card-content");
            hg.WriteElement("p", description[i]);
            hg.TerminateTag("div");

            hg.WriteTagWithAttr("div", "class", "card-action");

            for (int y = 0; y < anchors.Count; y++)
            {
                string anchor = anchors[y][i];
                if (anchor != "")
                {
                    hg.WriteTagWithAttr("a", "href", anchor);
                    hg.WriteRaw(anchorsLabel[y]);
                    hg.TerminateTag("a");
                }
            }
            
            hg.TerminateTag("div");

            hg.TerminateTag("div");
        }

        return hg.ToString();
    }
}
