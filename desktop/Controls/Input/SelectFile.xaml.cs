﻿using desktop.AwesomeFont;
using sunamo;
using sunamo.Essential;
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
    /// Interaction logic for SelectFile.xaml
    /// </summary>
    public partial class SelectFile : UserControl
    {
        /// <summary>
        /// In folder has hame Folder*Changed* but there already exists FileSelected
        /// </summary>
        public event VoidString FileSelected;
        public event VoidT<SelectFile> FileRemoved;

        public SelectFile()
        {
            InitializeComponent();
            SelectedFile = "";

            Loaded += SelectFile_Loaded;
        }

        private void SelectFile_Loaded(object sender, RoutedEventArgs e)
        {
            SetAwesomeIcons();
        }

        private void BtnRemoveFile_Click(object sender, RoutedEventArgs e)
        {
            if (FileRemoved != null)
            {
                FileRemoved(this);
            }
        }

        async void SetAwesomeIcons()
        {
            await AwesomeFontControls.SetAwesomeFontSymbol(btnRemoveFile, "\uf00d");
        }

        private void OnFileChanged(string File)
        {
            if (FileSelected != null)
            {
                FileSelected(File);
            }
        }

        private void SetSelectedFile(string v)
        {
            if (v == "")
            {
                v = "None";
            }
            selectedFile = v;
            tbSelectedFile.Text = "Selected file" + ": " + v;
        }

        private void btnSelectFile_Click(object sender, RoutedEventArgs e)
        {
            string file = DW.SelectOfFile(Environment.SpecialFolder.DesktopDirectory);
            if (file != null)
            {
                if (FS.ExistsFile(file))
                {
                    SelectedFile = file;
                    FileSelected(file);
                }
            }
        }

        public void Validate(TextBlock tbNewPath)
        {
            validated = FS.ExistsFile(this.SelectedFile);
            if (!validated)
            {
                InitApp.TemplateLogger.FileDontExists(this.SelectedFile);
            }
        }

        string selectedFile = "";
        public static bool validated;

        public string SelectedFile
        {
            get
            {
                return selectedFile;
            }
            set
            {
                OnFileChanged(value);
                SetSelectedFile(value);
            }
        }
    }
}
