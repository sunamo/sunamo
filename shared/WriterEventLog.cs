using sunamo;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using sunamo.Essential;
public static partial class WriterEventLog
{
    /// <summary>
    /// Potřebuje vždy admin práva pro běh programu
    /// </summary>
    public static void DeleteMainAppLog()
    {
        if (EventLog.Exists(ThisApp.Name))
        {
            EventLog.Delete(ThisApp.Name);
        }
    }

    /// <summary>
    /// Cant use in Web - SecurityException. Use WriteToWindowsLogsApplication()
    /// K prvnímu volání potřebuje administrátorská práva, aby mohla vytvořil log s názvem ThisApp.Name. 
    /// K zapisování událostí již pak administrátorská práva nepotřebuje.
    /// </summary>
    /// <param name = "text"></param>
    /// <param name = "type"></param>
    public static void WriteToMainAppLog(string text, EventLogEntryType type)
    {
        // Exception in ASP.NET: 'The source was not found, but some or all event logs could not be searched.  Inaccessible logs: Security.'
        // If the user account under which the code is running does not have read access to a subkey that it attempts to access (in your case, the Security subkey)
        string name = ThisApp.Name;
        CreateMainAppLog(name);
        if (mainEventLogOfApplication == null)
        {
            //logName, Environment.MachineName, ThisApp.Name - Tyto parametry jsem předával do konstruktoru EventLog ale s nimi to nikdy nefungovalo, nevyhodila se sice výjimka ale v EventLogu nic nebylo
            mainEventLogOfApplication = new EventLog(name);
            mainEventLogOfApplication.Source = name;
        }

        mainEventLogOfApplication.WriteEntry(text, type);
    }

    public static bool CreateMainAppLogScz()
    {
        bool b = CreateMainAppLog(scz);
        WriteToMainAppLogScz("Template", EventLogEntryType.Information);
        return b;
    }

    public static bool CreateMainAppLog(string name)
    {
        bool existsSource = false;
        try
        {
            existsSource = EventLog.SourceExists(name);
        }
        catch (Exception)
        {
        }

        if (!existsSource)
        {
            // Zkoušel jsme zadávat do metody CreateEventSource argumenty source, logName s hodnotami name, logName ale s těmi to taky nefungovalo
            EventLog.CreateEventSource(new EventSourceCreationData(name, name));
        }

        return existsSource;
    }
}