using Newtonsoft.Json;

namespace Pleer.Net.Model.Response
{
    public class TrackLyricsResponse : BaseResponse
    {
        [JsonProperty("text")]
        public string Text { get; set; }
    }
}
