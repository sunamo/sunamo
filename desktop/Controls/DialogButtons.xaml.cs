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
    /// Interaction logic for DialogButtons.xaml
    /// </summary>
    public partial class DialogButtons : UserControl, IUserControlInWindow
    {
        public DialogButtons()
        {
            InitializeComponent();
        }

        public UIElement CustomControl
        {
            set
            {
                grid.Children.Insert(0, value);
            }
            get
            {
                return grid.Children[0];
            }
        }

        public bool? DialogResult
        {
            set
            {
                ChangeDialogResult(value);
            }
        }

        public event VoidBoolNullable ChangeDialogResult;

        public bool IsEnabledBtnOk
        {
            set
            {
                btnOk.IsEnabled = value;
            }
        }

        public bool IsEnabledBtnApply
        {
            set
            {
                btnApply.IsEnabled = value;
            }
        }

        public Visibility VisibilityBtnApply
        {
            set
            {
                btnApply.Visibility = value;
            }
        }


        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void btnApply_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = null;
        }
    }
}
