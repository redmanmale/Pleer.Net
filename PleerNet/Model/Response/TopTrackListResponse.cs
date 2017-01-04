using Newtonsoft.Json;

namespace PleerNet.Model.Response
{
    public class TopTrackListResponse : BaseResponse
    {
        [JsonProperty("tracks")]
        public TopTracksResponse TopTracksResponse { get; set; }
    }
}
