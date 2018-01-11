using System.Windows.Forms;
using System;

/// <summary>
/// Tento OP nejde p�idat v Designeru, proto�e je generick�. 
/// Pokud pot�ebuje� n�co p�idat v designeru, pracuj d�le s ComboBoxEnumHelper
/// </summary>
/// <typeparam name="T"></typeparam>
public class ComboBoxEnum<T> : ComboBox
{
    #region base
    public ComboBoxEnum()
        : base()
    {
        PridejPolozky();
    }
    #endregion

    #region ICB<T> Members

    public T VratNastavene()
    {
        return (T)Enum.Parse(typeof(T), SelectedItem.ToString());
    }

    public void NastavHodnotuVyctu(T sablonyProjektu)
    {
        for (int i = 0; i < Items.Count; i++)
        {
            string gg = Items[i].ToString();
            if (gg == sablonyProjektu.ToString())
            {
                SelectedIndex = i;
                break;
            }
        }
    }

    void PridejPolozky()
    {
        if (!DesignMode)
        {
            this.DropDownStyle = ComboBoxStyle.DropDownList;
            foreach (string item in Enum.GetNames(typeof(T)))
            {
                if (!Items.Contains(item))
                {
                    this.Items.Add(item);

                }
            }
            this.SelectedIndex = 0;
            this.Text = Items[0].ToString();
        }
    }

    #endregion
}

