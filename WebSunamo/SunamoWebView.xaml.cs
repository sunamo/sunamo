using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using HtmlAgilityPack;
using Microsoft.Toolkit.Win32.UI.Controls.Interop.WinRT;
using sunamo.Html;


    /// <summary>
    /// Interaction logic for SunamoWebView.xaml
    /// </summary>
    public partial class SunamoWebView : UserControl, ISunamoBrowser<Control>
    {
        public Uri Source { get => wv.Source; set => wv.Source = value; }
        #region Events from WebView
        public event EventHandler<WebViewControlNavigationCompletedEventArgs> NavigationCompleted;
        public event EventHandler<WebViewControlDOMContentLoadedEventArgs> DOMContentLoaded;
        #endregion
        #region Events from UIElement
        public event EventHandler<ManipulationCompletedEventArgs> ManipulationCompleted; 
        #endregion

        public string html = null;

        public string HTML => html;

        public SunamoWebView()
        {
            InitializeComponent();

            // From UIElement
            wv.ManipulationCompleted += wv_ManipulationCompleted;

            wv.NavigationCompleted += wv_NavigationCompleted;
            wv.DOMContentLoaded += Wv_DOMContentLoaded;
        }

        private void Wv_DOMContentLoaded(object sender, WebViewControlDOMContentLoadedEventArgs e)
        {
        html = GetContent();
        // In e is only Uri

        if (DOMContentLoaded != null)
            {
                DOMContentLoaded(null, null);
            }
        }

        private void wv_NavigationCompleted(object sender, Microsoft.Toolkit.Win32.UI.Controls.Interop.WinRT.WebViewControlNavigationCompletedEventArgs e)
        {
            if (e.IsSuccess)
            {
                html = GetContent();
            }

            if (NavigationCompleted != null)
            {
                NavigationCompleted(null, null);
            }
        }

        /// <summary>
        /// Event is from UIElement
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void wv_ManipulationCompleted(object sender, ManipulationCompletedEventArgs e)
        {
            
        }

        public string GetContent()
        {
        

            WebViewTag tag = wv.Tag as WebViewTag;
            // Zde je to v pořádku, sunamo.cz vrátí dobrý výsledek ale gc.com si to chrání
            tag.content =  Eval("document.documentElement.outerHTML;");
            return tag.content;
        }

        public string Eval(string javascript)
        {
            try
            {
                string result = AsyncHelper.ci.GetResult<string>(wv.InvokeScriptAsync("eval", new List<string> { javascript }));
            }
            catch (Exception ex)
            {
                if (ex.Message == "Exception from HRESULT: 0x80020101")
                {
                    return string.Empty;
                }
            }

            return string.Empty;
        }

        /// <summary>
        /// Return null if HTML property is null
        /// </summary>
        /// <returns></returns>
        public HtmlDocument GetHtmlDocument()
        {
            if (string.IsNullOrEmpty(html))
            {
                return null;
            }
            var hd = HtmlAgilityHelper.CreateHtmlDocument();
            hd.LoadHtml(HTML);
            return hd;
        }

        public void Navigate(string uri)
        {
            wv.Navigate(new Uri(uri));
        }

        public bool ScrollToEnd()
        {
        return false;
        }

        public void Init()
        {
            
        }
    }
