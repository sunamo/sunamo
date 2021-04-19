using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public enum LyricsImageSize
{
    /// <summary>
    /// 34x34
    /// only lfm
    /// Important in Song and Interpret - small rounded images
    /// </summary>
    Small = 0,
    /// <summary>
    /// 64 spotify and lfm
    /// Is used nowhere
    /// </summary>
    Medium = 1,
    /// <summary>
    /// 174
    /// used nowhere
    /// 11/7
    /// </summary>
    Large = 2,
    /// <summary>
    /// 300(lfm), 320(spotify-artist), 300(spotify-album),
    /// lfm and spotify
    /// Only one item, must always compare for 300 and 320
    /// Is used in Album, Interpret, AddSong - Spotify, LastFm
    /// </summary>
    ExtraLarge = 3,
    /// <summary>
    /// 640
    /// </summary>
    Xxl = 4
}