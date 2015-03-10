using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace PleerAPI.Common
{
	public class TrackLyrics : AbstractResponse
	{
		[JsonProperty("text")]
		public String Text { get; set; }
	}
}
