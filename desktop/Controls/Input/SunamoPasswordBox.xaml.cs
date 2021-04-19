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

namespace desktop.Controls.Input
{
    /// <summary>
    /// Interaction logic for SunamoPasswordBox.xaml
    /// </summary>
    public partial class SunamoPasswordBox : UserControl, IUserControlWithSizeChange
    {
        List<PasswordBox> pwbc;
        List<Button> btnc;
        List<TextBlock> tbc;

        public SunamoPasswordBox()
        {
            InitializeComponent();

            SizeChanged += SunamoPasswordBox_SizeChanged;

        }

        bool noMargin = false;

        public void Init(bool noMargin)
        {
            btnShowPassword.Content = sess.i18n(XlfKeys.HidePassword);

            this.noMargin = noMargin;
            if (noMargin)
            {

            }
            else
            { 
                // only txtShowPassword has in xaml 5px
                ResourceDictionaryStyles.Margin10(pwbc);
                ResourceDictionaryStyles.Margin10(btnc);
                ResourceDictionaryStyles.Margin10(tbc);
            }
        }

        public string Password
        {
            get
            {
                return txtPassword.Password;
            }
            set
            {
                txtPassword.Password = value;
            }
        }

        private void SunamoPasswordBox_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            
        }

        public Brush BrushOfBorder
        {
            get; set;
        } = Brushes.Yellow;

        public bool ShowTxtShowPassword
        {
            set
            {
                var v = VisibilityBooleanConverter.FromBool(value);


                spShowPassword.Visibility = v;
            }
        }

        public double OverallWidth
        {
            set
            {
                txtPassword.Width = value;
            }
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            var passwordBox = (PasswordBox)e.Source;
            if (passwordBox != null)
            {
                System.Diagnostics.Debug.WriteLine(passwordBox.Password);
                var textBox = passwordBox.Template.FindName("RevealedPassword", passwordBox) as TextBox;
                if (textBox != null)
                {
                    textBox.Text = passwordBox.Password;
                }
            }

            txtShowPassword.Text = txtPassword.Password;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var b = !VisibilityBooleanConverter.ToBool(txtShowPassword.Visibility);
            var v = VisibilityBooleanConverter.FromBool(b);
            if (v == Visibility.Collapsed)
            {
                v = Visibility.Hidden;
            }
            if (b)
            {
                btnShowPassword.Content = sess.i18n(XlfKeys.HidePassword);
            }
            else
            {
                btnShowPassword.Content = sess.i18n(XlfKeys.ShowPassword);
            }
            txtShowPassword.Visibility = v;
        }

        public void OnSizeChanged(DesktopSize e)
        {
            if (noMargin)
            {
                txtPassword.Width = spShowPassword.Width = e.Width;
            }
            else
            {
                txtPassword.Width = e.Width - 2 * 5;
            }
        }
    }
}