using System;
using Newtonsoft.Json;

namespace Holodex.NET.DataTypes
{
    public sealed class Song
    {
        /// <summary>
        /// The URL of the cover art.
        /// </summary>
        [JsonProperty("art")]
        public string ArtUrl { get; internal set; }

        /// <summary>
        /// The name of the song.
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; internal set; }

        private int start { get; set; }
        private int end { get; set; }

        /// <summary>
        /// The time in the associated <see cref="Video"/> where the song began.
        /// </summary>
        public TimeSpan StartTime { get { return TimeSpan.FromSeconds(start); } }
        /// <summary>
        /// The time in the associated <see cref="Video"/> where the song ended.
        /// </summary>
        public TimeSpan EndTime { get { return TimeSpan.FromSeconds(end);  } }

        /// <summary>
        /// The unique ID of the song on iTunes, if it is on iTunes.
        /// </summary>
        [JsonProperty("itunesid")]
        public ulong? iTunesId { get; internal set; }

        /// <summary>
        /// The original artist who created/sang the song.
        /// </summary>
        [JsonProperty("original_artist")]
        public string Artist { get; internal set; }

    }
}
