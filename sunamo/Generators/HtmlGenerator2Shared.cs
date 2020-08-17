using sunamo;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;

/// <summary>
/// Summary description for HtmlGenerator2
/// </summary>
public partial class HtmlGenerator2 : HtmlGenerator
{
    public static string Calendar(List<string> htmlBoxesEveryDay, int year, int mesic)
    {
        List<string> colors = new List<string>(htmlBoxesEveryDay.Count);
        foreach (var item in htmlBoxesEveryDay)
        {
            colors.Add(null);
        }
        return Calendar(htmlBoxesEveryDay, colors, year, mesic);
    }

    public static string GenerateHtmlCheckBoxesFromFiles(string path, string masc, SearchOption so)
    {
        HtmlGenerator hg = new HtmlGenerator();

        var files = FS.GetFiles(path, masc, so);
        foreach (var item in files)
        {
            hg.WriteTagWithAttrs("input", "type", "checkbox");
            hg.WriteRaw(FS.GetFileName(item));
            hg.WriteBr();
        }

        return hg.ToString();
    }

    public static string Calendar(List<string> htmlBoxesEveryDay, List<string> colors, int year, int mesic)
    {
        HtmlGenerator hg = new HtmlGenerator();
        hg.WriteTagWith2Attrs("table", "class", "tabulkaNaStredAutoSirka", "style", "width: 600px");
        hg.WriteTag("tr");
        #region Zapíšu vrchní řádky - názvy dnů
        List<string> ppp = DTConstants.daysInWeekEN;
        hg.WriteTagWithAttr("td", "class", "bunkaTabulkyKalendare bunkaTabulkyKalendareLeft bunkaTabulkyKalendareTop");
        hg.WriteElement("b", ppp[0]);
        hg.TerminateTag("td");
        for (int i = 1; i < ppp.Count - 1; i++)
        {
            hg.WriteTagWithAttr("td", "class", "bunkaTabulkyKalendare bunkaTabulkyKalendareTop");
            hg.WriteElement("b", ppp[i]);
            hg.TerminateTag("td");
        }
        hg.WriteTagWithAttr("td", "class", "bunkaTabulkyKalendare bunkaTabulkyKalendareRight bunkaTabulkyKalendareTop");
        hg.WriteElement("b", ppp[ppp.Count - 1]);
        hg.TerminateTag("td");
        #endregion

        hg.TerminateTag("tr");

        hg.WriteTag("tr");
        DateTime dt = new DateTime(year, mesic, 1);
        int prazdneNaZacatku = 0;
        DayOfWeek dow = dt.DayOfWeek;
        switch (dow)
        {
            case DayOfWeek.Friday:
                prazdneNaZacatku = 4;
                break;
            case DayOfWeek.Monday:
                break;
            case DayOfWeek.Saturday:
                prazdneNaZacatku = 5;
                break;
            case DayOfWeek.Sunday:
                prazdneNaZacatku = 6;
                break;
            case DayOfWeek.Thursday:
                prazdneNaZacatku = 3;
                break;
            case DayOfWeek.Tuesday:
                prazdneNaZacatku = 1;
                break;
            case DayOfWeek.Wednesday:
                prazdneNaZacatku = 2;
                break;
            default:
                break;
        }
        for (int i2 = 0; i2 < prazdneNaZacatku; i2++)
        {
            string pt2 = "";
            if (i2 == 0)
            {
                pt2 = "bunkaTabulkyKalendareLeft";
            }
            hg.WriteTagWithAttr("td", "class", "bunkaTabulkyKalendare " + pt2);
            hg.WriteRaw("&nbsp;");
            hg.TerminateTag("td");
        }

        int radku2 = prazdneNaZacatku + (htmlBoxesEveryDay.Count / 7);
        if (prazdneNaZacatku != 0)
        {
            radku2++;
        }


        int prazdneNaZacatku2 = prazdneNaZacatku;
        int radku = 1;
        for (int i = 1; i < htmlBoxesEveryDay.Count + 1; i++, prazdneNaZacatku++)
        {
            string pridatTridu = "";

            if (prazdneNaZacatku % 7 == 0)
            {
                pridatTridu = "bunkaTabulkyKalendareLeft";
                radku++;
                hg.TerminateTag("tr");
                hg.WriteTag("tr");
            }
            else if (prazdneNaZacatku % 7 == 6)
            {
                pridatTridu = "bunkaTabulkyKalendareRight";
            }
            string color = colors[i - 1];
            string appendStyle = "";
            if (color == "#030")
            {
                appendStyle = "color:white;";
            }
            string datum = i + AllStrings.dot + mesic + AllStrings.dot;
            hg.WriteTagWith2Attrs("td", "class", "tableCenter bunkaTabulkyKalendare " + pridatTridu, "style", appendStyle + "background-color:" + colors[i - 1]);
            //hg.WriteTag("td");
            hg.WriteRaw("<b>" + datum + "</b>");
            hg.WriteBr();
            hg.WriteRaw(htmlBoxesEveryDay[i - 1]);
            hg.TerminateTag("td");
        }
        if (prazdneNaZacatku2 == 0)
        {
            radku--;
        }

        int bunekZbyva = (radku * 7) - prazdneNaZacatku2 - htmlBoxesEveryDay.Count;
        for (int i2 = 0; i2 < bunekZbyva; i2++)
        {
            string pt = "";
            if (bunekZbyva - 1 == i2)
            {
                pt = "bunkaTabulkyKalendareRight";
            }
            hg.WriteTagWithAttr("td", "class", /*bunkaTabulkyKalendareBottom */ "bunkaTabulkyKalendare " + pt);
            hg.WriteRaw("&nbsp;");
            hg.TerminateTag("td");
        }
        hg.TerminateTag("tr");
        hg.TerminateTag("table");
        return hg.ToString();
    }

