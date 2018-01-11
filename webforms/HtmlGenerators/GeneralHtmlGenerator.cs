using sunamo;
using System;
using System.Text;
using System.Web;

namespace web
{
    public class GeneralHtmlGenerator
    {
        public static string ViewCountToday(SunamoPage sp, bool addX)
        {
            string add = "";
            if (addX)
            {
                add = "x";
            }
            return "<b>Počet shlédnutí dnes: </b>" + sp.today + add + " " + AnchorToShowViews(sp);
        }

        public static string AnchorToShowViews(SunamoPage sp)
        {
            return "<a href =\"" + UH.GetWebUri3(sp, "ShowViews.aspx?idPage=" + sp.idPage + "\">Zobrazit graf zobrazení za posledních 7 dní</a>");
        }

        public static string AnchorToShowViews(SunamoPage sp, int idPage)
        {
            if (idPage == int.MaxValue)
            {
                return "";
            }
            return "<a href =\"" + UH.GetWebUri3(sp, "ShowViews.aspx?idPage=" + idPage + "\">Zobrazit graf zobrazení za posledních 7 dní</a>");
        }

        public static string ViewCountToday(SunamoPage sp)
        {
            return "<b>Počet shlédnutí dnes: </b>" + sp.today + " <a href=\"" + UH.GetWebUri3(sp, "ShowViews.aspx?idPage=" + sp.idPage + "\">Zobrazit graf zobrazení za posledních 7 dní</a>");
        }

        public static string ViewCountOverall(SunamoPage sp)
        {
            return "<b>Počet shlédnutí: </b>" + sp.overall.ToString();
        }

        public static string ViewCountLast7Days(int aViewLastWeek)
        {
            return "<b>Počet shlédnutí za posledních 7 dnů: </b>" + NormalizeNumbers.NormalizeInt(aViewLastWeek).ToString();
        }

        public static string ViewCountLast7Days(int aViewLastWeek, bool addX)
        {
            string add = "";
            if (addX)
            {
                add = "x";
            }
            return "<b>Počet shlédnutí za posledních 7 dnů: </b>" + NormalizeNumbers.NormalizeInt(aViewLastWeek).ToString() + add;
        }

        public static string GenerateRowOfSurveyAnswer(int idAnswer, string name, short idColor, string voteCountS)
        {
            HtmlGenerator hg = new HtmlGenerator();
            string id = idAnswer.ToString();
            string barvaHex = DatabaseRows.colors[idColor].hex;
            hg.WriteTagWith2Attrs("div", "class", "bunkaTabulky", "id", "divSurveyAnswerRow" + id);
            hg.WriteTagWith2Attrs("div", "style", "width: " + Constants.sirkaNazev + ";color:" + barvaHex + ";display: inline-block;text-align: center;", "id", "divAnswerName" + id);
            hg.WriteRaw(name);
            hg.TerminateTag("div");


            hg.WriteTagWith2Attrs("div", "style", "width: " + Constants.sirkaVoteCount + ";color:" + barvaHex + ";display: inline-block;text-align: center;", "id", "divAnswerVoteCount" + id);
            hg.WriteRaw(voteCountS);
            hg.TerminateTag("div");

            hg.WriteTagWithAttr("div", "style", "width: " + Constants.sirkaButtony + ";color:black;display: inline-block;text-align: center;");
            //hg.WriteRaw("Editovací tlačítka");
            hg.WriteTagWithAttr("a", "href", "DetailsClickSurvey.aspx");
            hg.WriteNonPairTagWithAttrs("img", "alt", "Kdy bylo kliknuto na odpovědi v této anketě", "src", "../img/stats.png");
            hg.TerminateTag("a");
            hg.WriteTagWith2Attrs("a", "href", "javascript:void(0)", "onclick", "return showDivEditAnswer(" + id + ");");
            hg.WriteNonPairTagWithAttrs("img", "alt", "Editovat", "src", "../img/edit.png");
            hg.TerminateTag("a");
            hg.WriteTagWith2Attrs("a", "href", "javascript:void(0)", "onclick", "return removeSurveyAnswer(" + id + ");");
            hg.WriteNonPairTagWithAttrs("img", "alt", "Smazat odpověď", "src", "../img/remove.png");
            hg.TerminateTag("a");

            hg.TerminateTag("div");
            hg.TerminateTag("div");
            return hg.ToString();
        }
        public static string AnchorWithHttp(HttpRequest req, bool targetBlank, string www, string text)
        {
            string http = UH.AppendSiteNameIfNotExists(req, www);
            http = sunamo.UH.AppendHttpIfNotExists(http);
            return HtmlGenerator2.AnchorWithHttpCore(targetBlank, text, http);
        }
        public static string GetNovelty(string HtmlContent, DateTime DT, String User)
        {
            HtmlGenerator hg = new HtmlGenerator();
            hg.WriteTagWithAttr("div", "class", "textVlevo tucne");
            hg.WriteRaw(DT.ToShortDateString());
            hg.TerminateTag("div");
            hg.WriteBr();
            hg.WriteTagWithAttr("div", "class", "textVlevo");
            hg.WriteRaw(HtmlContent);
            hg.TerminateTag("div");
            hg.WriteTagWithAttr("div", "class", "textVpravo");
            hg.WriteRaw(User);
            hg.TerminateTag("div");
            hg.WriteNonPairTag("hr");
            return hg.ToString();
        }





    }
}
