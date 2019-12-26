using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ConfigurableWindow.Shared;

/// <summary>
/// 
/// </summary>
public class ConfigurableWindowInstance : Window
{
    public ConfigurableWindowWrapper c = null;

    ApplicationDataContainer data = null;

    public ConfigurableWindowInstance(ApplicationDataContainer data)
    {
        this.data = data;
        SourceInitialized += ConfigurableWindow_SourceInitialized;
    }

    public IConfigurableWindowSettings CreateSettings()
    {
        return new ConfigurableWindow.Shared.WindowConfigSettings(this, data);
    }

    /// <summary>
    /// Don't copy, is not needed
    /// Has normal event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void ConfigurableWindow_SourceInitialized(object sender, EventArgs e)
    {
        ConfigurableWindowHelper.SourceInitialized(c);
    }

    /// <summary>
    /// Must be due to save changed size of window
    /// Dont have normal event
    /// </summary>
    /// <param name="info"></param>
    protected override void OnRenderSizeChanged(SizeChangedInfo info)
    {
        base.OnRenderSizeChanged(info);

        if (c != null)
        {
            if (c._isLoaded && this.WindowState == WindowState.Normal)
            {
                c._settings.WindowSize = this.RenderSize;
            }

            ConfigurableWindowHelper.RenderSizeChanged(c);
        }
    }
}