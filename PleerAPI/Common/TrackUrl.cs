using System;
using Newtonsoft.Json;

namespace PleerAPI.Common
{
	public class TrackUrl : AbstractResponse
	{
		[JsonProperty("url")]
		public String Url { get; set; }
	}
}