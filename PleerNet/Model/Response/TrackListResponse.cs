using System.Collections.Generic;
using Newtonsoft.Json;

namespace PleerNet.Model.Response
{
    public class TrackListResponse : BaseResponse
    {
        [JsonProperty("count")]
        public int Count { get; set; }

        [JsonProperty("tracks")]
        public Dictionary<string, Track> Tracks { get; set; }
    }
}
