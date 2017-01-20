using System.Collections.Generic;
using Newtonsoft.Json;

namespace Pleer.Net.Model.Response
{
    public class TopTracksResponse : BaseResponse
    {
        [JsonProperty("count")]
        public int Count { get; set; }

        [JsonProperty("data")]
        public Dictionary<string, Track> Tracks { get; set; }
    }
}
