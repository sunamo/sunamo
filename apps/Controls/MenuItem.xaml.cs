using apps.AwesomeFont;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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
    public sealed partial class MenuItem : UserControl
    {
        double widthOfIconControl = 50;
        double heightOfIconControl = 50;

        /// <summary>
        /// Pokud používáš tento ctor, musíš pak zavolat metodu Initialize
        /// </summary>
        public MenuItem()
        {
            this.InitializeComponent();

            Loaded += MenuItem_Loaded;
        }

        private void MenuItem_Loaded(object sender, RoutedEventArgs e)
        {
            double w1 = btn.ActualHeight;
            double w2 = gridButtonContent.ActualHeight;
        }

        public MenuItem(Brush fgIcon, string otfIcon, Brush fgText, string text) : this()
        {
            Initialize(fgIcon, otfIcon, fgText, text);
        }

        public async void Initialize(Brush fgIcon, string otfIcon, Brush fgText, string text)
        {
            txtIconBorder.Width = widthOfIconControl;
            txtIconBorder.Height = heightOfIconControl;
            
            gridButtonContent.Height = heightOfIconControl;

            await AwesomeFontControls.SetAwesomeFontSymbol(txtIcon, otfIcon, fgIcon, AwesomeFontControls.CalculateFontSize(widthOfIconControl, heightOfIconControl), text);

            txtText.Foreground = fgText;
            txtText.Text = " " + text;

            
        }

        public void SetAction(RoutedEventHandler reh, object tag)
        {
            btn.Click += reh;
            btn.Tag = tag;
        }

        public void SetAction(RoutedEventHandler reh)
        {
            btn.Click += reh;
        }
    }
}
