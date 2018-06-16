using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;

public class Types
    {
    public static readonly Type TextBlockType;
    public static readonly Type TextBoxType;

    static Types()
    {
        TextBlockType = typeof(TextBlock);
        TextBoxType = typeof(TextBox);
    }

    }

