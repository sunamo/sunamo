using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using desktop.Controls;
using desktop.Controls.Input;

namespace desktop.Controls.Input
{
    public partial class SelectManyFiles : UserControl
    {
    public static Type type = typeof(SelectManyFiles);

    public  void Validate(object tb, SelectManyFiles control, ValidateData d = null)
        {
            if (d == null)
            {
                d = new ValidateData();
            }
            foreach (SelectFile item in ControlFinder.StackPanel(this, "spFiles").Children)
        {
            item.Validate(tb, d);
        }
    }

    public void Validate(object tbFile, ValidateData d = null)
    {
        Validate(tbFile, this, d);
    }

    public static bool validated
    {
        get
        {
            return SelectFile.validated;
        }
        set
        {
            SelectFile.validated = value;
        }
    }
}
}