using sunamo.Helpers.Number;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


    public class CLProgressBar : ProgressState
    {
    

    public void Init()
    {
        base.Init(LyricsHelper_OverallSongs, LyricsHelper_AnotherSong, delegate { CL.WriteProgressBarEnd(); });
    }
    private void LyricsHelper_WriteProgressBarEnd()
    {
        CL.WriteProgressBarEnd();
    }

    PercentCalculator pc = null;

    public void LyricsHelper_OverallSongs(int obj)
    {

        pc = new PercentCalculator(obj);
        CL.WriteProgressBar(0);
    }

    public void LyricsHelper_AnotherSong(int i)
    {
        pc.AddOnePercent();
        CL.WriteProgressBar((int)pc.last, new WriteProgressBarArgs(true, i, pc._overallSum));
    }

}

