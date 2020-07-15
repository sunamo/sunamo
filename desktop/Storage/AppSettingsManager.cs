using sunamo.Data;
using sunamo.Essential;
using sunamo.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace desktop.Storage
{
    public class AppSettingsManager
    {
        /// <summary>
        /// Is filled in ctor MainWindow
        /// </summary>
        public static ISettingsManager<FrameworkElement, DependencyProperty> SettingsManager = null;

        static Dictionary<FrameworkElement, TUList<FrameworkElement, DependencyProperty>> savedElement = new Dictionary<FrameworkElement, TUList<FrameworkElement, DependencyProperty>>();

        public static void AddChildrenFrom(FrameworkElement fe)
        {
            if (fe is Panel)
            {

                Panel panel = fe as Panel;
                //The settings property 'sp.System.Windows.Controls.StackPanel' is of a non-compatible type.'
                //AddToSavedElements(panel);
                foreach (FrameworkElement item in panel.Children)
                {
                    AddChildrenFrom(item);
                }
            }
            else
            {
                AddToSavedElements(fe);

                if (fe is Window)
                {
                    Window panel = fe as Window;
                    AddChildrenFrom(panel.Content as FrameworkElement);
                }
            }
        }

        private static void AddToSavedElements(FrameworkElement fe)
        {
            TUList<FrameworkElement, DependencyProperty> list = new TUList<FrameworkElement, DependencyProperty>();

            // U TextBox mi to vrátilo 2, ačkoliv má jich mnohem vic i bez base class
            var depencies = DependencyObjectHelper.GetDependencyProperties(fe);
            var attached = DependencyObjectHelper.GetAttachedProperties(fe);

            if (fe is TextBox)
            {

            }

            foreach (var item in depencies)
            {
                list.Add(TU<FrameworkElement, DependencyProperty>.Get(fe, item));
            }

            foreach (var item in attached)
            {
                list.Add(TU<FrameworkElement, DependencyProperty>.Get(fe, item));
            }

            savedElement.Add(fe, list);
        }

        public static void LoadSettings()
        {
            
            foreach (var item in savedElement)
            {
                SettingsManager.AddFromSavedElements(item.Value);
                SettingsManager.LoadSettings(item.Key, item.Value);
            }
        }

        

        public static void SaveSettings()
        {
            foreach (var item in savedElement)
            {
                if (item.Key is TextBox)
                {

                }
                SettingsManager.SaveSettings(item.Key, item.Value);
            }
        }
    }
}