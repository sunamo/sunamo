using IWshRuntimeLibrary;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace sunamo.Helpers
{
    public class WindowsOSHelper
    {
        public static void CreateLnk(string sourceDirectory, string targetFile)
        {
            var fn = FS.GetFileNameWithoutExtension(targetFile);
            FS.CreateFoldersPsysicallyUnlessThere(sourceDirectory);

            var shell =  new WshShell();
            var shortCutLinkFilePath = FS.Combine(sourceDirectory, @"\\\\\"+fn+  ".lnk");
            if (!FS.ExistsFile(shortCutLinkFilePath))
            {
                var windowsApplicationShortcut = (IWshShortcut)shell.CreateShortcut(shortCutLinkFilePath);
                windowsApplicationShortcut.Description = "How to create short for application example";
                windowsApplicationShortcut.WorkingDirectory = FS.GetDirectoryName(targetFile);
                windowsApplicationShortcut.TargetPath = targetFile;
                windowsApplicationShortcut.Save();
            }
        }

        //public static void CreateLnk2()
        //{
        //    // Check necessary parameters first:
        //    if (String.IsNullOrEmpty(TargetPath))
        //        ThrowExceptions.Custom(RuntimeHelper.GetStackTrace(), type, RH.CallingMethod(),ArgumentNullException("TargetPath");
        //    if (String.IsNullOrEmpty(ShortcutFile))
        //        ThrowExceptions.Custom(RuntimeHelper.GetStackTrace(), type, RH.CallingMethod(),ArgumentNullException("ShortcutFile");

        //    // Create WshShellClass instance:
        //    IWshShell3 wshShell = new WshShellClass();

        //    // Create shortcut object:
        //    IWshRuntimeLibrary.IWshShortcut shorcut = (IWshRuntimeLibrary.IWshShortcut)wshShell.CreateShortcut(ShortcutFile);

        //    // Assign shortcut properties:
        //    shorcut.TargetPath = TargetPath;
        //    shorcut.Description = Description;
        //    if (!String.IsNullOrEmpty(Arguments))
        //        shorcut.Arguments = Arguments;
        //    if (!String.IsNullOrEmpty(HotKey))
        //        shorcut.Hotkey = HotKey;
        //    if (!String.IsNullOrEmpty(WorkingDirectory))
        //        shorcut.WorkingDirectory = WorkingDirectory;

        //    // Save the shortcut:
        //}

        //public static void CreateLnk3()
        //{
        //    WshShellClass wsh = new WshShellClass();
        //    IWshRuntimeLibrary.IWshShortcut shortcut = wsh.CreateShortcut(
        //        Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\shorcut.lnk") as IWshRuntimeLibrary.IWshShortcut;
        //    shortcut.Arguments = "c:\\app\\settings1.xml";
        //    shortcut.TargetPath = "c:\\app\\myftp.exe";
        //    // not sure about what this is for
        //    shortcut.WindowStyle = 1;
        //    shortcut.Description = "my shortcut description";
        //    shortcut.WorkingDirectory = "c:\\app";
        //    shortcut.IconLocation = "specify icon location";
        //    shortcut.Save();
        //}
    }
}