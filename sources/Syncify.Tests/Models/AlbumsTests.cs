using System;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Syncify.Models;

namespace Syncify.Tests.Models
{
    [TestClass]
    public class AlbumsTests
    {
        private static readonly Assembly assembly = Assembly.GetExecutingAssembly();

        [TestMethod]
        public async Task WhenParsingJsonReturnsAlbums()
        {
            using var stream = assembly.GetManifestResourceStream($"{assembly.GetName().Name}.Resources.Albums.json");

            var page = await JsonSerializer.DeserializeAsync<Page<SavedAlbum>>(stream);

            Assert.AreEqual(0, Uri.Compare(new Uri("https://api.spotify.com/v1/me/albums?offset=0&limit=1"), page?.Uri, UriComponents.AbsoluteUri, UriFormat.Unescaped, StringComparison.Ordinal));
            Assert.AreEqual(1, page?.Limit);
            Assert.AreEqual(0, Uri.Compare(new Uri("https://api.spotify.com/v1/me/albums?offset=1&limit=1"), page?.Next, UriComponents.AbsoluteUri, UriFormat.Unescaped, StringComparison.Ordinal));
            Assert.AreEqual(0, page?.Offset);
            Assert.IsNull(page?.Previous);
            Assert.AreEqual(19, page?.Total);
            var savedAlbum = page.Items.SingleOrDefault();
            Assert.AreEqual(new DateTime(2015, 11, 26, 19, 13, 31), savedAlbum?.AddedAt);
            var album = savedAlbum?.Album;
            Assert.AreEqual("album", album?.AlbumType);
            Assert.AreEqual("spotify", album?.Artists?.SingleOrDefault()?.ExternalUrls?.SingleOrDefault().Key);
            Assert.AreEqual(0, Uri.Compare(new Uri("https://open.spotify.com/artist/58RMTlPJKbmpmVk1AmRK3h"), album?.Artists?.SingleOrDefault()?.ExternalUrls?.SingleOrDefault().Value, UriComponents.AbsoluteUri, UriFormat.Unescaped, StringComparison.Ordinal));
            Assert.AreEqual(0, Uri.Compare(new Uri("https://api.spotify.com/v1/artists/58RMTlPJKbmpmVk1AmRK3h"), album?.Artists?.SingleOrDefault()?.Url, UriComponents.AbsoluteUri, UriFormat.Unescaped, StringComparison.Ordinal));
            Assert.AreEqual("58RMTlPJKbmpmVk1AmRK3h", album?.Artists?.SingleOrDefault()?.Id);
            Assert.AreEqual("Abidaz", album?.Artists?.SingleOrDefault()?.Name);
            Assert.AreEqual("artist", album?.Artists?.SingleOrDefault()?.Type);
            Assert.AreEqual("spotify:artist:58RMTlPJKbmpmVk1AmRK3h", album?.Artists?.SingleOrDefault()?.Uri);
            CollectionAssert.AreEqual(new string[] { "AR", "AT", "AU", "BE", "BR", "CL", "CO", "CY", "CZ", "DE" }, album?.AvailableMarkets?.ToArray());
            Assert.AreEqual(2, album?.Copyrights?.Count());
            Assert.AreEqual("(C) 2013 Soblue Music Group AB, Under exclusive license to Universal Music AB", album?.Copyrights?.ElementAt(0).Text);
            Assert.AreEqual("C", album?.Copyrights?.ElementAt(0).Type);
            Assert.AreEqual("(P) 2013 Soblue Music Group AB, Under exclusive license to Universal Music AB", album?.Copyrights?.ElementAt(1).Text);
            Assert.AreEqual("P", album?.Copyrights?.ElementAt(1).Type);
            Assert.AreEqual(1, album?.ExternalIds?.Count);
            Assert.AreEqual("upc", album?.ExternalIds?.Single().Key);
            Assert.AreEqual("00602537623730", album?.ExternalIds?.Single().Value);
            Assert.AreEqual("spotify", album?.ExternalUrls?.SingleOrDefault().Key);
            Assert.AreEqual(0, Uri.Compare(new Uri("https://open.spotify.com/album/5m4VYOPoIpkV0XgOiRKkWC"), album?.ExternalUrls?.SingleOrDefault().Value, UriComponents.AbsoluteUri, UriFormat.Unescaped, StringComparison.Ordinal));
            Assert.AreEqual(0, album?.Genres?.Count());
            Assert.AreEqual(0, Uri.Compare(new Uri("https://api.spotify.com/v1/albums/5m4VYOPoIpkV0XgOiRKkWC"), album?.Url, UriComponents.AbsoluteUri, UriFormat.Unescaped, StringComparison.Ordinal));
            Assert.AreEqual("5m4VYOPoIpkV0XgOiRKkWC", album?.Id);
            Assert.AreEqual(3, album?.Images?.Count());
            Assert.AreEqual(640, album?.Images?.ElementAt(0)?.Height);
            Assert.AreEqual(0, Uri.Compare(new Uri("https://i.scdn.co/image/ccbb1e3bea2461e69783895e880965b171e29f4c"), album?.Images?.ElementAt(0)?.Url, UriComponents.AbsoluteUri, UriFormat.Unescaped, StringComparison.Ordinal));
            Assert.AreEqual(640, album?.Images?.ElementAt(0)?.Width);
            Assert.AreEqual(300, album?.Images?.ElementAt(1)?.Height);
            Assert.AreEqual(0, Uri.Compare(new Uri("https://i.scdn.co/image/2210b7d23f320a2cab2736bd3b3b948415dd21d8"), album?.Images?.ElementAt(1)?.Url, UriComponents.AbsoluteUri, UriFormat.Unescaped, StringComparison.Ordinal));
            Assert.AreEqual(300, album?.Images?.ElementAt(1)?.Width);
            Assert.AreEqual(64, album?.Images?.ElementAt(2)?.Height);
            Assert.AreEqual(0, Uri.Compare(new Uri("https://i.scdn.co/image/609153aca7f4760136d97fbaccdb4ec0757e4c9e"), album?.Images?.ElementAt(2)?.Url, UriComponents.AbsoluteUri, UriFormat.Unescaped, StringComparison.Ordinal));
            Assert.AreEqual(64, album?.Images?.ElementAt(2)?.Width);
            Assert.AreEqual("In & ut", album?.Name);
            Assert.AreEqual(49, album?.Popularity);
            Assert.AreEqual("2013-01-01", album?.ReleaseDate);
            Assert.AreEqual("day", album?.ReleaseDatePrecision);
            var savedTracks = album?.Tracks;
            Assert.AreEqual(0, Uri.Compare(new Uri("https://api.spotify.com/v1/albums/5m4VYOPoIpkV0XgOiRKkWC/tracks?offset=0&limit=50"), savedTracks?.Uri, UriComponents.AbsoluteUri, UriFormat.Unescaped, StringComparison.Ordinal));
            Assert.AreEqual(50, savedTracks?.Limit);
            Assert.IsNull(savedTracks?.Next);
            Assert.AreEqual(0, savedTracks?.Offset);
            Assert.IsNull(savedTracks?.Previous);
            Assert.AreEqual(13, savedTracks?.Total);
            Assert.AreEqual(2, savedTracks?.Items?.Count());
            Assert.AreEqual("album", album?.Type);
            Assert.AreEqual("spotify:album:5m4VYOPoIpkV0XgOiRKkWC", album?.Uri);
        }
    }
}