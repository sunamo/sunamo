using desktop.Controls.Result;
using sunamo.Data;
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
    /// <summary>
    /// IsDerived from TextBoxBackend
    /// </summary>
    public class FoundedFileUCBackend : IKeysHandler
    {
        public CollectionWithoutDuplicates<string> OldRoots = new CollectionWithoutDuplicates<string>();
        private SelectedCastHelper<string> selectedCastHelperString;
        public TextBoxBackend textBoxBackend = null;
        private FoundedFilesUC foundedFilesUC;
        FoundedFileUC lastSelected = null;

        /// <summary>
        /// A1 and A3 can be null - ReviewRestoredSourceFilesUC
        /// A5 = Consts.addRowsToCodeTextBoxDuringScrolling
        /// </summary>
        /// <param name="selectedCastHelperString"></param>
        /// <param name="txtContent"></param>
        /// <param name="foundedFilesUC"></param>
        public FoundedFileUCBackend(TextBlock txtTextBoxState, TextBox txtContent, SelectedCastHelper<string> selectedCastHelperString,  FoundedFilesUC foundedFilesUC, int addRowsDuringScrolling) 
        {
            this.selectedCastHelperString = selectedCastHelperString;
            this.foundedFilesUC = foundedFilesUC;
            foundedFilesUC.Selected += FoundedFilesUC_Selected;

            textBoxBackend = new TextBoxBackend(txtTextBoxState, txtContent, addRowsDuringScrolling);
        }

        private void FoundedFilesUC_Selected(string s)
        {
            ControlHelper.SwitchBorder(lastSelected, BorderData.Black1);
            lastSelected = foundedFilesUC.GetFoundedFileByPath(s);
            ControlHelper.SwitchBorder(lastSelected, BorderData.Black1);
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
            if (FullPathSelectedFile == null)
            {
                return false;
            }

            if (e.Key == Key.Right)
            {
                // Is right
                if (FileIsRightEvent != null)
                {
                    FileIsRightEvent(FullPathSelectedFile);
                    return true;
                }
                
            }
            else if (e.Key == Key.Left)
            {
                // Is wrong, keep where is 
                if (LeaveInActualFolder != null)
                {
                    LeaveInActualFolder(FullPathSelectedFile);
                    return true;
                }
                
            }
            else if (e.Key == Key.Up)
            {
                // Action
                if (MoveLastFile != null)
                {
                    MoveLastFile(FullPathSelectedFile);
                    return true;
                }
                
            }
            else if (e.Key == Key.Down)
            {
                // Undo action
                if (ReturnMovedFileBack != null)
                {
                    ReturnMovedFileBack(FullPathSelectedFile);
                    return true;
                }
                
            }
            return false;
        }


    }
}