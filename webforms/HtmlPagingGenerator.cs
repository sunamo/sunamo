using System;
using System.Collections.Generic;
using System.Linq;


/// <summary>
/// Třída která generuje stránkování.
/// </summary>
public class HtmlPagingGenerator //: HtmlGenerator
{
    public class UniqueNumbers
    {
        public  int Lyr1 = 1;
        public int Geo1 = 1;
        public int Sda1 = 1;
    }

    /// <summary>
    /// Na rozdíl od metody ReturnPage se počítá od nuly
    /// </summary>
     public List<int[]> rozrazeny = new List<int[]>();
     int pocetStranek  = -1;
     string msss = null;
     string unique = "";
     public static UniqueNumbers Unique = new UniqueNumbers();
     public int raditPo = 0;

    /// <summary>
    /// A4 se přidává za JS metodu preskocNaStrankuSda kde Sda je MSS
    /// </summary>
    /// <param name="idcka"></param>
    /// <param name="raditPo"></param>
    /// <param name="mss"></param>
    /// <param name="unique"></param>
    public HtmlPagingGenerator(List<int> idcka, int raditPo, MySitesShort mss, int unique ) //string _dotaz,
	{
        
        int count = idcka.Count;
        if (count == 0)
        {
            return;
        }
        if (unique != 0)
        {
            this.unique = unique.ToString();
        }
        msss = mss.ToString();
        this.raditPo = raditPo;
        int stranek = (count / raditPo) ;
        int zacitNa = 0;
        int i = 0;
        while (true)
        {
            if (i == stranek)
            {
                raditPo = count % raditPo;
                if (raditPo == 0)
                {
                    break;
                }
            }
            if (i == stranek + 1)
            {
                break;
            }
            i++;
            int[] nt = new int[raditPo];
            idcka.CopyTo(zacitNa, nt, 0, raditPo);
            rozrazeny.Add(nt);
            pocetStranek++;
            zacitNa += raditPo;
        }
        //pocetStranek++;
	}

    public HtmlPagingGenerator(List<short> idcka, int raditPo, MySitesShort mss, int unique) : this(BTS.CastListShortToListInt(idcka), raditPo, mss, unique)
    {
    }

    private HtmlPagingGenerator()
    {
        // TODO: Complete member initialization
    }

    /// <summary>
    /// Generuje divPagingOP. 
    /// Číslování A1 je od 1 
    /// </summary>
    /// <param name="aktStrana"></param>
    /// <returns></returns>
    public  string ReturnPage(int aktStrana, CssFramework cssfw)
    {
        int posledniIndex = rozrazeny.Count - 1;
        if (aktStrana < 0 && aktStrana > posledniIndex)
        {
            return "Nepovolené hodnoty řazení, zřejmě interní chyba aplikace";
        }
        
        return Render(aktStrana, pocetStranek, cssfw);
    }

