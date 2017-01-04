using Newtonsoft.Json;

namespace PleerNet.Model
{
    public class Track : BaseTrack
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("text_id")]
        public string TextId { get; set; }

        [JsonProperty("server_id4")]
        public string ServerId4 { get; set; }

        [JsonProperty("count_listen")]
        public int CountListen { get; set; }

        [JsonProperty("position")]
        public int Position { get; set; }
    }
}
