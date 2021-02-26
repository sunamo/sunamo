using System;
using System.Collections.Generic;
using System.Text;


public enum ListOperation
{
    RemoveAt,
    Insert,
    Add,
    Remove,
    Clear,
    /// <summary>
    /// Property in item
    /// like IsChecked of CheckBox
    /// </summary>
    PropertyChanged,
    Checked,
    Unchecked
}