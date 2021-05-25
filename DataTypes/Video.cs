using Holodex.NET.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;

namespace Holodex.NET.DataTypes
{
    public sealed class Video
    {
        /// <summary>
        /// The unique ID of the video.
        /// </summary>
        [JsonProperty("id")]
        public string VideoId { get; internal set; }

        /// <summary>
        /// The Language of the video. Only applies to videos created by Subbers.
        /// </summary>
        [JsonProperty("lang")]
        public string Language { get; internal set; }

        /// <summary>
        /// The video's title.
        /// </summary>
        [JsonProperty("title")]
        public string Title { get; internal set; }

        /// <summary>
        /// The type of video, whether it is a stream or a clip.
        /// </summary>
        [JsonProperty("type")]
        [JsonConverter(typeof(StringEnumConverter))]
        public VideoType VideoType { get; internal set; }

        /// <summary>
        /// The internal topic ID of the video. Some videos, mostly clips, may not have a topic.
        /// </summary>
        [JsonProperty("topic_id")]
        public string Topic { get; internal set; }

        /// <summary>
        /// The date the video was published at.
        /// </summary>
        [JsonProperty("published_at")]
        public DateTime? PublishedAt { get; internal set; }

        /// <summary>
        /// The date the video was made available. This takes on the first non-null value of <see cref="PublishedAt"/>,
        /// <see cref="ActualStart"/>, <see cref="ScheduledStart"/>, or <see cref="ActualEnd"/>.
        /// </summary>
        [JsonProperty("available_at")]
        public DateTime AvailableAt { get; internal set; }

        [JsonProperty("duration")]
        private int duration { get; set; }

        /// <summary>
        /// The duration of the video.
        /// </summary>
        public TimeSpan Duration { get { return TimeSpan.FromSeconds(duration); } }

        /// <summary>
        /// The current status of the video on YouTube. The <see cref="VideoStatus"/> class contains all statuses.
        /// </summary>
        [JsonProperty("status")]
        public string Status { get; internal set; }

        /// <summary>
        /// The date when the stream started. Used with <see cref="ExtraData.LiveInfo"/>
        /// </summary>
        [JsonProperty("start_scheduled")]
        public DateTime? ScheduledStart { get; internal set; }

        /// <summary>
        /// The date when the stream actually started. Used with <see cref="ExtraData.LiveInfo"/>
        /// </summary>
        [JsonProperty("start_actual")]
        public DateTime? ActualStart { get; internal set; }

        /// <summary>
        /// The date when the stream ended. Used with <see cref="ExtraData.LiveInfo"/>
        /// </summary>
        [JsonProperty("end_actual")]
        public DateTime? ActualEnd { get; internal set; }

        /// <summary>
        /// The number of people currently watching the stream. Used with <see cref="ExtraData.LiveInfo"/>
        /// </summary>
        [JsonProperty("live_viewers")]
        public int? LiveViewers { get; internal set; }

        /// <summary>
        /// The description of the video. Used with <see cref="ExtraData.Description"/>
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; internal set; }

        /// <summary>
        /// The number of tagged songs related to this video.
        /// </summary>
        [JsonProperty("songcount")]
        public int? SongCount { get; internal set; }

        /// <summary>
        /// The channel ID the video creator.
        /// </summary>
        [JsonProperty("channel_id")]
        public string ChannelId { get; internal set; }

        /// <summary>
        /// The <see cref="DataTypes.Channel"/> object of the video creator.
        /// </summary>
        [JsonProperty("channel")]
        public Channel Channel { get; internal set; }


        /// <summary>
        /// A list of comments on this video, usually with timestamps. Used when searching for a specific video.
        /// </summary>
        [JsonProperty("comments")]
        public IReadOnlyCollection<Comment> Comments { get; internal set; }

        /// <summary>
        /// A list of clips related to this video. Used with <see cref="ExtraData.Clips"/>
        /// </summary>
        [JsonProperty("clips")]
        public IReadOnlyCollection<Video> Clips { get; internal set; }

        /// <summary>
        /// A list of sources for videos uploaded by Subbers. Used with <see cref="ExtraData.Sources"/>. Has no effect on VTubers.
        /// </summary>
        [JsonProperty("sources")]
        public IReadOnlyCollection<Video> Sources { get; internal set; }

        /// <summary>
        /// A list of videos that are referred by this video. Used with <see cref="ExtraData.Refers"/>
        /// </summary>
        [JsonProperty("refers")]
        public IReadOnlyCollection<Video> Refers { get; internal set; }

        /// <summary>
        /// A list of videos that are simulcast on another channel. Used with <see cref="ExtraData.Simulcasts"/>
        /// </summary>
        [JsonProperty("simulcasts")]
        public IReadOnlyCollection<Video> Simulcasts { get; internal set; }

        /// <summary>
        /// A list of channels that are mentioned by this video. Used with <see cref="ExtraData.Mentions"/>
        /// </summary>
        [JsonProperty("mentions")]
        public IReadOnlyCollection<Channel> Mentions { get; internal set; }

        /// <summary>
        /// A list of songs used in this video. Used with <see cref="ExtraData.Songs"/>
        /// </summary>
        [JsonProperty("songs")]
        public IReadOnlyCollection<Song> Songs { get; internal set; } 

    }
}
