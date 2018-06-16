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

namespace desktop.Controls
{
    /// <summary>
    /// Interaction logic for ShowTextResult.xaml
    /// </summary>
    public partial class ShowTextResult : UserControl, IUserControlInWindow
    {
        /// <summary>
        /// Must be empty constructor due to creating in SetMode()
        /// </summary>
        public ShowTextResult()
        {
            InitializeComponent();
        }

        public ShowTextResult(string text) : this()
        {
            txtResult.Text = text;
        }

        public bool? DialogResult { set => ChangeDialogResult(value); }

        public event VoidBoolNullable ChangeDialogResult;

        private void resultButtons_AllRightClick()
        {
            DialogResult = true;
        }

        private void resultButtons_CopyToClipboard()
        {
            ClipboardHelper.SetText(txtResult.Text);
            SunamoTemplateLogger.Instance.CopiedToClipboard("Result");
        }
    }
}
