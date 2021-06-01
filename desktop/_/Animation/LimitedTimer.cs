using System;
public class LimitedTimer : SunamoTimer
{
    int pocet = 0;
    int odbylo = 0;

    public LimitedTimer(int ms, int pocet, Action a) : base(ms,a, false)
    {
        Tick += LimitedTimer_Tick;
        this.pocet = pocet;
    }

    private void LimitedTimer_Tick()
    {
        odbylo++;

        if (pocet == odbylo)
        {
            //////DebugLogger.Instance.WriteLine(pocet.ToString());
            t.Stop();
        }
    }

    void t_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
    {
        odbylo++;

        if (pocet == odbylo)
        {
            //////DebugLogger.Instance.WriteLine(pocet.ToString());
            t.Stop();
        }


    }
}