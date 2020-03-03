using sunamo.Enums;
using System;
using System.Collections.Generic;

using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace desktop
{
    public sealed partial class ErrorListing : UserControl //, IPopupResponsive, IPopupEvents<object>
    {
        public event VoidT<object> ClickOK;

        public string Title
        {
            set
            {
                tbTitle.Text = value;
            }
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
                    tbCollapse.Visibility = Visibility.Visible;
                }
                else
                {
                    tbCollapse.Visibility = Visibility.Collapsed;
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
            ClickOK(null);
        }

        public Brush PopupBorderBrush
        {
            set { border.BorderBrush = value; }
        }


        public event VoidT<object> ClickCancel;

        public void ApplyColorTheme(ColorTheme ct)
        {
            ColorThemeHelper.ApplyColorTheme(border, ct);
        }
    }
}