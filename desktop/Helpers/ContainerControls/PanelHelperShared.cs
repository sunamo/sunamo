using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Threading;

public partial class PanelHelper
{
    public static UIElementCollection Children(StackPanel key, Dispatcher d)
    {
        //WpfApp.cd

        var r = d.Invoke(() => key.Children, DispatcherPriority.ContextIdle);
        return r;

    }
}