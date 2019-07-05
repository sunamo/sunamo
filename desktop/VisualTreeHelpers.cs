﻿using sunamo;
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

    public static List<T> FindDescendents2<T>(DataGrid dtGrid)
    {
        var count = VisualTreeHelper.GetChildrenCount(dtGrid);
        // border
        var child = VisualTreeHelper.GetChild(dtGrid, 0);
        count = VisualTreeHelper.GetChildrenCount(child);

        // scrollviewer
        child = VisualTreeHelper.GetChild(child, 0);
        count = VisualTreeHelper.GetChildrenCount(child);

        // button
        child = VisualTreeHelper.GetChild(child, 0);
        //5
        count = VisualTreeHelper.GetChildrenCount(child);

        return null;
    }

    /// <summary>
    /// For checkbox and textbox return 0
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="parent"></param>
    /// <returns></returns>
    public static List<T> GetLogicalChildCollection<T>(object parent) where T : DependencyObject
    {
        List<T> logicalCollection = new List<T>();
        GetLogicalChildCollection(parent as DependencyObject, logicalCollection);
        return logicalCollection;
    }

    /// <summary>
    /// Working, as in EncodingOfFiles
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="depObj"></param>
    /// <returns></returns>
    public static T GetChildOfType<T>(DependencyObject depObj)
    where T : DependencyObject
    {
        if (depObj == null) return null;

        for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
        {
            var child = VisualTreeHelper.GetChild(depObj, i);

            var result = (child as T) ?? GetChildOfType<T>(child);
            if (result != null) return result;
        }
        return null;
    }

    private static void GetLogicalChildCollection<T>(DependencyObject parent, List<T> logicalCollection) where T : DependencyObject
    {
        // Here is error, DataGrid have no LOGICAL children
        var children = LogicalTreeHelper.GetChildren(parent);
        foreach (object child in children)
        {
            if (child is DependencyObject)
            {
                DependencyObject depChild = child as DependencyObject;
                if (child is T)
                {
                    logicalCollection.Add(child as T);
                }
                GetLogicalChildCollection(depChild, logicalCollection);
            }
        }
    }

    /// <summary>
    /// Working, as in EncodingOfFiles
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="depObj"></param>
    /// <returns></returns>
    public static IEnumerable<T> FindDescendents3<T>(DependencyObject depObj) where T : DependencyObject
    {
        if (depObj != null)
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(depObj, i);
                if (child != null && child is T)
                {
                    yield return (T)child;
                }

                foreach (T childOfChild in FindDescendents3<T>(child))
                {
                    yield return childOfChild;
                }
            }
        }
    }

    public static IEnumerable<T> FindDescendents<T>(DependencyObject depObj) where T : DependencyObject
    {
        List<T> t = new List<T>();
        return FindDescendents<T>(t, depObj);
    }

    /// <summary>
    /// Dojebané mnou, nepoužívat
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="vr"></param>
    /// <param name="depObj"></param>
    /// <returns></returns>
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

                var desc = FindDescendents<T>(null, child).ToList();
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