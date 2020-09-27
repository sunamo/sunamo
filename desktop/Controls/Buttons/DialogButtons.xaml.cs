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
    public partial class DialogButtons : UserControl, IControlWithResult
    {
        public DialogButtons()
        {
            InitializeComponent();
        }

        public void FocusOnMainElement()
        {
            btnOk.Focus();
        }

        public UIElement CustomControl
        {
            set
            {
                grid.Children.Insert(0, value);
            }
            get
            {
                return grid.Children[0];
            }
        }

        public bool? DialogResult
        {
            set
            {
                ChangeDialogResult(value);
            }
        }

        public event VoidBoolNullable ChangeDialogResult;

        public bool IsEnabledBtnOk
        {
            set
            {
                btnOk.IsEnabled = value;
            }
        }

        public bool clickedOk = false;
        public bool clickedApply = false;
        public bool clickedCancel = false;


        public bool IsEnabledBtnApply
        {
            set
            {
                btnApply.IsEnabled = value;
            }
        }

        public Visibility VisibilityBtnApply
        {
            set
            {
                btnApply.Visibility = value;
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            clickedCancel = true;
            DialogResult = false;
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            clickedOk = true;
            DialogResult = true;
        }

        private void btnApply_Click(object sender, RoutedEventArgs e)
        {
            clickedApply = true;
            DialogResult = null;
        }

        public void Accept(object input)
        {
            ThrowExceptions.Custom(Exc.GetStackTrace(), type, Exc.CallingMethod(),sess.i18n(XlfKeys.OnlyButtonsCanBeAcceptedBecauseItHasNoDataForAccept) + ".");
        }

        static Type type = typeof(DialogButtons);
    }
}