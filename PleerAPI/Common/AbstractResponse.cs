using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace PleerAPI.Common
{
	public abstract partial class AbstractResponse
	{
		[JsonProperty("success")]
		public Boolean Success { get; set; }

		[JsonProperty("message")]
		public String Message { get; set; }

		[JsonProperty("error")]
		public String Error { get; set; }

		[JsonProperty("error_description")]
		public String ErrorDescription { get; set; }
	}
}
