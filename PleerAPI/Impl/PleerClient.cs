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

		private const String GrantType = "client_credentials";
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
			var client = new RestClient(TokenUrl);

			var request = new RestRequest { Method = Method.POST };

			request.AddParameter("grant_type", GrantType);
			request.AddParameter("client_id", _clientId);
			request.AddParameter("client_secret", _clientPassword);

			request.AddParameter("application/x-www-form-urlencoded", new JObject(), ParameterType.RequestBody);

			var response = client.Execute(request);
			var content = response.Content;

			return JsonConvert.DeserializeObject<TokenInfo>(content);
		}

		public TrackList TracksSearch(string query, int page = 1, int resultOnPage = 10, string quality = Quality.All)
		{
			var client = new RestClient(ResourceUrl);

			var request = new RestRequest { Method = Method.POST };
			
			request.AddParameter("access_token", Token.AccessToken);
			request.AddParameter("method", "tracks_search");
			request.AddParameter("query", query);
			request.AddParameter("page", page);
			request.AddParameter("resultOnPage", resultOnPage);
			request.AddParameter("quality", quality);

			request.AddParameter("application/x-www-form-urlencoded", new JObject(), ParameterType.RequestBody);

			var response = client.Execute(request);
			var content = response.Content;

			var trackList = JsonConvert.DeserializeObject<TrackList>(content);

			return trackList;
		}

		public TrackInfo GetTrackInfo(string trackId)
		{
			var client = new RestClient(ResourceUrl);

			var request = new RestRequest { Method = Method.POST };

			request.AddParameter("access_token", Token.AccessToken);
			request.AddParameter("method", "tracks_get_info");
			request.AddParameter("track_id", trackId);

			request.AddParameter("application/x-www-form-urlencoded", new JObject(), ParameterType.RequestBody);

			var response = client.Execute(request);
			var content = response.Content;
			
			var trackInfo = JsonConvert.DeserializeObject<TrackInfo>(content);

			return trackInfo;
		}

		public TrackLyrics GetTrackLyrics(string trackId)
		{
			var client = new RestClient(ResourceUrl);

			var request = new RestRequest { Method = Method.POST };

			request.AddParameter("access_token", Token.AccessToken);
			request.AddParameter("method", "tracks_get_lyrics");
			request.AddParameter("track_id", trackId);

			request.AddParameter("application/x-www-form-urlencoded", new JObject(), ParameterType.RequestBody);

			var response = client.Execute(request);
			var content = response.Content;

			var trackLyrics = JsonConvert.DeserializeObject<TrackLyrics>(content);

			return trackLyrics;
		}

		public TrackUrl GetTrackDownloadLink(string trackId, string reason = Reason.Listen)
		{
			var client = new RestClient(ResourceUrl);

			var request = new RestRequest { Method = Method.POST };

			request.AddParameter("access_token", Token.AccessToken);
			request.AddParameter("method", "tracks_get_download_link");
			request.AddParameter("track_id", trackId);
			request.AddParameter("reason", reason);

			request.AddParameter("application/x-www-form-urlencoded", new JObject(), ParameterType.RequestBody);

			var response = client.Execute(request);
			var content = response.Content;

			var trackUrl = JsonConvert.DeserializeObject<TrackUrl>(content);

			return trackUrl;
		}

		public TopTrackList GetTopList(int listType, int page, String language)
		{
			var client = new RestClient(ResourceUrl);

			var request = new RestRequest { Method = Method.POST };

			request.AddParameter("access_token", Token.AccessToken);
			request.AddParameter("method", "get_top_list");
			request.AddParameter("list_type", listType);
			request.AddParameter("page", page);
			request.AddParameter("language", language);

			request.AddParameter("application/x-www-form-urlencoded", new JObject(), ParameterType.RequestBody);

			var response = client.Execute(request);
			var content = response.Content;

			var trackList = JsonConvert.DeserializeObject<TopTrackList>(content);

			return trackList;
		}

		public SuggestList GetSuggest(string part)
		{
			var client = new RestClient(ResourceUrl);

			var request = new RestRequest { Method = Method.POST };

			request.AddParameter("access_token", Token.AccessToken);
			request.AddParameter("method", "get_suggest");
			request.AddParameter("part", part);

			request.AddParameter("application/x-www-form-urlencoded", new JObject(), ParameterType.RequestBody);

			var response = client.Execute(request);
			var content = response.Content;

			var suggest = JsonConvert.DeserializeObject<SuggestList>(content);

			return suggest;
		}
	}
}
