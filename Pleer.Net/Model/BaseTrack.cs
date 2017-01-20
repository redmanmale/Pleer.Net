using Newtonsoft.Json;

namespace Pleer.Net.Model
{
    public abstract class BaseTrack
    {
        [JsonProperty("artist")]
        public string Artist { get; set; }

        [JsonProperty("track")]
        public string TrackName { get; set; }

        // Yes, 'lenght'
        [JsonProperty("lenght")]
        public int Length { get; set; }

        [JsonProperty("bitrate")]
        public string Bitrate { get; set; }
    }
}