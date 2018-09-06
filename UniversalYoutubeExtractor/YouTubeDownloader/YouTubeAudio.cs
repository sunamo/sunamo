using UniversalYouTubeExtractor;
using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using UniversalId3;

using Windows.Storage;
using wsf;

namespace UniversalYouTubeExtractor
{
    /// <summary>
    /// AudioDownloader
    /// Provides a method to download a video and extract its audio track.
    /// </summary>
    public class YouTubeAudio : YouTubeDownloader
    {
        private bool isCanceled;
        protected SongFromInternet sfi = null;
        StorageFile sfVideo = null;

        /// <summary>
        /// Pokud nebudeš vědět co na A3 vyplnit, dej null
        /// Initializes a new instance of the <see cref="YouTubeAudio"/> class.
        /// </summary>
        /// <param name="video">The video to convert.</param>
        /// <param name="savePath">The path to save the audio.</param>
        /// /// <param name="bytesToDownload">An optional value to limit the number of bytes to download.</param>
        /// <exception cref="ArgumentNullException"><paramref name="video"/> or <paramref name="savePath"/> is <c>null</c>.</exception>
        public YouTubeAudio(Format video, StorageFile sfVideo, StorageFile savePath, int? bytesToDownload, SongFromInternet sfi)
            : base(video, savePath, bytesToDownload)
        {
            this.sfVideo = sfVideo;
            this.sfi = sfi;
        }

        /// <summary>
        /// Occurs when the progress of the audio extraction has changed.
        /// </summary>
        public event EventHandler<DownloadProgressEventArgs> AudioExtractionProgressChanged;

        /// <summary>
        /// Occurs when the download progress of the video file has changed.
        /// </summary>
        public event EventHandler<DownloadProgressEventArgs> DownloadProgressChanged;

        /// <summary>
        /// Soubor ve složce Videos si vytvoří podle VideoInfo objektu
        /// SavePath je pak název videa
        /// Downloads the video from YouTube and then extracts the audio track out if it.
        /// </summary>
        /// <exception cref="IOException">
        /// The temporary video file could not be created.
        /// - or -
        /// The audio file could not be created.
        /// </exception>
        /// <exception cref="AudioExtractionException">An error occured during audio extraction.</exception>
        /// <exception cref="WebException">An error occured while downloading the video.</exception>
        public async override Task<string> DoWork()
        {
            var tempPath = sfVideo;

            string vr = null;

            await this.DownloadVideo(tempPath);

            if (vr == null)
            {
                if (!this.isCanceled)
                {
                    vr = await this.ExtractAudio(tempPath);
                }
            }

            this.OnDownloadFinished(EventArgs.Empty);
            return vr;
        }

        

        private async Task DownloadVideo(StorageFile path)
        {
            var videoDownloader = new YouTubeVideo(this.Video, path, this.BytesToDownload);

            videoDownloader.DownloadProgressChanged += (sender, args) =>
            {
                if (this.DownloadProgressChanged != null)
                {
                    this.DownloadProgressChanged(this, args);

                    this.isCanceled = args.Cancel;
                }
            };

            await videoDownloader.DoWork();

            
        }

        /// <summary>
        /// Vyextrahuje audio z archívu A1 a přidá výsledné MP3 ID3 tagy
        /// </summary>
        /// <param name="tempPath"></param>
        /// <returns></returns>
        public async Task<string> ExtractAudio(StorageFile tempPath)
        {
            using (var flvFile = new FlvFile(tempPath, this.SavePath))
            {
                flvFile.ConversionProgressChanged += (sender, args) =>
                {
                    if (this.AudioExtractionProgressChanged != null)
                    {
                        this.AudioExtractionProgressChanged(this, new DownloadProgressEventArgs(args.ProgressPercentage));
                    }
                };

                try
                {
                    await flvFile.ExtractStreams();
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }

                ID3 id3 = new ID3();
                await id3.GetMusicPropertiesAsync(this.SavePath);
                id3.Artist = sfi.ArtistInConvention();
                id3.Title = sfi.TitleAndRemixInConvention();
                await id3.SaveMusicPropertiesAsync();

                return null;
            }
        }
    }
}
