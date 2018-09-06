using System;
using System.IO;
using System.Threading.Tasks;
using UniversalYouTubeExtractor;
using Windows.Storage;
using wsf;

namespace UniversalYouTubeExtractor
{
    internal class FlvFile : IDisposable
    {
        private  long fileLength;
        private StorageFile inputPath;
        private StorageFile outputPath;
        private IExtractAudio audioExtractor;
        private long fileOffset;
        private Stream fileStream;

        /// <summary>
        /// Initializes a new instance of the <see cref="FlvFile"/> class.
        /// </summary>
        /// <param name="inputPath">The path of the input.</param>
        /// <param name="outputPath">The path of the output without extension.</param>
        public FlvFile(StorageFile inputPath, StorageFile outputPath)
        {
            this.inputPath = inputPath;
            this.outputPath = outputPath;

            //this.fileStream = inputPath.OpenStreamForReadAsync(); //new FileStream(this.inputPath, FileMode.Open, FileAccess.Read, FileShare.Read, 64 * 1024);
            this.fileOffset = 0;
            //this.fileLength = fileStream.Length;
        }

        public event EventHandler<DownloadProgressEventArgs> ConversionProgressChanged;

        public bool ExtractedAudio { get; private set; }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }


        /// <summary>
        /// Vyhazuje hodně výjimek, musím odchytávat
        /// Nepodařilo se nainstalovat 
        /// AUtomaticky si zjistí 
        /// </summary>
        /// <exception cref="AudioExtractionException">The input file is not an FLV file.</exception>
        public async Task ExtractStreams()
        {
            this.fileStream = await inputPath.OpenStreamForReadAsync();
            this.fileLength = fileStream.Length;

            this.Seek(0);

            if (this.ReadUInt32() != 0x464C5601)
            {
                // not a FLV file
                throw new AudioExtractionException("Invalid input file. Impossible to extract audio track.");
            }

            this.ReadUInt8();
            uint dataOffset = this.ReadUInt32();

            this.Seek(dataOffset);

            this.ReadUInt32();

            while (fileOffset < fileLength)
            {
                if (!await CanExtractAudio())
                {
                    break;
                }

                if (fileLength - fileOffset < 4)
                {
                    break;
                }

                this.ReadUInt32();

                double progress = (this.fileOffset * 1.0 / this.fileLength) * 100;

                if (this.ConversionProgressChanged != null)
                {
                    this.ConversionProgressChanged(this, new DownloadProgressEventArgs(progress));
                }
            }

            this.CloseOutput(false);
        }

        private void CloseOutput(bool disposing)
        {
            if (this.audioExtractor != null)
            {
                if (disposing && this.audioExtractor.VideoPath != null)
                {
                    try
                    {
                        File.Delete(this.audioExtractor.VideoPath);
                    }
                    catch { }
                }

                this.audioExtractor.Dispose();
                this.audioExtractor = null;
            }
        }

        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this.fileStream != null)
                {
                    //this.fileStream.Close();
                    this.fileStream = null;
                }

                this.CloseOutput(true);
            }
        }

        private async Task< IExtractAudio> GetAudioWriter(uint mediaInfo)
        {
            uint format = mediaInfo >> 4;
            string path = outputPath.Path;
            Stream stream = await outputPath.OpenStreamForWriteAsync();
            switch (format)
            {
                case 14:
                case 2:
                    return new Mp3AudioExtractor(path, stream);

                case 10:
                    return new AacAudioExtractor(path, stream);
            }

            string typeStr;

            switch (format)
            {
                case 1:
                    typeStr = "ADPCM";
                    break;

                case 6:
                case 5:
                case 4:
                    typeStr = "Nellymoser";
                    break;

                default:
                    typeStr = "format=" + format;
                    break;
            }

            throw new AudioExtractionException("Unable to extract audio (" + typeStr + " is unsupported).");
        }

        

        /// <summary>
        /// ReadTag
        /// Sama si zjistí, o který typ se jedná a použije odpovídající třídu ExtractMp3/Aac
        /// </summary>
        /// <returns></returns>
        private async Task<bool> CanExtractAudio()
        {
            if (this.fileLength - this.fileOffset < 11)
                return false;

            // Read tag header
            uint tagType = ReadUInt8();
            uint dataSize = ReadUInt24();
            uint timeStamp = ReadUInt24();
            timeStamp |= this.ReadUInt8() << 24;
            this.ReadUInt24();

            // Read tag data
            if (dataSize == 0)
                return true;

            if (this.fileLength - this.fileOffset < dataSize)
                return false;

            uint mediaInfo = this.ReadUInt8();
            dataSize -= 1;
            byte[] data = this.ReadBytes((int)dataSize);

            if (tagType == 0x8)
            {
                // If we have no audio writer, create one
                if (this.audioExtractor == null)
                {
                    this.audioExtractor = await this.GetAudioWriter(mediaInfo);
                    this.ExtractedAudio = this.audioExtractor != null;
                }

                if (this.audioExtractor == null)
                {
                    throw new InvalidOperationException("No supported audio writer found.");
                }

                this.audioExtractor.WriteChunk(data, timeStamp);
            }

            return true;
        }

    }
}
