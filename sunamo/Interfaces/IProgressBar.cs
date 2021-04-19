public interface IProgressBar
{
    void Init();
    void LyricsHelper_AnotherSong();
    void LyricsHelper_AnotherSong(int i);
    void LyricsHelper_OverallSongs(int obj);
    void LyricsHelper_WriteProgressBarEnd();
    bool isRegistered { get; set; }
}