using System;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Syncify.Core.Models;

namespace Syncify.Core.Tests.Models
{
    [TestClass]
    public class TracksTests
    {
        private static readonly Assembly assembly = Assembly.GetExecutingAssembly();

        [TestMethod]
        public async Task WhenParsingJsonReturnsTracks()
        {
            using var stream = assembly.GetManifestResourceStream($"{assembly.GetName().Name}.Resources.Tracks.json");

            var page = await JsonSerializer.DeserializeAsync<Page<SavedTrack>>(stream);

            Assert.IsNotNull(page);
            Assert.AreEqual(0, Uri.Compare(new Uri("https://api.spotify.com/v1/me/tracks?offset=0&limit=20"), page.Uri, UriComponents.AbsoluteUri, UriFormat.Unescaped, StringComparison.Ordinal));
            Assert.AreEqual(20, page.Limit);
            Assert.AreEqual(0, Uri.Compare(new Uri("https://api.spotify.com/v1/me/tracks?offset=20&limit=20"), page.Next, UriComponents.AbsoluteUri, UriFormat.Unescaped, StringComparison.Ordinal));
            Assert.AreEqual(0, page.Offset);
            Assert.IsNull(page.Previous);
            Assert.AreEqual(53, page.Total);
            var savedTrack = page.Items.SingleOrDefault();
            Assert.IsNotNull(savedTrack);
            Assert.AreEqual(new DateTime(2016, 10, 24, 15, 3, 7), savedTrack.AddedAt);
            var track = savedTrack.Track;
            Assert.IsNotNull(track);
            Assert.AreEqual("album", track?.Album.AlbumType);
            Assert.AreEqual("spotify", track?.Album?.Artists?.SingleOrDefault().ExternalUrls?.SingleOrDefault().Key);
            Assert.AreEqual(0, Uri.Compare(new Uri("https://open.spotify.com/artist/0LIll5i3kwo5A3IDpipgkS"), track?.Album?.Artists?.SingleOrDefault().ExternalUrls?.SingleOrDefault().Value, UriComponents.AbsoluteUri, UriFormat.Unescaped, StringComparison.Ordinal));
            Assert.AreEqual(0, Uri.Compare(new Uri("https://api.spotify.com/v1/artists/0LIll5i3kwo5A3IDpipgkS"), track?.Album?.Artists?.SingleOrDefault().Url, UriComponents.AbsoluteUri, UriFormat.Unescaped, StringComparison.Ordinal));
            Assert.AreEqual("0LIll5i3kwo5A3IDpipgkS", track?.Album?.Artists?.SingleOrDefault()?.Id);
            Assert.AreEqual("Squirrel Nut Zippers", track?.Album?.Artists?.SingleOrDefault()?.Name);
            Assert.AreEqual("artist", track?.Album?.Artists?.SingleOrDefault()?.Type);
            Assert.AreEqual("spotify:artist:0LIll5i3kwo5A3IDpipgkS", track?.Album?.Artists?.SingleOrDefault()?.Uri);
            CollectionAssert.AreEqual(new string[] { "AD", "AR", "AT", "AU", "BE", "BG", "BO", "BR", "CH", "CL", "CO", "CR", "CY", "CZ", "DE", "DK", "DO", "EC", "EE", "ES", "FI", "FR", "GB", "GR", "GT", "HK", "HN", "HU", "ID", "IE", "IS", "IT", "JP", "LI", "LT", "LU", "LV", "MC", "MT", "MY", "NI", "NL", "NO", "NZ", "PA", "PE", "PH", "PL", "PT", "PY", "SE", "SG", "SK", "SV", "TR", "TW", "UY" }, track?.Album?.AvailableMarkets?.ToArray());
            Assert.AreEqual("spotify", track?.Album?.ExternalUrls?.SingleOrDefault().Key);
            Assert.AreEqual(0, Uri.Compare(new Uri("https://open.spotify.com/album/63GBbuUNBel2ovJjUrfh5r"), track?.Album?.ExternalUrls?.SingleOrDefault().Value, UriComponents.AbsoluteUri, UriFormat.Unescaped, StringComparison.Ordinal));
            Assert.AreEqual(0, Uri.Compare(new Uri("https://api.spotify.com/v1/albums/63GBbuUNBel2ovJjUrfh5r"), track?.Album?.Url, UriComponents.AbsoluteUri, UriFormat.Unescaped, StringComparison.Ordinal));
            Assert.AreEqual("63GBbuUNBel2ovJjUrfh5r", track?.Album?.Id);
            Assert.AreEqual(3, track?.Album?.Images?.Count());
            Assert.AreEqual(640, track?.Album?.Images?.ElementAt(0)?.Height);
            Assert.AreEqual(0, Uri.Compare(new Uri("https://i.scdn.co/image/e9c5fd63935b08ed27a7a5b0e65b2c6bf600fc4a"), track?.Album?.Images?.ElementAt(0)?.Url, UriComponents.AbsoluteUri, UriFormat.Unescaped, StringComparison.Ordinal));
            Assert.AreEqual(640, track?.Album?.Images?.ElementAt(0)?.Width);
            Assert.AreEqual(300, track?.Album?.Images?.ElementAt(1)?.Height);
            Assert.AreEqual(0, Uri.Compare(new Uri("https://i.scdn.co/image/416b6589d9e2d91147ff5072d640d0041b04cb41"), track?.Album?.Images?.ElementAt(1)?.Url, UriComponents.AbsoluteUri, UriFormat.Unescaped, StringComparison.Ordinal));
            Assert.AreEqual(300, track?.Album?.Images?.ElementAt(1)?.Width);
            Assert.AreEqual(64, track?.Album?.Images?.ElementAt(2)?.Height);
            Assert.AreEqual(0, Uri.Compare(new Uri("https://i.scdn.co/image/4bb6b451b8edde5881a5fcbe1a54bc8538f407ec"), track?.Album?.Images?.ElementAt(2)?.Url, UriComponents.AbsoluteUri, UriFormat.Unescaped, StringComparison.Ordinal));
            Assert.AreEqual(64, track?.Album?.Images?.ElementAt(2)?.Width);
            Assert.AreEqual("The Best of Squirrel Nut Zippers", track?.Album?.Name);
            Assert.AreEqual("album", track?.Album?.Type);
            Assert.AreEqual("spotify:album:63GBbuUNBel2ovJjUrfh5r", track?.Album?.Uri);
            Assert.AreEqual("spotify", track?.Artists?.SingleOrDefault().ExternalUrls?.SingleOrDefault().Key);
            Assert.AreEqual(0, Uri.Compare(new Uri("https://open.spotify.com/artist/0LIll5i3kwo5A3IDpipgkS"), track?.Artists?.SingleOrDefault().ExternalUrls?.SingleOrDefault().Value, UriComponents.AbsoluteUri, UriFormat.Unescaped, StringComparison.Ordinal));
            Assert.AreEqual(0, Uri.Compare(new Uri("https://api.spotify.com/v1/artists/0LIll5i3kwo5A3IDpipgkS"), track?.Artists?.SingleOrDefault().Url, UriComponents.AbsoluteUri, UriFormat.Unescaped, StringComparison.Ordinal));
            Assert.AreEqual("0LIll5i3kwo5A3IDpipgkS", track?.Artists?.SingleOrDefault()?.Id);
            Assert.AreEqual("Squirrel Nut Zippers", track?.Artists?.SingleOrDefault()?.Name);
            Assert.AreEqual("artist", track?.Artists?.SingleOrDefault()?.Type);
            Assert.AreEqual("spotify:artist:0LIll5i3kwo5A3IDpipgkS", track?.Artists?.SingleOrDefault()?.Uri);
            CollectionAssert.AreEqual(new string[] { "AD", "AR", "AT", "AU", "BE", "BG", "BO", "BR", "CH", "CL", "CO", "CR", "CY", "CZ", "DE", "DK", "DO", "EC", "EE", "ES", "FI", "FR", "GB", "GR", "GT", "HK", "HN", "HU", "ID", "IE", "IS", "IT", "JP", "LI", "LT", "LU", "LV", "MC", "MT", "MY", "NI", "NL", "NO", "NZ", "PA", "PE", "PH", "PL", "PT", "PY", "SE", "SG", "SK", "SV", "TR", "TW", "UY" }, track?.AvailableMarkets?.ToArray());
            Assert.AreEqual(1, track?.DiscNumber);
            Assert.AreEqual(137040, track?.Duration);
            Assert.AreEqual(false, track?.Explicit);
            Assert.AreEqual(1, track?.ExternalIds?.Count);
            Assert.AreEqual("isrc", track?.ExternalIds?.Single().Key);
            Assert.AreEqual("USMA20215185", track?.ExternalIds?.Single().Value);
            Assert.AreEqual("spotify", track?.ExternalUrls?.SingleOrDefault().Key);
            Assert.AreEqual(0, Uri.Compare(new Uri("https://open.spotify.com/track/2jpDioAB9tlYXMdXDK3BGl"), track?.ExternalUrls?.SingleOrDefault().Value, UriComponents.AbsoluteUri, UriFormat.Unescaped, StringComparison.Ordinal));
            Assert.AreEqual(0, Uri.Compare(new Uri("https://api.spotify.com/v1/tracks/2jpDioAB9tlYXMdXDK3BGl"), track?.Url, UriComponents.AbsoluteUri, UriFormat.Unescaped, StringComparison.Ordinal));
            Assert.AreEqual("2jpDioAB9tlYXMdXDK3BGl", track?.Id);
            Assert.AreEqual("Good Enough For Granddad", track?.Name);
            Assert.AreEqual(19, track?.Popularity);
            Assert.AreEqual(0, Uri.Compare(new Uri("https://p.scdn.co/mp3-preview/32cc6f7a3fca362dfcde753f0339f42539f15c9a"), track?.PreviewUrl, UriComponents.AbsoluteUri, UriFormat.Unescaped, StringComparison.Ordinal));
            Assert.AreEqual(1, track?.TrackNumber);
            Assert.AreEqual("track", track?.Type);
            Assert.AreEqual("spotify:track:2jpDioAB9tlYXMdXDK3BGl", track?.Uri);
        }
    }
}
