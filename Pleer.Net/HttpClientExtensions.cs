using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Pleer.Net.Model.Response;

namespace Pleer.Net
{
    internal static class HttpClientExtensions
    {
        public static async Task<T> RequestAsync<T>(this HttpClient client, RequestParameters parameters, Uri resourceUri)
            where T : BaseResponse
        {
            var request = new HttpRequestMessage(HttpMethod.Post, resourceUri)
            {
                Content = new FormUrlEncodedContent(parameters)
            };

            var response = await client.SendAsync(request).ConfigureAwait(false);
            var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            response.EnsureSuccessStatusCode();

            var result = JsonConvert.DeserializeObject<T>(content);
            return result;
        }
    }
}
