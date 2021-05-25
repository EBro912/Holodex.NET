using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Holodex.NET.Enums;
using Newtonsoft.Json.Converters;

namespace Holodex.NET.DataTypes
{
    public sealed class Channel
    {
        /// <summary>
        /// The unique ID of the channel.
        /// </summary>
        [JsonProperty("id")]
        public string ChannelId { get; internal set; }

        /// <summary>
        /// The channel's name.
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; internal set; }

        /// <summary>
        /// The channel's name in English, if it has one.
        /// </summary>
        [JsonProperty("english_name")]
        public string EnglishName { get; internal set; }

        /// <summary>
        /// The type of the channel, either VTuber or Subber.
        /// </summary>
        [JsonProperty("type")]
        [JsonConverter(typeof(StringEnumConverter))]
        public ChannelType ChannelType { get; internal set; }

        /// <summary>
        /// The channel's organization. Mainly used for VTubers only.
        /// </summary>
        [JsonProperty("org")]
        public string Organization { get; internal set; }

        // Some API endpoints return "suborg" instead of "group" so we handle it here
        [JsonProperty("suborg")]
        private string Suborganization { set { Group = value; } }

        /// <summary>
        /// The channel's group/suborganization. Mainly used for VTubers only, that allows them to be sorted internally.
        /// </summary>
        [JsonProperty("group")]
        public string Group { get; internal set; }

        /// <summary>
        /// The internal URL of the channel's profile picture.
        /// </summary>
        [JsonProperty("photo")]
        public string AvatarUrl { get; internal set; }

        /// <summary>
        /// The internal URL of the channel's channel banner.
        /// </summary>
        [JsonProperty("banner")]
        public string BannerUrl { get; internal set; }

        /// <summary>
        /// The channel's Twitter handle, if they have one.
        /// </summary>
        [JsonProperty("twitter")]
        public string TwitterName { get; internal set; }
        
        /// <summary>
        /// The number of videos the channel has uploaded.
        /// </summary>
        [JsonProperty("video_count")]
        public int? Videos { get; internal set; }

        /// <summary>
        /// The estimated amount of subscribers the channel has.
        /// </summary>
        [JsonProperty("subscriber_count")]
        public int? Subscribers { get; internal set; }

        /// <summary>
        /// The total number of views the channel has.
        /// </summary>
        [JsonProperty("view_count")]
        public int? Views { get; internal set; }

        /// <summary>
        /// The total number of clips associated with this channel. Mainly used for VTubers only.
        /// </summary>
        [JsonProperty("clip_count")]
        public int? Clips { get; internal set; }

        /// <summary>
        /// The language of the channel. Mainly used for Subbers only.
        /// </summary>
        [JsonProperty("lang")]
        public string Language { get; internal set; }

        /// <summary>
        /// The date this channel was created.
        /// </summary>
        [JsonProperty("published_at")]
        public DateTime? CreatedAt { get; internal set; }

        /// <summary>
        /// Whether or not the channel has been marked as inactive.
        /// </summary>
        [JsonProperty("inactive")]
        public bool IsInactive { get; internal set; }

        /// <summary>
        /// The channel's description on YouTube.
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; internal set; }
    }
}
