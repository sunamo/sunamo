using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace desktop
{
    public class TextBoxWithLabel : UserControl
    {
        ColumnDefinition cdLabel = null;
        public Label lbl = new Label();
        public TextBox txt = new TextBox();

        public double PxLabelColumn
        {
            get => cdLabel.ActualWidth;
            set => cdLabel.Width = new GridLength(value, GridUnitType.Pixel);
        }

        public TextBoxWithLabel()
        {
            Grid g = new Grid();
            g.RowDefinitions.Add(GridHelper.GetRowDefinition(GridLength.Auto));

            cdLabel = GridHelper.GetColumnDefinition(GridLength.Auto);
            g.ColumnDefinitions.Add(cdLabel);
            g.ColumnDefinitions.Add(GridHelper.GetColumnDefinition(GridLength.Auto));

            g.Children.Add(lbl);
            g.Children.Add(txt);
            Grid.SetColumn(txt, 1);

            Content = g;
        }
    }
}