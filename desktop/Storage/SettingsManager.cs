using sunamo.Constants;
using sunamo.Data;
using sunamo.Essential;
using sunamo.Interfaces;
using sunamo.Values;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace desktop.Storage
{
    /// <summary>
    /// Never use Settings class.
    /// Work only with my own coded property, no way to use with List, Dictionary, Array atd. 
    /// Indexer of Settings public class is used only on MY OWN CODED property, if not declare, not working
    /// I spent on that two days. 
    /// Property was succesfully created but Settings.Defaut[PropertyName] on my own Settings public class indexer dont working.
    /// EVEN IF I ADD do def.PropertyValues - WpfStateSettingsWin is in def.PropertyValues and def.Properties - but still NullRefenceException
    /// I Also tried DebuggerNonUserCodeAttribute and DefaultSettingValueAttribute, without result
    /// 
    /// </summary>
    public class SettingsManager : ISettingsManager<FrameworkElement, DependencyProperty>
    {
        private ApplicationSettingsBase def;
        private SettingsProviderCollection providers;
        /// <summary>
        /// Name, defValue
        /// </summary>
        public Dictionary<string, object> customProperties = new Dictionary<string, object>();

        /// <summary>
        /// Pass Settings.Default, Settings.Default.Providers
        /// </summary>
        /// <param name="def"></param>
        /// <param name="providers"></param>
        public SettingsManager(ApplicationSettingsBase def, SettingsProviderCollection providers)
        {
            this.def = def;
            this.providers = providers;
        }

        public void LoadSettings(FrameworkElement sender, TUList<FrameworkElement, DependencyProperty> savedElements)
        {
            ////DebugLogger.Instance.WriteList(savedElements.Select(d => d.Key.GetType().FullName).ToList());
            EnsureProperties(sender, savedElements);
            foreach (var element in savedElements)
            {
                try
                {
                    //if (sender.Name != element.Key.Name)
                    if (element.Key is Window)
                    {

                    }
                    else
                    {
                        //element.Key.SetValue(element.Value, def[sender.Name + AllStrings.dot + element.Key.Name]);
                        object value = null;
                        try
                        {
                            value = def[GetElementFullPath(element.Key, sender)];
                        }
                        catch (SettingsPropertyNotFoundException)
                        {

                            
                        }
                        catch(NullReferenceException)
                        {

                        }
                        if (value != null)
                        {
                            element.Key.SetValue(element.Value, value);
                        }
                    }
                }
                catch (Exception ex) { }
            }
        }

        /// <summary>
        /// Should be without . bue to is Property name
        /// </summary>
        /// <param name="fw"></param>
        /// <param name="sender"></param>
        private string GetElementFullPath(FrameworkElement fw, FrameworkElement sender)
        {
            if (false)
            {
                return sender.Name + AllStrings.dot + fw.GetType().FullName;
            }
            StringBuilder sb = new StringBuilder();
            sb.Append(fw.Name);
            var p = fw.Parent as FrameworkElement;
            while (p != null)
            {
                // Must be underscore because its PropertyName
                sb.Insert(0, p.Name + AllStrings.lowbar);
                p = p.Parent as FrameworkElement; ;
            }
            var result = sb.ToString();
            return result;
        }

        public void SaveSettings(FrameworkElement sender, TUList<FrameworkElement, DependencyProperty> savedElements)
        {
            EnsureProperties(sender, savedElements);
            foreach (var element in savedElements)
            {
                if (element.Key is Window)
                {

                }
                else
                {
                    if (element.Key is TextBox)
                    {

                    }
                    object value = null;
                    value = element.Key.GetValue(element.Value);
                    try
                    {
                        var elementName = GetElementFullPath(element.Key, sender);
                        def[elementName] = value;
                    }
                    catch (SettingsPropertyNotFoundException)
                    {

                        
                    }
                    catch (NullReferenceException)
                    {

                    }
                    //def[sender.Name + AllStrings.dot + element.Key.Name] = value;

                }
            }
            def.Save();
        }

        /// <summary>
        /// Add dynamically properties to code to avoid HardCoded this
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="savedElements"></param>
        public void EnsureProperties(FrameworkElement sender, TUList<FrameworkElement, DependencyProperty> savedElements)
        {
            foreach (var item in customProperties)
            {
                EnsureProperty(item.Value, item.Key);
            }
            foreach (var element in savedElements)
            {
                var defValue = element.Value.DefaultMetadata.DefaultValue;
                var propertyName = GetElementFullPath(element.Key, sender);
                EnsureProperty(defValue, propertyName);
            }
            def.Reload();
        }

        /// <summary>
        /// A1 can be null
        /// </summary>
        /// <param name="key"></param>
        /// <param name="defValueForDependencyProperty"></param>
        /// <param name="propertyName"></param>
        public void EnsureProperty(object defValueForDependencyProperty, string propertyName)
        {
            bool hasProperty = def.Properties[propertyName] != null;
            bool hasPropertyValue = def.Properties[propertyName] != null;

            if (!hasProperty || !hasPropertyValue)
            {
                SettingsAttributeDictionary attributes = new SettingsAttributeDictionary();
                UserScopedSettingAttribute attribute = new UserScopedSettingAttribute();
                attributes.Add(attribute.GetType(), attribute);

                SettingsProperty property = new SettingsProperty(propertyName, //name
                    defValueForDependencyProperty == null ? Types.tObject : defValueForDependencyProperty.GetType(), //propertyType
                    providers["LocalFileSettingsProvider"],
                    false, //isReadOnly
                    null, // defaultValue
                    SettingsSerializeAs.Binary, // Binary is universal
                    attributes,
                    true, // throwOnErrorSerializing
                    true); // throwOnErrorSerializing

                if (!hasProperty)
                {
                    // Its SettingsProperty[string,int]
                    def.Properties.Add(property);
                }

                if (!hasPropertyValue)
                {
                    SettingsPropertyValue propertyValue = new SettingsPropertyValue(property);
                    propertyValue.PropertyValue = defValueForDependencyProperty;
                    def.PropertyValues.Add(propertyValue);
                }
            }

        }

        /// <summary>
        /// Must be here due to ISettingsManager
        /// </summary>
        /// <param name="list"></param>
        public void AddFromSavedElements(TUList<FrameworkElement, DependencyProperty> list)
        {
            
        }
    }
}