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
        

        a.Invoke();
        if (Tick != null)
        {
            Tick();
        }

       
    }
}