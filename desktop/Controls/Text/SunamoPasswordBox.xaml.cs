﻿using System;
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

namespace desktop.Controls.Text
{
    /// <summary>
    /// Interaction logic for SunamoPasswordBox.xaml
    /// </summary>
    public partial class SunamoPasswordBox : UserControl
    {
        public SunamoPasswordBox()
        {
            InitializeComponent();
        }

        public bool ShowTxtShowPassword
        {
            set
            {
                var v = VisibilityBooleanConverter.FromBool(value);
                txtShowPassword.Visibility = v;
                btnShowPassword.Visibility = v;
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
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            txtShowPassword.Text = txtPassword.Password;
        }
    }
}