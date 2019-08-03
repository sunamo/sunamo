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
        private Dictionary<string, FileSystemWatcher> _watchers = new Dictionary<string, FileSystemWatcher>();
        private VoidStringT<bool> _onStart;
        private VoidStringT<bool> _onStop;
        private bool _moreFolders = false;
        private FileSystemWatcher _fileSystemWatcher = null;
        public FileSystemWatchers(bool moreFolders, VoidStringT<bool> onStart, VoidStringT<bool> onStop)
        {
            _moreFolders = moreFolders;
            _onStart = onStart;
            _onStop = onStop;
        }

        public void Start(string path)
        {
            // Adding handlers - must wrap up all
            if (_moreFolders)
            {
                if (!_watchers.ContainsKey(path))
                {
                    _onStart.Invoke(path, true);

                    var fileSystemWatcher = RegisterSingleFolder(path);

                    _watchers.Add(path, fileSystemWatcher);
                }
            }
        }

        public FileSystemWatcher RegisterSingleFolder(string path)
        {
            _fileSystemWatcher = new FileSystemWatcher(path);
            _fileSystemWatcher.Filter = "*.cs";
            if (!_moreFolders)
            {
                _fileSystemWatcher.IncludeSubdirectories = true;
            }
            _fileSystemWatcher.NotifyFilter = NotifyFilters.Attributes |
    NotifyFilters.CreationTime |
    NotifyFilters.FileName |
    NotifyFilters.LastAccess |
    NotifyFilters.LastWrite |
    NotifyFilters.Size |
    NotifyFilters.Security;

            _fileSystemWatcher.Deleted += FileSystemWatcher_Deleted;
            _fileSystemWatcher.Changed += FileSystemWatcher_Changed;
            _fileSystemWatcher.Renamed += FileSystemWatcher_Renamed;

            _fileSystemWatcher.EnableRaisingEvents = true;

            //fileSystemWatcher.SynchronizingObject;
            //fileSystemWatcher.InitializeLifetimeService();

            return _fileSystemWatcher;
        }

        public void Stop(string path)
        {
            _onStop.Invoke(path, true);

            if (_moreFolders)
            {
                _watchers.Remove(path);

                FileSystemWatcher fileSystemWatcher = _watchers[path];
                fileSystemWatcher.Deleted -= FileSystemWatcher_Deleted;
                fileSystemWatcher.Changed -= FileSystemWatcher_Changed;
                fileSystemWatcher.Renamed -= FileSystemWatcher_Renamed;
                fileSystemWatcher.Dispose();
            }

            // During delete call onStop which call this method
        }

        private void FileSystemWatcher_Renamed(object sender, RenamedEventArgs e)
        {
            _onStop.Invoke(e.OldFullPath, true);
            _onStart.Invoke(e.FullPath, true);
        }

        private void FileSystemWatcher_Changed(object sender, FileSystemEventArgs e)
        {
            _onStop.Invoke(e.FullPath, true);
            _onStart.Invoke(e.FullPath, true);
        }

        private void FileSystemWatcher_Deleted(object sender, FileSystemEventArgs e)
        {
            _onStop.Invoke(e.FullPath, true);
        }
    }
}
