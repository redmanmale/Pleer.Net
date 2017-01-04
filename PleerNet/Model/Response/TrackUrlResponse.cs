using Newtonsoft.Json;

namespace PleerNet.Model.Response
{
    public class TrackUrlResponse : BaseResponse
    {
        [JsonProperty("url")]
        public string Url { get; set; }
    }
}
