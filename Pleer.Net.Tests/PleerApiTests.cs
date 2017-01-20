using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;
using Pleer.Net.Model;

namespace Pleer.Net.Tests
{
    [TestFixture]
    public class PleerApiTests
    {
        private const string AppId = "777594";
        private const string AppKey = "TNxrjZYDAut9KSiwCJpa";

        private const string TrackId = "893280MipJ";
        private const string ArtistName = "Rick Astley";

        private readonly PleerApi _pleer = new PleerApi();

        [Test]
        public async Task AuthorizeTest()
        {
            await AuthorizeAsync().ConfigureAwait(false);

            Assert.IsFalse(string.IsNullOrWhiteSpace(_pleer.TokenInfo?.AccessToken));
        }

        [Test]
        public void MethodWithoutAuthTest()
        {
            Assert.ThrowsAsync<InvalidOperationException>(() =>
            {
                _pleer.ClearAuth();
                return _pleer.GetTrackInfoAsync(TrackId);
            });
        }

        [Test]
        public async Task ClearAuthTest()
        {
            await AuthorizeAsync().ConfigureAwait(false);
            _pleer.ClearAuth();

            Assert.IsNull(_pleer.TokenInfo);
        }

        [Test]
        public async Task AuthorizeWithTokenTest()
        {
            await AuthorizeAsync().ConfigureAwait(false);

            var accessToken = _pleer.TokenInfo.AccessToken;
            var expireToken = _pleer.TokenInfo.ExpiresIn;

            _pleer.ClearAuth();
            _pleer.Authorize(accessToken, expireToken);

            Assert.AreEqual(accessToken, _pleer.TokenInfo?.AccessToken);
        }

        [Test]
        public async Task TokenExpireTest()
        {
            await AuthorizeAsync().ConfigureAwait(false);

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
            await AuthorizeAsync().ConfigureAwait(false);
            var response = await _pleer.SearchAsync($"{ArtistName} Never Gonna Give You Up", 1, 5).ConfigureAwait(false);

            Assert.IsTrue(response.Success);
            Assert.AreEqual(5, response.Tracks.Count);
        }

        [Test]
        public async Task GetTrackInfoTest()
        {
            await AuthorizeAsync().ConfigureAwait(false);
            var response = await _pleer.GetTrackInfoAsync(TrackId).ConfigureAwait(false);

            Assert.IsTrue(response.Success);
            Assert.AreEqual(TrackId, response.Track.TrackId);
        }

        [Test]
        public async Task GetTrackLyricsTest()
        {
            await AuthorizeAsync().ConfigureAwait(false);
            var response = await _pleer.GetTrackLyricsAsync(TrackId).ConfigureAwait(false);

            Assert.IsTrue(response.Success);
            Assert.IsFalse(string.IsNullOrWhiteSpace(response.Text));
        }

        [Test]
        public async Task GetTrackDownloadLinkTest()
        {
            await AuthorizeAsync().ConfigureAwait(false);
            var response = await _pleer.GetTrackDownloadLinkAsync(TrackId, ReasonEnum.Listen).ConfigureAwait(false);

            Assert.IsTrue(response.Success);
            Assert.IsFalse(string.IsNullOrWhiteSpace(response.Url));
        }

        [Test]
        public async Task GetTopListTest()
        {
            await AuthorizeAsync().ConfigureAwait(false);
            var response = await _pleer.GetTopListAsync(IntervalEnum.Week, 2, LanguageEnum.En).ConfigureAwait(false);

            Assert.IsTrue(response.Success);
            Assert.IsTrue(response.TopTracksResponse.Tracks.Any());
        }

        [Test]
        public async Task GetSuggestTest()
        {
            await AuthorizeAsync().ConfigureAwait(false);
            var response = await _pleer.GetSuggestAsync(ArtistName).ConfigureAwait(false);

            Assert.IsTrue(response.Success);
            Assert.IsTrue(response.Suggest.Any());
        }

        private Task AuthorizeAsync() => _pleer.AuthorizeAsync(AppId, AppKey);
    }
}
