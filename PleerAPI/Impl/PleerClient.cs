using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Net;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PleerAPI.Common;
using RestSharp;

namespace PleerAPI.Impl
{
	public class PleerClient : IClient
	{
		private readonly String _clientId;
		private readonly String _clientPassword;

		public TokenInfo Token { get; private set; }

		private const String TokenUrl = "http://api.pleer.com/token.php";
		//		private const String ResourceUrl = "http://api.pleer.com/resource.php";
		private const String ResourceUrl = "http://api.pleer.com/index.php";

		public PleerClient(string clientId, string clientPassword)
		{
			_clientId = clientId;
			_clientPassword = clientPassword;

			Token = new TokenInfo();
		}

		public void Init()
		{
			Token = GetTokenInfo();
		}

		public TokenInfo GetTokenInfo()
		{
			return
				RestRequest<TokenInfo>(new Dictionary<string, object>
				{
					{"grant_type", "client_credentials"},
					{"client_id", _clientId},
					{"client_secret", _clientPassword}
				}, TokenUrl);
		}

		public TrackList TracksSearch(string query, int page = 1, int resultOnPage = 10, string quality = Quality.All)
		{
			return
				RestRequest<TrackList>(new Dictionary<string, object>
				{
					{"access_token", Token.AccessToken}, 
					{"method", "tracks_search"},
					{"query", query},
					{"page", page},
					{"resultOnPage", resultOnPage},
					{"quality", quality}
				});
		}

		public TrackInfoRs GetTrackInfo(string trackId)
		{
			return
				RestRequest<TrackInfoRs>(new Dictionary<string, object>
				{
					{"access_token", Token.AccessToken}, 
					{"method", "tracks_get_info"},
					{"track_id", trackId}
				});
		}

		public TrackLyrics GetTrackLyrics(string trackId)
		{
			return
				RestRequest<TrackLyrics>(new Dictionary<string, object>
				{
					{"access_token", Token.AccessToken}, 
					{"method", "tracks_get_lyrics"},
					{"track_id", trackId}
				});
		}

		public TrackUrl GetTrackDownloadLink(string trackId, string reason = Reason.Listen)
		{
			return
				RestRequest<TrackUrl>(new Dictionary<string, object>
				{
					{"access_token", Token.AccessToken}, 
					{"method", "tracks_get_download_link"},
					{"track_id", trackId},
					{"reason", reason}
				});
		}

		public TopTrackList GetTopList(int listType, int page, String language)
		{
			return
				RestRequest<TopTrackList>(new Dictionary<string, object>
				{
					{"access_token", Token.AccessToken}, 
					{"method", "get_top_list"},
					{"list_type", listType},
					{"page", page},
					{"language", language}
				});
		}

		public SuggestList GetSuggest(string part)
		{
			return RestRequest<SuggestList>(new Dictionary<string, object>
			{
				{"access_token", Token.AccessToken}, 
				{"method", "get_suggest"}, 
				{"part", part}
			});
		}

		private static T RestRequest<T>(Dictionary<String, Object> parameters, String url = ResourceUrl)
		{
			var client = new RestClient(url);

			var request = new RestRequest { Method = Method.POST };

			foreach (var parameter in parameters)
			{
				request.AddParameter(parameter.Key, parameter.Value);
			}

			request.AddParameter("application/x-www-form-urlencoded", new JObject(), ParameterType.RequestBody);

			var response = client.Execute(request);
			var content = response.Content;

			var result = JsonConvert.DeserializeObject<T>(content);

			return result;
		}
	}
}
