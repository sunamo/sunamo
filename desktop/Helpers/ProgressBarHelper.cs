using System.Windows;
using System.Windows.Controls;
namespace desktop
{
    public class ProgressBarHelper : Window
    {
        ProgressBar pb = null;

        double onePercent = 0;
        double last = 0;
        UIElement ui = null;
        IH ih = null;

        public ProgressBarHelper(ProgressBar pb, double overall, UIElement ui)
        {
            this.ih = ih;
            this.pb = pb;
            this.ui = ui;
            ui.Dispatcher.Invoke(IH.delegateUpdateProgressBarWpf, pb, 0d);
            ui.Dispatcher.Invoke(IH.delegateChangeVisibilityUIElementWpf, pb, System.Windows.Visibility.Visible);
            onePercent = 100 / overall;
        }

        public void Done()
        {
            ui.Dispatcher.Invoke(IH.delegateUpdateProgressBarWpf, pb, 100d);
            ui.Dispatcher.Invoke(IH.delegateChangeVisibilityUIElementWpf, pb, System.Windows.Visibility.Collapsed);
        }

        public void DonePartially()
        {
            last += onePercent;
            ui.Dispatcher.Invoke(IH.delegateUpdateProgressBarWpf, pb, last);
        }
    }
}
