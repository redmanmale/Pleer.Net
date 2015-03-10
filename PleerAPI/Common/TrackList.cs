using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace PleerAPI.Common
{
	public class TrackList : AbstractResponse
	{
		[JsonProperty("count")]
		[Description("Количество найденных треков")]
		public int Count { get; set; }

		[JsonProperty("tracks")]
		public Dictionary<String, Track> Tracks { get; set; }
	}

	public class TopTrackList : AbstractResponse
	{
		[JsonProperty("tracks")]
		public TopTracks TopTracks { get; set; }
	}

	public class TopTracks
	{
		[JsonProperty("count")]
		[Description("Количество найденных треков")]
		public int Count { get; set; }

		[JsonProperty("data")]
		public Dictionary<String, Track> Tracks { get; set; }
	}

	public class Track
	{
		[JsonProperty("id")]
		[Description("Идентификатор трека")]
		public String Id { get; set; }

		[JsonProperty("artist")]
		[Description("Исполнитель")]
		public String Artist { get; set; }

		[JsonProperty("track")]
		[Description("Название")]
		public String TrackName { get; set; }

		[JsonProperty("length")]
		[Description("Продолжительность воспроизведения в секундах")]
		public Int32 Length { get; set; }

		[JsonProperty("bitrate")]
		[Description("Толщина потока в человекочитаемом формате")]
		public String Bitrate { get; set; }

		[JsonProperty("text_id")]
		public String TextId { get; set; }

		[JsonProperty("server_id4")]
		public String ServerId4 { get; set; }

		[JsonProperty("count_listen")]
		public Int32 CountListen { get; set; }

		[JsonProperty("position")]
		public Int32 Position { get; set; }
	}
}
