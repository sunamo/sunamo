using System;
public class SunamoTimer
{
    protected System.Timers.Timer t = null;
    Action a = null;    
    public event VoidVoid Tick;

    public SunamoTimer(int ms,  Action a, bool runImmediately)
    {
        t = new System.Timers.Timer(ms);
        t.Elapsed += t_Elapsed;
        t.AutoReset = true;
        
        this.a = a;
        t.Start();

        if (runImmediately)
        {
            t_Elapsed(null, null);
        }
    }

    void t_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
    {
        try
        {
            a.Invoke();
        }
        catch (Exception)
        {
            // often The calling thread cannot access this object because a different thread owns it.'
        }
        if (Tick != null)
        {
            Tick();
        }

       
    }
}