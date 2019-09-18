using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace desktop.Controls.Input
{
    public partial class SelectMoreFolders : UserControl
    {
        public static Type type = typeof(SelectMoreFolders);
        public static bool validated
        {
            get
            {
                return SelectFolder.validated;
            }
            set
            {
                SelectFolder.validated = value;
            }
        }

        public static void Validate(object tb, SelectMoreFolders control, ValidateData d = null)
        {
            foreach (SelectFolder item in ControlFinder.StackPanel(control, "spFolders").Children)
            {
                item.Validate(tb, d);
            }
        }

        public void Validate(object tbFolder, ValidateData d = null)
        {
            Validate(tbFolder, this, d);
        }
    }

}