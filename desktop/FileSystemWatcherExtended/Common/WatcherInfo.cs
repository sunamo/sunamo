using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

    /// <summary>
    /// Contains settings that initializes the filesystem watchers created within the 
    /// Watcher class.
    /// </summary>
    public class WatcherInfo
    {
        string watchPath = null;
        public string WatchPath
        {
            get
            {
                return watchPath;
            }
            set
            {
                watchPath = value;
            }
        }

    bool includeSubFolders = false;
        public bool IncludeSubFolders
        {
            get
            {
                return includeSubFolders;
            }
            set
            {
                includeSubFolders = value;
            }
        }bool watchForError = false;
        public bool WatchForError
        {
            get
            {
                return watchForError;
            }
            set
            {
                watchForError = value;
            }
        }bool watchForDisposed = false;
        public bool WatchForDisposed
        {
            get
            {
                return watchForDisposed;
            }
            set
            {
                watchForDisposed = value;
            }
        }System.IO.NotifyFilters changesFilters = NotifyFilters.FileName;
        public System.IO.NotifyFilters ChangesFilters
        {
            get
            {
                return changesFilters;
            }
            set
            {
                changesFilters = value;
            }
        }WatcherChangeTypes watchesFilters = WatcherChangeTypes.All;
        public WatcherChangeTypes WatchesFilters
        {
            get
            {
                return watchesFilters;
            }
            set
            {
                watchesFilters = value;
            }
        }string fileFilter = null;
        public string FileFilter
        {
            get
            {
                return fileFilter;
            }
            set
            {
                fileFilter = value;
            }
        }uint bufferKBytes = 0;
        public uint BufferKBytes
        {
            get
            {
                return bufferKBytes;
            }
            set
            {
                bufferKBytes = value;
            }
        }int monitorPathInterval = 0;
        public int MonitorPathInterval
        {
            get
            {
                return monitorPathInterval;
            }
            set
            {
                monitorPathInterval = value;
            }
        }

        // -------------------------------------------------------------------------------
        public WatcherInfo()
        {
            this.WatchPath = "";
            this.IncludeSubFolders = false;
            this.WatchForError = false;
            this.WatchForDisposed = false;
            this.ChangesFilters = NotifyFilters.Attributes;
            this.WatchesFilters = WatcherChangeTypes.All;
            this.FileFilter = "";
            this.BufferKBytes = 8;
            this.MonitorPathInterval = 0;
        }
    }
