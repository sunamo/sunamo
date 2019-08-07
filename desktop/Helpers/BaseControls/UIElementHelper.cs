using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
public partial class UIElementHelper
{
    public static void SetIsEnabled(bool v, params UIElement[] elements)
    {
        foreach (var item in elements)
        {
            item.IsEnabled = v;
        }
    }

}