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
    /// Interaction logic for EnterValueForm.xaml
    /// </summary>
    public partial class EnterOneValueWindow : Window
    {
        /// <summary>
        /// access to everything via enterOneValueUC
        /// </summary>
        /// <param name="whatEnter"></param>
        public EnterOneValueWindow(string whatEnter)
        {
            InitializeComponent();
            enterOneValueUC.Init(whatEnter);
            enterOneValueUC.ChangeDialogResult += EnterOneValueUC_ChangeDialogResult;
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
            Close();
        }
    }
}
