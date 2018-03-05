using desktop.Essential;
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
    /// 
    /// </summary>
    public partial class InputTwoColumnsUC : UserControl, IUserControlInWindow
    {
        public InputTwoColumnsUC()
        {
            InitializeComponent();

            dialogButtons.ChangeDialogResult += DialogButtons_ChangeDialogResult;
        }

        private void DialogButtons_ChangeDialogResult(bool? b)
        {
            if (b.HasValue)
            {
                if (!b.Value)
                {
                    DialogResult = false;
                }
            }

            if (string.IsNullOrEmpty(txtFirst.Text) || string.IsNullOrEmpty(txtSecond.Text))
            {
                ThisApp.SetStatus(TypeOfMessage.Error, ExceptionStrings.AllOfInputsMustBeFilled);
                
            }
            else
            {
                DialogResult = true;

            }
        }

        public void Init(string textFirst, string textSecond)
        {
            tbFirst.Text = textFirst;
            tbSecond.Text = textSecond;
        }

        public bool? DialogResult
        {
            set
            {
                ChangeDialogResult(value);
            }
        }

        public event VoidBoolNullable ChangeDialogResult;

        private void Grid_KeyDown(object sender, KeyEventArgs e)
        {

        }
    }
}
