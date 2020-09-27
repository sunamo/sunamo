using System;
using System.Collections.Generic;
using System.IO;
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
    public partial class SelectFolderAdvanced : UserControl, IControlWithResult
    {
        public bool? DialogResult
        {
            set
            {
                ChangeDialogResult(value);
            }
        }

        public void FocusOnMainElement()
        {
            selectFolder.Focus();
        }

        public SelectFolderAdvanced()
        {
            InitializeComponent();
        }

        public event VoidBoolNullable ChangeDialogResult;

        public CheckBox chbDontAskAgain
        {
            get
            {
                return FrameworkElementHelper.FindByTag<CheckBox>(cDialogButtons.CustomControl, "chbDontAskAgain");
            }
        }

        public TextBox txtMasc
        {
            get
            {
                return FrameworkElementHelper.FindByTag<TextBox>(cDialogButtons.CustomControl, "txtMasc");
            }
        }

        public CheckBox chbFilesFromSubfolders
        {
            get
            {
                return FrameworkElementHelper.FindByTag<CheckBox>(cDialogButtons.CustomControl, "chbFilesFromSubfolders");
            }
        }

        public string SelectedFolder
        {
            get
            {
                return selectFolder.SelectedFolder;
            }
            set
            {
                selectFolder.SelectedFolder = value;
                selectFolder_FolderChanged(value);
            }
        }

        CheckBox GetChbDontAskAgain()
        {
            return ((CheckBox)cDialogButtons.CustomControl);
        }

        public bool DontAskAgain
        {
            get
            {
                return GetChbDontAskAgain().IsChecked.Value;
            }
        }

        public Visibility VisibilityBtnApply
        {
            set
            {
                cDialogButtons.btnApply.Visibility = value;
            }
        }

        public Visibility VisibilityChbDontAskAgain
        {
            set
            {
                GetChbDontAskAgain().Visibility = value;
            }
        }

        private void selectFolder_FolderChanged(string s)
        {
            if (FS.ExistsDirectory(s))
            {
                cDialogButtons.btnOk.IsEnabled = true;
            }
            else
            {
                cDialogButtons.btnOk.IsEnabled = false;
            }
        }

        private void cDialogButtons_ChangeDialogResult(bool? b)
        {
            DialogResult = b;
        }

        /// <summary>
        /// A1 = string, cant be null
        /// </summary>
        /// <param name="input"></param>
        public void Accept(object input)
        {
            selectFolder.SelectedFolder = input.ToString();
            ChangeDialogResult(true);
            // Cant be, window must be already showned as dialog
            //DialogResult = true;
        }

    }
}