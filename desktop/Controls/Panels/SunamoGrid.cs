using desktop.Controls.Visualization;
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
        public static readonly DependencyProperty PropertyNameProperty = DependencyProperty.Register(
            "DisplayEntity", typeof(string), typeof(TwoWayTable), new FrameworkPropertyMetadata(string.Empty, FrameworkPropertyMetadataOptions.AffectsRender));

        DrawingGroup backingStore = new DrawingGroup();
        DrawingContext dc = null;

        protected override void OnRender(DrawingContext dc)
        {
            base.OnRender(dc);

            this.dc = dc;
            ForceRender();
            dc.DrawDrawing(backingStore);
        }

        public void ForceRender()
        {
            // Problem there was in ReviewAspxPagesUC that sometimes OnRender in SunamoGrid is not called and painted lines keep on old coords

            //var drawingContext = backingStore.Open();
            //base.Render(drawingContext);
            //drawingContext.Close();

            //double leftOffset = 0;
            //double topOffset = 0;
            //Pen pen = new Pen(Brushes.Gray, 1);
            //pen.Freeze();

            //foreach (RowDefinition row in this.RowDefinitions)
            //{
            //    dc.DrawLine(pen, new Point(0, topOffset), new Point(this.ActualWidth, topOffset));
            //    topOffset += row.ActualHeight;
            //}
            //// draw last line at the bottom
            //dc.DrawLine(pen, new Point(0, topOffset), new Point(this.ActualWidth, topOffset));

            //foreach (ColumnDefinition column in this.ColumnDefinitions)
            //{
            //    dc.DrawLine(pen, new Point(leftOffset, 0), new Point(leftOffset, this.ActualHeight));
            //    leftOffset += column.ActualWidth;
            //}
            //// draw last line on the right
            //dc.DrawLine(pen, new Point(leftOffset, 0), new Point(leftOffset, this.ActualHeight));
        }
    }
}