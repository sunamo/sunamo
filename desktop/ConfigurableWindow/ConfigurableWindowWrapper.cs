using System;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace ConfigurableWindow.Shared
{
    /// <summary>
    /// An abstract Window descendant that provides simple 
    /// user settings persistence. The concrete backing 
    /// store is exposed by subclasses via overriding the 
    /// CreateSettings method.
    /// </summary>
    public  class ConfigurableWindowWrapper
    {
        public Window w = null;

        MenuItem miAlwaysOnTop = null;

       

        private void MiAlwaysOnTop_Click(object o, RoutedEventArgs e)
        {
            w.Topmost = miAlwaysOnTop.IsChecked;
            CheckMenuItemTopMost();
        }

        public void CheckMenuItemTopMost()
        {
            miAlwaysOnTop.IsChecked = _settings.AlwaysOnTop = w.Topmost;
        }

        #region Data

        public bool _isLoaded;
        public  IConfigurableWindowSettings _settings;

        #endregion // Data

        #region Constructor

        public ConfigurableWindowWrapper(Window w2, MenuItem miAlwaysOnTop)
        {
            this.miAlwaysOnTop = miAlwaysOnTop;
            miAlwaysOnTop.Click += MiAlwaysOnTop_Click;
            miAlwaysOnTop.IsCheckable = true;
            miAlwaysOnTop.Header = ContentControlHelper.GetContent(new ControlInitData { text = sess.i18n(XlfKeys.AlwaysOnTop) });

            w = w2;
            w.LocationChanged += W_LocationChanged;
            w.StateChanged += W_StateChanged;

            var ww = (IConfigurableWindow)w;

            _settings = ww.CreateSettings();

            if (_settings == null)
                ThrowExceptions.Custom(Exc.GetStackTrace(), type, Exc.CallingMethod(),"Cannot return null.");

            // Direct set _isLoaded to true, because I call this from _Loaded
            _isLoaded = true;
            //w.Loaded += delegate { _isLoaded = true; };

            this.ApplySettings();
        }

        static Type type = typeof(ConfigurableWindowWrapper);

        //

        private void W_StateChanged(object sender, EventArgs e)
        {
            if (_isLoaded)
            {
                // We don't want the Window to open in the 
                // minimized state, so ignore that value.
                if (w.WindowState != WindowState.Minimized)
                    _settings.WindowState = w.WindowState;
                else
                    _settings.WindowState = WindowState.Normal;
            }
        }



        #endregion // Constructor

        #region CreateSettings

        

        #endregion // CreateSettings

        #region Base Class Overrides

        private void W_LocationChanged(object sender, EventArgs e)
        {
            // We need to delay this call because we are 
            // notified of a location change before a 
            // window state change.  That causes a problem 
            // when maximizing the window because we record 
            // the maximized window's location, which is not 
            // something worth saving.
            w.Dispatcher.BeginInvoke(
                DispatcherPriority.Background,
                new ThreadStart(delegate
                {
                    if (_isLoaded && w.WindowState == WindowState.Normal)
                    {
                        Point loc = new Point(w.Left, w.Top);
                        _settings.WindowLocation = loc;
                    }
                }));
        }

        
        #endregion // Base Class Overrides

        #region Private Helpers

        void ApplySettings()
        {
            Size sz = _settings.WindowSize;
            w.Width = sz.Width;
            w.Height = sz.Height;

            Point loc = _settings.WindowLocation;

            // If the user's machine had two monitors but now only
            // has one, and the Window was previously on the other
            // monitor, we need to move the Window into view.
            bool outOfBounds =
                loc.X <= -sz.Width ||
                loc.Y <= -sz.Height ||
                SystemParameters.VirtualScreenWidth <= loc.X ||
                SystemParameters.VirtualScreenHeight <= loc.Y;

            if (_settings.IsFirstRun || outOfBounds)
            {
                w.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            }
            else
            {
                w.WindowStartupLocation = WindowStartupLocation.Manual;

                w.Left = loc.X;
                w.Top = loc.Y;

                // We need to wait until the HWND window is initialized before
                // setting the state, to ensure that this works correctly on
                // a multi-monitor system.  Thanks to Andrew Smith for this fix.
                w.SourceInitialized += delegate
                {
                    w.WindowState = _settings.WindowState;
                    w.Topmost = miAlwaysOnTop.IsChecked = _settings.AlwaysOnTop;
                };
            }
        }

        #endregion // Private Helpers
    }
}