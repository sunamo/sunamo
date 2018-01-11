/// <summary>
/// This class allows us to pass any type of watcher arguments to the calling object's 
/// handler via a single object instead of having to add a lot of event handlers for 
/// the various event args types.
/// </summary>
using System.IO;

public class WatcherExEventArgs
{
    #region Properties
    FileSystemWatcherEx watcher = null;
    public FileSystemWatcherEx Watcher
    {
        get
        {
            return watcher;
        }
        set
        {
            watcher = value;
        }
    }object arguments = null;
    public object Arguments
    {
        get
        {
            return arguments;
        }
        set
        {
            arguments = value;
        }
    }ArgumentType argType = ArgumentType.StandardEvent;
    public ArgumentType ArgType
    {
        get
        {
            return argType;
        }
        set
        {
            argType = value;
        }
    }NotifyFilters filter = NotifyFilters.FileName;
    public NotifyFilters Filter
    {
        get
        {
            return filter;
        }
        set
        {
            filter = value;
        }
    }
    #endregion Properties

    #region Constructors
    public WatcherExEventArgs(FileSystemWatcherEx watcher,
                              object arguments,
                              ArgumentType argType,
                              NotifyFilters filter)
    {
        Watcher = watcher;
        Arguments = arguments;
        ArgType = argType;
        Filter = filter;
    }
    public WatcherExEventArgs(FileSystemWatcherEx watcher,
                              object arguments,
                              ArgumentType argType)
    {
        Watcher = watcher;
        Arguments = arguments;
        ArgType = argType;
        Filter = NotifyFilters.Attributes;
    }
    #endregion Constructors
}
