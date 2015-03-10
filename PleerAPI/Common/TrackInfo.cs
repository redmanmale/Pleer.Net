using System;
using System.ComponentModel;
using Newtonsoft.Json;

namespace PleerAPI.Common
{
	public class TrackInfo
	{
		[Description("Идентификатор трека")]
		[JsonProperty("track_id")]
		public String TrackId { get; set; }

		[Description("Исполнитель")]
		[JsonProperty("artist")]
		public String Artist { get; set; }

		[Description("Название")]
		[JsonProperty("track")]
		public String TrackName { get; set; }

		[Description("Продолжительность воспроизведения в секундах")]
		[JsonProperty("length")]
		public Int32 Length { get; set; }

		[Description("Толщина потока в человекочитаемом формате")]
		[JsonProperty("bitrate")]
		public String Bitrate { get; set; }

		[Description("Размер файла в байтах")]
		[JsonProperty("size")]
		public Int32 Size { get; set; }
	}
}