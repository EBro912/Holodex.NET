using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Holodex.NET.DataTypes;
using Holodex.NET.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


namespace Holodex.NET
{
    public class HolodexClient
    {
        private static readonly string url = "https://holodex.net/api/v2/";
        private static string key;
        private static HttpClient httpClient;

        /// <summary>
        /// Creates a new instance of a HolodexClient.
        /// </summary>
        /// <param name="apiKey">Your personal API key. Be aware that the validity of the key is not checked, so ensure it is correct.</param>
        /// <param name="client">An existing HttpClient, if needed. When left null, an internal client will be created</param>
        public HolodexClient(string apiKey, HttpClient client = null)
        {
            key = apiKey;
            // If an HttpClient is not provided, create one
            httpClient = client ?? new HttpClient();
            // API requires use of a key, so add it to the headers
            httpClient.DefaultRequestHeaders.Add("X-APIKEY", key);
        }

        /// <summary>
        /// Retrieves a list of channels that match the given parameters.
        /// </summary>
        /// <param name="lang">The channel langauge. Only applies to <see cref="ChannelType.Subber"/>. The <see cref="Language"/> class is available for all valid Language types.</param>
        /// <param name="limit">The number of results to return. Has a maximum of 50.</param>
        /// <param name="offset">How many results to offset by. Usually can be left at 0.</param>
        /// <param name="order">The order to sort the results by.</param>
        /// <param name="organization">The organization to search by. Only applies to <see cref="ChannelType.VTuber"/>. The <see cref="Organization"/> class is available for all valid Organizations.</param>
        /// <param name="sortByColumn">The column to sort by. This can be the name of any data member from the <see cref="Channel"/> class.</param>
        /// <param name="channelType">The <see cref="ChannelType"/> to search by.</param>
        public async Task<IReadOnlyCollection<Channel>> GetChannels(string lang = null, int limit = 25, int offset = 0,
            SortOrder order = SortOrder.Ascending, string organization = null, string sortByColumn = "org", ChannelType channelType = ChannelType.Both)
        {
            StringBuilder sb = new StringBuilder("channels?");
            if (lang != null)
            {
                sb.Append("&lang=");
                sb.Append(lang);
            }
            // Clamp between min and max channel count
            limit = Clamp(limit, 1, 50);
            sb.Append("&limit=");
            sb.Append(limit);

            sb.Append("&offset=");
            sb.Append(offset);

            sb.Append("&order=");
            sb.Append(order == SortOrder.Ascending ? "asc" : "desc");

            if (organization != null)
            {
                sb.Append("&org=");
                sb.Append(organization);
            }

            sb.Append("&sort=");
            sb.Append(sortByColumn.ToLower());

            sb.Append("&type=");
            if (channelType == ChannelType.VTuber)
                sb.Append("vtuber");
            else if (channelType == ChannelType.Subber)
                sb.Append("subber");

            JArray result = await RequestArray(sb.ToString());
            
            return result.ToObject<List<Channel>>();
        }

        /// <summary>
        /// Retrieves a single channel.
        /// </summary>
        /// <param name="channelId">The channel ID to look up.</param>
        /// <returns></returns>
        public async Task<Channel> GetChannel(string channelId)
        {
            string endpoint = $"channels/{channelId}";
            JObject channel = await RequestObject(endpoint);
            return channel.ToObject<Channel>();
        }

        /// <summary>
        /// Retrieves a single video, and any associated clips.
        /// </summary>
        /// <param name="videoId">The video ID to look up.</param>
        /// <param name="includeComments">Whether or not to include timestamped comments.</param>
        /// <param name="languages">A list of clip/subber languages to include. The <see cref="Language"/> class provides all valid languages.</param>
        public async Task<Video> GetVideo(string videoId, bool includeComments = false, string[] languages = null)
        {
            StringBuilder sb = new StringBuilder($"videos/{videoId}?");

            if (includeComments)
            {
                sb.Append("&c=1");
            }

            sb.Append("&lang=");
            if (languages != null)
            {
                for (int i = 0; i < languages.Length - 1; i++)
                {
                    sb.Append(languages[i]);
                    sb.Append(',');
                }
                // append final lang without comma
                sb.Append(languages[languages.Length - 1]);
            }
            else
                sb.Append("all");

            JObject result = await RequestObject(sb.ToString());
            return result.ToObject<Video>();

        }

