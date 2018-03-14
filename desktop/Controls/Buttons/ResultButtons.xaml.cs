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

namespace desktop.Controls
{
    /// <summary>
    /// Interaction logic for ResultButtons.xaml
    /// </summary>
    public partial class ResultButtons : UserControl
    {
        public event VoidVoid AllRightClick;
        // as event - useful when output will be editable
        public event VoidVoid CopyToClipboard;

        public ResultButtons()
        {
            InitializeComponent();
        }

        private void btnCopyToClipboard_Click(object sender, RoutedEventArgs e)
        {
            CopyToClipboard();
        }

        private void btnAllRight_Click(object sender, RoutedEventArgs e)
        {
            AllRightClick();
        }
    }
}
