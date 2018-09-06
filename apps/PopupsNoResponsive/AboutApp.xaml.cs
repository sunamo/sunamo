using sunamo;
using sunamo.Enums;
using sunamo.Essential;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace apps.PopupsNoResponsive
{
    public sealed partial class AboutApp : UserControl, IPopupSmall
    {
        public event RoutedEventHandler ClickOK;

        public void ApplyColorTheme(ColorTheme ct)
        {
            ColorThemeHelper.ApplyColorTheme(border, ct);
        }

        public AboutApp(Langs l)
        {
            this.InitializeComponent();
            

            TBH tbh = new TBH(tbAboutApp);
            WRTBH tbh2 = new WRTBH(475, 10, FontArgs.DefaultRun());
            if (l == Langs.cs)
            {
                tbTitle.Text = "O aplikaci " + ThisApp.Name;
                tbh.Run("Tato aplikace byla vytvořena ve vývojovém prostředí "); //sunamo
                tbh.Bold("Visual Studio Express 2012 for Windows 8");
                tbh.Run(", které je zcela zdarma. Aplikaci vyvinula osoba, která na internetu vystupuje pod přezdívkou ");
                tbh.Bold("sunamo");
                tbh.Run(".");

                tbh2.HyperLink("Blog autora", "http://sunamoblog.blogspot.com");
                tbh2.HyperLink("Web autora", "http://www.sunamo.net");
                tbh2.HyperLink("Mail autora: sunamocz@outlook.com", "mailto:sunamocz@outlook.com");
            }
            else
            {
                tbTitle.Text = "About app " + ThisApp.Name;
                tbh.Run("This application was created in the development environment ");
                tbh.Bold("Visual Studio Express 2012 for Windows 8");
                tbh.Run(", which is entirely free of charge. The person who has made this app is on internet known under nick ");
                tbh.Bold("sunamo");
                tbh.Run(".");

                tbh2.HyperLink("Blog of author", "http://sunamoblog.blogspot.com");
                tbh2.HyperLink("Web of author", "http://www.sunamo.net");
                tbh2.HyperLink("Mail of author: sunamocz@outlook.com", "mailto:sunamocz@outlook.com");
            }
            

            

            
            wg.DataContext = tbh2.uis;

        }

        private void OnClickOK(object sender, RoutedEventArgs e)
        {
            ClickOK(sender, e);
        }

        public Brush PopupBorderBrush
        {
            set { border.BorderBrush = value; }
        }


        public event RoutedEventHandler ClickCancel;
    }
}
