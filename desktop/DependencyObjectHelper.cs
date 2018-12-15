using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Markup.Primitives;

namespace desktop
{
    public static class DependencyObjectHelper
    {
        public static object MarkupWriter { get; private set; }

        public static List<DependencyProperty> GetDependencyProperties(Object element)
        {
            List<DependencyProperty> properties = new List<DependencyProperty>();
            MarkupObject markupObject = System.Windows.Markup.Primitives.MarkupWriter.GetMarkupObjectFor(element);
            if (markupObject != null)
            {
                foreach (MarkupProperty mp in markupObject.Properties)
                {
                    if (mp.DependencyProperty != null)
                    {
                        properties.Add(mp.DependencyProperty);
                    }
                }
            }

            return properties;
        }

        /// <summary>
        /// Return only real atteched in App
        /// Subtype of dependency property
        /// It's property as Grid.Row etc.
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public static List<DependencyProperty> GetAttachedProperties(Object element)
        {
            List<DependencyProperty> attachedProperties = new List<DependencyProperty>();
            MarkupObject markupObject = System.Windows.Markup.Primitives.MarkupWriter.GetMarkupObjectFor(element);
            if (markupObject != null)
            {
                foreach (MarkupProperty mp in markupObject.Properties)
                {
                    if (mp.IsAttached)
                    {
                        attachedProperties.Add(mp.DependencyProperty);
                    }
                }
            }

            return attachedProperties;
        }

        public static T CreatedWithCopiedValues<T>(T t, params DependencyProperty[] p) where T : DependencyObject
        {
            T instance = Activator.CreateInstance<T>();
            foreach (var item in p)
            {
                var value = t.GetValue(item);
                instance.SetValue(item, value);
            }
            return instance;
        }
    }
}
