using Newtonsoft.Json;

namespace Pleer.Net.Model.Response
{
    public class TrackInfoResponse : BaseResponse
    {
        [JsonProperty("data")]
        public TrackInfo Track { get; set; }
    }
}
