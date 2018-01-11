using System;
using System.Windows.Forms;
/// <summary>
/// Pak je t�eba p�ekr�t jen metody:
/// 
/// OnCellBeginEdit - pokud edituji sloupec kter� nem��u zastavit. Pokud sloupec editovat m��u, ulo�it p�vodn� hodnotu
/// OnDataError - Nastavit p�vodn� hodnotu. Mo�n� to je trochu slo�it�j��, kdy�tak se koukni do ParsovacCammin
/// OnCurrentCellDirtyStateChanged - pouze zavol�m this.CommitEdit(DataGridViewDataErrorContexts.Commit); Tuto metodu je bezpodm�nen�n� nutn� zavolat, jinak se minim�ln� posledn� editovan� hodnota neulo��!!
/// OnCellValueChanged - pokud se nepoda�� vyparsovat nov� hodnota, ulo��m starou
/// </summary>
public class BaseDataGridView : DataGridView
{
    /// <summary>
    /// Je tu proto �e pozd�ji bych cht�l to automatizovat ud�lostmi kter� jsou ve komentu t��dy
    /// </summary>
    int poradiSloupcu = 0;

    public BaseDataGridView() : base()
    {
        
    }

    public virtual void CreateColumns()
    {
        this.AutoGenerateColumns = false;
    }

    protected DataGridViewColumn NewComboBoxColumn(string p)
    {
        DataGridViewComboBoxColumn cNazev = new DataGridViewComboBoxColumn();
        cNazev.DataPropertyName = p;
        
        cNazev.HeaderText = p;
        poradiSloupcu++;
        return cNazev;
    }

    protected DataGridViewColumn NewCheckedBoxColumn(string p)
    {
        DataGridViewCheckBoxColumn cNazev = new DataGridViewCheckBoxColumn();
        cNazev.DataPropertyName = p;
        cNazev.HeaderText = p;
        cNazev.ValueType = typeof(bool);
        poradiSloupcu++;
        return cNazev;
    }

    protected DataGridViewComboBoxColumn NewComboBoxColumn(string p, Type enumType)
    {

        DataGridViewComboBoxColumn cNazev = new DataGridViewComboBoxColumn();

        cNazev.Items.AddRange(Enum.GetNames(enumType));
        cNazev.Name = p;
        cNazev.DisplayMember = p;
        cNazev.HeaderText = p;


        poradiSloupcu++;
        return cNazev;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="p"></param>
    /// <param name="allowedType"></param>
    protected DataGridViewTextBoxColumn NewTextBoxColumn(string p, Type allowedType)
    {
        DataGridViewTextBoxColumn cNazev = new DataGridViewTextBoxColumn();
        cNazev.DataPropertyName = p;
        cNazev.HeaderText = p;
        cNazev.ValueType = allowedType;
        
        poradiSloupcu++;
        return cNazev;
    }

    protected DataGridViewColumn NewTextBoxColumn(string p)
    {
        DataGridViewTextBoxColumn cNazev = new DataGridViewTextBoxColumn();
        cNazev.DataPropertyName = p;
        cNazev.HeaderText = p;
        poradiSloupcu++;
        return cNazev;
    }
}
