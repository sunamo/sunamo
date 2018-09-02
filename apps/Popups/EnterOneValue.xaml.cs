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
using sunamo.Enums;
using apps;


namespace apps
{
    public sealed partial class EnterOneValue : UserControl, IPopupResponsive, IPopupEvents<EnterOneValueEventArgs>
    {
        Langs l = Langs.cs;
        public event VoidT<EnterOneValueEventArgs> ClickOK;
        public event VoidT<EnterOneValueEventArgs> ClickCancel;
        public EnterOneValue(string coZadat, Langs l)
        {
            this.InitializeComponent();
            this.l = l;
            Reset(coZadat);
        }

        private string EnteredText
        {
            get
            {
                return wtbZadani.Text;
            }
        }

        private void OnClickCancel(object sender, RoutedEventArgs e)
        {
            ClickCancel(null);
        }

        private void OnClickOK(object sender, RoutedEventArgs e)
        {
            ClickOK(new EnterOneValueEventArgs { EnteredText = EnteredText });
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

        public void Reset(string coZadat)
        {
            tbTitle.Text = SH.FirstCharUpper(coZadat);
            tbCoZadat.Text = RL.GetString("HereEnter") + coZadat;
            //wtbZadani.Text = tbCoZadat.Text;
            tbCoZadat.Text += ":";
        }

        public void ApplyColorTheme(ColorTheme ct)
        {
            ColorThemeHelper.ApplyColorTheme(border, ct);
        }
    }
}
