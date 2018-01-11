using System;
using System.Collections.Generic;
using System.Linq;


/// <summary>
/// Summary description for SunamoGridViewRow
/// </summary>
public class SunamoGridViewRow
{
    /// <summary>
    /// Obsahuje sloupce a indexy, které musím v nich nahradit
    /// radek[sloupec][obsažené indexy proměnných v sloupci]
    /// </summary>
    List<List<int>> sloupceKVyplneni = null;
    /// <summary>
    /// Všechny sloupce v objektech SunamoGridViewColumn, který obsahuje BaseControl a string pro hlavičku
    /// </summary>
    private List<SunamoGridViewColumn> columns;
    /// <summary>
    /// Všechny sloupce v tomto řádku - vč. proměnných
    /// </summary>
    private List<string> v;
    /// <summary>
    /// Aktuální řádek v SGV bez hlavičky, počítá se od nuly
    /// Zakomentováno, teď předávám aktuální řádek přímo do metody this.ToString() a navíc se tato proměnná nikde nevyužívala
    /// </summary>
    //int actualRow = 0;
    /// <summary>
    /// Zda mám pořadí řádku vyplnit šedou barvou
    /// </summary>
    bool alternateBG = false;
    string minimalniSirkaRadku = "";

    /// <summary>
    /// A1 jsou sloupce, které obsahují formáty. 
    /// A2 jsou samotné formáty - texty které vložit do jednotlivých sloupců.
    /// A3 je aktuální řádek - používá se při DataBindingu
    /// A4 je radek[sloupec][obsažené indexy proměnných v sloupci]
    /// A5 je zda se má řádek podbarvit šedou barvou
    /// </summary>
    /// <param name="columns"></param>
    /// <param name="v"></param>
    /// <param name="sloupceKVyplneni"></param>
    public SunamoGridViewRow(List<SunamoGridViewColumn> columns, List<string> v, int actualRow, List<List<int>> sloupceKVyplneni, bool alternateBG, string minimalniSirkaRadku)
    {
        // TODO: Complete member initialization
        this.columns = columns;
        this.v = v;
        //this.actualRow = actualRow;
        this.sloupceKVyplneni = sloupceKVyplneni;
        this.alternateBG = alternateBG;
        this.minimalniSirkaRadku = minimalniSirkaRadku;
    }

    /// <summary>
    /// Metoda která vygeneruje celý řádek.
    /// </summary>
    /// <returns></returns>
    public string ToString(int actualRow, List<String[]> _dataBinding)
    {
        HtmlGenerator hg = new HtmlGenerator();
        // 
        if (alternateBG)
        {
            hg.WriteTagWithAttrs("tr", "class", "sedePozadi", "id", "tr" + actualRow,"style", "height: " + minimalniSirkaRadku + ";");
        }
        else
        {
            hg.WriteTagWith2Attrs("tr", "id", "tr" + actualRow, "style", "height: " + minimalniSirkaRadku + ";");
        }
        // 
        for (int i = 0; i < columns.Count; i++)
        {
            hg.WriteTag("td");
            string    kNahrazeni = columns[i].bt.Render( actualRow, _dataBinding);
            hg.WriteRaw(kNahrazeni);
            
            hg.TerminateTag("td");
        }
        hg.TerminateTag("tr");
        return hg.ToString();
    }
}
