using Newtonsoft.Json;

namespace PleerNet.Model.Response
{
    public class TrackInfoResponse : BaseResponse
    {
        [JsonProperty("data")]
        public TrackInfo Track { get; set; }
    }
}
