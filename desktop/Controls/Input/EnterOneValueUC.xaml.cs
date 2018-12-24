using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace desktop.Controls.Input
{
    /// <summary>
    /// Interaction logic for EnterOneValueUC.xaml
    /// </summary>
    public partial class EnterOneValueUC : UserControl, IResult, IUserControlInWindow
    {
        public EnterOneValueUC()
        {

        }

        public EnterOneValueUC(string whatEnter)
        {
            InitializeComponent();
            Init(whatEnter);
        }

        public void Init(string whatEnter)
        {
            tbWhatEnter.Text = "Enter " + whatEnter + " and press enter.";
        }

        private void btnEnter_Click_1(object sender, RoutedEventArgs e)
        {
            ButtonBase bb;
            
            if (AfterEnteredValue(txtEnteredText))
            {
                Finished(txtEnteredText.Text);
                DialogResult = true;
            }
        }

        public bool? DialogResult
        {
            set
            {
                if (ChangeDialogResult != null)
                {
                    ChangeDialogResult(value);
                }
            }
        }

        private bool AfterEnteredValue(TextBox txtEnteredText)
        {
            txtEnteredText.Text = txtEnteredText.Text.Trim();
            if (txtEnteredText.Text != "")
            {
                return true;
            }
            txtEnteredText.BorderThickness = new Thickness(2);
            txtEnteredText.BorderBrush = new SolidColorBrush(Colors.Red);
            return false;
        }

        private void txtEnteredText_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (AfterEnteredValue(txtEnteredText))
                {
                    Finished(txtEnteredText.Text);
                }
            }
        }

        public void Accept(object input)
        {
            txtEnteredText.Text = input.ToString();
            ButtonHelper.PerformClick(btnEnter);
            // Cant be, window must be already showned as dialog
            //DialogResult = true;
        }

        public event VoidObject Finished;
        public event VoidBoolNullable ChangeDialogResult;
    }
}
