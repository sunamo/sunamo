using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace desktop.Storage
{
    public class SettingsManager
    {
        private ApplicationSettingsBase def;
        private SettingsProviderCollection providers;

        public SettingsManager(ApplicationSettingsBase def, SettingsProviderCollection providers)
        {
            this.def = def;
            this.providers = providers;
        }

            public  void LoadSettings(FrameworkElement sender, Dictionary<FrameworkElement, DependencyProperty> savedElements)
            {
                EnsureProperties(sender, savedElements);
                foreach (FrameworkElement element in savedElements.Keys)
                {
                    try
                    {
                        element.SetValue(savedElements[element], def[sender.Name + "." + element.Name]);
                    }
                    catch (Exception ex) { }
                }
            }

            public  void SaveSettings(FrameworkElement sender, Dictionary<FrameworkElement, DependencyProperty> savedElements)
            {
                EnsureProperties(sender, savedElements);
                foreach (FrameworkElement element in savedElements.Keys)
                {
                    def[sender.Name + "." + element.Name] = element.GetValue(savedElements[element]);
                }
                def.Save();
            }

            public  void EnsureProperties(FrameworkElement sender, Dictionary<FrameworkElement, DependencyProperty> savedElements)
            {
                foreach (FrameworkElement element in savedElements.Keys)
                {
                    bool hasProperty =
                        def.Properties[sender.Name + "." + element.Name] != null;

                    if (!hasProperty)
                    {
                        SettingsAttributeDictionary attributes = new SettingsAttributeDictionary();
                        UserScopedSettingAttribute attribute = new UserScopedSettingAttribute();
                        attributes.Add(attribute.GetType(), attribute);

                        SettingsProperty property = new SettingsProperty(sender.Name + "." + element.Name,
                            savedElements[element].DefaultMetadata.DefaultValue.GetType(), providers["LocalFileSettingsProvider"], false, null, SettingsSerializeAs.String, attributes, true, true);
                        def.Properties.Add(property);
                    }
                }
                def.Reload();
            }
        
    }
}
