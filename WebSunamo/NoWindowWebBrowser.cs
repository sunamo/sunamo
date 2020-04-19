using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Toolkit.Win32.Samples.WPF.WebView;
using Microsoft.Toolkit.Wpf.UI.Controls;
using SunamoWeb;

namespace sunamo.web

{
    class a
    {

    }
}

/// <summary>
/// Is called only DownloadOrReadHiddenWebBrowser, no SHow etc.
/// </summary>
public class NoWindowWebBrowser : Window
{
    string html = string.Empty;

    public string HTML {
        get => html;
    }

	public SunamoWebView wv = new SunamoWebView();
    public event Action NavigationCompleted;
    public event Action DOMContentLoaded;

    public NoWindowWebBrowser()
	{
        wv.NavigationCompleted += Wv_NavigationCompleted;
        wv.DOMContentLoaded += Wv_DOMContentLoaded;

        //Width = 0;
        //Height = 0;
        //WindowStyle = WindowStyle.None;
        //ShowInTaskbar = false;
        //ShowActivated = false;

        StackPanel sp = new StackPanel();
        sp.Orientation = Orientation.Vertical;

        sp.Children.Add(new TextBox());
        sp.Children.Add(wv);

		Content = sp;
	}

    private void Wv_DOMContentLoaded(object sender, Microsoft.Toolkit.Win32.UI.Controls.Interop.WinRT.WebViewControlDOMContentLoadedEventArgs e)
    {
        if (DOMContentLoaded != null)
        {
            DOMContentLoaded();
        }
    }

    private void Wv_NavigationCompleted(object sender, Microsoft.Toolkit.Win32.UI.Controls.Interop.WinRT.WebViewControlNavigationCompletedEventArgs e)
    {
        if (NavigationCompleted != null)
        {
            NavigationCompleted();
        }
    }

    //private void Wv_ManipulationCompleted(object sender, System.Windows.Input.ManipulationCompletedEventArgs e)
    //{
    //    //var html = AsyncHelper.GetResult<string>()
    //}

    //private void Wv_NavigationCompleted(object sender, Microsoft.Toolkit.Win32.UI.Controls.Interop.WinRT.WebViewControlNavigationCompletedEventArgs e)
    //{
    //    html = 
    //}

    public  void DownloadOrReadHiddenWebBrowser(string path, string uri)
    {
        

        if (false)
        {
            html = TF.ReadFile(path);
        }
        else
        {
            // To co to sekalo bylo navigate
            // užij Navigate, not Source!!
            // bez uri
            wv.wv.Navigate(uri);

            //wv.wv.Source =new Uri(uri);
            //wv.wv.BringIntoView();



            Show();
            //html = Download(uri, null, path);
        }

        
    }
}