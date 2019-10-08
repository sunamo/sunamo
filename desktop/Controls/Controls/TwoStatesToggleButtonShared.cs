using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

public static partial class TwoStatesToggleButton{

    static Dictionary<ToggleButton, bool?> previousCheched = new Dictionary<ToggleButton, bool?>();
    public static bool IsChecked(ToggleButton tb)
    {
        return previousCheched[tb].Value;
    } 
}