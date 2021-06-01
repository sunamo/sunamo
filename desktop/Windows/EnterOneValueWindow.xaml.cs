using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace desktop
{
    /// <summary>
    /// Select Value - more from selector
    /// EnterOneValueUC - single,  EnterOneValueUC - fwElemements 
    /// </summary>
    public partial class EnterOneValueWindow : Window
    {
        public Func<string, bool> ValidatorBeforeAdding
        {
            set
            {
                enterOneValueUC.ValidatorBeforeAdding = value;
            }
        }

        public string ValidatorBeforeAddingMessage
        {
            set
            {
                enterOneValueUC.ValidatorBeforeAddingMessage = value;
            }
        }

        

        /// <summary>
        /// access to everything via enterOneValueUC
        /// </summary>
        /// <param name="whatEnter"></param>
        public EnterOneValueWindow(string whatEnter)
        {
            InitializeComponent();
            enterOneValueUC.Init(whatEnter);
            // TODO Replaced during repair 0xc0000374
            //enterOneValueUC.ChangeDialogResult += EnterOneValueUC_ChangeDialogResult;
        }

        public bool IsMultiline
        {
            set
            {
                if (value)
                {
                    enterOneValueUC.IsMultiline = value;
                }
            }
        }
        private void EnterOneValueUC_ChangeDialogResult(bool? b)
        {
            // Close() + DialogResult = b - Dialog result can be only set when is show as the dialog
            // Only DialogResult = b - works rightly with attach ChangeDialogResult or ShowDialog()
            DialogResult = b;
        }
    }
}