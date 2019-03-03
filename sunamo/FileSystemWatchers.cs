using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace sunamo
{
    /// <summary>
    /// If I want to watch files in more directories
    /// </summary>
    public class FileSystemWatchers
    {
        Dictionary<string, FileSystemWatcher> watchers = new Dictionary<string, FileSystemWatcher>();
        VoidStringT<bool> onStart;
        VoidStringT<bool> onStop;
        bool moreFolders = false;
        FileSystemWatcher fileSystemWatcher = null;
        public FileSystemWatchers(bool moreFolders, VoidStringT<bool> onStart, VoidStringT<bool> onStop)
        {
            this.moreFolders = moreFolders;
            this.onStart = onStart;
            this.onStop = onStop;
        }

        public void Start(string path)
        {
            // Adding handlers - must wrap up all
            if (moreFolders)
            {
                if (!watchers.ContainsKey(path))
                {
                    onStart.Invoke(path, true);

                    var fileSystemWatcher = RegisterSingleFolder(path);

                    watchers.Add(path, fileSystemWatcher);
                }
            }
        }

        public FileSystemWatcher RegisterSingleFolder(string path)
        {
             fileSystemWatcher = new FileSystemWatcher(path);
            fileSystemWatcher.Filter = "*.cs";
            if (!moreFolders)
            {
                fileSystemWatcher.IncludeSubdirectories = true;
            }
            fileSystemWatcher.NotifyFilter = NotifyFilters.Attributes |
    NotifyFilters.CreationTime |
    NotifyFilters.FileName |
    NotifyFilters.LastAccess |
    NotifyFilters.LastWrite |
    NotifyFilters.Size |
    NotifyFilters.Security;
            
            fileSystemWatcher.Deleted += FileSystemWatcher_Deleted;
            fileSystemWatcher.Changed += FileSystemWatcher_Changed;
            fileSystemWatcher.Renamed += FileSystemWatcher_Renamed;

            fileSystemWatcher.EnableRaisingEvents = true;

            //fileSystemWatcher.SynchronizingObject;
            //fileSystemWatcher.InitializeLifetimeService();

            return fileSystemWatcher;
        }

        public void Stop(string path)
        {
            onStop.Invoke(path, true);

            if (moreFolders)
            {
                watchers.Remove(path);

                FileSystemWatcher fileSystemWatcher = watchers[path];
                fileSystemWatcher.Deleted -= FileSystemWatcher_Deleted;
                fileSystemWatcher.Changed -= FileSystemWatcher_Changed;
                fileSystemWatcher.Renamed -= FileSystemWatcher_Renamed;
                fileSystemWatcher.Dispose();
            }

            // During delete call onStop which call this method
        }

        private void FileSystemWatcher_Renamed(object sender, RenamedEventArgs e)
        {
            onStop.Invoke(e.OldFullPath, true);
            onStart.Invoke(e.FullPath, true);
        }

        private void FileSystemWatcher_Changed(object sender, FileSystemEventArgs e)
        {
            onStop.Invoke(e.FullPath, true);
            onStart.Invoke(e.FullPath, true);
        }

        private void FileSystemWatcher_Deleted(object sender, FileSystemEventArgs e)
        {
            onStop.Invoke(e.FullPath, true);
        }

        
    }
}
