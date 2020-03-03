using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ConfigurableWindow.Shared;

/// <summary>
/// ConfigurableWindowHelper - for use when Window is xaml
/// ConfigurableWindowInstance - for use when Window is only cs. Must be Instance because ConfigurableWindow is namespace
/// </summary>
public class ConfigurableWindowHelper
{
    public static void SourceInitialized(ConfigurableWindowWrapper c)
    {
        if (c != null)
        {
            c.w.WindowState = c._settings.WindowState;
        }
    }

    public static void RenderSizeChanged(ConfigurableWindowWrapper c)
    {
        if (c != null)
        {
            if (c._isLoaded && c.w.WindowState == WindowState.Normal)
            {
                c._settings.WindowSize = c.w.RenderSize;
            }
        }
    }
}