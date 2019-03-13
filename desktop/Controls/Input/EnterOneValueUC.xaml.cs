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
    public partial class EnterOneValueUC : UserControl, IUserControlInWindow, IUserControlWithResult
    {
        public EnterOneValueUC()
        {
            InitializeComponent();
        }

        public EnterOneValueUC(string whatEnter) : this()
        {
            Init(whatEnter);
        }

        public EnterOneValueUC(string whatEnter, Size size) : this()
        {
            
            Init(whatEnter);
            if (size != Size.Empty)
            {
                txtEnteredText.Width = size.Width;
                txtEnteredText.Height = size.Height;
            }
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
                if (Finished != null)
                {
                    Finished(txtEnteredText.Text);
                }
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
                    if (Finished != null)
                    {
                        Finished(txtEnteredText.Text);
                    }
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
