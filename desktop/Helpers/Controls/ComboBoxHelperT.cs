using System;
using System.Windows.Controls;
public class ComboBoxHelper<T> : ComboBoxHelper
{
    public ComboBoxHelper(ComboBox tsddb)
        : base(tsddb)
    { }

    public ComboBoxHelper(ComboBox tsddb, Array bs, T defaultValue)
        : base(tsddb)
    {
        if (tsddb.ToolTip == null)
        {
            originalToolTipText = "";
        }
        else
        {
            originalToolTipText = tsddb.ToolTip.ToString();
        }
        AddValuesOfEnumAsItems(bs);
        SelectedO = defaultValue;
        tsddb.SelectedItem = defaultValue;
        tsddb.ToolTip = originalToolTipText + " " + defaultValue.ToString();
    }

    public T SelectedT
    {
        get
        {
            return (T)SelectedO;
        }
    }
}
