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
    public class ProfileTests
    {
        private static readonly Assembly assembly = Assembly.GetExecutingAssembly();

        [TestMethod]
        public async Task WhenParsingJsonReturnsProfile()
        {
            using var stream = assembly.GetManifestResourceStream($"{assembly.GetName().Name}.Resources.Profile.json");

            var profile = await JsonSerializer.DeserializeAsync<Profile>(stream);

            Assert.AreEqual("SE", profile?.Country);
            Assert.AreEqual("JM Wizzler", profile?.DisplayName);
            Assert.AreEqual("email@example.com", profile?.Email);
            Assert.AreEqual(1, profile?.ExternalUrls?.Count);
            Assert.AreEqual("spotify", profile?.ExternalUrls?.Keys?.Single());
            Assert.AreEqual(0, Uri.Compare(new Uri("https://open.spotify.com/user/wizzler"), profile?.ExternalUrls?.Values?.Single(), UriComponents.AbsoluteUri, UriFormat.Unescaped, StringComparison.Ordinal));
            Assert.IsNull(profile?.Followers?.Link);
            Assert.AreEqual(3829, profile?.Followers?.Total);
            Assert.AreEqual(0, Uri.Compare(new Uri("https://api.spotify.com/v1/users/wizzler"), profile.Url, UriComponents.AbsoluteUri, UriFormat.Unescaped, StringComparison.Ordinal));
            Assert.IsNull(profile?.Images?.Single().Height);
            Assert.IsNull(profile?.Images?.Single().Width);
            Assert.AreEqual(0, Uri.Compare(new Uri("https://fbcdn-profile-a.akamaihd.net/hprofile-ak-frc3/t1.0-1/1970403_10152215092574354_1798272330_n.jpg"), profile?.Images?.Single().Url, UriComponents.AbsoluteUri, UriFormat.Unescaped, StringComparison.Ordinal));
            Assert.AreEqual("premium", profile?.Product);
            Assert.AreEqual("user", profile?.Type);
            Assert.AreEqual("spotify:user:wizzler", profile?.Uri);
        }
    }
}
