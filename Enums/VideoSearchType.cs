namespace Holodex.NET.Enums
{
    /// <summary>
    /// A class that provides different search types when retrieving videos.
    /// </summary>
    public static class VideoSearchType
    {
        /// <summary>
        /// Retrieve clips including a VTuber
        /// </summary>
        public const string Clips = "clips";

        /// <summary>
        /// Retrieve videos uploaded by a Channel
        /// </summary>
        public const string Videos = "videos";

        /// <summary>
        /// Retrieve videos that mention a Channel
        /// </summary>
        public const string Collabs = "collabs";
    }
}
