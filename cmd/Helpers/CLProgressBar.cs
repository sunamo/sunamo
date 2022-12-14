using sunamo.Essential;
using sunamo.Helpers.Number;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class CLProgressBar : ProgressState, IProgressBar
{
    int _writeOnlyDividableBy = 0;
    bool isWriteOnlyDividableBy = false;
    public bool isNotUt = false;
    public int writeOnlyDividableBy
    {
        get
        {
            return _writeOnlyDividableBy;
        }
        set
        {
            _writeOnlyDividableBy = value;
            isWriteOnlyDividableBy = value != 0;
        }
    }

    public void Init()
    {
        if (isNotUt)
        {
            Init(LyricsHelper_OverallSongs, LyricsHelper_AnotherSong, LyricsHelper_WriteProgressBarEnd);
        }
        
    }

    public void LyricsHelper_WriteProgressBarEnd()
    {
        if (isNotUt)
        {
            if (isWriteOnlyDividableBy)
            {
                CL.ClearCurrentConsoleLine();
                CL.WriteLine(n + " Finished");
            }
            else
            {
                CL.WriteProgressBarEnd();
            }
        }
    }

    PercentCalculator pc = null;

    public void LyricsHelper_OverallSongs(int obj)
    {
        if (isNotUt)
        {
            n = 0;
            if (isWriteOnlyDividableBy)
            {
                CL.WriteLine("Starting...");
            }
            else
            {
                pc = new PercentCalculator(obj);
                CL.WriteProgressBar(0);
            }
        }
    }

    public void LyricsHelper_AnotherSong()
    {
        if (isNotUt)
        {
            n++;
            LyricsHelper_AnotherSong(n);
        }
    }

    public void LyricsHelper_AnotherSong(int i)
    {
        if (isNotUt)
        {
            if (isWriteOnlyDividableBy)
            {
                if (i % writeOnlyDividableBy == 0)
                {
                    CL.ClearCurrentConsoleLine();
                    TypedSunamoLogger.Instance.Information(i.ToString());
                }
            }
            else
            {
                pc.AddOnePercent();
                CL.WriteProgressBar((int)pc.last, new WriteProgressBarArgs(true, i, pc._overallSum));
            }
        }
    }

    /// <summary>
    /// private coz should be called only in this class
    /// </summary>
    /// <returns></returns>
    private bool IsDividable()
    {
        if (isNotUt)
        {
            if (isWriteOnlyDividableBy)
            {
                return n % writeOnlyDividableBy == 0;

            }
        }
        { }
        return true;
    }
}