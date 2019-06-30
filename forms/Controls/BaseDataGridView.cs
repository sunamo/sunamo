using System;
using System.Windows.Forms;
/// <summary>
/// Pak je treba prekryt jen metody:
/// 
/// OnCellBeginEdit - pokud edituji sloupec ktery nemuzu zastavit. Pokud sloupec editovat muzu, ulozit puvodni hodnotu
/// OnDataError - Nastavit puvodni hodnotu. Mozna to je trochu slozitejsi, kdyztak se koukni do ParsovacCammin
/// OnCurrentCellDirtyStateChanged - pouze zavolam this.CommitEdit(DataGridViewDataErrorContexts.Commit); Tuto metodu je bezpodminenene nutne zavolat, jinak se minimalne posledni editovany hodnota neulozi!!
/// OnCellValueChanged - pokud se nepodari vyparsovat nova hodnota, ulozim starou
/// </summary>
public class BaseDataGridView : DataGridView
{
    /// <summary>
    /// Je tu proto ze pozdeji bych chtel to automatizovat udalostmi ktere jsou ve komentu tridy
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
