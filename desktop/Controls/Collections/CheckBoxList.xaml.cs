using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace desktop.Controls.Collections
{
    /// <summary>
    /// Here due to need in SunamoCzAdmin.Wpf
    /// Definition of class is in apps
    /// </summary>
    public partial class CheckBoxList : UserControl
    {
        public CheckBoxList()
        {
            InitializeComponent();
        }

        public IEnumerable<CheckBox> Checked()
        {
            throw new NotImplementedException();
        }

        public void AddCheckBox(CheckBox chb)
        {
            throw new NotImplementedException();
        }
    }
}
