
using System.Windows;
using System.Windows.Controls;
namespace desktop
{
    public class FrugalContainer : Decorator
    {
        public FrugalContainer() : base()
        {

        }

        protected override Size MeasureOverride(Size availableSize)
        {
            return new Size(0, 0);
        }

        protected override Size ArrangeOverride(Size arrangeSize)
        {
            // get it all
            Child.Measure(arrangeSize);
            Child.Arrange(new Rect(arrangeSize));
            return Child.RenderSize;
        }
    }
}
