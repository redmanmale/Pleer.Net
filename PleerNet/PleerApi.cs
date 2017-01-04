using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Timers;
using PleerNet.Model;
using PleerNet.Model.Response;

namespace PleerNet
{
    /// <summary>
    /// Pleer.net (Prostopleer) API
    /// </summary>
    public class PleerApi : IDisposable
    {
        private static readonly Uri ResourceUrl = new Uri("http://api.pleer.com/index.php");
        private static readonly Uri TokenUrl = new Uri("http://api.pleer.com/token.php");

        private readonly HttpClient _client;
        private readonly Timer _expireTokenTimer;

        /// <summary>
        /// Auth token info
        /// </summary>
        public TokenInfoResponse TokenInfo { get; private set; }

        /// <summary>
        /// Raises when access token expire
        /// </summary>
        public event EventHandler OnTokenExpire;

        /// <summary>
        /// Pleer.net (Prostopleer) API
        /// </summary>
        public PleerApi()
        {
            _client = new HttpClient();
            _expireTokenTimer = new Timer();
            _expireTokenTimer.Elapsed += (sender, e) => { OnTokenExpire?.Invoke(this, e); };
        }

        /// <summary>
        /// Authorize with existing access token
        /// </summary>
        /// <param name="token">access token</param>
        /// <param name="expireTime">expire time in seconds</param>
        public void Authorize(string token, int expireTime)
        {
            TokenInfo = new TokenInfoResponse
            {
                AccessToken = token,
                ExpiresIn = expireTime
            };

            SetUpExpireTokenTimer();
        }

        /// <summary>
        /// Authorize by getting access token
        /// </summary>
        public async Task AuthorizeAsync(string clientId, string clientPassword)
        {
            TokenInfo = await GetTokenInfoAsync(clientId, clientPassword).ConfigureAwait(false);
            SetUpExpireTokenTimer();
        }

        /// <summary>
        /// Remove access token
        /// </summary>
        public void ClearAuth()
        {
            TokenInfo = null;
            _expireTokenTimer.Stop();
        }

        /// <summary>
        /// Search for track
        /// </summary>
        /// <param name="query">name of track or artist</param>
        /// <param name="page">page of results</param>
        /// <param name="resultsOnPage">results per page</param>
        /// <param name="quality">track quality (bitrate)</param>
        /// <returns>track list</returns>
        public Task<TrackListResponse> SearchAsync(string query, int page = 1, int resultsOnPage = 10,
            QualityEnum quality = QualityEnum.All)
        {
            var requestParameters = new RequestParameters
            {
                {"method", "tracks_search"},
                {"query", query},
                {"page", page},
                {"result_on_page", resultsOnPage},
                {"quality", quality}
            };

            AddAuth(requestParameters);

            return _client.RequestAsync<TrackListResponse>(requestParameters, ResourceUrl);
        }

        /// <summary>
        /// Get track info
        /// </summary>
        /// <param name="trackId">track id</param>
        public Task<TrackInfoResponse> GetTrackInfoAsync(string trackId)
        {
            var requestParameters = new RequestParameters
            {
                {"method", "tracks_get_info"},
                {"track_id", trackId}
            };

            AddAuth(requestParameters);

            return _client.RequestAsync<TrackInfoResponse>(requestParameters, ResourceUrl);
        }

        /// <summary>
        /// Get track lyrics
        /// </summary>
        /// <param name="trackId">track id</param>
        /// <returns>Track lyrics in plain text</returns>
        public Task<TrackLyricsResponse> GetTrackLyricsAsync(string trackId)
        {
            var requestParameters = new RequestParameters
            {
                {"method", "tracks_get_lyrics"},
                {"track_id", trackId}
            };

            AddAuth(requestParameters);

            return _client.RequestAsync<TrackLyricsResponse>(requestParameters, ResourceUrl);
        }

        /// <summary>
        /// Get link to track for
        /// </summary>
        /// <param name="trackId">track id</param>
        /// <param name="reason">listen or download</param>
        public Task<TrackUrlResponse> GetTrackDownloadLinkAsync(string trackId, ReasonEnum reason = ReasonEnum.Save)
        {
            var requestParameters = new RequestParameters
            {
                {"method", "tracks_get_download_link"},
                {"track_id", trackId},
                {"reason", reason}
            };

            AddAuth(requestParameters);

            return _client.RequestAsync<TrackUrlResponse>(requestParameters, ResourceUrl);
        }

        /// <summary>
        /// Get top tracks for interval and language
        /// </summary>
        /// <param name="interval">interval type</param>
        /// <param name="page">page of results</param>
        /// <param name="language">russian top or foreign top</param>
        /// <returns>track list</returns>
        public Task<TopTrackListResponse> GetTopListAsync(IntervalEnum interval, int page = 1, LanguageEnum language = LanguageEnum.Ru)
        {
            var requestParameters = new RequestParameters
            {
                {"method", "get_top_list"},
                {"list_type", interval.ToString("D")},
                {"page", page.ToString()},
                {"language", language}
            };

            AddAuth(requestParameters);

            return _client.RequestAsync<TopTrackListResponse>(requestParameters, ResourceUrl);
        }

        /// <summary>
        /// Get suggestions by partial name of track or artrist
        /// </summary>
        /// <returns>suggestion list</returns>
        public Task<SuggestListResponse> GetSuggestAsync(string part)
        {
            var requestParameters = new RequestParameters
            {
                {"method", "get_suggest"},
                {"part", part}
            };

            AddAuth(requestParameters);

            return _client.RequestAsync<SuggestListResponse>(requestParameters, ResourceUrl);
        }

        private void SetUpExpireTokenTimer()
        {
            _expireTokenTimer.Stop();
            _expireTokenTimer.Interval = TokenInfo.ExpiresIn * 1000;
            _expireTokenTimer.Start();
        }

        private Task<TokenInfoResponse> GetTokenInfoAsync(string clientId, string clientPassword)
        {
            var requestParameters = new RequestParameters
            {
                {"grant_type", "client_credentials"},
                {"client_id", clientId},
                {"client_secret", clientPassword}
            };

            return _client.RequestAsync<TokenInfoResponse>(requestParameters, TokenUrl);
        }

        private void AddAuth(RequestParameters requestParameters)
        {
            if (string.IsNullOrWhiteSpace(TokenInfo?.AccessToken))
            {
                throw new InvalidOperationException("Unauthorized. Call AuthorizeAsync method first.");
            }

            requestParameters.Add("access_token", TokenInfo.AccessToken);
        }

        /// <summary>
        /// Clear all resources
        /// </summary>
        public void Dispose()
        {
            _client?.Dispose();

            if (_expireTokenTimer != null)
            {
                _expireTokenTimer.Stop();
                _expireTokenTimer.Dispose();
            }
        }
    }
}
