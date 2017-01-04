using Newtonsoft.Json;

namespace PleerNet.Model.Response
{
    public class TrackLyricsResponse : BaseResponse
    {
        [JsonProperty("text")]
        public string Text { get; set; }
    }
}
