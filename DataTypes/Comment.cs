using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Holodex.NET.DataTypes
{
    public sealed class Comment
    {
        /// <summary>
        /// The unique key associated with the comment.
        /// </summary>
        [JsonProperty("comment_key")]
        public string Key { get; internal set; }

        /// <summary>
        /// The video ID the comment is linked to.
        /// </summary>
        [JsonProperty("video_id")]
        public string VideoId { get; internal set; }

        /// <summary>
        /// The message content of the comment. Usually contains timestamps.
        /// </summary>
        [JsonProperty("message")]
        public string Content { get; internal set; }
    }
}
