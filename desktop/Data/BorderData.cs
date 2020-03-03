using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Media;

namespace sunamo.Data
{
    public class BorderData
    {
        public Thickness BorderThickness;
        public Brush BorderBrush;

        public BorderData(Brush borderBrush, int borderThickness)
        {
            BorderThickness = new Thickness( borderThickness);
            BorderBrush = borderBrush;
        }

        public BorderData(Brush borderBrush, Thickness borderThickness)
        {
            BorderThickness = borderThickness;
            BorderBrush = borderBrush;
        }

        public static Thickness Thickness(double d)
        {
            return new Thickness(d);
        }

        public static readonly BorderData None = new BorderData(Brushes.Beige, 0);
        public static readonly BorderData Black1 = new BorderData(Brushes.Black, Thickness(1));
    }
}