using desktop.Controls;
using sunamo;
using sunamo.Essential;
using sunamo.Helpers.Types;
using sunamo.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace desktop.Helpers.Backend
{
    public class FoundedFileUCBackend : IKeysHandler<KeyEventArgs>
    {
        static Type type = typeof(FoundedFileUCBackend);

        IAnotherLocation<string, string> targetCopyLocation;
        SelectedCastHelper<string> selectedCastHelperFoundedFilesUC;
        TextBox txtContent;
        FoundedFilesUC foundedFilesUC;

        public List<string> OldRoots;
        public string TargetFolderDuringCopy = null;

        public FoundedFileUCBackend(SelectedCastHelper<string> selectedCastHelperFoundedFilesUC, TextBox txtContent, FoundedFilesUC foundedFilesUC)
        {
            foundedFilesUC.CutFile += foundedFilesUC_CutFile;
            foundedFilesUC.CopyToClipboardAndDelete += foundedFilesUC_CopyToClipboardAndDelete;
            foundedFilesUC.CopyToAnotherLocation += FoundedFilesUC_CopyToAnotherLocation;
            this.selectedCastHelperFoundedFilesUC = selectedCastHelperFoundedFilesUC;
            this.txtContent = txtContent;
            this.foundedFilesUC = foundedFilesUC;
        }

        private void FoundedFilesUC_CopyToAnotherLocation(string oldLocation)
        {
            ThrowExceptions.IsNull(type, "FoundedFilesUC_CopyToAnotherLocation", "targetCopyLocation", targetCopyLocation);

            // promyslet si jak jsem to tu zamyslel
            //var newLocation = targetCopyLocation.ReturnRightLocation(FS.GetFileName(oldLocation));

            //var oldRoot = CA.ReturnStartingWith(OldRoots, newLocation);

            //string newPath = oldLocation.Replace(OldRoot, newLocation);
        }

        public void foundedFilesUC_CutFile(string t)
        {
            var selected = RemoveSelectedFile();
            ClipboardHelper.CutFiles(selected);
            
            TurnOffSelected();
        }

        string RemoveSelectedFile()
        {
            var selected = selectedCastHelperFoundedFilesUC.Selected;
            foundedFilesUC.RemoveFile(selected);
            return selected;
        }

        private void TurnOffSelected()
        {
            txtContent.Text = string.Empty;
            foundedFilesUC.IsSelected = false;
        }

        public void foundedFilesUC_CopyToClipboardAndDelete(string FilePath)
        {
            RemoveSelectedFile();
            string content = File.ReadAllText(FilePath);
            Clipboard.SetText(content);

            if (FS.TryDeleteFile(FilePath))
            {
                TurnOffSelected();
            }

            SunamoTemplateLogger.Instance.CopiedToClipboard("File content");
        }

        public bool HandleKey(KeyEventArgs e)
        {
            if (KeyboardHelper.KeyWithNoneModifier(e, Key.X))
            {
                if (foundedFilesUC.IsSelected)
                {
                    this.foundedFilesUC_CutFile(selectedCastHelperFoundedFilesUC.Selected);
                }
                return true;
            }
            if (KeyboardHelper.KeyWithNoneModifier(e, Key.D))
            {
                if (foundedFilesUC.IsSelected)
                {
                    string FilePath = selectedCastHelperFoundedFilesUC.Selected;
                    this.foundedFilesUC_CopyToClipboardAndDelete(FilePath);
                }
                return true;
            }

            return false;
        }

        
    }
}
