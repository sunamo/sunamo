using sunamo;
using sunamo.Enums;
using sunamo.Essential;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace apps.Popups
{
    /// <summary>
    /// Má jen tlačítko OK
    /// </summary>
    public sealed partial class ShowFSException : UserControl, IPopupResponsive, IPopupEvents<object>
    {
        Langs l = Langs.cs;
        string fileOrFolder = null;

        public void ApplyColorTheme(ColorTheme ct)
        {
            ColorThemeHelper.ApplyColorTheme(border, ct);
        }
        public ShowFSException(string fileOrFolder, FileExceptions fsExc, Langs l)
        {
            this.InitializeComponent();
            this.fileOrFolder = fileOrFolder;
            this.l = l;
            tbTitle.Text = ThisApp.Name + " - ";
            
            tbTitle.Text += RL.GetString("Warning");
            switch (fsExc)
            {
                case FileExceptions.None:
                    ExcNone();
                    break;
                case FileExceptions.FileNotFound:
                    ExcFileNotFound();
                    break;
                case FileExceptions.UnauthorizedAccess:
                    ExcUnauthorizedAccess();
                    break;
                case FileExceptions.General:
                default:
                    ExcGeneral();
                    break;
            }
        }

        private void ExcGeneral()
        {
            
            ShowMessage(RL.GetString("UnknownErrorWhenWorkWithFileOrFolder") + " " + fileOrFolder);
        }

        private void ShowMessage(string p)
        {
            tbZprava.Text = p;
        }

        public Size MaxContentSize
        {
            get
            {
                //return maxContentSize;
                return FrameworkElementHelper.GetMaxContentSize(this);
            }
            set
            {
                //maxContentSize = value;
                FrameworkElementHelper.SetMaxContentSize(this, value);
            }
        }

        private void ExcUnauthorizedAccess()
        {
            ShowMessage(RL.GetString("InsufficientRightsToAccessFileOrFolder") + " " + fileOrFolder);
        }

        private void ExcFileNotFound()
        {
            ShowMessage(RL.GetString("FileOrFolder") + " " + fileOrFolder + " " + RL.GetString("doesNotExists"));
        }

        private void ExcNone()
        {
            
                ShowMessage(RL.GetString("ExceptionNotExists"));
            
        }

        

        public Brush PopupBorderBrush
        {
            set { border.BorderBrush = value; }
        }

        public event VoidT<object> ClickCancel;

        public event VoidT<object> ClickOK;

        private void OnClickOK(object sender, RoutedEventArgs e)
        {
            ClickOK(null);
        }

        private void OnClickCancel(object sender, RoutedEventArgs e)
        {

        }
    }
}
