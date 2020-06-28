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
    /// Use for variable name always longer, showResult instead of sr etc.
    /// Stupid, open with highlighting in VSCode instead of my UC
    /// </summary>
    public partial class ShowTextResult : UserControl, IUserControl, IControlWithResult, IUserControlWithSizeChange
    {
        /// <summary>
        /// Must be empty constructor due to creating in SetMode()
        /// </summary>
        public ShowTextResult()
        {
            InitializeComponent();
        }

        public void FocusOnMainElement()
        {
            txtResult.Focus();
        }

        public ShowTextResult(string text) : this()
        {
            txtResult.Text = text;
        }

        public bool? DialogResult {
            set { 
                // In case ShowTextResult I dont need any handler, therefore checking
                if (ChangeDialogResult != null )
	{
                    // must have value because ResultButtons dont close window itself     
                    ChangeDialogResult(value);
                }
               
            }
        }

        public string Title => sess.i18n(XlfKeys.ShowResult);

        public event VoidBoolNullable ChangeDialogResult;

        public void Accept(object input)
        {
            DialogResult = true;
        }

        public void Init()
        {
            
        }

        public void OnSizeChanged(DesktopSize maxSize)
        {
            txtResult.Height = rowGrowing.ActualHeight;
        }

        private void resultButtons_AllRightClick()
        {
            DialogResult = true;
        }

        private void resultButtons_CopyToClipboard()
        {
            ClipboardHelper.SetText(txtResult.Text);
            SunamoTemplateLogger.Instance.CopiedToClipboard(sess.i18n(XlfKeys.Result));
        }
    }
}