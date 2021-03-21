

using desktop.Controls.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

/// <summary>
/// Padding - chb,tb
/// Margin - txt,btn
/// </summary>
public partial class ResourceDictionaryStyles
{
    #region 10 for remembering default size
    public static double def = 10;
    public static void Padding10(IEnumerable<Control> p)
    {
        Padding(def, p);
    }

    /// <summary>
    /// TextBlock is not deriving from Control, has own Padding
    /// </summary>
    /// <param name="d"></param>
    /// <param name="p"></param>
    public static void Padding10(IEnumerable<TextBlock> p)
    {
        Padding(def, p);
    }

    public static void Margin10(IEnumerable<TextBox> p)
    {
        Margin(def, p);
    }

    public static void Margin10(IEnumerable<TextBlock> p)
    {
        Margin(def, p);
    }

    public static void Margin10(IEnumerable<PasswordBox> p)
    {
        Margin(def, p);
    }

    public static void Margin10(IEnumerable<Grid> p)
    {
        Margin(def, p);
    }

    public static void Margin10(IEnumerable<CheckBox> p)
    {
        Margin(def, p);
    }

    public static void Margin10(IEnumerable<Button> p)
    {
        Margin(def, p);
    }


    #endregion

    public static void Padding(double d, IEnumerable<Control> p)
    {
        foreach (var item in p)
        {
            item.Padding = new Thickness(d);
        }
    }

    public static void Margin(double d, IEnumerable<CheckBox> p)
    {
        foreach (var item in p)
        {
            item.Margin = new Thickness(d);
        }
    }

    public static void Margin(double d, IEnumerable<TextBlock> p)
    {
        foreach (var item in p)
        {
            item.Margin = new Thickness(d);
        }
    }

    public static void Margin(double d, IEnumerable<Grid> p)
    {
        foreach (var item in p)
        {
            item.Margin = new Thickness(d);
        }
    }



    /// <summary>
    /// TextBlock is not deriving from Control, has own Padding
    /// </summary>
    /// <param name="d"></param>
    /// <param name="p"></param>
    public static void Padding(double d, IEnumerable<TextBlock> p)
    {
        foreach (var item in p)
        {
            item.Padding = new Thickness(d);
        }
    }

    public static void Margin(double v, IEnumerable<PasswordBox> p)
    {
        foreach (var item in p)
        {
            item.Margin = new Thickness(v);
        }
    }



    public static void Margin(double v, IEnumerable<TextBox> p)
    {
        foreach (var item in p)
        {
            item.Margin = new Thickness(v);
        }
    }

    public static void Margin(double v, IEnumerable<Button> p)
    {
        foreach (var item in p)
        {
            item.Margin = new Thickness(v);
        }
    }

    public static void Margin(int v, IEnumerable<StackPanel> p)
    {
        foreach (var item in p)
        {
            item.Margin = new Thickness(v);
        }
    }

    public static void Margin(int v, IEnumerable<SelectManyFiles> p)
    {
        foreach (var item in p)
        {
            item.Margin = new Thickness(v);
        }
    }
}