    /// <summary>
    /// Číslování A1 je od 1.
    /// Generuje divPagingOP. 
    /// Nepoužívat toto, ale metodu ReturnPage.
    /// </summary>
    /// <param name="aktStrana"></param>
    /// <param name="zobrazitBefore"></param>
    /// <param name="zobrazitNext"></param>
    /// <returns></returns>
    public string Render(int aktStrana, int pocetStranek2, CssFramework cssfw)
    {
        int pocetStranek = pocetStranek2 + 1;
        if (pocetStranek != 0)
        {
            if (cssfw == CssFramework.Metro)
            {
                int aktStranaIndex = aktStrana - 1;
                bool zobrazitBefore = aktStranaIndex != 0;
                bool zobrazitNext = aktStranaIndex != pocetStranek - 1;
                int zbyvaZnakuCelkem = 29;
                int zbyvaZnakuLeft = 0;
                int zbyvaZnakuRight = 0;

                zbyvaZnakuCelkem -= aktStrana.ToString().Length + 2;
                zbyvaZnakuLeft = zbyvaZnakuCelkem / 2;
                zbyvaZnakuRight = (zbyvaZnakuCelkem / 2);
                List<int> stranyLeft = new List<int>();
                List<int> stranyRight = new List<int>();

                if (aktStrana != 1)
                {
                    for (int i = aktStrana - 1; i > 0; i--)
                    {
                        int delkaDalsiho = NH.MakeUpTo2NumbersToZero(i).Length + 1;
                        if (zbyvaZnakuLeft - delkaDalsiho > -1)
                        {
                            stranyLeft.Add(i);
                            zbyvaZnakuLeft -= delkaDalsiho;
                        }
                        else
                        {
                            break;
                        }
                    }
                }
                zbyvaZnakuRight = zbyvaZnakuRight + zbyvaZnakuLeft;
                for (int i = aktStrana + 1; i < pocetStranek + 1; i++)
                {
                    int delkaDalsiho = NH.MakeUpTo2NumbersToZero(i).Length + 1;

                    if (zbyvaZnakuRight - delkaDalsiho > -1)
                    {
                        stranyRight.Add(i);
                        zbyvaZnakuRight -= delkaDalsiho;
                    }
                    else
                    {

                        break;
                    }
                }

                if (stranyLeft.Count != 0)
                {
                    zbyvaZnakuLeft = zbyvaZnakuRight + zbyvaZnakuLeft;
                    for (int i = stranyLeft[stranyLeft.Count - 1] - 1; i > 1; i--)
                    {
                        int delkaDalsiho = i.ToString().Length + 1;
                        if (zbyvaZnakuLeft - delkaDalsiho > -1)
                        {
                            stranyLeft.Add(i);
                            zbyvaZnakuLeft -= delkaDalsiho;
                        }
                        else
                        {
                            break;
                        }
                    }
                }

                HtmlGenerator hg = new HtmlGenerator();

                string textBeforeRapid = string.Empty;
                string textBefore = string.Empty;
                if (zobrazitBefore)
                {
                    textBeforeRapid = "&lt;&lt;";
                    textBefore = "&lt;";
                }
                else
                {
                    textBeforeRapid = "&nbsp;&nbsp;";
                    textBefore = "&nbsp;";
                }

                string textNextRapid = string.Empty;
                string textNext = string.Empty;
                if (zobrazitNext)
                {
                    textNextRapid = "&gt;&gt;";
                    textNext = "&nbsp;&nbsp;&nbsp;&gt;";
                }
                else
                {
                    textNextRapid = "&nbsp;&nbsp;";
                    textNext = "&nbsp;";
                }

                hg.WriteTagWithAttr("div", "class", "pagination no-border");
                if (zobrazitBefore)
                {
                    hg.WriteTagWithAttr("div", "class", "item");
                    hg.WriteTagWithAttrs("a", "runat", "server", "id", "anchorPrevRapid", "href", "javascript:", "onclick", "preskocNaStranku" + msss + unique + "(" + 1 + ");return false;");
                    hg.WriteRaw(textBeforeRapid);
                    hg.TerminateTag("a");
                    hg.TerminateTag("div");

                    int to = aktStrana - 1;
                    if (to < 1)
                    {
                        to = 1;
                    }
                    hg.WriteTagWithAttr("div", "class", "item");
                    hg.WriteTagWithAttrs("a", "runat", "server", "id", "anchorPrev", "href", "javascript:", "onclick", "preskocNaStranku" + msss + unique + "(" + to + ");return false;");
                    hg.WriteRaw(textBefore);
                    hg.TerminateTag("a");
                    hg.TerminateTag("div");
                }
                else
                {
                }
                stranyLeft.Reverse();

                foreach (int item in stranyLeft)
                {
                    AppendAnchor(hg, true, item, msss, unique, cssfw);
                }
                AppendAnchor(hg, false, aktStrana, msss, unique, cssfw);
                foreach (int item in stranyRight)
                {
                    AppendAnchor(hg, true, item, msss, unique, cssfw);
                }

                if (zobrazitNext)
                {

                    hg.WriteTagWithAttr("div", "class", "item");
                    hg.WriteTagWithAttrs("a", "runat", "server", "id", "anchorNext", "href", "javascript:", "onclick", "preskocNaStranku" + msss + unique + "(" + (aktStrana + 1).ToString() + ");return false;");
                    hg.WriteRaw(textNext);
                    hg.TerminateTag("a");
                    hg.TerminateTag("div");
                    //hg.WriteRaw("&nbsp;&nbsp;");
                    hg.WriteTagWithAttr("div", "class", "item");
                    hg.WriteTagWithAttrs("a", "runat", "server", "id", "anchorRapid", "href", "javascript:", "onclick", "preskocNaStranku" + msss + unique + "(" + pocetStranek + ");return false;");
                    hg.WriteRaw(textNextRapid);
                    hg.TerminateTag("a");


                    hg.TerminateTag("div");
                }
                else
                {
                }
                hg.TerminateTag("div");
                return hg.ToString();
            }
            else if (cssfw == CssFramework.Materialize)
            {
                int aktStranaIndex = aktStrana - 1;
                bool zobrazitBefore = aktStranaIndex != 0;
                bool zobrazitNext = aktStranaIndex != pocetStranek - 1;
                int zbyvaZnakuCelkem = 29;
                int zbyvaZnakuLeft = 0;
                int zbyvaZnakuRight = 0;

                zbyvaZnakuCelkem -= aktStrana.ToString().Length + 2;
                zbyvaZnakuLeft = zbyvaZnakuCelkem / 2;
                zbyvaZnakuRight = (zbyvaZnakuCelkem / 2);
                List<int> stranyLeft = new List<int>();
                List<int> stranyRight = new List<int>();

                if (aktStrana != 1)
                {
                    for (int i = aktStrana - 1; i > 0; i--)
                    {
                        int delkaDalsiho = NH.MakeUpTo2NumbersToZero(i).Length + 1;
                        if (zbyvaZnakuLeft - delkaDalsiho > -1)
                        {
                            stranyLeft.Add(i);
                            zbyvaZnakuLeft -= delkaDalsiho;
                        }
                        else
                        {
                            break;
                        }
                    }
                }
                zbyvaZnakuRight = zbyvaZnakuRight + zbyvaZnakuLeft;
                for (int i = aktStrana + 1; i < pocetStranek + 1; i++)
                {
                    int delkaDalsiho = NH.MakeUpTo2NumbersToZero(i).Length + 1;

                    if (zbyvaZnakuRight - delkaDalsiho > -1)
                    {
                        stranyRight.Add(i);
                        zbyvaZnakuRight -= delkaDalsiho;
                    }
                    else
                    {

                        break;
                    }
                }

                if (stranyLeft.Count != 0)
                {
                    zbyvaZnakuLeft = zbyvaZnakuRight + zbyvaZnakuLeft;
                    for (int i = stranyLeft[stranyLeft.Count - 1] - 1; i > 1; i--)
                    {
                        int delkaDalsiho = i.ToString().Length + 1;
                        if (zbyvaZnakuLeft - delkaDalsiho > -1)
                        {
                            stranyLeft.Add(i);
                            zbyvaZnakuLeft -= delkaDalsiho;
                        }
                        else
                        {
                            break;
                        }
                    }
                }

                HtmlGenerator hg = new HtmlGenerator();

                string textBeforeRapid = string.Empty;
                string textBefore = string.Empty;
                if (zobrazitBefore)
                {
                    textBeforeRapid = "&lt;&lt;";
                    textBefore = "&lt;";
                }
                else
                {
                    textBeforeRapid = "&nbsp;&nbsp;";
                    textBefore = "&nbsp;";
                }

                string textNextRapid = string.Empty;
                string textNext = string.Empty;
                if (zobrazitNext)
                {
                    textNextRapid = "&gt;&gt;";
                    textNext = "&nbsp;&nbsp;&nbsp;&gt;";
                }
                else
                {
                    textNextRapid = "&nbsp;&nbsp;";
                    textNext = "&nbsp;";
                }

                hg.WriteTagWithAttr("ul", "class", "pagination");
                if (zobrazitBefore)
                {
                    hg.WriteTagWithAttr("li", "class", "waves-effect");
                    hg.WriteTagWithAttrs("a", "runat", "server", "id", "anchorPrevRapid", "href", "javascript:", "onclick", "preskocNaStranku" + msss + unique + "(" + 1 + ");return false;");
                    hg.WriteRaw(textBeforeRapid);
                    hg.TerminateTag("a");
                    hg.TerminateTag("li");

                    int to = aktStrana - 1;
                    if (to < 1)
                    {
                        to = 1;
                    }
                    hg.WriteTagWithAttr("li", "class", "waves-effect");
                    hg.WriteTagWithAttrs("a", "runat", "server", "id", "anchorPrev", "href", "javascript:", "onclick", "preskocNaStranku" + msss + unique + "(" + to + ");return false;");
                    hg.WriteRaw(textBefore);
                    hg.TerminateTag("a");
                    hg.TerminateTag("li");
                }
                else
                {
                }
                stranyLeft.Reverse();

                foreach (int item in stranyLeft)
                {
                    AppendAnchor(hg, true, item, msss, unique, cssfw);
                }
                AppendAnchor(hg, false, aktStrana, msss, unique, cssfw);
                foreach (int item in stranyRight)
                {
                    AppendAnchor(hg, true, item, msss, unique, cssfw);
                }

                if (zobrazitNext)
                {

                    hg.WriteTagWithAttr("li", "class", "waves-effect");
                    hg.WriteTagWithAttrs("a", "runat", "server", "id", "anchorNext", "href", "javascript:", "onclick", "preskocNaStranku" + msss + unique + "(" + (aktStrana + 1).ToString() + ");return false;");
                    hg.WriteRaw(textNext);
                    hg.TerminateTag("a");
                    hg.TerminateTag("li");
                    //hg.WriteRaw("&nbsp;&nbsp;");
                    hg.WriteTagWithAttr("li", "class", "waves-effect");
                    hg.WriteTagWithAttrs("a", "runat", "server", "id", "anchorRapid", "href", "javascript:", "onclick", "preskocNaStranku" + msss + unique + "(" + pocetStranek + ");return false;");
                    hg.WriteRaw(textNextRapid);
                    hg.TerminateTag("a");


                    hg.TerminateTag("li");
                }
                else
                {
                }
                hg.TerminateTag("div");
                return hg.ToString();
            }
        }
        return "";
    }


