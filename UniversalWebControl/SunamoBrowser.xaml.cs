using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using HtmlAgilityPack;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using System.Threading.Tasks;

namespace UniversalWebControl
{
    public sealed partial class SunamoBrowser : UserControl, ISunamoAppsBrowser<Control>
    {
        public static Dictionary<WebView, SunamoBrowser> webViewToSunamoBrowser = new Dictionary<WebView, SunamoBrowser>();
        bool isNavigating = false;
        public ISunamoBrowserHost sbHost { get; set; }

        public bool IsNavigating
        {
            get
            {
                return isNavigating;
            }
            set
            {
                
                isNavigating = value;
            }
        }

        public SunamoBrowser()
        {
            this.InitializeComponent();

            instance = this;
            
            //webView.Navigate(new Uri("about:blank"));
            webViewToSunamoBrowser.Add(webView, this);

            GoBackCommand.Set(this);
            GoForwardCommand.Set(this);
            ReloadCommand.Set(this);
            StopLoadingCommand.Set(this);
        }

        ~SunamoBrowser()
        {
            webViewToSunamoBrowser.Remove(webView);
        }

        public Uri Source
        {
            get
            {
                return webView.Source;
            }
            set
            {
                webView.Source = value;
            }
        }

        static SunamoBrowser instance = null;

        public static SunamoBrowser Instance
        {
            get
            {
                return instance;
            }
            set
            {
                instance = value;
            }
        }

        public WebView WebView
        {
            get
            {
                return webView;
            }

            set
            {
                webView = value;
            }
        }
        

        HtmlDocument hd = null;
        public HtmlDocument HtmlDocument
        {
            
            set
            {
                hd = value;
            }
        }

        public async Task< HtmlDocument> GetHtmlDocument()
        {
            if (IsNavigating)
            {
                return null;
            }
            hd = new HtmlDocument();
            hd.LoadHtml(await HTML(webView));
            return hd;
        }

        public async Task Click(string buttonId)
        {
            await Eval(webView, "document.getElementById('" + buttonId + "').click();");
        }

        public async Task<string> Eval(WebView wv, string javascript)
        {
            return await wv.InvokeScriptAsync("eval", new string[] { javascript });
        }

        public async Task<string> HTML(WebView wv)
        {
            try
            {
                return await Eval(wv, "document.documentElement.outerHTML;");
            }
            catch (Exception)
            {
                return "";
            }
        }


        public event VoidUri SourceUpdated;

        private async void webView_NavigationStarting(WebView sender, WebViewNavigationStartingEventArgs args)
        {
                if (sbHost.lastOpenedSunamoBrowserAlsoInBackground.webView != sender && sbHost.SelectedWebView() == sender)
                {
                IsNavigating = true;
            }
            if (SourceUpdated != null)
            {
                SourceUpdated(args.Uri);
            }
            
            
            
            //bool b = await cmdStopLoadingCommand.CanExecute(null);
        }

        private async void webView_NavigationFailed(object sender, WebViewNavigationFailedEventArgs e)
        {
            IsNavigating = false;
        }

        private async void webView_NavigationCompleted(WebView sender, WebViewNavigationCompletedEventArgs args)
        {
            IsNavigating = false;
            //bool b = await cmdStopLoadingCommand.CanExecute(null);
        }
    }
}
