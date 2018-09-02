using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using sunamo.Enums;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace apps.Popups
{
    public sealed partial class YesNoDialog : UserControl, IPopupResponsive, IPopupEvents<YesNoDialogEventArgs>
    {
        Langs l = apps.Essential.ThisApp.l;
        object arg = null;

        /// <summary>
        /// Polož otázku A1 tak aby bylo na ní odpovědět Ano/Ne, např. Přejete si otevřít odkaz v novém okně?
        /// </summary>
        /// <param name="prompt"></param>
        public YesNoDialog(string prompt, object arg)
        {
            this.InitializeComponent();

            this.arg = arg;
           
                btnYes.Content = RL.GetString( "Yes");
                btnNo.Content = RL.GetString( "No");
            

            tbPromptMessage.Text = prompt;
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

        public Brush PopupBorderBrush
        {
            set
            {
                border.BorderBrush = value;
            }
        }

        public event VoidT<YesNoDialogEventArgs> ClickCancel;
        public event VoidT<YesNoDialogEventArgs> ClickOK;

        public void ApplyColorTheme(ColorTheme ct)
        {
            ColorThemeHelper.ApplyColorTheme(border, ct);
        }

        private void btnYes_Click(object sender, RoutedEventArgs e)
        {
            ClickOK(new YesNoDialogEventArgs { Arg = arg });
        }

        private void btnNo_Click(object sender, RoutedEventArgs e)
        {
            ClickCancel(new YesNoDialogEventArgs { Arg = arg });
        }
    }
}
