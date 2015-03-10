using System;
using Newtonsoft.Json;

namespace PleerAPI.Common
{
	public class TokenInfo : AbstractResponse
		{
		[JsonProperty("access_token")]
		public String AccessToken { get; set; }

		[JsonProperty("expires_in")]
		public Int32 ExpiresIn { get; set; }

		[JsonProperty("token_type")]
		public String TokenType { get; set; }

		[JsonProperty("scope")]
		public String Scope { get; set; }

	}
}
