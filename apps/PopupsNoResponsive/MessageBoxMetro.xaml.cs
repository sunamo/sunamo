using sunamo.Enums;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace apps.PopupsNoResponsive
{
    /// <summary>
    /// Přejmenován z MessageBox na MessageBoxMetro, protože MessageBox už v WPF byl, a nerad bych na něj odkazoval celou hiearchií. Dávej na to příště pozor.
    /// </summary>
    public sealed partial class MessageBoxMetro : UserControl, IPopupSmall
    {
        public event RoutedEventHandler ClickOK;
        public event RoutedEventHandler ClickCancel;

        public void ApplyColorTheme(ColorTheme ct)
        {
            ColorThemeHelper.ApplyColorTheme(border, ct);
        }

        public MessageBoxMetro(string title, string message)
        {
            this.InitializeComponent();

            tbTitle.Text = title;
            tbZprava.Text = message;
        }

       

        private void OnClickOK(object sender, RoutedEventArgs e)
        {
            ClickOK(sender, e);
        }

        private void OnClickCancel(object sender, RoutedEventArgs e)
        {
            ClickCancel(sender, e);
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
    }
}
