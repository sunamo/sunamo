using apps;
using sunamo;
using sunamo.Enums;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

namespace apps
{
    public sealed partial class WebBrowser : UserControl, IPopupWholeScreen
    {
        Uri uri = null;
        bool canGoBack = false;
        bool canGoNext = false;
        List<Uri> lastUri = new List<Uri>();
        int actualIndex = 0;
        public event UriEventHandler CustomButtonClick;
        public event VoidVoid CloseButtonClick;
        string homeAdressWithoutHttp = null;
        public event LoadCompletedEventHandler LoadCompleted;
        bool reload = false;
        List<bool> backnext = new List<bool>();

        public void ApplyColorTheme(ColorTheme ct)
        {
            ColorThemeHelper.ApplyColorTheme(border, ct);
        }

        public WebBrowser(string TextCustomButton, string homeAdressWithoutHttp)
        {
            this.InitializeComponent();
            this.homeAdressWithoutHttp = homeAdressWithoutHttp;
            btnBack.Content = ImageHelper.MsAppx(true, AppPics.Previous);
            btnNext.Content = ImageHelper.MsAppx(true, AppPics.Next);
            btnReload.Content = ImageHelper.MsAppx(false, AppPics.Reload);
            btnHome.Content = ImageHelper.MsAppx(false, AppPics.Home);
            btnClose.Content = ImageHelper.MsAppx(false, AppPics.Logout);
            

            if (AppLangHelper.currentUICulture.TwoLetterISOLanguageName == "cs")
            {
                ttBtnBack.Content = "Zpět";
                ttBtnNext.Content = "Vpřed";
                ttBtnReload.Content = "Znovu načíst";
                ttBtnHome.Content = "Domů na " + homeAdressWithoutHttp;
                ttBtnClose.Content = "Zavřít prohlížeč";
            }
            else
            {
                ttBtnBack.Content = "Back";
                ttBtnNext.Content = "Next";
                ttBtnReload.Content = "Reload";
                ttBtnHome.Content = "Home to " + homeAdressWithoutHttp;
                ttBtnHome.Content = "Close webbrowser";
            }

            if (TextCustomButton != "")
            {
                btnCustom.Content = ImageHelper.MsAppx(true, AppPics.BoardPin); 
                btnCustom.Visibility = Windows.UI.Xaml.Visibility.Visible;
                ttBtnCustom.Content = TextCustomButton;
            }
            else
            {
                btnCustom.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            }

            //webView.Navigate(new Uri("http://www.google.com"));
            webView.LoadCompleted += webView_LoadCompleted;
            NavigateHome();
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

        void webView_NavigationFailed(object sender, WebViewNavigationFailedEventArgs e)
        {
            
        }

        public void HideWebView()
        {
            webView.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
        }

        public void ShowWebView()
        {
            webView.Visibility = Windows.UI.Xaml.Visibility.Visible;
        }

        void webView_LoadCompleted(object sender, NavigationEventArgs e)
        {
            if (!reload)
            {
                uri = e.Uri;
                txtAddress.Text = uri.ToString();

                lastUri.Add(uri);

                if (actualIndex != 0)
                {
                    btnBack.Content = ImageHelper.MsAppx(false, AppPics.Previous);
                    canGoBack = true;
                }
                else
                {
                    btnBack.Content = ImageHelper.MsAppx(true, AppPics.Previous);
                    canGoBack = false;
                }
                if (actualIndex == lastUri.Count - 1 || (backnext[backnext.Count - 1] == true && backnext[backnext.Count - 2] == false))
                {
                    btnNext.Content = ImageHelper.MsAppx(true, AppPics.Next);
                    canGoNext = false;
                }
                else
                {
                    btnNext.Content = ImageHelper.MsAppx(false, AppPics.Next);
                    canGoNext = true;
                }
                if (LoadCompleted != null)
                {
                    LoadCompleted(sender, e);   
                }
                
            }
        }

        public void EnableCustomButton(bool enable)
        {
            btnCustom.IsEnabled = enable;
            btnCustom.Content = ImageHelper.MsAppx(!enable, AppPics.BoardPin);
        }



        private void btnBack_Click_1(object sender, RoutedEventArgs e)
        {
            if (canGoBack)
            {
                reload = false;
                backnext.Add(false);
                actualIndex--;
                webView.Navigate(lastUri[actualIndex]);
            }
        }

        private void btnNext_Click_1(object sender, RoutedEventArgs e)
        {
            if (canGoNext)
            {
                reload = false;
                backnext.Add(true);
                actualIndex++;
                webView.Navigate(lastUri[actualIndex]);
            }
        }

        private void btnReload_Click_1(object sender, RoutedEventArgs e)
        {
            reload = true;
            backnext.Add(false);
            webView.Navigate(uri);
        }

        private void btnHome_Click_1(object sender, RoutedEventArgs e)
        {
            reload = true;
            backnext.Clear();
            lastUri.Clear();
            backnext.Add(false);
            NavigateHome();
        }

        private void NavigateHome()
        {
            reload = false;
            backnext.Add(false);
            webView.Navigate(new Uri("http://" + homeAdressWithoutHttp));
        }

        private void btnCustom_Click_1(object sender, RoutedEventArgs e)
        {
            
            CustomButtonClick(webView, new UriEventArgs(uri));
        }

        private void TextBox_KeyUp_1(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == Windows.System.VirtualKey.Enter)
            {
                Uri uriOut = null;
                if (Uri.TryCreate(txtAddress.Text, UriKind.Absolute, out uriOut))
                {
                    reload = false;
                    actualIndex++;
                    webView.Navigate(uriOut);
                }
            }
        }

        private void btnClose_Click_1(object sender, RoutedEventArgs e)
        {
            CloseButtonClick();
        }



        public Brush PopupBorderBrush
        {
            set { border.BorderBrush = value; }
        }

        public string xName
        {
            set { border.Name = value; }
        }
    }
}
