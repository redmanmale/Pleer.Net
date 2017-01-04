﻿using Newtonsoft.Json;

namespace PleerNet.Model
{
    public class TrackInfo : BaseTrack
    {
        [JsonProperty("track_id")]
        public string TrackId { get; set; }

        /// <summary>
        /// Track size in Mb
        /// </summary>
        [JsonProperty("size")]
        public int Size { get; set; }
    }
}
