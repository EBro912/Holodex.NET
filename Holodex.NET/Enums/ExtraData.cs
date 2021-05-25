using System;

namespace Holodex.NET.Enums
{
    /// <summary>
    /// A class which contains strings that allow extra data to be returned when requesting videos.
    /// </summary>
    public static class ExtraData
    {
        /// <summary>
        /// Include clips using the videos.
        /// </summary>
        public const string Clips = "clips";

        /// <summary>
        /// Include videos that refer to other videos.
        /// </summary>
        public const string Refers = "refers";

        /// <summary>
        /// Include sources for videos created by Subbers.
        /// </summary>
        public const string Sources = "sources";

        /// <summary>
        /// Include simulcast videos alongside the videos.
        /// </summary>
        public const string Simulcasts = "simulcasts";

        /// <summary>
        /// Include channels that are mentioned.
        /// </summary>
        public const string Mentions = "mentions";

        /// <summary>
        /// Include video descriptions.
        /// </summary>
        public const string Description = "description";

        /// <summary>
        /// Include live streams.
        /// </summary>
        public const string LiveInfo = "live_info";

        /// <summary>
        /// Include channel stats.
        /// </summary>
        public const string ChannelStats = "channel_stats";

        /// <summary>
        /// Include any songs used in the videos.
        /// </summary>
        public const string Songs = "songs";
    }
}