    public static string GalleryZoomInProfilePhoto(List<string> membersName, List<string> memberProfilePicture, List<string> memberAnchors)
    {
        HtmlGenerator hg = new HtmlGenerator();
        hg.WriteTag("ul");
        for (int i = 0; i < membersName.Count; i++)
        {
            hg.WriteTag("li");

            hg.WriteTagWithAttr("a", "href", memberAnchors[i]);

            hg.WriteTag("p");
            hg.WriteRaw(membersName[i]);
            hg.TerminateTag("p");

            hg.WriteTagWithAttr("div", "style", "background-image: url(" + memberProfilePicture[i] + ");");
            hg.TerminateTag("div");

            hg.TerminateTag("a");
            hg.TerminateTag("li");
        }
        hg.TerminateTag("ul");
        return hg.ToString();
    }

    public static string GetSelect(string id, object def, IEnumerable list)
    {
        HtmlGenerator gh = new HtmlGenerator();
        gh.WriteTagWithAttr("select", "name", "select" + id);
        foreach (object item2 in list)
        {
            string item = item2.ToString();
            if (item != def.ToString())
            {
                gh.WriteElement("option", item);
            }
            else
            {
                gh.WriteTagWithAttr("option", "selected", "selected");
                gh.WriteRaw(item);
                gh.TerminateTag("option");
            }
        }
        gh.TerminateTag("select");
        return gh.ToString();
    }

    public static string GetInputText(string id, string value)
    {
        HtmlGenerator gh = new HtmlGenerator();
        gh.WriteTagWithAttrs("input", "type", "text", "name", "inputText" + id, "value", value);
        return gh.ToString();
    }

