using System;
using System.Collections.Generic;
using System.Linq;

using System.Reflection;

/// <summary>
/// Summary description for SunamoGridView
/// </summary>
public class SunamoGridView //: System.Web.UI.HtmlControls.HtmlInputFile
{
    string messageIfEmpty = "";
    HtmlGenerator hg = new HtmlGenerator();
    /// <summary>
    /// Musí to být string a nic jiného
    /// </summary>
    private List<String[]> _dataBinding = new List<string[]>();
    List<SunamoGridViewColumn> columns = new List<SunamoGridViewColumn>();
    List<SunamoGridViewRow> rows = new List<SunamoGridViewRow>();
    /// <summary>
    /// Zadejte například 30px
    /// </summary>
    public string minimalniSirkaRadku = "auto";
    /// <summary>
    /// Do A1 lze vložit SunamoStrings.messageIfEmpty
    /// </summary>
    /// <param name="messageIfEmpty"></param>
    /// <param name="dataBindings"></param>
    public SunamoGridView(string messageIfEmpty, params string[][] dataBindings) 
    {
        // dataBindings[sloupec][všechny_řádky_v_daném_sloupci]
        _dataBinding.AddRange(dataBindings);
        this.messageIfEmpty = messageIfEmpty;
    }

    #region Všechny metody zde se musí nakonec dobrat k zavolání AddColumn
    public void AddImageColumn(string nazevSloupce, InputImageButton edit)
    {
        AddColumn(nazevSloupce, edit);
    }

    public void AddSpanColumn(string nazevSloupce, string hodnotaSloupce)
    {
        Span columnPoradi1 = new Span();
        columnPoradi1.innerHtml = hodnotaSloupce;
        AddSpanColumn(nazevSloupce, columnPoradi1);
    }

    public void AddSpanColumn(string nazevSloupce, int hodnotaSloupce)
    {
        Span columnPoradi1 = new Span();
        columnPoradi1.innerHtml = "{" + hodnotaSloupce.ToString() + "}";
        AddSpanColumn(nazevSloupce, columnPoradi1);
    }

    public void AddInputCheckboxColumn(string header, string id, string text)
    {
        InputCheckBox op = new InputCheckBox();
        op.ID = id;
        op.value = text;
        AddInputCheckboxColumn(header, op);
    }

    /// <summary>
    /// Sloupce se zde nebudou přidávat automaticky, protože já musím určit ve sloupcích formáty
    /// </summary>
    /// <param name="o"></param>
    private void AddColumns(object o)
    {
        // Pouze bez parametrů mi získá všechny veřejné vlastnosti
        foreach (PropertyInfo item in o.GetType().GetProperties())
        {
            string type = item.GetGetMethod().ReturnType.ToString();
            if (type == "System.Boolean")
            {

            }
            else if (type == "System.Drawing.Image")
            {

            }
            else if (type == "System.Uri")
            {

            }

        }
    }

    private void AddColumns(params SunamoGridViewColumn[] cols)
    {

    }

    public void AddInputCheckboxColumn(string nazevSloupce, InputCheckBox c)
    {
        AddColumn(nazevSloupce, c);
    }

    public void AddImgColumn(string nazevSloupce, Img c)
    {
        AddColumn(nazevSloupce, c);
    }

    public void AddInputButtonColumn(string nazevSloupce, InputButton c)
    {
        AddColumn(nazevSloupce, c);
    }

    public void AddInputFileColumn(string nazevSloupce, InputFile c)
    {
        AddColumn(nazevSloupce, c);
    }

    public void AddInputTextColumn(string nazevSloupce, InputText c)
    {
        AddColumn(nazevSloupce, c);
    }

    public void AddSpanColumn(string nazevSloupce, Span c)
    {
        AddColumn(nazevSloupce, c);
    }

    public void AddCustomControlColumn(string nazevSloupce, CustomControl columnCacheName)
    {
        AddColumn(nazevSloupce, columnCacheName);
    }

    public void AddAnchorColumn(string nazevSloupce, Anchor columnCacheName)
    {
        AddColumn(nazevSloupce, columnCacheName);
    }

    private void AddColumn(string nazevSloupce, BaseControl c)
    {
        SunamoGridViewColumn col = new SunamoGridViewColumn();
        col.bt = c;
        col.nazevSloupce = nazevSloupce;
        columns.Add(col);
    } 
    #endregion

    //
    public string Generate(string idTable)
    {
        if (_dataBinding[0].Length == 0)
        {
            return "<p>" + messageIfEmpty + "</p>";
        }

        HtmlGenerator hg = new HtmlGenerator();
        hg.WriteTagWithAttrs("table", "border", "1", "class", "naStred", "id", idTable);

        // radek[sloupec][poradi_promennych_v_sloupci]
        List<List<int>> sloupceKVyplneni = new List<List<int>>();
        // Projdu si první řádek každého sloupce, abych určil, které sloupce v něm budu muset vyplnit
        hg.WriteTagWithAttr("thead", "style", "color: white;background-color: black;");
        // V hlavičce si zjistím všechny proměnné, které jsou v daném sloupci
        for (int i = 0; i < columns.Count; i++)
        {
            hg.WriteElement("th", columns[i].nazevSloupce);
            // Projdu všechny sloupce
            BaseControl d = columns[i].bt;
            d._dataBinding = _dataBinding;
            
            // A až pak první hodnotu daného sloupce
            string d2 = d.Render(0, _dataBinding);
            // Původně to bylo na 10, nestačilo, tak jsem to zvýšil 20, ani to nepomohlo tak 30
            sloupceKVyplneni.Add(SH.GetVariablesInString(d2));
        }
        hg.TerminateTag("thead");

        hg.WriteTag("tbody");
        // Procházím všechny řádky 1. sloupce
        for (int i = 0; i < _dataBinding[0].Length; i++)
        {
            bool alternate = false;
            if (i % 2 == 1)
            {
                alternate = true;
            }
            SunamoGridViewRow row = new SunamoGridViewRow(columns, null, i, sloupceKVyplneni, alternate, minimalniSirkaRadku);

            hg.WriteRaw(row.ToString(i, _dataBinding));
        }
        hg.TerminateTag("tbody");
        hg.TerminateTag("table");
        return hg.ToString() ;
    }
}
