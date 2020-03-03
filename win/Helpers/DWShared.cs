using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

public static partial class DW{ 
    /// <summary>
    /// Filter is set do default - PP filterDefault
    /// InitialFolder is MyDocuments
    /// G null při nenalezení
    /// </summary>
    
    private static List<string> SelectOfFiles(string filter, string initialDirectory, bool multiselect)
    {
        OpenFileDialog ofd = new OpenFileDialog();
        ofd.CustomPlaces.Add(new FileDialogCustomPlace(FS.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), Consts.@sunamo)));
        ofd.AddExtension = false;
        ofd.InitialDirectory = initialDirectory;
        //ofd.AutoUpgradeEnabled = true;
        ofd.DereferenceLinks = false;
        ofd.Filter = FS.RepairFilter(filter);
        ofd.CheckPathExists = true;
        ofd.CheckFileExists = true;
        ofd.RestoreDirectory = true;
        ofd.SupportMultiDottedExtensions = true;
        ofd.ValidateNames = true;
        ofd.Multiselect = multiselect;
        if (ofd.ShowDialog() == DialogResult.OK)
        {
            return ofd.FileNames.ToList();
        }

        return null;
    }
}