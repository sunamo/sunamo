

using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Navigation;
using HtmlAgilityPack;

namespace WebSunamo
{
    /// <summary>
    /// Use IE WebBrowser
    /// </summary>
    public partial class SunamoBrowser : UserControl, ISunamoBrowser<Control>, IControlWithResult
    {
        static Type type = typeof(SunamoBrowser);
        public SunamoBrowser()
        {
            InitializeComponent();
        }

        public Uri Source
        {
            get
            {
                return webControl.Source;
            }
            set
            {
                webControl.Source = value;
            }
        }

        public event VoidVoid CustomButtonClick;
        //public event UriEventHandler CustomButtonClick;
        public event VoidVoid CloseButtonClick;
        string homeAdressWithoutHttp = null;
        public event VoidString LoadCompleted;
        public bool automaticallyClickBtnCustom = false;
        Uri homeUrl = null;

        public string HTML
        {
            get
            {
                
                string html = webControl.InvokeScript("eval('document.documentElement.outerHTML');").ToString();
                return html;
            }
        }

        /// <summary>
        /// Můžeš si nastavit zachytávání až 3 událostí:
        /// CustomButtonClick - povinné, pokud tlačítko zobrazuji(Nastavil jsem mu nějaký text)
        /// CloseButtonClick - povinné, při ukončení awebrowseru ho musím skrýt z volajícího controlu
        /// LoadCompleted - Nepovinné, na rozdíl od ostatních událostí tato sebou nese string, který obsahuje full html načtené stránky
        /// </summary>
        /// <param name="TextCustomButton"></param>
        /// <param name="homeAdressWithoutHttp"></param>
        /// <param name="loadCompleted"></param>
        public void Initialize(string TextCustomButton, string homeAdressWithoutHttp)
        {
            homeUrl = new Uri("http" + ":" + "//" + homeAdressWithoutHttp);

            webControl.LoadCompleted += WebControl_LoadCompleted;

            ScrollToEnd();

            webControl.Source = homeUrl;
        }

        /// <summary>
        /// return whether was loaded new code
        /// </summary>
        public bool ScrollToEnd()
        {
            var html = MsHtml();
            html.parentWindow.scroll(0, 10000000);
            Thread.Sleep(5000);
                
                var html2 = MsHtml();

                var l1 = html.documentElement.outerHTML;
                var l2 = html2.documentElement.outerHTML;

                if (l1 != l2)
                {
                    return true;
                }
                return false;
            
        }

        public mshtml.HTMLDocument MsHtml()
        {
            return webControl.Document as mshtml.HTMLDocument;
        }

        private void WebControl_LoadCompleted(object sender, NavigationEventArgs e)
        {
            if (webControl.Source.ToString().StartsWith("https://samoobsluha.tescomobile.cz/selfcare/sc/history/showHistory?historyType=callList&msisdn="))
            {
                string cislo = webControl.Source.ToString().Replace("https://samoobsluha.tescomobile.cz/selfcare/sc/history/showHistory?historyType=callList&msisdn=", "");
                webControl.Source = new Uri("https://samoobsluha.tescomobile.cz/selfcare/sc/history?msisdn=" + cislo + "&filteringAttributesCallList.page=1&anchorValue=anchorValueCallList&render=renderLists#anchorValueCallList");
                return;
            }
            if (HTML.Contains("<body" + " "))
            {


                if (LoadCompleted != null)
                {
                    LoadCompleted(HTML);
                }

                if (automaticallyClickBtnCustom)
                {
                    CustomButtonClick();
                }
            }
        }

        public Button BtnCustom
        {
            get
            {
                return null;
                //return btnCustom;
            }
        }

        public void EnableCustomButton(bool enable)
        {
        }

        #region MyRegion

        void aweView_AddressChanged()
        {
            
        }

        public void Navigate(string uri)
        {
            webControl.Source = new Uri(uri);
        }

        

        private void SetUriToAddressBar(Uri uri)
        {
            txtAddress.Text = uri.ToString();
        }
        #endregion

        private void btnCustom_Click_1(object sender, RoutedEventArgs e)
        {
            CustomButtonClick();
            //CustomButtonClick(aweView, new UriEventArgs(uri));
        }

        private void btnClose_Click_1(object sender, RoutedEventArgs e)
        {
            CloseButtonClick();
        }

        public Brush PopupBorderBrush
        {
            set { border.BorderBrush = value; }
        }

        public event VoidT<Control> ShowPopup;
        public event VoidBoolNullable ChangeDialogResult;

        public string xName
        {
            set { border.Name = value; }
        }

        public HtmlDocument HtmlDocument
        {
            get
            {
                ThrowExceptions.NotImplementedMethod(Exc.GetStackTrace(),type, Exc.CallingMethod());
                return null;
            }

            set
            {
                ThrowExceptions.NotImplementedMethod(Exc.GetStackTrace(),type, Exc.CallingMethod());
            }
        }

        public bool? DialogResult { set => ChangeDialogResult(value); }

        private void txtAddress_KeyUp_1(object sender, System.Windows.Input.KeyEventArgs e)
        {
            #region Old
            #endregion
        }

        public string GetUri()
        {
            return webControl.Source.ToString();
        }

        public Task<HtmlDocument> GetHtmlDocument()
        {
            return null;
        }

        public void Accept(object input)
        {
            ThrowExceptions.NotImplementedMethod(Exc.GetStackTrace(),type, Exc.CallingMethod());
        }

        public void Init()
        {
            
        }

        public void FocusOnMainElement()
        {
            webControl.Focus();   
        }

        public Task<string> GetContent()
        {
            ThrowExceptions.NotImplementedMethod(Exc.GetStackTrace(), type, Exc.CallingMethod());
            return null;
        }
    }
}