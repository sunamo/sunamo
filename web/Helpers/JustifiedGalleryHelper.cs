using System;
using System.Collections.Generic;

public class JustifiedGalleryHelper
{
    public static string GetInnerHtml(List<string> anchors, List<string> images, List<string> alts)
    {
        string[] anch = null;
        if (anchors != null)
        {
            anch = anchors.ToArray();
        }
        return GetInnerHtml(anch, images.ToArray(), alts.ToArray());
    }


    public static string GetInnerHtml(string[] anchors, string[] images, string[] alts)
    {
        if (alts.Length != images.Length)
        {
            throw new Exception("JustifiedGalleryHelper.GetInnerHtml: Počet prvků v kolekci alts(" + alts.Length + ") nesouhlasí s počtem prvků v kolekci images("+images.Length + ")");
        }

        if (anchors != null)
        {
            if (anchors.Length != alts.Length)
            {
                throw new Exception("JustifiedGalleryHelper.GetInnerHtml: Počet prvků v kolekci anchors(" + anchors.Length + ") nesouhlasí s počtem prvků v kolekci alts(" + alts.Length + ")");
            }
        }

        HtmlGenerator hg = new HtmlGenerator();
        int c = images.Length;
        if (anchors == null)
        {
            for (int i = 0; i < c; i++)
            {
                hg.WriteNonPairTagWithAttrs("img", "alt", alts[i], "src", images[i]);
            }
        }
        else
        {
            for (int i = 0; i < c; i++)
            {
                hg.WriteTagWithAttr("a", "href", anchors[i]);
                hg.WriteNonPairTagWithAttrs("img", "alt", alts[i], "src", images[i]);
                hg.TerminateTag("a");
            }
        }
        return hg.ToString();
    }

    public static string GetInnerHtmlSunamoImagesViewer(List<string> photosNames2, List<string> javacript, List<string> imagesTn)
    {
        if (photosNames2.Count != javacript.Count)
        {
            throw new Exception("JustifiedGalleryHelper.GetInnerHtmlSunamoImagesViewer: Počet prvků v kolekci photosNames2(" + photosNames2.Count + ") nesouhlasí s počtem prvků v kolekci alts(" + javacript.Count + ")");
        }

        if (photosNames2.Count != imagesTn.Count)
        {
            throw new Exception("JustifiedGalleryHelper.GetInnerHtmlSunamoImagesViewer: Počet prvků v kolekci photosNames2(" + photosNames2.Count + ") nesouhlasí s počtem prvků v kolekci imagesTn(" + imagesTn.Count + ")");
        }

        HtmlGenerator hg = new HtmlGenerator();
        int c = javacript.Count;
        for (int i = 0; i < c; i++)
        {
            hg.WriteTagWithAttr("a", "href", "javascript:" + javacript[i]);
            hg.WriteNonPairTagWithAttrs("img", "alt", photosNames2[i] , "src", imagesTn[i]);
            hg.TerminateTag("a");
        }
        return hg.ToString();
    }
}
