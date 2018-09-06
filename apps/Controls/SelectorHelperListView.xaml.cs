using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Windows.Input;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace apps
{
    /// <summary>
    /// ListView který spolupracuje se třídou SelectorView
    /// </summary>
    public sealed partial class SelectorHelperListView : UserControl
    {
        public SelectorHelperListView()
        {
            this.InitializeComponent();
        }

        public object SelectedItem { get; set; }

    }

    
}