    /// <summary>
    /// Jedná se o divy pod sebou, nikoliv o ol/ul>li 
    /// </summary>
    /// <param name="hg"></param>
    /// <param name="odkazyPhoto"></param>
    /// <param name="odkazyText"></param>
    /// <param name="innerHtmlText"></param>
    /// <param name="srcPhoto"></param>
    public static string TopListWithImages(HtmlGenerator hg, int widthImage, int heightImage, string initialImageUri, List<string> odkazyPhoto, List<string> odkazyText, List<string> innerHtmlText, List<string> srcPhoto, string nameJsArray)
    {
        int count = odkazyPhoto.Count;
        if (count == 0)
        {
            //ThrowExceptions.Custom(Exc.GetStackTrace(), type, Exc.CallingMethod(),"Metoda HtmlGenerator2.TopListWithImages - odkazyPhoto nemá žádný prvek");
            return "";
        }
        if (count != odkazyText.Count)
        {
            ThrowExceptions.Custom(Exc.GetStackTrace(), type, Exc.CallingMethod(),"Metoda HtmlGenerator2.TopListWithImages - odkazyPhoto se nerovn\u00E1 po\u010Dtem odkazyText");
        }
        if (count != innerHtmlText.Count)
        {
            ThrowExceptions.Custom(Exc.GetStackTrace(), type, Exc.CallingMethod(),"Metoda HtmlGenerator2.TopListWithImages - odkazyPhoto se nerovn\u00E1 po\u010Dtem innerHtmlText");
        }
        if (count != srcPhoto.Count)
        {
            ThrowExceptions.Custom(Exc.GetStackTrace(), type, Exc.CallingMethod(),"Metoda HtmlGenerator2.TopListWithImages - odkazyPhoto se nerovn\u00E1 po\u010Dtem srcPhoto");
        }

        //HtmlGenerator hg = new HtmlGenerator();
        int nt = 0;
        bool animated = int.TryParse(srcPhoto[0], out nt);

        for (int i = 0; i < count; i++)
        {
            hg.WriteTagWithAttr("div", "style", "padding: 5px;");
            hg.WriteTagWithAttr("a", "href", odkazyPhoto[i]);
            hg.WriteTagWithAttr("div", "style", "display: inline-block;");
            if (animated)
            {
                hg.WriteNonPairTagWithAttrs("img", "style", "margin-left: auto; margin-right: auto; vertical-align-middle; width: " + widthImage + "px;height:" + heightImage + "px", "id", nameJsArray + srcPhoto[i], "class", "alternatingImage", "src", initialImageUri, "alt", odkazyText[i]);
            }
            else
            {
                hg.WriteNonPairTagWithAttrs("img", "src", srcPhoto[i], "alt", odkazyText[i]);
            }
            hg.TerminateTag("div");

            hg.TerminateTag("a");
            hg.WriteTagWithAttr("a", "href", odkazyText[i]);
            hg.WriteRaw(innerHtmlText[i]);
            hg.TerminateTag("a");
            hg.TerminateTag("div");
        }

        return hg.ToString();
    }

    /// <summary>
    /// Jedná se o divy pod sebou, nikoliv o ol/ul>li 
    /// </summary>
    /// <param name="hg"></param>
    /// <param name="odkazyPhoto"></param>
    /// <param name="odkazyText"></param>
    /// <param name="innerHtmlText"></param>
    /// <param name="srcPhoto"></param>
    public static string TopListWithImages(HtmlGenerator hg, int widthImage, int heightImage, string initialImageUri, List<string> odkazyPhoto, List<string> odkazyText, List<string> innerHtmlText, List<string> srcPhoto, List<string> idBadges, string nameJsArray)
    {
        int count = odkazyPhoto.Count;
        if (count == 0)
        {
            //ThrowExceptions.Custom(Exc.GetStackTrace(), type, Exc.CallingMethod(),"Metoda HtmlGenerator2.TopListWithImages - odkazyPhoto nemá žádný prvek");
            return "";
        }
        if (count != odkazyText.Count)
        {
            ThrowExceptions.Custom(Exc.GetStackTrace(), type, Exc.CallingMethod(),"Metoda HtmlGenerator2.TopListWithImages - odkazyPhoto se nerovn\u00E1 po\u010Dtem odkazyText");
        }
        if (count != innerHtmlText.Count)
        {
            ThrowExceptions.Custom(Exc.GetStackTrace(), type, Exc.CallingMethod(),"Metoda HtmlGenerator2.TopListWithImages - odkazyPhoto se nerovn\u00E1 po\u010Dtem innerHtmlText");
        }
        if (count != srcPhoto.Count)
        {
            ThrowExceptions.Custom(Exc.GetStackTrace(), type, Exc.CallingMethod(),"Metoda HtmlGenerator2.TopListWithImages - odkazyPhoto se nerovn\u00E1 po\u010Dtem srcPhoto");
        }
        if (count != idBadges.Count)
        {
            ThrowExceptions.Custom(Exc.GetStackTrace(), type, Exc.CallingMethod(),SunamoPageHelperSunamo.i18n(XlfKeys.MetodaHtmlGenerator2TopListWithImagesOdkazyPhoto) + " " + count + " se nerovn\u00E1 po\u010Dtem idBadges " + idBadges.Count);
        }

        //HtmlGenerator hg = new HtmlGenerator();
        int nt = 0;
        bool animated = int.TryParse(srcPhoto[0], out nt);
        for (int i = 0; i < count; i++)
        {
            hg.WriteTagWith2Attrs("div", "style", "padding: 5px;", "class", "hoverable");
            hg.WriteTagWithAttr("a", "href", odkazyPhoto[i]);
            hg.WriteTagWithAttrs("div", "style", "display: inline-block;", "id", "iosBadge" + idBadges[i], "class", "iosbRepair");
            
            if (animated)
            {
                hg.WriteNonPairTagWithAttrs("img", "style", "margin-left: auto; margin-right: auto; vertical-align-middle; width: " + widthImage + "px;height:" + heightImage + "px", "id", nameJsArray + srcPhoto[i], "class", "alternatingImage", "src", initialImageUri, HtmlAttrs.alt, odkazyText[i]);
            }
            else
            {
                hg.WriteNonPairTagWithAttrs("img", "src", srcPhoto[i], HtmlAttrs.alt, odkazyText[i]);
            }

            hg.TerminateTag("div");
            hg.TerminateTag("a");

            hg.WriteTagWithAttr("a", "href", odkazyText[i]);
            hg.WriteRaw(innerHtmlText[i]);
            hg.TerminateTag("a");
            hg.TerminateTag("div");
        }
        return hg.ToString();
    }


