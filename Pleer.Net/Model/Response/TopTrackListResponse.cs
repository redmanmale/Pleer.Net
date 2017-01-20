using Newtonsoft.Json;

namespace Pleer.Net.Model.Response
{
    public class TopTrackListResponse : BaseResponse
    {
        [JsonProperty("tracks")]
        public TopTracksResponse TopTracksResponse { get; set; }
    }
}
