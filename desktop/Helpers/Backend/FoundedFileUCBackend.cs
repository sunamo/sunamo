using desktop.Controls.Result;
using sunamo.Helpers.Types;
using sunamo.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace desktop.Helpers.Backend
{
    public class FoundedFileUCBackend : IKeysHandler<KeyEventArgs>
    {
        public List<string> OldRoots = null;
        private SelectedCastHelper<string> selectedCastHelperString;
        private TextBox txtContent;
        private FoundedFilesUC foundedFilesUC;

        public FoundedFileUCBackend(SelectedCastHelper<string> selectedCastHelperString, TextBox txtContent, FoundedFilesUC foundedFilesUC)
        {
            this.selectedCastHelperString = selectedCastHelperString;
            this.txtContent = txtContent;
            this.foundedFilesUC = foundedFilesUC;

            
        }

        public string FullPathSelectedFile
        {
            get
            {
                return selectedCastHelperString.SelectedItem;
            }
        }

        public event VoidString FileIsRightEvent;
        public event VoidString LeaveInActualFolder;
        public event VoidString MoveLastFile;
        public event VoidString ReturnMovedFileBack;
        

        public bool HandleKey(KeyEventArgs e)
        {
            if (e.Key == Key.Right)
            {
                FileIsRightEvent(FullPathSelectedFile);
                return true;
            }
            else if (e.Key == Key.Left)
            {
                LeaveInActualFolder(FullPathSelectedFile);
                return true;
            }
            else if (e.Key == Key.Up)
            {
                MoveLastFile(FullPathSelectedFile);
                return true;
            }
            else if (e.Key == Key.Back)
            {
                ReturnMovedFileBack(FullPathSelectedFile);
                return true;
            }
            return false;
        }


    }
}