        /// <summary>
        /// Retrieves a list of videos from a channel matching the given requirements.
        /// </summary>
        /// <param name="channelId">The channel ID to search videos on.</param>
        /// <param name="searchType">The search type. The <see cref="VideoSearchType"/> class provides all valid search types.</param>
        /// <param name="extraData">A list of any extra data to include. The <see cref="ExtraData"/> class provides all valid strings.</param>
        /// <param name="languages">A list of clip/subber languages to include. The <see cref="Language"/> class provides all valid languages.</param>
        /// <param name="limit">The number of videos to return. Has a maximum is 50.</param>
        /// <param name="offset">The number of videos to offset by. Can usually be left as 0.</param>
        public async Task<IReadOnlyCollection<Video>> GetVideosFromChannel(string channelId, string searchType,
            string[] extraData = null, string[] languages = null, int limit = 25, int offset = 0)
        {
            StringBuilder sb = new StringBuilder($"channels/{channelId}/{searchType}?");
            sb.Append("lang=");
            if (languages != null)
            {
                for (int i = 0; i < languages.Length - 1; i++)
                {
                    sb.Append(languages[i]);
                    sb.Append(',');
                }
                // append final lang without comma
                sb.Append(languages[languages.Length - 1]);
            }
            else
                sb.Append("all");

            limit = Clamp(limit, 1, 50);
            sb.Append("&limit=");
            sb.Append(limit);

            sb.Append("&offset=");
            sb.Append(offset);

            if (extraData != null)
            {
                sb.Append("&include=");
                for (int i = 0; i < extraData.Length - 1; i++)
                {
                    sb.Append(extraData[i]);
                    sb.Append(',');
                }
                // append final include without comma
                sb.Append(extraData[extraData.Length - 1]);
            }

            JArray result = await RequestArray(sb.ToString());

            return result.ToObject<List<Video>>();

        }

        /// <summary>
        /// Retrieves all live streams for the given channel IDs.
        /// <para>
        /// The most basic and quickest way to search for livestreams, using many default parameters.
        /// </para>
        /// </summary>
        /// <param name="channelIds">A list of channel IDs to search for live streams.</param>
        /// <returns></returns>
        public async Task<IReadOnlyCollection<Video>> GetLiveVideosByChannelId(string[] channelIds)
        {
            StringBuilder sb = new StringBuilder("users/live?");
            for (int i = 0; i < channelIds.Length - 1; i++)
            {
                sb.Append(channelIds[i]);
                sb.Append(',');
            }
            sb.Append(channelIds[channelIds.Length - 1]);
            JArray result = await RequestArray(sb.ToString());

            return result.ToObject<List<Video>>();
        }

        /// <summary>
        /// Retrieve all live streams for the given paramaters.
        /// <para>
        /// A more advanced way to search for live streams, however still uses a few default parameters.
        /// </para>
        /// </summary>
        /// <param name="channelId">The channel ID to retrieve videos from.</param>
        /// <param name="videoId">The video ID to search for.</param>
        /// <param name="languages">A list of clip/subber languages to include. The <see cref="Language"/> class provides all valid languages.</param>
        /// <param name="mentionedChannelId">Include any videos that mention a channel ID.</param>
        /// <param name="offset">The amount of videos to offset by. Can usually be left as 0.</param>
        /// <param name="organization">The organization to search for. The <see cref="Organization"/> class provides all valid organizations.</param>
        /// <param name="topic">The topic ID to search for.</param>
        /// <returns></returns>
        public async Task<IReadOnlyCollection<Video>> GetLiveVideos(string channelId = null, string videoId = null, string[] languages = null,
            string mentionedChannelId = null, int offset = 0, string organization = null, string topic = null)
        {
            StringBuilder sb = new StringBuilder("live?");
            if (channelId != null)
            {
                sb.Append("&channel_id=");
                sb.Append(channelId);
            }
            if (videoId != null)
            {
                sb.Append("&id=");
                sb.Append(videoId);
            }

            sb.Append("&lang=");
            if (languages != null)
            {
                for (int i = 0; i < languages.Length - 1; i++)
                {
                    sb.Append(languages[i]);
                    sb.Append(',');
                }
                sb.Append(languages[languages.Length - 1]);
            }
            else
                sb.Append("all");

            if (mentionedChannelId != null)
            {
                sb.Append("&mentioned_channel_id=");
                sb.Append(mentionedChannelId);
            }

            sb.Append("&offset=");
            sb.Append(offset);

            if (organization != null)
            {
                sb.Append("&org=");
                sb.Append(organization);
            }

            if (topic != null)
            {
                sb.Append("&topic");
                sb.Append(topic);
            }

            JArray result = await RequestArray(sb.ToString());

            return result.ToObject<List<Video>>();
        }

