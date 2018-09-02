using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.System.Threading;
using Windows.UI.Xaml.Controls;

namespace apps
{
    public class WebViewHelper
    {
         WebView wv = new WebView();
         bool loaded = false;
         string loadedSource = "";

        public WebViewHelper()
        {
            wv.NavigationFailed += wv_NavigationFailed;
            wv.LoadCompleted += wv_LoadCompleted;
        }

         void wv_LoadCompleted(object sender, Windows.UI.Xaml.Navigation.NavigationEventArgs e)
        {
            loaded = true;
            loadedSource = e.Content.ToString();
        }

         void wv_NavigationFailed(object sender, WebViewNavigationFailedEventArgs e)
        {
            
        }

        public async Task<string> GetStringAsync(string uri)
        {
            loadedSource = "";
            loaded = false;
            wv.Navigate(new Uri(uri));
            
            while (!loaded)
            {
                WorkItemHandler wih = new WorkItemHandler(WaitOneSecond);
                await Windows.System.Threading.ThreadPool.RunAsync(wih);
                
            }
            return loadedSource;
        }

        async void WaitOneSecond(IAsyncAction h)
        {
            await Task.Delay(TimeSpan.FromSeconds(1));   
        }
    }
}
