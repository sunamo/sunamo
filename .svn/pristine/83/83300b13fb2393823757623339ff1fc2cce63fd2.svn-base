using System;
public class LimitedTimer
{
    System.Timers.Timer t = null;
    Action a = null;
    int pocet = 0;
    int odbylo = 0;
    public event VoidVoid Tick;

    public LimitedTimer(int ms, int pocet, Action a)
    {
        t = new System.Timers.Timer(ms);
        t.Elapsed += t_Elapsed;

        this.pocet = pocet;
        this.a = a;
        t.Start();
    }
    
    void t_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
    {
        odbylo++;

        a.Invoke();
        Tick();
        if (pocet == odbylo)
        {
            //Debug.Print(pocet.ToString());
            t.Stop();
        }
    }
}
