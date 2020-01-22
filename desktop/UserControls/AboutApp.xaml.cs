using sunamo.Essential;
using System.Windows;
using System.Windows.Controls;

namespace desktop.Controls
{
    public sealed partial class AboutApp : UserControl, IControlWithResult //, IPopupResponsive, IPopupEvents<object>
    {
        public event VoidT<object> ClickOK;
        public event VoidT<object> ClickCancel;
        public event VoidBoolNullable ChangeDialogResult;

        public void FocusOnMainElement()
        {
            btnOk.Focus();
        }

        public AboutApp(Langs l)
        {
            this.InitializeComponent();

            

                tbTitle.Text = RL.GetString("AboutApp") + AllStrings.space  + ThisApp.Name;
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

        public bool? DialogResult { set => throw new System.NotImplementedException(); }

        private void OnClickOK(object sender, RoutedEventArgs e)
        {
           
            ClickOK(null);
        }

        public void Accept(object input)
        {
            throw new System.NotImplementedException();
        }
    }
}
