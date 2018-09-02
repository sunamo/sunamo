using apps;
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

namespace UniversalWebControl
{
    public sealed partial class OpenInControl : UserControl
    {
        public OpenInControl()
        {
            this.InitializeComponent();

           
                rbNever.Content = RL.GetString( "Never");
                rbAlways.Content = RL.GetString( "Always");
                rbPrompt.Content = RL.GetString( "Prompt");


            OpenIn = OpenInNewTab.Never;
        }
        public OpenInNewTab openIn = OpenInNewTab.Always;

        

        public OpenInNewTab OpenIn {
            get
            {
                return openIn;
            }
            set
            {
                switch (value)
                {
                    case OpenInNewTab.Never:
                        rbNever.IsChecked = true;
                        break;
                    case OpenInNewTab.Always:
                        rbAlways.IsChecked = true;
                        break;
                    case OpenInNewTab.Prompt:
                        rbPrompt.IsChecked = true;
                        break;
                    default:
                        throw new NotImplementedException("OpenInControl.OpenIn");
                        break;
                }
                openIn = value;

            }
        }

        private void rbNever_Click(object sender, RoutedEventArgs e)
        {
            openIn = OpenInNewTab.Never;
        }

        private void rbAlways_Click(object sender, RoutedEventArgs e)
        {
            openIn = OpenInNewTab.Always;
        }

        private void rbPrompt_Click(object sender, RoutedEventArgs e)
        {
            openIn = OpenInNewTab.Prompt;
        }
    }


}
