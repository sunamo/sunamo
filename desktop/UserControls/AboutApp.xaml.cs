using sunamo.Essential;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace desktop.Controls
{
    public sealed partial class AboutApp : UserControl, IUserControl, IControlWithResult, IKeysHandler //, IPopupResponsive, IPopupEvents<object>
    {
        //public event VoidT<object> ClickOK;
        //public event VoidT<object> ClickCancel;
        public event VoidBoolNullable ChangeDialogResult;

        public void FocusOnMainElement()
        {
            //btnOk.Focus();
        }

        public AboutApp()
        {
            this.InitializeComponent();

            tbTitle.Text = sess.i18n("AboutApp") + AllStrings.space  + ThisApp.Name;

            System.Reflection.Assembly assembly = System.Reflection.Assembly.GetEntryAssembly();
            System.Diagnostics.FileVersionInfo fvi = System.Diagnostics.FileVersionInfo.GetVersionInfo(assembly.Location);

            tbAboutApp.Text = sess.i18n(XlfKeys.Version) + ": " + fvi.FileVersion;

            

            //WRTBH tbh2 = new WRTBH(475, 10, FontArgs.DefaultRun());
            ParagraphBuilderTextBlock tbh2 = new ParagraphBuilderTextBlock();
            // padding / margin top / bottom not working, therefore create new line will
            tbh2.Hyperlink(sess.i18n("CzechBlog"), "http://jepsano.net");
            tbh2.LineBreak();
            tbh2.LineBreak();
            tbh2.Hyperlink(sess.i18n("EnglishBlog"), "http://blog.sunamo.cz");
            tbh2.LineBreak();
            tbh2.LineBreak();
            tbh2.Hyperlink("Web", "http://www.sunamo.cz");
            tbh2.LineBreak();
            tbh2.LineBreak();
            tbh2.Hyperlink("Google+", "https://plus.google.com/111524962367375368826");
            tbh2.LineBreak();
            tbh2.LineBreak();
            tbh2.Hyperlink("Mail: sunamo@outlook.com", "mailto:radek.jancik@sunamo.cz");
            tbh2.LineBreak();
            tbh2.LineBreak();

            tbh2.margin = new Thickness(25, 0, 0, 0);
            tbh2.padding = new Thickness(0, 0, 0, 0);
            var sp = tbh2.Final();
            Grid.SetRow(sp, 4);
            grid.Children.Add(sp);

            //wg.DataContext = tbh2.uis;
            //var itemsPanel = wg.ItemsPanel;
            ////var ipt = itemsPanel.te
            //var d = wg;
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

        public bool? DialogResult { set => RuntimeHelper.EmptyDummyMethod(); }

        public string Title => sess.i18n(XlfKeys.AboutApp);

        private void OnClickOK(object sender, RoutedEventArgs e)
        {
            ChangeDialogResult(true);
        }

        public void Accept(object input)
        {
            
        }

        public void Init()
        {
            
        }

        public bool HandleKey(KeyEventArgs e)
        {
            return false;
        }

        private void btnCheckNewVersion_Click(object sender, RoutedEventArgs e)
        {
            //HttpRequestHelper.GetResponseText();
        }
    }
}