    private static int AppendAnchor( HtmlGenerator hg, bool klikaci, int item, string msss, string unique, CssFramework cssfw)
    {
        if (cssfw == CssFramework.Materialize)
        {
            hg.WriteTagWithAttr("li", "class", "waves-effect");
            if (klikaci)
            {
                hg.WriteTagWithAttrs("a", "href", "javascript:", "onclick", "preskocNaStranku" + msss + unique + "(" + item + ");return false;");
            }
            else
            {
                //hg.WriteTagWithAttr("span", "class", "strankovani");
            }
            hg.WriteRaw(item.ToString().TrimStart('0'));
            if (klikaci)
            {
                hg.TerminateTag("a");
            }
            else
            {
                //hg.TerminateTag("span");
            }
            //hg.WriteRaw(" ");
            hg.TerminateTag("li");
        }
        else if (cssfw == CssFramework.Metro)
        {
            hg.WriteTagWithAttr("div", "class", "item");
            if (klikaci)
            {
                hg.WriteTagWithAttrs("a", "href", "javascript:", "onclick", "preskocNaStranku" + msss + unique + "(" + item + ");return false;", "class", "strankovani");
            }
            else
            {
                //hg.WriteTagWithAttr("span", "class", "strankovani");
            }
            hg.WriteRaw(item.ToString().TrimStart('0'));
            if (klikaci)
            {
                hg.TerminateTag("a");
            }
            else
            {
                //hg.TerminateTag("span");
            }
            //hg.WriteRaw(" ");
            hg.TerminateTag("div");
        }
        return item;
    }

