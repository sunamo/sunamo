using desktop.Controls.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

public partial class ResourceDictionaryStyles
{
    #region 10 for remembering default size
    public static void Margin10(IEnumerable<SunamoPasswordBox> p)
    {
        Margin(def, p);
    }
    #endregion

    public static void Margin(double d, IEnumerable<SunamoPasswordBox> p)
    {
        foreach (var item in p)
        {
            item.Margin = new Thickness(d);
        }
    }
}