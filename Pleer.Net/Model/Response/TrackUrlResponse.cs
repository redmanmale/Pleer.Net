using Newtonsoft.Json;

namespace Pleer.Net.Model.Response
{
    public class TrackUrlResponse : BaseResponse
    {
        [JsonProperty("url")]
        public string Url { get; set; }
    }
}
