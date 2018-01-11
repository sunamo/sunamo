using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.IO;
using System.Text;

    /// <summary>
    /// This is the main class (and the one you'll use directly). Create an instance of 
    /// the class (passing in a WatcherInfo object for intialization), and then attach 
    /// event handlers to this object.  One or more watchers will be created to handle 
    /// the various events and filters, and will marshal these evnts into a single set 
    /// from which you can gather info.
    /// </summary>
    public class WatcherEx : IDisposable
    {
        #region Data Members
        private bool disposed = false;
        private WatcherInfo watcherInfo = null;
        private WatchersExList watchers = new WatchersExList();
        #endregion Data Members

        #region Event Definitions
        public event FileSystemEventHandler ChangedAttribute = delegate { };
        public FileSystemEventHandler ChangedCreationTime = delegate { };
        public FileSystemEventHandler ChangedDirectoryName = delegate { };
        public FileSystemEventHandler ChangedFileName = delegate { };
        public FileSystemEventHandler ChangedLastAccess = delegate { };
        public FileSystemEventHandler ChangedLastWrite = delegate { };
        public FileSystemEventHandler ChangedSecurity = delegate { };
        public FileSystemEventHandler ChangedSize = delegate { };
        public FileSystemEventHandler Created = delegate { };
        public FileSystemEventHandler Deleted = delegate { };
        public RenamedEventHandler Renamed = delegate { };
        public ErrorEventHandler Error = delegate { };
        public EventHandler Disposed = delegate { };
        public PathAvailabilityHandler PathAvailability = delegate { };
        private string p;
        private string p_2;
        #endregion Event Definitions

        #region Constructors
        // -------------------------------------------------------------------------------
        public WatcherEx(WatcherInfo info)
        {
            if (info == null)
            {
                throw new Exception("WatcherInfo object cannot be null");
            }
            this.watcherInfo = info;

            // Zaregistruj� se pouze ty handlery zm�n, kter� si budu p��t v prom�nn� ChangesFilters v��tu NotifyFilters
            Initialize();
        }

        public WatcherEx(string p, string p_2)
        {
            // TODO: Complete member initialization
            this.p = p;
            this.p_2 = p_2;
        }
        #endregion Constructors

        #region Dispose Methods
        // -------------------------------------------------------------------------------
        /// <summary>
        /// Disposes all of the FileSystemWatcher objects, and disposes this object.
        /// </summary>
        public void Dispose()
        {
            ////Debug.WriteLine("WatcherEx.Dispose()");
            if (!this.disposed)
            {
                DisposeWatchers();
                this.disposed = true;
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
        }

        // -------------------------------------------------------------------------------
        /// <summary>
        /// Disposes of all of our watchers (called from Dispose, or as a result of 
        /// loosing access to a folder)
        /// </summary>
        public void DisposeWatchers()
        {
            ////Debug.WriteLine("WatcherEx.DisposeWatchers()");
            for (int i = 0; i < this.watchers.Count; i++)
            {
                this.watchers[i].Dispose();
            }
            this.watchers.Clear();
        }
        #endregion Dispose Methods

        #region Helper Methods
        // -------------------------------------------------------------------------------
        /// <summary>
        /// Determines if the specified NotifyFilter item has been specified to be 
        /// handled by this object.
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public bool HandleNotifyFilter(NotifyFilters filter)
        {
            return (((NotifyFilters)(this.watcherInfo.ChangesFilters & filter)) == filter);
        }

        // -------------------------------------------------------------------------------
        /// <summary>
        /// Determines if the specified WatcherChangeType item has been specified to be 
        /// handled by this object.
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public bool HandleWatchesFilter(WatcherChangeTypes filter)
        {
            return (((WatcherChangeTypes)(this.watcherInfo.WatchesFilters & filter)) == filter);
        }

        // -------------------------------------------------------------------------------
        /// <summary>
        /// Initializes this oibject by creating all of the required public 
        /// FileSystemWatcher objects necessary to mointor the folder/file for the 
        /// desired changes
        /// Zaregistruj� se pouze ty handlery zm�n, kter� si budu p��t v prom�nn� ChangesFilters v��tu NotifyFilters
        /// </summary>
        private void Initialize()
        {
            this.watcherInfo.BufferKBytes = Math.Max(4, Math.Min(this.watcherInfo.BufferKBytes, 64));

            CreateWatcher(false, this.watcherInfo.ChangesFilters);
            // create a change watcher for each NotifyFilter item
            CreateWatcher(true, NotifyFilters.Attributes);
            CreateWatcher(true, NotifyFilters.CreationTime);
            CreateWatcher(true, NotifyFilters.DirectoryName);
            CreateWatcher(true, NotifyFilters.FileName);
            CreateWatcher(true, NotifyFilters.LastAccess);
            CreateWatcher(true, NotifyFilters.LastWrite);
            CreateWatcher(true, NotifyFilters.Security);
            CreateWatcher(true, NotifyFilters.Size);

            ////Debug.WriteLine(string.Format("WatcherEx.Initialize() - {0} watchers created", this.watchers.Count));
        }


        // -------------------------------------------------------------------------------
        /// <summary>
        /// Actually creates the necessary FileSystemWatcher objects, depending oin which 
        /// notify filters and change types the user specified.
        /// </summary>
        /// <param name="changeType"></param>
        /// <param name="filter"></param>
        private void CreateWatcher(bool changedWatcher, NotifyFilters filter)
        {
            FileSystemWatcherEx watcher = null;
            int bufferSize = (int)this.watcherInfo.BufferKBytes * 1024;
            if (changedWatcher)
            {
                // if we're not handling the currently specified filter, get out
                if (HandleNotifyFilter(filter))
                {
                    watcher = new FileSystemWatcherEx(this.watcherInfo.WatchPath);
                    watcher.IncludeSubdirectories = this.watcherInfo.IncludeSubFolders;
                    watcher.Filter = this.watcherInfo.FileFilter;
                    watcher.NotifyFilter = filter;
                    watcher.InternalBufferSize = bufferSize;
                    switch (filter)
                    {
                        case NotifyFilters.Attributes:
                            watcher.Changed += new FileSystemEventHandler(watcher_ChangedAttribute);
                            break;
                        case NotifyFilters.CreationTime:
                            watcher.Changed += new FileSystemEventHandler(watcher_ChangedCreationTime);
                            break;
                        case NotifyFilters.DirectoryName:
                            watcher.Changed += new FileSystemEventHandler(watcher_ChangedDirectoryName);
                            break;
                        case NotifyFilters.FileName:
                            watcher.Changed += new FileSystemEventHandler(watcher_ChangedFileName);
                            break;
                        case NotifyFilters.LastAccess:
                            watcher.Changed += new FileSystemEventHandler(watcher_ChangedLastAccess);
                            break;
                        case NotifyFilters.LastWrite:
                            watcher.Changed += new FileSystemEventHandler(watcher_ChangedLastWrite);
                            break;
                        case NotifyFilters.Security:
                            watcher.Changed += new FileSystemEventHandler(watcher_ChangedSecurity);
                            break;
                        case NotifyFilters.Size:
                            watcher.Changed += new FileSystemEventHandler(watcher_ChangedSize);
                            break;
                    }
                }
            }
            else
            {
                if (HandleWatchesFilter(WatcherChangeTypes.Created) ||
                    HandleWatchesFilter(WatcherChangeTypes.Deleted) ||
                    HandleWatchesFilter(WatcherChangeTypes.Renamed) ||
                    this.watcherInfo.WatchForError ||
                    this.watcherInfo.WatchForDisposed)
                {
                    watcher = new FileSystemWatcherEx(this.watcherInfo.WatchPath, watcherInfo.MonitorPathInterval);
                    watcher.IncludeSubdirectories = this.watcherInfo.IncludeSubFolders;
                    watcher.Filter = this.watcherInfo.FileFilter;
                    watcher.InternalBufferSize = bufferSize;
                }

                if (HandleWatchesFilter(WatcherChangeTypes.Created))
                {
                    watcher.Created += new FileSystemEventHandler(watcher_CreatedDeleted);
                }
                if (HandleWatchesFilter(WatcherChangeTypes.Deleted))
                {
                    watcher.Deleted += new FileSystemEventHandler(watcher_CreatedDeleted);
                }
                if (HandleWatchesFilter(WatcherChangeTypes.Renamed))
                {
                    watcher.Renamed += new RenamedEventHandler(watcher_Renamed);
                }
                if (watcherInfo.MonitorPathInterval > 0)
                {
                    watcher.EventPathAvailability += new PathAvailabilityHandler(watcher_EventPathAvailability);
                }
            }
            if (watcher != null)
            {
                if (this.watcherInfo.WatchForError)
                {
                    watcher.Error += new ErrorEventHandler(watcher_Error);
                }
                if (this.watcherInfo.WatchForDisposed)
                {
                    watcher.Disposed += new EventHandler(watcher_Disposed);
                }
                this.watchers.Add(watcher);
            }
        }

        // -------------------------------------------------------------------------------
        /// <summary>
        /// Starts all of the public FileSystemWatcher objects by setting their 
        /// EnableRaisingEvents property to true.
        /// </summary>
        public void Start()
        {
            for (int i = 0; i < this.watchers.Count; i++)
            {
                this.watchers[i].EnableRaisingEvents = true;
                this.watchers[i].StartFolderMonitor();
            }
        }

        public bool Run
        {

            set
            {
                for (int i = 0; i < this.watchers.Count; i++)
                {
                    //FileSystemWatcherEx ex = (FileSystemWatcherEx) this.watchers[i];
                    this.watchers[i].Run = value;

                }
            }
        }

        // -------------------------------------------------------------------------------
        /// <summary>
        /// Stops all of the public FileSystemWatcher objects by setting their 
        /// EnableRaisingEvents property to true.
        /// </summary>
        public void Stop()
        {
            ////Debug.WriteLine("WatcherEx.Stop()");
            this.watchers[0].StopFolderMonitor();
            for (int i = 0; i < this.watchers.Count; i++)
            {
                this.watchers[i].EnableRaisingEvents = false;
            }
        }
        #endregion Helper Methods

        #region Native Watcher Events
        // -------------------------------------------------------------------------------
        /// <summary>
        /// Fired when the watcher responsible for monitoring attribute changes is 
        /// triggered.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void watcher_ChangedAttribute(object sender, FileSystemEventArgs e)
        {
            ////Debug.WriteLine("EVENT - Changed Attribute");
            ChangedAttribute(this, e);
        }

        // -------------------------------------------------------------------------------
        /// <summary>
        /// Fired when the watcher responsible for monitoring creation time changes is 
        /// triggered.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void watcher_ChangedCreationTime(object sender, FileSystemEventArgs e)
        {
            ////Debug.WriteLine("EVENT - Changed CreationTime");
            ChangedCreationTime(this, e);
        }

        // -------------------------------------------------------------------------------
        /// <summary>
        /// Fired when the watcher responsible for monitoring directory name changes is 
        /// triggered.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void watcher_ChangedDirectoryName(object sender, FileSystemEventArgs e)
        {
            ////Debug.WriteLine("EVENT - Changed DirectoryName");
            ChangedDirectoryName(this, e);
        }

        // -------------------------------------------------------------------------------
        /// <summary>
        /// Fired when the watcher responsible for monitoring file name changes is 
        /// triggered.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void watcher_ChangedFileName(object sender, FileSystemEventArgs e)
        {
            ////Debug.WriteLine("EVENT - Changed FileName");
            ChangedFileName(this, e);
        }

        // -------------------------------------------------------------------------------
        /// <summary>
        /// Fired when the watcher responsible for monitoring last access date/time 
        /// changes is triggered.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void watcher_ChangedLastAccess(object sender, FileSystemEventArgs e)
        {
            ////Debug.WriteLine("EVENT - Changed LastAccess");
            ChangedLastAccess(this, e);
        }

        // -------------------------------------------------------------------------------
        /// <summary>
        /// Fired when the watcher responsible for monitoring last write date/time 
        /// changes is triggered.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void watcher_ChangedLastWrite(object sender, FileSystemEventArgs e)
        {
            ////Debug.WriteLine("EVENT - Changed LastWrite");
            ChangedLastWrite(this, e);
        }

        // -------------------------------------------------------------------------------
        /// <summary>
        /// Fired when the watcher responsible for monitoring security changes is 
        /// triggered.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void watcher_ChangedSecurity(object sender, FileSystemEventArgs e)
        {
            ////Debug.WriteLine("EVENT - Changed Security");
            ChangedSecurity(this, e);
        }

        // -------------------------------------------------------------------------------
        /// <summary>
        /// Fired when the watcher responsible for monitoring size changes is 
        /// triggered.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void watcher_ChangedSize(object sender, FileSystemEventArgs e)
        {
            ////Debug.WriteLine("EVENT - Changed Size");
            ChangedSize(this, e);
        }

        // -------------------------------------------------------------------------------
        /// <summary>
        /// Fired when an public watcher is disposed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void watcher_Disposed(object sender, EventArgs e)
        {
            ////Debug.WriteLine("EVENT - Disposed");
            Disposed(this, e);
        }

        // -------------------------------------------------------------------------------
        /// <summary>
        /// Fired when the main watcher detects an error (the watcher that detected the 
        /// error is part of the event's arguments object)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void watcher_Error(object sender, ErrorEventArgs e)
        {
            ////Debug.WriteLine("EVENT - Error");
            Error(this, e);
        }

        // -------------------------------------------------------------------------------
        /// <summary>
        /// Fired when the main watcher detects a file rename.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void watcher_Renamed(object sender, RenamedEventArgs e)
        {
            ////Debug.WriteLine("EVENT - Renamed");
            Renamed(this, e);
        }

        // -------------------------------------------------------------------------------
        private void watcher_CreatedDeleted(object sender, FileSystemEventArgs e)
        {
            switch (e.ChangeType)
            {
                case WatcherChangeTypes.Created:
                    ////Debug.WriteLine("EVENT - Created");
                    Created(this, e);
                    break;
                case WatcherChangeTypes.Deleted:
                    ////Debug.WriteLine("EVENT - Changed Deleted");
                    Deleted(this, e);
                    break;
            }
        }

        // -------------------------------------------------------------------------------
        void watcher_EventPathAvailability(object sender, PathAvailablitiyEventArgs e)
        {
            ////Debug.WriteLine("EVENT - PathAvailability");
            PathAvailability(this, e);
            if (e.PathIsAvailable)
            {
                DisposeWatchers();
                Initialize();
            }
        }

        #endregion Native Watcher Events

    }

//}
