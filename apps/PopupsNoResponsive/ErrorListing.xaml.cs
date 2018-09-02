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
    public sealed partial class ErrorListing : UserControl, IPopupSmall
    {
        public event RoutedEventHandler ClickOK;

        public void ApplyColorTheme(ColorTheme ct)
        {
            ColorThemeHelper.ApplyColorTheme(border, ct);
        }

        public string Title
        {
            set
            {
                tbTitle.Text = value;
            }
        }

        /// <summary>
        /// Musíš nastavit i Visible
        /// </summary>
        public string Collapse
        {
            set
            {
                ttCollapse.Content = value;
                if (!string.IsNullOrWhiteSpace(value))
                {
                    tbCollapse.Visibility = Windows.UI.Xaml.Visibility.Visible;
                }
                else
                {
                    tbCollapse.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                }
            }
        }
        /// <summary>
        /// Musíš nastavit i Collapsee
        /// </summary>
        public string Visible
        {
            set
            {
                tbChybovaZprava.Text = value;
            }
        }

        public ErrorListing()
        {
            this.InitializeComponent();
        }

        private void OnClickOK(object sender, RoutedEventArgs e)
        {
            ClickOK(sender, e);
        }

        public Brush PopupBorderBrush
        {
            set { border.BorderBrush = value; }
        }


        public event RoutedEventHandler ClickCancel;
    }
}
