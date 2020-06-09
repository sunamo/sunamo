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
        static bool watch = false;

        /// <summary>
        /// In key are folders (never files), in value instance
        /// </summary>
        public FsWatcherDictionary<string, FileSystemWatcher> _watchers = new FsWatcherDictionary<string, FileSystemWatcher>();
        private VoidStringT<bool> _onStart;
        private VoidStringT<bool> _onStop;
        
        private FileSystemWatcher _fileSystemWatcher = null;
        public FileSystemWatchers(VoidStringT<bool> onStart, VoidStringT<bool> onStop)
        {
            if (watch)
            {
                _onStart = onStart;
                _onStop = onStop;

                var val = EnumHelper.GetValues<WatcherChangeTypes>();
                foreach (var item in val)
                {
                    lastProcessedFile.Add(item, string.Empty);
                    lastProcessedFileOld.Add(item, string.Empty);
                }
            }
        }

        /// <summary>
        /// Check whether folder is already indexing
        /// Is called from ProcessFile
        /// </summary>
        /// <param name="path"></param>
        public void Start(string path)
        {
            if (watch)
            {
                // Adding handlers - must wrap up all

                if (!_watchers.ContainsKey(path))
                {
                    var fileSystemWatcher = RegisterSingleFolder(path);



                    DictionaryHelper.AddOrSet<string, FileSystemWatcher>(_watchers, path, fileSystemWatcher);
                }
                else
                {
                    _watchers[path].EnableRaisingEvents = true;
                }
            }
        }

        /// <summary>
        /// Is called just from Start
        /// </summary>
        /// <param name="path"></param>
        private FileSystemWatcher RegisterSingleFolder(string path)
        {
            if (watch)
            {
                // A1 must be directory, never file
                _fileSystemWatcher = new FileSystemWatcher(path);
                _fileSystemWatcher.Filter = "*.cs";

                _fileSystemWatcher.IncludeSubdirectories = true;

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

                
            }
            return _fileSystemWatcher;
        }

        public void Stop(string path, bool fromFileSystemWatcher = false)
        {
            if (watch)
            {
                _onStop.Invoke(path, fromFileSystemWatcher);

                FileSystemWatcher fileSystemWatcher = _watchers[path];

                _watchers.Remove(path);

                fileSystemWatcher.EnableRaisingEvents = false;
                //fileSystemWatcher.Deleted -= FileSystemWatcher_Deleted;
                //fileSystemWatcher.Changed -= FileSystemWatcher_Changed;
                //fileSystemWatcher.Renamed -= FileSystemWatcher_Renamed;
                //fileSystemWatcher.Dispose();


                // During delete call onStop which call this method
            }
        }

        private void FileSystemWatcher_Renamed(object sender, RenamedEventArgs e)
        {
            if (watch)
            {
                if (lastProcessedFile[e.ChangeType] == e.FullPath)
                {
                    return;
                }

                if (lastProcessedFileOld[e.ChangeType] == e.OldFullPath)
                {
                    return;
                }

                lastProcessedFile[e.ChangeType] = e.FullPath;
                lastProcessedFileOld[e.ChangeType] = e.OldFullPath;

                bool existsNew = false;
                bool existsOld = false;

                try
                {
                    existsNew = FS.ExistsFile(e.FullPath);
                }
                catch (Exception)
                {
                }

                try
                {
                    existsOld = FS.ExistsFile(e.OldFullPath);
                }
                catch (Exception)
                {
                }

                if (existsOld || existsNew)
                {
                    _onStop.Invoke(e.OldFullPath, true);
                    _onStart.Invoke(e.FullPath, true);
                } 
            }
        }


        Dictionary<WatcherChangeTypes, string> lastProcessedFile = new Dictionary<WatcherChangeTypes, string>();
        Dictionary<WatcherChangeTypes, string> lastProcessedFileOld = new Dictionary<WatcherChangeTypes, string>();

        private void FileSystemWatcher_Changed(object sender, FileSystemEventArgs e)
        {
            if (watch)
            {
                if (lastProcessedFile[e.ChangeType] == e.FullPath)
                {
                    return;
                }

                lastProcessedFile[e.ChangeType] = e.FullPath;


                if (FS.ExistsFile(e.FullPath))
                {
                    _onStop.Invoke(e.FullPath, true);
                    _onStart.Invoke(e.FullPath, true);
                } 
            }
            
        }

        private void FileSystemWatcher_Deleted(object sender, FileSystemEventArgs e)
        {
            if (watch)
            {
                if (lastProcessedFile[e.ChangeType] == e.FullPath)
                {
                    return;
                }

                lastProcessedFile[e.ChangeType] = e.FullPath;


                _onStop.Invoke(e.FullPath, true); 
            }
        }
    }
}