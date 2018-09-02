using sunamo;
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
using sunamo.Enums;
using apps;
using apps.Helpers;
using Windows.UI.Popups;
using Windows.ApplicationModel.Resources;
using sunamo.Essential;

namespace apps.Popups
{
    public sealed partial class AboutApp : UserControl, IPopupResponsive, IPopupEvents<object>
    {
        

        public event VoidT<object> ClickOK;

        public AboutApp()
        {
            this.InitializeComponent();

                tbTitle.Text = RL.GetString("AboutApp") + " "  + ThisApp.Name;
            string ad = RL.GetString("AboutDeveloper");
            tbAboutApp.Text = ad;

            WRTBH tbh2 = new WRTBH(475, 10, FontArgs.DefaultRun());
            tbh2.HyperLink(RL.GetString("CzechBlog"), "http://jepsano.net");
            tbh2.LineBreak();
            tbh2.HyperLink(RL.GetString("EnglishBlog"), "http://for-you-the.best");
            tbh2.LineBreak();
            tbh2.HyperLink("Web", "http://www.sunamo.cz");
            tbh2.LineBreak();
            tbh2.HyperLink("Google+", "https://plus.google.com/111524962367375368826");
            tbh2.LineBreak();
            tbh2.HyperLink("Mail: sunamo@outlook.com", "mailto:sunamo@outlook.com");
            tbh2.LineBreak();

            wg.DataContext = tbh2.uis;

        }

        public Size MaxContentSize
        {
            get
            {
                //return maxContentSize;
                return FrameworkElementHelper.GetMaxContentSize(this);
            }
            set
            {
                //maxContentSize = value;
                FrameworkElementHelper.SetMaxContentSize(this, value);
            }
        }

        private void OnClickOK(object sender, RoutedEventArgs e)
        {
           
            ClickOK(null);
        }

        public void ApplyColorTheme(ColorTheme ct)
        {
            ColorThemeHelper.ApplyColorTheme(border, ct);
        }

        public Brush PopupBorderBrush
        {
            set { border.BorderBrush = value; }
        }


        public event VoidT<object> ClickCancel;
    }
}
