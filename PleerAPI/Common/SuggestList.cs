using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace PleerAPI.Common
{
	public class SuggestList : AbstractResponse
	{
		[JsonProperty("suggest")]
		public List<String> Suggest { get; set; }
	}
}
