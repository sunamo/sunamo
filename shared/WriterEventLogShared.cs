using sunamo;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using sunamo.Essential;

public static partial class WriterEventLog{
    static EventLog mainEventLogOfApplication = null;
    public const string ApplicationLogName = "Application";
    static EventLog eventLogWindowsApplication = null;
    static string scz = "sunamo.cz";

    public static void WriteToMainAppLogScz(string text, EventLogEntryType type)
    {
        WriteToWindowsLogs(scz, text, type);
    }

/// <summary>
    /// Zda to funguje nevím, já už jsem v aplikaci zapisoval metodou WriteToMainAppLog a žádná aplikace nemůže zapisovat do více logů
    /// </summary>
    /// <param name = "text"></param>
    /// <param name = "type"></param>
    public static void WriteToWindowsLogs(string appName, string text, EventLogEntryType type)
    {
        // Exists every time. Cant iterate - SecurityException will be happen in asp.net app
        if (!EventLog.SourceExists(appName))
        {
            EventLog.CreateEventSource(new EventSourceCreationData(appName, ApplicationLogName));
        }

        if (eventLogWindowsApplication == null)
        {
            eventLogWindowsApplication = new EventLog();
            eventLogWindowsApplication.Source = appName;
        }

        eventLogWindowsApplication.WriteEntry(text, type);
    }
}