    #region GetForUlWoCheckDuplicate
    /// <summary>
    /// Do A1 doplň třeba EditMister.aspx?mid= - co za toto si automaticky doplní a A2 jsou texty do inner textu a
    /// Nehodí se tedy proto vždy, například, když máš přehozené IDčka v DB
    /// When uri args and titles are the same
    /// </summary>
    /// <param name="baseAnchor"></param>
    /// <param name="to"></param>
    public static string GetForUlWoCheckDuplicate(string baseAnchor, List<string> to)
    {
        return GetForUlWoCheckDuplicate(baseAnchor, to, to);
    }

    /// <summary>
    /// Automatically replace A3 for A4
    /// </summary>
    /// <param name="baseAnchor"></param>
    /// <param name="idcka"></param>
    /// <param name="najitVTextu"></param>
    /// <param name="nahraditVTextu"></param>
    /// <param name="pripona"></param>
    public static string GetForUlWoCheckDuplicate(string baseAnchor, List<string> idcka, string najitVTextu, string nahraditVTextu, string pripona = "")
    {
        HtmlGenerator hg = new HtmlGenerator();

        for (int i = 0; i < idcka.Count; i++)
        {
            string s = idcka[i];

            hg.WriteTag("li");
            hg.WriteTagWithAttr("a", "href", baseAnchor + s + pripona);
            if (!string.IsNullOrEmpty(najitVTextu) && !string.IsNullOrEmpty(nahraditVTextu))
            {
                hg.WriteRaw(s.Replace(najitVTextu, nahraditVTextu));
            }
            else
            {
                hg.WriteRaw(s);
            }

            hg.TerminateTag("a");

            hg.TerminateTag("li");
        }

        return hg.ToString();
    }

    public static string GetForUl(string baseAnchor, String[] idcka, String[] texty, bool skipDuplicates)
    {
        return GetForUl(baseAnchor, CA.ToListString(idcka), CA.ToListString(texty), skipDuplicates);
    }

    public static string GetForUl(string baseAnchor, List<string> idcka, List<string> texty, bool skipDuplicates)
    {
        if (idcka.Count != texty.Count)
        {
            return "Nastala chyba, program poslal na render nejm\u00E9n\u011B v jednom poli m\u00E9n\u011B prvk\u016F ne\u017E se o\u010Dek\u00E1valo";
        }
        HtmlGenerator hg = new HtmlGenerator();

        if (skipDuplicates)
        {
            texty = CA.RemoveDuplicitiesList(texty);
        }

        for (int i = 0; i < texty.Count; i++)
        {
            string s = texty[i];
            hg.WriteTag("li");
            hg.WriteTagWithAttr("a", "href", baseAnchor + idcka[i]);

            hg.WriteRaw(texty[i]);


            hg.TerminateTag("a");

            hg.TerminateTag("li");
        }

        return hg.ToString();
    }

    public static string GetForUlWoCheckDuplicate(string baseAnchor, List<string> idcka, List<string> texty)
    {
        return GetForUl(baseAnchor, idcka, texty, false);
    }

