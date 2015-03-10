using System;
using System.ComponentModel;
using Newtonsoft.Json;

namespace PleerAPI.Common
{
	public class TrackInfoRs : AbstractResponse
	{
		[JsonProperty("data")]
		public TrackInfo Track { get; set; }
	}

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

//
//{
//	"success":true,
//	"data":
//	{
//		"track_id":"11426AUEz",
//		"artist":"\u0410\u0440\u0438\u044f",
//		"track":"\u0421\u0432\u043e\u0431\u043e\u0434\u0430",
//		"lenght":"313",
//		"bitrate":"VBR",
//		"size":"5275648"
//	}
//}