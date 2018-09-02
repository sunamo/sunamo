namespace UniversalYouTubeExtractor
{
    /// <summary>
    /// The video type. Also known as video container.
    /// </summary>
    public enum VideoStreamType
    {
        /// <summary>
        /// Video for mobile devices (3GP).
        /// </summary>
        Mobile,

        Flash,
        Mp4,
        WebM,

        /// <summary>
        /// The video type is unknown. This can occur if UniversalYouTubeExtractor is not up-to-date.
        /// </summary>
        Unknown
    }
}
