using System.Collections.Generic;
public class MultiColorLine
{
    /// <summary>
    /// Používej vždy tuto metodu, ne tu s parametrem width
    /// Protože je všechno zadáno v %, nemusíš specifikokovat šířku stránky
    /// </summary>
    /// <returns></returns>
    public static string GetHtml()
    {
        return GetHtml(800);
    }

    /// <summary>
    /// Tuto metodu nepoužívej, používej tu bez parametru A1
    /// </summary>
    /// <param name="width"></param>
    /// <returns></returns>
    public static string GetHtml(short width)
    {
        byte[] sirky = GeneralHelper.percentOfCountBars[width];
        List<string> colors = GeneralHelper.colorsOfBars[width];
        HtmlGenerator hg = new HtmlGenerator();
        int bl = sirky.Length -1;
        for (int i = 0; i < bl; i++)
        {
            hg.WriteTagWith2Attrs("div", "class", "castBarevneListy", "style", "width:" + sirky[i] + "%;background-color:" + colors[i]);
            hg.TerminateTag("div");
        }
        hg.WriteTagWith2Attrs("div", "class", "castBarevneListy", "style", "width:" + sirky[bl] + "%;background-color:" + colors[bl] + ";float:right;");
        hg.TerminateTag("div");
        return hg.ToString();
    }

    public static string GetHtmlCustom(short width)
    {
        List<string> colors;
        byte[] sirky;
        GeneralHelper.CalculatePercentOfColorBar(31, out colors, out sirky);
        HtmlGenerator hg = new HtmlGenerator();
        int bl = sirky.Length - 1;
        for (int i = 0; i < bl; i++)
        {
            hg.WriteTagWith2Attrs("div", "class", "castBarevneListy", "style", "width:" + sirky[i] + "%;background-color:" + colors[i]);
            hg.TerminateTag("div");
        }
        hg.WriteTagWith2Attrs("div", "class", "castBarevneListy", "style", "width:" + sirky[bl] + "%;background-color:" + colors[bl] + ";float:right;");
        hg.TerminateTag("div");
        return hg.ToString();
    }
}
