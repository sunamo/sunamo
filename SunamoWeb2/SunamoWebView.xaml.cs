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
using sunamo.Html;
using Windows.Web.UI;

namespace SunamoWeb
{
    /// <summary>
    /// Use Edge WebView
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

            wv.NavigationCompleted += Wv_NavigationCompleted;
            //DOMContentLoaded is not available in WebView2
            //wv.DOMContentLoaded += Wv_DOMContentLoaded;
        }

      

        private void Wv_DOMContentLoaded(object sender, WebViewControlDOMContentLoadedEventArgs e)
        {
            html = AsyncHelper.ci.GetResult<string>(GetContent());
            // In e is only Uri

            if (DOMContentLoaded != null)
            {
                DOMContentLoaded(null, null);
            }
        }

        private void Wv_NavigationCompleted(object sender, Microsoft.Web.WebView2.Core.CoreWebView2NavigationCompletedEventArgs e)
        {
            if (!FS.ExistsFile(file))
            {
                var html = HTML;
                TF.SaveFile(html, file);
            }

                if (e.IsSuccess)
            {
                html = AsyncHelper.ci.GetResult<string>(GetContent());
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

        public async Task<string> GetContent()
        {
            return null;
        }

  

        /// <summary>
        /// Return null if HTML property is null
        /// </summary>
        public async Task<HtmlDocument> GetHtmlDocument()
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
            wv.Source = new Uri(uri);
        }

        public bool ScrollToEnd()
        {
            return false;
        }

        public void Init()
        {

        }

        string file = null;

        public void DownloadOrReadHiddenWebBrowser(string file, string searchQuery)
        {
            this.file = file;
            if (FS.ExistsFile(file))
            {
                html = TF.ReadAllText(file);
                
                Wv_NavigationCompleted(null, null);
            }
            else
            {
                
                Navigate(searchQuery);

            }
        }
    }
}