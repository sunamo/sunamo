using UniversalYouTubeExtractor;
using System.Collections.Generic;


namespace UniversalYouTubeExtractor
{
    public class Format
    {
        public static List<Format> Defaults = new List<Format>
        {
            /* Non-adaptive */
            new Format(5, VideoStreamType.Flash, 240, false, AudioStreamType.Mp3, 64, AdaptiveType.None),
            new Format(6, VideoStreamType.Flash, 270, false, AudioStreamType.Mp3, 64, AdaptiveType.None),
            new Format(13, VideoStreamType.Mobile, 0, false, AudioStreamType.Aac, 0, AdaptiveType.None),
            new Format(17, VideoStreamType.Mobile, 144, false, AudioStreamType.Aac, 24, AdaptiveType.None),
            new Format(18, VideoStreamType.Mp4, 360, false, AudioStreamType.Aac, 96, AdaptiveType.None),
            new Format(22, VideoStreamType.Mp4, 720, false, AudioStreamType.Aac, 192, AdaptiveType.None),
            new Format(34, VideoStreamType.Flash, 360, false, AudioStreamType.Aac, 128, AdaptiveType.None),
            new Format(35, VideoStreamType.Flash, 480, false, AudioStreamType.Aac, 128, AdaptiveType.None),
            new Format(36, VideoStreamType.Mobile, 240, false, AudioStreamType.Aac, 38, AdaptiveType.None),
            new Format(37, VideoStreamType.Mp4, 1080, false, AudioStreamType.Aac, 192, AdaptiveType.None),
            new Format(38, VideoStreamType.Mp4, 3072, false, AudioStreamType.Aac, 192, AdaptiveType.None),
            new Format(43, VideoStreamType.WebM, 360, false, AudioStreamType.Vorbis, 128, AdaptiveType.None),
            new Format(44, VideoStreamType.WebM, 480, false, AudioStreamType.Vorbis, 128, AdaptiveType.None),
            new Format(45, VideoStreamType.WebM, 720, false, AudioStreamType.Vorbis, 192, AdaptiveType.None),
            new Format(46, VideoStreamType.WebM, 1080, false, AudioStreamType.Vorbis, 192, AdaptiveType.None),

            /* 3d */
            new Format(82, VideoStreamType.Mp4, 360, true, AudioStreamType.Aac, 96, AdaptiveType.None),
            new Format(83, VideoStreamType.Mp4, 240, true, AudioStreamType.Aac, 96, AdaptiveType.None),
            new Format(84, VideoStreamType.Mp4, 720, true, AudioStreamType.Aac, 152, AdaptiveType.None),
            new Format(85, VideoStreamType.Mp4, 520, true, AudioStreamType.Aac, 152, AdaptiveType.None),
            new Format(100, VideoStreamType.WebM, 360, true, AudioStreamType.Vorbis, 128, AdaptiveType.None),
            new Format(101, VideoStreamType.WebM, 360, true, AudioStreamType.Vorbis, 192, AdaptiveType.None),
            new Format(102, VideoStreamType.WebM, 720, true, AudioStreamType.Vorbis, 192, AdaptiveType.None),

            /* Adaptive (aka DASH) - Video */
            new Format(133, VideoStreamType.Mp4, 240, false, AudioStreamType.Unknown, 0, AdaptiveType.Video),
            new Format(134, VideoStreamType.Mp4, 360, false, AudioStreamType.Unknown, 0, AdaptiveType.Video),
            new Format(135, VideoStreamType.Mp4, 480, false, AudioStreamType.Unknown, 0, AdaptiveType.Video),
            new Format(136, VideoStreamType.Mp4, 720, false, AudioStreamType.Unknown, 0, AdaptiveType.Video),
            new Format(137, VideoStreamType.Mp4, 1080, false, AudioStreamType.Unknown, 0, AdaptiveType.Video),
            new Format(138, VideoStreamType.Mp4, 2160, false, AudioStreamType.Unknown, 0, AdaptiveType.Video),
            new Format(160, VideoStreamType.Mp4, 144, false, AudioStreamType.Unknown, 0, AdaptiveType.Video),
            new Format(242, VideoStreamType.WebM, 240, false, AudioStreamType.Unknown, 0, AdaptiveType.Video),
            new Format(243, VideoStreamType.WebM, 360, false, AudioStreamType.Unknown, 0, AdaptiveType.Video),
            new Format(244, VideoStreamType.WebM, 480, false, AudioStreamType.Unknown, 0, AdaptiveType.Video),
            new Format(247, VideoStreamType.WebM, 720, false, AudioStreamType.Unknown, 0, AdaptiveType.Video),
            new Format(248, VideoStreamType.WebM, 1080, false, AudioStreamType.Unknown, 0, AdaptiveType.Video),
            new Format(264, VideoStreamType.Mp4, 1440, false, AudioStreamType.Unknown, 0, AdaptiveType.Video),
            new Format(271, VideoStreamType.WebM, 1440, false, AudioStreamType.Unknown, 0, AdaptiveType.Video),
            new Format(272, VideoStreamType.WebM, 2160, false, AudioStreamType.Unknown, 0, AdaptiveType.Video),
            new Format(278, VideoStreamType.WebM, 144, false, AudioStreamType.Unknown, 0, AdaptiveType.Video),

            /* Adaptive (aka DASH) - Audio */
            new Format(139, VideoStreamType.Mp4, 0, false, AudioStreamType.Aac, 48, AdaptiveType.Audio),
            new Format(140, VideoStreamType.Mp4, 0, false, AudioStreamType.Aac, 128, AdaptiveType.Audio),
            new Format(141, VideoStreamType.Mp4, 0, false, AudioStreamType.Aac, 256, AdaptiveType.Audio),
            new Format(171, VideoStreamType.WebM, 0, false, AudioStreamType.Vorbis, 128, AdaptiveType.Audio),
            new Format(172, VideoStreamType.WebM, 0, false, AudioStreamType.Vorbis, 192, AdaptiveType.Audio)
        };
   