        /// <summary>
        /// Retrieve videos matching the given parameters.
        /// <para>
        /// The most advanced way to search for videos, using no default parameters.
        /// </para>
        /// </summary>
        /// <param name="channelId">The channel ID to retrieve videos from.</param>
        /// <param name="videoId">The video ID to search for.</param>
        /// <param name="extraData">Extra data to include in the response data. The <see cref="ExtraData"/> class provides all valid strings.</param>
        /// <param name="languages">A list of clip/subber languages to include. The <see cref="Language"/> class provides all valid languages.</param>
        /// <param name="limit">The number of videos to return. Has a maximum of 50.</param>
        /// <param name="maxUpcomingHours">The max hours to look up for upcoming videos. Helps exclude videos such as waiting rooms.</param>
        /// <param name="mentionedChannelId">Include any videos that mention a channel ID.</param>
        /// <param name="offset">The amount of videos to offset by. Can usually be left as 0.</param>
        /// <param name="order">The <see cref="SortOrder"/> to sort the videos by.</param>
        /// <param name="organization">The organization to search for. The <see cref="Organization"/> class provides all valid organizations.</param>
        /// <param name="sortColumn">The column to sort by. Can be the name of any <see cref="Video"/> data member.</param>
        /// <param name="status">The video status to search for. The <see cref="VideoStatus"/> class provides all valid video statuses.</param>
        /// <param name="topic">The topic ID to search for.</param>
        /// <param name="type">The <see cref="VideoType"/> to search for, either Stream or Clip.</param>
        public async Task<IReadOnlyCollection<Video>> GetVideos(string channelId = null, string videoId = null, string[] extraData = null,
            string[] languages = null, int limit = 25, uint? maxUpcomingHours = null, string mentionedChannelId = null, int offset = 0,
            SortOrder order = SortOrder.Descending, string organization = null, string sortColumn = "available_at", string status = null,
            string topic = null, VideoType type = VideoType.Both)
        {
            StringBuilder sb = new StringBuilder("videos?");
            if (channelId != null)
            {
                sb.Append("&channel_id=");
                sb.Append(channelId);
            }
            if (videoId != null)
            {
                sb.Append("&id=");
                sb.Append(videoId);
            }
            if (extraData != null)
            {
                sb.Append("&include=");
                for (int i = 0; i < extraData.Length - 1; i++)
                {
                    sb.Append(extraData[i]);
                    sb.Append(',');
                }
                sb.Append(extraData[extraData.Length - 1]);
            }
            sb.Append("&lang=");
            if (languages != null)
            {
                for (int i = 0; i < languages.Length - 1; i++)
                {
                    sb.Append(languages[i]);
                    sb.Append(',');
                }
                sb.Append(languages[languages.Length - 1]);
            }
            else
                sb.Append("all");
            limit = Clamp(limit, 1, 50);
            sb.Append("&limit=");
            sb.Append(limit);
            if (maxUpcomingHours.HasValue)
            {
                sb.Append("&max_upcoming_hours=");
                sb.Append(maxUpcomingHours);
            }

            if (mentionedChannelId != null)
            {
                sb.Append("&mentioned_channel_id=");
                sb.Append(mentionedChannelId);
            }

            sb.Append("&offset=");
            sb.Append(offset);

            sb.Append("&order=");
            sb.Append(order == SortOrder.Descending ? "desc" : "asc");

            if (organization != null)
            {
                sb.Append("&org=");
                sb.Append(organization);
            }
            sb.Append("&sort=");
            sb.Append(sortColumn.ToLower());

            if (status != null)
            {
                sb.Append("&status=");
                sb.Append(status);
            }

            if (topic != null)
            {
                sb.Append("&topic=");
                sb.Append(topic);
            }

            sb.Append("&type=");
            if (type == VideoType.Stream)
                sb.Append("stream");
            else if (type == VideoType.Clip)
                sb.Append("clip");

            JArray result = await RequestArray(sb.ToString());

            return result.ToObject<List<Video>>();
        }

        // Use seperate request functions, since some endpoints return arrays and some return objects

        private async Task<JArray> RequestArray(string endpoint)
        {
            string response;
            try
            {
                HttpResponseMessage httpResponse = await httpClient.GetAsync(url + endpoint);
                response = await httpResponse.Content.ReadAsStringAsync();
            }
            catch (HttpRequestException e)
            {
                throw new HttpRequestException(e.Message);
            }

            JArray result;
            // since errors are returned as objects, we have to handle if the json fails to deserialize to a JArray
            try
            {
                result = JArray.Parse(response);
            }
            catch (JsonException e)
            {
                /* Handle invalid endpoint parameters
                *  API returns a single JSON object named 'message'
                *  Example: {"message":"Channel Id not found"}
                */

                // parse the error into an object
                JObject error = JObject.Parse(response);
                if (error["message"] != null)
                    throw new ArgumentException(error["message"].ToString());
                // special case for when we fail to parse the error message
                else
                    throw new Exception("Unhandled error when parsing Json Array: " + e.Message);
            }

            return result;
        }

        private async Task<JObject> RequestObject(string endpoint)
        {
            string response;
            try
            {
                HttpResponseMessage httpResponse = await httpClient.GetAsync(url + endpoint);
                response = await httpResponse.Content.ReadAsStringAsync();
            }
            catch (HttpRequestException e)
            {
                throw new HttpRequestException(e.Message);
            }

           
            JObject result = JObject.Parse(response);

            /* Handle invalid endpoint parameters
            *  API returns a single JSON object named 'message'
            *  Example: {"message":"Channel Id not found"}
            */

            if (result["message"] != null)
                throw new ArgumentException(result["message"].ToString());

            return result;
        }

        // Since .NET Standard 2.0 does not support Math.Clamp, write our own
        private int Clamp(int input, int min, int max)
        {
            if (input < min)
                return min;
            if (input > max)
                return max;
            return input;
        }
        
    }
}