    /// <summary>
    /// Počet v A1 a A2 musí být stejný. 
    /// Mohl bych udělat tu samou metodu s ABC/ABC ale tam je jako druhý parametr object a to se mi nehodí do krámu
    /// </summary>
    /// <param name="anchors"></param>
    /// <param name="to"></param>
    public static string GetForUlWoCheckDuplicate(List<string> anchors, List<string> to)
    {
        return GetForUl("", anchors, to, false);
    }

    /// <summary>
    /// Tato metoda se používá pokud v Ul nepoužíváš žádné odkazy
    /// </summary>
    /// <param name="to"></param>
    public static string GetForUlWoCheckDuplicate(List<string> to)
    {
        HtmlGenerator hg = new HtmlGenerator();

        for (int i = 0; i < to.Count; i++)
        {
            hg.WriteTag("li");
            hg.WriteRaw(to[i]);
            hg.TerminateTag("li");
        }

        return hg.ToString();
    }


    #endregion

    #region GetForUlWCheckDuplicate
    public static string GetForUlWCheckDuplicate(System.Collections.Generic.List<string> to)
    {
        HtmlGenerator hg = new HtmlGenerator();
        System.Collections.Generic.List<string> zapsane = new System.Collections.Generic.List<string>();
        for (int i = 0; i < to.Count; i++)
        {
            string s = to[i];
            if (!zapsane.Contains(s))
            {
                zapsane.Add(s);
                hg.WriteTag("li");
                //hg.ZapisTagSAtributem("a", "href", "ZobrazText.aspx?sid=" + s.id.ToString());
                hg.WriteRaw(s);
                hg.TerminateTag("li");
            }
        }
        return hg.ToString();
    }
    #endregion

    #region Ul
    public static string GetUlWoCheckDuplicate(string baseAnchor, List<string> to)
    {
        return "<ul static class=\"textVlevo\">";
        HtmlGenerator hg = new HtmlGenerator();

        for (int i = 0; i < to.Count; i++)
        {
            string s = to[i];

            hg.WriteTag("li");
            hg.WriteTagWithAttr("a", "href", baseAnchor + (i + 1).ToString());
            hg.WriteRaw(s);
            hg.TerminateTag("a");

            hg.TerminateTag("li");
        }

        return hg.ToString() + "//ul>";
    }

