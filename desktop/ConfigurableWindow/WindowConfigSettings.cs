using ConfigurableWindow.Shared;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ConfigurableWindow.Shared
{
    public class WindowConfigSettings : ConfigurableWindowSettings
    {
        const string IS_FIRST_RUN = "IsFirstRun";
        const string WINDOW_LOCATION = "DemoWindowLocation";
        const string WINDOW_SIZE = "DemoWindowSize";
        const string WINDOW_STATE = "DemoWindowState";
        const string ALWAYS_ON_TOP = "AlwaysOnTop";

        /// <summary>
        /// A2 = Settings.Default
        /// </summary>
        /// <param name="window"></param>
        /// <param name="s"></param>
        public WindowConfigSettings(Window window, ApplicationDataContainer data)
            : base(
            //s, , ApplicationSettingsBase s
            data,
            IS_FIRST_RUN,
            WINDOW_LOCATION,
            WINDOW_SIZE,
            WINDOW_STATE, ALWAYS_ON_TOP)
        {
            // Note: You only want to have this code
            // in the application's main Window, not
            // in dialog boxes or other child Windows.
            window.Closed += delegate
            {
                if (this.IsFirstRun)
                    this.IsFirstRun = false;
            };
        }
    }
}