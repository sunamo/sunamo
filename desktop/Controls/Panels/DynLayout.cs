using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

public class DynLayout
{
    Grid gridGrowable = null;

    public DynLayout(Grid g)
    {
        gridGrowable = g;
    }

    public List<FrameworkElement> fwElements = new List<FrameworkElement>();

    public object GetContentByTag(object tag)
    {
        foreach (var item in fwElements)
        {
            if (item.Tag == tag)
            {
                return item.GetContent();
            }
        }
        return null;
    }

    public object this[int i]
    {
        get
        {
            return fwElements[i].GetContent();
        }
    }

    Thickness uit = new Thickness(10, 5, 10, 5);

    /// <summary>
    /// Example and best case use is in Wpf.Tests
    /// </summary>
    /// <param name="row"></param>
    /// <param name="name"></param>
    /// <param name="ui"></param>
    public void AddControl(int row, string name, FrameworkElement ui)
    {
        Grid.SetRow(ui, row);
        Grid.SetColumn(ui, 1);
        // Horizontal alignment cant be set here - otherwise won't be horizontally stretched
        //ui.HorizontalAlignment = HorizontalAlignment.Left;
        ui.Margin = uit;
        // double.NaN to fill all available width
        // HorizontalAligment have no effect
        ui.Width = double.NaN;
        gridGrowable.Children.Add(ui);

        if (name != null)
        {
            AddLabel(row, name);
        }

        fwElements.Add(ui);
    }

    public void AddLabel(int row, string name)
    {
        var tb = TextBlockHelper.Get(new ControlInitData { text = name });
        
        AddLabel(row, tb);
    }

    public void AddLabel(int row, UIElement tb)
    {
        if (tb is TextBlock)
        {
            var tbb = (TextBlock)tb;
            tbb.HorizontalAlignment = HorizontalAlignment.Right;
            tbb.VerticalAlignment = VerticalAlignment.Center;
            tbb.Margin = uit;
        }

        Grid.SetRow(tb, row);
        Grid.SetColumn(tb, 0);
        gridGrowable.Children.Add(tb);
    }
}