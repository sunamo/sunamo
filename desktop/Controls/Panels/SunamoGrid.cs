using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace desktop.Controls.Panels
{
    public class SunamoGrid : Grid
    {
        protected override void OnRender(DrawingContext dc)
        {
            double leftOffset = 0;
            double topOffset = 0;
            Pen pen = new Pen(Brushes.Black, 3);
            pen.Freeze();

            foreach (RowDefinition row in this.RowDefinitions)
            {
                dc.DrawLine(pen, new Point(0, topOffset), new Point(this.ActualWidth, topOffset));
                topOffset += row.ActualHeight;
            }
            // draw last line at the bottom
            dc.DrawLine(pen, new Point(0, topOffset), new Point(this.ActualWidth, topOffset));

            //foreach (ColumnDefinition column in this.ColumnDefinitions)
            //{
            //   dc.DrawLine(pen, new Point(leftOffset, 0), new Point(leftOffset, this.ActualHeight));
            //   leftOffset += column.ActualWidth;
            //}
            // draw last line on the right
            //dc.DrawLine(pen, new Point(leftOffset, 0), new Point(leftOffset, this.ActualHeight));

            base.OnRender(dc);
        }
    }
}