        /// <summary>
        /// Konstruktor který se používá pokud jde o neznámý typ přeneseného videa - znám jen jeho itag/formatCode
        /// </summary>
        /// <param name="formatCode"></param>
        internal Format(int formatCode)
            : this(formatCode, VideoStreamType.Unknown, 0, false, AudioStreamType.Unknown, 0, AdaptiveType.None)
        { }

        /// <summary>
        /// Konstruktor který se používá pokud vím přesné parametry videa.
        /// </summary>
        /// <param name="info"></param>
        internal Format(Format info)
            : this(info.FormatCode, info.VideoType, info.Resolution, info.Is3D, info.AudioType, info.AudioBitrate, info.AdaptiveType)
        { }

        /// <summary>
        /// Konstruktor který se používá pokud vím přesné parametry videa.
        /// </summary>
        /// <param name="formatCode"></param>
        /// <param name="videoType"></param>
        /// <param name="resolution"></param>
        /// <param name="is3D"></param>
        /// <param name="audioType"></param>
        /// <param name="audioBitrate"></param>
        /// <param name="adaptiveType"></param>
        private Format(int formatCode, VideoStreamType videoType, int resolution, bool is3D, AudioStreamType audioType, int audioBitrate, AdaptiveType adaptiveType)
        {
            this.FormatCode = formatCode;
            this.VideoType = videoType;
            this.Resolution = resolution;
            this.Is3D = is3D;
            this.AudioType = audioType;
            this.AudioBitrate = audioBitrate;
            this.AdaptiveType = adaptiveType;
        }

        /// <summary>
        /// Gets an enum indicating whether the format is adaptive or not.
        /// </summary>
        /// <value>
        /// <c>AdaptiveType.Audio</c> or <c>AdaptiveType.Video</c> if the format is adaptive;
        /// otherwise, <c>AdaptiveType.None</c>.
        /// </value>
        public AdaptiveType AdaptiveType { get; private set; }

        /// <summary>
        /// The approximate audio bitrate in kbit/s.
        /// </summary>
        /// <value>The approximate audio bitrate in kbit/s, or 0 if the bitrate is unknown.</value>
        public int AudioBitrate { get; private set; }

        /// <summary>
        /// Gets the audio extension.
        /// </summary>
        /// <value>The audio extension, or <c>null</c> if the audio extension is unknown.</value>
        public string AudioExtension
        {
            get
            {
                switch (this.AudioType)
                {
                    case AudioStreamType.Aac:
                        return ".aac";

                    case AudioStreamType.Mp3:
                        return ".mp3";

                    case AudioStreamType.Vorbis:
                        return ".ogg";
                }

                return null;
            }
        }

        /// <summary>
        /// Gets the audio type (encoding).
        /// </summary>
        public AudioStreamType AudioType { get; private set; }

        /// <summary>
        /// Dává True pouze u Flash videí
        /// Gets a value indicating whether the audio of this video can be extracted by UniversalYouTubeExtractor.
        /// </summary>
        /// <value>
        /// <c>true</c> if the audio of this video can be extracted by UniversalYouTubeExtractor; otherwise, <c>false</c>.
        /// </value>
        public bool CanExtractAudio
        {
            get { return this.VideoType == VideoStreamType.Flash; }
        }

        /// <summary>
        /// Gets the download URL.
        /// </summary>
        public string DownloadUrl { get; internal set; }

        /// <summary>
        /// Gets the format code, that is used by YouTube internally to differentiate between
        /// quality profiles.
        /// </summary>
        public int FormatCode { get; private set; }

        public bool Is3D { get; private set; }

        /// <summary>
        /// Gets a value indicating whether this video info requires a signature decryption before
        /// the download URL can be used.
        ///
        /// This can be achieved with the <see cref="DownloadUH.DecryptDownloadUrl"/>
        /// </summary>
        public bool RequiresDecryption { get; internal set; }

        /// <summary>
        /// Gets the resolution of the video.
        /// </summary>
        /// <value>The resolution of the video, or 0 if the resolution is unkown.</value>
        public int Resolution { get; private set; }

        /// <summary>
        /// Gets the video title.
        /// </summary>
        public string Title { get; internal set; }

        /// <summary>
        /// Gets the video extension.
        /// </summary>
        /// <value>The video extension, or <c>null</c> if the video extension is unknown.</value>
        public string VideoExtension
        {
            get
            {
                return YouTubeDownloader.GetVideoExtension(this.VideoType);
            }
        }

        

        /// <summary>
        /// Gets the video type (container).
        /// </summary>
        public VideoStreamType VideoType { get; private set; }

        /// <summary>
        /// We use this in the <see cref="DownloadUH.DecryptDownloadUrl" /> method to
        /// decrypt the signature
        /// </summary>
        /// <returns></returns>
        internal string HtmlPlayerVersion { get; set; }

        public override string ToString()
        {
            return string.Format("Full Title: {0}, Type: {1}, Resolution: {2}p", this.Title + this.VideoExtension, this.VideoType, this.Resolution);
        }
    }
}
