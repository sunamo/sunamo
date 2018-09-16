﻿//Declare a class that inherits from ToolStripControlHost.
using System.Windows.Forms;
using System;
public class ToolStripMonthCalendar : ToolStripControlHost
{
    // Call the base constructor passing in a MonthCalendar instance.
    public ToolStripMonthCalendar() : base(new MonthCalendar()) { }

    public MonthCalendar mc
    {
        get
        {
            return Control as MonthCalendar;
        }
    }

    // Expose the MonthCalendar.FirstDayOfWeek as a property.
    public Day FirstDayOfWeek
    {
        get
        {
            return mc.FirstDayOfWeek;
        }
        set { mc.FirstDayOfWeek = value; }
    }
    public int MaxSelectionDays
    {
        set
        {
            mc.MaxSelectionCount = value;
        }
    }
    // Expose the AddBoldedDate method.
    public void AddBoldedDate(DateTime dateToBold)
    {
        mc.AddBoldedDate(dateToBold);
    }

    // Subscribe and unsubscribe the control events you wish to expose.
    protected override void OnSubscribeControlEvents(Control c)
    {
        // Call the base so the base events are connected.
        base.OnSubscribeControlEvents(c);

        // Cast the control to a MonthCalendar control.
        MonthCalendar monthCalendarControl = (MonthCalendar)c;

        // Add the event.
        monthCalendarControl.DateChanged +=
            new DateRangeEventHandler(OnDateChanged);
    }

    protected override void OnUnsubscribeControlEvents(Control c)
    {
        // Call the base method so the basic events are unsubscribed.
        base.OnUnsubscribeControlEvents(c);

        // Cast the control to a MonthCalendar control.
        MonthCalendar monthCalendarControl = (MonthCalendar)c;

        // Remove the event.
        monthCalendarControl.DateChanged -=
            new DateRangeEventHandler(OnDateChanged);
    }

    // Declare the DateChanged event.
    public event DateRangeEventHandler DateChanged;

    // Raise the DateChanged event.
    private void OnDateChanged(object sender, DateRangeEventArgs e)
    {
        if (DateChanged != null)
        {
            DateChanged(this, e);
        }
    }
}
