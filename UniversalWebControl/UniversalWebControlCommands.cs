using apps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml.Controls;

namespace UniversalWebControl
{
    public class GoBackCommand : ISunamoAsyncCommand
    {
        static bool? previousCanExecute = null;
        /// <summary>
        /// Bude nastaven na null pokud nebude zobrazen žádný SunamoBrowser
        /// </summary>
        static WebView webView = null;
        /// <summary>
        /// 
        /// </summary>
        static SunamoBrowser sunamoBrowser = null;

        public async static Task<bool> Set(SunamoBrowser sunamoBrowser2)
        {
            ISunamoAsyncCommand currentCommand = SunamoBrowser.Instance.cmdGoBackCommand;
            bool canExecute = false;
            if (sunamoBrowser2 == null)
            {
                canExecute = false;
            }
            else
            {
                sunamoBrowser = sunamoBrowser2;
                webView = sunamoBrowser2.WebView;
                canExecute = await currentCommand.CanExecute(null);
            }
            if (previousCanExecute.HasValue)
            {
                if (previousCanExecute.Value != canExecute)
                {
                    currentCommand.RaiseCanExecuteChanged(canExecute);
                }
            }
            else
            {
                currentCommand.RaiseCanExecuteChanged(canExecute);
            }

            previousCanExecute = canExecute;
            return canExecute;
        }

        public void RaiseCanExecuteChanged(bool canNow)
        {
            if (CanExecuteChanged != null)
            {
                CanExecuteChanged(webView, canNow);
            }
        }

        public event VoidObjectBool CanExecuteChanged;

        public async Task<bool> CanExecute(object parameter)
        {
            if (webView != null)
            {
                if (!sunamoBrowser.IsNavigating)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// A1 je aktuální index(ne nový!!)
        /// </summary>
        /// <param name="parameter"></param>
        public void Execute(object parameter)
        {
            webView.GoBack();
        }
    }

    public class GoForwardCommand : ISunamoAsyncCommand
    {
        static bool? previousCanExecute = null;
        /// <summary>
        /// Bude nastaven na null pokud nebude zobrazen žádný SunamoBrowser
        /// </summary>
        static WebView webView = null;
        /// <summary>
        /// 
        /// </summary>
        static SunamoBrowser sunamoBrowser = null;

        public async static Task<bool> Set(SunamoBrowser sunamoBrowser2)
        {
            ISunamoAsyncCommand currentCommand = SunamoBrowser.Instance.cmdGoForwardCommand;
            bool canExecute = false;
            if (sunamoBrowser2 == null)
            {
                canExecute = false;
            }
            else
            {
                sunamoBrowser = sunamoBrowser2;
                webView = sunamoBrowser2.WebView;
                canExecute = await currentCommand.CanExecute(null);
            }
            if (previousCanExecute.HasValue)
            {
                if (previousCanExecute.Value != canExecute)
                {
                    currentCommand.RaiseCanExecuteChanged(canExecute);
                }
            }
            else
            {
                currentCommand.RaiseCanExecuteChanged(canExecute);
            }
            previousCanExecute = canExecute;
            return canExecute;
        }

        public void RaiseCanExecuteChanged(bool canNow)
        {
            if (CanExecuteChanged != null)
            {
                CanExecuteChanged(webView, canNow);
            }
        }

        public event VoidObjectBool CanExecuteChanged;

        public async Task<bool> CanExecute(object parameter)
        {
            if (webView != null)
            {
                if (!sunamoBrowser.IsNavigating)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// A1 je aktuální index(ne nový!!)
        /// </summary>
        /// <param name="parameter"></param>
        public void Execute(object parameter)
        {
            webView.Refresh();
        }
    }

    public class ReloadCommand : ISunamoAsyncCommand
    {
        static bool? previousCanExecute = null;
        /// <summary>
        /// Bude nastaven na null pokud nebude zobrazen žádný SunamoBrowser
        /// </summary>
        static WebView webView = null;
        /// <summary>
        /// 
        /// </summary>
        static SunamoBrowser sunamoBrowser = null;

        public async static Task<bool> Set(SunamoBrowser sunamoBrowser2)
        {
            ISunamoAsyncCommand currentCommand = SunamoBrowser.Instance.cmdReloadCommand;
            bool canExecute = false;
            if (sunamoBrowser2 == null)
            {
                canExecute = false;
            }
            else
            {
                sunamoBrowser = sunamoBrowser2;
                webView = sunamoBrowser2.WebView;
                canExecute = await currentCommand.CanExecute(null);
            }
            if (previousCanExecute.HasValue)
            {
                if (previousCanExecute.Value != canExecute)
                {
                    currentCommand.RaiseCanExecuteChanged(canExecute);
                }
            }
            else
            {
                currentCommand.RaiseCanExecuteChanged(canExecute);
            }
            previousCanExecute = canExecute;
            return canExecute;
        }

        public void RaiseCanExecuteChanged(bool canNow)
        {
            if (CanExecuteChanged != null)
            {
                CanExecuteChanged(webView, canNow);
            }
        }

        public event VoidObjectBool CanExecuteChanged;

        public async Task<bool> CanExecute(object parameter)
        {
            if (webView != null)
            {
                if (!sunamoBrowser.IsNavigating)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// A1 je aktuální index(ne nový!!)
        /// </summary>
        /// <param name="parameter"></param>
        public void Execute(object parameter)
        {
            webView.Refresh();
        }
    }

    public class StopLoadingCommand : ISunamoAsyncCommand
    {
        static bool? previousCanExecute = null;
        /// <summary>
        /// Bude nastaven na null pokud nebude zobrazen žádný SunamoBrowser
        /// </summary>
        static WebView webView = null;
        /// <summary>
        /// 
        /// </summary>
        static SunamoBrowser sunamoBrowser = null;

        public async static Task<bool> Set(SunamoBrowser sunamoBrowser2)
        {
            ISunamoAsyncCommand currentCommand = SunamoBrowser.Instance.cmdStopLoadingCommand;
            bool canExecute = false;
            if (sunamoBrowser2 == null)
            {
                canExecute = false;
            }
            else
            {
                sunamoBrowser = sunamoBrowser2;
                webView = sunamoBrowser2.WebView;
                canExecute = await currentCommand.CanExecute(null);
            }
            if (previousCanExecute.HasValue)
            {
                if (previousCanExecute.Value != canExecute)
                {
                    currentCommand.RaiseCanExecuteChanged(canExecute);
                }
            }
            else
            {
                currentCommand.RaiseCanExecuteChanged(canExecute);
            }
            previousCanExecute = canExecute;
            return canExecute;
        }

        public void RaiseCanExecuteChanged(bool canNow)
        {
            if (CanExecuteChanged != null)
            {
                CanExecuteChanged(webView, canNow);
            }
        }

        public event VoidObjectBool CanExecuteChanged;

        public async Task<bool> CanExecute(object parameter)
        {
            if (webView != null)
            {
                if (sunamoBrowser.IsNavigating)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// A1 je aktuální index(ne nový!!)
        /// </summary>
        /// <param name="parameter"></param>
        public void Execute(object parameter)
        {
            webView.Stop();
        }
    }
}
