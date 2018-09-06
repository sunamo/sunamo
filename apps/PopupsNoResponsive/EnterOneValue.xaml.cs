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
    public sealed partial class EnterOneValue : UserControl, IPopupSmall
    {
        Langs l = Langs.cs;
        public event RoutedEventHandler ClickOK;
        public event RoutedEventHandler ClickCancel;
        public void ApplyColorTheme(ColorTheme ct)
        {
            ColorThemeHelper.ApplyColorTheme(border, ct);
        }

        public EnterOneValue(string coZadat, Langs l)
        {
            this.InitializeComponent();
            this.l = l;
            Reset(coZadat);
            
        }

        public string EnteredText
        {
            get
            {
                return wtbZadani.Text;
            }
        }

        private void OnClickCancel(object sender, RoutedEventArgs e)
        {
            ClickCancel(this, null);
        }

        private void OnClickOK(object sender, RoutedEventArgs e)
        {
            ClickOK(this, null);
        }

        public Brush PopupBorderBrush
        {
            set { border.BorderBrush = value; }
        }




        public void Reset(string coZadat)
        {
            tbTitle.Text = SH.FirstCharUpper(coZadat);
            if (l == Langs.cs)
            {
                tbCoZadat.Text = "Zde zadejte " + coZadat;
            }
            else
            {
                tbCoZadat.Text = "Here enter " + coZadat;
            }
            //wtbZadani.Text = tbCoZadat.Text;
            tbCoZadat.Text += ":";
        }
    }
}
