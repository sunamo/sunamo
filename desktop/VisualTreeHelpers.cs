using sunamo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

public class VisualTreeHelpers
{
    /// <summary>
    /// Returns the first ancester of specified type
    /// </summary>
    public static T FindAncestor<T>(DependencyObject current)
    where T : DependencyObject
    {
        current = VisualTreeHelper.GetParent(current);

        while (current != null)
        {
            if (current is T)
            {
                return (T)current;
            }
            current = VisualTreeHelper.GetParent(current);
        };
        return null;
    }

    /// <summary>
    /// Returns a specific ancester of an object
    /// </summary>
    public static T FindAncestor<T>(DependencyObject current, T lookupItem)
    where T : DependencyObject
    {
        while (current != null)
        {
            if (current is T && current == lookupItem)
            {
                return (T)current;
            }
            current = VisualTreeHelper.GetParent(current);
        };
        return null;
    }

    /// <summary>
    /// Finds an ancestor object by name and type
    /// </summary>
    public static T FindAncestor<T>(DependencyObject current, string parentName)
    where T : DependencyObject
    {
        while (current != null)
        {
            if (!string.IsNullOrEmpty(parentName))
            {
                var frameworkElement = current as FrameworkElement;
                if (current is T && frameworkElement != null && frameworkElement.Name == parentName)
                {
                    return (T)current;
                }
            }
            else if (current is T)
            {
                return (T)current;
            }
            current = VisualTreeHelper.GetParent(current);
        };

        return null;

    }

    /// <summary>
    /// Is used nowhere in code
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="dtGrid"></param>
    
    static IEnumerable<T> FindDescendents<T>(List<T> vr, DependencyObject depObj) where T : DependencyObject
    {
        if (depObj != null)
        {
            var count = VisualTreeHelper.GetChildrenCount(depObj);
            for (int i = 0; i < count; i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(depObj, i);
                if (vr != null)
                {
                    if (child != null && RH.IsOrIsDeriveFromBaseClass(child.GetType(), typeof(T)))
                    {
                        vr.Add((T)child);
                    }
                }

                var desc = FindDescendents<T>(child).ToList();
                //for (int y = desc.Count() - 1; y >= 0; y--)
                //{
                //    vr.Add(desc[i]);
                //}
                if (vr != null)
                {
                    foreach (T childOfChild in desc)
                    {
                        vr.Add(childOfChild);
                    }
                }
            }
        }
        return vr;
    }
}
