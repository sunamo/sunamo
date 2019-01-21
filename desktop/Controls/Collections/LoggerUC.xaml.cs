using sunamo.Essential;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace desktop.Controls.Collections
{
    /// <summary>
    /// Interaction logic for LoggerUC.xaml
    /// </summary>
    public partial class LoggerUC : UserControl
    {
        public LoggerUC()
        {
            InitializeComponent();
        }

        private void BtnClear_Click(object sender, RoutedEventArgs e)
        {
            lbLogs.Children.Clear();
        }

        private void BtnCopyToClipboard_Click(object sender, RoutedEventArgs e)
        {
            List<string> result = new List<string>(lbLogs.Children.Count);
            foreach (var item in lbLogs.Children)
            {
                result.Add(((TextBlock)item).Text);
            }
            ClipboardHelper.SetLines(result);
            SunamoTemplateLogger.Instance.CopiedToClipboard("logs");
        }
    }
}
