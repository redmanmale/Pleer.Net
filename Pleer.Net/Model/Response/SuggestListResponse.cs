using System.Collections.Generic;
using Newtonsoft.Json;

namespace Pleer.Net.Model.Response
{
    public class SuggestListResponse : BaseResponse
    {
        [JsonProperty("suggest")]
        public List<string> Suggest { get; set; }
    }
}
