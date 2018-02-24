using sunamo.Helpers.Number;
using System.Windows;
using System.Windows.Controls;
namespace desktop
{               
    public class ProgressBarHelper : Window
    {
        ProgressBar pb = null;
        PercentCalculator percentCalculator;
        UIElement ui = null;
        IH ih = null;

        public ProgressBarHelper(ProgressBar pb, double overall, UIElement ui)
        {
            this.ih = ih;
            this.pb = pb;
            this.ui = ui;
            ui.Dispatcher.Invoke(IH.delegateUpdateProgressBarWpf, pb, 0d);
            ui.Dispatcher.Invoke(IH.delegateChangeVisibilityUIElementWpf, pb, System.Windows.Visibility.Visible);
            percentCalculator = new PercentCalculator(overall);
        }

        public void Done()
        {
            ui.Dispatcher.Invoke(IH.delegateUpdateProgressBarWpf, pb, 100d);
            ui.Dispatcher.Invoke(IH.delegateChangeVisibilityUIElementWpf, pb, System.Windows.Visibility.Collapsed);
        }

        public void DonePartially()
        {
            percentCalculator.last += percentCalculator.onePercent;
            ui.Dispatcher.Invoke(IH.delegateUpdateProgressBarWpf, pb, percentCalculator.last);
        }
    }
}
