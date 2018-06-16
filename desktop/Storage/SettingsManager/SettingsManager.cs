using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace desktop.Storage
{
    /*Example usage

        IWindowWithSettingsManager

SettingsManager settingsManager = null;
        Dictionary<FrameworkElement, DependencyProperty> savedElements = new Dictionary<FrameworkElement, DependencyProperty>();
        public SettingsManager SettingsManager => settingsManager;

        public MainWindow()
        {
            InitializeComponent();

            settingsManager = new SettingsManager(Settings.Default, Settings.Default.Providers);
        }

        public void SaveSettings()
        {
            settingsManager.SaveSettings(this, savedElements);
        }

 private void MainWindow_Closing(object sender, CancelEventArgs e)
        {
            SaveSettings();
        }

private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            savedElements.Add(this, Window.BackgroundProperty);
            savedElements.Add(chb, CheckBox.BackgroundProperty);

            settingsManager.LoadSettings(this, savedElements);
        }

     */

    /// <summary>
    /// 6-3-2018 Working fine
    /// </summary>
    public class SettingsManager
    {
        // COpy to MainWindow
        //Dictionary<FrameworkElement, DependencyProperty> savedElements = new Dictionary<FrameworkElement, DependencyProperty>();
        ApplicationSettingsBase defaultSettings = null;
        SettingsProviderCollection providers = null;

        /// <summary>
        /// A1 - Settings.Default
        /// A2 - Settings.Default.Providers
        /// </summary>
        /// <param name="defaultSettings"></param>
        /// <param name="providers"></param>
        public SettingsManager(ApplicationSettingsBase defaultSettings, SettingsProviderCollection providers)
        {
            this.defaultSettings = defaultSettings;
            this.providers = providers;
        }

        public void LoadSettings(FrameworkElement sender, Dictionary<FrameworkElement, DependencyProperty> savedElements)
        {
            EnsureProperties(sender, savedElements);
            foreach (FrameworkElement element in savedElements.Keys)
            {
                try
                {
                    element.SetValue(savedElements[element], defaultSettings[sender.Name + "." + element.Name]);
                }
                catch (Exception ex)
                {
                }
            }
        }

        public void SaveSettings(FrameworkElement rootElement, Dictionary<FrameworkElement, DependencyProperty> savedElements)
        {
            EnsureProperties(rootElement, savedElements);
            foreach (FrameworkElement element in savedElements.Keys)
            {
                defaultSettings[rootElement.Name + "." + element.Name] = element.GetValue(savedElements[element]);
            }
            defaultSettings.Save();
        }

        public void EnsureProperties(FrameworkElement rootElement, Dictionary<FrameworkElement, DependencyProperty> savedElements)
        {
            foreach (FrameworkElement element in savedElements.Keys)
            {
                bool hasProperty =
                    defaultSettings.Properties[rootElement.Name + "." + element.Name] != null;

                if (!hasProperty)
                {
                    SettingsAttributeDictionary attributes = new SettingsAttributeDictionary();
                    UserScopedSettingAttribute attribute = new UserScopedSettingAttribute();
                    attributes.Add(attribute.GetType(), attribute);

                    Type defaultValueType = WpfHelper.GetDefaultValue(element, savedElements[element]);

                    SettingsProperty property = new SettingsProperty(rootElement.Name + "." + element.Name,
                       defaultValueType, providers["LocalFileSettingsProvider"], false, null, SettingsSerializeAs.String, attributes, true, true);
                    defaultSettings.Properties.Add(property);
                }
            }
            defaultSettings.Reload();
        }
    }
}
