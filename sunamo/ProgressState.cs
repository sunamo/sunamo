using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class ProgressState
{
    public  bool isRegistered = false;

    public void Init(Action<int> OverallSongs, Action<int> AnotherSong,  Action WriteProgressBarEnd)
    {
        isRegistered = true;

        this.AnotherSong += AnotherSong;
        this.OverallSongs += OverallSongs;
        this.WriteProgressBarEnd += WriteProgressBarEnd;
    }

    public event Action<int> AnotherSong;
    public event Action<int> OverallSongs;
    public event Action WriteProgressBarEnd;

    public void OnAnotherSong(int n)
    {
       AnotherSong(n);
    }

    public void OnOverallSongs(int n)
    {
        OverallSongs(n);
    }

    public void OnWriteProgressBarEnd()
    {
        WriteProgressBarEnd();
    }
}
