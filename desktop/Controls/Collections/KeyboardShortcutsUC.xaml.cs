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
    /// Interaction logic for KeyboardShortcutsUC.xaml
    /// </summary>
    public partial class KeyboardShortcutsUC : UserControl
    {
        public KeyboardShortcutsUC(Dictionary<string, string> dictionary)
        {
            InitializeComponent();



            foreach (var item in dictionary)
            {

            }
        }
    }
}
