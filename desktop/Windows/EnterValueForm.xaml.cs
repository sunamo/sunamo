using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace desktop
{
    /// <summary>
    /// Interaction logic for EnterValueForm.xaml
    /// </summary>
    public partial class EnterValueForm : Window, IResult
    {
        public EnterValueForm(string whatEnter)
        {
            InitializeComponent();
            tbWhatEnter.Text = "Enter " + whatEnter + " and press enter.";

        }

        private void btnEnter_Click_1(object sender, RoutedEventArgs e)
        {
            if (AfterEnteredValue(txtEnteredText))
            {
                Finished(txtEnteredText.Text);
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
            txtEnteredText.BorderBrush = new SolidColorBrush( Colors.Red);
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



        public event VoidObject Finished;
    }
}