    /// <summary>
    /// Tuto metodu používej když se žádné výsledky nepodaří najít
    /// </summary>
    /// <returns></returns>
    public static string NothingFound(CssFramework cssfw)
    {
        HtmlPagingGenerator hpg = new HtmlPagingGenerator();
        return hpg.Render(1, 0, cssfw);
    }

    /// <summary>
    /// Tuto metodu volej pouze tehdy, když se všechny výsledky vlezou na 1 stranu
    /// </summary>
    /// <returns></returns>
    public static string WoPaging(CssFramework cssfw)
    {
        HtmlPagingGenerator hpg = new HtmlPagingGenerator();
        return hpg.Render(1, 0, cssfw);
    }

    /// <summary>
    /// A1 je od nuly
    /// </summary>
    /// <param name="seriePage"></param>
    /// <returns></returns>
    public int[] GetPagedOrZeroIfOutOfRange(int seriePage)
    {
        if (rozrazeny.Count == 0)
        {
            return new int[0];
        }
        if (rozrazeny.Count > seriePage && seriePage > -1)
        {
            return rozrazeny[seriePage];
        }
        return new int[0];
    }

    public List<short[]> ConvertRozrazenyToShort()
    {
        List<short[]> vr = new List<short[]>();
        foreach (var item in rozrazeny)
        {
            vr.Add(CA.ToShort(item).ToArray());
        }
        return vr;
    }

    /// <summary>
    /// Zadává se od 0
    /// Vrátí virtuální série, tedy při stránkování po 2 a IDčkách 1, 5, 7, a 8 vrátí pro A1 1 3, 4
    /// </summary>
    /// <param name="pi"></param>
    /// <returns></returns>
    public string[] SeriesOnIndex(int pi)
    {
        if (rozrazeny.Count != 0)
        {
            int od = ((pi) * raditPo) + 1;
            var skutecnyPocetPrvku = rozrazeny[pi].Length;
            int pocet = 0;
            for (int i = 0; i < pi; i++)
            {
                pocet += rozrazeny[i].Length;
            }
            int to = pocet + skutecnyPocetPrvku;
            return CA.ToListString(NH.GenerateIntervalInt(od, to)).ToArray();
        }
        return new string[0];
    }
}
