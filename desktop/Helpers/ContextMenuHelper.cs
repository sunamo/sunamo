using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace desktop
{
    public class ContextMenuHelper
    {
        public static MenuItem GetMenuItemWithName(ContextMenu cm, string name)
        {
            int i = 0;
            while (true)
            {
                if (i == cm.Items.Count)
                {
                    break;
                }
                object o = cm.Items.GetItemAt(i);
                if (o is MenuItem)
                {
                    MenuItem vr = o as MenuItem;
                    if (vr.Name == name)
                    {
                        return vr;
                    }
                }
                i++;
            }
            return null;
        }

        public static ContextMenu FindContextMenu(DependencyObject depObj, DependencyProperty ContextMenuProperty)
        {
            ContextMenu cm = depObj.GetValue(ContextMenuProperty) as ContextMenu;
            if (cm != null)
                return cm;
            int children = VisualTreeHelper.GetChildrenCount(depObj);
            for (int i = 0; i < children; i++)
            {
                cm = FindContextMenu(VisualTreeHelper.GetChild(depObj, i), ContextMenuProperty);
                if (cm != null)
                    return cm;
            }
            return null;
        }

    }


}
