using System;
using System.Threading.Tasks;
using Windows.Storage;
using wsf;

namespace UniversalYouTubeExtractor
{
    /// <summary>
    /// Provides the base class for the <see cref="YouTubeAudio"/> and <see cref="YouTubeVideo"/> class.
    /// </summary>
    public abstract class YouTubeDownloader
    {
        public static string GetVideoExtension(VideoStreamType VideoType)
        {
            switch (VideoType)
            {
                case VideoStreamType.Mp4:
                    return ".mp4";

                case VideoStreamType.Mobile:
                    return ".3gp";

                case VideoStreamType.Flash:
                    return ".flv";

                case VideoStreamType.WebM:
                    return ".webm";
            }

            return null;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="YouTubeDownloader"/> class.
        /// </summary>
        /// <param name="video">The video to download/convert.</param>
        /// <param name="savePath">The path to save the video/audio.</param>
        /// /// <param name="bytesToDownload">An optional value to limit the number of bytes to download.</param>
        /// <exception cref="ArgumentNullException"><paramref name="video"/> or <paramref name="savePath"/> is <c>null</c>.</exception>
        protected YouTubeDownloader(Format video, StorageFile savePath, int? bytesToDownload = null)
        {
            this.Video = video;
            this.SavePath = savePath;
            this.BytesToDownload = bytesToDownload;
        }

        /// <summary>
        /// Occurs when the download finished.
        /// </summary>
        public event EventHandler DownloadFinished;

        /// <summary>
        /// Occurs when the download is starts.
        /// </summary>
        public event EventHandler DownloadStarted;

        /// <summary>
        /// Gets the number of bytes to download. <c>null</c>, if everything is downloaded.
        /// </summary>
        public int? BytesToDownload { get; private set; }

        /// <summary>
        /// Gets the path to save the video/audio.
        /// </summary>
        public StorageFile SavePath { get; private set; }

        /// <summary>
        /// Gets the video to download/convert.
        /// </summary>
        public Format Video { get; private set; }

        /// <summary>
        /// Starts the work of the <see cref="YouTubeDownloader"/>.
        /// </summary>
        public  abstract Task<string> DoWork();

        protected void OnDownloadFinished(EventArgs e)
        {
            if (this.DownloadFinished != null)
            {
                this.DownloadFinished(this, e);
            }
        }

        protected void OnDownloadStarted(EventArgs e)
        {
            if (this.DownloadStarted != null)
            {
                this.DownloadStarted(this, e);
            }
        }
    }
}
