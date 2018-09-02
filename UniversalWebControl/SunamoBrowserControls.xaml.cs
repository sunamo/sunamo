using apps;
using apps.AwesomeFont;
using sunamo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Windows.UI.Xaml.Controls;

namespace UniversalWebControl
{
    /// <summary>
    /// Interaction logic for SunamoBrowserControls.xaml
    /// </summary>
    public partial class SunamoBrowserControls : UserControl
    {
        public event VoidVoid BackButtonClick;
        public event VoidVoid NextButtonClick;
        public event VoidVoid StopButtonClick;
        public event VoidVoid ReloadButtonClick;
        //public event VoidVoid CloseButtonClick;
        public event VoidVoid CustomButtonClick;

        /// <summary>
        /// Nutno vždy pak zavolat metodu Initialize
        /// </summary>
        public SunamoBrowserControls()
        {
            InitializeComponent();

            if (!UniversalInterop.IsLargeScreen())
            {
                if (UniversalInterop.IsPortrait(false))
                {
                    txtSearch.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                }
            }

            Loaded += SunamoBrowserControls_Loaded;    
        }

        public ISunamoBrowser<Control> MainPage { get; set; }

        private async void SunamoBrowserControls_Loaded(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
             Initialize(apps.Essential.ThisApp.l, "");
        }

        public event VoidUri NewUriEntered;
        public event VoidString NewSearchRequested;

        /// <summary>
        /// Můžeš si nastavit zachytávání až 3 událostí:
        /// CustomButtonClick - povinné, pokud tlačítko zobrazuji(Nastavil jsem mu nějaký text)
        /// CloseButtonClick - povinné, při ukončení awebrowseru ho musím skrýt z volajícího controlu
        /// LoadCompleted - Nepovinné, na rozdíl od ostatních událostí tato sebou nese string, který obsahuje full html načtené stránky
        /// </summary>
        /// <param name="TextCustomButton"></param>
        /// <param name="homeAdressWithoutHttp"></param>
        /// <param name="loadCompleted"></param>
        public void Initialize(Langs l, string TextCustomButton)
        {
            SetAwesomeFontIcon(afBtnBack, "\uf060");
            SetAwesomeFontIcon(afBtnNext, "\uf061");
            SetAwesomeFontIcon(afBtnReload, "\uf021");
            SetAwesomeFontIcon(afBtnStopLoading, "\uf256");
            if (TextCustomButton != "")
            {   
                btnCustom.Visibility = Windows.UI.Xaml.Visibility.Visible;
                afBtnCustom.Text = TextCustomButton;
            }
            else
            {
                btnCustom.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            }
        }

        private async void SetAwesomeFontIcon(TextBlock afBtnNext, string otf)
        {
            int nextDivide = 2;
            await AwesomeFontControls.SetAwesomeFontSymbol(afBtnNext, otf, Brushes.Orange, (ActualHeight / 2 / nextDivide) - 6, null);
        }

        public void SetUri(string uri)
        {
            txtAddress.Text = uri;
        }

        private void txtAddress_KeyUp(object sender, Windows.UI.Xaml.Input.KeyRoutedEventArgs e)
        {
        }

        private void btnBack_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            BackButtonClick();
        }

        private void btnNext_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            NextButtonClick();
        }

        private void btnReload_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            ReloadButtonClick();
        }

        private void btnHome_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            
        }

        private void btnStopLoading_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            StopButtonClick();
        }

        private void txtSearch_KeyUp(object sender, Windows.UI.Xaml.Input.KeyRoutedEventArgs e)
        {
            if (e.Key == Windows.System.VirtualKey.Enter)
            {
                string text = txtSearch.Text.Trim();
                if (text != "")
                {
                    txtSearch.Text = "";
                    NewSearchRequested(WebUtility.UrlEncode( text));
                }
            }
        }
    }
}
