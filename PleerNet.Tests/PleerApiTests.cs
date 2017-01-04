using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;
using PleerNet.Model;

namespace PleerNet.Tests
{
    [TestFixture]
    public class PleerApiTests
    {
        private const string ClientId = "777594";
        private const string ClientPassword = "TNxrjZYDAut9KSiwCJpa";

        private const string TrackId = "893280MipJ";
        private const string ArtistName = "Rick Astley";
        private const string SearchQuery = ArtistName + " - Never Gonna Give You Up";

        private readonly PleerApi _pleer = new PleerApi();

        [Test]
        public async Task AuthorizeTest()
        {
            await AuthorizeAsync();

            Assert.IsFalse(string.IsNullOrWhiteSpace(_pleer.TokenInfo?.AccessToken));
        }

        [Test]
        public async Task ClearAuthTest()
        {
            await AuthorizeAsync();
            _pleer.ClearAuth();

            Assert.IsNull(_pleer.TokenInfo);
        }

        [Test]
        public async Task AuthorizeWithTokenTest()
        {
            await AuthorizeAsync();

            var accessToken = _pleer.TokenInfo.AccessToken;
            var expireToken = _pleer.TokenInfo.ExpiresIn;

            _pleer.ClearAuth();
            _pleer.Authorize(accessToken, expireToken);

            Assert.AreEqual(accessToken, _pleer.TokenInfo?.AccessToken);
        }

        [Test]
        public async Task TokenExpireTest()
        {
            await AuthorizeAsync();

            const int expireToken = 1;
            var accessToken = _pleer.TokenInfo.AccessToken;

            _pleer.ClearAuth();

            var eventRaised = false;
            _pleer.OnTokenExpire += (sender, args) => eventRaised = true;
            _pleer.Authorize(accessToken, expireToken);

            // Wait for token to expire
            Thread.Sleep((expireToken + 2) * 1000);

            Assert.IsTrue(eventRaised);
        }

        [Test]
        public async Task SearchTest()
        {
            await AuthorizeAsync();
            var response = await _pleer.SearchAsync(SearchQuery, 1, 5);

            Assert.IsTrue(response.Success);
            Assert.AreEqual(5, response.Tracks.Count);
        }

        [Test]
        public async Task GetTrackInfoTest()
        {
            await AuthorizeAsync();
            var response = await _pleer.GetTrackInfoAsync(TrackId);

            Assert.IsTrue(response.Success);
            Assert.AreEqual(TrackId, response.Track.TrackId);
        }

        [Test]
        public async Task GetTrackLyricsTest()
        {
            await AuthorizeAsync();
            var response = await _pleer.GetTrackLyricsAsync(TrackId);

            Assert.IsTrue(response.Success);
            Assert.IsFalse(string.IsNullOrWhiteSpace(response.Text));
        }

        [Test]
        public async Task GetTrackDownloadLinkTest()
        {
            await AuthorizeAsync();
            var response = await _pleer.GetTrackDownloadLinkAsync(TrackId, ReasonEnum.Listen);

            Assert.IsTrue(response.Success);
            Assert.IsFalse(string.IsNullOrWhiteSpace(response.Url));
        }

        [Test]
        public async Task GetTopListTest()
        {
            await AuthorizeAsync();
            var response = await _pleer.GetTopListAsync(IntervalEnum.Week, 2, LanguageEnum.En);

            Assert.IsTrue(response.Success);
            Assert.IsTrue(response.TopTracksResponse.Tracks.Any());
        }

        [Test]
        public async Task GetSuggestTest()
        {
            await AuthorizeAsync();
            var response = await _pleer.GetSuggestAsync(ArtistName);

            Assert.IsTrue(response.Success);
            Assert.IsTrue(response.Suggest.Any());
        }

        private async Task AuthorizeAsync()
        {
            await _pleer.AuthorizeAsync(ClientId, ClientPassword);
        }
    }
}
