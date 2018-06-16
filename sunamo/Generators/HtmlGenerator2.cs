using sunamo;
using System;
using System.Collections.Generic;
using System.Text;

/// <summary>
/// Summary description for HtmlGenerator2
/// </summary>
public class HtmlGenerator2 : HtmlGenerator
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

    public static string Calendar(List<string> htmlBoxesEveryDay, List<string> colors, int year, int mesic)
    {
        HtmlGenerator hg = new HtmlGenerator();
        hg.WriteTagWith2Attrs("table", "class", "tabulkaNaStredAutoSirka", "style", "width: 600px");
        hg.WriteTag("tr");
        #region Zapíšu vrchní řádky - názvy dnů
        string[] ppp = DTConstants.daysInWeekCS;
        hg.WriteTagWithAttr("td", "class", "bunkaTabulkyKalendare bunkaTabulkyKalendareLeft bunkaTabulkyKalendareTop");
        hg.WriteElement("b", ppp[0]);
        hg.TerminateTag("td");
        for (int i = 1; i < ppp.Length - 1; i++)
        {
            hg.WriteTagWithAttr("td", "class", "bunkaTabulkyKalendare bunkaTabulkyKalendareTop");
            hg.WriteElement("b", ppp[i]);
            hg.TerminateTag("td");
        }
        hg.WriteTagWithAttr("td", "class", "bunkaTabulkyKalendare bunkaTabulkyKalendareRight bunkaTabulkyKalendareTop");
        hg.WriteElement("b", ppp[ppp.Length - 1]);
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
            string color = colors[i-1];
            string appendStyle = "";
            if (color == "#030")
            {
                appendStyle = "color:white;";
            }
            string datum = i + "." + mesic + ".";
            hg.WriteTagWith2Attrs("td", "class", "tableCenter bunkaTabulkyKalendare " + pridatTridu, "style", appendStyle + "background-color:" + colors[i-1]);
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

    public static string GetSelect(string id, object def, object[] list)
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

    /// <summary>
    /// Jedná se o divy pod sebou, nikoliv o ol/ul>li 
    /// </summary>
    /// <param name="hg"></param>
    /// <param name="odkazyPhoto"></param>
    /// <param name="odkazyText"></param>
    /// <param name="innerHtmlText"></param>
    /// <param name="srcPhoto"></param>
    /// <returns></returns>
    public static string TopListWithImages(HtmlGenerator hg, int widthImage, int heightImage, string initialImageUri, List<string> odkazyPhoto, List<string> odkazyText, List<string> innerHtmlText, List<string> srcPhoto, string nameJsArray)
    {
        int count = odkazyPhoto.Count;
        if (count == 0)
        {
            //throw new Exception("Metoda HtmlGenerator2.TopListWithImages - odkazyPhoto nemá žádný prvek");
            return "";
        }
        if (count != odkazyText.Count)
        {
            throw new Exception("Metoda HtmlGenerator2.TopListWithImages - odkazyPhoto se nerovná počtem odkazyText");
        }
        if (count != innerHtmlText.Count)
        {
            throw new Exception("Metoda HtmlGenerator2.TopListWithImages - odkazyPhoto se nerovná počtem innerHtmlText");
        }
        if (count != srcPhoto.Count)
        {
            throw new Exception("Metoda HtmlGenerator2.TopListWithImages - odkazyPhoto se nerovná počtem srcPhoto");
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
                hg.WriteNonPairTagWithAttrs("img", "style", "margin-left: auto; margin-right: auto; vertical-align-middle; width: " + widthImage + "px;height:" + heightImage + "px", "id", nameJsArray + srcPhoto[i], "class", "alternatingImage", "src", initialImageUri);
            }
            else
            {
                hg.WriteNonPairTagWithAttrs("img", "src", srcPhoto[i]);
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
    /// <returns></returns>
    public static string TopListWithImages(HtmlGenerator hg, int widthImage, int heightImage, string initialImageUri, List<string> odkazyPhoto, List<string> odkazyText, List<string> innerHtmlText, List<string> srcPhoto, List<string> idBadges, string nameJsArray)
    {
        int count = odkazyPhoto.Count;
        if (count == 0)
        {
            //throw new Exception("Metoda HtmlGenerator2.TopListWithImages - odkazyPhoto nemá žádný prvek");
            return "";
        }
        if (count != odkazyText.Count)
        {
            throw new Exception("Metoda HtmlGenerator2.TopListWithImages - odkazyPhoto se nerovná počtem odkazyText");
        }
        if (count != innerHtmlText.Count)
        {
            throw new Exception("Metoda HtmlGenerator2.TopListWithImages - odkazyPhoto se nerovná počtem innerHtmlText");
        }
        if (count != srcPhoto.Count)
        {
            throw new Exception("Metoda HtmlGenerator2.TopListWithImages - odkazyPhoto se nerovná počtem srcPhoto");
        }
        if (count != idBadges.Count)
        {
            throw new Exception("Metoda HtmlGenerator2.TopListWithImages - odkazyPhoto "+count+" se nerovná počtem idBadges " + idBadges.Count);
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
                hg.WriteNonPairTagWithAttrs("img", "style", "margin-left: auto; margin-right: auto; vertical-align-middle; width: " + widthImage + "px;height:" + heightImage + "px", "id", nameJsArray + srcPhoto[i], "class", "alternatingImage", "src", initialImageUri);
            }
            else
            {
                hg.WriteNonPairTagWithAttrs("img", "src", srcPhoto[i]);
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
    /// Do A1 doplň třeba EditMister.aspx?mid= - co za toto si automaticky doplní a A2 jsou texty do inner textu a
    /// Nehodí se tedy proto vždy, například, když máš přehozené IDčka v DB
    /// </summary>
    /// <param name="baseAnchor"></param>
    /// <param name="to"></param>
    /// <returns></returns>
    public static string GetForUlWoCheckDuplicate(string baseAnchor, string[] to)
    {
        HtmlGenerator hg = new HtmlGenerator();

        for (int i = 0; i < to.Length; i++)
        {
            string s = to[i];

            hg.WriteTag("li");
            hg.WriteTagWithAttr("a", "href", baseAnchor + (i + 1).ToString());
            hg.WriteRaw(s);
            hg.TerminateTag("a");

            hg.TerminateTag("li");
        }

        return hg.ToString();
    }

    public static string GetForUlWoCheckDuplicate(string baseAnchor, List<string> to, string najitVTextu, string nahraditVTextu, string pripona = "")
    {
        HtmlGenerator hg = new HtmlGenerator();

        for (int i = 0; i < to.Count; i++)
        {
            string s = to[i];

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

    public static string GetForUlWoCheckDuplicate(string baseAnchor, string[] idcka, string[] texty)
    {
        if (idcka.Length != texty.Length)
        {
            return "Nastala chyba, program poslal na render nejméně v jednom poli méně prvků než se očekávalo";
        }
        HtmlGenerator hg = new HtmlGenerator();

        for (int i = 0; i < texty.Length; i++)
        {
            hg.WriteTag("li");
            hg.WriteTagWithAttr("a", "href", baseAnchor + idcka[i]);
            hg.WriteRaw(texty[i]);
            hg.TerminateTag("a");

            hg.TerminateTag("li");
        }

        return hg.ToString();
    }

    public static string GetUlWoCheckDuplicate(string baseAnchor, string[] to)
    {
        return "<ul class=\"textVlevo\">";
        HtmlGenerator hg = new HtmlGenerator();

        for (int i = 0; i < to.Length; i++)
        {
            string s = to[i];

            hg.WriteTag("li");
            hg.WriteTagWithAttr("a", "href", baseAnchor + (i + 1).ToString());
            hg.WriteRaw(s);
            hg.TerminateTag("a");

            hg.TerminateTag("li");
        }

        return hg.ToString() + "</ul>";
    }

    /// <summary>
    /// Počet v A1 a A2 musí být stejný. 
    /// Mohl bych udělat tu samou metodu s ABC/AB[] ale tam je jako druhý parametr object a to se mi nehodí do krámu
    /// </summary>
    /// <param name="anchors"></param>
    /// <param name="to"></param>
    /// <returns></returns>
    public static string GetForUlWoCheckDuplicate(string[] anchors, string[] to)
    {
        if (anchors.Length != to.Length)
        {
            throw new Exception("Počty odrážek a odkazů se liší");
        }

        HtmlGenerator hg = new HtmlGenerator();

        for (int i = 0; i < to.Length; i++)
        {
            string s = to[i];

            hg.WriteTag("li");
            hg.WriteTagWithAttr("a", "href", anchors[i]);
            hg.WriteRaw(s);
            hg.TerminateTag("a");

            hg.TerminateTag("li");
        }

        return hg.ToString();
    }

    public static string GetOlWoCheckDuplicate(List<string> anchors, List<string> to)
    {
        if (anchors.Count != to.Count)
        {
            throw new Exception("Počty odrážek a odkazů se liší");
        }

        HtmlGenerator hg = new HtmlGenerator();
        hg.WriteTag("ol");
        for (int i = 0; i < to.Count; i++)
        {
            string s = to[i];

            hg.WriteTag("li");
            hg.WriteTagWithAttr("a", "href", anchors[i]);
            hg.WriteRaw(s);
            hg.TerminateTag("a");

            hg.TerminateTag("li");
        }
        hg.TerminateTag("ol");
        return hg.ToString();
    }

    /// <summary>
    /// Tato metoda se používá pokud v Ul nepoužíváš žádné odkazy
    /// </summary>
    /// <param name="to"></param>
    /// <returns></returns>
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

    /// <summary>
    /// Tato metoda se používá pokud v Ul nepoužíváš žádné odkazy
    /// </summary>
    /// <param name="to"></param>
    /// <returns></returns>
    public static string GetForUlWoCheckDuplicate(string[] to)
    {
        HtmlGenerator hg = new HtmlGenerator();

        for (int i = 0; i < to.Length; i++)
        {
            hg.WriteTag("li");
            hg.WriteRaw(to[i]);
            hg.TerminateTag("li");
        }

        return hg.ToString();
    }

    /// <summary>
    /// Bere si pouze jeden parametr, tedy je bez odkazů
    /// 
    /// </summary>
    /// <param name="list"></param>
    /// <returns></returns>
    public static string GetUlWoCheckDuplicate(string[] list, string appendClass)
    {
        return "<ul class=\"textVlevo "+ appendClass +"\">" + GetForUlWoCheckDuplicate(list) + "</ul>";
    } 

    

    /// <summary>
    /// 
    /// </summary>
    /// <param name="anchors"></param>
    /// <param name="texts"></param>
    /// <returns></returns>
    public static string GetUlWoCheckDuplicate(string[] anchors, string[] texts)
    {
        return "<ul class=\"textVlevo\">" + GetForUlWoCheckDuplicate(anchors, texts) + "</ul>";
    }

    

    public static string Success(string p)
    {
        HtmlGenerator hg = new HtmlGenerator();
        hg.WriteTagWithAttr("div", "class", "success");
        hg.WriteRaw(p);
        hg.TerminateTag("div");
        return hg.ToString();
    }






    /// <summary>
    /// Zadávej A1 bez http://, do odkazu se doplní samo, do textu nikoliv
    /// </summary>
    /// <param name="www"></param>
    /// <returns></returns>
    public static string Anchor(string www)
    {
        string http = UH.AppendHttpIfNotExists(www);
        return "<a href=\"" + http + "\">" + www + "</a>";
    }

    public static string AnchorMailto(string t)
    {
        return "<a href=\"mailto:" + t + "\">" + t + "</a>";
    }

    /// <summary>
    /// A1 je text bez http:// / https://, který se doplní do odkazu sám pokud tam nebude. 
    /// V textu se ale vždy nahradí pokud tam bude.
    /// </summary>
    /// <param name="www"></param>
    /// <returns></returns>
    public static string AnchorWithHttp(string www)
    {
        string http = UH.AppendHttpIfNotExists(www);
        return "<a href=\"" + http + "\">" + SH.ReplaceOnce(SH.ReplaceOnce(www, "http://", ""), "https://", "") + "</a>";
    }

    public static string AnchorWithHttp(string www, string text)
    {
        string http = UH.AppendHttpIfNotExists(www);
        return "<a href=\"" + http + "\">" + text + "</a>";
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
        return "<a href=\"" + http + "\">" + text + "</a>";
    }

    public static string GetRadioButtonsWoCheckDuplicate(string nameOfRBs, List<string> idcka, List<string> nazvy)
    {
        HtmlGenerator hg = new HtmlGenerator();

        for (int i = 0; i < idcka.Count; i++)
        {
            hg.WriteTagWithAttrs("input", "type", "radio", "name", nameOfRBs, "value", idcka[i]);
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
    /// <returns></returns>
    public static string GetWordsForTagCloud(Dictionary<string, short> dWordCount)
    {
        string nameJavascriptMethod = "AfterWordCloudClick";
        return GetWordsForTagCloud(dWordCount, nameJavascriptMethod);
    }

    public static string GetWordsForTagCloudManageTags(Dictionary<string, short> dWordCount)
    {
        string nameJavascriptMethod = "AfterWordCloudClick2";
        return GetWordsForTagCloud(dWordCount, nameJavascriptMethod);
    }

    private static string GetWordsForTagCloud(Dictionary<string, short> dWordCount, string nameJavascriptMethod)
    {
        HtmlGenerator hg = new HtmlGenerator();
        foreach (var item in dWordCount)
        {
            string bezmezer = item.Key.Replace(" ", "");
            hg.WriteTagWithAttrs("a", "id", "tag" + bezmezer, "href", "javascript:" + nameJavascriptMethod + "($('#tag" + bezmezer + "'), '" + item.Key + "');", "rel", item.Value.ToString());
            hg.WriteRaw(item.Key);
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
        return "<div style='color:" + green + "'><b>" + name + ":</b> " + value.ToString() + "</div>";
    }

    
    public static string ShortForLettersCount(string p1, int p2)
    {
        p1 = p1.Replace("  ", " ");
        if (p1.Length > p2)
        {
            string whatLeave = SH.ShortForLettersCount(p1, p2);
            //"<span class='tooltip'>" +
            whatLeave +=  "<span class='showonhover'><a href='#'> ... </a><span class='hovertext'>" + SH.ReplaceOnce(p1, whatLeave, "") + "</span></span>";
            return whatLeave;
        }
        return p1;
    }

    public static string LiI(string p)
    {
        return "<li><i>" + p + "</i></li>";
    }

    public static string GetForCheckBoxListWoCheckDuplicate(string idClassCheckbox, string idClassSpan, List<string> idCheckBoxes, List<string> list)
    {
        HtmlGenerator hg = new HtmlGenerator();
        if (idCheckBoxes.Count != list.Count)
        {
            throw new Exception("Nestejný počet parametrů v metodě GetForCheckBoxListWoCheckDuplicate "+ idCheckBoxes.Count + ":" + list.Count);
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



    public static string Italic(string p)
    {
        return "<i>" + p + "</i>";
    }

    public static void ButtonDelete(HtmlGenerator hg, string text, string a1, string v1)
    {
        hg.WriteTagWithAttr("button", a1, v1);
        hg.WriteTagWithAttr("i", "class", "icon-remove");
        hg.TerminateTag("i");
        hg.WriteRaw(" " + text);
        hg.TerminateTag("button");
    }

    public static string Bold(string p)
    {
        return "<b>" + p + "</b>";
    }

    public static string AnchorWithCustomLabel(string uri, string text)
    {
        return "<a href=\"" + uri + "\">" + text + "</a>";
    }

    public static string AllMonthsTable(List<string> AllYearsHtmlBoxes, List<string> AllMonthsBoxColors)
    {
        if (AllYearsHtmlBoxes.Count != 12)
        {
            throw new Exception("Délka AllMonthsHtmlBoxes není 12.");
        }
        if (AllMonthsBoxColors.Count != 12)
        {
            throw new Exception("Délka AllMonthsBoxColors není 12.");
        }
        HtmlGenerator hg = new HtmlGenerator();
        hg.WriteTagWith2Attrs("table", "class", "tabulkaNaStredAutoSirka", "style", "width: 100%");
        hg.WriteTag("tr");
        #region Zapíšu vrchní řádky - názvy dnů
        string[] ppp = DTConstants.monthsInYearCZ;
        hg.WriteTagWithAttr("td", "class", "bunkaTabulkyKalendare bunkaTabulkyKalendareLeft bunkaTabulkyKalendareTop");
        hg.WriteElement("b", ppp[0]);
        hg.TerminateTag("td");
        for (int i = 1; i < ppp.Length - 1; i++)
        {
            hg.WriteTagWithAttr("td", "class", "bunkaTabulkyKalendare bunkaTabulkyKalendareTop");
            hg.WriteElement("b", ppp[i]);
            hg.TerminateTag("td");
        }
        hg.WriteTagWithAttr("td", "class", "bunkaTabulkyKalendare bunkaTabulkyKalendareRight bunkaTabulkyKalendareTop");
        hg.WriteElement("b", ppp[ppp.Length - 1]);
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
            hg.WriteTagWith2Attrs("td", "class", "tableCenter bunkaTabulkyKalendare " + pridatTridu, "style", appendStyle + "background-color:" + color );
            
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
            throw new Exception("Počet prvků v AllYearsHtmlBoxes není stejný jako v kolekci years");
        }
        if (AllYearsBoxColors.Count != yearsCount)
        {
            throw new Exception("Počet prvků v AllYearsBoxColors není stejný jako v kolekci years");
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
