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
public class ConfigurableWindowInstance : Window, IConfigurableWindow
{
    public ApplicationDataContainer data { get; set; }
    public ConfigurableWindow.Shared.ConfigurableWindowWrapper configurableWindowWrapper { get; set; }

    /// <summary>
    /// Data cant be static
    /// </summary>
    /// <param name="data"></param>
    public ConfigurableWindowInstance()
    {
        //this.data = data;
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
        ConfigurableWindowHelper.SourceInitialized(configurableWindowWrapper);
    }

    /// <summary>
    /// Must be due to save changed size of window
    /// Dont have normal event
    /// </summary>
    /// <param name="info"></param>
    protected override void OnRenderSizeChanged(SizeChangedInfo info)
    {
        base.OnRenderSizeChanged(info);

        if (configurableWindowWrapper != null)
        {
            if (configurableWindowWrapper._isLoaded && this.WindowState == WindowState.Normal)
            {
                configurableWindowWrapper._settings.WindowSize = this.RenderSize;
            }

            ConfigurableWindowHelper.RenderSizeChanged(configurableWindowWrapper);
        }
    }
}