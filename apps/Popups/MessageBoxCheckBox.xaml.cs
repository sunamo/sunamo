using sunamo.Enums;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace apps.Popups
{
    public sealed partial class MessageBoxCheckBox : UserControl, IPopupResponsive, IPopupEvents<object>
    {
        public event VoidT<object> ClickOK;
        public event VoidT<object> ClickCancel;

        public void ApplyColorTheme(ColorTheme ct)
        {
            ColorThemeHelper.ApplyColorTheme(border, ct);
        }

        /// <summary>
        /// Pokud nechceš zobrazit Storno tlačítko, musíš zavolat metodu this.ShowCancelButton. Pak nemusíš registrovat událost ClickCancel
        /// </summary>
        /// <param name="title"></param>
        /// <param name="message"></param>
        public MessageBoxCheckBox(string title, string message)
        {
            this.InitializeComponent();

            tbTitle.Text = title;
            tbZprava.Text = message;
        }

        private void OnClickOK(object sender, RoutedEventArgs e)
        {
            ClickOK(null);
        }

        private void OnClickCancel(object sender, RoutedEventArgs e)
        {
            ClickCancel(null);
        }

        public void ShowCheckbox(bool p)
        {
            if (p)
            {
                chbFirst.Visibility = Windows.UI.Xaml.Visibility.Visible;
            }
            else
            {
                chbFirst.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            }
        }

        public void ShowCancelButton(bool p)
        {
            if (p)
            {
                btnCancel.Visibility = Windows.UI.Xaml.Visibility.Visible;
            }
            else
            {
                btnCancel.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            }
        }

        public Brush PopupBorderBrush
        {
            set { border.BorderBrush = value; }
        }

        public Size MaxContentSize
        {
            get
            {
                //return maxContentSize;
                return FrameworkElementHelper.GetMaxContentSize(this);
            }
            set
            {
                //maxContentSize = value;
                FrameworkElementHelper.SetMaxContentSize(this, value);
            }
        }
    }
}
