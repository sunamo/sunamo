
using sunamo.Values;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace sunamo
{
    public static class DW
    {
        #region PathToSaveFileTo
        public static string SelectPathToSaveFileTo(AppFolders af, string filter, bool checkFileExists, string nameWithExt)
        {
            return SelectPathToSaveFileTo(AppData.GetFolder(af), filter, checkFileExists, nameWithExt);
        }

        public static string SelectPathToSaveFileTo(AppFolders af, string filter, bool checkFileExists)
        {
            return SelectPathToSaveFileTo(AppData.GetFolder(af), filter, checkFileExists);
        }

        public static string SelectPathToSaveFileTo(string initialDirectory, string filter, bool checkFileExists)
        {
            return SelectPathToSaveFileTo(initialDirectory, filter, checkFileExists, "");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="p"></param>
        /// <param name="filtr"></param>
        /// <returns></returns>
        public static string SelectPathToSaveFileTo(string initialDirectory, string filter, bool checkFileExists, string nameWithExt)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.CustomPlaces.Add(new FileDialogCustomPlace(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), Consts.@sunamo)));
            sfd.SupportMultiDottedExtensions = true;
            sfd.InitialDirectory = initialDirectory;
            sfd.CheckPathExists = true;
            sfd.CheckFileExists = checkFileExists;
            //sfd.DefaultExt = ".txt";
            sfd.Filter = FS.RepairFilter(filter);
            sfd.ValidateNames = true;
            sfd.FileName = nameWithExt;
            sfd.Title = "Vyberte soubor do ktereho se ulozi";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                return sfd.FileName;
            }
            return null;
        }

        public static string SelectPathToSaveFileTo(string initialDirectory, string filter)
        {
            return SelectPathToSaveFileTo(initialDirectory, filter, false);
        }

        /// <summary>
        /// As filter will be set AllFiles *.*
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public static string SelectPathToSaveFileTo(string initialDirectory)
        {
            return SelectPathToSaveFileTo(initialDirectory, filterDefault);
        }
        #endregion

        #region SelectPathToSaveFileToMustExists
        public static string SelectPathToSaveFileToMustExists(string initialDirectory, string filter)
        {
            return SelectPathToSaveFileTo(initialDirectory, filter, true);
        }
        #endregion

        /// <summary>
        /// Default is All Files|*.*
        /// </summary>
        public static string filterDefault = "All Files|*.*";

        #region Other
        /// <summary>
        /// ...
        /// </summary>
        /// <param name="description"></param>
        /// <param name="masc"></param>
        public static void UpdateDefaultFilter(string description, string masc)
        {
            filterDefault = description + "|" + masc;
        }

        public static string GetFilter(string description, string masc)
        {
            return description + "|" + masc;
        }


        #endregion

        #region SelectOfFolder
        /// <summary>
        /// G null if no folder selected
        /// </summary>
        /// <param name="rootFolder"></param>
        /// <returns></returns>
        public static string SelectOfFolder(Environment.SpecialFolder rootFolder)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.Description = "Vyberte si slozku";
            // Here is available set this only way
            fbd.RootFolder = rootFolder;
            fbd.ShowNewFolderButton = true;

            if (fbd.ShowDialog() == DialogResult.OK)
            {
                return fbd.SelectedPath;
            }

            return null;
        }
#endregion

#region SelectOfFile
        /// <summary>
        /// Filter is set do default - PP filterDefault
        /// InitialFolder is MyDocuments
        /// G null při nenalezení
        /// </summary>
        /// <returns></returns>
        public static string SelectOfFile()
        {
            return SelectOfFile(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments));
        }

        /// <summary>
        /// G null při nenalezení
        /// Filter is set do default - PP filterDefault
        /// </summary>
        /// <param name="initialFolder"></param>
        /// <returns></returns>
        public static string SelectOfFile(Environment.SpecialFolder initialFolder)
        {
            return SelectOfFile(Environment.GetFolderPath(initialFolder));
        }


        /// <summary>
        /// G null při nenalezení
        /// </summary>
        /// <param name="initialFolder"></param>
        /// <returns></returns>
        public static string SelectOfFile(AppFolders initialFolder)
        {
            return SelectOfFile(AppData.GetFolder(initialFolder));
        }

        /// <summary>
        /// G null při nenalezení
        /// Filter is set do default - PP filterDefault
        /// </summary>
        /// <param name="initialFolder"></param>
        /// <returns></returns>
        public static string SelectOfFile(string initialFolder)
        {
            return SelectOfFile(filterDefault, initialFolder);
        }

        /// <summary>
        /// G null při nenalezení
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="initialFolder"></param>
        /// <returns></returns>
        public static string SelectOfFile(string filter, string initialFolder)
        {
            string[] d = SelectOfFiles(filter, initialFolder, false);
            if (CA.HasAtLeastOneElementInArray(d))
            {
                return d[0];
            }
            return null;
        }
#endregion

#region SelectOfFiles
        /// <summary>
        /// As filter is set PP filterDefault, multiselect is enabled.
        /// InitialDirectory is MyDocuments.
        /// </summary>
        /// <returns></returns>
        public static string[] SelectOfFiles()
        {
            return SelectOfFiles(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments));
        }

        /// <summary>
        /// As filter is set filterDefault, multiselect is enabled.
        /// </summary>
        /// <param name="sf"></param>
        /// <returns></returns>
        public static string[] SelectOfFiles(Environment.SpecialFolder sf)
        {
            return SelectOfFiles(Environment.GetFolderPath(sf));
        }

        /// <summary>
        /// As filter is set filterDefault, multiselect is enabled.
        /// </summary>
        /// <param name="initialFolder"></param>
        /// <returns></returns>
        public static string[] SelectOfFiles(string initialFolder)
        {
            return SelectOfFiles(filterDefault, initialFolder, true);
        }

        /// <summary>
        /// Multiselect is enabled.
        /// </summary>
        /// <param name="filtr"></param>
        /// <param name="initialFolder"></param>
        /// <returns></returns>
        public static string[] SelectOfFiles(string filtr, string initialFolder)
        {
            return SelectOfFiles(filtr, initialFolder, true);
        }

        /// <summary>
        /// Vrati seznam vybranych souboru nebo null
        /// 
        /// </summary>
        /// <param name="filtr"></param>
        /// <param name="slozka"></param>
        /// <param name="multiselect"></param>
        /// <returns></returns>
        private static string[] SelectOfFiles(string filter, string initialDirectory, bool multiselect)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.CustomPlaces.Add(new FileDialogCustomPlace(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), Consts.@sunamo)));
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
                return ofd.FileNames;
            }
            return null;
        }
#endregion
    }
}
