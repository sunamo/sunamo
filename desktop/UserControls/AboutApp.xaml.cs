using sunamo.Essential;
using System;
using System.Diagnostics;
using System.Net.Http;
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
        string updateUri = null;
        string actualVersion = null;
        string appUri = null;
        string appName = null;

        public void FocusOnMainElement()
        {
            //btnOk.Focus();
        }

        /// <summary>
        /// A2 like used-car-comparing
        /// A4 = SellingHelper.CheckNewVersion etc.
        /// </summary>
        /// <param name="updateUri"></param>
        /// <param name="appUri"></param>
        public AboutApp(string updateUri, string appUri, string appName, Action<string, string, string> CheckNewVersion)
        {
            this.InitializeComponent();

            this.updateUri = updateUri;
            this.appUri = appUri;
            this.appName = appName;

            tbTitle.Text = sess.i18n(XlfKeys.AboutApp) + AllStrings.space  + appName;

            System.Reflection.Assembly assembly = System.Reflection.Assembly.GetEntryAssembly();
            
            System.Diagnostics.FileVersionInfo fvi = System.Diagnostics.FileVersionInfo.GetVersionInfo(assembly.Location);
            actualVersion = fvi.FileVersion;
            tbAboutApp.Text = sess.i18n(XlfKeys.Version) + ": " + actualVersion;

            //WRTBH tbh2 = new WRTBH(475, 10, FontArgs.DefaultRun());
            ParagraphBuilderTextBlock tbh2 = new ParagraphBuilderTextBlock();
            // padding / margin top / bottom not working, therefore create new line will
            tbh2.Hyperlink(sess.i18n(XlfKeys.CzechBlog), "http://jepsano.net");
            tbh2.LineBreak();
            tbh2.LineBreak();
            tbh2.Hyperlink(sess.i18n(XlfKeys.EnglishBlog), "http://blog.sunamo.cz");
            tbh2.LineBreak();
            tbh2.LineBreak();
            tbh2.Hyperlink("Web", "http://www.sunamo.cz");
            tbh2.LineBreak();
            tbh2.LineBreak();

            //tbh2.Hyperlink(sess.i18n(XlfKeys.Google), "https://plus.google.com/111524962367375368826");
            //tbh2.LineBreak();
            //tbh2.LineBreak();
            tbh2.Hyperlink("Mail: radek.jancik@sunamo.cz", "mailto:radek.jancik@sunamo.cz");
            tbh2.LineBreak();
            tbh2.LineBreak();

            // cant be named new instead of updated - translation toolkit check for new a, new b, atd... 
            btnCheckNewVersion.Content = sess.i18n(XlfKeys.CheckUpdatedVersion);

            tbh2.margin = new Thickness(25, 0, 0, 0);
            tbh2.padding = new Thickness(0, 0, 0, 0);
            var sp = tbh2.Final();
            Grid.SetRow(sp, 4);
            grid.Children.Add(sp);

            //wg.DataContext = tbh2.uis;
            //var itemsPanel = wg.ItemsPanel;
            ////var ipt = itemsPanel.te
            //var d = wg;

            this.CheckNewVersion = CheckNewVersion;
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

        public  Action<string, string, string> CheckNewVersion;

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
            CheckNewVersion(updateUri, actualVersion, appUri);
        }
    }
}