using System;
using System.Collections.Generic;
/// <summary>
/// Generická třída třídy TSDDBH - ToolStripDropDownButtonHelper
/// </summary>
/// <typeparam name="T"></typeparam>
using System.Windows.Controls;
public class MenuItemWithSubitemsHelper<T> : MenuItemWithSubitemsHelper
{
    public MenuItemWithSubitemsHelper(MenuItem tsddb)
        : base(tsddb, true)
    { }

    public MenuItemWithSubitemsHelper(MenuItem tsddb, Array bs, T defaultValue)
        : base(tsddb, true)
    {
        if (tsddb.ToolTip == null)
        {
            originalToolTipText = "";
        }
        else
        {
            originalToolTipText = tsddb.ToolTip.ToString();
        }
        AddValuesOfEnumAsItems(bs, false);
        SelectedO = defaultValue;
        prev = GetItemWithTag(defaultValue);
        tsddb.ToolTip = originalToolTipText + " " + defaultValue.ToString();
    }

    private MenuItem GetItemWithTag(T defaultValue)
    {
        foreach (MenuItem item in tsddb.Items)
        {
            if (item.Tag.ToString() == defaultValue.ToString())
            {
                return item;
            }
        }
        return null;
    }

    public T SelectedEnumValue
    {
        get
        {
            if (Selected)
            {
                return (T)Enum.Parse(typeof(T), SelectedS);
            }
            return default(T);
        }
    }

    public void AddValuesOfEnumAsItems(T t)
    {
        base.AddValuesOfEnumAsItems<T>(t);
    }

    
}