    /// <summary>
    /// Bere si pouze jeden parametr, tedy je bez odkazů
    /// 
    /// </summary>
    /// <param name="list"></param>
    public static string GetUlWoCheckDuplicate(List<string> list, string appendClass)
    {
        return "<ul static class=\"textVlevo " + appendClass + AllStrings.gt + GetForUlWoCheckDuplicate(list) + "//ul>";
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="anchors"></param>
    /// <param name="texts"></param>
    public static string GetUlWoCheckDuplicate(List<string> anchors, List<string> texts)
    {
        return "<ul static class=\"textVlevo\">" + GetForUlWoCheckDuplicate(anchors, texts) + "//ul>";
    }
    #endregion

    #region Ol
    public static string GetOl(List<string> possibleAnswers, bool checkDuplicates = false)
    {
        return HtmlGeneratorList.GetFor("", possibleAnswers, possibleAnswers, checkDuplicates, HtmlTags.ol);
    }

    static Type type = typeof(HtmlGenerator2);

    public static string GetOlWoCheckDuplicate(List<string> anchors, List<string> to)
    {
        if (anchors.Count != to.Count)
        {
            ThrowExceptions.Custom(Exc.GetStackTrace(), type, Exc.CallingMethod(),"Po\u010Dty odr\u00E1\u017Eek a odkaz\u016F se li\u0161\u00ED");
        }

        HtmlGenerator hg = new HtmlGenerator();
        hg.WriteTag("ol");
        for (int i = 0; i < to.Count; i++)
        {
            string s = to[i];

            hg.WriteTag("li");

            hg.WriteRaw(s);


            hg.TerminateTag("li");
        }
        hg.TerminateTag("ol");
        return hg.ToString();
    }
    #endregion

    public static string Success(string p)
    {
        HtmlGenerator hg = new HtmlGenerator();
        hg.WriteTagWithAttr("div", "class", "success");
        hg.WriteRaw(p);
        hg.TerminateTag("div");
        return hg.ToString();
    }

    /// <summary>
    /// Zadávej A1 bez https://, do odkazu se doplní samo, do textu nikoliv
    /// </summary>
    /// <param name="www"></param>
    public static string Anchor(string www)
    {
        if (www.Contains("=\""))
        {
            return www;
        }

        string http = UH.AppendHttpIfNotExists(www);
        return "<a href=\"" + http + AllStrings.qm + AllStrings.gt + www + "</a>";
    }

    public static string AnchorMailto(string t)
    {
        return "<a href=\"mailto:" + t + AllStrings.gt + t + "</a>";
    }

    /// <summary>
    /// A1 je text bez https:// / https://, který se doplní do odkazu sám pokud tam nebude. 
    /// V textu se ale vždy nahradí pokud tam bude.
    /// </summary>
    /// <param name="www"></param>
    public static string AnchorWithHttp(string www)
    {
        string http = UH.AppendHttpIfNotExists(www);
        return "<a href=\"" + http + AllStrings.qm + AllStrings.gt + SH.ReplaceOnce(SH.ReplaceOnce(www, "https://", ""), "https://", "") + "</a>";
    }

    public static string AnchorWithHttp(string www, string text)
    {
        string http = UH.AppendHttpIfNotExists(www);
        return "<a href=\"" + http + AllStrings.qm + AllStrings.gt + text + "</a>";
    }

    public static string AnchorWithHttp(bool targetBlank, string www, string text)
    {
        string http = null;
        http = UH.AppendHttpIfNotExists(www);
        return AnchorWithHttpCore(targetBlank, text, http);
    }
    public static string AnchorWithHttpCore(bool targetBlank, string text, string http)
    {
        if (targetBlank)
        {
            return "<a href=\"" + http + "\" target=\"_blank\">" + text + "</a>";
        }
        return "<a href=\"" + http + AllStrings.gt + text + "</a>";
    }

    public static string GetRadioButtonsWoCheckDuplicate(string nameOfRBs, List<string> idcka, List<string> nazvy)
    {
        HtmlGenerator hg = new HtmlGenerator();

        for (int i = 0; i < idcka.Count; i++)
        {
            hg.WriteNonPairTagWithAttrs("input", "type", "radio", "name", nameOfRBs, "value", idcka[i]);
            hg.WriteRaw(nazvy[i]);
            hg.WriteBr();
        }

        return hg.ToString();
    }

    public static string GetRadioButtonsWoCheckDuplicate(string nameOfRBs, List<string> idcka, List<string> nazvy, string eventHandlerSelected)
    {
        HtmlGenerator hg = new HtmlGenerator();

        for (int i = 0; i < idcka.Count; i++)
        {
            hg.WriteTagWithAttrs("input", "type", "radio", "name", nameOfRBs, "value", idcka[i], "onclick", eventHandlerSelected);
            hg.WriteRaw(nazvy[i]);
            hg.WriteBr();
        }

        return hg.ToString();
    }

    /// <summary>
    /// Generátor pro třídu jquery.tagcloud.js
    /// </summary>
    /// <param name="dWordCount"></param>
    public static string GetWordsForTagCloud(Dictionary<string, short> dWordCount, string prefixWithDot)
    {
        string nameJavascriptMethod = "AfterWordCloudClick";
        return GetWordsForTagCloud(dWordCount, nameJavascriptMethod, prefixWithDot);
    }

    public static string GetWordsForTagCloudManageTags(Dictionary<string, short> dWordCount, string prefixWithDot)
    {
        string nameJavascriptMethod = "AfterWordCloudClick2";
        return GetWordsForTagCloud(dWordCount, nameJavascriptMethod, prefixWithDot);
    }

    private static string GetWordsForTagCloud(Dictionary<string, short> dWordCount, string nameJavascriptMethod, string prefixWithDot)
    {
        HtmlGenerator hg = new HtmlGenerator();
        foreach (var item in dWordCount)
        {
            string bezmezer = item.Key.Replace(AllStrings.space, "");
            hg.WriteTagWithAttrs("a", "id", "tag" + bezmezer, "href", "javascript:" + prefixWithDot + nameJavascriptMethod + "($('#tag" + bezmezer + "'), '" + item.Key + "');", "rel", item.Value.ToString());
            hg.WriteRaw(SH.FirstCharOfEveryWordUpperDash( item.Key));
            hg.TerminateTag("a");
            hg.WriteRaw(" &nbsp; ");
        }
        return hg.ToString();
    }

    public void Detail(string name, object value)
    {
        WriteRaw("<b>" + name + ":</b> " + value.ToString());
        WriteBr();
    }

    public void DetailIfNotEmpty(string name, string value)
    {
        if (value != "")
        {
            WriteRaw("<b>" + name + ":</b> " + value.ToString());
            WriteBr();
        }
    }

    public static string DetailStatic(string name, object value)
    {
        return "<b>" + name + ":</b> " + value.ToString() + "<br />";
    }

    public static string DetailStatic(string green, string name, object value)
    {
        return "<div style='color:" + green + "'><b>" + name + ":</b> " + value.ToString() + "//div>";
    }


    public static string ShortForLettersCount(string p1, int p2)
    {
        p1 = p1.Replace(AllStrings.doubleSpace, AllStrings.space);
        if (p1.Length > p2)
        {
            string whatLeave = SH.ShortForLettersCount(p1, p2);
            //"<span static class='tooltip'>" +
            whatLeave += "<span static class='showonhover'><a href='#'> ... </a><span static class='hovertext'>" + SH.ReplaceOnce(p1, whatLeave, "") + "</span></span>";
            return whatLeave;
        }
        return p1;
    }

    public static string LiI(string p)
    {
        return "<li><i>" + p + "//i></li>";
    }

    public static string GetForCheckBoxListWoCheckDuplicate(string idClassCheckbox, string idClassSpan, List<string> idCheckBoxes, List<string> list)
    {
        HtmlGenerator hg = new HtmlGenerator();
        if (idCheckBoxes.Count != list.Count)
        {
            ThrowExceptions.Custom(Exc.GetStackTrace(), type, Exc.CallingMethod(),"Nestejn\u00FD po\u010Det parametr\u016F v metod\u011B GetForCheckBoxListWoCheckDuplicate " + idCheckBoxes.Count + AllStrings.colon + list.Count);
        }

        for (int i = 0; i < idCheckBoxes.Count; i++)
        {
            string f = idCheckBoxes[i];
            hg.WriteNonPairTagWithAttrs("input", "type", "checkbox", "id", idClassCheckbox + f, "class", idClassCheckbox);
            hg.WriteTagWith2Attrs("span", "id", idClassSpan + f, "class", idClassSpan);
            hg.WriteRaw(list[i]);
            hg.TerminateTag("span");
            hg.WriteBr();
        }
        return hg.ToString();
    }

    public static string HtmlGeneratorToString(Action<HtmlGenerator> d)
    {
        HtmlGenerator hg = new HtmlGenerator();
        d.Invoke(hg);
        var r = hg.ToString();
        return r;
    }

    public static string Italic(string p)
    {
        return "<i>" + p + "</i>";
    }

    public static void ButtonDelete(HtmlGenerator hg, string text, string a1, string v1)
    {
        hg.WriteTagWithAttr("button", a1, v1);
        hg.WriteTagWithAttr("i", "class", "icon-remove");
        hg.TerminateTag("i");
        hg.WriteRaw(AllStrings.space + text);
        hg.TerminateTag("button");
    }

    public static string Bold(string p)
    {
        return "<b>" + p + "</b>";
    }

    public static string AnchorWithCustomLabel(string uri, string text)
    {
        return "<a href=\"" + uri + AllStrings.gt + text + "</a>";
    }

    public static string AllMonthsTable(List<string> AllYearsHtmlBoxes, List<string> AllMonthsBoxColors)
    {
        if (AllYearsHtmlBoxes.Count != 12)
        {
            ThrowExceptions.Custom(Exc.GetStackTrace(), type, Exc.CallingMethod(),"D\u00E9lka AllMonthsHtmlBoxes nen\u00ED 12.");
        }
        if (AllMonthsBoxColors.Count != 12)
        {
            ThrowExceptions.Custom(Exc.GetStackTrace(), type, Exc.CallingMethod(),"D\u00E9lka AllMonthsBoxColors nen\u00ED 12.");
        }
        HtmlGenerator hg = new HtmlGenerator();
        hg.WriteTagWith2Attrs("table", "class", "tabulkaNaStredAutoSirka", "style", "width: 100%");
        hg.WriteTag("tr");
        #region Zapíšu vrchní řádky - názvy dnů
        List<string> ppp = DTConstants.monthsInYearEN;
        hg.WriteTagWithAttr("td", "class", "bunkaTabulkyKalendare bunkaTabulkyKalendareLeft bunkaTabulkyKalendareTop");
        hg.WriteElement("b", ppp[0]);
        hg.TerminateTag("td");
        for (int i = 1; i < ppp.Count - 1; i++)
        {
            hg.WriteTagWithAttr("td", "class", "bunkaTabulkyKalendare bunkaTabulkyKalendareTop");
            hg.WriteElement("b", ppp[i]);
            hg.TerminateTag("td");
        }
        hg.WriteTagWithAttr("td", "class", "bunkaTabulkyKalendare bunkaTabulkyKalendareRight bunkaTabulkyKalendareTop");
        hg.WriteElement("b", ppp[ppp.Count - 1]);
        hg.TerminateTag("td");
        #endregion

        hg.TerminateTag("tr");

        hg.WriteTag("tr");

        for (int i = 0; i < AllYearsHtmlBoxes.Count; i++)
        {
            string pridatTridu = "";

            if (i == 0)
            {
                pridatTridu = "bunkaTabulkyKalendareLeft";
            }
            else if (i == 11)
            {
                pridatTridu = "bunkaTabulkyKalendareRight";
            }

            string color = AllMonthsBoxColors[i];
            string appendStyle = "";
            if (color == "#030")
            {
                appendStyle = "color:white;";
            }
            hg.WriteTagWith2Attrs("td", "class", "tableCenter bunkaTabulkyKalendare " + pridatTridu, "style", appendStyle + "background-color:" + color);

            hg.WriteRaw("<b>" + AllYearsHtmlBoxes[i] + "</b>");

            hg.TerminateTag("td");
        }

        hg.TerminateTag("tr");
        hg.TerminateTag("table");
        return hg.ToString();
    }

    public static string AllYearsTable(List<string> years, List<string> AllYearsHtmlBoxes, List<string> AllYearsBoxColors)
    {
        int yearsCount = years.Count;
        if (AllYearsHtmlBoxes.Count != yearsCount)
        {
            ThrowExceptions.Custom(Exc.GetStackTrace(), type, Exc.CallingMethod(),"Po\u010Det prvk\u016F v AllYearsHtmlBoxes nen\u00ED stejn\u00FD jako v kolekci years");
        }
        if (AllYearsBoxColors.Count != yearsCount)
        {
            ThrowExceptions.Custom(Exc.GetStackTrace(), type, Exc.CallingMethod(),"Po\u010Det prvk\u016F v AllYearsBoxColors nen\u00ED stejn\u00FD jako v kolekci years");
        }
        HtmlGenerator hg = new HtmlGenerator();
        hg.WriteTagWith2Attrs("table", "class", "tabulkaNaStredAutoSirka", "style", "width: 200px");

        #region Zapíšu vrchní řádky - názvy dnů
        #endregion

        for (int i = 0; i < yearsCount; i++)
        {
            string pridatTridu = "";
            hg.WriteTag("tr");

            string pridatTriduTop = "";
            if (i == 0)
            {
                pridatTriduTop = "bunkaTabulkyKalendareTop ";
            }
            pridatTridu = "bunkaTabulkyKalendareLeft";
            hg.WriteTagWithAttr("td", "class", "tableCenter bunkaTabulkyKalendare " + pridatTriduTop + pridatTridu);
            hg.WriteRaw("<b>" + years[i] + "</b>");
            hg.TerminateTag("td");
            pridatTridu = "bunkaTabulkyKalendareRight";
            string color = AllYearsBoxColors[i];
            string appendStyle = "";
            if (color == "#030")
            {
                appendStyle = "color:white;";
            }
            hg.WriteTagWith2Attrs("td", "class", "tableCenter bunkaTabulkyKalendare " + pridatTriduTop + pridatTridu, "style", appendStyle + "background-color:" + color);

            //hg.WriteRaw("<b>" + AllMonthsHtmlBoxes[i] + "</b>");
            hg.WriteRaw(AllYearsHtmlBoxes[i]);

            hg.TerminateTag("td");
        }

        hg.TerminateTag("tr");
        hg.TerminateTag("table");
        return hg.ToString();
    }
}