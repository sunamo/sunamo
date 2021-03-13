using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using desktop.Controls;
using sunamo;

public partial class PanelHelper
{
    public static List<UIElement> GetThisAndRecursiveAllSubUIElements(UIElement f)
    {
        List<UIElement> vr = new List<UIElement>();
        vr.Add(f);

        foreach (UIElement item in Childrens(f))
        {
            GetThisAndRecursiveAllSubUIElements(f, vr);
        }
        return vr;
    }

    /// <summary>
    /// because every of structure is other innered, is stupidity have own method for get content control without closer determination
    /// </summary>
    /// <param name="p"></param>
    public static object ContentOfFirstChild(Panel p)
    {
        //var first = p.Children.FirstOrNull();
        //if (first == null)
        //{
        //    return null;
        //}
        //var c = VisualTreeHelpers.FindDescendents<ContentControl>(p);
        return null;
    }

    private static IList Childrens(UIElement maybePanel)
    {
        if (maybePanel != null)
        {
            if (maybePanel is ContentControl)
            {
                ContentControl c = (ContentControl)maybePanel;
                // Will check for Panel
                return Childrens(c.Content as UIElement);
            }

            else if (maybePanel is Panel)
            {
                return ((Panel)maybePanel).Children;
            }
        }

        return new System.Collections.Generic.List<object>();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="f"></param>
    /// <param name="vr"></param>
    private static void GetThisAndRecursiveAllSubUIElements(UIElement f, List<UIElement> vr)
    {
        vr.Add(f);


        foreach (UIElement item in Childrens(f))
        {
            GetThisAndRecursiveAllSubUIElements(item, vr);
        }
    }

    private static void GetThisAndRecursiveAllSubUIElements<T>(UIElement f, List<T> vr) where T : class
    {
        // Cant compare with ==, but check for parent classes. 
        // In most cases I will search for UIElement, Control etc. and nothing will found
        if (RH.IsOrIsDeriveFromBaseClass(f.GetType(), typeof(T)))
        {
            vr.Add(f as T);
        }

        foreach (UIElement item in Childrens(f))
        {
            GetThisAndRecursiveAllSubUIElements(item, vr);
        }
    }

    public static List<T> GetRecursiveAllSubUIElementsOfType<T>(UIElement control) where T : class
    {
        List<T> vr = new List<T>();
        foreach (UIElement item in Childrens(control))
        {
            GetThisAndRecursiveAllSubUIElements<T>(item, vr);
        }
        return vr;
    }

   
}