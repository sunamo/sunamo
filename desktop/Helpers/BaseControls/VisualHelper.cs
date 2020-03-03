using desktop.WindowsSettings;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace desktop
{
    public class VisualHelper
    {
        /// <summary>
        /// A1 je Control jen proto abych mohl zjistit DPI, pokud bys ho zjistil z jiného controlu můžeš do A1 použít UIElement
        /// </summary>
        /// <param name="uiElement"></param>
        /// <param name="resolution"></param>
        
        public static Rect BoundsRelativeTo( FrameworkElement element, Visual relativeTo)
        {
            return
              element.TransformToVisual(relativeTo)
                     .TransformBounds(LayoutInformation.GetLayoutSlot(element));
        }

        static DrawingVisual ModifyToDrawingVisual(Visual v)
        {
            Rect b = VisualTreeHelper.GetDescendantBounds(v);
            /// new a drawing visual and get its context
            DrawingVisual dv = new DrawingVisual();
            DrawingContext dc = dv.RenderOpen();

            /// generate a visual brush by input, and paint
            VisualBrush vb = new VisualBrush(v);
            dc.DrawRectangle(vb, null, b);
            dc.Close();

            return dv;
        }
    }
}
